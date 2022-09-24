using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gyermekvasút.Modellek;
using Gyermekvasút.Telefon;

namespace Gyermekvasút
{
    public partial class I : AllomasForm
    {
        #region EMELTYŰKNEK MEGFELELTETETT VÁLTOZÓK (setter: PB.Image, Jelzo.Szabad)
        bool aej;
        public bool Aej
        {
            get { return aej; }
            set
            {
                aej = value;
                if (aej)
                {
                    aejPB.Image = Gyermekvasút.Properties.Resources.Ej_fent;
                }
                else
                {
                    aejPB.Image = Gyermekvasút.Properties.Resources.Ej_lent;
                }
                aejPB.Refresh();
            }
        }
        bool a1;
        public bool A1
        {
            get { return a1; }
            set
            {
                a1 = value;
                if (a1)
                {
                    a1PB.Image = Gyermekvasút.Properties.Resources.A1_fent;
                    Allomas.Jelzok[0].Szabad = true;
                }
                else
                {
                    a1PB.Image = Gyermekvasút.Properties.Resources.A1_lent;
                    if (a2)
                    {//MEGÁLLJba vissza
                        Allomas.Jelzok[0].Szabad = false;
                        Allomas.VisszjelTimerStart();
                    }
                }
                a1PB.Refresh();
            }
        }
        bool a2 = true;
        public bool A2
        {
            get { return a2; }
            set
            {
                a2 = value;
                if (a2)
                {
                    a2PB.Image = Gyermekvasút.Properties.Resources.A2_fent;
                    if (!a1)
                    {//MEGÁLLJ
                        Allomas.Jelzok[0].Szabad = false;
                        Allomas.VisszjelTimerStart();
                    }
                }
                else
                {
                    a2PB.Image = Gyermekvasút.Properties.Resources.A2_lent;
                    Allomas.Jelzok[0].Szabad = true;
                }
                a2PB.Refresh();
            }
        }
        bool b1;
        public bool B1
        {
            get { return b1; }
            set
            {
                b1 = value;
                if (b1)
                {
                    b1PB.Image = Gyermekvasút.Properties.Resources.B1_fent;
                    Allomas.Jelzok[5].Szabad = true;
                }
                else
                {
                    b1PB.Image = Gyermekvasút.Properties.Resources.B1_lent;
                    if (b2)
                    {//MEGÁLLJ
                        Allomas.Jelzok[5].Szabad = false;
                        Allomas.VisszjelTimerStart();
                    }
                }
                b1PB.Refresh();
            }
        }
        bool b2 = true;
        public bool B2
        {
            get { return b2; }
            set
            {
                b2 = value;
                if (b2)
                {
                    b2PB.Image = Gyermekvasút.Properties.Resources.B2_fent;
                    if (!b1)
                    {
                        Allomas.Jelzok[5].Szabad = false;
                        Allomas.VisszjelTimerStart();
                    }
                }
                else
                {
                    b2PB.Image = Gyermekvasút.Properties.Resources.B2_lent;
                    Allomas.Jelzok[5].Szabad = true;
                }
                b2PB.Refresh();
            }
        }
        bool bej;
        public bool Bej
        {
            get { return bej; }
            set
            {
                bej = value;
                if (bej)
                {
                    bejPB.Image = Gyermekvasút.Properties.Resources.Ej_fent;
                }
                else
                {
                    bejPB.Image = Gyermekvasút.Properties.Resources.Ej_lent;
                }
                bejPB.Refresh();
            }
        }
        #endregion

        public I(Index index, Allomas allomas)
        {
            InitializeComponent();
            Ind = index;
            Allomas = allomas;

            Allomas.ValtKezek[1].Progbar = s_vgut;
            Allomas.ValtKezek[0].Progbar = l_vgut;

            s_vgut.Value = Allomas.ValtKezek[1].ProgressBar;
            l_vgut.Value = Allomas.ValtKezek[0].ProgressBar;

            Allomas.Valtok[1].Lezart = true;
        }

