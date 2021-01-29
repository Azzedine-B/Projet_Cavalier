using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_Cavalier
{
    public partial class Form2 : Form
    {
        static int[,] echec = new int[12, 12];
        static int[] depi = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
        static int[] depj = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
        

        /* Définitions et déclarations */
        int nb_fuite, min_fuite, lmin_fuite = 0;
        int i, j, k, l, ii, jj, saisieI, saisieJ;
        Button[,] echiquier;
        Image cavalier;
        static int[] dernierI = new int[64];
        static int[] dernierJ = new int[64];
        bool pause = false;
        int gardeI = 0, gardeJ = 0;
        int cptTour;
        int cptRetour;

        /** 
         * Load formulaire 2
         * Initialise l'échiquier comme un tableau 2D de boutons 12 * 12
         */
        private void Form2_Load(object sender, EventArgs e)
        {
            this.cptTour = 0;
            this.cptRetour = 5;
            this.cavalier = Image.FromFile("img\\cavalier.jpg");
            this.echiquier = new Button[12, 12];
            this.label1.Text = "Choisissez une case ou cliquez pour en générer une aléatoirement";
            this.button2.Enabled = false;
            this.button4.Enabled = false;
            //initialisation des cases d'échecs
            for (i = 0; i < 12; i++)
                for (j = 0; j < 12; j++)
                    echec[i, j] = ((i < 2 | i > 9 | j < 2 | j > 9) ? -1 : 0);

            // initialisation des boutton de l'échiquier
            for (int l = 0; l < 12; l++)
            {
                for (int c = 0; c < 12; c++)
                {
                    Button b;
                    b = new Button();
                    b.Location = new Point(l * 50, c * 50);
                    b.Size = new Size(50, 50);
                    b.Click += new System.EventHandler(this.Mon_Bouton_Click);
                    if (c < 2 | c > 9 || l < 2 | l > 9)
                    {
                        b.Visible = false;
                        b.Enabled = false;
                    }
                        
                    this.echiquier[l, c] = b;
                    this.Controls.Add(b);
                }
            }
        }

        public Form2()
        {
            InitializeComponent();
        }

        /* Bouton qui permet de sélectionner une case de départ pour l'utilisateur
         * Génére deux chiffres aléatoires
         * Place le cavalier et débute la partie à cet endroit
         * Stock les coordonnées de la première case jouée pour la fonction abandon & simulation
         */
        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            //effacerEchiquier();
            Random random = new Random();
            int iR = random.Next(1, 8) + 1;
            int jR = random.Next(1, 8) + 1;
            // iR et jR evoluent de 2 à 9 !
            //lance la simulation à la case générée.
            jouerModeJoueur(iR, jR);
            button2.Enabled = true;
            this.gardeI = iR;
            this.gardeJ = jR;
        }

        /** 
         * Bouton qui permet d'abandonner la partie et de jouer la simulation du départ
         * Efface l'échiquier et réactive l'ensemble des cases
         * Récupère les données du 1er coup jouer et lance le jeu en mode simulation
         */
        private void button2_Click(object sender, EventArgs e)
        {
            effacerEchiquier();
            activerEchiquier();
            jouer(gardeI, gardeJ, 1000, 1);
            button4.Enabled = false;
        }

        /* Bouton qui permet de revenir en arrière
         * Décrémente le compteur de retour et le cptTour (pour savoir à quel indice du tableau dernierI/dernierJ on se trouve
         * Désactive le bouton lorsque le joueur a utilisé toutes ses tentatives de retour
         */
        private void button4_Click(object sender, EventArgs e)
        {
            cptRetour--;
            button4.Text = "Retour en arrière" + "(" + cptRetour + ")";
            if (cptRetour > 0)
            {
                cptTour--;
                effacerEchiquier();
                echiquier[dernierI[cptTour], dernierJ[cptTour]].BackgroundImage = cavalier;
                echiquier[dernierI[cptTour], dernierJ[cptTour]].Enabled = true;
                afficherFuite(dernierI[cptTour], dernierJ[cptTour]); 
            }
            else
            {
                label1.Text = "Plus de retours en arrière possibles"; 
                button4.Enabled = false;
            }
        }

        /*
         * Bouton (click) qui permet les fonctions de jeu
         * S'assure que la case est jouable par l'utilisateur         
         */
        private void Mon_Bouton_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            button1.Enabled = false;
            button2.Enabled = true;
            saisieI = trouverI(sender, echiquier);
            saisieJ = trouverJ(sender, echiquier);
            if (!impasse(saisieI, saisieJ) && !echiquierParcouru())
            {
                if (cptTour == 0 || echiquier[saisieI, saisieJ].Text == "X")
                {
                    jouerModeJoueur(saisieI, saisieJ);
                }
                else
                    label1.Text = "Le cavalier ne peut pas se déplacer sur cette case ! ";
            }
            else if (echiquierParcouru())
            {
                label1.Text = "Vous avez gagné cavalier !";
            }
            else
            {
                effacerEchiquier();
                echiquier[saisieI, saisieJ].BackgroundImage = cavalier;             
                label1.Text = "Cavalier dans une impasse !";
                if (cptRetour == 0) 
                    desactiverEchiquier();
            }

            if (cptTour == 1)
            {
                this.gardeI = saisieI;
                this.gardeJ = saisieJ;
            }
        }

        /** Fonction de jeu
         * Passe l'image de la case sur cavalier et empêche de la jouer a nouveau
         * Enregistre la case jouée dans les tableaux dernierI / dernierJ pour le retour en arrière
         */
        public void jouerModeJoueur(int ip, int jp)
        {
            effacerEchiquier();
            button4.Enabled = true;
            echiquier[ip,jp].BackgroundImage = cavalier;
            echiquier[ip,jp].Enabled = false;
            afficherFuite(ip, jp);
            ++cptTour;
            dernierI[cptTour] = saisieI;
            dernierJ[cptTour] = saisieJ;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string about = "Jeu du cavalier développé par Azzedine et Yan dans le cadre de notre DUT informatique AS à l'Université Paris-Descartes";
            MessageBox.Show(about);
        }

        private void règlesDuJeuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regles = "Le but de cette application graphique WinForms (C#), est de faire parcourir à un cavalier l'ensemble d'un échiquier sans passer deux fois sur la même case. On rappelle la technique de déplacement d'un cavalier sur un échiquier : à partir d'une case, le cavalier ne peut se déplacer que sur l'une des cases avec un X";
            MessageBox.Show(regles);
        }

        private void noirBlancToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 2; i < 10; i++)
            {
                for (int j = 2; j < 10; j++)
                {
                    if ((j % 2 == 0 && (i + 1) % 2 == 0) || (j % 2 != 0 && i % 2 == 0))
                    {
                        echiquier[i, j].BackColor = Color.White;
                    }
                    else echiquier[i, j].BackColor = Color.FromArgb(133, 144, 161);
                }
            }
        }

        private void jauneEtChocolatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 2; i < 10; i++)
            {
                for (int j = 2; j < 10; j++)
                {
                    if ((j % 2 == 0 && (i + 1) % 2 == 0) || (j % 2 != 0 && i % 2 == 0))
                    {
                        echiquier[i, j].BackColor = Color.FromArgb(255, 222, 51);
                    }
                    else echiquier[i, j].BackColor = Color.FromArgb(124, 43, 4);
                }
            }
        }

        private void bleuEtRougeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 2; i < 10; i++)
            {
                for (int j = 2; j < 10; j++)
                {
                    if ((j % 2 == 0 && (i + 1) % 2 == 0) || (j % 2 != 0 && i % 2 == 0))
                    {
                        echiquier[i, j].BackColor = Color.FromArgb(51, 180, 255);
                    }
                    else echiquier[i, j].BackColor = Color.FromArgb(255, 105, 51);
                }
            }
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = button2.Enabled = button3.Enabled = true;
            effacerEchiquier();
            this.cptTour = 0;
            this.cptRetour = 5;
            this.label1.Text = "Choisissez une case ou cliquez pour en générer une aléatoirement";
            this.button2.Enabled = false;
            this.button4.Enabled = false;
            //initialisation des cases d'échecs
            for (i = 0; i < 12; i++)
                for (j = 0; j < 12; j++)
                    echec[i, j] = ((i < 2 | i > 9 | j < 2 | j > 9) ? -1 : 0);

            // initialisation des boutton de l'échiquier
            for (int l = 2; l < 10; l++)
            {
                for (int c = 2; c < 10; c++)
                {
                    echiquier[l, c].Enabled = true;
                }
            }
        }

        /*
         * Vérifie si le cavalier est dans une impasse 
         */
        public bool impasse(int x, int y)
        {
            int n, l;

            for (l = 0, n = 8; l < 8; l++)
                if (!echiquier[x + depi[l], y + depj[l]].Enabled)
                    --n;
            return n == 0;
        }


        /*
         * affiche les fuites possible pour notre brave cavalier
         */
        public void afficherFuite(int x, int y)
        {
            for (l = 0; l < 8; l++)
            {      
                ii = x + depi[l]; jj = y + depj[l];
                if (echiquier[ii, jj].Enabled)
                    echiquier[ii, jj].Text = "X";
            }
        }

        /*
         * Désactive toutes les cases de l'échiquier 
         */
        public void desactiverEchiquier()
        {
            for (int i = 2; i < 10; i++)

            {
                for (int j = 2; j < 10; j++)
                {
                    echiquier[i, j].Enabled = false;
                }

            }
        }

        public void activerEchiquier()
        {
            for (int i = 2; i < 10; i++)

            {
                for (int j = 2; j < 10; j++)
                {
                    echiquier[i, j].Enabled = true;
                }

            }
        }

        /*
         * L'échiquier est-il totalement parcouru? Si oui on l'utilise pour annoncer la victoire au joueur
         */
        public Boolean echiquierParcouru()
        {
            int cpt = 0;
            for (int i = 2; i < 10; i++)

            {
                for (int j = 2; j < 10; j++)
                {
                    if (!echiquier[i, j].Enabled)
                        ++cpt;
                }

            }
            return cpt == 63;
        }

        /* Lance le Jeu en mode ordinateur en partant de la première case jouée */
        public async void jouer(int ip, int jp, int duree, int pas)
        {
            for (i = 0; i < 12; i++)
                for (j = 0; j < 12; j++)
                    echec[i, j] = ((i < 2 | i > 9 | j < 2 | j > 9) ? -1 : 0);

            echec[ip, jp] = 1;
            echiquier[ip, jp].BackgroundImage = cavalier;
            await Task.Delay(duree);

            for (k = 2; k <= 64; k++)
            {
                // met en pause l'application
                while (pause)
                {
                    Application.DoEvents();
                }

                echiquier[ip, jp].BackgroundImage = null;
                if (k == 2 || k % pas == 1 || k % pas == 0)
                    echiquier[ip, jp].Text = "" + (k - 1);
                for (l = 0, min_fuite = 11; l < 8; l++)
                {
                    ii = ip + depi[l]; jj = jp + depj[l];

                    nb_fuite = ((echec[ii, jj] != 0) ? 10 : fuite(ii, jj));

                    if (nb_fuite < min_fuite)
                    {
                        min_fuite = nb_fuite; lmin_fuite = l;
                    }
                }
                if (min_fuite == 9 & k != 64)
                {
                    break;
                }
                ip += depi[lmin_fuite]; jp += depj[lmin_fuite];
                echec[ip, jp] = k;
                if (k % pas == 0 || k == 64)
                {
                    echiquier[ip, jp].BackgroundImage = cavalier;
                    await Task.Delay(duree);
                }
            }
        }

        /* Efface toutes les cases de l'échiquier */
        public void effacerEchiquier()
        {
            for (int i = 2; i < 10; i++)
            {
                for (int j = 2; j < 10; j++)
                {
                    echiquier[i, j].Text = "";
                    echiquier[i, j].BackgroundImage = null;
                }
            }
        }

        /* Trouve la valeur de i dans un tableau 2d */
        static int trouverI(object o, Button[,] b)
        {
            for (int i = 2; i < 10; i++)
            {
                for (int j = 2; j < 10; j++)
                {
                    if (o == b[i, j])
                        return i;
                }
            }
            return 0;
        }

        /* Trouve la valeur de j dans un tableau 2d */
        static int trouverJ(object o, Button[,] b)
        {
            for (int i = 2; i < 10; i++)
            {
                for (int j = 2; j < 10; j++)
                {
                    if (o == b[i, j])
                        return j;
                }
            }
            return 0;
        }

        /** Fonction de recherche de fuite 
         */
        static int fuite(int i, int j)
        {
            int n, l;

            for (l = 0, n = 8; l < 8; l++)
                if (echec[i + depi[l], j + depj[l]] != 0) n--;

            return (n == 0) ? 9 : n;
        }
    }
}


