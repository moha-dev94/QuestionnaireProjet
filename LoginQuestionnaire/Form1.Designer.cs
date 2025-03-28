namespace gestionQuestionnaires
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblNomUser;
        private Label lblMdp;
        private TextBox txtNomUtilisateur;
        private TextBox txtMotDePasse;
        private Button btnConnexion;
        private Label lblErreur;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">booléen indiquant si les ressources doivent être supprimées.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Initialisation des composants de l'interface utilisateur.
        /// </summary>
        private void InitializeComponent()
        {
            lblNomUser = new Label();
            lblMdp = new Label();
            txtNomUtilisateur = new TextBox();
            txtMotDePasse = new TextBox();
            btnConnexion = new Button();
            lblErreur = new Label();
            SuspendLayout();
            // 
            // lblNomUser
            // 
            lblNomUser.AutoSize = true;
            lblNomUser.Location = new Point(254, 142);
            lblNomUser.Name = "lblNomUser";
            lblNomUser.Size = new Size(148, 25);
            lblNomUser.TabIndex = 0;
            lblNomUser.Text = "Nom d'utilisateur";
            // 
            // lblMdp
            // 
            lblMdp.AutoSize = true;
            lblMdp.Location = new Point(254, 203);
            lblMdp.Name = "lblMdp";
            lblMdp.Size = new Size(120, 25);
            lblMdp.TabIndex = 1;
            lblMdp.Text = "Mot de passe";
            // 
            // txtNomUtilisateur
            // 
            txtNomUtilisateur.Location = new Point(438, 142);
            txtNomUtilisateur.Name = "txtNomUtilisateur";
            txtNomUtilisateur.Size = new Size(150, 31);
            txtNomUtilisateur.TabIndex = 2;
            // 
            // txtMotDePasse
            // 
            txtMotDePasse.Location = new Point(438, 203);
            txtMotDePasse.Name = "txtMotDePasse";
            txtMotDePasse.Size = new Size(150, 31);
            txtMotDePasse.TabIndex = 3;
            txtMotDePasse.UseSystemPasswordChar = true;
            // 
            // btnConnexion
            // 
            btnConnexion.Location = new Point(441, 277);
            btnConnexion.Name = "btnConnexion";
            btnConnexion.Size = new Size(112, 34);
            btnConnexion.TabIndex = 4;
            btnConnexion.Text = "Connexion";
            btnConnexion.UseVisualStyleBackColor = true;
            btnConnexion.Click += btnConnexion_Click;
            // 
            // lblErreur
            // 
            lblErreur.AutoSize = true;
            lblErreur.ForeColor = Color.Red;
            lblErreur.Location = new Point(381, 75);
            lblErreur.Name = "lblErreur";
            lblErreur.Size = new Size(169, 25);
            lblErreur.TabIndex = 5;
            lblErreur.Text = "Erreur de connexion";
            lblErreur.Visible = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(923, 450);
            Controls.Add(lblErreur);
            Controls.Add(btnConnexion);
            Controls.Add(txtMotDePasse);
            Controls.Add(txtNomUtilisateur);
            Controls.Add(lblMdp);
            Controls.Add(lblNomUser);
            Name = "LoginForm";
            Text = "Connexion";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}