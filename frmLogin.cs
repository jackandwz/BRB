using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BRBLog
{
    public partial class frmLogin : Form
    {
        private string usuarioRede;

        public frmLogin()
        {
            InitializeComponent();
            CarregarLogo();
            ConfigurarBoasVindas();

            // Associa o evento KeyDown para o Enter funcionar no campo de senha
            txtPassword.KeyDown += new KeyEventHandler(txtPassword_KeyDown);

            // Garante o foco no campo de senha logo na inicialização
            this.Shown += (sender, e) => { txtPassword.Focus(); };
        }

        private void ConfigurarBoasVindas()
        {
            // Obtém o nome do usuário atual do Windows e exibe na saudação
            usuarioRede = Environment.UserName;
            lblUsuario.Text = $"Olá, {usuarioRede}";
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnLogin_Click(sender, e);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string senhaDigitada = txtPassword.Text;

            if (string.IsNullOrEmpty(senhaDigitada))
            {
                MessageBox.Show("Por favor, digite a sua senha.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // 1. Verifica se o usuário de rede já existe na base SQLite (brblog.db)
            if (!BancoDados.UsuarioExiste(usuarioRede))
            {
                MessageBox.Show("Primeiro acesso detectado para este usuário de rede. Por favor, cadastre sua senha.", "Primeiro Acesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                string novaSenha = Microsoft.VisualBasic.Interaction.InputBox("Digite sua nova senha de acesso:", "Cadastro de Senha", "");

                if (!string.IsNullOrEmpty(novaSenha))
                {
                    bool cadastrado = BancoDados.CadastrarUsuario(usuarioRede, usuarioRede, novaSenha);
                    if (cadastrado)
                    {
                        MessageBox.Show("Senha cadastrada com sucesso! Agora efetue o login.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPassword.Clear();
                        txtPassword.Focus();
                    }
                }
                return;
            }

            // 2. Valida as credenciais normais no banco de dados
            bool loginValido = BancoDados.ValidarLogin(usuarioRede, senhaDigitada);

            if (loginValido)
            {
                this.Hide();
                frmDashboard principalForm = new frmDashboard();
                principalForm.Show();
            }
            else
            {
                MessageBox.Show("Senha incorreta. Tente novamente.", "Erro de Autenticação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void CarregarLogo()
        {
            string nomeArquivo = "Marca-Banco-BRB.png";
            string caminhoCompleto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomeArquivo);

            if (File.Exists(caminhoCompleto))
            {
                ptbLogo.Image = Image.FromFile(caminhoCompleto);
                ptbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                MessageBox.Show($"Logo não encontrada no caminho: {caminhoCompleto}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }
    }
}