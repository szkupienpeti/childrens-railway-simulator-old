using Gyermekvasút.Hálózat;
using Gyermekvasút.Modellek;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút.Telefon
{
    public partial class TelefonValasz : Form
    {
        Allomas hivoAll;
        IAllomas fogadoAll;
        Index ind;

        public TelefonValasz()
        {
            InitializeComponent();
        }

        public TelefonValasz(string formText, string labelText, string valaszoloAllomas, Allomas hivoAll, IAllomas fogadoAll, Index ind)
        {
            InitializeComponent();
            this.Text = formText;
            text.Text = labelText;
            toolStripStatusLabel1.Text += valaszoloAllomas;
            this.hivoAll = hivoAll;
            this.fogadoAll = fogadoAll;
            this.ind = ind;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            fogadoAll.OnHivasMegszakitva(hivoAll.KezdopontiAllomas == fogadoAll);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KozlemenyValaszto kv = new KozlemenyValaszto(hivoAll, fogadoAll, ind);
            this.Close();
            kv.ShowDialog();
        }

        private void TelefonValasz_Load(object sender, EventArgs e)
        {
            
        }
    }
}
