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
    public partial class SHInduktorPanel : UserControl
    {
        public SHInduktorPanel()
        {
            InitializeComponent();
        }

        public SHInduktor Induktor { get; set; }
        static Pen p = new Pen(Color.FromArgb(26, 48, 24), 4);

        private void SHInduktorPanel_Paint(object sender, PaintEventArgs e)
        {
            if (Induktor != null)
            {
                if (Math.Sin(Induktor.Szog) < 0)
                {//lent
                    e.Graphics.DrawLine(p, 0, Induktor.Location.Y - Induktor.SH.Location.Y + 15, 0, 256);
                    e.Graphics.DrawLine(p, 1, Induktor.Location.Y - Induktor.SH.Location.Y + 15, 1, 256);
                    e.Graphics.DrawLine(p, 2, Induktor.Location.Y - Induktor.SH.Location.Y + 15, 2, 256);
                    e.Graphics.DrawLine(p, 3, Induktor.Location.Y - Induktor.SH.Location.Y + 15, 3, 256);
                }
                else
                {//fent
                    e.Graphics.DrawLine(p, 0, Induktor.Location.Y - Induktor.SH.Location.Y + 9, 0, 264);
                    e.Graphics.DrawLine(p, 1, Induktor.Location.Y - Induktor.SH.Location.Y + 9, 1, 264);
                    e.Graphics.DrawLine(p, 2, Induktor.Location.Y - Induktor.SH.Location.Y + 9, 2, 264);
                    e.Graphics.DrawLine(p, 3, Induktor.Location.Y - Induktor.SH.Location.Y + 9, 3, 264);
                }
            }
        }
    }
}
