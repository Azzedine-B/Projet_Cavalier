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

        /* Initialisation */
        Form3 azzedine = new Form3();

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
                    this.Controls.Add(b); // ??
                }
            }
        }

        public Form2()
        {
            InitializeComponent();
        }

        /* Bouton qui permet de sélectionner une case de départ pour l'utilisateur
         * Place le cavalier et débute la partie à cet endroit
         */
        private void button1_Click(object sender, EventArgs e)
        {
            effacerEchiquier();
            Random random = new Random();
            int iR = random.Next(1, 8) + 1;
            int jR = random.Next(1, 8) + 1;
            // iR et jR evoluent de 2 à 9 !
            gardeI = iR;
            gardeJ = jR;
            jouerModeJoueur(iR, jR);
            button1.Enabled = false;
        }

        /** Joue la simulation précédente 
         * Efface l'échiquier
         * Joue un jeu avec les données de la simulation précédente stockées dans gardeI, gardeJ
         */
        private void button2_Click(object sender, EventArgs e)
        {
            effacerEchiquier();
            // on récupère les valeur de la simulation précédente
            jouer(gardeI, gardeJ);
        }

        /* Bouton qui permet de revenir en arrière
         * Décrémente le compteur de retour et le cptTour (pour savoir à quel indice du tableau dernierI/dernierJ on se trouve
         * Désactive le bouton lorsque le joueur a utilisé toutes ses tentatives de retour
         */
        private void button4_Click(object sender, EventArgs e)
        {
            
            button4.Text = "Retour en arrière" + "(" + cptRetour + ")";
            if (cptRetour > 0)
            {
                cptTour--;
                cptRetour--;
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
        private void button3_Click(object sender, EventArgs e)
        {
            azzedine.Show();
        }

        /*
         * Bouton qui permet les fonctions de jeu
         * S'assure que la case est jouable par l'utilisateur         * 
         */

        private void Mon_Bouton_Click(object sender, EventArgs e)
        {
            // recodage de la méthode clique pour qu'elle réagisse a tout type d'erreur
            label1.Text = "";
            saisieI = trouverI(sender, echiquier);
            saisieJ = trouverJ(sender, echiquier);
            if (!impasse(saisieI, saisieJ) && !echiquierParcouru())
            {
                if (cptTour == 0 || echiquier[saisieI, saisieJ].Text == "X")
                {
                    jouerModeJoueur(saisieI, saisieJ);
                }
                else
                    label1.Text = "Le cavalier ne peux pas se déplacer sur cette case ! ";
            }
            else if (echiquierParcouru())
            {
                label1.Text = "Vous avez gagné cavalier ! ";
            }
            else
            {
                effacerEchiquier();
                echiquier[saisieI, saisieJ].BackgroundImage = cavalier;
                desactiverEchiquier();
                label1.Text = "Cavalier dans une impasse !";
            }
        }

        /*
        public void jouerModeJoueur(int ip, int jp)
        {
            cptTour++;
            echiquier[ip, jp].BackgroundImage = cavalier;
            echiquier[ip, jp].Enabled = false;
            dernierI[cptTour] = ip;
            dernierJ[cptTour] = jp;
            afficherAide(ip, jp);
        }*/




        /** Fonction de jeu
         * Passe l'image de la case sur cavalier et empêche de la jouer a nouveau
         * Enregistre la case jouée dans les tableaux dernierI / dernierJ pour le retour en arrière
         */
        public void jouerModeJoueur(int ip, int jp)
        {
            effacerEchiquier();
            echiquier[saisieI, saisieJ].BackgroundImage = cavalier;
            echiquier[saisieI, saisieJ].Enabled = false;
            afficherFuite(saisieI, saisieJ);
            ++cptTour;
            dernierI[cptTour] = saisieI;
            dernierJ[cptTour] = saisieJ;
            //label1.Text = "" + impasse(saisieI, saisieJ);
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

        /* il s'agit de ta méthode que j'ai mit en commentaire
        /* Booléen utilisé pour s'assurer que l'utilisateur choisit une case où il a le droit de jouer */
        /*
        public Boolean coupPossible(int x, int y)
        {
            if (cptTour == 0)
                return true;
            else if (cptTour >= 1 && echiquier[saisieI, saisieJ].Text == "X")
                return true;
            else return false;

        }
        */

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

        /*
         * L'échiquier est-il totalement parcouru? si oui on l'utilise pour annoncer la victoire au joueur
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
        public async void jouer(int ip, int jp)
        {
            echec[ip, jp] = 1;
            echiquier[ip, jp].BackgroundImage = cavalier;


            for (k = 2; k <= 64; k++)
            {
                //met en pause l'application
                while (pause)
                {
                    Application.DoEvents();
                }

                echiquier[ip, jp].BackgroundImage = null;
                if (k == 2 || k % 1 == 1 || k % 1 == 0)
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
                    label1.Text = "Impasse !!";
                    break;
                }
                ip += depi[lmin_fuite]; jp += depj[lmin_fuite];
                echec[ip, jp] = k;
                if (k % 1 == 0 || k == 64)
                {
                    echiquier[ip, jp].BackgroundImage = cavalier;
                    await Task.Delay(0);
                }
            }
        }

        /*
         * Cette méthode est la même que afficher fuite, du coup je l'ai mis en commentaire aussi
        /* affiche les "X" sur les cases jouables  */
        /*
        private void afficherAide(int x, int y)
        {
            for (l = 0; l < 8; l++)
            {
                ii = x + depi[l]; jj = y + depj[l];
                echiquier[ii, jj].Text = "X";
            }
        }
        */

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


