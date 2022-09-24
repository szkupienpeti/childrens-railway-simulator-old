using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút.Biztberek
{
    public class JelzoKapcsologomb : Kapcsologomb
    {
        public JelzoKapcsologomb()
        {
            Img = Gyermekvasút.Properties.Resources.jelzoKapcsGomb;
            Image = Img;
            Size = new Size(66, 55);
        }

        public bool JobbraAllitas
        {
            get
            {
                if (Alpha > ElozoAlpha)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        bool jelzogombotZaroMagnes;
        public bool JelzogombotZaroMagnes
        {
            get { return jelzogombotZaroMagnes; }
            set
            {
                jelzogombotZaroMagnes = value;
                if (VES != null)
                    VES.JelzogombotZaroMagnes_Changed(this);
            }
        }

        public bool Kezelve12 { get; set; }
        public bool Kezelve45 { get; set; }

        public override double Alpha
        {
            get
            {
                return base.Alpha;
            }
            set
            {
                if (base.Alpha != value)
                {
                    ElozoAlpha = Alpha;
                    base.Alpha = value;
                    if (JobbraAllitas)
                    {//KI vagy BE Jelző Megállj!
                        if (Alpha == 90)
                        {//SZABAD JELZÉS
                            Kezelve12 = false;
                            Kezelve45 = false;

                            MessageBox.Show("Szabad jelzés!");
                            MinimumAllas = 90;
                            CelAllas = 45;
                        }
                        else if (Alpha >= 45 && !Kezelve45)
                        {
                            Kezelve45 = true;
                            MinimumAllas = 45;

                            BalfelsoZold.BackColor = VES.Feher;
                            VES.VgutiKG_megad(this).VaganyutZaroMagnes = false;
                        }
                        else if (Alpha >= 12 && !Kezelve12)
                        {//FELTÉTELEK VIZSGÁLATA -- KI
                            Kezelve12 = true;
                            if (true) //TODo feltételek
                            {
                                JelzogombotZaroMagnes = true;
                                JobbfelsoVoros.BackColor = VES.Feher;
                                CelAllas = 90;
                            }
                            else
                            {
                                CelAllas = 30;
                            }
                        }
                        else if (Alpha == -45)
                        {
                            MinimumAllas = -45;
                            CelAllas = -45;
                            MessageBox.Show("Jelző megállj!");
                        }
                        else if (Alpha >= -12 && Alpha <= 0 && Kezelve12)
                        {//már túlvittem -12 fokon, de visszaviszem 0 felé
                            Kezelve12 = false;
                            JelzogombotZaroMagnes = false;
                            JobbfelsoVoros.BackColor = VES.Voros;
                            CelAllas = 30;
                            MinimumAllas = -30;
                        }
                    }
                    else
                    {//balra állítás - BE vagy KI Jelző Megállj!
                        if (Alpha == -90)
                        {//SZABAD JELZÉS
                            Kezelve12 = false;
                            Kezelve45 = false;

                            MessageBox.Show("Szabad jelzés!");
                            MinimumAllas = -90;
                            CelAllas = -45;
                        }
                        else if (Alpha <= -45 && !Kezelve45)
                        {
                            Kezelve45 = true;
                            MinimumAllas = -45;
                            
                            BalfelsoZold.BackColor = VES.Feher;
                            VES.VgutiKG_megad(this).VaganyutZaroMagnes = false;
                        }
                        else if (Alpha <= -12 && !Kezelve12)
                        {//FELTÉTELEK VIZSGÁLATA -- BE
                            Kezelve12 = true;
                            if (true) //TODO feltételek
                            {
                                JelzogombotZaroMagnes = true;
                                JobbfelsoVoros.BackColor = VES.Feher;
                                CelAllas = -90;
                            }
                            else
                            {
                                CelAllas = -30;
                            }
                        }
                        else if (Alpha == 45)
                        {
                            MinimumAllas = 45;
                            CelAllas = 45;
                            MessageBox.Show("Jelző megállj!");
                        }
                        else if (Alpha <= 12 && Alpha >= 0 && Kezelve12)
                        {//már túlvittem 12 fokon, de visszaviszem 0 felé
                            Kezelve12 = false;
                            JelzogombotZaroMagnes = false;
                            JobbfelsoVoros.BackColor = VES.Voros;
                            CelAllas = -30;
                            MinimumAllas = 30;
                        }
                    }
                }
            }
        }

        public PictureBox BalfelsoZold { get; set; }
        public PictureBox JobbfelsoVoros { get; set; }
        public PictureBox BalalsoBe { get; set; }
        public PictureBox JobbalsoKi { get; set; }  
    }
}
