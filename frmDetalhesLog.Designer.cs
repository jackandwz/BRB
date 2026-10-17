namespace BRBLog
{
    partial class frmDetalhesLog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDetalhesLog));
            rtbDetalhe = new RichTextBox();
            panelInferior = new Panel();
            btnExcluir = new Button();
            panelInferior.SuspendLayout();
            SuspendLayout();
            // 
            // rtbDetalhe
            // 
            rtbDetalhe.BackColor = Color.White;
            rtbDetalhe.Dock = DockStyle.Fill;
            rtbDetalhe.Font = new Font("Consolas", 10F);
            rtbDetalhe.Location = new Point(0, 0);
            rtbDetalhe.Margin = new Padding(4, 4, 4, 4);
            rtbDetalhe.Name = "rtbDetalhe";
            rtbDetalhe.ReadOnly = true;
            rtbDetalhe.Size = new Size(900, 651);
            rtbDetalhe.TabIndex = 0;
            rtbDetalhe.Text = "";
            // 
            // panelInferior
            // 
            panelInferior.BackColor = Color.WhiteSmoke;
            panelInferior.Controls.Add(btnExcluir);
            panelInferior.Dock = DockStyle.Bottom;
            panelInferior.Location = new Point(0, 651);
            panelInferior.Margin = new Padding(4, 4, 4, 4);
            panelInferior.Name = "panelInferior";
            panelInferior.Size = new Size(900, 89);
            panelInferior.TabIndex = 1;
            // 
            // btnExcluir
            // 
            btnExcluir.BackColor = Color.FromArgb(220, 53, 69);
            btnExcluir.FlatAppearance.BorderSize = 0;
            btnExcluir.FlatStyle = FlatStyle.Flat;
            btnExcluir.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnExcluir.ForeColor = Color.White;
            btnExcluir.Location = new Point(30, 15);
            btnExcluir.Margin = new Padding(4, 4, 4, 4);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(270, 59);
            btnExcluir.TabIndex = 0;
            btnExcluir.Text = "🗑️ Excluir Atendimento";
            btnExcluir.UseVisualStyleBackColor = false;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // frmDetalhesLog
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(900, 740);
            Controls.Add(rtbDetalhe);
            Controls.Add(panelInferior);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmDetalhesLog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "BRBLog - Detalhes do Atendimento";
            panelInferior.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbDetalhe;
        private System.Windows.Forms.Panel panelInferior;
        private System.Windows.Forms.Button btnExcluir;
    }
}