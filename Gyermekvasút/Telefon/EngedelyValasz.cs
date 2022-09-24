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
    public partial class EngedelyValasz : Form
    {
        bool kpFeleHiv;
        IAllomas hivoAllomas;
        Allomas fogadoAllomas;
        KozlemenyTipus kozlemenyTipus;
        object[] parameters;

        public EngedelyValasz()
        {
            InitializeComponent();
        }

        public EngedelyValasz(bool kpFeleHiv, int kozlemenyTipus, object[] parameters, Allomas fogadoAll, IAllomas hivoAll)
        {
            InitializeComponent();
            this.kozlemenyTipus = (KozlemenyTipus)kozlemenyTipus;
            this.kpFeleHiv = kpFeleHiv;
            this.parameters = parameters;
            this.fogadoAllomas = fogadoAll;
            this.hivoAllomas = hivoAll;

            if (this.kozlemenyTipus == KozlemenyTipus.EngKeresAzonos)
            {
                bejovo.Text = "Mehet-e kb. " + parameters[0] + " óra " + parameters[1] + " perckor a(z) " + (parameters[2] as Vonat).Vonatszam + " számú vonat\n" + fogadoAllomas.Name + " állomásra?";
            }
            else if (this.kozlemenyTipus == KozlemenyTipus.EngKeresEllenkVan)
            {
                bejovo.Text = "Ha itt a(z) " + (parameters[0] as Vonat).Vonatszam + " számú vonat, mehet-e kb. " + parameters[1] + " óra\n" + parameters[2] + " perckor a(z) " + (parameters[3] as Vonat).Vonatszam + " számú vonat " + fogadoAllomas.Name + " állomásra?";
            }
            else if (this.kozlemenyTipus == KozlemenyTipus.EngKeresEllenkVolt)
            {
                bejovo.Text = "Utolsó vonat a(z) " + (parameters[0] as Vonat).Vonatszam + " számú vonat. Mehet-e kb. " + parameters[1] + " óra\n" + parameters[2] + " perckor a(z) " + (parameters[3] as Vonat).Vonatszam + " számú vonat " + fogadoAllomas.Name + " állomásra?";
            }
            toolStripStatusLabel1.Text += Index.GetSzomszedAllomasNev(fogadoAllomas.Name, !kpFeleHiv);
            if (this.kozlemenyTipus == KozlemenyTipus.EngKeresAzonos)
            {
                engLabel1.Text = "Vonatot nem indítok. A(z) " + (parameters[2] as Vonat).Vonatszam + " számú vonat " + fogadoAllomas.Name + " állomásra jöhet.";
                megtagad1.Text = "A(z) " + (parameters[2] as Vonat).Vonatszam + " számú vonatot";
            }
            else //ellenvonat
            {
                engLabel1.Text = "Vonatot nem indítok. Ha ott a(z) " + (parameters[0] as Vonat).Vonatszam + " számú vonat, a(z)" + (parameters[3] as Vonat).Vonatszam + " számú vonat\n" + fogadoAllomas.Name + " állomásra jöhet.";
                megtagad1.Text = "A(z) " + (parameters[3] as Vonat).Vonatszam + " számú vonatot";
            }            
        }

        private void EngedelyValasz_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                groupBox2.Enabled = true;
                groupBox3.Enabled = false;
                button1.Text = "Engedélyadás";
            }
            else if (radioButton2.Checked)
            {
                groupBox2.Enabled = false;
                groupBox3.Enabled = true;
                button1.Text = "Engedélymegtagadás";
            }
        }

        private void engLabel1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            object[] parameters2;
            if (radioButton1.Checked)
            {//engedélyadás                
                if (kozlemenyTipus == KozlemenyTipus.EngKeresAzonos)
                {
                    parameters2 = new object[1];
                    parameters2[0] = parameters[2];
                    hivoAllomas.OnKozlemeny(kpFeleHiv, 3, parameters2);
                }
                else
                {
                    parameters2 = new object[2];
                    parameters2[0] = parameters[0];
                    parameters2[1] = parameters[3];
                    hivoAllomas.OnKozlemeny(kpFeleHiv, 4, parameters2);
                }

                //fogadoAllomas.allkoz.Engedelyben SET --
                if (kpFeleHiv)
                {
                    //(fogadoAllomas.Szakaszok[6] as Allomaskoz).TimerStart();
                    if (kozlemenyTipus == KozlemenyTipus.EngKeresEllenkVan)
                    {//engedelyben != null >> use engedelyben2
                        (fogadoAllomas.Szakaszok[6] as Allomaskoz).Engedelyben2 = parameters[3] as Vonat;
                        
                    }
                    else
                    {//engedelyben == null >> use engedelyben
                        if (kozlemenyTipus == KozlemenyTipus.EngKeresAzonos)
                        {
                            (fogadoAllomas.Szakaszok[6] as Allomaskoz).Engedelyben = parameters[2] as Vonat;
                        }
                        else
                        {
                            (fogadoAllomas.Szakaszok[6] as Allomaskoz).Engedelyben = parameters[3] as Vonat;
                        }
                    }

                    //(fogadoAllomas.Szakaszok[6] as Allomaskoz).TimerStart();
                }
                else
                {
                    //(fogadoAllomas.Szakaszok[0] as Allomaskoz).TimerStart();
                    if (kozlemenyTipus == KozlemenyTipus.EngKeresEllenkVan)
                    {//engedelyben != null >> use engedelyben2
                        (fogadoAllomas.Szakaszok[0] as Allomaskoz).Engedelyben2 = parameters[3] as Vonat;
                    }
                    else
                    {//engedelyben == null >> use engedelyben
                        if (kozlemenyTipus == KozlemenyTipus.EngKeresAzonos)
                        {
                            (fogadoAllomas.Szakaszok[0] as Allomaskoz).Engedelyben = parameters[2] as Vonat;
                        }
                        else
                        {
                            (fogadoAllomas.Szakaszok[0] as Allomaskoz).Engedelyben = parameters[3] as Vonat;
                        }
                    }

                    //(fogadoAllomas.Szakaszok[0] as Allomaskoz).TimerStart();
                }
            }
            else
            {//engedélymegtagadás
                parameters2 = new object[3];
                if (kozlemenyTipus == KozlemenyTipus.EngKeresAzonos)
	            {
                    parameters2[0] = parameters[2];
	            }
                else
                {
                    parameters2[0] = parameters[3];
                }
                parameters2[1] = textBox1.Text;
                parameters2[2] = (int)numericUpDown1.Value;
                hivoAllomas.OnKozlemeny(kpFeleHiv, 5, parameters2);
            }            
            this.Close();
        }
    }
}
