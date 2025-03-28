using System.Data;
using System.Drawing.Text;
using gestionQuestionnaires;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;


namespace gestionQuestionnaires
{

    public partial class QuestionnaireFormModif : Form
    {

        public TextBox TextBoxQuestionnaireID => txtQuestionnaireID;
        public TextBox TextBoxLibelle => textboxLibelle;
        public TextBox TextBoxThemeID => textBoxThemeID;
        public ComboBox TxtBoxNomTheme => txtBoxNomTheme;

        private MySqlConnection connection;

        private int questionnaireId; // ID du questionnaire (0 pour un nouvel ajout)
        private bool estAjt; // Détermine si l'on ajoute ou modifie
        public bool estModif = false;
       // string connString = "Server=localhost;Database=gestionquestionnaires;Uid=root;Pwd=;";
        private int obtentionIDQuestionnaire()
        {
            try
            {
                connection.Open();

                // Récupérer le dernier ID
                string sqlCommande = "SELECT MAX(ID) FROM Questionnaire";
                MySqlCommand command = new MySqlCommand(sqlCommande, connection);
                object resultat = command.ExecuteScalar();

                // Si la table est vide, retourner 1
                if (resultat == DBNull.Value || resultat == null)
                {
                    return 1;
                }

                return Convert.ToInt32(resultat) + 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la récupération du prochain ID : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }
        private int obtentionProchaineID()
        {
            try
            {
                connection.Open();
                // Requête pour obtenir le dernier ID de la table Question
                string sqlCommande = "SELECT MAX(ID) FROM Question";
                MySqlCommand command = new MySqlCommand(sqlCommande, connection);
                object resultat = command.ExecuteScalar();

                // Si aucun résultat, retourner 1
                if (resultat == DBNull.Value || resultat == null)
                {
                    return 1;
                }

                // Retourner le prochain ID
                return Convert.ToInt32(resultat) + 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la récupération du prochain ID Question : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; // Retourner une valeur invalid si une erreur s'est produite
            }
            finally
            {
                connection.Close();
            }
        }
        public QuestionnaireFormModif(int id = 0)
        {
            InitializeComponent();

            string connString = "Server=localhost;Database=gestionquestionnaires;Uid=root;Pwd=;";
            connection = new MySqlConnection(connString);

            questionnaireId = id;

            if (id > 0)
            {
                txtQuestionnaireID.Text = id.ToString();
            }
            else
            {
                int nextId = obtentionIDQuestionnaire();
                txtQuestionnaireID.Text = nextId.ToString();
            }
        }
        private bool estAjout;
        public QuestionnaireFormModif(MySqlConnection conn, int id = 0)
        {
            InitializeComponent();
            connection = conn;
            questionnaireId = id;

            estAjout = id == 0; // Mode ajout si ID = 0, sinon modification

            if (estAjout)
                txtQuestionnaireID.Text = obtentionIDQuestionnaire().ToString();
            else
                txtQuestionnaireID.Text = questionnaireId.ToString();
        }
        private void QuestionnaireFormModif_Load(object sender, EventArgs e)
        {
            LoadThemes(); // Appel impératif ici !

            if (!estAjout)
            {
                LoadQuestionnaireDetails();
                questionPourQuestionnaire(questionnaireId);
            }
        }
        private void questionPourQuestionnaire(int questionnaireId)
        {
            try
            {
                // Vider la DataGridView avant de recharger les données
                dataGridViewQuestions.DataSource = null;

                connection.Open();

                // Requête SQL pour récupérer les questions associées à l'ID du questionnaire
                string query = @"
        SELECT 
            ID AS 'ID Question',
            Libelle AS 'Question',
            bonneReponse AS 'Bonne réponse',
            IFNULL(Reponse1, '') AS 'Choix1',
            IFNULL(Reponse2, '') AS 'Choix2',
            IFNULL(Reponse3, '') AS 'Choix3',
            VraiFaux AS 'Vrai', 
            FauxVrai AS 'Faux', 
            QuestionnaireID AS 'Questionnaire ID'
        FROM 
            Question
        WHERE 
            QuestionnaireID = @QuestionnaireID";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@QuestionnaireID", questionnaireId);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                // vérification avant d'afficher
                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("⚠ Aucune question trouvée pour ce questionnaire.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Lier les données à la DataGridView
                dataGridViewQuestions.DataSource = table;

                // Masquer les colonnes inutiles
                if (dataGridViewQuestions.Columns["ID Question"] != null)
                    dataGridViewQuestions.Columns["ID Question"].Visible = false;
                if (dataGridViewQuestions.Columns["Questionnaire ID"] != null)
                    dataGridViewQuestions.Columns["Questionnaire ID"].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des questions : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }
        // Charger les thèmes existants dans la ComboBox
        private void LoadThemes()
        {
            try
            {
                connection.Open();

                string query = "SELECT ID, Nom FROM Theme";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);

                // Debug : affiche clairement le nombre de thèmes récupérés
                MessageBox.Show($"Nombre de thèmes récupérés : {table.Rows.Count}");

                txtBoxNomTheme.DataSource = table;
                txtBoxNomTheme.DisplayMember = "Nom";
                txtBoxNomTheme.ValueMember = "ID";

                // Force un rafraichissement
                txtBoxNomTheme.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
        public void valeurInitiales(string libelle)
        {
            textboxLibelle.Text = libelle; // Remplir la TextBox avec le libellé du questionnaire
        }

        private void LoadQuestionnaireDetails()
        {
            try
            {
                connection.Open();

                string query = @"
            SELECT 
                q.ID AS 'QuestionnaireID', 
                q.Libelle, 
                q.ThemeID, 
                t.Nom AS ThemeName
            FROM Questionnaire q
            LEFT JOIN Theme t ON q.ThemeID = t.ID
            WHERE q.ID = @ID";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", questionnaireId);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtQuestionnaireID.Text = reader["QuestionnaireID"].ToString();
                        textboxLibelle.Text = reader["Libelle"].ToString();
                        textBoxThemeID.Text = reader["ThemeID"].ToString();

                        // Sélectionner automatiquement le thème dans la ComboBox
                        txtBoxNomTheme.SelectedValue = Convert.ToInt32(reader["ThemeID"]);
                    }
                    else
                    {
                        MessageBox.Show("Questionnaire introuvable.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
        private bool validationThemeID(int themeId) //Limite de 1 a 3 pour le theme ID
        {
            try
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Theme WHERE ID = @ThemeID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ThemeID", themeId);

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0; // Retourne vrai si le ThemeID existe
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la validation du ThemeID : {ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        // Charger les questions associées à un questionnaire
        private void LoadQuestions()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtQuestionnaireID.Text))
                {
                    MessageBox.Show("L'ID du questionnaire est introuvable. Assurez-vous que les détails du questionnaire sont chargés.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Requête SQL pour récupérer les questions liées à l'ID du questionnaire
                string query = @"
        SELECT 
            ID AS 'ID Question', 
            Libelle AS 'Question',
            bonneReponse AS 'Bonne Réponse'
        FROM 
            Question
        WHERE 
            QuestionnaireID = @QuestionnaireID";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@QuestionnaireID", txtQuestionnaireID.Text);
                DataTable table = new DataTable();

                adapter.Fill(table);

                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("Aucune question trouvée pour ce questionnaire.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dataGridViewQuestions.DataSource = table; // Lier les données au DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des questions : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        // Gestion du clic sur "Sauvegarder" : ajout ou modification
        private void boutonSauvegarde_Click(object sender, EventArgs e)
        {
            try
            {
                // Étape 1 : Sauvegarder le questionnaire (ajout ou modification)
                if (estAjout)
                    AjouterQuestionnaire();
                else
                    MAJQuestionnaire();

                // Étape 2 : Ensuite sauvegarder les questions associées
                SauvegarderQuestions();

                MessageBox.Show("✅ Questionnaire et questions sauvegardés avec succès !",
                                "Succès",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                // Fermer automatiquement le formulaire après succès
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la sauvegarde : {ex.Message}",
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Ajouter un nouveau questionnaire
        private void AjouterQuestionnaire()
        {
            if (string.IsNullOrWhiteSpace(textboxLibelle.Text))
            {
                MessageBox.Show("Veuillez entrer un nom de questionnaire.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int themeId = Convert.ToInt32(txtBoxNomTheme.SelectedValue);

            try
            {
                connection.Open();
                string query = "INSERT INTO Questionnaire (Libelle, ThemeID) VALUES (@Libelle, @ThemeID)";
                using (MySqlCommand commande = new MySqlCommand(query, connection))
                {
                    commande.Parameters.AddWithValue("@Libelle", textboxLibelle.Text);
                    commande.Parameters.AddWithValue("@ThemeID", themeId);
                    commande.ExecuteNonQuery();
                }

                string lastInsertIdQuery = "SELECT LAST_INSERT_ID()";
                using (MySqlCommand lastInsertIdCommand = new MySqlCommand(lastInsertIdQuery, connection))
                {
                    int questionnaireId = Convert.ToInt32(lastInsertIdCommand.ExecuteScalar());
                    txtQuestionnaireID.Text = questionnaireId.ToString();
                    MessageBox.Show($"Questionnaire ajouté avec succès avec l'ID {questionnaireId}.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }
        private bool VerifSiQuestionExiste(int questionId)
        {
            bool exists = false;
            bool wasClosed = false; // Vérifier si on doit fermer la connexion après l'exécution

            try
            {
                // Vérifier si la connexion est fermée avant de l'ouvrir
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                    wasClosed = true; // Marquer pour fermeture après l'exécution
                }

                string query = "SELECT COUNT(*) FROM Question WHERE ID = @ID";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ID", questionId);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    exists = count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la vérification de l'existence de la question : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (wasClosed && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return exists;
        }

        // Sauvegarder les questions associées
        // Méthode pour sauvegarder les questions associées au questionnaire
        private void SauvegarderQuestions()
        {
            MySqlTransaction transaction = null;

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                transaction = connection.BeginTransaction();

                int questionnaireId = Convert.ToInt32(txtQuestionnaireID.Text);
                int themeId = Convert.ToInt32(txtBoxNomTheme.SelectedValue);

                foreach (DataGridViewRow row in dataGridViewQuestions.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string libelle = row.Cells["Question"].Value?.ToString() ?? "";
                        string bonneReponse = row.Cells["Bonne réponse"].Value?.ToString() ?? "";
                        string choix1 = row.Cells["Choix1"].Value?.ToString() ?? "";
                        string choix2 = row.Cells["Choix2"].Value?.ToString() ?? "";
                        string choix3 = row.Cells["Choix3"].Value?.ToString() ?? "";
                        bool vraiFaux = row.Cells["Vrai"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["Vrai"].Value);
                        bool fauxVrai = row.Cells["Faux"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["Faux"].Value);

                        int questionId = row.Cells["ID Question"].Value != null ? Convert.ToInt32(row.Cells["ID Question"].Value) : 0;

                        if (questionId == 0 || !VerifSiQuestionExiste(questionId))
                        {
                            string insertQuery = @"
                        INSERT INTO Question 
                        (Libelle, bonneReponse, Reponse1, Reponse2, Reponse3, VraiFaux, FauxVrai, QuestionnaireID, ThemeID)
                        VALUES 
                        (@Libelle, @BonneReponse, @Choix1, @Choix2, @Choix3, @Vrai, @Faux, @QuestionnaireID, @ThemeID)";

                            using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@Libelle", libelle);
                                insertCommand.Parameters.AddWithValue("@BonneReponse", bonneReponse);
                                insertCommand.Parameters.AddWithValue("@Choix1", choix1);
                                insertCommand.Parameters.AddWithValue("@Choix2", choix2);
                                insertCommand.Parameters.AddWithValue("@Choix3", choix3);
                                insertCommand.Parameters.AddWithValue("@Vrai", vraiFaux);
                                insertCommand.Parameters.AddWithValue("@Faux", fauxVrai);
                                insertCommand.Parameters.AddWithValue("@QuestionnaireID", questionnaireId);
                                insertCommand.Parameters.AddWithValue("@ThemeID", themeId);
                                insertCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string updateQuery = @"
                        UPDATE Question SET
                            Libelle = @Libelle,
                            bonneReponse = @BonneReponse,
                            Reponse1 = @Choix1,
                            Reponse2 = @Choix2,
                            Reponse3 = @Choix3,
                            ThemeID = @ThemeID
                        WHERE ID = @ID";

                            using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection, transaction))
                            {
                                updateCommand.Parameters.AddWithValue("@Libelle", libelle);
                                updateCommand.Parameters.AddWithValue("@BonneReponse", bonneReponse);
                                updateCommand.Parameters.AddWithValue("@Choix1", choix1);
                                updateCommand.Parameters.AddWithValue("@Choix2", choix2);
                                updateCommand.Parameters.AddWithValue("@Choix3", choix3);
                                updateCommand.Parameters.AddWithValue("@ThemeID", themeId);
                                updateCommand.Parameters.AddWithValue("@ID", questionId);
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show($"Erreur lors de la sauvegarde des questions : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }
        private void SauvegarderQuestionnaire()
        {
            try
            {
                int questionnaireId;

                // Vérifier si l'ID du questionnaire est valide
                if (string.IsNullOrWhiteSpace(txtQuestionnaireID.Text) || !int.TryParse(txtQuestionnaireID.Text, out questionnaireId) || questionnaireId == 0)
                {
                    // Si l'ID est vide, on crée le questionnaire
                    AjouterQuestionnaire();

                    // Vérifier que l'ajout a bien récupéré un ID valide
                    if (!int.TryParse(txtQuestionnaireID.Text, out questionnaireId) || questionnaireId == 0)
                    {
                        MessageBox.Show("❌ Erreur : Le questionnaire n'a pas pu être créé.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Maintenant que l'ID est valide, on peut mettre à jour
                MAJQuestionnaire();

                // Sauvegarder les questions associées au questionnaire
                SauvegarderQuestions();

                MessageBox.Show("✅ Toutes les données ont été sauvegardées avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erreur lors de la sauvegarde du questionnaire : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void verifLogique()
        {
            foreach (DataGridViewRow row in dataGridViewQuestions.Rows)
            {
                // Récupérer les valeurs des colonnes Vrai/Faux et des choix multiples
                bool vraiFaux = row.Cells["Vrai"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["Vrai"].Value);
                bool fauxVrai = row.Cells["Faux"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["Faux"].Value);
                string choix1 = row.Cells["Choix1"].Value?.ToString() ?? string.Empty;
                string choix2 = row.Cells["Choix2"].Value?.ToString() ?? string.Empty;
                string choix3 = row.Cells["Choix3"].Value?.ToString() ?? string.Empty;

                // Si "Vrai/Faux" est activé, désactiver les colonnes des choix multiples
                if (vraiFaux || fauxVrai)
                {
                    row.Cells["Choix1"].ReadOnly = true;
                    row.Cells["Choix2"].ReadOnly = true;
                    row.Cells["Choix3"].ReadOnly = true;
                    row.Cells["Choix1"].Style.BackColor = Color.LightGray;
                    row.Cells["Choix2"].Style.BackColor = Color.LightGray;
                    row.Cells["Choix3"].Style.BackColor = Color.LightGray;
                }
                else if (!string.IsNullOrWhiteSpace(choix1) || !string.IsNullOrWhiteSpace(choix2) || !string.IsNullOrWhiteSpace(choix3))
                {
                    // Si des choix multiples existent, désactiver les colonnes Vrai/Faux
                    row.Cells["Vrai"].ReadOnly = true;
                    row.Cells["Faux"].ReadOnly = true;

                    row.Cells["Vrai"].Style.BackColor = Color.LightGray;
                    row.Cells["Faux"].Style.BackColor = Color.LightGray;
                }
                else
                {
                    // Si aucune règle ne s'applique, réactiver tout
                    row.Cells["Choix1"].ReadOnly = false;
                    row.Cells["Choix2"].ReadOnly = false;
                    row.Cells["Choix3"].ReadOnly = false;
                    row.Cells["Vrai"].ReadOnly = false;
                    row.Cells["Faux"].ReadOnly = false;

                    row.Cells["Choix1"].Style.BackColor = Color.White;
                    row.Cells["Choix2"].Style.BackColor = Color.White;
                    row.Cells["Choix3"].Style.BackColor = Color.White;
                    row.Cells["Vrai"].Style.BackColor = Color.White;
                    row.Cells["Faux"].Style.BackColor = Color.White;
                }
            }
        }

        private void MAJQuestionnaire()
        {
            if (!int.TryParse(txtQuestionnaireID.Text, out int questionnaireId) || questionnaireId <= 0)
            {
                MessageBox.Show("L'ID du questionnaire est invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int themeId = Convert.ToInt32(txtBoxNomTheme.SelectedValue);

            if (!validationThemeID(themeId))
            {
                MessageBox.Show("Le thème sélectionné est invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                connection.Open();

                string updateQuery = @"
            UPDATE Questionnaire 
            SET Libelle = @Libelle, ThemeID = @ThemeID, DateModification = NOW()
            WHERE ID = @ID";

                using (MySqlCommand vCommande = new MySqlCommand(updateQuery, connection))
                {
                    vCommande.Parameters.AddWithValue("@Libelle", textboxLibelle.Text);
                    vCommande.Parameters.AddWithValue("@ThemeID", themeId);
                    vCommande.Parameters.AddWithValue("@ID", questionnaireId);
                    vCommande.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        // Modifier une question
        private void EditQuestion(object sender, EventArgs e)
        {
            if (dataGridViewQuestions.SelectedRows.Count > 0)
            {
                string questionId = dataGridViewQuestions.SelectedRows[0].Cells["ID"].Value.ToString();
                string questionText = dataGridViewQuestions.SelectedRows[0].Cells["Question"].Value.ToString();

                string newText = Microsoft.VisualBasic.Interaction.InputBox(
                    "Modifier la question :", "Modification", questionText);

                if (!string.IsNullOrEmpty(newText))
                {
                    try
                    {
                        connection.Open();
                        string query = "UPDATE Question SET Texte = @Texte WHERE ID = @ID";
                        MySqlCommand vCommande = new MySqlCommand(query, connection);
                        vCommande.Parameters.AddWithValue("@Texte", newText);
                        vCommande.Parameters.AddWithValue("@ID", questionId);
                        vCommande.ExecuteNonQuery();

                        LoadQuestions(); // Recharger les questions après modification
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de la modification : {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        // Supprimer une question
        private void DeleteQuestion(object sender, EventArgs e)
        {
            if (dataGridViewQuestions.SelectedRows.Count > 0)
            {
                string questionId = dataGridViewQuestions.SelectedRows[0].Cells["ID"].Value.ToString();

                DialogResult result = MessageBox.Show("Supprimer cette question ?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM Question WHERE ID = @ID";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", questionId);
                        command.ExecuteNonQuery();

                     //   rafData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de la suppression : {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtQuestionnaireID_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtQuestionnaireID.Text, out int id))
            {
                questionPourQuestionnaire(id);
            }
            else
            {
                dataGridViewQuestions.DataSource = null; // Vider la DataGridView si l'ID est invalide
            }
        }
        private void dataGridViewQuestions_AutoSizeColumnsModeChanged(object sender, DataGridViewAutoSizeColumnsModeEventArgs e)
        {

        }

        private void questionSupprBtn_Click(object sender, EventArgs e)
        {
            if (dataGridViewQuestions.SelectedRows.Count > 0)
            {
                // Récupérer l'ID de la question sélectionnée
                DataGridViewRow selectedRow = dataGridViewQuestions.SelectedRows[0];
                int questionId = Convert.ToInt32(selectedRow.Cells["ID Question"].Value);

                // Demander confirmation
                DialogResult dialogResult = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette question ?",
                    "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        // Supprimer de la base de données
                        connection.Open();
                        string deleteQuery = "DELETE FROM Question WHERE ID = @ID";
                        MySqlCommand command = new MySqlCommand(deleteQuery, connection);
                        command.Parameters.AddWithValue("@ID", questionId);
                        command.ExecuteNonQuery();

                        // Supprimer de la DataGridView
                        dataGridViewQuestions.Rows.Remove(selectedRow);

                        MessageBox.Show("Question supprimée avec succès !");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de la suppression : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une question à supprimer.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void questionAjtBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtQuestionnaireID.Text))
                {
                    MessageBox.Show("L'ID du questionnaire est introuvable. Veuillez vérifier.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Vérification si la DataTable est liée à la DataGridView
                DataTable dataTable = dataGridViewQuestions.DataSource as DataTable;

                if (dataTable != null)
                {
                    // Obtenir le prochain ID pour la nouvelle question
                    int nextQuestionID = obtentionProchaineID();

                    if (nextQuestionID <= 0)
                    {
                        MessageBox.Show("Impossible de générer un nouvel ID pour la question.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Ajouter une nouvelle ligne avec les colonnes vides et le prochain ID
                    DataRow newRow = dataTable.NewRow();
                    newRow["ID Question"] = nextQuestionID; // Assigner l'ID généré
                    newRow["Question"] = string.Empty;     // Valeur par défaut pour la colonne Question
                    newRow["Bonne Réponse"] = string.Empty; // Valeur par défaut pour Bonne Réponse
                    newRow["Choix1"] = string.Empty;
                    newRow["Choix2"] = string.Empty;
                    newRow["Choix3"] = string.Empty;
                    newRow["Vrai"] = false;
                    newRow["Faux"] = false;
                    newRow["Questionnaire ID"] = txtQuestionnaireID.Text; // ID du questionnaire

                    // Ajouter la nouvelle ligne à la DataTable
                    dataTable.Rows.Add(newRow);

                    // Informer l'utilisateur
                    MessageBox.Show($"Nouvelle question ajoutée avec l'ID {nextQuestionID}. Remplissez les détails puis cliquez sur 'Sauvegarder'.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erreur : La DataGridView n'est pas liée à une source de données valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout de la nouvelle question : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void dataGridViewQuestions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            verifLogique();
        }
        private void txtBoxNomTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtBoxNomTheme.SelectedValue != null)
                textBoxThemeID.Text = txtBoxNomTheme.SelectedValue.ToString();
        }

    }
}
