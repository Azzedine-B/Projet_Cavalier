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
        bool pause = false;
        int gardeI = 0, gardeJ = 0;
        int cptTour;

        /* Initialisation */
        Form3 azzedine = new Form3();
        int[,] derniersCoups = new int[2, 5];



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

        /** Initialise au load
         * Initialise l'échiquier comme un tableau 2D de boutons 12 * 12
         * Fixe la taille des boutons 
         * Mon_Bouton_Click = jouer en choisissant la case de départ
         * Boucle pour rendre les boutons invisibles car hors tableau
         * + Q : this controls.add
         */
        private void Form2_Load(object sender, EventArgs e)
        {
            this.cptTour = 0;
            this.cavalier = Image.FromFile("img\\cavalier.jpg");
            this.echiquier = new Button[12, 12];
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
                        b.Visible = false;
                    this.echiquier[l, c] = b;
                    this.Controls.Add(b); // ??
                }
            }
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            azzedine.Show();
        }

        //mode aléatoire
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

        private void Mon_Bouton_Click(object sender, EventArgs e)
        {

            saisieI = trouverI(sender, echiquier);
            saisieJ = trouverJ(sender, echiquier);

            if (coupPossible(trouverI(sender, echiquier), trouverJ(sender, echiquier)))
                {
                effacerEchiquier();
                jouerModeJoueur(saisieI,saisieJ);
            }

            //gardeI = trouverI(sender, echiquier);
            //gardeJ = trouverJ(sender, echiquier);
        }

        /** Fonction de jeu
         * Passe l'image de la case sur cavalier et empêche de la jouer a nouveau
         */
        public async void jouerModeJoueur(int ip, int jp)
        {
            cptTour++;
            echiquier[ip, jp].BackgroundImage = cavalier;
            echiquier[ip, jp].Enabled = false;

            for (l = 0; l < 8; l++)
            {
                ii = ip + depi[l]; jj = jp + depj[l];
                echiquier[ii, jj].Text = "X";
            }
        }


        public Boolean coupPossible(int x, int y)
        {
            if (echec[x, y] != -1)
            {
                return true;
            }
            if (cptTour > 1 && echiquier[saisieI, saisieJ].Text == "X")
                return true;
            else return false;

        }
        public async void jouer(int ip, int jp)
        {
            echec[ip, jp] = 1;
            echiquier[ip, jp].BackgroundImage = cavalier;


            for (k = 2; k <= 64; k++)
            {
                // met en pause l'application
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

        //effacer toutes les cases de l'échiquier
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

        // trouve la valeur de i dans un tableau 2d
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

        // trouve la valeur de j dans un tableau 2d
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


