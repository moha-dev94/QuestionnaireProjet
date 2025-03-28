using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace gestionQuestionnaires
{
    public partial class QuestionnaireResponseForm : Form
    {
        private MySqlConnection connection;
        private int questionnaireId;
        private string questionnaireLibelle;
        private int score = 0;
        private int questionActuelleIndex = 0;
        private List<Question> questions;
        private int id;
        private string? libelle;
        private string? theme;

        public QuestionnaireResponseForm(int id, string libelle, MySqlConnection conn)
        {
            InitializeComponent();
            questionnaireId = id;
            questionnaireLibelle = libelle;
            connection = conn;
            lblNomQuestionnaire.Text = $"Questionnaire : {questionnaireLibelle}";
        }

        public QuestionnaireResponseForm(int id, string? libelle, string? theme)
        {
            this.id = id;
            this.libelle = libelle;
            this.theme = theme;
        }

        private void QuestionnaireResponseForm_Load(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                MessageBox.Show("Connexion à la base réussie !");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion : {ex.Message}");
            }

            questions = ChargerQuestions();
            if (questions.Count > 0)
            {
                AfficherQuestion(questions[questionActuelleIndex]);
            }
            else
            {
                MessageBox.Show("Aucune question disponible pour ce questionnaire.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private List<Question> ChargerQuestions()
        {
            List<Question> questionsList = new List<Question>();
            try
            {
                connection.Open();

                string query = @"
        SELECT 
            ID, 
            Libelle, 
            bonneReponse, 
            IFNULL(Reponse1, '') AS Reponse1, 
            IFNULL(Reponse2, '') AS Reponse2, 
            IFNULL(Reponse3, '') AS Reponse3, 
            VraiFaux 
        FROM Question
        WHERE QuestionnaireID = @QuestionnaireID";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@QuestionnaireID", questionnaireId);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Question question = new Question
                        {
                            Id = reader.GetInt32("ID"),
                            Libelle = reader.GetString("Libelle"),
                            BonneReponse = reader.GetString("bonneReponse"),
                            Reponses = new List<string>
                    {
                        reader.GetString("Reponse1"),
                        reader.GetString("Reponse2"),
                        reader.GetString("Reponse3")
                    },
                            IsVraiFaux = reader.GetBoolean("VraiFaux")
                        };
                        questionsList.Add(question);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des questions : {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return questionsList;
        }

        private void AfficherQuestion(Question question)
        {
            // Nettoyer les anciens contrôles
            foreach (Control control in this.Controls.OfType<Control>().ToList())
            {
                if (control.Tag != null) // Contrôles dynamiques
                {
                    this.Controls.Remove(control);
                }
            }

            // Afficher la question
            lblQuestion.Text = question.Libelle;
            lblProgress.Text = $"Question {questionActuelleIndex + 1}/{questions.Count}";

            int yPosition = 100; // Position verticale initiale pour les réponses

            if (question.IsVraiFaux)
            {
                // Ajouter RadioButtons pour Vrai/Faux
                RadioButton radioVrai = new RadioButton
                {
                    Text = "Vrai",
                    Location = new Point(50, yPosition),
                    Tag = "Vrai"
                };
                this.Controls.Add(radioVrai);

                RadioButton radioFaux = new RadioButton
                {
                    Text = "Faux",
                    Location = new Point(150, yPosition),
                    Tag = "Faux"
                };
                this.Controls.Add(radioFaux);
            }
            else
            {
                // Mélanger les réponses
                List<string> allReponses = question.Reponses.Where(r => !string.IsNullOrWhiteSpace(r)).ToList();
                allReponses.Add(question.BonneReponse);
                Random random = new Random();
                allReponses = allReponses.OrderBy(r => random.Next()).ToList();

                foreach (string reponse in allReponses)
                {
                    CheckBox checkBox = new CheckBox
                    {
                        Text = reponse,
                        Location = new Point(50, yPosition),
                        AutoSize = true,
                        Tag = reponse
                    };
                    checkBox.CheckedChanged += (s, e) =>
                    {
                        foreach (Control control in this.Controls.OfType<CheckBox>())
                        {
                            if (control != checkBox)
                            {
                                ((CheckBox)control).Checked = false;
                            }
                        }
                    };
                    this.Controls.Add(checkBox);
                    yPosition += 30;
                }
            }

            // Gérer les boutons
            btnSuivant.Visible = questionActuelleIndex < questions.Count - 1;
            btnTerminer.Visible = questionActuelleIndex == questions.Count - 1;
        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            SauvegarderReponse();
            questionActuelleIndex++;
            AfficherQuestion(questions[questionActuelleIndex]);
        }

        private void btnTerminer_Click(object sender, EventArgs e)
        {
            SauvegarderReponse();
            lblScore.Text = $"Score final : {score}/{questions.Count}";
            lblScore.Visible = true;
            MessageBox.Show($"Votre score : {score}/{questions.Count}", "Résultat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void SauvegarderReponse()
        {
            Question questionActuelle = questions[questionActuelleIndex];
            string reponseSelectionnee = string.Empty;

            // Récupérer la réponse sélectionnée
            if (questionActuelle.IsVraiFaux)
            {
                RadioButton selectedRadio = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                if (selectedRadio != null)
                {
                    reponseSelectionnee = selectedRadio.Tag.ToString();
                }
            }
            else
            {
                CheckBox selectedCheckBox = this.Controls.OfType<CheckBox>().FirstOrDefault(c => c.Checked);
                if (selectedCheckBox != null)
                {
                    reponseSelectionnee = selectedCheckBox.Tag.ToString();
                }
            }

            // Comparer la réponse avec la bonne réponse
            if (!string.IsNullOrEmpty(reponseSelectionnee) && reponseSelectionnee == questionActuelle.BonneReponse)
            {
                score++;
            }
        }

    }
}
        public class Question
{
    public int Id { get; set; }
    public string Libelle { get; set; }
    public string BonneReponse { get; set; }
    public List<string> Reponses { get; set; }
    public bool IsVraiFaux { get; set; }
}
