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
    public partial class Koruljaras : Form
    {
        string vonatszam = "";
        string vagany = "";
        Allomas allomas = null;

        public Koruljaras()
        {
            InitializeComponent();
        }
        public Koruljaras(Allomas allomas, string vonatszam)
        {
            this.vonatszam = vonatszam;
            InitializeComponent();
            if (vonatszam == "")
            {
                throw new Exception("A Koruljaras form public Koruljaras(string vonatszam) konstruktora üres stringet kapott bemenetként.");
            }
            this.allomas = allomas;

            ValtKez vk = null;
            if (allomas.Name == "Hűvösvölgy")
            {
                label1.Text = "Váltókezelő 2. pajtás!"; //O
                vk = allomas.ValtKezek[0];
            }
            else
            {
                label1.Text = "Váltókezelő 1. pajtás!"; //A
                vk = allomas.ValtKezek[1];
            }

            //sima BE után: masikVgString
            //Be, ellenvonatos ki: a vk.Vagany a BE-hez tartozik >> masikVgString
            //sima kij.: a vk.vagany a KI-hez tartozik (ahol majd a másik körüljár) >> sima vgString
            vagany = vk.Be == null ? vk.VaganyString : ValtKez.MasikvaganyStringMegad(vk.VaganyString);
        }

        private void Koruljaras_Load(object sender, EventArgs e)
        {
            allomas.KoruljarasElrendelesSzuksegesVonatszam = "";

            //vagy érk. után vagy kihaladás után.
            if (allomas.Vonatok.Count == 0 || (allomas.Vonatok.Count == 1 && allomas.Vonatok[0].Vonatszam == vonatszam))
            {//nincs másik vonat >> érk. után
                elrendeles.Text = "A(z) " + vonatszam + " számú vonat gépe érkezés után körüljár a(z) " + vagany + " vágányon.";
            }
            else
            {//van másik vonat >> a másik kihaladása után
                string kihaladoVonat = "";
                if (allomas.Vonatok.Count == 1)
                {
                    kihaladoVonat = allomas.Vonatok[0].Vonatszam;
                }
                else
                {
                    if (allomas.Vonatok[0].Vonatszam == vonatszam) kihaladoVonat = allomas.Vonatok[1].Vonatszam;
                    else kihaladoVonat = allomas.Vonatok[0].Vonatszam;
                }
                elrendeles.Text = "A(z) " + kihaladoVonat + " számú vonat kihaladása után a(z) " + vonatszam + " számú vonat\ngépe körüljár a(z) " + vagany + " vágányon.";
            }
        }

        private void button1_Click(object sender, EventArgs e)//elrendelés
        {
            //SET allomas.KoruljarasElrendelve
            allomas.KoruljarasElrendelve = allomas.VonatokEsEngedelybenLevoVonatok.Where(v => v.Vonatszam == vonatszam).Single();
            kimenet.BackColor = Color.Green;
            kimenet.Text = "✓ A kiválasztott vonat körüljáratásának elrendelése sikeresen megtörtént!";
            visszamondas.Text = "Rendelkező pajtás!\n" + elrendeles.Text + "\nVettem!";
            button2.Enabled = true;
            button1.Enabled = false;
            button2.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
