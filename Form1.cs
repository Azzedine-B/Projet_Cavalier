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
        int gardeI = 0, gardeJ = 0;

        /* Echiqiuer console */
        static int[,] echec = new int[12, 12];
        static int[] depi = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
        static int[] depj = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
        int nb_fuite, min_fuite, lmin_fuite = 0;
        int i, j, k, l, ii, jj;

        // mettre en pause le jeu a n'importe quel moment
        private void button4_Click(object sender, EventArgs e)
        {
            pause = !pause;
        }

        // joue la simulation précédente
        private void button1_Click(object sender, EventArgs e)
        {
            effacerEchiquier();
            // on récupère les valeur de la simulation précédente
            jouer(gardeI, gardeJ, 1000, 1);

        }

        //mode aléatoire
        private void button5_Click(object sender, EventArgs e)
        {

            effacerEchiquier();
            
            Random random = new Random();
            int iR = random.Next(1, 8) + 1;
            int jR = random.Next(1, 8) + 1;
            // iR et jR evoluent de 2 à 9 !
           
            gardeI = iR;
            gardeJ = jR;

            jouer(iR,jR, 1000, 1);
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
            //a changer de place mais me permet de charger l'autre fenêtre
            Form2 ModeJeu = new Form2();
            ModeJeu.Show();
            Form3 azzedine = new Form3();
            azzedine.Show();
        }

        // choix de la case
        private void Mon_Bouton_Click(object sender, EventArgs e)
        {  

            effacerEchiquier();

            // stocke les valeurs pour rejouer la simulation 
            gardeI = trouverI(sender, echiquier);
            gardeJ = trouverJ(sender, echiquier);

            jouer(trouverI(sender, echiquier), trouverJ(sender, echiquier), 1000, 1);
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
                if(k == 2 || k % pas == 1 || k % pas == 0)
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
    }
}