        public I()
        {
            InitializeComponent();
        }

        public override void Frissit()
        {
            base.Frissit();

            #region VÁLTÓZÁRKULCSOK VISSZAJELENTÉSE
            if (Allomas.Valtok[1].Lezart)
            {
                if (Allomas.Valtok[1].Allas)
                {
                    vzk1.Image = Gyermekvasút.Properties.Resources.Vzk_1_E;
                }
                else
                {
                    vzk1.Image = Gyermekvasút.Properties.Resources.Vzk_1_K;
                }
            }
            else
            {
                vzk1.Image = Gyermekvasút.Properties.Resources.Vzk_1_alap;
            }

            if (Allomas.Valtok[0].Lezart)
            {
                if (Allomas.Valtok[0].Allas)
                {
                    vzk2.Image = Gyermekvasút.Properties.Resources.Vzk_2_E;
                }
                else
                {
                    vzk2.Image = Gyermekvasút.Properties.Resources.Vzk_2_K;
                }
            }
            else
            {
                vzk2.Image = Gyermekvasút.Properties.Resources.Vzk_1_alap;
            }
            #endregion

            #region VÁGÁNYÚTOLDÁS
            if (Allomas.ValtKezek[1].Ki != null)
            {
                if (Allomas.ValtKezek[1].Vagany == 1 && !Allomas.Szakaszok[4].Foglalt && Allomas.Szakaszok[5].Foglalt && Allomas.Szakaszok[5].Vonat.Paros)
                {//1ről jár ki, váltóról lehaladt >> oldás
                    //állás E >> jó; lezárás szabványos >> jó
                    Allomas.ValtKezek[1].ProgressBar = 0;
                    Allomas.ValtKezek[1].Be = null;
                    Allomas.ValtKezek[1].Ki = null;
                    Allomas.ValtKezek[1].Vagany = 0;
                    Frissit();
                }
                if (Allomas.ValtKezek[1].Vagany == 2 && !Allomas.Szakaszok[9].Foglalt && Allomas.Szakaszok[5].Foglalt && Allomas.Szakaszok[5].Vonat.Paros)
                {//2ről jár ki, váltóról lehaladt >> oldás
                    //állás K >> rossz; lezárás szabványos >> jó (old-állít-lezár)
                    Allomas.Valtok[1].Lezart = false;
                    Allomas.ValtKezek[1].ProgressBar = 0;
                    Allomas.ValtKezek[1].Be = null;
                    Allomas.ValtKezek[1].Ki = null;
                    Allomas.ValtKezek[1].Vagany = 0;
                    Frissit();
                    Allomas.Valtok[1].Allas = true;
                    Allomas.Valtok[1].Lezart = true;
                    Frissit();
                }
            }
            if (Allomas.ValtKezek[0].Ki != null)
            {
                if (Allomas.ValtKezek[0].Vagany == 1 && Allomas.Szakaszok[1].Foglalt && !Allomas.Szakaszok[2].Foglalt && !Allomas.Szakaszok[1].Vonat.Paros)
                {//1ről járunk ki >> E (szabv.) >> oldás
                    Allomas.Valtok[0].Lezart = false;
                    Allomas.ValtKezek[0].ProgressBar = 0;
                    Allomas.ValtKezek[0].Be = null;
                    Allomas.ValtKezek[0].Ki = null;
                    Allomas.ValtKezek[0].Vagany = 0;
                    Frissit();
                }
                if (Allomas.ValtKezek[0].Vagany == 2 && Allomas.Szakaszok[1].Foglalt && !Allomas.Szakaszok[7].Foglalt && !Allomas.Szakaszok[1].Vonat.Paros)
                {//2ről járunk ki >> K >> oldás-állítás
                    Allomas.Valtok[0].Lezart = false;
                    Allomas.ValtKezek[0].ProgressBar = 0;
                    Allomas.ValtKezek[0].Be = null;
                    Allomas.ValtKezek[0].Ki = null;
                    Allomas.ValtKezek[0].Vagany = 0;
                    Allomas.Valtok[0].Allas = true;
                    Frissit();
                }
            }
            #endregion
        }

