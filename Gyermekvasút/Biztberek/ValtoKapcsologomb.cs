using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút.Biztberek
{
    public class ValtoKapcsologomb : Kapcsologomb
    {
        /* A ValtoKapcsologomb működési elve
         * A mozgatás feltétele egyedül az, hogy a VaganyutiKapcsologomb ne fogja a váltót.
         * Ennek az ellenőrzése nem a ValtoKapcsologomb dolga, az Alpha setterében ez nem szükséges, mert
         * a  form _MouseUp-ja a ValtoKapcsologomb MinimumAllasát és CelAllasát szükséges
         * esetben egyenlővé teszi, vagyis így a ValtoKapcsologomb nem mozgatható!*/        

        public ValtoKapcsologomb()
        {
            Img = Gyermekvasút.Properties.Resources.valtoKapcsGomb;
            Size = new Size(44, 44);
        }

        public PictureBox Ablak { get; set; }
        
        public Valto Valto { get; set; }
        public override double Alpha
        {
            get
            {
                return base.Alpha;
            }
            set
            {
                base.Alpha = value;
                if (Alpha == 0)
                {//E
                    if (VES != null && !VES.MasikValtoKG_megad(this).Valto.AllitasAlatt)
                        VES.ValtoCsengetesStop();
                    if (Ablak != null)
                        Ablak.Image = Gyermekvasút.Properties.Resources.VESvaltoFeher;
                    if (Valto != null)
                    {
                        Valto.Allas = true;
                        Valto.AllitasAlatt = false;
                    }
                    MinimumAllas = 0;
                    CelAllas = -90;
                }
                else if (Alpha == -90)
                {//K
                    if (VES != null && !VES.MasikValtoKG_megad(this).Valto.AllitasAlatt)
                        VES.ValtoCsengetesStop();
                    if (Ablak != null)
                        Ablak.Image = Gyermekvasút.Properties.Resources.VESvaltoFeher;
                    if (Valto != null)
                    {
                        Valto.Allas = false;
                        Valto.AllitasAlatt = false;
                    }
                    MinimumAllas = -90;
                    CelAllas = 0;
                }
                else
                {
                    if (Ablak != null)
                        Ablak.Image = Gyermekvasút.Properties.Resources.VESvaltoVoros;
                    if (Valto != null)
                    {
                        Valto.AllitasAlatt = true;
                        if (VES != null && !VES.IsPlaying)
                            VES.ValtoCsengetesPlay();
                        if (CelAllas > MinimumAllas)
                        {
                            Valto.Allas = false;
                        }
                        else
                        {
                            Valto.Allas = true;
                        }
                    }
                }
            }
        }

    }
}
