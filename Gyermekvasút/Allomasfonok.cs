using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gyermekvasút.Modellek;

namespace Gyermekvasút
{
    public partial class Allomasfonok : Form
    {
        Vaganyut selectedVgut;
        public S szjn = null;
        Allomas Allomas;
        Index ind = null;

        public Allomasfonok(Allomas allBE, S szj, Index ind)
        {
            InitializeComponent();
            Allomas = allBE;
            szjn = szj;
            this.ind = ind;
        }

        private void Allomasfonok_Load(object sender, EventArgs e)
        {
            label1.Text = "Ebben az ablakban lehetőséged van olyan vágányutak visszavételére,\namelyekhez számlálóval ellátott nyomógombok kezelése szükséges.";
            label2.Text = "Válaszd ki a legördülő listából, hogy melyik vágányutat szeretnéd\nvisszavenni. Csak azok a vágányutak jelennek meg, amelyek\nvisszavételéhez valóban számlálóval ellátott nyomógomb\nkezelése szükséges.";

            for (int i = 0; i < Allomas.Vaganyutak.Count; i++)
            {
                if ((Allomas.Vaganyutak[i].Felepitett && Allomas.Vaganyutak[i].KezdoJelzoSzabad) || Allomas.Vaganyutak[i].JelzoMegallj)
                {
                    comboBox1.Items.Add(Allomas.Vaganyutak[i].Nev);
                }
            }
        }

        private void bezar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ko.Enabled = true;

            kimenet.Text = "";
            kimenet.BackColor = SystemColors.Control;

