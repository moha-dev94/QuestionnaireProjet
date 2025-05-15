using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace gestionQuestionnaires
{
    public class PdfGenerator
    {
        private MySqlConnection connection;

        public PdfGenerator(MySqlConnection conn)
        {
            connection = conn;
        }

        public void GenerateQuestionnairesPdf(string filePath)
        {
            try
            {
                // Créer le document PDF
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Liste des Questionnaires";

                // Récupérer les questionnaires depuis la BDD
                List<Questionnaire> questionnaires = GetQuestionnaires();

                foreach (var questionnaire in questionnaires)
                {
                    // Ajouter une nouvelle page pour chaque questionnaire
                    // Ajouter une nouvelle page pour chaque questionnaire
                    PdfPage page = document.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    XFont titleFont = new XFont("Arial", 18);
                    XFont headerFont = new XFont("Arial", 14);
                    XFont normalFont = new XFont("Arial", 12);
                    XFont answerFont = new XFont("Arial", 10);

                    // Position initiale
                    int yPosition = 50;

                    // Ajouter un titre si c'est le premier questionnaire
                    if (questionnaires.IndexOf(questionnaire) == 0)
                    {
                        gfx.DrawString("Liste des Questionnaires", titleFont, XBrushes.Black,
                            new XRect(0, yPosition, page.Width, page.Height),
                            XStringFormats.TopCenter);
                        yPosition += 40;
                    }

                    // Ajouter les informations du questionnaire
                    gfx.DrawString($"Questionnaire: {questionnaire.Libelle}", headerFont, XBrushes.Black,
                        new XRect(40, yPosition, page.Width - 80, page.Height),
                        XStringFormats.TopLeft);
                    yPosition += 25;

                    gfx.DrawString($"Thème: {questionnaire.Theme}", headerFont, XBrushes.Black,
                        new XRect(40, yPosition, page.Width - 80, page.Height),
                        XStringFormats.TopLeft);
                    yPosition += 30;

                    // Ajouter le titre des questions
                    gfx.DrawString("Questions:", normalFont, XBrushes.Black,
                        new XRect(40, yPosition, page.Width - 80, page.Height),
                        XStringFormats.TopLeft);
                    yPosition += 20;

                    foreach (var question in questionnaire.Questions)
                    {
                        // Vérifier si on a besoin d'une nouvelle page
                        if (yPosition > page.Height - 50)
                        {
                            page = document.AddPage();
                            gfx = XGraphics.FromPdfPage(page);
                            yPosition = 50;
                        }

                        // Question
                        gfx.DrawString($"- {question.Libelle}", normalFont, XBrushes.Black,
                            new XRect(50, yPosition, page.Width - 100, page.Height),
                            XStringFormats.TopLeft);
                        yPosition += 20;

                        // Bonne réponse
                        gfx.DrawString($"  Bonne réponse: {question.BonneReponse}", answerFont, XBrushes.Black,
                            new XRect(60, yPosition, page.Width - 120, page.Height),
                            XStringFormats.TopLeft);
                        yPosition += 25;
                    }
                }

                // Enregistrer le document
                document.Save(filePath);
                MessageBox.Show($"PDF généré avec succès: {filePath}", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la génération du PDF: {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private List<Questionnaire> GetQuestionnaires()
        {
            List<Questionnaire> questionnaires = new List<Questionnaire>();

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                // Récupérer les questionnaires
                string query = @"SELECT q.ID, q.Libelle, t.Nom AS Theme 
                                FROM Questionnaire q 
                                JOIN Theme t ON q.ThemeID = t.ID";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Questionnaire q = new Questionnaire
                    {
                        Id = reader.GetInt32("ID"),
                        Libelle = reader.GetString("Libelle"),
                        Theme = reader.GetString("Theme"),
                        Questions = new List<Question>()
                    };
                    questionnaires.Add(q);
                }
                reader.Close();

                // Récupérer les questions pour chaque questionnaire
                foreach (var questionnaire in questionnaires)
                {
                    GetQuestionsForQuestionnaire(questionnaire.Id, questionnaire.Questions);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la récupération des questionnaires: {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return questionnaires;
        }

        private void GetQuestionsForQuestionnaire(int questionnaireId, List<Question> questions)
        {
            MySqlDataReader reader = null;
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                string query = @"SELECT Libelle, bonneReponse 
                                FROM Question 
                                WHERE QuestionnaireID = @QuestionnaireID";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@QuestionnaireID", questionnaireId);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    questions.Add(new Question
                    {
                        Libelle = reader.GetString("Libelle"),
                        BonneReponse = reader.GetString("bonneReponse")
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la récupération des questions: {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                reader?.Close();
            }
        }
    }

    public class Questionnaire
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Theme { get; set; }
        public List<Question> Questions { get; set; }
    }

    public class Question
    {
        public string Libelle { get; set; }
        public string BonneReponse { get; set; }
    }
}