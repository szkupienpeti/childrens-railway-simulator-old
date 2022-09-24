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
    public partial class SHKenyszeroldo : UserControl
    {
        static public Index Ind { get; set; }

        public SHKenyszeroldo()
        {
            InitializeComponent();
        }

        private void SHKenyszeroldo_Click(object sender, EventArgs e)
        {
            if (Ind != null)
            {
                Ind.Hibak[10]++;
            }
            SzamlaloFigy szf = new SzamlaloFigy("a");
            szf.ShowDialog();
        }
    }
}
