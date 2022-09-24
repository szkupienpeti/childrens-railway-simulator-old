using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace Gyermekvasút.Biztberek
{
    public partial class VES : UserControl
    {
        public VES()
        {
            BackgroundImage = Gyermekvasút.Properties.Resources.VES;
            InitializeComponent();
        }

        public bool IsPlaying { get; set; }

        SoundPlayer valtoPlayer = new SoundPlayer("s_zav.gyv");
        public SoundPlayer vgutiPlayer = new SoundPlayer("s_zav.gyv");
        public bool VgutiPlayerIsPlaying { get; set; }
        public void ValtoCsengetesPlay()
        {
            valtoPlayer.PlayLooping();
            IsPlaying = true;
        }
        public void ValtoCsengetesStop()
        {
            valtoPlayer.Stop();
            IsPlaying = false;
        }

        public void VgutiCsengetesPlay()
        {//TODO ebben még nicns benne a vonat behaladása utáni csengetés!
            if (!VgutiPlayerIsPlaying)
            {
                vgutiPlayer.PlayLooping();
                VgutiPlayerIsPlaying = true;
            }
        }
        //TODO: a váltócsengőt, a vguticsengőt és a vonat behaladása utáni csengőt egy soundplayerre kell rakni, mert a hangok így felülírják egymást!!!
        public void VgutiCsengetesStop()
        {
            if ((vgutKG_A.Alpha == 0 || JelzoKG_megad(vgutKG_A).Kezelve45) && (vgutKG_L.Alpha == 0 || JelzoKG_megad(vgutKG_L).Kezelve45))
            {
                VgutiPlayerIsPlaying = false;
                vgutiPlayer.Stop();
            }
        }

        public static Color Zold = Color.FromArgb(9, 132, 9);
        public static Color Voros = Color.FromArgb(237, 28, 36);
        public static Color Feher = Color.FromArgb(255, 255, 255);
        public static Color Fekete = Color.FromArgb(0, 0, 0);


        public JelzoKapcsologomb jelzoKG_A { get; set; }
        public JelzoKapcsologomb jelzoKG_L { get; set; }

        public VaganyutiKapcsoloGomb vgutKG_A { get; set; }
        public VaganyutiKapcsoloGomb vgutKG_L { get; set; }

        public ValtoKapcsologomb valtoKG_1 { get; set; }
        public ValtoKapcsologomb valtoKG_2 { get; set; }

        public VaganyutiKapcsoloGomb VgutiKG_megad(JelzoKapcsologomb jelzoKG)
        {
            if (jelzoKG == jelzoKG_A)
            {
                return vgutKG_A;
            }
            else if (jelzoKG == jelzoKG_L)
            {
                return vgutKG_L;
            }
            else
            {
                return null;
            }
        }
        public VaganyutiKapcsoloGomb vgutiKG_megad(ValtoKapcsologomb valtoKG)
        {
            if (valtoKG == valtoKG_1)
            {
                return vgutKG_L;
            }
            else if (valtoKG == valtoKG_2)
            {
                return vgutKG_A;
            }
            else
            {
                return null;
            }
        }

        public VaganyutiKapcsoloGomb MasikVgutiKG_megad(VaganyutiKapcsoloGomb vgutiKG)
        {
            if (vgutiKG == vgutKG_A)
            {
                return vgutKG_L;
            }
            else if (vgutiKG == vgutKG_L)
            {
                return vgutKG_A;
            }
            else
            {
                return null;
            }
        }
        public ValtoKapcsologomb ValtoKG_megad(VaganyutiKapcsoloGomb vgutiKG)
        {
            if (vgutiKG == vgutKG_A)
            {
                return valtoKG_2;
            }
            else if (vgutiKG == vgutKG_L)
            {
                return valtoKG_1;                
            }
            else
            {
                return null;
            }
        }
        public ValtoKapcsologomb MasikValtoKG_megad (ValtoKapcsologomb valtoKG)
        {
            if (valtoKG == valtoKG_1)
            {
                return valtoKG_2;
            }
            else if(valtoKG == valtoKG_2)
            {
                return valtoKG_1;
            }
            else
            {
                return null;
            }
        }
        public JelzoKapcsologomb JelzoKG_megad (VaganyutiKapcsoloGomb vgutiKG)
        {
            if (vgutiKG == vgutKG_A)
            {
                return jelzoKG_A;
            }
            else if (vgutiKG == vgutKG_L)
	        {
                return jelzoKG_L;
	        }
            else
	        {
                return null;
	        }
        }

        public void JelzogombotZaroMagnes_Changed(JelzoKapcsologomb jelzoKG)
        {
            if (jelzoKG == jelzoKG_A)
            {
                if (jelzoKG.JelzogombotZaroMagnes)
                {
                    
                }
                else
                {

                }
            }
            else if (jelzoKG == jelzoKG_L)
            {
                if (jelzoKG.JelzogombotZaroMagnes)
                {

                }
                else
                {

                }
            }
        }
        public void VaganyutZaroMagnes_Changed(VaganyutiKapcsoloGomb vgutKG)
        {
            if (vgutKG == vgutKG_A)
            {
                if (vgutKG.VaganyutZaroMagnes)
                {
                    
                }
                else
                {

                }
            }
            else if (vgutKG == vgutKG_L)
            {
                if (vgutKG.VaganyutZaroMagnes)
                {

                }
                else
                {

                }
            }
        }
    }
}
