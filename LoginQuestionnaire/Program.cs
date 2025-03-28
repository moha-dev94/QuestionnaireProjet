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

            // Démarrer le formulaire de connexion
            Application.Run(new LoginForm());
        }
    }
}