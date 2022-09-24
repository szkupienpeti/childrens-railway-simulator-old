using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút.Biztberek
{
    public class VaganyutiKapcsoloGomb : Kapcsologomb
    {
        public VaganyutiKapcsoloGomb()
        {
            Img = Gyermekvasút.Properties.Resources.vgutiKapcsGomb;
            Image = Img;
            Size = new Size(66, 55);
        }

        bool vaganyutZaroMagnes;
        public bool VaganyutZaroMagnes
        {
            get { return vaganyutZaroMagnes; }
            set
            {
                vaganyutZaroMagnes = value;
                if(VES != null)
                    VES.VaganyutZaroMagnes_Changed(this);
            }
        }

        public override double Alpha
        {
            get
            {
                return base.Alpha;
            }
            set
            {
                base.Alpha = value;

                #region CSÖRGÉS
                if (Alpha != 0 && VES != null)
                {
                    VES.VgutiCsengetesPlay();
                }
                else if (VES != null)
                {//mindkét vgútiKG alapban van (0 fok)
                    VES.VgutiCsengetesStop();
                }
                #endregion

                if (Alpha == -45) //ELFORDÍTVA, tiltja a ValtoKG-t
                {
                    if (this == VES.vgutKG_A)
                    {//A (2-es váltó), -45 fok >> I. vágány (2E)
                        
                    }
                    else
                    {//L (1-es váltó), -45 fok >> II. vágány (1K)

                    }
                }
                else if (Alpha == -45) //ELFORDÍTVA, tiltja a ValtoKG-t
                {
                    if (this == VES.vgutKG_A)
                    {//A (2-es váltó), 45 fok >> II. vágány (2K)
                        
                    }
                    else
                    {//L (1-es váltó), 45 fok >> I. vágány (1E)

                    }
                }
                else if (Alpha == 0) //ALAPBAN, engedélyezi a ValtoKG-t
                {

                }
                else
                {

                }
                try
                {
                    if (Alpha < -5)
                    {
                        if (this == VES.vgutKG_A)
                        {//1
                            Vg1.BackgroundImage = Gyermekvasút.Properties.Resources.vgutiAblak1;
                            Vg2.BackgroundImage = null;
                        }
                        else
                        {//2
                            Vg2.BackgroundImage = Gyermekvasút.Properties.Resources.vgutiAblak2;
                            Vg1.BackgroundImage = null;
                        }
                    }
                    else if (Alpha > 5)
                    {
                        if (this == VES.vgutKG_A)
                        {//2
                            Vg2.BackgroundImage = Gyermekvasút.Properties.Resources.vgutiAblak2;
                            Vg1.BackgroundImage = null;
                        }
                        else
                        {//1
                            Vg1.BackgroundImage = Gyermekvasút.Properties.Resources.vgutiAblak1;
                            Vg2.BackgroundImage = null;
                        }
                    }
                    else
                    {
                        Vg1.BackgroundImage = null;
                        Vg2.BackgroundImage = null;
                    }
                }
                catch (NullReferenceException)
                { }
            }
        }

        public PictureBox Vg1 { get; set; }
        public PictureBox Vg2 { get; set; }
    }
}
