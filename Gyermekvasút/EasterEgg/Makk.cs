using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút.EasterEgg
{
    public partial class Makk : UserControl
    {
        public bool Poisoned { get; set; }

        public Makk()
        {
            InitializeComponent();
        }

        public Makk(bool poison)
        {
            InitializeComponent();
            Poisoned = poison;
            if (Poisoned)
            {
                BackgroundImage = Gyermekvasút.Properties.Resources.P_MAKK;
            }
            else
            {
                BackgroundImage = Gyermekvasút.Properties.Resources.MAKK;
            }
        }
    }
}
