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
    public partial class Form3 : Form
    {
        static int[,] echec = new int[12, 12];
        static int[] depi = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
        static int[] depj = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };

        /* Définitions et déclarations */
        int nb_fuite, min_fuite, lmin_fuite = 0;
        int i, j, k, l, ii, jj;
        Button[,] echiquier;
        Image cavalier;
        bool pause = false;
        int gardeI = 0, gardeJ = 0;
        int cptTour = 0;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
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

        private void Mon_Bouton_Click(object sender, EventArgs e)
        {
            int cordX = trouverI(sender, echiquier);
            int cordY = trouverJ(sender, echiquier);
            ++cptTour;
            if (cptTour == 1)
            {
                echiquier[cordX, cordY].BackgroundImage = cavalier;
                afficherCoupsPossible(cordX, cordY);

            }

            if(cptTour > 1)
            {
                if (echiquier[cordX, cordY].Text == "X")
                {
                    effacerCoupsPossible(gardeI, gardeJ);
                    echiquier[gardeI, gardeJ].BackgroundImage = null;
                    echiquier[cordX, cordY].BackgroundImage = cavalier;
                    afficherCoupsPossible(cordX, cordY);
                    gardeI = cordX;
                    gardeJ = cordY;
                }

                if(cptTour == 1)
                {
                    gardeI = cordX;
                    gardeJ = cordY;
                }
                   

            }

           

        }


        public void afficherCoupsPossible(int x, int y)
        {
            for (l = 0, min_fuite = 11; l < 8; l++)
            {
                ii = x + depi[l]; jj = y + depj[l];
                if (echec[ii,jj] != -1)
                    echiquier[ii, jj].Text = "X";

            }
        }

        public void effacerCoupsPossible(int x, int y)
        {
            for (l = 0, min_fuite = 11; l < 8; l++)
            {
                ii = x + depi[l]; jj = y + depj[l];
                if (echec[ii, jj] != -1)
                    if (echiquier[ii, jj].Text == "X")
                        echiquier[ii, jj].Text = "";

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
    }
}
