using System;
using System.Drawing;
using System.Windows.Forms;

namespace BRBLog
{
    public class frmFeedback : Form
    {
        private Label lblInstrucao;
        private TextBox txtEmail;
        private Button btnCopiarEmail;
        private TextBox txtConta;
        private Button btnFechar;

        public frmFeedback()
        {
            InitializeComponentCustomizado();
        }

        private void InitializeComponentCustomizado()
        {
            this.Text = "BRBLog - Feedback, Sugestões e Bugs";
            this.Size = new Size(480, 260);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            // Instrução
            lblInstrucao = new Label
            {
                Text = "Envie suas sugestões, melhorias ou reporte bugs por e-mail (Zimbra):\nAssunto sugerido: BRBLog - Feedback",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40),
                Location = new Point(20, 20),
                Size = new Size(425, 45)
            };

            // Campo de E-mail
            Label lblEmailTitulo = new Label
            {
                Text = "E-mail Corporativo:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(20, 75),
                AutoSize = true
            };

            txtEmail = new TextBox
            {
                Text = "jackson.andrade@concentrix.com",
                Font = new Font("Segoe UI", 9.5F),
                Location = new Point(20, 95),
                Size = new Size(300, 26),
                ReadOnly = true,
                BackColor = Color.White
            };

            btnCopiarEmail = new Button
            {
                Text = "📋 Copiar E-mail",
                Location = new Point(325, 94),
                Size = new Size(115, 30),
                BackColor = Color.FromArgb(0, 102, 204), // Azul corporativo
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCopiarEmail.FlatAppearance.BorderSize = 0;
            btnCopiarEmail.Click += BtnCopiarEmail_Click;

            // Campo de Conta
            Label lblContaTitulo = new Label
            {
                Text = "Conta / Projeto:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(20, 135),
                AutoSize = true
            };

            txtConta = new TextBox
            {
                Text = "BRB Azul",
                Font = new Font("Segoe UI", 9.5F),
                Location = new Point(20, 155),
                Size = new Size(420, 26),
                ReadOnly = true,
                BackColor = Color.White
            };

            // Botão Fechar
            btnFechar = new Button
            {
                Text = "Fechar",
                Location = new Point(345, 195),
                Size = new Size(95, 30),
                BackColor = Color.FromArgb(108, 117, 125), // Cinza corporativo
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnFechar.FlatAppearance.BorderSize = 0;
            btnFechar.Click += (s, e) => this.Close();

            // Adiciona os controles ao formulário
            this.Controls.Add(lblInstrucao);
            this.Controls.Add(lblEmailTitulo);
            this.Controls.Add(txtEmail);
            this.Controls.Add(btnCopiarEmail);
            this.Controls.Add(lblContaTitulo);
            this.Controls.Add(txtConta);
            this.Controls.Add(btnFechar);
        }

        private void BtnCopiarEmail_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtEmail.Text);
                MessageBox.Show("E-mail copiado para a área de transferência!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao copiar o e-mail: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}