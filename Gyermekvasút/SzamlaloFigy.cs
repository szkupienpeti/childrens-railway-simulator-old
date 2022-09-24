using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút
{
    public partial class SzamlaloFigy : Form
    {
        bool u = false;

        public SzamlaloFigy(bool Csilleberc = false)
        {
            InitializeComponent();
            u = Csilleberc;
        }

        public SzamlaloFigy(string all)
        {
            InitializeComponent();
            if (all.ToLower() == "a")
            {
                a = true;
            }
        }

        bool a = false;

        private void SzamlaloFigy_Load(object sender, EventArgs e)
        {
            if (a)
            {
                pictureBox1.Image = null;
                pictureBox1.BackgroundImage = Gyermekvasút.Properties.Resources.shKoFigy;
                pictureBox1.Size = new Size(92, 76);
                pictureBox1.Location = new Point(133, pictureBox1.Location.Y - 11);
                label1.Text = "Ólomzárral lezárt nyomógombot\ngyermekvasutas szolgálatot ellátó\nszemély önállóan nem kezelhet!";
            }
            else if (u)
            {
                pictureBox1.Image = null;
                pictureBox1.BackgroundImage = Gyermekvasút.Properties.Resources.OlomzarFigy;
                pictureBox1.Size = new Size(70, 71);
                pictureBox1.Location = new Point(144, pictureBox1.Location.Y - 11);
                label1.Text = "Ólomzárral lezárt nyomógombot\ngyermekvasutas szolgálatot ellátó\nszemély önállóan nem kezelhet!";
            }
            else
            {
                label1.Text = "Számlálóval ellátott nyomógombot\ngyermekvasutas szolgálatot ellátó\nszemély önállóan nem kezelhet!";
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
