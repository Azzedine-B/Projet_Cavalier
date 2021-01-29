namespace Projet_Cavalier
{
    partial class Form2
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.règlesDuJeuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paramètreDuJeuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.couleurDuDamierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noirBlancToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jauneEtChocolatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bleuEtRougeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(858, 335);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 68);
            this.button1.TabIndex = 1;
            this.button1.Text = "Générer une case de départ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(858, 431);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(160, 65);
            this.button2.TabIndex = 2;
            this.button2.Text = "J\'abandonne, aidez-moi !";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 3;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(858, 519);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(160, 68);
            this.button4.TabIndex = 5;
            this.button4.Text = "Revenir en arrière";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.paramètreDuJeuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1330, 33);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.règlesDuJeuToolStripMenuItem,
            this.quitterToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(73, 29);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // règlesDuJeuToolStripMenuItem
            // 
            this.règlesDuJeuToolStripMenuItem.Name = "règlesDuJeuToolStripMenuItem";
            this.règlesDuJeuToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.règlesDuJeuToolStripMenuItem.Text = "Règles du jeu";
            this.règlesDuJeuToolStripMenuItem.Click += new System.EventHandler(this.règlesDuJeuToolStripMenuItem_Click);
            // 
            // paramètreDuJeuToolStripMenuItem
            // 
            this.paramètreDuJeuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.couleurDuDamierToolStripMenuItem});
            this.paramètreDuJeuToolStripMenuItem.Name = "paramètreDuJeuToolStripMenuItem";
            this.paramètreDuJeuToolStripMenuItem.Size = new System.Drawing.Size(161, 29);
            this.paramètreDuJeuToolStripMenuItem.Text = "Paramètre du jeu";
            // 
            // couleurDuDamierToolStripMenuItem
            // 
            this.couleurDuDamierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noirBlancToolStripMenuItem,
            this.jauneEtChocolatToolStripMenuItem,
            this.bleuEtRougeToolStripMenuItem});
            this.couleurDuDamierToolStripMenuItem.Name = "couleurDuDamierToolStripMenuItem";
            this.couleurDuDamierToolStripMenuItem.Size = new System.Drawing.Size(261, 34);
            this.couleurDuDamierToolStripMenuItem.Text = "Couleur du damier";
            // 
            // noirBlancToolStripMenuItem
            // 
            this.noirBlancToolStripMenuItem.Name = "noirBlancToolStripMenuItem";
            this.noirBlancToolStripMenuItem.Size = new System.Drawing.Size(250, 34);
            this.noirBlancToolStripMenuItem.Text = "Noir et blanc";
            this.noirBlancToolStripMenuItem.Click += new System.EventHandler(this.noirBlancToolStripMenuItem_Click);
            // 
            // jauneEtChocolatToolStripMenuItem
            // 
            this.jauneEtChocolatToolStripMenuItem.Name = "jauneEtChocolatToolStripMenuItem";
            this.jauneEtChocolatToolStripMenuItem.Size = new System.Drawing.Size(250, 34);
            this.jauneEtChocolatToolStripMenuItem.Text = "Jaune et chocolat";
            this.jauneEtChocolatToolStripMenuItem.Click += new System.EventHandler(this.jauneEtChocolatToolStripMenuItem_Click);
            // 
            // bleuEtRougeToolStripMenuItem
            // 
            this.bleuEtRougeToolStripMenuItem.Name = "bleuEtRougeToolStripMenuItem";
            this.bleuEtRougeToolStripMenuItem.Size = new System.Drawing.Size(250, 34);
            this.bleuEtRougeToolStripMenuItem.Text = "Bleu et rouge";
            this.bleuEtRougeToolStripMenuItem.Click += new System.EventHandler(this.bleuEtRougeToolStripMenuItem_Click);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.quitterToolStripMenuItem.Text = "Quitter";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1063, 335);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(129, 68);
            this.button3.TabIndex = 8;
            this.button3.Text = "Recommencer";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1330, 941);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form2";
            this.Text = "Mode Jeu";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem règlesDuJeuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paramètreDuJeuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem couleurDuDamierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noirBlancToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jauneEtChocolatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bleuEtRougeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.Button button3;
    }
}