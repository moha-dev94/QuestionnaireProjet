
using MySql.Data.MySqlClient;

namespace gestionQuestionnaires
{
    public partial class LoginForm : Form
    {
        private MySqlConnection connection;

        public LoginForm()
        {
            InitializeComponent();
            string connectionStringOff = "Server=localhost;Database=GestionQuestionnaires;Uid=root;Pwd=;";
            string connectionStringOn = "Server=104.40.137.99;Port=22260;Database=mohamedamine_ppedesktop;Uid=developer;Pwd=cerfal1313;";
            connection = new MySqlConnection(connectionStringOn);
        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            string nomUtilisateur = txtNomUtilisateur.Text.Trim();
            string motDePasse = txtMotDePasse.Text.Trim();

            if (string.IsNullOrEmpty(nomUtilisateur) || string.IsNullOrEmpty(motDePasse))
            {
                lblErreur.Text = "Veuillez remplir tous les champs.";
                lblErreur.Visible = true;
                return;
            }

            try
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Utilisateur WHERE NomUtilisateur = @NomUtilisateur AND MotDePasse = @MotDePasse";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@NomUtilisateur", nomUtilisateur);
                    cmd.Parameters.AddWithValue("@MotDePasse", motDePasse);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Connexion réussie
                        MessageBox.Show("Connexion réussie !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Ouvrir le formulaire des questionnaires
                        QuestionnaireForm questionnaireForm = new QuestionnaireForm();
                        questionnaireForm.Show();

                        // Fermer la fenêtre de login
                        this.Hide();
                    }
                    else
                    {
                        lblErreur.Text = "Nom d'utilisateur ou mot de passe incorrect.";
                        lblErreur.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
