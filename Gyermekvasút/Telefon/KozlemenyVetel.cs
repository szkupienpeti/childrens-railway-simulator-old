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
    public partial class KozlemenyVetel : Form
    {
        bool kpFeleHiv;
        IAllomas hivoAllomas;
        Allomas fogadoAllomas;
        KozlemenyTipus kozlemenyTipus;
        object[] parameters;

        public KozlemenyVetel()
        {
            InitializeComponent();
        }

        public KozlemenyVetel(bool kpFele, Allomas fogadoAll, IAllomas hivoAll, object[] parameters, int kozlemenyTipus)
        {
            InitializeComponent();
            this.kozlemenyTipus = (KozlemenyTipus)kozlemenyTipus;
            this.kpFeleHiv = kpFele;
            this.parameters = parameters;
            this.fogadoAllomas = fogadoAll;
            this.hivoAllomas = hivoAll;

            if (this.kozlemenyTipus == KozlemenyTipus.IndulasiAdas)
            {
                this.Text = "Indulásiidő-közlés vétele";
                groupBox1.Text = "Indulásiidő-közlés";
                bejovoText.Text = "A(z) " + (parameters[0] as Vonat).Vonatszam + " számú vonat " + parameters[1].ToString() + " óra " + parameters[2].ToString() + " perckor\nelment " + fogadoAllomas.Name + " állomásra.";
                valaszText.Text = "A(z) " + (parameters[0] as Vonat).Vonatszam + " számú vonat indulásiidő-közlését\n" + Index.GetSzomszedAllomasNev(fogadoAll.Name, !kpFele) + " állomásról vettem.";
            }
            else if (this.kozlemenyTipus == KozlemenyTipus.VisszjelAdas)
            {
                this.Text = "Visszajelentés vétele";
                groupBox1.Text = "Visszajelentés";
                bejovoText.Text = "A(z) " + (parameters[0] as Vonat).Vonatszam + " számú vonat megérkezett\n" + Index.GetSzomszedAllomasNev(fogadoAll.Name, !kpFele) + " állomásra.";
                valaszText.Text = "A(z) " + (parameters[0] as Vonat).Vonatszam + " számú vonat visszajelentését\n" + Index.GetSzomszedAllomasNev(fogadoAll.Name, !kpFele) + " állomásról vettem.";
            }

            toolStripStatusLabel1.Text += Index.GetSzomszedAllomasNev(fogadoAll.Name, !kpFele);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object[] parameters2 = new object[1];
            parameters2[0] = parameters[0] as Vonat;
            if (kozlemenyTipus == KozlemenyTipus.IndulasiAdas)
	        {
		        hivoAllomas.OnKozlemeny(kpFeleHiv, 7, parameters2);
                this.Close();
	        }
            else if (kozlemenyTipus == KozlemenyTipus.VisszjelAdas)
            {
                hivoAllomas.OnKozlemeny(kpFeleHiv, 9, parameters2);
                this.Close();
            }
        }
    }
}
