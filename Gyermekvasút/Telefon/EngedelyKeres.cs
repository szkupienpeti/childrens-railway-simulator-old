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
    public partial class EngedelyKeres : Form
    {
        Index ind;
        Allomas hivoAll;
        IAllomas hivandoAll;
        bool kpFele;
        List<Vonat> indithatoVonatok = new List<Vonat>();
        bool nemIksz;
        KozlemenyValaszto kozlval;

        public EngedelyKeres()
        {
            InitializeComponent();
        }

        public EngedelyKeres(Allomas hivoAll, IAllomas hivandoAll, Index ind, bool kpFele, KozlemenyValaszto kv)
        {            
            InitializeComponent();
            this.ind = ind;
            this.hivoAll = hivoAll;
            this.hivandoAll = hivandoAll;
            this.kpFele = kpFele;
            kozlval = kv;

            #region OLD
            //if (kpFele)
            //{
            //    foreach (Vonat v in hivoAll.Vonatok)
            //    {
            //        if (Convert.ToInt32(v.Vonatszam) % 2 == 1)
            //        {
            //            indithatoVonatok.Add(v);
            //        }
            //    }

            //    //COMBOBOXOK FELTÖLTÉSE ---
            //    if (hivoAll.Szakaszok[0].Vonat != null)
            //    {//fogadoAll felől épp jön egy vonat hivoAll-ra >> ellenk ir. vonat VAN útban
            //        //***ELLENKEZŐ IRÁNYÚ VONAT >>VAN<< ÚTBAN***
            //        vanEllenv.Items.Add(hivoAll.Szakaszok[0].Vonat);
            //        foreach (Vonat v in indithatoVonatok)
            //        {
            //            vanVonat.Items.Add(v);
            //        }
            //    }
            //    else
            //    {//nincs útban vonat >> utolsó vonat azonos / ellenk. ir.
            //        if ((hivoAll.Szakaszok[0] as Allomaskoz).UtolsoVonat != null)
            //        {
            //            if (Convert.ToInt32((hivoAll.Szakaszok[0] as Allomaskoz).UtolsoVonat.Vonatszam) % 2 == 1)
            //            {
            //                //utolsó vonat páratlan >> AZONOS IRÁNYÚ
            //                foreach (Vonat v in indithatoVonatok)
            //                {
            //                    azonosVonat.Items.Add(v);
            //                }
            //            }
            //            else
            //            {
            //                //utolsó vonat páros >> ELLENKEZŐ IRÁNYÚ (VOLT)
            //                voltEllenv.Items.Add((hivoAll.Szakaszok[0] as Allomaskoz).UtolsoVonat);
            //                foreach (Vonat v in indithatoVonatok)
            //                {
            //                    voltVonat.Items.Add(v);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            //nincs utolsó vonat >> AZONOS IRÁNYÚ
            //            foreach (Vonat v in indithatoVonatok)
            //            {
            //                azonosVonat.Items.Add(v);
            //            }
            //        }
            //    }
            //}
            //else
            //{//vp fele
            //    foreach (Vonat v in hivoAll.Vonatok)
            //    {
            //        if (Convert.ToInt32(v.Vonatszam) % 2 == 0)
            //        {
            //            indithatoVonatok.Add(v);
            //        }
            //    }

            //    //COMBOBOXOK FELTÖLTÉSE ---
            //    if (hivoAll.Szakaszok[6].Vonat != null)
            //    {//fogadoAll felől épp jön egy vonat hivoAll-ra >> ellenk ir. vonat VAN útban
            //        //***ELLENKEZŐ IRÁNYÚ VONAT >>VAN<< ÚTBAN***
            //        vanEllenv.Items.Add(hivoAll.Szakaszok[6].Vonat);
            //        foreach (Vonat v in indithatoVonatok)
            //        {
            //            vanVonat.Items.Add(v);
            //        }
            //    }
            //    else
            //    {//nincs útban vonat >> utolsó vonat azonos / ellenk. ir.
            //        if ((hivoAll.Szakaszok[6] as Allomaskoz).UtolsoVonat != null)
            //        {
            //            if (Convert.ToInt32((hivoAll.Szakaszok[6] as Allomaskoz).UtolsoVonat.Vonatszam) % 2 == 0)
            //            {
            //                //utolsó vonat páros >> AZONOS IRÁNYÚ
            //                foreach (Vonat v in indithatoVonatok)
            //                {
            //                    azonosVonat.Items.Add(v);
            //                }
            //            }
            //            else
            //            {
            //                //utolsó vonat páratlan >> ELLENKEZŐ IRÁNYÚ (VOLT)
            //                voltEllenv.Items.Add((hivoAll.Szakaszok[6] as Allomaskoz).UtolsoVonat);
            //                foreach (Vonat v in indithatoVonatok)
            //                {
            //                    voltVonat.Items.Add(v);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            //nincs utolsó vonat >> AZONOS IRÁNYÚ
            //            foreach (Vonat v in indithatoVonatok)
            //            {
            //                azonosVonat.Items.Add(v);
            //            }
            //        }
            //    }
            //}
            #endregion

            foreach (Vonat v in hivoAll.Vonatok)
            {
                if (!v.Koruljar && v.Paros != kpFele)
                {
                    indithatoVonatok.Add(v);
                    azonosVonat.Items.Add(v);
                    vanVonat.Items.Add(v);
                    voltVonat.Items.Add(v);
                }
            }

            if (hivoAll.Vegallomas)
            {//az engedélyben lévő vonatból forduló vonatnak is kérhessen engedélyt
                if (kpFele)
                {//O>H
                    Vonat engben = (hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben;
                    if (engben != null && engben.Forda[engben.Forda.Length - 1] != engben.Vonatszam)
                    {//még fordul
                        Vonat v = null;
                        if (hivoAll.ValtKezek[0].Ki != null && hivoAll.ValtKezek[0].Ki.Virtualis)
                        {//valtKez.Ki már a virtuális objektum
                            v = hivoAll.ValtKezek[0].Ki;
                        }
                        else
                        {//new instance
                            v = new Vonat(true) { Vonatszam = engben.Forda[engben.FordaIndexOfVonatszam() + 1], Virtualis = true };
                        }
                        
                        indithatoVonatok.Add(v);
                        azonosVonat.Items.Add(v);
                        vanVonat.Items.Add(v);
                        voltVonat.Items.Add(v);
                    }
                    //már adott visszjelt > UtolsoVonat
                    Vonat utolso = (hivoAll.Szakaszok[0] as Allomaskoz).UtolsoVonat;
                    if (utolso != null && utolso.Koruljar)
                    {//éppen körüljár
                        Vonat v = null;
                        if (hivoAll.ValtKezek[0].Ki != null && hivoAll.ValtKezek[0].Ki.Virtualis)
                        {//valtKez.Ki már a virtuális objektum
                            v = hivoAll.ValtKezek[0].Ki;
                        }
                        else
                        {//new instance
                            v = new Vonat(true) { Vonatszam = utolso.Forda[utolso.FordaIndexOfVonatszam() + 1], Virtualis = true };
                        }

                        indithatoVonatok.Add(v);
                        azonosVonat.Items.Add(v);
                        vanVonat.Items.Add(v);
                        voltVonat.Items.Add(v);
                    }
                }
                else
                {//A>U(L)
                    Vonat engben = (hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben;
                    if (engben != null && engben.Forda[engben.Forda.Length - 1] != engben.Vonatszam)
                    {//még fordul
                        Vonat v = null;
                        if (hivoAll.ValtKezek[1].Ki != null && hivoAll.ValtKezek[1].Ki.Virtualis)
                        {//valtKez.Ki már a virtuális objektum
                            v = hivoAll.ValtKezek[1].Ki;
                        }
                        else
                        {//new instance
                            v = new Vonat(true) { Vonatszam = engben.Forda[engben.FordaIndexOfVonatszam() + 1], Virtualis = true };
                        }

                        indithatoVonatok.Add(v);
                        azonosVonat.Items.Add(v);
                        vanVonat.Items.Add(v);
                        voltVonat.Items.Add(v);
                    }
                    //már adott visszjelt > UtolsoVonat
                    Vonat utolso = (hivoAll.Szakaszok[6] as Allomaskoz).UtolsoVonat;
                    if (utolso != null && utolso.Koruljar)
                    {//éppen körüljár
                        Vonat v = null;
                        if (hivoAll.ValtKezek[1].Ki != null && hivoAll.ValtKezek[1].Ki.Virtualis)
                        {//valtKez.Ki már a virtuális objektum
                            v = hivoAll.ValtKezek[1].Ki;
                        }
                        else
                        {//new instance
                            v = new Vonat(true) { Vonatszam = utolso.Forda[utolso.FordaIndexOfVonatszam() + 1], Virtualis = true };
                        }

                        indithatoVonatok.Add(v);
                        azonosVonat.Items.Add(v);
                        vanVonat.Items.Add(v);
                        voltVonat.Items.Add(v);
                    }
                }
            }

            if (kpFele)
            {
                if ((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben != null && !(hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben.Paros && !indithatoVonatok.Contains((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben))
                {//másik állomásközben engedélyben lévő megfelelő irányú vonat
                    indithatoVonatok.Add((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben);
                    azonosVonat.Items.Add((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben);
                    vanVonat.Items.Add((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben);
                    voltVonat.Items.Add((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben);
                }
                if ((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben2 != null && !(hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben2.Paros && !indithatoVonatok.Contains((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben2))
                {
                    indithatoVonatok.Add((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben2);
                    azonosVonat.Items.Add((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben2);
                    vanVonat.Items.Add((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben2);
                    voltVonat.Items.Add((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben2);
                }

                if ((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben != null && (hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben.Paros)
                    vanEllenv.Items.Add((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben);
                if ((hivoAll.Szakaszok[0] as Allomaskoz).UtolsoVonat != null && (hivoAll.Szakaszok[0] as Allomaskoz).UtolsoVonat.Paros)
                    voltEllenv.Items.Add((hivoAll.Szakaszok[0] as Allomaskoz).UtolsoVonat);
            }
            else
            {
                if ((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben != null && (hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben.Paros && !indithatoVonatok.Contains((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben))
                {//másik állomásközben engedélyben lévő megfelelő irányú vonat
                    indithatoVonatok.Add((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben);
                    azonosVonat.Items.Add((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben);
                    vanVonat.Items.Add((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben);
                    voltVonat.Items.Add((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben);
                }
                if ((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben2 != null && (hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben2.Paros && !indithatoVonatok.Contains((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben2))
                {
                    indithatoVonatok.Add((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben2);
                    azonosVonat.Items.Add((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben2);
                    vanVonat.Items.Add((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben2);
                    voltVonat.Items.Add((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben2);
                }

                if ((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben != null && !(hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben.Paros)
                    vanEllenv.Items.Add((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben);
                if ((hivoAll.Szakaszok[6] as Allomaskoz).UtolsoVonat != null && !(hivoAll.Szakaszok[6] as Allomaskoz).UtolsoVonat.Paros)
                    voltEllenv.Items.Add((hivoAll.Szakaszok[6] as Allomaskoz).UtolsoVonat);
            }
        }

        private void EngedelyKeres_Load(object sender, EventArgs e)
        {
            if (hivandoAll != null)
            {
                toolStripStatusLabel1.Text += Index.GetSzomszedAllomasNev(hivoAll.Name, kpFele);
                azonosAllomas.Text = Index.GetSzomszedAllomasNev(hivoAll.Name, kpFele) + " állomásra?";
                vanAll.Text = "számú vonat " + Index.GetSzomszedAllomasNev(hivoAll.Name, kpFele) + " állomásra?";
                voltAll.Text = "számú vonat " + Index.GetSzomszedAllomasNev(hivoAll.Name, kpFele) + " állomásra?";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAzonos.Checked)
            {
                gbAzonos.Enabled = true;
                gbVolt.Enabled = false;
                gbVan.Enabled = false;
            }
            else if (rbVolt.Checked)
            {
                gbAzonos.Enabled = false;
                gbVolt.Enabled = true;
                gbVan.Enabled = false;
            }
            else if (rbVan.Checked)
            {
                gbAzonos.Enabled = false;
                gbVolt.Enabled = false;
                gbVan.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //check
            if (rbAzonos.Checked)
            {
                if (azonosVonat.SelectedIndex == -1)
                {
                    kimenet.BackColor = Color.Red;
                    kimenet.Text = "✘ Adj meg egy vonatot!";
                    return;
                }
                //check#2 -- állomástávolságú közlekedés ellenőrzése (azonos irányú vonat nem lehet engedélyben)
                if (kpFele)
                {//az indítandó vonat a KP felé közlekedik, vagyis PÁRATLAN számú
                    if ((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben != null && (hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben.Paros == false)
                    {
                        ind.Hibak[15]++;
                        MessageBox.Show("Állomástávolságú követési rend esetén két szomszédos (nyitva tartó) állomás között egy időben csak egy vonat lehet útban.\nA követő vonat (" + (azonosVonat.SelectedItem as Vonat).Vonatszam + ") részére engedély csak az elölhaladó vonatról (" + (hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben.Vonatszam + ") kapott visszajelentés után kérhető!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {//az iondítandó vonat a VP felé közlekedik, vagyis PÁROS számú
                    if ((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben != null && (hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben.Paros)
                    {
                        ind.Hibak[15]++;
                        MessageBox.Show("Állomástávolságú követési rend esetén két szomszédos (nyitva tartó) állomás között egy időben csak egy vonat lehet útban.\nA követő vonat (" + (azonosVonat.SelectedItem as Vonat).Vonatszam + ") részére engedély csak az elölhaladó vonatról (" + (hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben.Vonatszam + ") kapott visszajelentés után kérhető!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //check#3 -- szembeközlekedés ellenőrzése (engedélyben lévő vonat ellenkező irányú >> azonos irányús szöveggel nem kérhető engedély!)
                if (kpFele)
                {
                    if ((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben != null && (hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben.Paros)
                    {
                        ind.Hibak[17]++;
                        MessageBox.Show(Index.GetSzomszedAllomasNev(hivoAll.Name, kpFele) + " állomásnak már adtál engedélyt egy ellenkező irányú (páros) vonat (" + (hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben.Vonatszam + ") részére.\nHasználj egy másik engedélykérés-szöveget!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    if ((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben != null && (hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben.Paros == false)
                    {
                        ind.Hibak[17]++;
                        MessageBox.Show(Index.GetSzomszedAllomasNev(hivoAll.Name, kpFele) + " állomásnak már adtál engedélyt egy ellenkező irányú (páratlan) vonat (" + (hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben.Vonatszam + ") részére.\nHasználj egy másik engedélykérés-szöveget!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //check#4 -- ha az utolsó vonat ellenkező irányú, nem lehet azonosos szöveggel engedélyt kérni
                if (kpFele)
                {
                    if ((hivoAll.Szakaszok[0] as Allomaskoz).UtolsoVonat != null && (hivoAll.Szakaszok[0] as Allomaskoz).UtolsoVonat.Paros != (azonosVonat.SelectedItem as Vonat).Paros)
                    {
                        ind.Hibak[22]++;
                        MessageBox.Show(Index.GetSzomszedAllomasNev(hivoAll.Name, kpFele) + " állomás felől az utolsó vonat, ami leközlekedett ellenkező irányú volt, így nem kérhetsz engedélyt azonos irányú utolsó vonat leközlekedése után használandó szöveggel!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    if ((hivoAll.Szakaszok[6] as Allomaskoz).UtolsoVonat != null && (hivoAll.Szakaszok[6] as Allomaskoz).UtolsoVonat.Paros != (azonosVonat.SelectedItem as Vonat).Paros)
                    {
                        ind.Hibak[22]++;
                        MessageBox.Show(Index.GetSzomszedAllomasNev(hivoAll.Name, kpFele) + " állomás felől az utolsó vonat, ami leközlekedett ellenkező irányú volt, így nem kérhetsz engedélyt azonos irányú utolsó vonat leközlekedése után használandó szöveggel!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //check#5 -- ha a hívó állomás már elrendelt egy kijáratot ebbe az állomásközbe, akkor nem kérhet egy másik vonatnak engedélyt
                if (hivoAll.ValtKezek.Count == 2)
                {
                    if (kpFele)
                    {
                        if (hivoAll.ValtKezek[0].Ki != null && hivoAll.ValtKezek[0].Ki.Vonatszam != azonosVonat.SelectedItem.ToString())
                        {
                            ind.Hibak[27]++;
                            MessageBox.Show("A(z) " + hivoAll.ValtKezek[0].Ki.Vonatszam + " számú vonat kijárati vágányútjának már elrendelted a beállítását, most mégis a(z) " + azonosVonat.SelectedItem.ToString() + " számú vonatnak próbáltál engedélyt kérni.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        if (hivoAll.ValtKezek[1].Ki != null && hivoAll.ValtKezek[1].Ki.Vonatszam != azonosVonat.SelectedItem.ToString())
                        {
                            ind.Hibak[27]++;
                            MessageBox.Show("A(z) " + hivoAll.ValtKezek[1].Ki.Vonatszam + " számú vonat kijárati vágányútjának már elrendelted a beállítását, most mégis a(z) " + azonosVonat.SelectedItem.ToString() + " számú vonatnak próbáltál engedélyt kérni.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }
            else if (rbVan.Checked)
            {
                if (vanEllenv.SelectedIndex == -1 || vanVonat.SelectedIndex == -1)
                {
                    kimenet.BackColor = Color.Red;
                    kimenet.Text = "✘ Adj meg egy vonatot és egy ellenvonatot!";
                    return;
                }
                //check#2 -- van-e indulási az ellenvonatról
                if (!hivoAll.IndulasitKaptam.Contains((vanEllenv.SelectedItem as Vonat).Vonatszam))
                {
                        ind.Hibak[16]++;
                        MessageBox.Show("Az ellenvonatról (" + (vanEllenv.SelectedItem as Vonat).Vonatszam + ") még nem kaptál indulásiidő-közlést!\nEllenvonat esetén az indítandó vonat részére engedély csak az ellenvonatról kapott indulásiidő-közlés után kérhető!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }
                //check#3 -- ha a hívó állomás már elrendelt egy kijáratot ebbe az állomásközbe, akkor nem kérhet egy másik vonatnak engedélyt
                if (hivoAll.ValtKezek.Count == 2)
                {
                    if (kpFele)
                    {
                        if (hivoAll.ValtKezek[0].Ki != null && hivoAll.ValtKezek[0].Ki.Vonatszam != vanVonat.SelectedItem.ToString())
                        {
                            ind.Hibak[27]++;
                            MessageBox.Show("A(z) " + hivoAll.ValtKezek[0].Ki.Vonatszam + " számú vonat kijárati vágányútjának már elrendelted a beállítását, most mégis a(z) " + vanVonat.SelectedItem.ToString() + " számú vonatnak próbáltál engedélyt kérni.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        if (hivoAll.ValtKezek[1].Ki != null && hivoAll.ValtKezek[1].Ki.Vonatszam != vanVonat.SelectedItem.ToString())
                        {
                            ind.Hibak[27]++;
                            MessageBox.Show("A(z) " + hivoAll.ValtKezek[1].Ki.Vonatszam + " számú vonat kijárati vágányútjának már elrendelted a beállítását, most mégis a(z) " + vanVonat.SelectedItem.ToString() + " számú vonatnak próbáltál engedélyt kérni.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                //check#4 -- virtuális vonatnak akar engedélyt kérni, aminek az elődjének körül kéne járnia, de az nincs elrendelve (ha másik vonatnak elrendelt ellenvonatos kij. után rendelte e3l a körüljárást, azt az előző check lekezeli)
                if (hivoAll.Vegallomas)
                {
                    if ((hivoAll.KoruljarasElrendelve == null || hivoAll.KoruljarasElrendelve.Vonatszam != (vanEllenv.SelectedItem as Vonat).Vonatszam) && (vanEllenv.SelectedItem as Vonat).KoruljarasSzukseges)
                    {//nincs elrendelve, pedig szükséges
                        ind.Hibak[28]++;
                        MessageBox.Show("Egy olyan vonatnak próbáltál engedélyt kérni, amelyiknek előbb körül kellene járnia az állomáson, ám ezt akadályozza egy másik vonat, amely az állomáson tartózkodik.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            else if (rbVolt.Checked)
            {
                if (voltEllenv.SelectedIndex == -1 || voltVonat.SelectedIndex == -1)
                {
                    kimenet.BackColor = Color.Red;
                    kimenet.Text = "✘ Adj meg egy vonatot és egy ellenvonatot!";
                    return;
                }
                //check#2 -- ha a hívó állomás már elrendelt egy kijáratot ebbe az állomásközbe, akkor nem kérhet egy másik vonatnak engedélyt
                if (hivoAll.ValtKezek.Count == 2)
                {
                    if (kpFele)
                    {
                        if (hivoAll.ValtKezek[0].Ki != null && hivoAll.ValtKezek[0].Ki.Vonatszam != voltVonat.SelectedItem.ToString())
                        {
                            ind.Hibak[27]++;
                            MessageBox.Show("A(z) " + hivoAll.ValtKezek[0].Ki.Vonatszam + " számú vonat kijárati vágányútjának már elrendelted a beállítását, most mégis a(z) " + azonosVonat.SelectedItem.ToString() + " számú vonatnak próbáltál engedélyt kérni.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        if (hivoAll.ValtKezek[1].Ki != null && hivoAll.ValtKezek[1].Ki.Vonatszam != voltVonat.SelectedItem.ToString())
                        {
                            ind.Hibak[27]++;
                            MessageBox.Show("A(z) " + hivoAll.ValtKezek[1].Ki.Vonatszam + " számú vonat kijárati vágányútjának már elrendelted a beállítását, most mégis a(z) " + azonosVonat.SelectedItem.ToString() + " számú vonatnak próbáltál engedélyt kérni.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }
            
            nemIksz = true;
            object[] parameters;
            if (rbAzonos.Checked)
            {
                parameters = new object[3];
                parameters[0] = azonosOra.Value.ToString();
                parameters[1] = azonosPerc.Value.ToString();
                parameters[2] = azonosVonat.SelectedItem;
                hivandoAll.OnKozlemeny(kpFele, 0, parameters);
            }
            else if (rbVolt.Checked)
            {
                parameters = new object[4];
                parameters[0] = voltEllenv.SelectedItem;
                parameters[1] = voltOra.Value.ToString();
                parameters[2] = voltPerc.Value.ToString();
                parameters[3] = voltVonat.SelectedItem;
                hivandoAll.OnKozlemeny(kpFele, 2, parameters);
            }
            else if (rbVan.Checked)
            {   parameters = new object[4];
                parameters[0] = vanEllenv.SelectedItem;
                parameters[1] = vanOra.Value.ToString();
                parameters[2] = vanPerc.Value.ToString();
                parameters[3] = vanVonat.SelectedItem;
                hivandoAll.OnKozlemeny(kpFele, 1, parameters);
            }

            ind.notifyIcon1.ShowBalloonTip(100, "Telefon", "Várakozás " + Index.GetSzomszedAllomasNev(hivoAll.Name, kpFele) + " állomás válaszára", ToolTipIcon.None);
            this.Close();
        }

        private void EngedelyKeres_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!nemIksz)
            {
                if (MessageBox.Show("Biztosan megszakítod a hívást?", "Hívás megszakítása", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {//megszakítja a hívást >> kilépés (ballon a szomszédnak)
                    hivandoAll.OnHivasMegszakitva(kpFele);
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
    }
}
