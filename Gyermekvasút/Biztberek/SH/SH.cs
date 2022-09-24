using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gyermekvasút.Modellek;
using Gyermekvasút.Modellek.Emeltyűtípusok;

namespace Gyermekvasút.Biztberek
{
    public partial class SH : UserControl
    {
        public SH()
        {
            InitializeComponent();
            induktorTimer.Tick += induktorTimer_Tick;
            vaganyutBeallitasTimer.Tick += vaganyutBeallitasTimer_Tick;
        }

        void vaganyutBeallitasTimer_Tick(object sender, EventArgs e)
        {//ValtKez.Timer helyett
            if (VK1.ProgressBar == 9)
            {
                VK1.ProgressBar = 10;
            }
            else if (VK1.ProgressBar == 10)
            {//bejelentés
                vaganyutBeallitasTimer.Stop();
                VK1.Valto.AllitasAlatt = false;
                if ((VK1.ElsoVaganyEgyenes && VK1.Vagany == 1) || (!VK1.ElsoVaganyEgyenes && VK1.Vagany == 2))
                {//E
                    VK1.Valto.Allas = true;
                }
                else
                {//K
                    VK1.Valto.Allas = false;
                }
                VK1.Valto.Lezart = true;

                #region BEJELENTÉS MBOX
                if ((VK1.Be != null && VK1.Ki == null) || (VK1.Be != null && VK1.Ki != null && VK1.Bejarat))
                {
                    MessageBox.Show("Rendelkező pajtás!\nA(z) " + VK1.Be.Vonatszam + " számú vonat bejárata szabad a(z) " + VK1.VaganyString + " vágányra.", "Vágányút-beállítás bejelentése", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if ((VK1.Be == null && VK1.Ki != null) || (VK1.Be != null && VK1.Ki != null && !VK1.Bejarat))
                {
                    MessageBox.Show("Rendelkező pajtás!\nA(z) " + VK1.Ki.Vonatszam + " számú vonat kijárata szabad a(z) " + VK1.VaganyString + " vágányról.", "Vágányút-beállítás bejelentése", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion

                VaganyutiBlokk.Allas = SHBlokkAllas.Lezart;
            }
            else
            {
                if (VK1.Valto.Lezart)
                {
                    VK1.Valto.Lezart = false;
                }
                VK1.ProgressBar++;
            }
            AllForm.Frissit();
        }

        void induktorTimer_Tick(object sender, EventArgs e)
        {
            (sender as Timer).Stop();

            if (BalInduktor == null || JobbInduktor == null) return;
            //Szög
            BalInduktor.Szog += SHInduktor.SzogSebesseg * (double)(sender as Timer).Interval / (double)1000;
            JobbInduktor.Szog = BalInduktor.Szog + Math.PI;
            //Pozíció
            BalInduktor.Location = new Point(this.Location.X + SHInduktor.BalKozepsoRelativPozicio.X, this.Location.Y + SHInduktor.BalKozepsoRelativPozicio.Y - Convert.ToInt32(Math.Sin(BalInduktor.Szog) * 75));
            JobbInduktor.Location = new Point(this.Location.X + SHInduktor.JobbKozepsoRelativPozicio.X, this.Location.Y + SHInduktor.JobbKozepsoRelativPozicio.Y - Convert.ToInt32(Math.Sin(JobbInduktor.Szog) * 75));
            //Vonal
            JobbInduktor.VonalKirajzol();
            BalInduktor.VonalKirajzol();
            (sender as Timer).Start();
        }

        public SHInduktor BalInduktor { get; set; }
        public SHInduktor JobbInduktor { get; set; }
        Timer induktorKesleltetes = new Timer() { Interval = 500 };
        public void InduktorTeker()
        {
            #region Megjelenés
            induktorTimer.Start();
            #endregion
            if (CsengoLenyomva)
            {
                Csengetes();
            }
            else
            {
                if (blokkbillLehuzva == BejaratiJelzoBlokkbillentyu) //~ RET2 jelző szabadba állítás
                {
                    if (blokkbillLehuzva.Blokk.Allas == SHBlokkAllas.Lezart)
                    {//feloldás
                        if (VaganyutiBlokk.Allas == SHBlokkAllas.Lezart)
                        {//villamos függés a torony és a forgalmi között
                            BejaratiJelzoBlokk.Allas = SHBlokkAllas.Oldott;
                            induktorKesleltetesTipus = 1;
                            induktorKesleltetes.Start();
                        }
                    }
                }
                else if (blokkbillLehuzva == VaganyutiBlokkbillentyu) //~ RET2 váltó feloldás
                {
                    if (blokkbillLehuzva.Blokk.Allas == SHBlokkAllas.Lezart)
                    {//feloldás
                        if (BejaratiJelzoBlokk.Allas == SHBlokkAllas.Lezart && KijaratiJelzoBlokk.Allas == SHBlokkAllas.Lezart)
                        {
                            VaganyutiBlokk.Allas = SHBlokkAllas.Oldott;
                            OK1.Stop();
                            induktorKesleltetesTipus = 2;
                            induktorKesleltetes.Start();
                        }
                    }
                }
                else if (blokkbillLehuzva == KijaratiJelzoBlokkbillentyu) //~ RET2 jelző szabadba állítás
                {
                    if (blokkbillLehuzva.Blokk.Allas == SHBlokkAllas.Lezart)
                    {//feloldás
                        if (VaganyutiBlokk.Allas == SHBlokkAllas.Lezart)
                        {//villamos függés a torony és a forgalmi között
                            KijaratiJelzoBlokk.Allas = SHBlokkAllas.Oldott;
                            induktorKesleltetesTipus = 3;
                            induktorKesleltetes.Start();
                        }
                    }
                }
                if (blokkbillLehuzva != null)
                {
                    blokkbillLehuzva.Lehuzva = false;
                    blokkbillLehuzva = null;
                }
            }
        }
        public void InduktorTekeresVege()
        {
            induktorTimer.Stop();
            if (blokkbillLehuzva != null)
            {
                blokkbillLehuzva.Lehuzva = false;
                blokkbillLehuzva = null;
            }
            CsengoLenyomva = false;
        }
        Timer induktorTimer = new Timer() { Interval = 30 };
        int induktorKesleltetesTipus = 0;
        void induktorKesleltetes_Tick(object sender, EventArgs e)
        {
            induktorKesleltetes.Stop();
            switch (induktorKesleltetesTipus)
            {
                case 1://bej                    
                    SZAKASZOK[6].Jelzo.Szabad = true;
                    break;
                case 2://vgút                    
                    VK1.Valto.Lezart = false;
                    VK1.ProgressBar = 0;
                    KicsengetettVonatszam = "";
                    OnValtoFeloldva();
                    if (VK1.Be != null)
                    {
                        if (VK1.Ki != null)
                        {//BE, ellenvonatos KI
                            if (VK1.Ki.Virtualis == false)
                            {//egyébként a körüljárás után
                                if (!VK1.Be.Forda.Contains(VK1.Ki.Vonatszam))
                                {//ha nem az érkező vonatból (körüljárás NÉLKÜL) forduló vonatnak a kijárata, csak akkor cserél vágányt
                                    VK1.Vagany = VK1.Vagany == 1 ? 2 : 1;
                                }
                                VK1.Bejarat = false;
                            }
                        }
                        else
                        {//csak BE
                            VK1.Be = null;
                            VK1.Vagany = 0;
                        }
                    }
                    else
                    {//Be == null ==> csak KI
                        VK1.Ki = null;
                        VK1.Vagany = 0;
                    }
                    OK1.Stop();
                    OK1.Elinditva = false;
                    break;
                case 3://kij
                    if (VK1.Vagany == 1)
                    {//K
                        SZAKASZOK[9].Jelzo.Szabad = true;
                    }
                    else
                    {//E
                        SZAKASZOK[4].Jelzo.Szabad = true;
                    }
                    break;
                default:
                    break;
            }
        }
        public bool CsengoLenyomva { get; set; }
        public void CsengoLenyomas()
        {
            if (VK1.Ki == null && VK1.Be == null)
            {//nincs elrendelve
                MessageBox.Show("Még egy vonat vágányútjának beállítását sem rendelted el.\nElőször rendeld el egy vágányút beállítását, és csak utána csengesd ki a vágányutat!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (KicsengetettVonatszam != "" && KicsengetettVonatszam != null)
            {
                MessageBox.Show("Már kicsengetted egy vonat vágányútját, és azt a vágányutat még nem oldottad fel.\nEgyszerre csak egy vágányút lehet kicsengetve.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {//iránykallantyú, tológomb vizsgálata
                bool tologombRossz = false, iranykallRossz = false;
                if (VK1.Vagany != (int)Tologomb.Allas)
                {//tológomb rossz helyen
                    tologombRossz = true;
                }
                if (!((((VK1.Be != null && VK1.Ki == null) || (VK1.Ki != null && VK1.Be != null && VK1.Bejarat)) && Iranykallantyu.Allas == SHIranykallantyuAllas.Bejarat) || ((VK1.Be == null || (VK1.Ki != null && VK1.Be != null && VK1.Bejarat == false)) && Iranykallantyu.Allas == SHIranykallantyuAllas.Kijarat)))
                {//iránykallantyú rossz helyen
                    iranykallRossz = true;
                }
                if (tologombRossz || iranykallRossz)
                {//mbox
                    string err = "";
                    err = iranykallRossz && !tologombRossz ? "Az iránykallantyú állása nem megfelelő." : err;
                    err = !iranykallRossz && tologombRossz ? "A tológomb állása nem megfelelő." : err;
                    err = tologombRossz && iranykallRossz ? "A tológomb és az iráykallantyú is helytelen állásban van." : err;
                    MessageBox.Show("Azt a vágányutat csengesd ki, amelyik beállítását már elrendelted a váltókezelőnek!\n" + err, "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (blokkbillLehuzva != null)
            {
                blokkbillLehuzva.Lehuzva = false;
                blokkbillLehuzva = null;
            }
            CsengoLenyomva = true;
        }
        public string KicsengetettVonatszam { get; set; }
        Timer csengoKesleltetes = new Timer() { Interval = 500 };
        bool csengoKesleltetesBejarat = false;
        void csengoKesleltetes_Tick(object sender, EventArgs e)
        {
            csengoKesleltetes.Stop();
            if (csengoKesleltetesBejarat)
            {//be
                KicsengetettVonatszam = VK1.Be.Vonatszam;
                vaganyutBeallitasTimer.Start();
                VK1.Valto.AllitasAlatt = true;
            }
            else
            {//ki
                KicsengetettVonatszam = VK1.Ki.Vonatszam;
                vaganyutBeallitasTimer.Start();
                VK1.Valto.AllitasAlatt = true;
            }
        }
        public void Csengetes() //~ RET2 váltó lezárás
        {
            //a mbox-ot dobó ellenőrzések a CsengoLenyomas()-ban vannak!

            if (VK1.Be == null && VK1.Ki != null)
            {//csak ki van elrendelve >> KI
                csengoKesleltetesBejarat = false;
                csengoKesleltetes.Start();
            }
            else
            {//BE (vagy csak be vagy mindkettő)
                csengoKesleltetesBejarat = true;
                csengoKesleltetes.Start();
            }
        }

        //blokkok readonlyk a blokkbillentyűkből (blokkbill.blokk)
        public SHBlokk BejaratiJelzoBlokk
        {
            get
            {
                if (BejaratiJelzoBlokkbillentyu == null) return null;
                return BejaratiJelzoBlokkbillentyu.Blokk;
            }
        }
        public SHBlokk KijaratiJelzoBlokk
        {
            get
            {
                if (KijaratiJelzoBlokkbillentyu == null) return null;
                return KijaratiJelzoBlokkbillentyu.Blokk;
            }
        }
        public SHBlokk VaganyutiBlokk
        {
            get
            {
                if (VaganyutiBlokkbillentyu == null) return null;
                return VaganyutiBlokkbillentyu.Blokk;
            }
        }

        public SHBlokkbillentyu BejaratiJelzoBlokkbillentyu { get; set; }
        public SHBlokkbillentyu KijaratiJelzoBlokkbillentyu { get; set; }
        public SHBlokkbillentyu VaganyutiBlokkbillentyu { get; set; }

        private SHBlokkbillentyu blokkbillLehuzva = null;
        public void BlokkbillentyuLehuzValtozas(SHBlokkbillentyu sender)
        {
            CsengoLenyomva = false;
            //sender: a _Clickben rögtön ezt a fgv-t hívja, nem változtat propertyt
            if (sender.Lehuzva)//old
            {//lent>fent
                sender.Lehuzva = false;
                blokkbillLehuzva = null;
            }
            else
            {//fent>lent
                if ((sender.Blokk.Tipus == SHBlokkTipus.Vaganyuti && sender.Blokk.Allas == SHBlokkAllas.Lezart) || (sender.Blokk.Tipus == SHBlokkTipus.BejaratiJelzo && Iranykallantyu.Allas == SHIranykallantyuAllas.Bejarat) || (sender.Blokk.Tipus == SHBlokkTipus.KijaratiJelzo && Iranykallantyu.Allas == SHIranykallantyuAllas.Kijarat))
                {
                    if (blokkbillLehuzva != null)
                    {
                        blokkbillLehuzva.Lehuzva = false;
                    }
                    blokkbillLehuzva = sender;
                    sender.Lehuzva = true;
                }
            }
        }

        public SHTologomb Tologomb { get; set; }

        public SHIranykallantyu Iranykallantyu { get; set; }

        //Pen p = new Pen(Color.FromArgb(26, 48, 24), 3);
        private void SH_Paint(object sender, PaintEventArgs e)
        {
            //if (BalInduktor == null || JobbInduktor == null) return;
            //e.Graphics.DrawLine(p, BalInduktor.Location.X - this.Location.X + 56, BalInduktor.Location.Y - this.Location.Y + 9, 59, 261);
        }

        public AllomasForm AllForm { get; set; }
        public OldasKenyszerito OK1 { get; set; }

        public ValtKez VK1 { get; set; }
        public Index Ind { get; set; }
        public List<Szakasz> SZAKASZOK { get; set; }
        public Allomas ALLOMAS { get; set; }

        public event FrissuljDelegate ValtoFeloldva;
        public void OnValtoFeloldva()
        {
            if (ValtoFeloldva != null)
            {
                ValtoFeloldva();
            }
        }

        Timer vaganyutBeallitasTimer = new Timer(); //{ Interval = 6000 / Gyermekvasút.Modellek.Settings.SebessegOszto }; -- commented out to avoid DivideByZeroException before defining the value of SebessegOszto

        private void SH_Load(object sender, EventArgs e)
        {
            csengoKesleltetes.Tick += new EventHandler(csengoKesleltetes_Tick);
            induktorKesleltetes.Tick += new EventHandler(induktorKesleltetes_Tick);

            vaganyutBeallitasTimer.Interval = 6000 / Gyermekvasút.Modellek.Settings.SebessegOszto;
        }
    }
}
