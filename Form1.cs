using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace Projet_Cavalier
{
    public partial class Form1 : Form
    {
        /* Echiquier graphique */
        Button[,] echiquier;
        Image cavalier;
        bool pause = false;

        /* Echiqiuer console */
        static int[,] echec = new int[12, 12];
        static int[] depi = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
        static int[] depj = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
        int nb_fuite, min_fuite, lmin_fuite = 0;
        int i, j, k, l, ii, jj;

        private void button4_Click(object sender, EventArgs e)
        {
            pause = !pause;
        }

        private async void button1_Click(object sender, EventArgs e)
        { // joue la simulation précédente

            effacerEchiquier();

            for (int i = 2; i < 10; i++)
            {
                for (int j = 2; j < 10; j++)
                {
                    echiquier[i, j].Visible = true;
                    await Task.Delay(1000);
                }
            } 

        }

        //mode aléatoire
        private async void button5_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            ii = random.Next(1, 8);
            jj = random.Next(1, 8);
            // ii et jj evoluent de 1 à 8 !
            

            for (i = 0; i < 12; i++)
                for (j = 0; j < 12; j++)
                    echec[i, j] = ((i < 2 | i > 9 | j < 2 | j > 9) ? -1 : 0);


           
            //echiquier[ii, jj].BackgroundImage = null;
            //echiquier[ii, jj].Text = "1";
            i = ii + 1; j = jj +1;
            echec[i, j] = 1;
            echiquier[i, j].BackgroundImage = cavalier;
            await Task.Delay(1000);


            jouer();


        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.cavalier = Image.FromFile("img\\cavalier.jpg");

            this.echiquier = new Button[12, 12];
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
                    if (c <2  | c > 9 || l < 2 | l > 9)
                        b.Visible = false;
                    this.echiquier[l, c] = b;
                    this.Controls.Add(b); // ??
                }
            }
        }

        // choix de la case
        private async void Mon_Bouton_Click(object sender, EventArgs e)
        {   // mettre async pour utiliser await 

            ii = trouverI(sender, echiquier);
            jj = trouverJ(sender, echiquier);
            // le cavalier s'affiche sur la case selectionnée
            echiquier[ii, jj].BackgroundImage = cavalier;
            await Task.Delay(1000);

            for (i = 0; i < 12; i++)
                for (j = 0; j < 12; j++)
                    echec[i, j] = ((i < 2 | i > 9 | j < 2 | j > 9) ? -1 : 0);

            i = ii; j = jj;
            echec[i, j] = 1;

            jouer();

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

        static int fuite(int i, int j)
        {
            int n, l;

            for (l = 0, n = 8; l < 8; l++)
                if (echec[i + depi[l], j + depj[l]] != 0) n--;

            return (n == 0) ? 9 : n;
        }

        public async void jouer()
        {


            for (k = 2; k <= 64; k++)
            {
                // met en pause l'application
                while (pause)
                {
                    Application.DoEvents();
                }

                echiquier[i, j].BackgroundImage = null;
                if(k == 2 || k % 5 == 1)
                    echiquier[i, j].Text = "" + (k - 1);
                for (l = 0, min_fuite = 11; l < 8; l++)
                {
                    ii = i + depi[l]; jj = j + depj[l];

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
                i += depi[lmin_fuite]; j += depj[lmin_fuite];
                echec[i, j] = k;
                if (k % 5 == 0 || k == 64)
                {
                    echiquier[i, j].BackgroundImage = cavalier;
                    await Task.Delay(1000);
                }
            }

            /*
            for (i = 2; i < 10; i++)
            {
                for (j = 2; j < 10; j++)
                {

                    echiquier[i, j].Visible = true;
                }

            }
            */

        }
    }
}
