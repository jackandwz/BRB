using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BRBLog
{
    public static class BancoDados
    {
        private static string connectionString = "Data Source=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "brblog.db") + ";";

        // Exclui o Log selecionado corrigido para usar SQLiteConnection
        public static void ExcluirLog(int idRegistro)
        {
            try
            {
                using (var conexao = new SQLiteConnection(connectionString))
                {
                    conexao.Open();
                    string query = "DELETE FROM LogsAtendimento WHERE Id = @id";
                    using (var comando = new SQLiteCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@id", idRegistro);
                        comando.ExecuteNonQuery();
                    }
                }
                RegistrarAuditoria(Environment.UserName, "EXCLUIR_LOG", $"Atendimento ID {idRegistro} excluído.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir o registro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Mascara o telefone mantendo apenas os últimos 4 dígitos (ex: (**) ****-1749)
        public static string MascararTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone)) return telefone;
            string apenasDigitos = Regex.Replace(telefone, @"[^\d]", "");
            if (apenasDigitos.Length >= 4)
            {
                string ultimos4 = apenasDigitos.Substring(apenasDigitos.Length - 4);
                return $"(**) ****-{ultimos4}";
            }
            return "(**) ****-****";
        }

        // Mascara RG/CNH mantendo apenas os 3 últimos dígitos visíveis (ex: ********-123)
        public static string MascararRgCnh(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento)) return documento;
            string apenasDigitos = Regex.Replace(documento, @"[^\d]", "");
            if (apenasDigitos.Length >= 3)
            {
                string ultimos3 = apenasDigitos.Substring(apenasDigitos.Length - 3);
                return new string('*', Math.Max(0, documento.Length - 3)) + ultimos3;
            }
            return "***";
        }

        // Expurga registros com mais de 30 dias da base local
        public static void ExpurgarLogsAntigos()
        {
            try
            {
                using (var conexao = new SQLiteConnection(connectionString))
                {
                    conexao.Open();
                    string sql = "DELETE FROM LogsAtendimento WHERE datetime(DataHora) < datetime('now', '-30 days')";

                    using (var comando = new SQLiteCommand(sql, conexao))
                    {
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erro ao expurgar logs antigos: " + ex.Message);
            }
        }

        // Método privado para gerar o Hash SHA-256 da senha
        private static string GerarHashSha256(string texto)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        #region Regras de Mascaramento LGPD

        // Mascara o CPF mantendo apenas os 3 primeiros dígitos (ex: 123.***.***-**)
        public static string MascararCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return cpf;
            string apenasDigitos = Regex.Replace(cpf, @"[^\d]", "");
            if (apenasDigitos.Length >= 3)
            {
                return $"{apenasDigitos.Substring(0, 3)}.***.***-**";
            }
            return "***.***.***-**";
        }

        // Mascara o Nome mantendo apenas o primeiro e o último nome (ex: João *** Silva)
        public static string MascararNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) return nome;
            string[] partes = nome.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (partes.Length == 1)
                return partes[0];

            if (partes.Length == 2)
                return $"{partes[0]} {partes[1].Substring(0, 1)}***";

            return $"{partes[0]} *** {partes[partes.Length - 1]}";
        }

        #endregion

        #region Registro de Auditoria (Audit Trails - LGPD)

        public static void RegistrarAuditoria(string loginRede, string acao, string detalhes = null)
        {
            try
            {
                using (var conexao = new SQLiteConnection(connectionString))
                {
                    conexao.Open();
                    string sql = @"INSERT INTO AuditLog (LoginRede, Acao, Detalhes, DataHora) 
                                    VALUES (@loginRede, @acao, @detalhes, @dataHora)";

                    using (var comando = new SQLiteCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@loginRede", loginRede);
                        comando.Parameters.AddWithValue("@acao", acao);
                        comando.Parameters.AddWithValue("@detalhes", detalhes ?? (object)DBNull.Value);
                        comando.Parameters.AddWithValue("@dataHora", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Silencia falhas secundárias de log
            }
        }

        #endregion

        public static void InicializarBanco()
        {
            try
            {
                using (var conexao = new SQLiteConnection(connectionString))
                {
                    conexao.Open();

                    string sql = @"
                        CREATE TABLE IF NOT EXISTS LogsAtendimento (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            DataHora TEXT NOT NULL,
                            Cpf TEXT,
                            Nome TEXT,
                            Bandeira TEXT,
                            UltimosDigitos TEXT,
                            Pid1 TEXT,
                            RespPid1 TEXT,
                            Pid2 TEXT,
                            RespPid2 TEXT,
                            Motivo TEXT,
                            Tratativa TEXT,
                            TelCadastro TEXT,
                            TelContato TEXT,
                            LogCompleto TEXT
                        );

                        CREATE TABLE IF NOT EXISTS Usuarios (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            NomeCompleto TEXT NOT NULL,
                            Username TEXT UNIQUE NOT NULL,
                            SenhaHash TEXT NOT NULL,
                            Perfil TEXT NOT NULL DEFAULT 'Operador',
                            Ativo INTEGER DEFAULT 1,
                            DataCriacao TEXT DEFAULT CURRENT_TIMESTAMP
                        );

                        CREATE TABLE IF NOT EXISTS AuditLog (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            UsuarioId INTEGER,
                            LoginRede TEXT NOT NULL,
                            Acao TEXT NOT NULL,
                            Detalhes TEXT,
                            DataHora TEXT DEFAULT CURRENT_TIMESTAMP,
                            EnderecoIP TEXT,
                            FOREIGN KEY(UsuarioId) REFERENCES Usuarios(Id)
                        );";

                    using (var comando = new SQLiteCommand(sql, conexao))
                    {
                        comando.ExecuteNonQuery();
                    }

                    AdicionarColunaSeNaoExiste(conexao, "LogsAtendimento", "TelCadastro", "TEXT");
                    AdicionarColunaSeNaoExiste(conexao, "LogsAtendimento", "TelContato", "TEXT");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inicializar o banco de dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void AdicionarColunaSeNaoExiste(SQLiteConnection conexao, string tabela, string coluna, string tipo)
        {
            using (var cmd = new SQLiteCommand($"PRAGMA table_info({tabela});", conexao))
            using (var reader = cmd.ExecuteReader())
            {
                bool existe = false;
                while (reader.Read())
                {
                    if (reader["name"].ToString().Equals(coluna, StringComparison.OrdinalIgnoreCase))
                    {
                        existe = true;
                        break;
                    }
                }

                if (!existe)
                {
                    using (var alterCmd = new SQLiteCommand($"ALTER TABLE {tabela} ADD COLUMN {coluna} {tipo};", conexao))
                    {
                        alterCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void SalvarLog(string cpf, string nome, string bandeira, string ultimos,
                                    string pid1, string resp1, string pid2, string resp2,
                                    string motivo, string tratativa, string telCadastro, string telContato, string logCompleto)
        {
            try
            {
                string cpfMascarado = MascararCpf(cpf);
                string nomeMascarado = MascararNome(nome);

                using (var conexao = new SQLiteConnection(connectionString))
                {
                    conexao.Open();
                    string sql = @"
                        INSERT INTO LogsAtendimento 
                        (DataHora, Cpf, Nome, Bandeira, UltimosDigitos, Pid1, RespPid1, Pid2, RespPid2, Motivo, Tratativa, TelCadastro, TelContato, LogCompleto) 
                        VALUES 
                        (@DataHora, @Cpf, @Nome, @Bandeira, @UltimosDigitos, @Pid1, @Resp1, @Pid2, @Resp2, @Motivo, @Tratativa, @TelCadastro, @TelContato, @LogCompleto)";

                    using (var comando = new SQLiteCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@DataHora", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        comando.Parameters.AddWithValue("@Cpf", cpfMascarado);
                        comando.Parameters.AddWithValue("@Nome", nomeMascarado);
                        comando.Parameters.AddWithValue("@Bandeira", bandeira);
                        comando.Parameters.AddWithValue("@UltimosDigitos", ultimos);
                        comando.Parameters.AddWithValue("@Pid1", pid1);
                        comando.Parameters.AddWithValue("@Resp1", resp1);
                        comando.Parameters.AddWithValue("@Pid2", pid2);
                        comando.Parameters.AddWithValue("@Resp2", resp2);
                        comando.Parameters.AddWithValue("@Motivo", motivo);
                        comando.Parameters.AddWithValue("@Tratativa", tratativa);
                        comando.Parameters.AddWithValue("@TelCadastro", string.IsNullOrEmpty(telCadastro) ? "" : telCadastro);
                        comando.Parameters.AddWithValue("@TelContato", string.IsNullOrEmpty(telContato) ? "Igual ao Cadastro" : telContato);
                        comando.Parameters.AddWithValue("@LogCompleto", logCompleto);

                        comando.ExecuteNonQuery();
                    }
                }

                RegistrarAuditoria(Environment.UserName, "NOVO_LOG_ATENDIMENTO", "Atendimento salvo com dados LGPD mascarados e telefones tabulados.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar o registro no banco: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static DataTable ObterTodosLogs()
        {
            try
            {
                using (var conexao = new SQLiteConnection(connectionString))
                {
                    conexao.Open();
                    string sql = "SELECT Id, DataHora, Cpf, Nome, Bandeira, LogCompleto FROM LogsAtendimento ORDER BY Id DESC";
                    using (var adaptador = new SQLiteDataAdapter(sql, conexao))
                    {
                        DataTable tabela = new DataTable();
                        adaptador.Fill(tabela);
                        return tabela;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar o banco de dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static DataTable PesquisarLogs(string cpf, string nome, DateTime dataInicio, DateTime dataFim, bool filtrarPorData)
        {
            try
            {
                using (var conexao = new SQLiteConnection(connectionString))
                {
                    conexao.Open();
                    string query = "SELECT Id, DataHora, Cpf, Nome, Bandeira, LogCompleto FROM LogsAtendimento WHERE 1=1";

                    if (!string.IsNullOrEmpty(cpf))
                        query += " AND Cpf LIKE @cpf";

                    if (!string.IsNullOrEmpty(nome))
                        query += " AND (Nome LIKE @nome OR LogCompleto LIKE @nomeCompleto)";

                    if (filtrarPorData)
                    {
                        query += " AND date(DataHora) BETWEEN date(@dataInicio) AND date(@dataFim)";
                    }

                    query += " ORDER BY Id DESC";

                    using (var cmd = new SQLiteCommand(query, conexao))
                    {
                        if (!string.IsNullOrEmpty(cpf))
                            cmd.Parameters.AddWithValue("@cpf", cpf.Trim() + "%");

                        if (!string.IsNullOrEmpty(nome))
                        {
                            cmd.Parameters.AddWithValue("@nome", "%" + nome.Trim() + "%");
                            cmd.Parameters.AddWithValue("@nomeCompleto", "%" + nome.Trim() + "%");
                        }

                        if (filtrarPorData)
                        {
                            cmd.Parameters.AddWithValue("@dataInicio", dataInicio.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@dataFim", dataFim.ToString("yyyy-MM-dd"));
                        }

                        using (var adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar os logs: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static void LimparBaseDeDados()
        {
            try
            {
                using (var conexao = new SQLiteConnection(connectionString))
                {
                    conexao.Open();
                    string sql = "DELETE FROM LogsAtendimento; VACUUM;";

                    using (var comando = new SQLiteCommand(sql, conexao))
                    {
                        comando.ExecuteNonQuery();
                    }
                }
                RegistrarAuditoria(Environment.UserName, "LIMPEZA_BASE_DADOS", "Base de dados de atendimento limpa.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao limpar a base de dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool UsuarioExiste(string username)
        {
            try
            {
                using (var conexao = new SQLiteConnection(connectionString))
                {
                    conexao.Open();
                    string sql = "SELECT COUNT(1) FROM Usuarios WHERE Username = @username AND Ativo = 1";
                    using (var comando = new SQLiteCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@username", username);
                        long resultado = (long)comando.ExecuteScalar();
                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar usuário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CadastrarUsuario(string nomeCompleto, string username, string senhaPura)
        {
            try
            {
                string senhaHash = GerarHashSha256(senhaPura);

                using (var conexao = new SQLiteConnection(connectionString))
                {
                    conexao.Open();
                    string sql = @"INSERT INTO Usuarios (NomeCompleto, Username, SenhaHash, Perfil) 
                                   VALUES (@nome, @username, @senhaHash, 'Operador')";

                    using (var comando = new SQLiteCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@nome", nomeCompleto);
                        comando.Parameters.AddWithValue("@username", username);
                        comando.Parameters.AddWithValue("@senhaHash", senhaHash);
                        comando.ExecuteNonQuery();
                    }
                }

                RegistrarAuditoria(username, "CADASTRO_PRIMEIRO_ACESSO", "Primeiro acesso de usuário configurado.");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar usuário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool ValidarLogin(string username, string senhaPura)
        {
            try
            {
                string senhaHash = GerarHashSha256(senhaPura);

                using (var conexao = new SQLiteConnection(connectionString))
                {
                    conexao.Open();
                    string sql = "SELECT COUNT(1) FROM Usuarios WHERE Username = @username AND SenhaHash = @senhaHash AND Ativo = 1";
                    using (var comando = new SQLiteCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@username", username);
                        comando.Parameters.AddWithValue("@senhaHash", senhaHash);
                        long resultado = (long)comando.ExecuteScalar();

                        bool sucesso = resultado > 0;
                        if (sucesso)
                        {
                            RegistrarAuditoria(username, "LOGIN_SUCESSO", "Autenticação bem-sucedida.");
                        }
                        else
                        {
                            RegistrarAuditoria(username, "LOGIN_FALHA", "Tentativa de login com senha incorreta.");
                        }

                        return sucesso;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao validar login: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static DataTable ObterLogsDoDia()
        {
            try
            {
                using (var conexao = new SQLiteConnection(connectionString))
                {
                    conexao.Open();
                    string dataHoje = DateTime.Now.ToString("yyyy-MM-dd");
                    string sql = "SELECT Id, DataHora, Cpf, Nome, Bandeira, LogCompleto FROM LogsAtendimento WHERE date(DataHora) = @dataHoje ORDER BY Id DESC";

                    using (var comando = new SQLiteCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@dataHoje", dataHoje);

                        using (var adaptador = new SQLiteDataAdapter(comando))
                        {
                            DataTable tabela = new DataTable();
                            adaptador.Fill(tabela);
                            return tabela;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os atendimentos do dia: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}