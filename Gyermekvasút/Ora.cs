using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút
{
    public partial class Ora : Form
    {
        public Ora()
        {
            InitializeComponent();
        }

        public void RefreshTime()
        {
            label1.Text = Program.Ido.ToShortTimeString();
            label1.Location = new Point(this.Width / 2 - label1.Width / 2, label1.Location.Y);
        }
    }
}
