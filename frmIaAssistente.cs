using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BRBLog
{
    public class frmIaAssistente : Form
    {
        private string caminhoBaseConhecimento = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "BaseConhecimento.txt");

        private RichTextBox rtbChat;
        private TextBox txtPergunta;
        private Button btnEnviar;
        private Button btnAprender;
        private Label lblInstrucao;

        public frmIaAssistente()
        {
            InitializeComponentCustomizado();
            GarantirBaseInicial();
            CarregarBoasVindas();

            this.Shown += FrmIaAssistente_Shown;
        }

        private void InitializeComponentCustomizado()
        {
            this.Text = "Assistente IA - BRBLog (Suporte Operacional Definitivo)";
            this.Size = new Size(750, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            lblInstrucao = new Label
            {
                Text = "Digite sua dúvida ou procedimento abaixo:",
                Location = new Point(15, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40)
            };

            rtbChat = new RichTextBox
            {
                Location = new Point(15, 45),
                Size = new Size(705, 395),
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F),
                TabStop = false,
                BorderStyle = BorderStyle.FixedSingle
            };

            txtPergunta = new TextBox
            {
                Location = new Point(15, 460),
                Size = new Size(505, 27),
                Font = new Font("Segoe UI", 10F),
                TabIndex = 0
            };
            txtPergunta.KeyDown += TxtPergunta_KeyDown;

            // Botão Enviar Padronizado (Azul Corporativo)
            btnEnviar = new Button
            {
                Text = "Enviar 🚀",
                Location = new Point(526, 458),
                Size = new Size(95, 32),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                TabIndex = 1
            };
            btnEnviar.FlatAppearance.BorderSize = 0;
            btnEnviar.Click += BtnEnviar_Click;

            // Botão Ensinar IA Padronizado (Cinza de Suporte / Destacado)
            btnAprender = new Button
            {
                Text = "➕ Ensinar IA",
                Location = new Point(627, 458),
                Size = new Size(93, 32),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                TabIndex = 2
            };
            btnAprender.FlatAppearance.BorderSize = 0;
            btnAprender.Click += BtnAprender_Click;

            this.Controls.Add(lblInstrucao);
            this.Controls.Add(rtbChat);
            this.Controls.Add(txtPergunta);
            this.Controls.Add(btnEnviar);
            this.Controls.Add(btnAprender);
        }

        private void FrmIaAssistente_Shown(object sender, EventArgs e)
        {
            txtPergunta.Focus();
            txtPergunta.Select();
        }

        private void CarregarBoasVindas()
        {
            AdicionarMensagemChat("🤖 Assistente IA", "Olá! Estou pronto para auxiliar nos atendimentos. Digite sua dúvida sobre PID, senhas, cartões bloqueados ou procedimentos.");
        }

        private void AdicionarMensagemChat(string remetente, string mensagem)
        {
            rtbChat.SelectionStart = rtbChat.TextLength;
            rtbChat.SelectionLength = 0;

            if (remetente.Contains("IA"))
            {
                rtbChat.SelectionColor = Color.FromArgb(0, 80, 160);
                rtbChat.SelectionFont = new Font("Segoe UI", 10F, FontStyle.Bold);
                rtbChat.AppendText($"{remetente}:\n");

                rtbChat.SelectionColor = Color.FromArgb(40, 40, 40);
                rtbChat.SelectionFont = new Font("Segoe UI", 10F, FontStyle.Regular);
                rtbChat.AppendText($"{mensagem}\n\n");
            }
            else
            {
                rtbChat.SelectionColor = Color.FromArgb(0, 128, 0);
                rtbChat.SelectionFont = new Font("Segoe UI", 10F, FontStyle.Bold);
                rtbChat.AppendText($"{remetente}:\n");

                rtbChat.SelectionColor = Color.FromArgb(40, 40, 40);
                rtbChat.SelectionFont = new Font("Segoe UI", 10F, FontStyle.Regular);
                rtbChat.AppendText($"{mensagem}\n\n");
            }

            rtbChat.ScrollToCaret();
        }

        private void TxtPergunta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ProcessarEnvio();
            }
        }

        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            ProcessarEnvio();
        }

        private void ProcessarEnvio()
        {
            string pergunta = txtPergunta.Text.Trim();
            if (string.IsNullOrEmpty(pergunta)) return;

            AdicionarMensagemChat("👤 Você", pergunta);
            txtPergunta.Clear();

            string resposta = BuscarRespostaNaBase(pergunta);

            if (!string.IsNullOrEmpty(resposta))
            {
                AdicionarMensagemChat("🤖 Assistente IA", resposta);
            }
            else
            {
                AdicionarMensagemChat("🤖 Assistente IA", "Não encontrei uma diretriz exata para isso na base atual. Caso conheça o procedimento correto, clique em 'Ensinar IA' ao lado para retro-alimentar o sistema!");
            }

            txtPergunta.Focus();
        }

        private void GarantirBaseInicial()
        {
            string diretorioTemplates = Path.GetDirectoryName(caminhoBaseConhecimento);
            if (!Directory.Exists(diretorioTemplates)) Directory.CreateDirectory(diretorioTemplates);

            if (!File.Exists(caminhoBaseConhecimento))
            {
                File.WriteAllLines(caminhoBaseConhecimento, ObterBasePadraoCompleta());
            }
        }

        private List<string> ObterBasePadraoCompleta()
        {
            return new List<string>
            {
                "Dados de PID obrigatórios|Os obrigatórios (se errar 1, encerra o atendimento) são: CPF, Nome Completo, Bandeira e Últimos 4 dígitos do cartão.",
                "Dados de PID opcionais|Os opcionais (pode errar no máximo 1) são: RG/CNH, Possui um adicional? (Sim ou Não), Data do Vencimento da Fatura, Valor da Última compra (Valor exato), Valor da última fatura (Valor exato) e se é Puro Crédito ou Múltiplo.",
                "Regras de senhas dos cartões|Puro Crédito: 4 dígitos. Múltiplo: 6 dígitos (a senha do débito é a mesma da senha do crédito).",
                "Telefone deve bater exatamente com o cadastro|Necessário para Limites (Overlimit, Alteração de Limite, Transferência de Limite), Cartões (Adicionar Cartão, Alterar Status para Normal/Retirada de Bloqueio), Acesso (Alteração de Dados Cadastrais, Procurações, Cadastro de Curatela, Alterar Domicílio), Digital (Ativar Carteira Digital) e Compras (Habilitar Compras Internacionais, Ativar Aviso Viagem, Ativar Contactless, Contratar Telesaque).",
                "Capital de Giro Prova|Ocorre quando o cartão BRB é passado em maquininha BRB ou de outra empresa contendo o CPF do titular igual ao dono da maquininha. Gera Bloqueio Cod 27. Script: Apresentação, Sondar Cod 27, PID, Conferir Dados, Checar na Linha do Tempo do cartão, Ler e coletar o aceite do cliente, Tabular. Prazo: 5 dias para aguardar; se vencer, aguardar Prevenção de Fraude retornar. Jamais voltar para status 0-Ativo (apenas N3 pode). Citar Cláusula 9.1 F (Opcional): Clientes Dux (D+1), Clientes de outros cartões (D+5).",
                "Cartão novo chega bloqueado Cod 2|Cod 2 significa Bloqueio de Fábrica. Orientar o cliente a utilizar Chip e senha na primeira compra para ativar o cartão. Efetuar o PID e desbloquear. Atenção: Somente pelo número cadastrado.",
                "Senha incorreta Cod 33|O número de telefone precisa bater com o cadastro e é obrigatório realizar o PID.",
                "Segunda via de cartao custos|Fraude não tem custo. Para os demais casos: Dux custa R$ 340 e Outros custam R$ 17. O N1 solicita e o cliente acompanha. Tabulação: Solicitar Segunda Via (de acordo com o tipo: Cancelado Perda, Extravio, Danificado ou Roubo).",
                "Cancelamento definitivo CRM|Menu 'Contas Cartão'. É diferente de bloquear/cancelar um cartão comum ou pedir segunda via. O cancelamento definitivo é realizado junto ao N2 em linha (transferido para retenção). Tabulação: Transferir Atendimento -> Central Retenção, incluindo o motivo do cancelamento no log manual.",
                "Esquecimento de senha e ver senha|Em caso de esquecimento no cartão Múltiplo, informar que a senha do débito é a mesma do crédito e orientar ir ao banco efetuar a troca (não atendemos BRB Banco, somente Card). Para ver a senha no BRB Card (Crédito): Meu Cartão -> Configurações -> Ver senha -> Realizar Facial (4 Dígitos).",
                "Compra negada cartao bloqueado suspeita de fraude|Jamais utilizar o termo 'Falcon'. Usar no lugar: 'Medida de Segurança', 'Bloqueio de Segurança' ou 'Sistema de Anti-fraude'. O BRB N1 não emite novo cartão (a emissão é gerada ao fim da tabulação). Se o cliente responder 'Sim' à verificação: desbloqueia normalmente. Se responder 'Não': fraude detectada, gera novo cartão, cancela o antigo automaticamente sem custo. Se não responder: fica bloqueado por segurança. Script: Fazer PID, sondar o cliente, orientar a responder a mensagem, caso contrário pedir para entrar em contato via Chat ou WhatsApp. Tabulação: Transferências -> Transferências Negado Falcon.",
                "Como usar os atalhos de bandeira|Pressione Ctrl + M para selecionar Mastercard e preencher os PIDs automaticamente, ou Ctrl + V para Visa."
            };
        }

        private string BuscarRespostaNaBase(string termoBusca)
        {
            var linhas = ObterBasePadraoCompleta();

            if (File.Exists(caminhoBaseConhecimento))
            {
                var linhasArquivo = File.ReadAllLines(caminhoBaseConhecimento);
                linhas.AddRange(linhasArquivo);
            }

            var resultadosEncontrados = new List<string>();

            string buscaNormalizada = NormalizarTexto(termoBusca);
            if (string.IsNullOrWhiteSpace(buscaNormalizada)) return null;

            foreach (var linha in linhas)
            {
                if (string.IsNullOrWhiteSpace(linha) || !linha.Contains("|")) continue;

                var partes = linha.Split('|');
                string perguntaBase = partes[0].Trim();
                string respostaBase = partes[1].Trim();

                string linhaCompletaNormalizada = NormalizarTexto($"{perguntaBase} {respostaBase}");

                if (linhaCompletaNormalizada.Contains(buscaNormalizada))
                {
                    resultadosEncontrados.Add($"📌 **{perguntaBase}**\n{respostaBase}");
                }
            }

            if (resultadosEncontrados.Count > 0)
            {
                return string.Join("\n\n----------------------------------------\n\n", resultadosEncontrados.Take(3));
            }

            return null;
        }

        private string NormalizarTexto(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return string.Empty;

            string textoLower = texto.ToLower().Trim();

            string comAcento = "áàãâäéèêëíìîïóòõôöúùûüç";
            string semAcento = "aaaaaeeeeiiiiooooouuuuc";

            for (int i = 0; i < comAcento.Length; i++)
            {
                textoLower = textoLower.Replace(comAcento[i], semAcento[i]);
            }

            return textoLower;
        }

        private void BtnAprender_Click(object sender, EventArgs e)
        {
            string novaPergunta = Microsoft.VisualBasic.Interaction.InputBox("Digite a pergunta ou dúvida que deseja cadastrar:", "Retro-Aprendizado - Nova Pergunta", "");
            if (string.IsNullOrWhiteSpace(novaPergunta)) return;

            string novaResposta = Microsoft.VisualBasic.Interaction.InputBox($"Digite a resposta correta para:\n\"{novaPergunta}\"", "Retro-Aprendizado - Nova Resposta", "");
            if (string.IsNullOrWhiteSpace(novaResposta)) return;

            string linhaParaSalvar = $"{novaPergunta.Trim()}|{novaResposta.Trim()}";
            File.AppendAllText(caminhoBaseConhecimento, Environment.NewLine + linhaParaSalvar);

            AdicionarMensagemChat("🤖 Assistente IA", $"Obrigado! Novo aprendizado salvo com sucesso na base de conhecimento:\nQ: {novaPergunta}\nR: {novaResposta}");

            txtPergunta.Focus();
        }
    }
}