namespace BRBLog
{
    partial class frmConsulta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConsulta));
            dtgDados = new DataGridView();
            groupBox1 = new GroupBox();
            btnConsultaTudo = new Button();
            dtpDataFim = new DateTimePicker();
            dtpDataInicio = new DateTimePicker();
            label2 = new Label();
            label1 = new Label();
            btnPesquisar = new Button();
            txtFiltroNome = new TextBox();
            lblFiltroNome = new Label();
            mtbFiltroCpf = new MaskedTextBox();
            lblFiltroCpf = new Label();
            ((System.ComponentModel.ISupportInitialize)dtgDados).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dtgDados
            // 
            dtgDados.AllowUserToAddRows = false;
            dtgDados.AllowUserToDeleteRows = false;
            dtgDados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgDados.BackgroundColor = Color.White;
            dtgDados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgDados.Dock = DockStyle.Fill;
            dtgDados.Font = new Font("Segoe UI", 9.5F);
            dtgDados.Location = new Point(0, 130);
            dtgDados.Name = "dtgDados";
            dtgDados.ReadOnly = true;
            dtgDados.RowHeadersVisible = false;
            dtgDados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgDados.Size = new Size(1024, 618);
            dtgDados.TabIndex = 0;
            dtgDados.CellDoubleClick += dtgDados_CellDoubleClick;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(btnConsultaTudo);
            groupBox1.Controls.Add(dtpDataFim);
            groupBox1.Controls.Add(dtpDataInicio);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnPesquisar);
            groupBox1.Controls.Add(txtFiltroNome);
            groupBox1.Controls.Add(lblFiltroNome);
            groupBox1.Controls.Add(mtbFiltroCpf);
            groupBox1.Controls.Add(lblFiltroCpf);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1024, 130);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "🔍 Filtros de Pesquisa";
            // 
            // btnConsultaTudo
            // 
            btnConsultaTudo.BackColor = Color.FromArgb(108, 117, 125);
            btnConsultaTudo.FlatAppearance.BorderSize = 0;
            btnConsultaTudo.FlatStyle = FlatStyle.Flat;
            btnConsultaTudo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnConsultaTudo.ForeColor = Color.White;
            btnConsultaTudo.Location = new Point(780, 75);
            btnConsultaTudo.Name = "btnConsultaTudo";
            btnConsultaTudo.Size = new Size(210, 35);
            btnConsultaTudo.TabIndex = 7;
            btnConsultaTudo.Text = "🔄 Consultar Todo o Histórico";
            btnConsultaTudo.UseVisualStyleBackColor = false;
            btnConsultaTudo.Click += button1_Click;
            // 
            // dtpDataFim
            // 
            dtpDataFim.Font = new Font("Segoe UI", 9.5F);
            dtpDataFim.Format = DateTimePickerFormat.Short;
            dtpDataFim.Location = new Point(310, 78);
            dtpDataFim.Name = "dtpDataFim";
            dtpDataFim.Size = new Size(130, 29);
            dtpDataFim.TabIndex = 6;
            // 
            // dtpDataInicio
            // 
            dtpDataInicio.Font = new Font("Segoe UI", 9.5F);
            dtpDataInicio.Format = DateTimePickerFormat.Short;
            dtpDataInicio.Location = new Point(90, 78);
            dtpDataInicio.Name = "dtpDataInicio";
            dtpDataInicio.Size = new Size(130, 29);
            dtpDataInicio.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.5F);
            label2.Location = new Point(230, 81);
            label2.Name = "label2";
            label2.Size = new Size(74, 21);
            label2.TabIndex = 5;
            label2.Text = "Data Fim:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.5F);
            label1.Location = new Point(20, 81);
            label1.Name = "label1";
            label1.Size = new Size(84, 21);
            label1.TabIndex = 5;
            label1.Text = "Data Início:";
            // 
            // btnPesquisar
            // 
            btnPesquisar.BackColor = Color.FromArgb(0, 102, 204);
            btnPesquisar.FlatAppearance.BorderSize = 0;
            btnPesquisar.FlatStyle = FlatStyle.Flat;
            btnPesquisar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnPesquisar.ForeColor = Color.White;
            btnPesquisar.Location = new Point(780, 32);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(210, 35);
            btnPesquisar.TabIndex = 4;
            btnPesquisar.Text = "🔎 Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = false;
            btnPesquisar.Click += btnPesquisar_Click;
            // 
            // txtFiltroNome
            // 
            txtFiltroNome.Font = new Font("Segoe UI", 9.5F);
            txtFiltroNome.Location = new Point(360, 35);
            txtFiltroNome.Name = "txtFiltroNome";
            txtFiltroNome.Size = new Size(380, 29);
            txtFiltroNome.TabIndex = 3;
            // 
            // lblFiltroNome
            //
            lblFiltroNome.AutoSize = true;
            lblFiltroNome.Font = new Font("Segoe UI", 9.5F);
            lblFiltroNome.Location = new Point(300, 38);
            lblFiltroNome.Name = "lblFiltroNome";
            lblFiltroNome.Size = new Size(57, 21); // <--- Correção aplicada aqui
            lblFiltroNome.TabIndex = 2;
            lblFiltroNome.Text = "Nome:";
            // 
            // mtbFiltroCpf
            // 
            mtbFiltroCpf.Font = new Font("Segoe UI", 9.5F);
            mtbFiltroCpf.Location = new Point(90, 35);
            mtbFiltroCpf.Mask = "000\\.000\\.000-00";
            mtbFiltroCpf.Name = "mtbFiltroCpf";
            mtbFiltroCpf.Size = new Size(180, 29);
            mtbFiltroCpf.TabIndex = 1;
            mtbFiltroCpf.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            // 
            // lblFiltroCpf
            // 
            lblFiltroCpf.AutoSize = true;
            lblFiltroCpf.Font = new Font("Segoe UI", 9.5F);
            lblFiltroCpf.Location = new Point(20, 38);
            lblFiltroCpf.Name = "lblFiltroCpf";
            lblFiltroCpf.Size = new Size(41, 21);
            lblFiltroCpf.TabIndex = 0;
            lblFiltroCpf.Text = "CPF:";
            // 
            // frmConsulta
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1024, 748);
            Controls.Add(dtgDados);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "frmConsulta";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BRBLog - Consulta de Atendimentos Salvos";
            Load += frmConsulta_Load;
            ((System.ComponentModel.ISupportInitialize)dtgDados).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dtgDados;
        private GroupBox groupBox1;
        private Label lblFiltroCpf;
        private MaskedTextBox mtbFiltroCpf;
        private TextBox txtFiltroNome;
        private Label lblFiltroNome;
        private Button btnPesquisar;
        private DateTimePicker dtpDataFim;
        private DateTimePicker dtpDataInicio;
        private Label label2;
        private Label label1;
        private Button btnConsultaTudo;
    }
}