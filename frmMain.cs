using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BRBLog
{
    public partial class frmMain : Form
    {
        private string templateCaminho = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "TemplateLogTabulação.txt");

        // Lista mestre com as opções separadas do PID Aleatório
        private readonly string[] opcoesPidOriginal = new string[]
        {
            "",
            "RG/CNH",
            "Possui um adicional? (Sim ou Não)",
            "Data do Vencimento da Fatura",
            "Valor da Ultima Compra (Valor Exato)",
            "Valor da Ultima Fatura (Valor Exato)",
            "Puro Crédito / Múltiplo"
        };

        // Variável de controle para evitar loop infinito entre os ComboBoxes do PID
        private bool atualizandoCombos = false;

        public frmMain()
        {
            InitializeComponent();
            ToolTips();
            InicializarControlesTelefone();
            AplicarPadraoVisual();

            // VINCULAÇÃO DOS EVENTOS DOS RADIOBUTTONS MANUALMENTE
            rdbSim.CheckedChanged += rdbSim_CheckedChanged;
            rdbNao.CheckedChanged += rdbNao_CheckedChanged;

            // Configuração dos estilos das ComboBoxes
            cbxPid1.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxPid2.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxBandeira.DropDownStyle = ComboBoxStyle.DropDownList;

            // Registrar eventos de alteração para os campos básicos e telefones
            mtbCPF.TextChanged += AtualizarPreVisualizacao;
            mtbCPF.TextChanged += MtbCPF_TextChanged;
            txtNome.TextChanged += AtualizarPreVisualizacao;
            cbxBandeira.SelectedIndexChanged += AtualizarPreVisualizacao;
            txtUltimos4Digitos.TextChanged += AtualizarPreVisualizacao;

            // Monitoramento dos campos de telefone e radios para preview em tempo real
            mtbCadastro.TextChanged += AtualizarPreVisualizacao;
            mtbContato.TextChanged += AtualizarPreVisualizacao;

            // Eventos dos PIDs Aleatórios (ComboBox e Respostas)
            cbxPid1.SelectedIndexChanged += CbxPid1_SelectedIndexChanged;
            cbxPid2.SelectedIndexChanged += CbxPid2_SelectedIndexChanged;

            txtRespPid1.TextChanged += AtualizarPreVisualizacao;
            txtRespPid2.TextChanged += AtualizarPreVisualizacao;

            // Monitorar Motivo e Tratativa
            rtbMotivo.TextChanged += AtualizarPreVisualizacao;
            rtbTratativa.TextChanged += AtualizarPreVisualizacao;
        }

        private void AplicarPadraoVisual()
        {
            if (btnSalvar != null)
            {
                btnSalvar.BackColor = Color.FromArgb(0, 102, 204);
                btnSalvar.ForeColor = Color.White;
                btnSalvar.FlatStyle = FlatStyle.Flat;
                btnSalvar.FlatAppearance.BorderSize = 0;
                btnSalvar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            }

            if (btnCopiarLog != null)
            {
                btnCopiarLog.BackColor = Color.FromArgb(0, 102, 204);
                btnCopiarLog.ForeColor = Color.White;
                btnCopiarLog.FlatStyle = FlatStyle.Flat;
                btnCopiarLog.FlatAppearance.BorderSize = 0;
                btnCopiarLog.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            }

            if (btnLimpar != null)
            {
                btnLimpar.BackColor = Color.FromArgb(108, 117, 125);
                btnLimpar.ForeColor = Color.White;
                btnLimpar.FlatStyle = FlatStyle.Flat;
                btnLimpar.FlatAppearance.BorderSize = 0;
                btnLimpar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            }
        }

        private void rdbSim_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSim.Checked)
            {
                mtbContato.Visible = false;
                lblTel.Visible = false;
                mtbContato.Clear();
            }
        }

        private void rdbNao_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNao.Checked)
            {
                mtbContato.Visible = true;
                lblTel.Visible = true;
                mtbContato.Focus();
            }
        }

        private void ToolTips()
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnSalvar, "Salvar atendimento no banco de dados.");
            toolTip.SetToolTip(btnCopiarLog, "Copiar log para a área de transferência.");
            toolTip.SetToolTip(btnLimpar, "Limpar todos os campos do formulário.");

            ToolTip toolTipTelefone = new ToolTip
            {
                AutoPopDelay = 15000,
                InitialDelay = 400,
                ReshowDelay = 500
            };

            string mensagemRestricoes = "ATENÇÃO: Operações NÃO permitidas com telefone diferente:\n\n" +
                                       "• LIMITES:\n" +
                                       "  - Overlimit\n" +
                                       "  - Alteração de Limite\n" +
                                       "  - Transferência de Limite\n\n" +
                                       "• CARTÕES:\n" +
                                       "  - Adicionar um cartão\n" +
                                       "  - Alterar status para 0-Ativo (Retirada de Bloqueio)\n\n" +
                                       "• DADOS CADASTRAIS:\n" +
                                       "  - Alterar dados de cadastro\n" +
                                       "  - Procurações\n" +
                                       "  - Cadastro de Curatela\n" +
                                       "  - Alterar Domicílio\n\n" +
                                       "• CARTEIRA DIGITAL:\n" +
                                       "  - Ativar Carteira Digital\n\n" +
                                       "• FUNÇÕES:\n" +
                                       "  - Habilitar compra internacional\n" +
                                       "  - Ativar aviso viagem\n" +
                                       "  - Ativar Contactless\n" +
                                       "  - Contratar telesaque";

            toolTipTelefone.SetToolTip(rdbNao, mensagemRestricoes);
        }

        private void InicializarControlesTelefone()
        {
            rdbSim.Checked = true;
            mtbContato.Visible = false;
            lblTel.Visible = false;

            mtbCadastro.Mask = "(00) 00000-0000";
            mtbContato.Mask = "(00) 00000-0000";

            mtbCadastro.PromptChar = '_';
            mtbContato.PromptChar = '_';

            mtbCadastro.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += FrmMain_KeyDown;

            if (this.lblValidaCPF != null)
                this.lblValidaCPF.Text = string.Empty;

            CarregarOpcoesPid();
            AtualizarPreVisualizacao(sender, e);

            this.mtbCPF.Focus();
            if (this.rtbCab != null)
            {
                this.rtbCab.Enabled = false;
            }

            rdbSim.Checked = true;
            mtbContato.Visible = false;
            lblTel.Visible = false;
            mtbCadastro.Clear();
        }

        private void CarregarOpcoesPid()
        {
            cbxPid1.DataSource = opcoesPidOriginal.ToArray();
            cbxPid2.DataSource = opcoesPidOriginal.ToArray();
        }

        private void CbxPid1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (atualizandoCombos) return;
            atualizandoCombos = true;

            string selecionado1 = cbxPid1.SelectedItem?.ToString();
            string selecionadoAtual2 = cbxPid2.SelectedItem?.ToString();

            var opcoesFiltradas = opcoesPidOriginal
                .Where(op => string.IsNullOrEmpty(op) || op != selecionado1)
                .ToArray();

            cbxPid2.DataSource = opcoesFiltradas;

            if (opcoesFiltradas.Contains(selecionadoAtual2))
            {
                cbxPid2.SelectedItem = selecionadoAtual2;
            }

            atualizandoCombos = false;
            AtualizarPreVisualizacao(sender, e);
        }

        private void CbxPid2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (atualizandoCombos) return;
            atualizandoCombos = true;

            string selecionado2 = cbxPid2.SelectedItem?.ToString();
            string selecionadoAtual1 = cbxPid1.SelectedItem?.ToString();

            var opcoesFiltradas = opcoesPidOriginal
                .Where(op => string.IsNullOrEmpty(op) || op != selecionado2)
                .ToArray();

            cbxPid1.DataSource = opcoesFiltradas;

            if (opcoesFiltradas.Contains(selecionadoAtual1))
            {
                cbxPid1.SelectedItem = selecionadoAtual1;
            }

            atualizandoCombos = false;
            AtualizarPreVisualizacao(sender, e);
        }

        private void AtualizarPreVisualizacao(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(templateCaminho))
                {
                    if (rtbCab != null) rtbCab.Text = "[Aviso: Arquivo 'TemplateLogTabulação.txt' não encontrado]";
                    return;
                }

                string textoTemplate = File.ReadAllText(templateCaminho);

                string cpf = mtbCPF.MaskCompleted ? mtbCPF.Text : string.Empty;
                string nome = txtNome.Text.Trim();
                string bandeira = cbxBandeira.SelectedItem?.ToString() ?? string.Empty;
                string ultimos = txtUltimos4Digitos.Text.Trim();

                string telCadastro = mtbCadastro.Text;
                string telContato = rdbSim.Checked ? mtbCadastro.Text : mtbContato.Text;

                string pid1 = cbxPid1.SelectedItem?.ToString() ?? string.Empty;
                string respPid1 = txtRespPid1.Text.Trim();
                string pid2 = cbxPid2.SelectedItem?.ToString() ?? string.Empty;
                string respPid2 = txtRespPid2.Text.Trim();
                string motivo = rtbMotivo.Text.Trim();
                string tratativa = rtbTratativa.Text.Trim();

                textoTemplate = textoTemplate.Replace("[PlaceHolderCPF]", cpf);
                textoTemplate = textoTemplate.Replace("[PlaceHolderNome]", nome);
                textoTemplate = textoTemplate.Replace("[PlaceHolderBandeira]", bandeira);
                textoTemplate = textoTemplate.Replace("[PlaceHolderUltimos]", ultimos);
                textoTemplate = textoTemplate.Replace("[PlaceHolderTelCadastro]", telCadastro);
                textoTemplate = textoTemplate.Replace("[PlaceHolderTelContato]", telContato);

                textoTemplate = textoTemplate.Replace("[PlaceHolderPid1]", pid1);
                textoTemplate = textoTemplate.Replace("[PlaceHolderRespostaPid1]", respPid1);
                textoTemplate = textoTemplate.Replace("[PlaceHolderPid2]", pid2);
                textoTemplate = textoTemplate.Replace("[PlaceHolderRespostaPid2]", respPid2);

                textoTemplate = textoTemplate.Replace("[PlaceHolderCliente]", nome);
                textoTemplate = textoTemplate.Replace("[PlaceHolderMotivo]", motivo);
                textoTemplate = textoTemplate.Replace("[PlaceHolderTratativa]", tratativa);

                if (rtbCab != null) rtbCab.Text = textoTemplate;
            }
            catch (Exception ex)
            {
                if (rtbCab != null) rtbCab.Text = "Erro ao processar template: " + ex.Message;
            }
        }

        private bool ValidarCPF(string cpf)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            switch (cpf)
            {
                case "00000000000":
                case "11111111111":
                case "22222222222":
                case "33333333333":
                case "44444444444":
                case "55555555555":
                case "66666666666":
                case "77777777777":
                case "88888888888":
                case "99999999999":
                    return false;
            }

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        private void MtbCPF_TextChanged(object sender, EventArgs e)
        {
            if (mtbCPF.MaskCompleted)
            {
                if (ValidarCPF(mtbCPF.Text))
                {
                    mtbCPF.BackColor = Color.LightGreen;
                    if (lblValidaCPF != null)
                    {
                        lblValidaCPF.ForeColor = Color.Green;
                        lblValidaCPF.Text = "CPF Válido";
                    }
                }
                else
                {
                    mtbCPF.BackColor = Color.LightPink;
                    if (lblValidaCPF != null)
                    {
                        lblValidaCPF.ForeColor = Color.Red;
                        lblValidaCPF.Text = "CPF Inválido";
                    }
                }
            }
            else
            {
                mtbCPF.BackColor = Color.White;
                if (lblValidaCPF != null)
                {
                    lblValidaCPF.Text = string.Empty;
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposPreenchidos()) return;

            if (string.IsNullOrWhiteSpace(mtbCadastro.Text) || mtbCadastro.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", "").Trim().Length < 10)
            {
                MessageBox.Show("O preenchimento do Telefone de Cadastro é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbCadastro.Focus();
                return;
            }

            string telCadastro = mtbCadastro.Text.Trim();
            string telContato = telCadastro;

            if (rdbNao.Checked)
            {
                if (string.IsNullOrWhiteSpace(mtbContato.Text) || mtbContato.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", "").Trim().Length < 10)
                {
                    MessageBox.Show("Como o telefone é diferente do cadastro, informe o Telefone de Contato (Tel.).", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mtbContato.Focus();
                    return;
                }
                telContato = mtbContato.Text.Trim();
            }

            string cpfOriginal = mtbCPF.Text;
            string nomeOriginal = txtNome.Text.Trim();
            string bandeira = cbxBandeira.SelectedItem?.ToString() ?? string.Empty;
            string ultimos = txtUltimos4Digitos.Text.Trim();
            string pid1 = cbxPid1.SelectedItem?.ToString() ?? string.Empty;
            string resp1 = txtRespPid1.Text.Trim();
            string pid2 = cbxPid2.SelectedItem?.ToString() ?? string.Empty;
            string resp2 = txtRespPid2.Text.Trim();
            string motivo = rtbMotivo.Text.Trim();
            string tratativa = rtbTratativa.Text.Trim();

            string cpfMascarado = BancoDados.MascararCpf(cpfOriginal);
            string nomeMascarado = BancoDados.MascararNome(nomeOriginal);
            string telCadastroMascarado = BancoDados.MascararTelefone(telCadastro);
            string telContatoMascarado = BancoDados.MascararTelefone(telContato);

            string resp1ParaBanco = resp1;
            if (!string.IsNullOrEmpty(pid1) && (pid1.IndexOf("RG", StringComparison.OrdinalIgnoreCase) >= 0 || pid1.IndexOf("CNH", StringComparison.OrdinalIgnoreCase) >= 0))
            {
                resp1ParaBanco = MascararRgCnh(resp1);
            }

            string resp2ParaBanco = resp2;
            if (!string.IsNullOrEmpty(pid2) && (pid2.IndexOf("RG", StringComparison.OrdinalIgnoreCase) >= 0 || pid2.IndexOf("CNH", StringComparison.OrdinalIgnoreCase) >= 0))
            {
                resp2ParaBanco = MascararRgCnh(resp2);
            }

            string logMascaradoParaBanco = string.Empty;
            if (File.Exists(templateCaminho))
            {
                string textoTemplate = File.ReadAllText(templateCaminho);
                logMascaradoParaBanco = textoTemplate
                    .Replace("[PlaceHolderCPF]", cpfMascarado)
                    .Replace("[PlaceHolderNome]", nomeMascarado)
                    .Replace("[PlaceHolderBandeira]", bandeira)
                    .Replace("[PlaceHolderUltimos]", ultimos)
                    .Replace("[PlaceHolderTelCadastro]", telCadastroMascarado)
                    .Replace("[PlaceHolderTelContato]", telContatoMascarado)
                    .Replace("[PlaceHolderPid1]", pid1)
                    .Replace("[PlaceHolderRespostaPid1]", resp1ParaBanco)
                    .Replace("[PlaceHolderPid2]", pid2)
                    .Replace("[PlaceHolderRespostaPid2]", resp2ParaBanco)
                    .Replace("[PlaceHolderCliente]", nomeMascarado)
                    .Replace("[PlaceHolderMotivo]", motivo)
                    .Replace("[PlaceHolderTratativa]", tratativa);
            }

            BancoDados.SalvarLog(
                cpfOriginal, nomeOriginal, bandeira, ultimos,
                pid1, resp1ParaBanco, pid2, resp2ParaBanco, motivo, tratativa,
                telCadastro, telContato, logMascaradoParaBanco
            );

            MessageBox.Show("Atendimento salvo com sucesso no banco de dados!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

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

        private void btnCopiarLog_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposPreenchidos()) return;

            if (rtbCab != null && !string.IsNullOrEmpty(rtbCab.Text))
            {
                Clipboard.SetText(rtbCab.Text);
                MessageBox.Show("Log copiado para a área de transferência!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool ValidarCamposPreenchidos()
        {
            if (!mtbCPF.MaskCompleted || (lblValidaCPF != null && lblValidaCPF.Text == "CPF Inválido"))
            {
                MessageBox.Show("Por favor, informe um CPF válido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbCPF.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O campo 'Nome Completo' é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return false;
            }

            if (cbxBandeira.SelectedIndex == -1 || string.IsNullOrEmpty(cbxBandeira.SelectedItem?.ToString()))
            {
                MessageBox.Show("Selecione a 'Bandeira' do cartão.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxBandeira.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUltimos4Digitos.Text) || txtUltimos4Digitos.Text.Length < 4)
            {
                MessageBox.Show("Informe os 'Últimos 4 Dígitos' do cartão.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUltimos4Digitos.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(mtbCadastro.Text) || mtbCadastro.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", "").Trim().Length < 10)
            {
                MessageBox.Show("O preenchimento do 'Telefone de Cadastro' é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbCadastro.Focus();
                return false;
            }

            if (rdbNao.Checked)
            {
                if (string.IsNullOrWhiteSpace(mtbContato.Text) || mtbContato.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", "").Trim().Length < 10)
                {
                    MessageBox.Show("Como o telefone de contato é diferente do cadastro, informe o número corretamente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mtbContato.Focus();
                    return false;
                }
            }

            if (cbxPid1.SelectedIndex == -1 || string.IsNullOrEmpty(cbxPid1.SelectedItem?.ToString()))
            {
                MessageBox.Show("Selecione a opção do 'PID Opcional 1'.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxPid1.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtRespPid1.Text))
            {
                MessageBox.Show("Preencha a resposta do 'PID Opcional 1'.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRespPid1.Focus();
                return false;
            }

            if (cbxPid2.SelectedIndex == -1 || string.IsNullOrEmpty(cbxPid2.SelectedItem?.ToString()))
            {
                MessageBox.Show("Selecione a opção do 'PID Opcional 2'.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxPid2.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtRespPid2.Text))
            {
                MessageBox.Show("Preencha a resposta do 'PID Opcional 2'.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRespPid2.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(rtbMotivo.Text))
            {
                MessageBox.Show("O campo 'Motivo do Contato' é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rtbMotivo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(rtbTratativa.Text))
            {
                MessageBox.Show("O campo 'Tratativa Realizada' é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rtbTratativa.Focus();
                return false;
            }

            return true;
        }

        private void LimparFormulario()
        {
            mtbCPF.Clear();
            txtNome.Clear();
            txtUltimos4Digitos.Clear();
            txtRespPid1.Clear();
            txtRespPid2.Clear();
            rtbMotivo.Clear();
            rtbTratativa.Clear();

            mtbCadastro.Clear();
            mtbContato.Clear();
            rdbSim.Checked = true;
            mtbContato.Visible = false;
            lblTel.Visible = false;

            cbxBandeira.SelectedIndex = -1;
            CarregarOpcoesPid();

            mtbCPF.BackColor = Color.White;
            if (lblValidaCPF != null) lblValidaCPF.Text = string.Empty;

            mtbCPF.Focus();
        }

        private void PreencherDadosTeste()
        {
            atualizandoCombos = true;
            CarregarOpcoesPid();

            mtbCPF.Text = "09208332926";
            txtNome.Text = "Jackson Antonio de Andrade";
            cbxBandeira.SelectedItem = "Mastercard";
            txtUltimos4Digitos.Text = "1749";
            mtbCadastro.Text = "41996131749";

            cbxPid1.SelectedItem = "RG/CNH";
            txtRespPid1.Text = "8445142";

            cbxPid2.SelectedItem = "Data do Vencimento da Fatura";
            txtRespPid2.Text = "15";

            rtbMotivo.Text = "Cliente entra em contato informando que seu cartão está bloqueado";
            rtbTratativa.Text = "Cliente ciente do arrasto de bloqueio realizado no cartão";

            atualizandoCombos = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "Deseja realmente limpar todos os campos do atendimento?",
                "Confirmar Limpeza",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.OK)
            {
                LimparFormulario();
            }
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1))
            {
                e.SuppressKeyPress = true;
                SelecionarItemDireto(cbxBandeira, "Visa");
                PreencherPidsAutomaticamente();
                AtualizarPreVisualizacao(sender, e);
            }
            else if (e.Control && (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2))
            {
                e.SuppressKeyPress = true;
                SelecionarItemDireto(cbxBandeira, "Mastercard");
                PreencherPidsAutomaticamente();
                AtualizarPreVisualizacao(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.S) 
            {
                e.SuppressKeyPress = true;
                btnSalvar.PerformClick(); 
            }
            else if (e.Control && (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0))
            {
                e.SuppressKeyPress = true;

                string usuarioAtual = Environment.UserName;
                bool usuarioPermitido = string.Equals(usuarioAtual, "Jackson", StringComparison.OrdinalIgnoreCase) ||
                                        string.Equals(usuarioAtual, "andrade.jackson", StringComparison.OrdinalIgnoreCase);

                if (usuarioPermitido)
                {
                    PreencherDadosTeste();
                    AtualizarPreVisualizacao(sender, e);
                }
                else
                {
                    MessageBox.Show("Atalho restrito.", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void PreencherPidsAutomaticamente()
        {
            atualizandoCombos = true;
            try
            {
                SelecionarItemDireto(cbxPid1, "Possui um adicional");

                string selecionado1 = cbxPid1.SelectedItem?.ToString();
                var opcoesFiltradas = opcoesPidOriginal
                    .Where(op => string.IsNullOrEmpty(op) || op != selecionado1)
                    .ToArray();

                cbxPid2.DataSource = opcoesFiltradas;
                SelecionarItemDireto(cbxPid2, "vencimento");
            }
            finally
            {
                atualizandoCombos = false;
            }
        }

        private void SelecionarItemDireto(ComboBox combo, string textoProcurado)
        {
            string termoBusca = textoProcurado.ToLower();

            foreach (var item in combo.Items)
            {
                if (item != null)
                {
                    string textoItem = item.ToString().ToLower();
                    if (textoItem.Contains(termoBusca))
                    {
                        combo.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void cbxPid1_SelectedIndexChanged_1(object sender, EventArgs e) { }
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton2_CheckedChanged(object sender, EventArgs e) { }
    }
}