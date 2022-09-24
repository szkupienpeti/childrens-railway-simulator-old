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
    public partial class SHBlokkbillentyu : UserControl
    {
        public SHBlokkbillentyu()
        {
            InitializeComponent();
            Lehuzva = false;
        }

        public SHBlokkbillentyu(SHBlokk blokk) : this()
        {
            this.Blokk = blokk;
        }

        public SHBlokk Blokk { get; set; }
        public SH SH { get; set; }

        bool lehuzva;
        public bool Lehuzva
        {
            get { return lehuzva; }
            set
            {
                lehuzva = value;
                if (lehuzva)
                {
                    BackgroundImage = Gyermekvasút.Properties.Resources.shBlokkBillentyuLehuzva;
                }
                else
                {
                    BackgroundImage = Gyermekvasút.Properties.Resources.shBlokkBillentyu;
                }
            }
        }

        private void SHBlokkbillentyu_Click(object sender, EventArgs e)
        {
            if (SH != null)
            {
                SH.BlokkbillentyuLehuzValtozas(this);
            }
        }
    }
}
