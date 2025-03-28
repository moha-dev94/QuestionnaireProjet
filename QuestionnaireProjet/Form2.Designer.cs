namespace gestionQuestionnaires
{
    partial class QuestionnaireFormModif
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewQuestions;

        private void InitializeComponent()
        {
            dataGridViewQuestions = new DataGridView();
            label1 = new Label();
            boutonSauvegarde = new Button();
            label3 = new Label();
            textBoxThemeID = new TextBox();
            txtQuestionnaireID = new TextBox();
            label2 = new Label();
            questionAjtBtn = new Button();
            questionSupprBtn = new Button();
            label4 = new Label();
            textboxLibelle = new TextBox();
            txtBoxNomTheme = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewQuestions).BeginInit();
            this.Load += new System.EventHandler(this.QuestionnaireFormModif_Load);
            SuspendLayout();
            // 
            // dataGridViewQuestions
            // 
            dataGridViewQuestions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewQuestions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewQuestions.Location = new Point(12, 25);
            dataGridViewQuestions.Name = "dataGridViewQuestions";
            dataGridViewQuestions.RowHeadersWidth = 62;
            dataGridViewQuestions.Size = new Size(840, 373);
            dataGridViewQuestions.TabIndex = 0;
            dataGridViewQuestions.AutoSizeColumnsModeChanged += dataGridViewQuestions_AutoSizeColumnsModeChanged;
            dataGridViewQuestions.CellValueChanged += dataGridViewQuestions_CellValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(933, 25);
            label1.Name = "label1";
            label1.Size = new Size(190, 25);
            label1.TabIndex = 3;
            label1.Text = "Nom du questionnaire";
            label1.Click += label1_Click;
            // 
            // boutonSauvegarde
            // 
            boutonSauvegarde.Location = new Point(965, 403);
            boutonSauvegarde.Name = "boutonSauvegarde";
            boutonSauvegarde.Size = new Size(148, 34);
            boutonSauvegarde.TabIndex = 5;
            boutonSauvegarde.Text = "Sauvegarder";
            boutonSauvegarde.UseVisualStyleBackColor = true;
            boutonSauvegarde.Click += boutonSauvegarde_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(933, 231);
            label3.Name = "label3";
            label3.Size = new Size(116, 25);
            label3.TabIndex = 7;
            label3.Text = "N° du Thème";
            label3.Click += label3_Click;
            // 
            // textBoxThemeID
            // 
            textBoxThemeID.Location = new Point(933, 259);
            textBoxThemeID.Name = "textBoxThemeID";
            textBoxThemeID.Size = new Size(232, 31);
            textBoxThemeID.TabIndex = 8;
            // 
            // txtQuestionnaireID
            // 
            txtQuestionnaireID.Location = new Point(933, 332);
            txtQuestionnaireID.Name = "txtQuestionnaireID";
            txtQuestionnaireID.Size = new Size(232, 31);
            txtQuestionnaireID.TabIndex = 9;
            txtQuestionnaireID.TextChanged += txtQuestionnaireID_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(933, 304);
            label2.Name = "label2";
            label2.Size = new Size(170, 25);
            label2.TabIndex = 10;
            label2.Text = "N° du questionnaire";
            // 
            // questionAjtBtn
            // 
            questionAjtBtn.Location = new Point(65, 414);
            questionAjtBtn.Name = "questionAjtBtn";
            questionAjtBtn.Size = new Size(182, 23);
            questionAjtBtn.TabIndex = 11;
            questionAjtBtn.Text = "Ajouter une nouvelle question";
            questionAjtBtn.UseVisualStyleBackColor = true;
            questionAjtBtn.Click += questionAjtBtn_Click;
            // 
            // questionSupprBtn
            // 
            questionSupprBtn.Location = new Point(599, 414);
            questionSupprBtn.Name = "questionSupprBtn";
            questionSupprBtn.Size = new Size(161, 23);
            questionSupprBtn.TabIndex = 12;
            questionSupprBtn.Text = "Supprimer";
            questionSupprBtn.UseVisualStyleBackColor = true;
            questionSupprBtn.Click += questionSupprBtn_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(933, 87);
            label4.Name = "label4";
            label4.Size = new Size(136, 25);
            label4.TabIndex = 14;
            label4.Text = "Nom du Thème";
            // 
            // textboxLibelle
            // 
            textboxLibelle.Location = new Point(933, 53);
            textboxLibelle.Name = "textboxLibelle";
            textboxLibelle.Size = new Size(232, 31);
            textboxLibelle.TabIndex = 15;
            // 
            // txtBoxNomTheme
            // 
            txtBoxNomTheme.DropDownStyle = ComboBoxStyle.DropDownList;
            txtBoxNomTheme.FormattingEnabled = true;
            txtBoxNomTheme.Location = new Point(933, 127);
            txtBoxNomTheme.Name = "txtBoxNomTheme";
            txtBoxNomTheme.Size = new Size(229, 33);
            txtBoxNomTheme.TabIndex = 16;
            txtBoxNomTheme.SelectedIndexChanged += txtBoxNomTheme_SelectedIndexChanged;
            // 
            // QuestionnaireFormModif
            // 
            ClientSize = new Size(1202, 459);
            Controls.Add(txtBoxNomTheme);
            Controls.Add(textboxLibelle);
            Controls.Add(label4);
            Controls.Add(questionSupprBtn);
            Controls.Add(questionAjtBtn);
            Controls.Add(label2);
            Controls.Add(txtQuestionnaireID);
            Controls.Add(textBoxThemeID);
            Controls.Add(label3);
            Controls.Add(boutonSauvegarde);
            Controls.Add(label1);
            Controls.Add(dataGridViewQuestions);
            Name = "QuestionnaireFormModif";
            ((System.ComponentModel.ISupportInitialize)dataGridViewQuestions).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
        private Button boutonSauvegarde;
        private Label label3;
        private TextBox textBoxThemeID;
        private TextBox txtQuestionnaireID;
        private Label label2;
        private Button questionAjtBtn;
        private Button questionSupprBtn;
        private Label label4;
        private TextBox textboxLibelle;
        private ComboBox txtBoxNomTheme;
    }
}
