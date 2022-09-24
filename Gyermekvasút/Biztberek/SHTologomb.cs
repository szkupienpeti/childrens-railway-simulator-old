using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút.Biztberek
{
    public partial class SHTologomb : UserControl
    {
        public SHTologomb()
        {
            InitializeComponent();
            Allas = SHTologombAllas.Alap;
        }

        SHTologombAllas allas;
        public SHTologombAllas Allas
        {
            get { return allas; }
            set
            {
                allas = value;
                switch (Allas)
                {
                    case SHTologombAllas.Alap:
                        BackgroundImage = Gyermekvasút.Properties.Resources.shTologombAlap;
                        break;
                    case SHTologombAllas.Egy:
                        BackgroundImage = Gyermekvasút.Properties.Resources.shTologombEgy;
                        break;
                    case SHTologombAllas.Ketto:
                        BackgroundImage = Gyermekvasút.Properties.Resources.shTologombKetto;
                        break;
                    default:
                        break;
                }
            }
        }

        public SH SH { get; set; }

        private void SHTologomb_Click(object sender, EventArgs e)
        {

        }

        private bool allitas = false;

        private void SHTologomb_MouseDown(object sender, MouseEventArgs e)
        {
            if (SH != null && SH.Iranykallantyu != null && SH.Iranykallantyu.Allas == SHIranykallantyuAllas.Alap) //iránykallanytú alapban
            {
                allitas = true;
            }
        }

        private void SHTologomb_MouseUp(object sender, MouseEventArgs e)
        {
            MouseAllitas();
        }

        private void MouseAllitas() // -- most a control közepétől nézi
        {
            if (allitas)
            {
                //kurzor pozíciója a tológomb közepénél lejjebb vagy feljebb
                int y = this.PointToClient(Cursor.Position).Y;
                if (y > this.Height / 2)
                {//le
                    if (Allas == SHTologombAllas.Egy)
                    {
                        Allas = SHTologombAllas.Alap;
                    }
                    else
                    {
                        Allas = SHTologombAllas.Ketto;
                    }
                }
                else if (y < this.Height / 2)
                {//fel
                    if (Allas == SHTologombAllas.Ketto)
                    {
                        Allas = SHTologombAllas.Alap;
                    }
                    else
                    {
                        Allas = SHTologombAllas.Egy;
                    }
                }
                allitas = false;
            }
        }

        private void SHTologomb_MouseLeave(object sender, EventArgs e)
        {
            MouseAllitas();
        }
    }

    public enum SHTologombAllas
    {
        Alap, Egy, Ketto
    }
}