            if (comboBox1.SelectedItem.ToString() == Allomas.Vaganyutak[0].Nev)
            {
                selectedVgut = Allomas.Vaganyutak[0];

                kp.Text = "Kezdőpont: A";
                vp.Text = "Végpont: V1";
                if (Allomas.Vaganyutak[0].JelzoMegallj)
	            {
                    kpallas.Text = "Kezdő jelző állása: Megállj! (Zöldzavar)";
	            }
                else
                {
                    kpallas.Text = "Kezdő jelző állása: Szabad!";
                }
            }
            else
            {
                if (comboBox1.SelectedItem.ToString() == Allomas.Vaganyutak[1].Nev)
                {
                    selectedVgut = Allomas.Vaganyutak[1];

                    kp.Text = "Kezdőpont: A";
                    vp.Text = "Végpont: V2";
                    if (Allomas.Vaganyutak[1].JelzoMegallj)
	                {
                        kpallas.Text = "Kezdő jelző állása: Megállj! (Zöldzavar)";
	                }
                    else
                    {
                        kpallas.Text = "Kezdő jelző állása: Szabad!";
                    }
                }
                else
                {
                    if (comboBox1.SelectedItem.ToString() == Allomas.Vaganyutak[5].Nev)
                    {
                        selectedVgut = Allomas.Vaganyutak[5];

                        kp.Text = "Kezdőpont: B";
                        vp.Text = "Végpont: K1";
                        if (Allomas.Vaganyutak[5].JelzoMegallj)
	                    {
                            kpallas.Text = "Kezdő jelző állása: Megállj! (Zöldzavar)";
	                    }
                        else
                        {
                            kpallas.Text = "Kezdő jelző állása: Szabad!";
                        }
                    }
                    else
                    {
                        if (comboBox1.SelectedItem.ToString() == Allomas.Vaganyutak[4].Nev)
                        {
                            selectedVgut = Allomas.Vaganyutak[4];

                            kp.Text = "Kezdőpont: B";
                            vp.Text = "Végpont: K2";
                            if (Allomas.Vaganyutak[4].JelzoMegallj)
	                        {
                                kpallas.Text = "Kezdő jelző állása: Megállj! (Zöldzavar)";
	                        }
                            else
                            {
                                kpallas.Text = "Kezdő jelző állása: Szabad!";
                            }
                        }
                        else
                        {
                            if (comboBox1.SelectedItem.ToString() == Allomas.Vaganyutak[6].Nev)
                            {
                                selectedVgut = Allomas.Vaganyutak[6];

                                kp.Text = "Kezdőpont: K2";
                                vp.Text = "Végpont: a (János-hegy állomás felé)";
                                if (Allomas.Vaganyutak[6].JelzoMegallj)
	                            {
                                    kpallas.Text = "Kezdő jelző állása: Megállj! (Zöldzavar)";
	                            }
                                else
                                {
                                    kpallas.Text = "Kezdő jelző állása: Szabad!";
                                }
                            }
                            else
                            {
                                if (comboBox1.SelectedItem.ToString() == Allomas.Vaganyutak[7].Nev)
                                {
                                    selectedVgut = Allomas.Vaganyutak[7];

                                    kp.Text = "Kezdőpont: K1";
                                    vp.Text = "Végpont: a (János-hegy állomás felé)";
                                    if (Allomas.Vaganyutak[7].JelzoMegallj)
	                                {
                                        kpallas.Text = "Kezdő jelző állása: Megállj! (Zöldzavar)";
	                                }
                                    else
                                    {
                                        kpallas.Text = "Kezdő jelző állása: Szabad!";
                                    }
                                }
                                else
                                {
                                    if (comboBox1.SelectedItem.ToString() == Allomas.Vaganyutak[2].Nev)
                                    {
                                        selectedVgut = Allomas.Vaganyutak[2];

                                        kp.Text = "Kezdőpont: V2";
                                        vp.Text = "Végpont: b (Hárs-hegy állomás felé)";
                                        if (Allomas.Vaganyutak[2].JelzoMegallj)
	                                    {
                                            kpallas.Text = "Kezdő jelző állása: Megállj! (Zöldzavar)";
	                                    }
                                        else
                                        {
                                            kpallas.Text = "Kezdő jelző állása: Szabad!";
                                        }
                                    }
                                    else
                                    {
                                        if (comboBox1.SelectedItem.ToString() == Allomas.Vaganyutak[3].Nev)
                                        {
                                            selectedVgut = Allomas.Vaganyutak[3];

                                            kp.Text = "Kezdőpont: V1";
                                            vp.Text = "Végpont: b (Hárs-hegy állomás felé)";
                                            if (Allomas.Vaganyutak[3].JelzoMegallj)
	                                        {
                                                kpallas.Text = "Kezdő jelző állása: Megállj! (Zöldzavar)";
	                                        }
                                            else
                                            {
                                                kpallas.Text = "Kezdő jelző állása: Szabad!";
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Porszem került a gépezetbe! :(\nA legördülő lista .SelectedItem-jének .ToString() metódusának visszatérési értéke nem feleltethető meg az Allomas objektum egyetlen Vaganyut példányának .Nev tulajdonságának string értékének sem!\nA következőkben a program működése meglehetősen bizonytalan!\nÉrtesíts valakit a problémáról!\n\nTudni kell, hogy ilyen nem történhetne. Ez egy olyan eset lekezelése, amely ha bekövetkezett, az azt jelenti, hogy valamit elrontottunk a program tervezése során (vagy persze azt, hogy valaki Admin módban megpiszkált pár beállítást).", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            comboBox1.Items.Clear();
            for (int i = 0; i < Allomas.Vaganyutak.Count; i++)
            {
                if ((Allomas.Vaganyutak[i].Felepitett && Allomas.Vaganyutak[i].KezdoJelzoSzabad) || Allomas.Vaganyutak[i].JelzoMegallj)
                {
                    comboBox1.Items.Add(Allomas.Vaganyutak[i].Nev);
                }
            }
        }

        public void ko_Click(object sender, EventArgs e)
        {
            if (selectedVgut != null)
            {
                if (selectedVgut.Bejarat)
                {
                    //bejárati vágányút >> az első két szakaszt kell vizsgálni (az utolsót nem, hiszen az a vágány. ha már megérkezett a vonat, KOzható)
                    if (selectedVgut.Szakaszok[0].Foglalt == false && selectedVgut.Szakaszok[1].Foglalt == false)
                    {
                        //kényszeroldás
                        selectedVgut.JelzoMegallj = false;
                        selectedVgut.Felepitett = false;
                        selectedVgut.Kezdopont.Szabad = false;
                        if (selectedVgut == Allomas.Vaganyutak[0] || selectedVgut == Allomas.Vaganyutak[1])
                        {
                            Allomas.Valtok[0].Lezart = false;
                            szjn.egyeni2.Image = Gyermekvasút.Properties.Resources.d55_2_alap;
                        }
                        else
                        {
                            Allomas.Valtok[1].Lezart = false;
                            szjn.egyeni2.Image = Gyermekvasút.Properties.Resources.d55_1_alap;
                        }
                        kimenet.BackColor = System.Drawing.Color.Green;
                        kimenet.Text = "✓ A vágányút kényszeroldása sikeresen megtörtént!";
                        szjn.Ind.s_ko++;
                        ind.Hibak[11]++;
                        comboBox1.Items.Remove(comboBox1.SelectedItem);
                    }
                    else
                    {
                        kimenet.BackColor = System.Drawing.Color.Red;
                        kimenet.Text = "✘ A vágányút kényszeroldása veszélyeztetné a forgalombiztonságot,\nígy az nem történt meg!\nCsak olyan vágányút kényszeroldható, amelyen nem közlekedik jármű!";
                    }
                }
                else
                {
                    //kijárati vágányút >> az első szakaszt nem kell vizsgálni (hiszen az a vágány)
                    if (selectedVgut.Szakaszok[1].Foglalt == false && selectedVgut.Szakaszok[2].Foglalt == false)
                    {
                        //kényszeroldás
                        selectedVgut.JelzoMegallj = false;
                        selectedVgut.Felepitett = false;
                        selectedVgut.Kezdopont.Szabad = false;
                        if (selectedVgut == Allomas.Vaganyutak[6] || selectedVgut == Allomas.Vaganyutak[7])
                        {
                            Allomas.Valtok[0].Lezart = false;
                            szjn.egyeni2.Image = Gyermekvasút.Properties.Resources.d55_2_alap;
                        }
                        else
                        {
                            Allomas.Valtok[1].Lezart = false;
                            szjn.egyeni2.Image = Gyermekvasút.Properties.Resources.d55_1_alap;
                        }
                        kimenet.BackColor = System.Drawing.Color.Green;
                        kimenet.Text = "✓ A vágányút kényszeroldása sikeresen megtörtént!";
                        szjn.Ind.s_ko++;
                        ind.Hibak[11]++;
                        comboBox1.Items.Remove(comboBox1.SelectedItem);
                    }
                    else
                    {
                        kimenet.BackColor = System.Drawing.Color.Red;
                        kimenet.Text = "✘ A vágányút kényszeroldása veszélyeztetné a forgalombiztonságot,\nígy az nem történt meg!\nCsak olyan vágányút kényszeroldható, amelyen nem közlekedik jármű!";
                    }
                }
            }
            comboBox1.Items.Clear();
            for (int i = 0; i < Allomas.Vaganyutak.Count; i++)
            {
                if ((Allomas.Vaganyutak[i].Felepitett && Allomas.Vaganyutak[i].KezdoJelzoSzabad) || Allomas.Vaganyutak[i].JelzoMegallj)
                {
                    comboBox1.Items.Add(Allomas.Vaganyutak[i].Nev);
                }
            }
            ko.Enabled = false;
        }
    }
}
