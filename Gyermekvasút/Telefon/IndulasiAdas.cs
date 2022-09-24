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
    public partial class IndulasiAdas : Form
    {
        Index ind;
        Allomas hivoAll;
        IAllomas fogadoAll;
        bool kpFele;
        bool nemIksz;
        KozlemenyValaszto kozlval;

        public IndulasiAdas()
        {
            InitializeComponent();
        }

        public IndulasiAdas(Allomas hivoAll, IAllomas fogadoAll, bool kpFele, Index ind, KozlemenyValaszto kv)
        {
            InitializeComponent();

            this.hivoAll = hivoAll;
            this.fogadoAll = fogadoAll;
            this.kpFele = kpFele;
            this.ind = ind;
            kozlval = kv;

            toolStripStatusLabel1.Text += Index.GetSzomszedAllomasNev(hivoAll.Name, kpFele);
            label7.Text = "elment " + Index.GetSzomszedAllomasNev(hivoAll.Name, kpFele) + " állomásra.";

            foreach (Vonat v in hivoAll.IndulasiSzukseges)
            {
                if (v.Paros != kpFele)
                {
                    comboBox1.Items.Add(v);
                }              
            }
        }

        private void IndulasiAdas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!nemIksz)
            {
                if (MessageBox.Show("Biztosan megszakítod a hívást?", "Hívás megszakítása", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {//megszakítja a hívást >> kilépés (ballon a szomszédnak)
                    fogadoAll.OnHivasMegszakitva(kpFele);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                kozlval.Close();
            }
        }

        private void IndulasiAdas_Load(object sender, EventArgs e)
        {
            label1.Text = "Ebben az ablakban lehetőséged van közölni egy vonat indulási idejét\na szomszéd állomás rendelkezőjével.";
        }

        private void button1_Click(object sender, EventArgs e)
        {//ok
            if (comboBox1.SelectedIndex == -1)
            {//nicns vonat kiválasztva
                kimenet.BackColor = Color.Red;
                kimenet.Text = "✘ Adj meg egy vonatot!";
                return;
            }
            kimenet.BackColor = SystemColors.Control;
            kimenet.Text = "";
            object[] parameters = new object[3];
            parameters[0] = comboBox1.SelectedItem as Vonat;
            parameters[1] = (int)ora.Value;
            parameters[2] = (int)perc.Value;
            fogadoAll.OnKozlemeny(kpFele, 6, parameters);
            hivoAll.IndulasiSzuksegesIdo.Remove(hivoAll.IndulasiSzuksegesIdo[hivoAll.IndulasiSzukseges.IndexOf(parameters[0] as Vonat)]);
            hivoAll.IndulasiSzukseges.Remove(parameters[0] as Vonat);
            if (hivoAll.IndulasiSzukseges.Count == 0)
            {
                hivoAll.IndulasiTimerStop();
            }
            nemIksz = true;
            ind.notifyIcon1.ShowBalloonTip(100, "Telefon", "Várakozás " + Index.GetSzomszedAllomasNev(hivoAll.Name, kpFele) + " állomás válaszára", ToolTipIcon.None);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {//help
            MessageBox.Show("Ha nem találsz valamit, annak az oka a következő lehet:\n*Csak olyan vonatról adhatsz indulásiidő-közlést, amely már elindult az állomásodról, és még nem közölted ezt a szomszéd állomás rendelkezőjével.", "Segítség", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedItem == null)
            {
                return;
            }
            ora.Value = hivoAll.IndulasiSzuksegesIdo[hivoAll.IndulasiSzukseges.IndexOf((sender as ComboBox).SelectedItem as Vonat)].Hour;
            perc.Value = hivoAll.IndulasiSzuksegesIdo[hivoAll.IndulasiSzukseges.IndexOf((sender as ComboBox).SelectedItem as Vonat)].Minute;
        }
    }
}
