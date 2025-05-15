using System.Data;
using MySql.Data.MySqlClient;

namespace gestionQuestionnaires
{
    public partial class QuestionnaireForm : Form
    {
        private MySqlConnection connection;
        private ContextMenuStrip contextMenuGridView;
        public QuestionnaireForm()
        {
            InitializeComponent();
            string connectionStringOff = "Server=localhost;Database=gestionquestionnaires;Uid=root;Pwd=;";
            string connectionStringOn = "Server=104.40.137.99;Port=22260;Database=mohamedamine_ppedesktop;Uid=developer;Pwd=cerfal1313;";
            connection = new MySqlConnection(connectionStringOn);

            // Initialisation du menu contextuel
            InitContextMenu();
        }
        private void QuestionnaireForm_Load(object sender, EventArgs e)
        {
            LoadQuestionnaires(); // Charger les questionnaires au chargement
        }
        private void InitContextMenu()
        {
            // Créer un menu contextuel
            contextMenuGridView = new ContextMenuStrip();

            // Ajouter les options
            var modifierMenuItem = new ToolStripMenuItem("Modifier", null, ModifierQuestionnaire);
            var supprimerMenuItem = new ToolStripMenuItem("Supprimer", null, SupprimerQuestionnaire);
            var ajouterMenuItem = new ToolStripMenuItem("Ajouter", null, AjouterQuestionnaire);

            contextMenuGridView.Items.Add(modifierMenuItem);
            contextMenuGridView.Items.Add(supprimerMenuItem);
            contextMenuGridView.Items.Add(ajouterMenuItem);

            // Associer le menu contextuel à la DataGridView
            gridViewQuestionnaire.ContextMenuStrip = contextMenuGridView;
        }

        // Charger les questionnaires dans la DataGridView
        private void LoadQuestionnaires()
        {
            try
            {
                connection.Open();

                string query = @"
        SELECT 
            ID AS 'ID',
            Libelle AS 'Nom du Questionnaire',
            ThemeID AS 'ThèmeID',
            (SELECT Nom FROM Theme WHERE Theme.ID = Questionnaire.ThemeID) AS 'Thème'
        FROM 
            Questionnaire";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                gridViewQuestionnaire.DataSource = table;

                // Assurer que la colonne ID est bien visible
                if (gridViewQuestionnaire.Columns["ID"] != null)
                {
                    gridViewQuestionnaire.Columns["ID"].Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des questionnaires : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        // Double-clic sur une ligne pour répondre au questionnaire


        // Ajouter un nouveau questionnaire
        private void AjouterQuestionnaire(object sender, EventArgs e)
        {
            using (var formAjout = new QuestionnaireFormModif(connection, 0))
            {
                formAjout.ShowDialog();
            }
            LoadQuestionnaires();
        }

        // Modifier un questionnaire existant
        private void ModifierQuestionnaire(object sender, EventArgs e)
        {
            if (gridViewQuestionnaire.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = gridViewQuestionnaire.SelectedRows[0];

                // Vérifier les noms exacts des colonnes
                string idColumn = "ID"; // À remplacer avec le bon nom trouvé dans l'étape 2
                string nomColumn = "Nom du Questionnaire"; // À vérifier
                string themeIDColumn = "ThèmeID"; // À vérifier
                string themeColumn = "Thème"; // À vérifier

                // Vérifier si les colonnes existent
                if (!gridViewQuestionnaire.Columns.Contains(idColumn) ||
                    !gridViewQuestionnaire.Columns.Contains(nomColumn) ||
                    !gridViewQuestionnaire.Columns.Contains(themeIDColumn) ||
                    !gridViewQuestionnaire.Columns.Contains(themeColumn))
                {
                    MessageBox.Show("⚠ Certaines colonnes sont manquantes dans DataGridView.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ Vérifie si l'ID est NULL avant de le convertir
                object idValue = selectedRow.Cells[idColumn].Value;
                int id = (idValue != DBNull.Value && idValue != null) ? Convert.ToInt32(idValue) : 0;

                string libelle = selectedRow.Cells[nomColumn].Value?.ToString() ?? "Inconnu";
                string themeID = selectedRow.Cells[themeIDColumn].Value?.ToString() ?? "0";
                string theme = selectedRow.Cells[themeColumn].Value?.ToString() ?? "Inconnu";

                MessageBox.Show($"ID: {id}, Nom: {libelle}, ThemeID: {themeID}, Thème: {theme}");

                if (id == 0)
                {
                    MessageBox.Show("⚠ L'ID du questionnaire est NULL. Vérifiez si la colonne ID est bien affichée.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ouvrir Form2 avec les bonnes valeurs
                using (var formModif = new QuestionnaireFormModif(connection, id))
                {
                    formModif.valeurInitiales(libelle);
                    formModif.TextBoxQuestionnaireID.Text = id.ToString();
                    formModif.TextBoxThemeID.Text = themeID;
                    formModif.TxtBoxNomTheme.Text = theme;
                    formModif.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une ligne pour la modifier.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // Supprimer un questionnaire
        private void SupprimerQuestionnaire(object sender, EventArgs e)
        {
            if (gridViewQuestionnaire.SelectedRows.Count > 0)
            {
                var selectedRow = gridViewQuestionnaire.SelectedRows[0];
                int id = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                // Confirmation avant suppression
                DialogResult dialogResult = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer cet élément ?",
                    "Confirmation de suppression",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        connection.Open();

                        string deleteQuery = "DELETE FROM Questionnaire WHERE ID = @ID";
                        MySqlCommand command = new MySqlCommand(deleteQuery, connection);
                        command.Parameters.AddWithValue("@ID", id);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Questionnaire supprimé avec succès.");
                        //LoadQuestionnaires(); // Recharger la liste après suppression
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
                MessageBox.Show("Veuillez sélectionner une ligne à supprimer.");
            }
        }

        // Gérer le clic droit sur la DataGridView
        private void gridViewQuestionnaire_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) // Vérifier si c'est un clic droit
            {
                var hitTestInfo = gridViewQuestionnaire.HitTest(e.X, e.Y);
                if (hitTestInfo.RowIndex >= 0) // Vérifier que la ligne cliquée est valide
                {
                    gridViewQuestionnaire.ClearSelection(); // Désélectionner toutes les lignes
                    gridViewQuestionnaire.Rows[hitTestInfo.RowIndex].Selected = true; // Sélectionner la ligne cliquée
                    gridViewQuestionnaire.CurrentCell = gridViewQuestionnaire.Rows[hitTestInfo.RowIndex].Cells[0];

                    // Debug pour vérifier si la ligne est bien sélectionnée
                   // MessageBox.Show($"Ligne sélectionnée : {hitTestInfo.RowIndex}");
                }
                // Vérifier si des données sont bien chargées dans le DataGridView
                if (gridViewQuestionnaire.Rows.Count == 0)
                {
                    MessageBox.Show("Aucun questionnaire n'est chargé dans DataGridView.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                /* Lister toutes les colonnes disponibles dans DataGridView
                string columnNames = string.Join(", ", gridViewQuestionnaire.Columns
                    .Cast<DataGridViewColumn>()
                    .Select(c => c.Name));

                MessageBox.Show($"Colonnes disponibles : {columnNames}", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);*/
            }
        }
        }
    }
