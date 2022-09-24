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
    public partial class Valtokezelo : Form
    {
        bool vk1;
        List<Szakasz> belsoSzakaszLista;
        ValtKez valtKez;
        ValtKez masik;
        Allomas allomas;
        bool szechenyihegy = false;
        /// <summary>
        /// Értéke megadja, hogy a form bezárása után megnyissa-e a Körüljárás formot.
        /// </summary>
        string koruljaras = "";
        

        public Valtokezelo(Allomas allomasBE, bool vk1BEMENET, List<Szakasz> szakaszLista, ValtKez valtKezBEMENET, ValtKez _MASIK_valtKez)
        {
            InitializeComponent();
            vk1 = vk1BEMENET;
            belsoSzakaszLista = szakaszLista;
            valtKez = valtKezBEMENET;
            masik = _MASIK_valtKez;
            allomas = allomasBE;
        }
        public Valtokezelo(Allomas allomasBE, bool vk1BEMENET, List<Szakasz> szakaszLista, ValtKez valtKezBEMENET, ValtKez _MASIK_valtKez, bool szechenyihegy)
        {
            InitializeComponent();
            vk1 = vk1BEMENET;
            belsoSzakaszLista = szakaszLista;
            valtKez = valtKezBEMENET;
            masik = _MASIK_valtKez;
            this.szechenyihegy = szechenyihegy;
            allomas = allomasBE;
        }

        private void Valtokezelo_Load(object sender, EventArgs e)//load
        {       
            if (vk1)
            {
                this.Text = "Váltókezelő 1.";
                label2.Text = "Váltókezelő 1. pajtás!";
            }
            else
        	{
                this.Text = "Váltókezelő 2.";
                label2.Text = "Váltókezelő 2. pajtás!";
	        }

            if (valtKez.Be != null)
            {//bejárati vgút már van, csak ellenvonatos kijárat rendelhető el
                ellenvRB.Checked = true;
                ellenvRB.Enabled = true;
                bejRB.Enabled = false;
                kijRB.Enabled = false;

                ellenv_ellenvonat.Items.Add(valtKez.Be);
                ellenv_ellenvonat.SelectedIndex = 0;
                ellenv_ellenvonat.Enabled = false;

                ellenv_vg.Items.Clear();
                ellenv_vg.Items.Add("I.");
                ellenv_vg.Items.Add("II.");
            }
            else
            {
                ellenvRB.Enabled = false;

                bej_vg.Items.Clear();
                bej_vg.Items.Add("I.");
                bej_vg.Items.Add("II.");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)//listák feltöltése
        {
            if (bejRB.Checked)
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;

                kij_ora.Value = 0;
                kij_perc.Value = 0;
                kij_vg.SelectedIndex = -1;
                kij_vonat.SelectedIndex = -1;

                ellenv_ellenvonat.SelectedIndex = -1;
                ellenv_ora.Value = 0;
                ellenv_perc.Value = 0;
                ellenv_vg.SelectedIndex = -1;
                ellenv_vonat.SelectedIndex = -1;

                bej_vg.Items.Clear();
                bej_vonat.Items.Clear();

                //bej_vonat feltöltése
                if (vk1)
                {//vk1: [6], páratlan
                    if (belsoSzakaszLista[6].Vonat != null && Convert.ToInt32(belsoSzakaszLista[6].Vonat.Vonatszam) % 2 == 1)
                    {//az állközben van vonat, páratlan >> add
                        bej_vonat.Items.Add(belsoSzakaszLista[6].Vonat);
                    }
                    else
                    {//engedélyben lévő vonat vizsgálata
                        if ((belsoSzakaszLista[6] as Allomaskoz).Engedelyben != null && Convert.ToInt32((belsoSzakaszLista[6] as Allomaskoz).Engedelyben.Vonatszam) % 2 == 1)
                        {
                            bej_vonat.Items.Add((belsoSzakaszLista[6] as Allomaskoz).Engedelyben);
                        }
                        if ((belsoSzakaszLista[6] as Allomaskoz).Engedelyben2 != null && Convert.ToInt32((belsoSzakaszLista[6] as Allomaskoz).Engedelyben2.Vonatszam) % 2 == 1)
                        {
                            bej_vonat.Items.Add((belsoSzakaszLista[6] as Allomaskoz).Engedelyben2);
                        }
                    }
                }
                else
                {//vk2: [0], páros
                    if (belsoSzakaszLista[0].Vonat != null && Convert.ToInt32(belsoSzakaszLista[0].Vonat.Vonatszam) % 2 == 0)
                    {//az állközben van vonat, páros >> add
                        bej_vonat.Items.Add(belsoSzakaszLista[0].Vonat);
                    }
                    else
                    {
                        if ((belsoSzakaszLista[0] as Allomaskoz).Engedelyben != null && Convert.ToInt32((belsoSzakaszLista[0] as Allomaskoz).Engedelyben.Vonatszam) % 2 == 0)
                        {
                            bej_vonat.Items.Add((belsoSzakaszLista[0] as Allomaskoz).Engedelyben);
                        }
                        if ((belsoSzakaszLista[0] as Allomaskoz).Engedelyben2 != null && Convert.ToInt32((belsoSzakaszLista[0] as Allomaskoz).Engedelyben2.Vonatszam) % 2 == 0)
                        {
                            bej_vonat.Items.Add((belsoSzakaszLista[0] as Allomaskoz).Engedelyben2);
                        }
                    }
                }

                bej_vg.Items.Clear();
                bej_vg.Items.Add("I.");
                bej_vg.Items.Add("II.");
            }
            if (kijRB.Checked)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = true;
                groupBox3.Enabled = false;

                bej_ora.Value = 0;
                bej_perc.Value = 0;
                bej_vg.SelectedIndex = -1;
                bej_vonat.SelectedIndex = -1;

                ellenv_ellenvonat.SelectedIndex = -1;
                ellenv_ora.Value = 0;
                ellenv_perc.Value = 0;
                ellenv_vg.SelectedIndex = -1;
                ellenv_vonat.SelectedIndex = -1;

                kij_vg.Items.Clear();
                kij_vonat.Items.Clear();
                                
                if (vk1)
                {//páros vonatok (kijárat)
                    if (belsoSzakaszLista[3].Vonat != null && Convert.ToInt32(belsoSzakaszLista[3].Vonat.Vonatszam) % 2 == 0)
                    {
                        kij_vonat.Items.Add(belsoSzakaszLista[3].Vonat);
                    }
                    if (belsoSzakaszLista[8].Vonat != null && Convert.ToInt32(belsoSzakaszLista[8].Vonat.Vonatszam) % 2 == 0)
                    {
                        kij_vonat.Items.Add(belsoSzakaszLista[8].Vonat);
                    }
                    if (kij_vonat.Items.Count == 0)
                    {
                        if (belsoSzakaszLista[2].Vonat != null && Convert.ToInt32(belsoSzakaszLista[2].Vonat.Vonatszam) % 2 == 0)
                        {
                            kij_vonat.Items.Add(belsoSzakaszLista[2].Vonat);
                        }
                        if (belsoSzakaszLista[7].Vonat != null && Convert.ToInt32(belsoSzakaszLista[7].Vonat.Vonatszam) % 2 == 0)
                        {
                            kij_vonat.Items.Add(belsoSzakaszLista[7].Vonat);
                        }
                    }
                    if (kij_vonat.Items.Count == 0)
                    {
                        if (belsoSzakaszLista[1].Vonat != null && Convert.ToInt32(belsoSzakaszLista[1].Vonat.Vonatszam) % 2 == 0)
                        {
                            kij_vonat.Items.Add(belsoSzakaszLista[1].Vonat);
                        }
                    }
                    if (kij_vonat.Items.Count == 0)
                    {
                        if (belsoSzakaszLista[0].Vonat != null && Convert.ToInt32(belsoSzakaszLista[0].Vonat.Vonatszam) % 2 == 0) // && (masik.Be != null && masik.Ki == null || masik.Be != null && masik.Bejarat) --- ez nem tudom, mi volt itt :S
                        {
                            kij_vonat.Items.Add(belsoSzakaszLista[0].Vonat);
                        }
                    }
                    if (kij_vonat.Items.Count == 0)
                    {//engedélyben lévő vonat
                        if ((belsoSzakaszLista[0] as Allomaskoz).Engedelyben != null && (belsoSzakaszLista[0] as Allomaskoz).Engedelyben.Paros)
                        {
                            kij_vonat.Items.Add((belsoSzakaszLista[0] as Allomaskoz).Engedelyben);
                        }
                        if ((belsoSzakaszLista[0] as Allomaskoz).Engedelyben2 != null && (belsoSzakaszLista[0] as Allomaskoz).Engedelyben2.Paros)
                        {
                            kij_vonat.Items.Add((belsoSzakaszLista[0] as Allomaskoz).Engedelyben2);
                        }
                    }
                }
                else
                {//páratlan vonatok (kijárat)
                    if (belsoSzakaszLista[3].Vonat != null && Convert.ToInt32(belsoSzakaszLista[3].Vonat.Vonatszam) % 2 == 1)
                    {
                        kij_vonat.Items.Add(belsoSzakaszLista[3].Vonat);
                    }
                    if (belsoSzakaszLista[8].Vonat != null && Convert.ToInt32(belsoSzakaszLista[8].Vonat.Vonatszam) % 2 == 1)
                    {
                        kij_vonat.Items.Add(belsoSzakaszLista[8].Vonat);
                    }
                    if (kij_vonat.Items.Count == 0)
                    {
                        if (belsoSzakaszLista[4].Vonat != null && Convert.ToInt32(belsoSzakaszLista[4].Vonat.Vonatszam) % 2 == 1)
                        {
                            kij_vonat.Items.Add(belsoSzakaszLista[4].Vonat);
                        }
                        if (belsoSzakaszLista[9].Vonat != null && Convert.ToInt32(belsoSzakaszLista[9].Vonat.Vonatszam) % 2 == 1)
                        {
                            kij_vonat.Items.Add(belsoSzakaszLista[9].Vonat);
                        }
                    }
                    if (kij_vonat.Items.Count == 0)
                    {
                        if (belsoSzakaszLista[5].Vonat != null && Convert.ToInt32(belsoSzakaszLista[5].Vonat.Vonatszam) % 2 == 1)
                        {
                            kij_vonat.Items.Add(belsoSzakaszLista[5].Vonat);
                        }
                    }
                    if (kij_vonat.Items.Count == 0)
                    {
                        if (belsoSzakaszLista[6].Vonat != null && Convert.ToInt32(belsoSzakaszLista[6].Vonat.Vonatszam) % 2 == 1)
                        {
                            kij_vonat.Items.Add(belsoSzakaszLista[6].Vonat);
                        }
                    }
                    if (kij_vonat.Items.Count == 0)
                    {//engedélyben lévő vonat
                        if ((belsoSzakaszLista[6] as Allomaskoz).Engedelyben != null && !(belsoSzakaszLista[6] as Allomaskoz).Engedelyben.Paros)
                        {
                            kij_vonat.Items.Add((belsoSzakaszLista[6] as Allomaskoz).Engedelyben);
                        }
                        if ((belsoSzakaszLista[6] as Allomaskoz).Engedelyben2 != null && !(belsoSzakaszLista[6] as Allomaskoz).Engedelyben2.Paros)
                        {
                            kij_vonat.Items.Add((belsoSzakaszLista[6] as Allomaskoz).Engedelyben2);
                        }
                    }
                }
                kij_vg.Items.Clear();
                kij_vg.Items.Add("I.");
                kij_vg.Items.Add("II.");
            }
            if (ellenvRB.Checked)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = true;

                bej_ora.Value = 0;
                bej_perc.Value = 0;
                bej_vg.SelectedIndex = -1;
                bej_vonat.SelectedIndex = -1;

                kij_ora.Value = 0;
                kij_perc.Value = 0;
                kij_vg.SelectedIndex = -1;
                kij_vonat.SelectedIndex = -1;

                if (vk1)
                {//1-es >> kijárat >> páros
                    if (belsoSzakaszLista[3].Vonat != null && belsoSzakaszLista[3].Vonat.Paros)
                    {
                        ellenv_vonat.Items.Add(belsoSzakaszLista[3].Vonat);
                    }
                    if (belsoSzakaszLista[8].Vonat != null && belsoSzakaszLista[8].Vonat.Paros)
                    {
                        ellenv_vonat.Items.Add(belsoSzakaszLista[8].Vonat);
                    }
                    if (ellenv_vonat.Items.Count == 0)
	                {
                        if (belsoSzakaszLista[2].Vonat != null && belsoSzakaszLista[2].Vonat.Paros)
                        {
                            ellenv_vonat.Items.Add(belsoSzakaszLista[2].Vonat);
                        }
                        if (belsoSzakaszLista[7].Vonat != null && belsoSzakaszLista[7].Vonat.Paros)
                        {
                            ellenv_vonat.Items.Add(belsoSzakaszLista[7].Vonat);
                        }
	                }
                    if (ellenv_vonat.Items.Count == 0)
                    {
                        if (belsoSzakaszLista[1].Vonat != null && belsoSzakaszLista[1].Vonat.Paros)
                        {
                            ellenv_vonat.Items.Add(belsoSzakaszLista[1].Vonat);
                        }
                    }
                    if (ellenv_vonat.Items.Count == 0)
                    {
                        if (belsoSzakaszLista[0].Vonat != null && belsoSzakaszLista[0].Vonat.Paros)
                        {
                            ellenv_vonat.Items.Add(belsoSzakaszLista[0].Vonat);
                        }
                    }
                    if (allomas.Vegallomas && valtKez.Be != null)
                    {//A
                        if (valtKez.Be.FordaIndexOfVonatszam() != valtKez.Be.Forda.Length - 1)
                        {//fordul még a vonat
                            if ((allomas.KoruljarasElrendelve != null && allomas.KoruljarasElrendelve.Vonatszam == valtKez.Be.Vonatszam) || !valtKez.Be.KoruljarasSzukseges)
                            {//a bejáró vonat körüljárása el van rendelve || nem kell körüljárnia >> elrendelhető a forduló vonat ellenvonatos kijárata
                                if ((allomas.Szakaszok[6] as Allomaskoz).Engedelyben2 != null && (allomas.Szakaszok[6] as Allomaskoz).Engedelyben2.Virtualis)
                                {//eng2 már egy virtuális vonat >> annak rendeljük el
                                    ellenv_vonat.Items.Add((allomas.Szakaszok[6] as Allomaskoz).Engedelyben2);
                                }
                                else
                                {//new instance
                                    Vonat v = new Vonat(true) { Vonatszam = valtKez.Be.Forda[valtKez.Be.FordaIndexOfVonatszam() + 1], Virtualis = true };
                                    ellenv_vonat.Items.Add(v);
                                }
                            }
                        }
                    }
                }
                else
                {//2-es >> kijárat >> páratlan
                    if (belsoSzakaszLista[3].Vonat != null && !belsoSzakaszLista[3].Vonat.Paros)
                    {
                        ellenv_vonat.Items.Add(belsoSzakaszLista[3].Vonat);
                    }
                    if (belsoSzakaszLista[8].Vonat != null && !belsoSzakaszLista[8].Vonat.Paros)
                    {
                        ellenv_vonat.Items.Add(belsoSzakaszLista[8].Vonat);
                    }
                    if (ellenv_vonat.Items.Count == 0)
                    {
                        if (belsoSzakaszLista[4].Vonat != null && !belsoSzakaszLista[4].Vonat.Paros)
                        {
                            ellenv_vonat.Items.Add(belsoSzakaszLista[4].Vonat);
                        }
                        if (belsoSzakaszLista[9].Vonat != null && !belsoSzakaszLista[9].Vonat.Paros)
                        {
                            ellenv_vonat.Items.Add(belsoSzakaszLista[9].Vonat);
                        }
                    }
                    if (ellenv_vonat.Items.Count == 0)
                    {
                        if (belsoSzakaszLista[5].Vonat != null && !belsoSzakaszLista[5].Vonat.Paros)
                        {
                            ellenv_vonat.Items.Add(belsoSzakaszLista[5].Vonat);
                        }
                    }
                    if (ellenv_vonat.Items.Count == 0)
                    {
                        if (belsoSzakaszLista[6].Vonat != null && !belsoSzakaszLista[6].Vonat.Paros)
                        {
                            ellenv_vonat.Items.Add(belsoSzakaszLista[6].Vonat);
                        }
                    }
                    if (allomas.Vegallomas && valtKez.Be != null)
                    {//O
                        if (valtKez.Be.FordaIndexOfVonatszam() != valtKez.Be.Forda.Length - 1)
                        {//fordul még a vonat
                            if ((allomas.KoruljarasElrendelve != null && allomas.KoruljarasElrendelve.Vonatszam == valtKez.Be.Vonatszam) || !valtKez.Be.KoruljarasSzukseges)
                            {//a bejáró vonat körüljárása el van rendelve || nem kell körüljárnia >> elrendelhető a forduló vonat ellenvonatos kijárata
                                if ((allomas.Szakaszok[0] as Allomaskoz).Engedelyben2 != null && (allomas.Szakaszok[0] as Allomaskoz).Engedelyben2.Virtualis)
                                {//eng2 már egy virtuális vonat >> annak rendeljük el
                                    ellenv_vonat.Items.Add((allomas.Szakaszok[0] as Allomaskoz).Engedelyben2);
                                }
                                else
                                {//new instance
                                    Vonat v = new Vonat(true) { Vonatszam = valtKez.Be.Forda[valtKez.Be.FordaIndexOfVonatszam() + 1], Virtualis = true };
                                    ellenv_vonat.Items.Add(v);
                                }
                            }
                        }                        
                    }
                }
            }
            kimenet.BackColor = SystemColors.Control;
            kimenet.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)//elrendelés
        {
            valtKez.Timer.Interval = 6000 / Gyermekvasút.Modellek.Settings.SebessegOszto;

            visszamondas.Hide();
            if (bejRB.Checked)
            {
                int vg = 0;
                if (bej_vg.SelectedIndex != -1) vg = bej_vg.SelectedItem.ToString() == "I." ? 1 : 2;
                    
                if (bej_vg.SelectedIndex == -1 || bej_vonat.SelectedIndex == -1)
                {
                    kimenet.BackColor = Color.Red;
                    kimenet.Text = "✘ Adj meg egy vonatot és egy vágányszámot!";
                }
                else if (((masik.Vagany == 2 && bej_vg.SelectedItem.ToString() == "II.") || (masik.Vagany == 1 && bej_vg.SelectedItem.ToString() == "I.")) && masik.Be != null)
                {//szembe bejárat
                    kimenet.BackColor = Color.Red;
                    kimenet.Text = "✘ Egy vágányra csak egy bejárati vágányút beállítása rendelhető el!";
                }
                else if ((vk1 && vg == 1 && (belsoSzakaszLista[3].Foglalt || belsoSzakaszLista[9].Foglalt || belsoSzakaszLista[4].Foglalt || belsoSzakaszLista[5].Foglalt)) || (vk1 && vg == 2 && (belsoSzakaszLista[4].Foglalt || belsoSzakaszLista[5].Foglalt || belsoSzakaszLista[8].Foglalt || belsoSzakaszLista[9].Foglalt)) || (!vk1 && vg == 1 && (belsoSzakaszLista[7].Foglalt || belsoSzakaszLista[1].Foglalt || belsoSzakaszLista[2].Foglalt || belsoSzakaszLista[3].Foglalt)) || (!vk1 && vg == 2 && (belsoSzakaszLista[2].Foglalt || belsoSzakaszLista[1].Foglalt || belsoSzakaszLista[7].Foglalt || belsoSzakaszLista[8].Foglalt)))
                {//foglaltra elrendelés
                    kimenet.BackColor = Color.Red;
                    kimenet.Text = "✘ A bejárati vágányútban vonat található! Ne feledkezz meg a vágányút-ellenőrzésről!";
                }
                else
                {
                    valtKez.Be = (Vonat)bej_vonat.SelectedItem;
                    valtKez.Ki = null;
                    if (bej_vg.SelectedItem.ToString() == "I.")
                    {
                        valtKez.Vagany = 1;
                    }
                    else
                    {
                        valtKez.Vagany = 2;
                    }
                    kimenet.BackColor = Color.Green;
                    kimenet.Text = "✓ A kiválasztott vonat vágányútjának beállításának elrendelése sikeresen megtörtént!";
                    visszamondas.Show();
                    visszamondas.Text = "Rendelkező pajtás!\nA(z) " + bej_vonat.SelectedItem.ToString() + " számú vonat kb. " + bej_ora.Value + " óra " + bej_perc.Value + " perckor bejár a(z) " + bej_vg.SelectedItem.ToString() + " vágányra.\nVettem!";
                    allomas.AllomasLogTextAdd("ElrendelesBe?" + bej_vonat.SelectedItem.ToString());

                    groupBox1.Enabled = false;
                    bejRB.Enabled = false;
                    kijRB.Enabled = false;
                    ellenvRB.Enabled = false;
                    button1.Enabled = false;

                    valtKez.ProgressBar = 0;
                    if (valtKez.SzechenyiHegy == false)
                    {
                        valtKez.Timer.Start();
                        valtKez.Valto.AllitasAlatt = true;
                    }

                    //sikeres bejárat elrendelés >> körüljárás?
                    // HA másikVG.Vonat != null ? Allomas.KoruljarasElrendelesSzukseges SET : this.Koruljaras SET
                    if ((bej_vonat.SelectedItem as Vonat).KoruljarasSzukseges)
                    {
                        if (allomas.Vonatok.Count == 0 || (allomas.Vonatok.Count == 1 && allomas.Vonatok[0].Vonatszam == bej_vonat.SelectedItem.ToString()))
                        {
                            koruljaras = bej_vonat.SelectedItem.ToString();
                        }
                        else
                        {
                            allomas.KoruljarasElrendelesSzuksegesVonatszam = bej_vonat.SelectedItem.ToString();
                        }
                    }
                }
            }
            else if (kijRB.Checked)
            {
                if (kij_vg.SelectedIndex == -1 || kij_vonat.SelectedIndex == -1)
                {
                    kimenet.BackColor = Color.Red;
                    kimenet.Text = "✘ Adj meg egy vonatot és egy vágányszámot!";
                }
                else if ((!szechenyihegy && ((kij_vg.SelectedItem.ToString() == "I." && (kij_vonat.SelectedItem as Vonat).Szakasz != belsoSzakaszLista[3]) || (kij_vg.SelectedItem.ToString() == "II." && (kij_vonat.SelectedItem as Vonat).Szakasz != belsoSzakaszLista[8])) && masik.Be != (kij_vonat.SelectedItem as Vonat)) || (szechenyihegy && ((kij_vg.SelectedItem.ToString() == "I." && (kij_vonat.SelectedItem as Vonat).Szakasz != belsoSzakaszLista[8]) || (kij_vg.SelectedItem.ToString() == "II." && (kij_vonat.SelectedItem as Vonat).Szakasz != belsoSzakaszLista[3])) && masik.Be != (kij_vonat.SelectedItem as Vonat)) || (masik.Be == (kij_vonat.SelectedItem as Vonat) && masik.VaganyString != kij_vg.SelectedItem.ToString()))
                {//a kiválasztott vágányon nincs ott a kiválasztott vonat vagy a másik váltókezelő bejáratának a vágánya nem a kiválasztott vágány
                    kimenet.BackColor = Color.Red;
                    kimenet.Text = "✘ A kiválasztott vágányon nem található a kiválasztott vonat vagy\na kiválasztott vonat bejárati vágányútja nem a kiválasztott vágányra van elrendelve!";
                }
                else if (valtKez.Valto.SzakaszE.Foglalt || valtKez.Valto.SzakaszK.Foglalt || (vk1 && belsoSzakaszLista[5].Foglalt) || (!vk1 && belsoSzakaszLista[1].Foglalt))
                {//van vonat a vgútban
                    kimenet.BackColor = Color.Red;
                    kimenet.Text = "✘ A kijárati vágányútban vonat található! Ne feledkezz meg a vágányút-ellenőrzésről!";
                }
                else if ((vk1 && (belsoSzakaszLista[6] as Allomaskoz).Engedelyben != null && !(belsoSzakaszLista[6] as Allomaskoz).Engedelyben.Paros && !allomas.FogadottVonatok.Contains((belsoSzakaszLista[6] as Allomaskoz).Engedelyben)) || (!vk1 && (belsoSzakaszLista[0] as Allomaskoz).Engedelyben != null && (belsoSzakaszLista[0] as Allomaskoz).Engedelyben.Paros && !allomas.FogadottVonatok.Contains((belsoSzakaszLista[0] as Allomaskoz).Engedelyben)))
                {//engedélyben van egy szembe vonat, ami még nem érkezett meg
                    kimenet.BackColor = Color.Red;
                    kimenet.Text = "✘ Már engedélyt adtál egy vonatnak, amely még nem érkezett meg az állomásodra,\nvagyis van útban ellenvonat. Először rendeld el a bejárati vágányút beállítását,\r\nmajd használd a másik (ellenvonatos) szöveget!";
                }
                else if ((vk1 && (belsoSzakaszLista[6] as Allomaskoz).Engedelyben != null && (belsoSzakaszLista[6] as Allomaskoz).Engedelyben.Paros && (belsoSzakaszLista[6] as Allomaskoz).Engedelyben.Vonatszam != kij_vonat.SelectedItem.ToString() && (belsoSzakaszLista[6] as Allomaskoz).Engedelyben != (belsoSzakaszLista[6] as Allomaskoz).Vonat) || (!vk1 && (belsoSzakaszLista[0] as Allomaskoz).Engedelyben != null && !(belsoSzakaszLista[0] as Allomaskoz).Engedelyben.Paros && (belsoSzakaszLista[0] as Allomaskoz).Engedelyben.Vonatszam != kij_vonat.SelectedItem.ToString() && (belsoSzakaszLista[0] as Allomaskoz).Engedelyben != (belsoSzakaszLista[0] as Allomaskoz).Vonat))
                {//engedelyben != null && én indítom irányú && nem annak rednelem el éppen
                    //két azonos irányú vonat van az állomáson - esetben
                    //az egyik engedélyben van, de a másiknak rendeli el a kijáratát -- ez csak akkor baj, ha az engedélyben lévő még nem indult el
                    kimenet.BackColor = Color.Red;
                    kimenet.Text = "✘ Már engedélyt kértél a szomszéd állomástól egy vonat indítására, amelyet még\nnem indítottál el, most mégis egy másik vonat kijárati vágányútját szeretnéd elrendelni.";
                }
                else
                {
                    valtKez.Ki = (Vonat)kij_vonat.SelectedItem;
                    valtKez.Be = null;
                    if (kij_vg.SelectedItem.ToString() == "I.")
                    {
                        valtKez.Vagany = 1;
                    }
                    else
                    {
                        valtKez.Vagany = 2;
                    }
                    kimenet.BackColor = Color.Green;
                    kimenet.Text = "✓ A kiválasztott vonat vágányútjának beállításának elrendelése sikeresen megtörtént!";
                    visszamondas.Show();
                    visszamondas.Text = "Rendelkező pajtás!\nA(z) " + kij_vonat.SelectedItem.ToString() + " számú vonat kb. " + kij_ora.Value + " óra " + kij_perc.Value + " perckor kijár a(z) " + kij_vg.SelectedItem.ToString() + " vágányról. Ellenvonat útban nincs.\nVettem!";
                    allomas.AllomasLogTextAdd("ElrendelesKi?" + kij_vonat.SelectedItem.ToString());

                    groupBox2.Enabled = false;
                    bejRB.Enabled = false;
                    kijRB.Enabled = false;
                    ellenvRB.Enabled = false;
                    button1.Enabled = false;

                    valtKez.ProgressBar = 0;
                    if (valtKez.SzechenyiHegy == false)
                    {
                        valtKez.Timer.Start();
                        valtKez.Valto.AllitasAlatt = true;
                    }

                    //körüljárás -- ha van körüljáratandó, beteszi azt, ha nincs, üresen ahgyja
                    koruljaras = allomas.KoruljarasElrendelesSzuksegesVonatszam;
                }
            }
            else if (ellenvRB.Checked)
            {
                if (ellenv_vg.SelectedIndex == -1 || ellenv_vonat.SelectedIndex == -1 || ellenv_ellenvonat.SelectedIndex == -1)
                {
                    kimenet.BackColor = Color.Red;
                    kimenet.Text = "✘ Adj meg egy vonatot és egy vágányszámot!";
                    return;
                }

                #region SEGÉDVÁLTOZÓ
                                string seged_KijaratVaganyString = "";
                                if ((ellenv_vonat.SelectedItem as Vonat).Szakasz == belsoSzakaszLista[3])
                                {//E vágányon van
                                    if (szechenyihegy)
                                    {
                                        seged_KijaratVaganyString = "II.";
                                    }
                                    else
                                    {
                                        seged_KijaratVaganyString = "I.";
                                    }
                                }
                                else if ((ellenv_vonat.SelectedItem as Vonat).Szakasz == belsoSzakaszLista[8])
                                {//K vágányon
                                    if (szechenyihegy)
                                    {
                                        seged_KijaratVaganyString = "I.";
                                    }
                                    else
                                    {
                                        seged_KijaratVaganyString = "II.";
                                    }
                                }
                                else
                                {
                                    seged_KijaratVaganyString = "NINCS ÁLLOMÁSI VÁGÁNYON";
                                }
                #endregion
                                
                if (allomas.FogadottVonatok.Contains(ellenv_vonat.SelectedItem as Vonat) && seged_KijaratVaganyString != ellenv_vg.SelectedItem.ToString())
                {//már megérkezett az állomásra, de nem arról a vgról rendelik el, ahol van
                    kimenet.BackColor = Color.Red;
                    kimenet.Text = "✘ A kiválasztott vágányon nem található a kiválasztott vonat!";
                }
                else if ((ellenv_vonat.SelectedItem as Vonat).Virtualis == false && !allomas.FogadottVonatok.Contains(ellenv_vonat.SelectedItem as Vonat) && masik.VaganyString != ellenv_vg.SelectedItem.ToString())
                {//még nem érkezett meg az állomásra, és nem arról a vgról rendelik el, amelyikre el van rendelve
                    kimenet.BackColor = Color.Red;
                    kimenet.Text = "✘ A kiválasztott vonat bejárati vágányútja nem a kiválasztott vágányra van elrendelve!";
                }                
                else if ((!vk1 && (allomas.Szakaszok[0] as Allomaskoz).Engedelyben2 != null && !(allomas.Szakaszok[0] as Allomaskoz).Engedelyben2.Virtualis) || (vk1 && (allomas.Szakaszok[6] as Allomaskoz).Engedelyben2 != null && !(allomas.Szakaszok[6] as Allomaskoz).Engedelyben2.Virtualis))
                {//ha már egy másik vonatnak kért engedélyt ha itt-tel (végállomási kereszt)
                 //ha eng2 egy nem virtuális, létező vonat, akkor annak kéne elrendelnie
                    kimenet.BackColor = Color.Red;
                    kimenet.Text = "✘ Már engedélyt kértél a szomszéd állomástól (Ha itt...-tel) egy vonat indítására,\nmost mégis egy másik vonat kijárati vágányútját szeretnéd elrendelni.";
                }
                else if ((ellenv_vonat.SelectedItem as Vonat).Virtualis && valtKez.VaganyString != ellenv_vg.SelectedItem.ToString())
                {//virtuális: máshonnan rendeli el a kijáratot, mint ahova a bejárat szól
                    kimenet.BackColor = Color.Red;
                    kimenet.Text = "✘ A körüljárás után kijáró vonat kijárati vágányútját arról a vágányról kell elrendelned,\nahova a bejáró vonat bejárati vágányútját elrendelted.";
                }
                else
                {
                    valtKez.Ki = (Vonat)ellenv_vonat.SelectedItem;

                    kimenet.BackColor = Color.Green;
                    kimenet.Text = "✓ A kiválasztott vonat vágányútjának beállításának elrendelése sikeresen megtörtént!";
                    visszamondas.Show();
                    visszamondas.Text = "Rendelkező pajtás!\nA(z) " + ellenv_ellenvonat.SelectedItem.ToString() + " számú vonat behaladása után a(z) " + ellenv_vonat.SelectedItem.ToString() + " számú vonat\nkb. " + ellenv_ora.Value + " óra " + bej_perc.Value + " perckor kijár a(z) " + ellenv_vg.SelectedItem.ToString() + " vágányról.\nVettem!";
                    allomas.AllomasLogTextAdd("ElrendelesEllenv?" + ellenv_vonat.SelectedItem.ToString());
                    groupBox3.Enabled = false;
                    bejRB.Enabled = false;
                    kijRB.Enabled = false;
                    ellenvRB.Enabled = false;
                    button1.Enabled = false;

                    valtKez.Bejarat = true;

                    //körüljárás -- ha van körüljáratandó, beteszi azt, ha nincs, üresen ahgyja
                    koruljaras = allomas.KoruljarasElrendelesSzuksegesVonatszam;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)//bezárás
        {
            this.Close();
        }

        private void Valtokezelo_FormClosed(object sender, FormClosedEventArgs e)//formClosed
        {
            DialogResult = DialogResult.OK;
            if (allomas.Vegallomas && koruljaras != "" && koruljaras != null)
            {
                //open körüljárás form
                Koruljaras frmKorulj = new Koruljaras(allomas, koruljaras);
                frmKorulj.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)//help
        {
            MessageBox.Show("Ha nem találsz valamit, annak az okai a következők lehetnek:\n*Csak az elölhaladó – megfelelő irányú – vonatnak a bejárati vágányútjának a beállítását tudod elrendelni.\n*Csak annak a vonatnak a kijárati vágányútjának a beállítását tudod elrendelni, amelynek a bejárati vágányútjának a beállítását már elrendelted.\n*Kijárati vágányút esetén csak arról a vágányról rendelhető el a vágányút beállítása, amelyre a bejárat el lett rendelve.\n*Ellenvonat behaladása után kijáró vonat vágányútjának elrendelésekor ellenvonatnak csak már elrendelt bejárati vágányúttal rendelkező vonat adható meg.\n\nA programban a szimuláció megkönnyítése érdekében a VALÓSÁGTÓL ELTÉRŐEN a vágányút-beállítás elrendelésének VAN legkorábbi időpontja. Csak olyan vonat bejárati vágányútjának beállítása rendelhető el, amelyik részére már engedélyt adtál.", "Segítség", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
