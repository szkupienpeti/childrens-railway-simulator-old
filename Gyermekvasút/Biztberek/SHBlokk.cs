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
    public partial class SHBlokk : UserControl
    {
        public SHBlokk()
        {
            InitializeComponent();
        }

        public SHBlokk(SHBlokkTipus tipus)
        {
            InitializeComponent();
            Tipus = tipus;
            switch (Tipus)
            {
                case SHBlokkTipus.BejaratiJelzo:
                    Allas = SHBlokkAllas.Oldott;
                    break;
                case SHBlokkTipus.Vaganyuti:
                    Allas = SHBlokkAllas.Lezart;
                    break;
                case SHBlokkTipus.KijaratiJelzo:
                    Allas = SHBlokkAllas.Oldott;
                    break;
                default:
                    break;
            }
            //Allas setter >> backgroundImg set
        }

        public SHBlokkTipus Tipus { get; set; }
        private SHBlokkAllas allas;
        public SHBlokkAllas Allas
        {
            get { return allas; }
            set //set bckgrndImg
            {
                allas = value;
                if ((allas == SHBlokkAllas.Oldott) != (Tipus == SHBlokkTipus.Vaganyuti))
                {
                    BackgroundImage = Gyermekvasút.Properties.Resources.shBlokkFeher;
                    return;
                }
                else
                {
                    if (Tipus == SHBlokkTipus.Vaganyuti)
                    {
                        BackgroundImage = Gyermekvasút.Properties.Resources.shBlokkZold;
                    }
                    else
                    {
                        BackgroundImage = Gyermekvasút.Properties.Resources.shBlokkVoros;
                    }
                }
            }
        }
    }

    public enum SHBlokkTipus
    {
        BejaratiJelzo, Vaganyuti, KijaratiJelzo
    }

    public enum SHBlokkAllas
    {
        Oldott, Lezart
    }
}
