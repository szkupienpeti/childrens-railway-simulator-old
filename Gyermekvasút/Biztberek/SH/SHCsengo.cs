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
    public partial class SHCsengo : UserControl
    {
        public SHCsengo()
        {
            InitializeComponent();
        }

        public SH SH { get; set; }

        private void SHCsengo_Click(object sender, EventArgs e)
        {
            if (SH != null)
            {
                SH.CsengoLenyomas();
            }
        }
    }
}
