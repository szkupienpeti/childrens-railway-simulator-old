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

namespace Gyermekvasút
{
    public partial class KimenoHivas : Form
    {
        IAllomas felhivandoAllomas;
        Allomas senderAllomas;
        bool egyCsengetes;
        Index ind;
        bool nemIksz;

        public KimenoHivas(IAllomas felhivando, Allomas sender, Index ind)
        {
            InitializeComponent();
            this.felhivandoAllomas = felhivando;
            this.senderAllomas = sender;
            this.ind = ind;

            if (sender.KezdopontiAllomas == felhivando)
            {
                egyCsengetes = true;                
            }
            else if (sender.VegpontiAllomas == felhivando)
            {
                egyCsengetes = false;
            }
            else
            {
                throw new Exception("TELEFON: KimenoHivas(IAllomas felhivando, IAllomas sender, Index ind) konstruktor: A felhívandó állomás nem feleltethető meg a hívó állomás egyik szomszéd állomásának sem!");
            }
        }

        public KimenoHivas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            visszacsengetes.Text += (sender as Button).Text + " ";
            kimenet.Text = "";
        }

        private void KimenoHivas_Load(object sender, EventArgs e)
        {
            visszacsengetes.Text = "";

            if (felhivandoAllomas == null)
            {
                allomas.Text = "NULL állomás felhívása";
                allomas.BackColor = Color.Red;
            }
            else
            {
                allomas.Text += Index.GetSzomszedAllomasNev(senderAllomas.Name, egyCsengetes);
            }            
        }

        private void button3_Click(object sender, EventArgs e)
        {//CHECK
            if (egyCsengetes && visszacsengetes.Text == "— " || !egyCsengetes && visszacsengetes.Text == "— — ")
            {//jó     
                nemIksz = true;
                this.Close();                
            }
            else
            {//rossz
                kimenet.BackColor = Color.Red;
                kimenet.Text = "✘ Helytelen csengetés!";
                visszacsengetes.Text = "";
                ind.Hibak[12]++;
            }
        }

        private void KimenoHivas_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (nemIksz)
            {
                try
                {
                    felhivandoAllomas.OnMegcsengettek(!egyCsengetes);
                }
                catch (Exception)
                {
                    MessageBox.Show("Hiba a szomszéd állomással való kommunikáció közben!\nEllenőrizd a hálózati kapcsolatot!\nÉrtesíts valakit a problémáról!", "Hálózati hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ind.notifyIcon1.ShowBalloonTip(100, "Telefon", "Várakozás " + Index.GetSzomszedAllomasNev(senderAllomas.Name, egyCsengetes) + " állomás visszacsengetésére", ToolTipIcon.None);
            }            
        }
    }
}
