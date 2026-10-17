namespace BRBLog
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            ptbLogo = new PictureBox();
            lblUsuario = new Label();
            lblInstrucao = new Label();
            txtPassword = new TextBox();
            btnLogin = new Button();
            panelCard = new Panel();
            ((System.ComponentModel.ISupportInitialize)ptbLogo).BeginInit();
            panelCard.SuspendLayout();
            SuspendLayout();
            // 
            // panelCard
            // 
            panelCard.BackColor = Color.White;
            panelCard.Controls.Add(ptbLogo);
            panelCard.Controls.Add(lblUsuario);
            panelCard.Controls.Add(lblInstrucao);
            panelCard.Controls.Add(txtPassword);
            panelCard.Controls.Add(btnLogin);
            panelCard.Dock = DockStyle.Fill;
            panelCard.Location = new Point(0, 0);
            panelCard.Name = "panelCard";
            panelCard.Size = new Size(460, 480);
            panelCard.TabIndex = 0;
            // 
            // ptbLogo
            // 
            ptbLogo.Location = new Point(110, 25);
            ptbLogo.Name = "ptbLogo";
            ptbLogo.Size = new Size(240, 100);
            ptbLogo.TabStop = false;
            // 
            // lblUsuario
            // 
            lblUsuario.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblUsuario.ForeColor = Color.FromArgb(30, 30, 30);
            lblUsuario.TextAlign = ContentAlignment.MiddleCenter;
            lblUsuario.Location = new Point(30, 140);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(400, 35);
            lblUsuario.TabIndex = 1;
            lblUsuario.Text = "Olá, Usuário";
            // 
            // lblInstrucao
            // 
            lblInstrucao.Font = new Font("Segoe UI", 9.5F);
            lblInstrucao.ForeColor = Color.FromArgb(100, 100, 100);
            lblInstrucao.TextAlign = ContentAlignment.MiddleCenter;
            lblInstrucao.Location = new Point(30, 175);
            lblInstrucao.Name = "lblInstrucao";
            lblInstrucao.Size = new Size(400, 25);
            lblInstrucao.TabIndex = 2;
            lblInstrucao.Text = "Digite sua senha corporativa para acessar";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(60, 225);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.PlaceholderText = "Sua senha";
            txtPassword.Size = new Size(340, 39);
            txtPassword.TabIndex = 0;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(0, 102, 204);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(60, 290);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(340, 45);
            btnLogin.TabIndex = 1;
            btnLogin.Text = "Entrar";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(460, 450);
            Controls.Add(panelCard);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BRBLog - Autenticação";
            Load += frmLogin_Load;
            ((System.ComponentModel.ISupportInitialize)ptbLogo).EndInit();
            panelCard.ResumeLayout(false);
            panelCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox ptbLogo;
        private Label lblUsuario;
        private Label lblInstrucao;
        private TextBox txtPassword;
        private Button btnLogin;
        private Panel panelCard;
    }
}