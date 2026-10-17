using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BRBLog
{
    public partial class frmConsulta : Form
    {
        public frmConsulta()
        {
            InitializeComponent();
            AplicarPadraoVisual();
        }

        private void AplicarPadraoVisual()
        {
            // Padronização dos botões no azul corporativo
            if (btnPesquisar != null)
            {
                btnPesquisar.BackColor = Color.FromArgb(0, 102, 204);
                btnPesquisar.ForeColor = Color.White;
                btnPesquisar.FlatStyle = FlatStyle.Flat;
                btnPesquisar.FlatAppearance.BorderSize = 0;
                btnPesquisar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            }

            if (btnConsultaTudo != null)
            {
                btnConsultaTudo.BackColor = Color.FromArgb(108, 117, 125); // Cinza neutro
                btnConsultaTudo.ForeColor = Color.White;
                btnConsultaTudo.FlatStyle = FlatStyle.Flat;
                btnConsultaTudo.FlatAppearance.BorderSize = 0;
                btnConsultaTudo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            }
        }

        private void frmConsulta_Load(object sender, EventArgs e)
        {
            BancoDados.InicializarBanco();
            CarregarDadosGrid();

            dtpDataInicio.Format = DateTimePickerFormat.Short;
            dtpDataFim.Format = DateTimePickerFormat.Short;
        }

        private void CarregarDadosGrid()
        {
            DataTable dados = BancoDados.ObterTodosLogs();
            if (dados != null)
            {
                dtgDados.DataSource = dados;

                if (dtgDados.Columns["LogCompleto"] != null)
                {
                    dtgDados.Columns["LogCompleto"].Visible = false;
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string cpfFiltro = mtbFiltroCpf.MaskCompleted ? mtbFiltroCpf.Text : mtbFiltroCpf.Text.Replace("_", "").Trim();
            if (cpfFiltro.Length < 3) cpfFiltro = string.Empty;

            string nomeFiltro = txtFiltroNome.Text.Trim();

            DateTime dataInicio = dtpDataInicio.Value.Date;
            DateTime dataFim = dtpDataFim.Value.Date;

            bool usarFiltroData = true;

            DataTable dadosFiltrados = BancoDados.PesquisarLogs(cpfFiltro, nomeFiltro, dataInicio, dataFim, usarFiltroData);

            if (dadosFiltrados != null)
            {
                dtgDados.DataSource = dadosFiltrados;

                if (dtgDados.Columns["LogCompleto"] != null)
                {
                    dtgDados.Columns["LogCompleto"].Visible = false;
                }
            }
        }

        private void dtgDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow linhaSelecionada = dtgDados.Rows[e.RowIndex];
                string logCompleto = linhaSelecionada.Cells["LogCompleto"].Value?.ToString();
                int idRegistro = Convert.ToInt32(linhaSelecionada.Cells["Id"].Value);

                if (!string.IsNullOrEmpty(logCompleto))
                {
                    frmDetalhesLog telaDetalhes = new frmDetalhesLog(logCompleto, idRegistro);

                    // Se o usuário clicou em excluir e a janela fechou com DialogResult.OK, atualizamos a grid
                    if (telaDetalhes.ShowDialog(this) == DialogResult.OK)
                    {
                        CarregarDadosGrid();
                    }
                }
                else
                {
                    MessageBox.Show("Nenhum log detalhado encontrado para este registro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mtbFiltroCpf.Clear();
            txtFiltroNome.Clear();
            CarregarDadosGrid();
        }

    }
}