        private void i_Load(object sender, EventArgs e)
        {
            Ind.I_open = true;            

            if (Hr == null)
            {
                Hr = new Hr(true, false, Allomas);
            }

            if (Hr.Frissitheto)
            {
                Hr.vszkRefresh();
            }
            Frissit();
        }

        private void i_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ind.I_open = false;

            Hr.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)//napl
        {
            Naplozo napl = new Naplozo(Allomas, true, true, Ind);
            if (napl.ShowDialog() == DialogResult.OK)
            {
                Frissit();
            }
        }

        #region EMELTYŰK ÁLLÍTÁSA
        private void bej_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (Bej)
                {
                    //felső állás >> visszavétel
                    Bej = false;
                }
                else
                {
                    //alsó állás >> az állítás feltétele, hogy a B1 fent
                    if (B1)
                    {
                        Bej = true;
                    }
                }
            }
        }
        private void b1PB_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (B1)
                {
                    //felső állás >> alsó állás (megálljba állítás)
                    if (!Bej)
                    {
                        if (Allomas.Szakaszok[5].Foglalt || Allomas.Szakaszok[4].Foglalt)
                        {//vonat a megálljraejtőn vagy a váltón
                            MessageBox.Show("A bejárati jelzőt leghamarabb azután szabad visszaállítani Megállj!-állásba, hogy a behaladó vonat lehaladt a bejárati váltóról!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Ind.Hibak[3]++;
                        }
                        else
                        {
                            if (!Allomas.FogadottVonatok.Contains(Allomas.ValtKezek[1].Be))
                            {//a vonat vagy még csak engedélyben van, vagy az állomásközben
                                MessageBox.Show("A bejárati jelzőt még nem állíthatod vissza Megállj!-állásba, mert a tőle kezdődő vágányúton még nem közlekedett le vonat (a bejárati jelzőt még meg sem haladta a vonat, amelyiknek a bejárati vágányútjának a szabad mivoltát bejelentette a váltókezelő)!\nA vonat még vagy el sem indult a szomszéd állomásról, vagy éppen az állomásközben van.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ind.Hibak[4]++;
                            }
                            else
                            {//1re jártam be, tehát a váltó állása E >> jó, lezárás szabv >> jó
                                B1 = false;
                                Allomas.ValtKezek[1].ProgressBar = 0;
                                if (Allomas.ValtKezek[1].Ki == null)
                                {//nem ellenvonatos kijárat
                                    Allomas.ValtKezek[1].Be = null;
                                    Allomas.ValtKezek[1].Vagany = 0;
                                }
                                else
                                {
                                    Allomas.ValtKezek[1].Bejarat = false;
                                    Allomas.ValtKezek[1].Vagany = Allomas.ValtKezek[1].Vagany == 1 ? 2 : 1;
                                    Allomas.ValtKezek[1].Be = null;
                                    Allomas.ValtKezek[1].ProgressBar = 0;
                                    Allomas.ValtKezek[1].Timer.Start();
                                    Allomas.ValtKezek[1].Valto.Lezart = false;
                                    Allomas.ValtKezek[1].Valto.AllitasAlatt = true;
                                }
                                Frissit();
                            }
                        }
                    }
                }
                else
                {
                    //alsó állás >> szabadra állítás
                    if (Allomas.Valtok[1].Allas && Allomas.Valtok[1].Lezart && !Allomas.Jelzok[0].Szabad)
                    {
                        if (Allomas.ValtKezek[1].Be == null)
                        {
                            MessageBox.Show("A bejárati vágányút beállításának elrendelését nem előzheti meg a bejárati jelző szabadra állítása!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Ind.Hibak[5]++;
                        }
                        else
                        {
                            B1 = true;
                        }
                    }
                }
            }
        }
        private void b2PB_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (B2)
                {
                    //felső állás >> alsó állás (szabadra állítás)
                    if (!Allomas.Valtok[1].Allas && Allomas.Valtok[1].Lezart && !Allomas.Jelzok[0].Szabad)
                    {
                        if (Allomas.ValtKezek[1].Be == null)
                        {
                            MessageBox.Show("A bejárati vágányút beállításának elrendelését nem előzheti meg a bejárati jelző szabadra állítása!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Ind.Hibak[5]++;
                        }
                        else
                        {
                            B2 = false;
                        }
                    }
                }
                else
                {
                    //alsó állásból felsőbe >> nicns feltétel (K, így előjelző nicsn meghúzva) (megálljra állítás)
                    //annyi feltétel azért mégis van, hogy a vonat lehaladt a váltóról :)

                    if (Allomas.Szakaszok[5].Foglalt || Allomas.Szakaszok[9].Foglalt)
                    {//vonat a megálljraejtőn vagy a váltón
                        MessageBox.Show("A bejárati jelzőt leghamarabb azután szabad visszaállítani Megállj!-állásba, hogy a behaladó vonat lehaladt a bejárati váltóról!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Ind.Hibak[3]++;
                    }
                    else
                    {
                        if (!Allomas.FogadottVonatok.Contains(Allomas.ValtKezek[1].Be))
                        {//a vonat vagy még csak engedélyben van, vagy az állomásközben
                            MessageBox.Show("A bejárati jelzőt még nem állíthatod vissza Megállj!-állásba, mert a tőle kezdődő vágányúton még nem közlekedett le vonat (a bejárati jelzőt még meg sem haladta a vonat, amelyiknek a bejárati vágányútjának a szabad mivoltát bejelentette a váltókezelő)!\nA vonat még vagy el sem indult a szomszéd állomásról, vagy éppen az állomásközben van.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Ind.Hibak[4]++;
                        }
                        else
                        {
                            B2 = true;
                            Allomas.Valtok[1].Lezart = false;
                            Allomas.ValtKezek[1].ProgressBar = 0;
                            Frissit();
                            Allomas.Valtok[1].Allas = true;
                            Allomas.Valtok[1].Lezart = true;
                            if (Allomas.ValtKezek[1].Ki == null)
                            {//nem ellenvonatos kijárat
                                Allomas.ValtKezek[1].Be = null;
                                Allomas.ValtKezek[1].Vagany = 0;
                            }
                            else
                            {
                                Allomas.ValtKezek[1].Bejarat = false;
                                Allomas.ValtKezek[1].Vagany = Allomas.ValtKezek[1].Vagany == 1 ? 2 : 1;
                                Allomas.ValtKezek[1].Be = null;
                                Allomas.ValtKezek[1].ProgressBar = 0;
                                Allomas.ValtKezek[1].Timer.Start();
                                Allomas.ValtKezek[1].Valto.Lezart = false;
                                Allomas.ValtKezek[1].Valto.AllitasAlatt = true;
                            }
                            Frissit();
                        }
                    }
                }
            }
        }
        private void a1PB_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (A1)
                {
                    if (!Aej)
                    {
                        //felső állás >> alsó állás (megálljba állítás)
                        if (Allomas.Szakaszok[1].Foglalt || Allomas.Szakaszok[2].Foglalt)
                        {//vonat a megálljraejtőn vagy a váltón
                            MessageBox.Show("A bejárati jelzőt leghamarabb azután szabad visszaállítani Megállj!-állásba, hogy a behaladó vonat lehaladt a bejárati váltóról!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Ind.Hibak[3]++;
                        }
                        else
                        {
                            if (!Allomas.FogadottVonatok.Contains(Allomas.ValtKezek[0].Be))
                            {//a vonat vagy még csak engedélyben van, vagy az állomásközben
                                MessageBox.Show("A bejárati jelzőt még nem állíthatod vissza Megállj!-állásba, mert a tőle kezdődő vágányúton még nem közlekedett le vonat (a bejárati jelzőt még meg sem haladta a vonat, amelyiknek a bejárati vágányútjának a szabad mivoltát bejelentette a váltókezelő)!\nA vonat még vagy el sem indult a szomszéd állomásról, vagy éppen az állomásközben van.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ind.Hibak[4]++;
                            }
                            else
                            {
                                A1 = false;
                                Allomas.Valtok[0].Lezart = false;
                                Allomas.ValtKezek[0].ProgressBar = 0;
                                Frissit();
                                if (Allomas.ValtKezek[0].Ki == null)
                                {//nem ellenvonatos kijárat
                                    Allomas.ValtKezek[0].Be = null;
                                    Allomas.ValtKezek[0].Vagany = 0;
                                }
                                else
                                {
                                    Allomas.ValtKezek[0].Bejarat = false;
                                    Allomas.ValtKezek[0].Vagany = Allomas.ValtKezek[0].Vagany == 1 ? 2 : 1;
                                    Allomas.ValtKezek[0].Be = null;
                                    Allomas.ValtKezek[0].ProgressBar = 0;
                                    Allomas.ValtKezek[0].Timer.Start();
                                    Allomas.ValtKezek[0].Valto.Lezart = false;
                                    Allomas.ValtKezek[0].Valto.AllitasAlatt = true;
                                }
                                Frissit();
                            }
                        }
                    }
                }
                else
                {
                    //alsó állás >> szabadra állítás
                    if (Allomas.Valtok[0].Allas && Allomas.Valtok[0].Lezart && !Allomas.Jelzok[5].Szabad)
                    {
                        if (Allomas.ValtKezek[0].Be == null)
                        {
                            MessageBox.Show("A bejárati vágányút beállításának elrendelését nem előzheti meg a bejárati jelző szabadra állítása!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Ind.Hibak[5]++;
                        }
                        else
                        {
                            A1 = true;
                        }
                    }
                }
            }
        }
        private void a2PB_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (A2)
                {
                    //felső állás >> alsó állás (szabadra állítás)
                    if (!Allomas.Valtok[0].Allas && Allomas.Valtok[0].Lezart && !Allomas.Jelzok[5].Szabad)
                    {
                        if (Allomas.ValtKezek[0].Be == null)
                        {
                            MessageBox.Show("A bejárati vágányút beállításának elrendelését nem előzheti meg a bejárati jelző szabadra állítása!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Ind.Hibak[5]++;
                        }
                        else
                        {
                            A2 = false;
                        }
                    }
                }
                else
                {
                    //alsó állásból felsőbe >> nicns feltétel (K, így előjelző nicsn meghúzva) (megálljra állítás)
                    //annyi feltétel azért mégis van, hogy a vonat lehaladt a váltóról :)

                    if (Allomas.Szakaszok[1].Foglalt || Allomas.Szakaszok[7].Foglalt)
                    {//vonat a megálljraejtőn vagy a váltón
                        MessageBox.Show("A bejárati jelzőt leghamarabb azután szabad visszaállítani Megállj!-állásba, hogy a behaladó vonat lehaladt a bejárati váltóról!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Ind.Hibak[3]++;
                    }
                    else
                    {
                        if (!Allomas.FogadottVonatok.Contains(Allomas.ValtKezek[0].Be))
                        {//a vonat vagy még csak engedélyben van, vagy az állomásközben
                            MessageBox.Show("A bejárati jelzőt még nem állíthatod vissza Megállj!-állásba, mert a tőle kezdődő vágányúton még nem közlekedett le vonat (a bejárati jelzőt még meg sem haladta a vonat, amelyiknek a bejárati vágányútjának a szabad mivoltát bejelentette a váltókezelő)!\nA vonat még vagy el sem indult a szomszéd állomásról, vagy éppen az állomásközben van.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Ind.Hibak[4]++;
                        }
                        else
                        {
                            A2 = true;
                            Allomas.Valtok[0].Lezart = false;
                            Allomas.ValtKezek[0].ProgressBar = 0;
                            Frissit();
                            Allomas.Valtok[0].Allas = true;
                            if (Allomas.ValtKezek[0].Ki == null)
                            {//nem ellenvonatos kijárat
                                Allomas.ValtKezek[0].Be = null;
                                Allomas.ValtKezek[0].Vagany = 0;
                            }
                            else
                            {
                                Allomas.ValtKezek[0].Bejarat = false;
                                Allomas.ValtKezek[0].Vagany = Allomas.ValtKezek[0].Vagany == 1 ? 2 : 1;
                                Allomas.ValtKezek[0].Be = null;
                                Allomas.ValtKezek[0].ProgressBar = 0;
                                Allomas.ValtKezek[0].Timer.Start();
                                Allomas.ValtKezek[0].Valto.Lezart = false;
                                Allomas.ValtKezek[0].Valto.AllitasAlatt = true;
                            }
                            Frissit();
                        }
                    }
                }
            }
        }
        private void aejPB_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (Aej)
                {
                    //felső állás >> visszavétel
                    Aej = false;
                }
                else
                {
                    //alsó állás >> felső állás (feltétel: A1 fent)
                    if (A1)
                    {
                        Aej = true;
                    }
                }
            }
        }
        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)//vk1
        {
            if (Allomas.ValtKezek[1].Ki != null)
            {
                //vagy kijárat vagy ellenvonatos kijárat van >> nem nyílik meg, hanem tájékoztató MB
                MessageBox.Show("A kiválasztott váltókezelő (1) jelenleg nem rendelhető el egyetlen vágányút beállítására sem.\nAz ok a következő lehet:\nA váltókezelő már beállított vagy éppen beállít egy kijárati vágányutat.\nA következő vágányút csak a kijáró vonat leközlekedése (a vágányút feloldása) után rendelhető el.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Ind.Hibak[0]++;
            }
            else
            {
                Valtokezelo vk1 = new Valtokezelo(Allomas, true, Allomas.Szakaszok, Allomas.ValtKezek[1], Allomas.ValtKezek[0]);
                if (vk1.ShowDialog() == DialogResult.OK)
                {
                    Frissit();
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)//vk2
        {
            if (Allomas.ValtKezek[0].Ki != null)
            {
                //vagy kijárat vagy ellenvonatos kijárat van >> nem nyílik meg, hanem tájékoztató MB
                MessageBox.Show("A kiválasztott váltókezelő (1) jelenleg nem rendelhető el egyetlen vágányút beállítására sem.\nAz ok a következő lehet:\nA váltókezelő már beállított vagy éppen beállít egy kijárati vágányutat.\nA következő vágányút csak a kijáró vonat leközlekedése (a vágányút feloldása) után rendelhető el.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Ind.Hibak[0]++;
            }
            else
            {
                Valtokezelo vk2 = new Valtokezelo(Allomas, false, Allomas.Szakaszok, Allomas.ValtKezek[0], Allomas.ValtKezek[1]);
                if (vk2.ShowDialog() == DialogResult.OK)
                {
                    Frissit();
                }
            }
        }

        private void a2PB_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Allomas.AddVonat(new Vonat(new string[]{"217"}, Allomas.Szakaszok[3], Allomas));
            Frissit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Allomas.AddVonat(new Vonat(new string[]{"217"}, Allomas.Szakaszok[6], Allomas));
            Frissit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Allomas.AddVonat(new Vonat(new string[]{"112"}, Allomas.Szakaszok[0], Allomas));
            Frissit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EngedelyKeres ek = new EngedelyKeres();
            ek.ShowDialog();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void I_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hr.Hide();
            this.Hide();
            e.Cancel = true;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            label3.BackColor = Color.Red;
            Allomas.AllomasLezar();
        }

        private void I_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z && e.Modifiers == (Keys.Alt | Keys.Control))
            {
                AllomasFormAllomasLezar(label3, label6);
            }
        }
    }
    public class VZKfelirat : Label
    {
        public VZKfelirat()
        {
            BackColor = System.Drawing.Color.FromArgb(195, 195, 195);
        }
    }
}