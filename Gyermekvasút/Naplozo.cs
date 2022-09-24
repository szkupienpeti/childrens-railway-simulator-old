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
    public partial class Naplozo : Form
    {
        bool seged = false;
        List<Szakasz> belsoSzakaszLista = new List<Szakasz>();
        List<Vonat> meneszthetoVonatok = new List<Vonat>();
        Szakasz vagany1;
        Szakasz vagany2;
        Jelzo vagany1jelzoK;
        Jelzo vagany1jelzoV;
        Jelzo vagany2jelzoK;
        Jelzo vagany2jelzoV;

        bool nicnsKijaratiJelzo;
        bool vagany1egyenes;

        bool listClearelheto;

        Allomas allomas;

        Index ind;

        public Naplozo(Allomas allomas, bool virtualisKijaratiJelzok, bool vagany1egyenesBEMENET, Index ind)
        {
            InitializeComponent();
            belsoSzakaszLista = allomas.Szakaszok;
            vagany1egyenes = vagany1egyenesBEMENET;
            nicnsKijaratiJelzo = virtualisKijaratiJelzok;
            this.allomas = allomas;
            this.ind = ind;
        }

        private void Naplozo_Load(object sender, EventArgs e)
        {
            seged = false;
            button1.Enabled = false;
            kimenet.BackColor = SystemColors.Control;

            label1.Text = "Ebben az ablakban lehetőséged van a naplózó\nfelhatalmazására a vonatok menesztésére.";

            if (vagany1egyenes)
            {
                //1. vágány E
                vagany1 = belsoSzakaszLista[3];
                vagany1jelzoK = belsoSzakaszLista[3].Jelzo;
                vagany1jelzoV = belsoSzakaszLista[4].Jelzo;
                vagany2 = belsoSzakaszLista[8];
                vagany2jelzoK = belsoSzakaszLista[8].Jelzo;
                vagany2jelzoV = belsoSzakaszLista[9].Jelzo;
            }
            else
            {
                //1. vágány K
                vagany1 = belsoSzakaszLista[8];
                vagany1jelzoK = belsoSzakaszLista[8].Jelzo;
                vagany1jelzoV = belsoSzakaszLista[9].Jelzo;
                vagany2 = belsoSzakaszLista[3];
                vagany2jelzoK = belsoSzakaszLista[3].Jelzo;
                vagany2jelzoV = belsoSzakaszLista[4].Jelzo;
            }

            for (int i = 0; i < allomas.Vonatok.Count; i++)
            {
                if ((allomas.Vonatok[i].Szakasz == vagany2 || allomas.Vonatok[i].Szakasz == vagany1) && allomas.Vonatok[i].Menesztett == false) //vagyis az állomáson van és még nem menesztett
                {
                    meneszthetoVonatok.Add(allomas.Vonatok[i]);
                    vonat.Items.Add(allomas.Vonatok[i].Vonatszam);
                    listClearelheto = true;
                }
            }

            vg.Items.Add("I.");
            vg.Items.Add("II.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (vonat.SelectedIndex == -1 && vg.SelectedIndex == -1)
            {
                kimenet.BackColor = System.Drawing.Color.Red;
                kimenet.Text = "✘ Adj meg egy vonatot és egy vágányszámot!";
            }
            else
            {
                if ((meneszthetoVonatok[vonat.SelectedIndex].Szakasz == vagany2 && vg.SelectedItem.ToString() != "II.") || (meneszthetoVonatok[vonat.SelectedIndex].Szakasz == vagany1 && vg.SelectedItem.ToString() != "I."))
                {
                    kimenet.BackColor = System.Drawing.Color.Red;
                    kimenet.Text = "✘ A kiválasztott vágányon nem található\na kiválasztott vonat!";
                }
                else
                {
                    //ott van a vonat a kiválasztott vágányon
                    if (Convert.ToInt32(meneszthetoVonatok[vonat.SelectedIndex].Vonatszam) % 2 == 0)
                    {
                        //páros vonat HŰVÖSVÖLGY felé
                        if (((allomas.Szakaszok[6] as Allomaskoz).Engedelyben != null && (allomas.Szakaszok[6] as Allomaskoz).Engedelyben.Vonatszam == vonat.SelectedItem.ToString()) || ((allomas.Szakaszok[6] as Allomaskoz).Engedelyben2 != null && (allomas.Szakaszok[6] as Allomaskoz).Engedelyben2.Vonatszam == vonat.SelectedItem.ToString() && allomas.FogadottVonatok.Contains((allomas.Szakaszok[6] as Allomaskoz).Engedelyben)))
                        {//van engedély (engedelyben VAGY engedelyben2 és engedelyben megérkezett)
                            if (vg.SelectedItem.ToString() == "I.")
                            {
                                //V1_KI
                                if ((vagany1jelzoV.Szabad && nicnsKijaratiJelzo == false) || (nicnsKijaratiJelzo && vagany1egyenes && (belsoSzakaszLista[5] as CsucsSzakasz).Valto.Allas && (belsoSzakaszLista[5] as CsucsSzakasz).Valto.Lezart) || (nicnsKijaratiJelzo && vagany1egyenes == false && (belsoSzakaszLista[5] as CsucsSzakasz).Valto.Allas == false) && (belsoSzakaszLista[5] as CsucsSzakasz).Valto.Lezart)
                                {
                                    vagany1.Vonat.Menesztett = true;
                                    kimenet.BackColor = System.Drawing.Color.Green;
                                    kimenet.Text = "✓ A kiválasztott vonat menesztésére való\nfelhatalmazás sikeresen megtörtént!";
                                    visszamondas.Text = "Rendelkező pajtás!\nA(z) " + vonat.SelectedItem.ToString() + " számú vonat meneszthető\na(z) " + vg.SelectedItem.ToString() + " vágányról.\nVettem!";
                                    allomas.IndulasiSzukseges.Add(vagany1.Vonat);
                                    allomas.IndulasiSzuksegesIdo.Add(Program.Ido);
                                    allomas.IndulasiTimerStart();

                                    vagany1jelzoV.Menesztes = true;
                                    allomas.AllomasLogTextAdd("Menesztes?" + vagany1.Vonat.Vonatszam + "?" + vagany1jelzoV.Name);

                                    seged = true;
                                    vonat.SelectedIndex = -1;
                                    vg.SelectedIndex = -1;
                                    seged = false;

                                    button1.Enabled = false;
                                    meneszthetoVonatok.Clear();
                                    listClearelheto = false;
                                    vonat.Items.Clear();
                                    for (int i = 0; i < allomas.Vonatok.Count; i++)
                                    {
                                        if ((allomas.Vonatok[i].Szakasz == vagany2 || allomas.Vonatok[i].Szakasz == vagany1) && allomas.Vonatok[i].Menesztett == false) //vagyis az állomáson van és nem menesztett
                                        {
                                            meneszthetoVonatok.Add(allomas.Vonatok[i]);
                                            vonat.Items.Add(allomas.Vonatok[i].Vonatszam);
                                            listClearelheto = true;
                                        }
                                    }
                                }
                                else
                                {
                                    //vörösbe menesztés vagy rosszul áll a kij. váltó vagy nincs lezárva
                                    kimenet.BackColor = System.Drawing.Color.Red;
                                    ind.Hibak[20]++;
                                    if (nicnsKijaratiJelzo)
                                    {
                                        kimenet.Text = "✘ A kiválasztott vonat nem meneszthető, mert\na kijárati váltó (1) állása nem megfelelő vagy nincs lezárva!";
                                    }
                                    else
                                    {
                                        kimenet.Text = "✘ A kiválasztott vonat nem meneszthető, mert\na kijárati jelző (" + vagany1jelzoV.Name + ") továbbhaladást tiltó állásban van!";
                                    }
                                }

                            }
                            else
                            {
                                //V2_KI
                                if ((vagany2jelzoV.Szabad && nicnsKijaratiJelzo == false) || (nicnsKijaratiJelzo && vagany1egyenes && (belsoSzakaszLista[5] as CsucsSzakasz).Valto.Allas == false && (belsoSzakaszLista[5] as CsucsSzakasz).Valto.Lezart) || (nicnsKijaratiJelzo && vagany1egyenes == false && (belsoSzakaszLista[5] as CsucsSzakasz).Valto.Allas == true && (belsoSzakaszLista[5] as CsucsSzakasz).Valto.Lezart))
                                {
                                    vagany2.Vonat.Menesztett = true;
                                    kimenet.BackColor = System.Drawing.Color.Green;
                                    kimenet.Text = "✓ A kiválasztott vonat menesztésére való\nfelhatalmazás sikeresen megtörtént!";
                                    visszamondas.Text = "Rendelkező pajtás!\nA(z) " + vonat.SelectedItem.ToString() + " számú vonat meneszthető\na(z) " + vg.SelectedItem.ToString() + " vágányról.\nVettem!";

                                    allomas.IndulasiSzukseges.Add(vagany2.Vonat);
                                    allomas.IndulasiSzuksegesIdo.Add(Program.Ido);
                                    allomas.IndulasiTimerStart();
                                    vagany2jelzoV.Menesztes = true;
                                    allomas.AllomasLogTextAdd("Menesztes?" + vagany2.Vonat.Vonatszam + "?" + vagany2jelzoV.Name);

                                    seged = true;
                                    vonat.SelectedIndex = -1;
                                    vg.SelectedIndex = -1;
                                    seged = false;

                                    button1.Enabled = false;
                                    meneszthetoVonatok.Clear();
                                    listClearelheto = false;
                                    vonat.Items.Clear();
                                    for (int i = 0; i < allomas.Vonatok.Count; i++)
                                    {
                                        if ((allomas.Vonatok[i].Szakasz == vagany2 || allomas.Vonatok[i].Szakasz == vagany1) && allomas.Vonatok[i].Menesztett == false) //vagyis az állomáson van és nem menesztett
                                        {
                                            meneszthetoVonatok.Add(allomas.Vonatok[i]);
                                            vonat.Items.Add(allomas.Vonatok[i].Vonatszam);
                                            listClearelheto = true;
                                        }
                                    }
                                }
                                else
                                {
                                    //vörösbe menesztés vagy kij. váltó állása rossz vagy nincs lezárva
                                    kimenet.BackColor = System.Drawing.Color.Red;
                                    ind.Hibak[20]++;
                                    if (nicnsKijaratiJelzo)
                                    {
                                        kimenet.Text = "✘ A kiválasztott vonat nem meneszthető, mert\na kijárati váltó (1) állása nem megfelelő vagy nincs lezárva!";
                                    }
                                    else
                                    {
                                        kimenet.Text = "✘ A kiválasztott vonat nem meneszthető, mert\na kijárati jelző (" + vagany2jelzoV.Name + ") továbbhaladást tiltó állásban van!";
                                    }
                                }
                            }
                        }
                        else
                        {//nincs engedély >> vagy nincs engedélyben vagy engedelyben2 de engedelyben még nem érkezet meg
                            kimenet.BackColor = System.Drawing.Color.Red;
                            if ((allomas.Szakaszok[6] as Allomaskoz).Engedelyben2 != null && (allomas.Szakaszok[6] as Allomaskoz).Engedelyben2.Vonatszam == vonat.SelectedItem.ToString() && !allomas.FogadottVonatok.Contains((allomas.Szakaszok[6] as Allomaskoz).Engedelyben))
                            {//szembemenesztés
                                kimenet.Text = "✘ A kiválasztott vonat nem meneszthető,\nmert a(z) " + (allomas.Szakaszok[6] as Allomaskoz).Engedelyben.Vonatszam + " számú vonat éppen szembeközlekedik vele.";
                                ind.Hibak[19]++;
                            }
                            else
                            {//nincs engedély
                                kimenet.Text = "✘ A kiválasztott vonat nem meneszthető,\nmert nincs engedélye " + Index.GetSzomszedAllomasNev(allomas.Name, false) + " állomástól.";
                                ind.Hibak[18]++;
                            }
                        }
                    }
                    else
                    {
                        //páratlan vonat SZÉCHENYI-HEGY felé
                        if (((allomas.Szakaszok[0] as Allomaskoz).Engedelyben != null && (allomas.Szakaszok[0] as Allomaskoz).Engedelyben.Vonatszam == vonat.SelectedItem.ToString()) || ((allomas.Szakaszok[0] as Allomaskoz).Engedelyben2 != null && (allomas.Szakaszok[0] as Allomaskoz).Engedelyben2.Vonatszam == vonat.SelectedItem.ToString() && allomas.FogadottVonatok.Contains((allomas.Szakaszok[0] as Allomaskoz).Engedelyben)))
                        {//van engedély (engedelyben VAGY engedelyben2 és engedelyben megérkezett)
                            if (vg.SelectedItem.ToString() == "I.")
                            {
                                //K1_KI
                                if ((vagany1jelzoK.Szabad && nicnsKijaratiJelzo == false) || (nicnsKijaratiJelzo && vagany1egyenes && (belsoSzakaszLista[1] as CsucsSzakasz).Valto.Allas == true && (belsoSzakaszLista[1] as CsucsSzakasz).Valto.Lezart) || (nicnsKijaratiJelzo && vagany1egyenes == false && (belsoSzakaszLista[1] as CsucsSzakasz).Valto.Allas == false && (belsoSzakaszLista[1] as CsucsSzakasz).Valto.Lezart))
                                {
                                    vagany1.Vonat.Menesztett = true;
                                    kimenet.BackColor = System.Drawing.Color.Green;
                                    kimenet.Text = "✓ A kiválasztott vonat menesztésére való\nfelhatalmazás sikeresen megtörtént!";
                                    visszamondas.Text = "Rendelkező pajtás!\nA(z) " + vonat.SelectedItem.ToString() + " számú vonat meneszthető\na(z) " + vg.SelectedItem.ToString() + " vágányról.\nVettem!";

                                    allomas.IndulasiSzukseges.Add(vagany1.Vonat);
                                    allomas.IndulasiSzuksegesIdo.Add(Program.Ido);
                                    allomas.IndulasiTimerStart();
                                    vagany1jelzoK.Menesztes = true;
                                    allomas.AllomasLogTextAdd("Menesztes?" + vagany1.Vonat.Vonatszam + "?" + vagany1jelzoK.Name);

                                    seged = true;
                                    vonat.SelectedIndex = -1;
                                    vg.SelectedIndex = -1;
                                    seged = false;

                                    button1.Enabled = false;
                                    meneszthetoVonatok.Clear();
                                    listClearelheto = false;
                                    vonat.Items.Clear();
                                    for (int i = 0; i < allomas.Vonatok.Count; i++)
                                    {
                                        if ((allomas.Vonatok[i].Szakasz == vagany2 || allomas.Vonatok[i].Szakasz == vagany1) && allomas.Vonatok[i].Menesztett == false) //vagyis az állomáson van és nem menesztett
                                        {
                                            meneszthetoVonatok.Add(allomas.Vonatok[i]);
                                            vonat.Items.Add(allomas.Vonatok[i].Vonatszam);
                                            listClearelheto = true;
                                        }
                                    }
                                }
                                else
                                {
                                    //vörösbe menesztés vagy a kij. váltó állása nem megfelelő vagy nincs lezárva
                                    kimenet.BackColor = System.Drawing.Color.Red;
                                    ind.Hibak[20]++;
                                    if (nicnsKijaratiJelzo)
                                    {
                                        kimenet.Text = "✘ A kiválasztott vonat nem meneszthető, mert\na kijárati váltó (2) állása nem megfelelő vagy nincs lezárva!";
                                    }
                                    else
                                    {
                                        kimenet.Text = "✘ A kiválasztott vonat nem meneszthető, mert\na kijárati jelző (" + vagany1jelzoK.Name + ") továbbhaladást tiltó állásban van!";
                                    }
                                }
                            }
                            else
                            {
                                //K2_KI
                                if ((vagany2jelzoK.Szabad && nicnsKijaratiJelzo == false) || (nicnsKijaratiJelzo && vagany1egyenes && (belsoSzakaszLista[1] as CsucsSzakasz).Valto.Allas == false && (belsoSzakaszLista[1] as CsucsSzakasz).Valto.Lezart) || (nicnsKijaratiJelzo && vagany1egyenes == false && (belsoSzakaszLista[1] as CsucsSzakasz).Valto.Allas == true && (belsoSzakaszLista[1] as CsucsSzakasz).Valto.Lezart))
                                {
                                    vagany2.Vonat.Menesztett = true;
                                    kimenet.BackColor = System.Drawing.Color.Green;
                                    kimenet.Text = "✓ A kiválasztott vonat menesztésére való\nfelhatalmazás sikeresen megtörtént!";
                                    visszamondas.Text = "Rendelkező pajtás!\nA(z) " + vonat.SelectedItem.ToString() + " számú vonat meneszthető\na(z) " + vg.SelectedItem.ToString() + " vágányról.\nVettem!";

                                    allomas.IndulasiSzukseges.Add(vagany2.Vonat);
                                    allomas.IndulasiSzuksegesIdo.Add(Program.Ido);
                                    allomas.IndulasiTimerStart();
                                    vagany2jelzoK.Menesztes = true;
                                    allomas.AllomasLogTextAdd("Menesztes?" + vagany2.Vonat.Vonatszam + "?" + vagany2jelzoK.Name);

                                    seged = true;
                                    vonat.SelectedIndex = -1;
                                    vg.SelectedIndex = -1;
                                    seged = false;

                                    button1.Enabled = false;
                                    meneszthetoVonatok.Clear();
                                    listClearelheto = false;
                                    vonat.Items.Clear();
                                    for (int i = 0; i < allomas.Vonatok.Count; i++)
                                    {
                                        if ((allomas.Vonatok[i].Szakasz == vagany2 || allomas.Vonatok[i].Szakasz == vagany1) && allomas.Vonatok[i].Menesztett == false) //vagyis az állomáson van és nem menesztett
                                        {
                                            meneszthetoVonatok.Add(allomas.Vonatok[i]);
                                            vonat.Items.Add(allomas.Vonatok[i].Vonatszam);
                                            listClearelheto = true;
                                        }
                                    }
                                }
                                else
                                {
                                    //vörösbe menesztés vagy a kij. váltó állása nem megfelelő vagy nincs lezárva
                                    kimenet.BackColor = System.Drawing.Color.Red;
                                    ind.Hibak[20]++;
                                    if (nicnsKijaratiJelzo)
                                    {
                                        kimenet.Text = "✘ A kiválasztott vonat nem meneszthető, mert\na kijárati váltó (2) állása nem megfelelő vagy nincs lezárva!";
                                    }
                                    else
                                    {
                                        kimenet.Text = "✘ A kiválasztott vonat nem meneszthető, mert\na kijárati jelző (" + vagany2jelzoK.Name + ") továbbhaladást tiltó állásban van!";
                                    }
                                }
                            }
                        }
                        else
                        {//nincs engedély >> vagy nincs engedélyben vagy engedelyben2 de engedelyben még nem érkezet meg
                            kimenet.BackColor = System.Drawing.Color.Red;
                            if ((allomas.Szakaszok[0] as Allomaskoz).Engedelyben2 != null && (allomas.Szakaszok[0] as Allomaskoz).Engedelyben2.Vonatszam == vonat.SelectedItem.ToString() && !allomas.FogadottVonatok.Contains((allomas.Szakaszok[0] as Allomaskoz).Engedelyben))
                            {//szembemenesztés
                                kimenet.Text = "✘ A kiválasztott vonat nem meneszthető,\nmert a(z) " + (allomas.Szakaszok[0] as Allomaskoz).Engedelyben.Vonatszam + " számú vonat éppen szembeközlekedik vele.";
                                ind.Hibak[19]++;
                            }
                            else
                            {//nincs engedély
                                kimenet.Text = "✘ A kiválasztott vonat nem meneszthető,\nmert nincs engedélye " + Index.GetSzomszedAllomasNev(allomas.Name, true) + " állomástól.";
                                ind.Hibak[18]++;
                            }
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void vonat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (vonat.SelectedIndex != -1 && vg.SelectedIndex != -1)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
            if (seged == false)
            {
                kimenet.BackColor = SystemColors.Control;
                kimenet.Text = "";
                visszamondas.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listClearelheto)
            {
                meneszthetoVonatok.Clear();
            }
            DialogResult = DialogResult.OK;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Ha nem találsz valamit, annak az oka a következő lehet:\n*Csak olyan vonat menesztésére tudsz felhatalmazást adni, amely már rajta van az egyik állomási vágányon.", "Segítség", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
