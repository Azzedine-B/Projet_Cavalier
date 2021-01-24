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
        /* Echiquier graphique */
        Button[,] echiquier;
        Image cavalier;
       
        private void Form2_Load(object sender, EventArgs e)
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
                    //b.Click += new System.EventHandler(this.Mon_Bouton_Click);
                    if (c < 2 | c > 9 || l < 2 | l > 9)
                        b.Visible = false;
                    this.echiquier[l, c] = b;
                    this.Controls.Add(b); // ??
                }
            }
        }

        int i, j, k, l, ii, jj;

       
        public Form2()
        {
            InitializeComponent();
        }
    }
}
