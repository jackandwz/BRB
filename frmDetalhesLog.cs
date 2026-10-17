using System;
using System.Drawing;
using System.Windows.Forms;

namespace BRBLog
{
    public partial class frmDetalhesLog : Form
    {
        private int idAtendimento;

        public frmDetalhesLog(string conteudoLog, int idRegistro)
        {
            InitializeComponent();

            idAtendimento = idRegistro;

            if (rtbDetalhe != null)
            {
                rtbDetalhe.Text = conteudoLog;
            }

            AplicarPadraoVisual();
        }

        private void AplicarPadraoVisual()
        {
            if (btnExcluir != null)
            {
                btnExcluir.BackColor = Color.FromArgb(220, 53, 69); // Vermelho de alerta
                btnExcluir.ForeColor = Color.White;
                btnExcluir.FlatStyle = FlatStyle.Flat;
                btnExcluir.FlatAppearance.BorderSize = 0;
                btnExcluir.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DialogResult confirmacao = MessageBox.Show(
                "Tem certeza de que deseja excluir permanentemente este atendimento?",
                "Confirmar Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    BancoDados.ExcluirLog(idAtendimento);
                    MessageBox.Show("Atendimento excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Informa a tela de consulta que houve exclusão e fecha
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir o registro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}