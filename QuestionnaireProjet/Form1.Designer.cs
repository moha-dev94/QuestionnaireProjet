namespace gestionQuestionnaires
{
    partial class QuestionnaireForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">Indique si les ressources doivent être supprimées.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - 
        /// ne modifiez pas le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            gridViewQuestionnaire = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)gridViewQuestionnaire).BeginInit();
            SuspendLayout();
            // 
            // gridViewQuestionnaire
            // 
            gridViewQuestionnaire.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridViewQuestionnaire.ColumnHeadersHeight = 34;
            gridViewQuestionnaire.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridViewQuestionnaire.EditMode = DataGridViewEditMode.EditOnEnter;
            gridViewQuestionnaire.GridColor = SystemColors.Info;
            gridViewQuestionnaire.Location = new Point(146, 77);
            gridViewQuestionnaire.Name = "gridViewQuestionnaire";
            gridViewQuestionnaire.ReadOnly = true;
            gridViewQuestionnaire.RowHeadersWidth = 62;
            gridViewQuestionnaire.Size = new Size(1000, 400);
            gridViewQuestionnaire.TabIndex = 0;
            gridViewQuestionnaire.CellMouseDoubleClick += gridViewQuestionnaire_CellMouseDoubleClick;
            gridViewQuestionnaire.MouseDown += gridViewQuestionnaire_MouseDown;
            // 
            // QuestionnaireForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1301, 600);
            Controls.Add(gridViewQuestionnaire);
            Name = "QuestionnaireForm";
            Text = "Gestion des Questionnaires";
            Load += QuestionnaireForm_Load;
            ((System.ComponentModel.ISupportInitialize)gridViewQuestionnaire).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView gridViewQuestionnaire;
    }
}
