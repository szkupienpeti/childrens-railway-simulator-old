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
    public partial class VESOlomzarasGomb : UserControl
    {
        public AllomasForm AllomasForm { get; set; }

        public VESOlomzarasGomb()
        {
            InitializeComponent();
        }

        private void VESOlomzarasGomb_Click(object sender, EventArgs e)
        {
            AllomasForm.Ind.Hibak[10]++;
            SzamlaloFigy szf = new SzamlaloFigy(true);
            szf.ShowDialog();
        }
    }
}
