namespace gestionQuestionnaires
{
    partial class QuestionnaireResponseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblNomQuestionnaire = new Label();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            lblQuestion = new Label();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox4 = new CheckBox();
            lblProgress = new Label();
            lblScore = new Label();
            btnSuivant = new Button();
            btnTerminer = new Button();
            SuspendLayout();
            // 
            // lblNomQuestionnaire
            // 
            lblNomQuestionnaire.AutoSize = true;
            lblNomQuestionnaire.Location = new Point(268, 5);
            lblNomQuestionnaire.Margin = new Padding(2, 0, 2, 0);
            lblNomQuestionnaire.Name = "lblNomQuestionnaire";
            lblNomQuestionnaire.Size = new Size(0, 15);
            lblNomQuestionnaire.TabIndex = 0;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(305, 272);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(94, 19);
            radioButton1.TabIndex = 1;
            radioButton1.TabStop = true;
            radioButton1.Text = "radioButton1";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(305, 312);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(94, 19);
            radioButton2.TabIndex = 2;
            radioButton2.TabStop = true;
            radioButton2.Text = "radioButton2";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // lblQuestion
            // 
            lblQuestion.AutoSize = true;
            lblQuestion.Location = new Point(447, 193);
            lblQuestion.Name = "lblQuestion";
            lblQuestion.Size = new Size(38, 15);
            lblQuestion.TabIndex = 3;
            lblQuestion.Text = "label1";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(469, 272);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(82, 19);
            checkBox1.TabIndex = 4;
            checkBox1.Text = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(591, 312);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(82, 19);
            checkBox2.TabIndex = 5;
            checkBox2.Text = "checkBox2";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(469, 311);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(82, 19);
            checkBox3.TabIndex = 6;
            checkBox3.Text = "checkBox3";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(591, 272);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(82, 19);
            checkBox4.TabIndex = 7;
            checkBox4.Text = "checkBox4";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(56, 130);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(38, 15);
            lblProgress.TabIndex = 8;
            lblProgress.Text = "label2";
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Location = new Point(438, 422);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(38, 15);
            lblScore.TabIndex = 9;
            lblScore.Text = "label3";
            lblScore.Visible = false;
            // 
            // btnSuivant
            // 
            btnSuivant.Location = new Point(438, 498);
            btnSuivant.Name = "btnSuivant";
            btnSuivant.Size = new Size(75, 23);
            btnSuivant.TabIndex = 10;
            btnSuivant.Text = "Suivant";
            btnSuivant.UseVisualStyleBackColor = true;
            // 
            // btnTerminer
            // 
            btnTerminer.Location = new Point(438, 527);
            btnTerminer.Name = "btnTerminer";
            btnTerminer.Size = new Size(75, 23);
            btnTerminer.TabIndex = 11;
            btnTerminer.Text = "Terminer";
            btnTerminer.UseVisualStyleBackColor = true;
            btnTerminer.Visible = false;
            // 
            // QuestionnaireResponseForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1050, 597);
            Controls.Add(btnTerminer);
            Controls.Add(btnSuivant);
            Controls.Add(lblScore);
            Controls.Add(lblProgress);
            Controls.Add(checkBox4);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(lblQuestion);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(lblNomQuestionnaire);
            Name = "QuestionnaireResponseForm";
            Text = "Form3";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNomQuestionnaire;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private Label lblQuestion;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private Label lblProgress;
        private Label lblScore;
        private Button btnSuivant;
        private Button btnTerminer;
    }
}