using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BRBLog
{
    public partial class frmDashboard : Form
    {
        private DataGridView dtgAtendimentosHoje;
        private Button btnNovoAtendimento;
        private Button btnConsultarTodos;
        private Button btnLimparBase;
        private Button btnIaAssistente;
        private Button btnAtualizarHoje;
        private Button btnFeedback;
        private Label lblTitulo;
        private Label lblContador;

        public frmDashboard()
        {
            InitializeComponentCustomizado();

            try
            {
                string caminhoIcone = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "brblog_card_log.ico");
                if (File.Exists(caminhoIcone))
                {
                    this.Icon = new Icon(caminhoIcone);
                }
                else if (File.Exists("brblog_card_log.ico"))
                {
                    this.Icon = new Icon("brblog_card_log.ico");
                }
            }
            catch
            {
                // Ignora caso o ícone não esteja acessível
            }

            this.Load += FrmDashboard_Load;
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            BancoDados.ExpurgarLogsAntigos();
            CarregarDadosDoDia();
        }

        private void InitializeComponentCustomizado()
        {
            this.Text = "BRBLog - Painel de Atendimentos do Dia - Bem-vindo: " + Environment.UserName;
            this.Size = new Size(1024, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Panel pnlLateral = new Panel
            {
                Dock = DockStyle.Left,
                Width = 230,
                BackColor = Color.White
            };

            lblTitulo = new Label
            {
                Text = "MENU PRINCIPAL\n" + Environment.UserName,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40),
                Location = new Point(15, 20),
                AutoSize = true
            };

            btnNovoAtendimento = CustomVisuals.CriarBotaoPadrao("➕ Novo Atendimento");
            btnNovoAtendimento.Location = new Point(15, 80);
            btnNovoAtendimento.Click += BtnNovoAtendimento_Click;

            btnIaAssistente = CustomVisuals.CriarBotaoPadrao("🤖 IA de Apoio");
            btnIaAssistente.Location = new Point(15, 135);
            btnIaAssistente.Click += btnIaAssistente_Click;

            btnAtualizarHoje = CustomVisuals.CriarBotaoPadrao("🔄 Atualizar Lista");
            btnAtualizarHoje.Location = new Point(15, 190);
            btnAtualizarHoje.Click += BtnAtualizarHoje_Click;

            btnConsultarTodos = CustomVisuals.CriarBotaoPadrao("🔍 Consultar Histórico");
            btnConsultarTodos.Location = new Point(15, 245);
            btnConsultarTodos.Click += BtnConsultarTodos_Click;

            btnLimparBase = CustomVisuals.CriarBotaoPadrao("🗑️ Limpar Base");
            btnLimparBase.Location = new Point(15, 300);
            btnLimparBase.Click += BtnLimparBase_Click;

            btnFeedback = CustomVisuals.CriarBotaoPadrao("💡 Feedback / Bug");
            btnFeedback.Location = new Point(15, 665);
            btnFeedback.Click += BtnFeedback_Click;

            pnlLateral.Controls.Add(lblTitulo);
            pnlLateral.Controls.Add(btnNovoAtendimento);
            pnlLateral.Controls.Add(btnIaAssistente);
            pnlLateral.Controls.Add(btnAtualizarHoje);
            pnlLateral.Controls.Add(btnConsultarTodos);
            pnlLateral.Controls.Add(btnLimparBase);
            pnlLateral.Controls.Add(btnFeedback);

            Panel pnlSuperior = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(248, 249, 250)
            };

            lblContador = new Label
            {
                Text = "Atendimentos realizados hoje: 0",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                Location = new Point(20, 18),
                AutoSize = true
            };

            pnlSuperior.Controls.Add(lblContador);

            dtgAtendimentosHoje = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White
            };

            // Correção do vínculo do evento para o nome correto
            dtgAtendimentosHoje.CellDoubleClick += DtgAtendimentosHoje_CellDoubleClick;

            this.Controls.Add(dtgAtendimentosHoje);
            this.Controls.Add(pnlSuperior);
            this.Controls.Add(pnlLateral);
        }

        private void CarregarDadosDoDia()
        {
            DataTable dt = BancoDados.ObterLogsDoDia();
            if (dt != null)
            {
                dtgAtendimentosHoje.DataSource = dt;

                if (dtgAtendimentosHoje.Columns["LogCompleto"] != null)
                {
                    dtgAtendimentosHoje.Columns["LogCompleto"].Visible = false;
                }

                lblContador.Text = $"Atendimentos realizados hoje: {dt.Rows.Count}";
            }
        }

        private void BtnNovoAtendimento_Click(object sender, EventArgs e)
        {
            frmMain frm = new frmMain();
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
            CarregarDadosDoDia();
        }

        private void BtnConsultarTodos_Click(object sender, EventArgs e)
        {
            frmConsulta telaConsulta = new frmConsulta();
            telaConsulta.ShowDialog(this);
            CarregarDadosDoDia();
        }

        private void BtnLimparBase_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "Tem certeza que deseja apagar TODOS os logs salvos na base de dados?",
                "Atenção - Limpeza Geral",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (resultado == DialogResult.Yes)
            {
                BancoDados.LimparBaseDeDados();
                CarregarDadosDoDia();
                MessageBox.Show("Base de dados limpa com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Método corrigido para atender ao evento da grid do Dashboard e suportar exclusão
        private void DtgAtendimentosHoje_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow linhaSelecionada = dtgAtendimentosHoje.Rows[e.RowIndex];
                string logCompleto = linhaSelecionada.Cells["LogCompleto"].Value?.ToString();

                if (linhaSelecionada.Cells["Id"].Value != null)
                {
                    int idRegistro = Convert.ToInt32(linhaSelecionada.Cells["Id"].Value);

                    if (!string.IsNullOrEmpty(logCompleto))
                    {
                        frmDetalhesLog telaDetalhes = new frmDetalhesLog(logCompleto, idRegistro);

                        // Se o usuário excluiu o registro, atualiza a listagem do dia
                        if (telaDetalhes.ShowDialog(this) == DialogResult.OK)
                        {
                            CarregarDadosDoDia();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nenhum log detalhado encontrado para este registro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void btnIaAssistente_Click(object sender, EventArgs e)
        {
            frmIaAssistente formIa = new frmIaAssistente();
            formIa.Show(this);
        }

        private void BtnAtualizarHoje_Click(object sender, EventArgs e)
        {
            CarregarDadosDoDia();
        }

        private void BtnFeedback_Click(object sender, EventArgs e)
        {
            frmFeedback telaFeedback = new frmFeedback();
            telaFeedback.ShowDialog(this);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }
    }
}