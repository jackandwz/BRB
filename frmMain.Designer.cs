namespace BRBLog
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            mtbCPF = new MaskedTextBox();
            txtNome = new TextBox();
            cbxBandeira = new ComboBox();
            txtUltimos4Digitos = new TextBox();
            cbxPid1 = new ComboBox();
            txtRespPid1 = new TextBox();
            cbxPid2 = new ComboBox();
            txtRespPid2 = new TextBox();
            rtbMotivo = new RichTextBox();
            rtbTratativa = new RichTextBox();
            rtbCab = new RichTextBox();
            btnSalvar = new Button();
            btnCopiarLog = new Button();
            lblValidaCPF = new Label();
            groupBox1 = new GroupBox();
            label16 = new Label();
            lblTel = new Label();
            mtbContato = new MaskedTextBox();
            mtbCadastro = new MaskedTextBox();
            rdbNao = new RadioButton();
            rdbSim = new RadioButton();
            label14 = new Label();
            btnLimpar = new Button();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label8 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label9 = new Label();
            label7 = new Label();
            label13 = new Label();
            label6 = new Label();
            label5 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(label16);
            groupBox1.Controls.Add(lblTel);
            groupBox1.Controls.Add(mtbContato);
            groupBox1.Controls.Add(mtbCadastro);
            groupBox1.Controls.Add(rdbNao);
            groupBox1.Controls.Add(rdbSim);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(btnLimpar);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(mtbCPF);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(lblValidaCPF);
            groupBox1.Controls.Add(txtNome);
            groupBox1.Controls.Add(cbxBandeira);
            groupBox1.Controls.Add(btnCopiarLog);
            groupBox1.Controls.Add(txtUltimos4Digitos);
            groupBox1.Controls.Add(btnSalvar);
            groupBox1.Controls.Add(cbxPid1);
            groupBox1.Controls.Add(txtRespPid1);
            groupBox1.Controls.Add(rtbTratativa);
            groupBox1.Controls.Add(cbxPid2);
            groupBox1.Controls.Add(rtbMotivo);
            groupBox1.Controls.Add(txtRespPid2);
            groupBox1.Dock = DockStyle.Left;
            groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(590, 750);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tabulação de Atendimento";
            // 
            // label5 (👤 Dados Pessoais)
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(0, 102, 204);
            label5.Location = new Point(20, 32);
            label5.Name = "label5";
            label5.Size = new Size(166, 23);
            label5.TabIndex = 14;
            label5.Text = "👤 Dados Pessoais";
            // 
            // label1 (CPF:)
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.5F);
            label1.Location = new Point(20, 70);
            label1.Name = "label1";
            label1.Size = new Size(41, 21);
            label1.TabIndex = 1;
            label1.Text = "CPF: ";
            // 
            // mtbCPF
            // 
            mtbCPF.Font = new Font("Segoe UI", 10F);
            mtbCPF.Location = new Point(220, 67);
            mtbCPF.Mask = "000\\.000\\.000-00";
            mtbCPF.Name = "mtbCPF";
            mtbCPF.Size = new Size(180, 30);
            mtbCPF.TabIndex = 0;
            // 
            // lblValidaCPF
            // 
            lblValidaCPF.AutoSize = true;
            lblValidaCPF.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblValidaCPF.Location = new Point(410, 70);
            lblValidaCPF.Name = "lblValidaCPF";
            lblValidaCPF.Size = new Size(0, 21);
            lblValidaCPF.TabIndex = 14;
            // 
            // label2 (Nome Completo:)
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.5F);
            label2.Location = new Point(20, 107);
            label2.Name = "label2";
            label2.Size = new Size(130, 21);
            label2.TabIndex = 15;
            label2.Text = "Nome Completo: ";
            // 
            // txtNome
            // 
            txtNome.Font = new Font("Segoe UI", 10F);
            txtNome.Location = new Point(220, 104);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(340, 30);
            txtNome.TabIndex = 1;
            // 
            // label3 (Bandeira:)
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.5F);
            label3.Location = new Point(20, 144);
            label3.Name = "label3";
            label3.Size = new Size(80, 21);
            label3.TabIndex = 16;
            label3.Text = "Bandeira: ";
            // 
            // cbxBandeira
            // 
            cbxBandeira.Font = new Font("Segoe UI", 10F);
            cbxBandeira.FormattingEnabled = true;
            cbxBandeira.Items.AddRange(new object[] { "", "Mastercard", "Visa" });
            cbxBandeira.Location = new Point(220, 141);
            cbxBandeira.Name = "cbxBandeira";
            cbxBandeira.Size = new Size(180, 31);
            cbxBandeira.TabIndex = 2;
            // 
            // label4 (Últimos 4 Dígitos:)
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.5F);
            label4.Location = new Point(20, 181);
            label4.Name = "label4";
            label4.Size = new Size(147, 21);
            label4.TabIndex = 16;
            label4.Text = "Últimos 4 Dígitos: ";
            // 
            // txtUltimos4Digitos
            // 
            txtUltimos4Digitos.Font = new Font("Segoe UI", 10F);
            txtUltimos4Digitos.Location = new Point(220, 178);
            txtUltimos4Digitos.MaxLength = 4;
            txtUltimos4Digitos.Name = "txtUltimos4Digitos";
            txtUltimos4Digitos.Size = new Size(100, 30);
            txtUltimos4Digitos.TabIndex = 3;
            // 
            // label14 (Telefone igual ao Cadastro:)
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9.5F);
            label14.Location = new Point(20, 218);
            label14.Name = "label14";
            label14.Size = new Size(184, 21);
            label14.TabIndex = 21;
            label14.Text = "Telefone igual Cadastro?";
            // 
            // rdbSim
            // 
            rdbSim.AutoSize = true;
            rdbSim.Font = new Font("Segoe UI", 9.5F);
            rdbSim.Location = new Point(220, 216);
            rdbSim.Name = "rdbSim";
            rdbSim.Size = new Size(56, 25);
            rdbSim.TabIndex = 22;
            rdbSim.TabStop = true;
            rdbSim.Text = "Sim";
            rdbSim.UseVisualStyleBackColor = true;
            // 
            // rdbNao
            // 
            rdbNao.AutoSize = true;
            rdbNao.Font = new Font("Segoe UI", 9.5F);
            rdbNao.Location = new Point(280, 216);
            rdbNao.Name = "rdbNao";
            rdbNao.Size = new Size(61, 25);
            rdbNao.TabIndex = 23;
            rdbNao.TabStop = true;
            rdbNao.Text = "Não";
            rdbNao.UseVisualStyleBackColor = true;
            // 
            // label16 (Telefone de Cadastro)
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 9.5F);
            label16.Location = new Point(20, 253);
            label16.Name = "label16";
            label16.Size = new Size(160, 21);
            label16.TabIndex = 27;
            label16.Text = "Telefone de Cadastro:";
            // 
            // mtbCadastro
            // 
            mtbCadastro.Font = new Font("Segoe UI", 10F);
            mtbCadastro.Location = new Point(220, 250);
            mtbCadastro.Mask = "(99) 00000-0000";
            mtbCadastro.Name = "mtbCadastro";
            mtbCadastro.Size = new Size(180, 30);
            mtbCadastro.TabIndex = 4;
            // 
            // lblTel (Telefone de Contato)
            // 
            lblTel.AutoSize = true;
            lblTel.Font = new Font("Segoe UI", 9.5F);
            lblTel.Location = new Point(20, 290);
            lblTel.Name = "lblTel";
            lblTel.Size = new Size(153, 21);
            lblTel.TabIndex = 26;
            lblTel.Text = "Telefone de Contato:";
            // 
            // mtbContato
            // 
            mtbContato.Font = new Font("Segoe UI", 10F);
            mtbContato.Location = new Point(220, 287);
            mtbContato.Mask = "(99) 00000-0000";
            mtbContato.Name = "mtbContato";
            mtbContato.Size = new Size(180, 30);
            mtbContato.TabIndex = 25;
            // 
            // label6 (🔒 PIDs de Validação)
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(0, 102, 204);
            label6.Location = new Point(20, 332);
            label6.Name = "label6";
            label6.Size = new Size(168, 23);
            label6.TabIndex = 14;
            label6.Text = "🔒 PIDs de Validação";
            // 
            // label7 (PID Aleatório 1:)
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9.5F);
            label7.Location = new Point(20, 368);
            label7.Name = "label7";
            label7.Size = new Size(116, 21);
            label7.TabIndex = 14;
            label7.Text = "PID Aleatório 1:";
            // 
            // cbxPid1
            // 
            cbxPid1.Font = new Font("Segoe UI", 10F);
            cbxPid1.FormattingEnabled = true;
            cbxPid1.Location = new Point(220, 365);
            cbxPid1.Name = "cbxPid1";
            cbxPid1.Size = new Size(340, 31);
            cbxPid1.TabIndex = 5;
            // 
            // label8 (Resposta PID 1:)
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9.5F);
            label8.Location = new Point(20, 405);
            label8.Name = "label8";
            label8.Size = new Size(122, 21);
            label8.TabIndex = 17;
            label8.Text = "Resposta PID 1:";
            // 
            // txtRespPid1
            // 
            txtRespPid1.Font = new Font("Segoe UI", 10F);
            txtRespPid1.Location = new Point(220, 402);
            txtRespPid1.Name = "txtRespPid1";
            txtRespPid1.Size = new Size(340, 30);
            txtRespPid1.TabIndex = 6;
            // 
            // label9 (PID Aleatório 2:)
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9.5F);
            label9.Location = new Point(20, 442);
            label9.Name = "label9";
            label9.Size = new Size(116, 21);
            label9.TabIndex = 14;
            label9.Text = "PID Aleatório 2:";
            // 
            // cbxPid2
            // 
            cbxPid2.Font = new Font("Segoe UI", 10F);
            cbxPid2.FormattingEnabled = true;
            cbxPid2.Location = new Point(220, 439);
            cbxPid2.Name = "cbxPid2";
            cbxPid2.Size = new Size(340, 31);
            cbxPid2.TabIndex = 7;
            // 
            // label10 (Resposta PID 2:)
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9.5F);
            label10.Location = new Point(20, 479);
            label10.Name = "label10";
            label10.Size = new Size(122, 21);
            label10.TabIndex = 17;
            label10.Text = "Resposta PID 2:";
            // 
            // txtRespPid2
            // 
            txtRespPid2.Font = new Font("Segoe UI", 10F);
            txtRespPid2.Location = new Point(220, 476);
            txtRespPid2.Name = "txtRespPid2";
            txtRespPid2.Size = new Size(340, 30);
            txtRespPid2.TabIndex = 8;
            // 
            // label13 (📌 Dados do Atendimento)
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label13.ForeColor = Color.FromArgb(0, 102, 204);
            label13.Location = new Point(20, 520);
            label13.Name = "label13";
            label13.Size = new Size(207, 23);
            label13.TabIndex = 14;
            label13.Text = "📌 Dados do Atendimento";
            // 
            // label11 (Motivo do Contato:)
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9.5F);
            label11.Location = new Point(20, 555);
            label11.Name = "label11";
            label11.Size = new Size(148, 21);
            label11.TabIndex = 18;
            label11.Text = "Motivo do Contato:";
            // 
            // rtbMotivo
            // 
            rtbMotivo.Font = new Font("Segoe UI", 10F);
            rtbMotivo.Location = new Point(220, 552);
            rtbMotivo.Name = "rtbMotivo";
            rtbMotivo.Size = new Size(340, 60);
            rtbMotivo.TabIndex = 9;
            rtbMotivo.Text = "";
            // 
            // label12 (Tratativa Realizada:)
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9.5F);
            label12.Location = new Point(20, 625);
            label12.Name = "label12";
            label12.Size = new Size(141, 21);
            label12.TabIndex = 18;
            label12.Text = "Tratativa Realizada:";
            // 
            // rtbTratativa
            // 
            rtbTratativa.Font = new Font("Segoe UI", 10F);
            rtbTratativa.Location = new Point(220, 622);
            rtbTratativa.Name = "rtbTratativa";
            rtbTratativa.Size = new Size(340, 60);
            rtbTratativa.TabIndex = 10;
            rtbTratativa.Text = "";
            // 
            // btnSalvar
            // 
            btnSalvar.BackColor = Color.FromArgb(0, 102, 204);
            btnSalvar.FlatAppearance.BorderSize = 0;
            btnSalvar.FlatStyle = FlatStyle.Flat;
            btnSalvar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSalvar.ForeColor = Color.White;
            btnSalvar.Location = new Point(20, 695);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(160, 40);
            btnSalvar.TabIndex = 11;
            btnSalvar.Text = "💾 Salvar";
            btnSalvar.UseVisualStyleBackColor = false;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnCopiarLog
            // 
            btnCopiarLog.BackColor = Color.FromArgb(0, 102, 204);
            btnCopiarLog.FlatAppearance.BorderSize = 0;
            btnCopiarLog.FlatStyle = FlatStyle.Flat;
            btnCopiarLog.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCopiarLog.ForeColor = Color.White;
            btnCopiarLog.Location = new Point(190, 695);
            btnCopiarLog.Name = "btnCopiarLog";
            btnCopiarLog.Size = new Size(160, 40);
            btnCopiarLog.TabIndex = 12;
            btnCopiarLog.Text = "📋 Copiar Log";
            btnCopiarLog.UseVisualStyleBackColor = false;
            btnCopiarLog.Click += btnCopiarLog_Click;
            // 
            // btnLimpar
            // 
            btnLimpar.BackColor = Color.FromArgb(108, 117, 125);
            btnLimpar.FlatAppearance.BorderSize = 0;
            btnLimpar.FlatStyle = FlatStyle.Flat;
            btnLimpar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLimpar.ForeColor = Color.White;
            btnLimpar.Location = new Point(360, 695);
            btnLimpar.Name = "btnLimpar";
            btnLimpar.Size = new Size(160, 40);
            btnLimpar.TabIndex = 19;
            btnLimpar.Text = "🧹 Limpar";
            btnLimpar.UseVisualStyleBackColor = false;
            btnLimpar.Click += button1_Click;
            // 
            // rtbCab
            // 
            rtbCab.BackColor = Color.White;
            rtbCab.Dock = DockStyle.Fill;
            rtbCab.Font = new Font("Consolas", 10F);
            rtbCab.Location = new Point(3, 26);
            rtbCab.Name = "rtbCab";
            rtbCab.Size = new Size(428, 721);
            rtbCab.TabIndex = 10;
            rtbCab.Text = "";
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.White;
            groupBox2.Controls.Add(rtbCab);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBox2.Location = new Point(590, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(434, 750);
            groupBox2.TabIndex = 16;
            groupBox2.TabStop = false;
            groupBox2.Text = "Pré-visualização da Tabulação";
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1024, 750);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BRBLog - Novo Atendimento";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.MaskedTextBox mtbCPF;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.ComboBox cbxBandeira;
        private System.Windows.Forms.TextBox txtUltimos4Digitos;
        private System.Windows.Forms.ComboBox cbxPid1;
        private System.Windows.Forms.TextBox txtRespPid1;
        private System.Windows.Forms.ComboBox cbxPid2;
        private System.Windows.Forms.TextBox txtRespPid2;
        private System.Windows.Forms.RichTextBox rtbMotivo;
        private System.Windows.Forms.RichTextBox rtbTratativa;
        private System.Windows.Forms.RichTextBox rtbCab;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCopiarLog;
        private System.Windows.Forms.Label lblValidaCPF;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label10;
        private Label label8;
        private Label label9;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label12;
        private Label label11;
        private Label label13;
        private Button btnLimpar;
        private RadioButton rdbNao;
        private RadioButton rdbSim;
        private Label label14;
        private MaskedTextBox mtbCadastro;
        private Label label16;
        private Label lblTel;
        private MaskedTextBox mtbContato;
    }
}