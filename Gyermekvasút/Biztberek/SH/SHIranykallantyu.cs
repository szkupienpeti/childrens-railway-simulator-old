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
    public partial class SHIranykallantyu : UserControl
    {
        public SHIranykallantyu()
        {
            InitializeComponent();
            Allas = SHIranykallantyuAllas.Alap; //sets backgroundImage
        }

        private SHIranykallantyuAllas allas;
        public SHIranykallantyuAllas Allas //sets bckgrndImg
        {
            get { return allas; }
            set
            {
                allas = value;
                switch (Allas)
                {
                    case SHIranykallantyuAllas.Alap:
                        BackgroundImage = Gyermekvasút.Properties.Resources.shIranykallantyuAlap;
                        break;
                    case SHIranykallantyuAllas.Bejarat:
                        BackgroundImage = Gyermekvasút.Properties.Resources.shIranykallantyuBejarat;
                        break;
                    case SHIranykallantyuAllas.Kijarat:
                        BackgroundImage = Gyermekvasút.Properties.Resources.shIranykallantyuKijarat;
                        break;
                    default:
                        break;
                }
            }
        }

        public SH SH { get; set; }

        private bool allitas = false;

        private void MouseAllitas()
        {
            if (allitas)
            {
                int x = this.PointToClient(Cursor.Position).X;
                if (x > this.Width / 2)
                {//jobbra
                    if (Allas == SHIranykallantyuAllas.Bejarat)
                    {
                        Allas = SHIranykallantyuAllas.Alap;
                    }
                    else
                    {
                        Allas = SHIranykallantyuAllas.Kijarat;
                    }
                }
                else if (x < this.Width / 2)
                {//balra
                    if (Allas == SHIranykallantyuAllas.Kijarat)
                    {
                        Allas = SHIranykallantyuAllas.Alap;
                    }
                    else
                    {
                        Allas = SHIranykallantyuAllas.Bejarat;
                    }
                }
                allitas = false;
            }
        }

        private void SHIranykallantyu_MouseDown(object sender, MouseEventArgs e)
        {
            if (SH != null && SH.Tologomb.Allas != SHTologombAllas.Alap && SH.VaganyutiBlokk != null && SH.VaganyutiBlokk.Allas == SHBlokkAllas.Oldott)
            {
                if (SH.VK1.Valto.AllitasAlatt)
                {
                    MessageBox.Show("A váltókezelő éppen beállítja az elrendelt, majd kicsengetett vágányutat.\nIlyenkor ne nyúlj az iránykallantyúhoz!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                allitas = true;
            }
        }

        private void SHIranykallantyu_MouseUp(object sender, MouseEventArgs e)
        {
            MouseAllitas();
        }

        private void SHIranykallantyu_MouseLeave(object sender, EventArgs e)
        {
            MouseAllitas();
        }
    }

    
    public enum SHIranykallantyuAllas
    {
        Alap, Bejarat, Kijarat
    }    
}
