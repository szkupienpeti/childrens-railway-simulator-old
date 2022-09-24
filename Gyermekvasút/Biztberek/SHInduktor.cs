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
    public partial class SHInduktor : UserControl
    {
        public SHInduktor()
        {
            InitializeComponent();
        }

        public SH SH { get; set; }
        SHInduktorOldal oldal;
        public SHInduktorOldal Oldal
        {
            get { return oldal; }
            set
            {
                oldal = value;
                if (oldal == SHInduktorOldal.Bal)
                {
                    this.BackgroundImage = Gyermekvasút.Properties.Resources.shInduktorBal;
                    Szog =  -1 * Math.PI / 2;
                }
                else
                {
                    this.BackgroundImage = Gyermekvasút.Properties.Resources.shInduktorJobb;
                    Szog = Math.PI / 2;
                }
            }
        }

        public double Szog { get; set; }
        static public double SzogSebesseg = 10;
        /// <summary>
        /// A bal induktor középső állásának az SH objektum bal felső sarkához viszonyított relatív pozíciója.
        /// </summary>
        static public Point BalKozepsoRelativPozicio = new Point(0, 248);
        /// <summary>
        /// A jobb induktor középső állásának az SH objektum bal felső sarkához viszonyított relatív pozíciója.
        /// </summary>
        static public Point JobbKozepsoRelativPozicio = new Point(393, 248);
        /// <summary>
        /// Az az InduktorPanel (UserControl) objektum, amelyre kirajzolásra kerül az induktorfejet a blokkszekrénnyel összekötő vonal.
        /// </summary>
        public SHInduktorPanel Panel { get; set; }

        /// <summary>
        /// Frissíti az induktorhoz tartozó SHInduktorPanel-t, amely így kirajzolja az összekötő vonalat.
        /// </summary>
        public void VonalKirajzol()
        {
            if (Panel != null)
            {
                Panel.Invalidate();
            }
        }
    }

    public enum SHInduktorOldal { Bal, Jobb }
}
