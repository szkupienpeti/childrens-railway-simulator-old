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
    public partial class BejovoHivas : Form
    {
        IAllomas hivoAllomas;
        Allomas fogadoAllomas;
        bool egyHosszu;
        Index Ind;

        public BejovoHivas(IAllomas hivoAllomas, Allomas fogadoAllomas, Index ind)
        {
            this.hivoAllomas = hivoAllomas;
            this.fogadoAllomas = fogadoAllomas;
            InitializeComponent();
            this.Ind = ind;

            if (fogadoAllomas.KezdopontiAllomas == hivoAllomas)
            {
                egyHosszu = false;
            }
            else if (fogadoAllomas.VegpontiAllomas == hivoAllomas)
            {
                egyHosszu = true;
            }
            else
            {
                throw new Exception("TELEFON: BejovoHivas(Allomas hivoAllomas, Allomas fogadoAllomas, Index ind) konstruktor: A hívó állomás nem feleltethető meg a fogadó állomás egyik szomszéd állomásának sem!");
            }
        }

        public BejovoHivas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            visszacsengetes.Text += (sender as Button).Text;
            visszacsengetes.Text += " ";
            kimenet.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((egyHosszu && visszacsengetes.Text == "— — ") || (!egyHosszu && visszacsengetes.Text == "— "))
            {//jó
                this.Close();
                try
                {
                    hivoAllomas.OnVisszacsengettek(egyHosszu);         
                }
                catch (Exception)
                {
                    MessageBox.Show("Hiba a szomszéd állomással való kommunikáció közben!\nEllenőrizd a hálózati kapcsolatot!\nÉrtesíts valakit a problémáról!", "Hálózati hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Ind.notifyIcon1.ShowBalloonTip(100, "Telefon", "Várakozás " + Index.GetSzomszedAllomasNev(fogadoAllomas.Name, !egyHosszu) + " állomás közleményére", ToolTipIcon.None);                       
            }
            else
            {//rossz
                kimenet.BackColor = Color.Red;
                kimenet.Text = "✘ Helytelen visszacsengetés!";
                visszacsengetes.Text = "";
                Ind.Hibak[13]++;
            }
        }

        private void BejovoHivas_Load(object sender, EventArgs e)
        {
            visszacsengetes.Text = "";
            bejovoCsengetes.Text = egyHosszu ? "—" : "— —";
        }
    }
}
