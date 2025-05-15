using System;
using System.Windows.Forms;

namespace gestionQuestionnaires
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Désactiver le scaling DPI automatique
            Application.SetHighDpiMode(HighDpiMode.DpiUnaware);

            Application.Run(new LoginForm());
        }
    }
}