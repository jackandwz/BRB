namespace BRBLog
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        /*
          static void Main()
          {
              // To customize application configuration such as set high DPI settings or default font,
              // see https://aka.ms/applicationconfiguration.
              ApplicationConfiguration.Initialize();
              Application.Run(new frmMain());
          }
        */
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Inicializa o banco de dados antes de abrir o painel
            BancoDados.InicializarBanco();

            // Define o Dashboard como a janela principal do sistema
            Application.Run(new frmLogin());
        }
    }
}