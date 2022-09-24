using Gyermekvasút.Modellek;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gyermekvasút.Biztberek;

namespace Gyermekvasút.AllomasFormok
{
    public partial class A : AllomasForm
    {
        public bool KoruljarasZajlik { get; set; }

        public SH SH { get; set; }

        public A()
        {
            InitializeComponent();
        }        
        public A(Index index, Allomas allomas)
        {
            this.Allomas = allomas;
            this.Ind = index;
            SHKenyszeroldo.Ind = index;

            InitializeComponent();

            SH = sh1;
            SH.Ind = this.Ind;

            Allomas.ValtKezek[1].Progbar = u_vgut;
            u_vgut.Value = Allomas.ValtKezek[0].ProgressBar;

            SH.SZAKASZOK = Allomas.Szakaszok;
            SH.VK1 = Allomas.ValtKezek[1];
            SH.ALLOMAS = Allomas;

            SH.OK1 = OK1;
            OK1.Valto = Allomas.Valtok[1];

            SH.AllForm = this;
            SH.ValtoFeloldva += ValtoFeloldvaKoruljaras;

            koruljarasTimer.Tick += koruljarasTimer_Tick;
        }

        private void A_Load(object sender, EventArgs e)
        {
            if (Hr == null)
            {
                Hr = new Hr(true, true, Allomas);
            }

            if (Hr.Frissitheto)
            {
                Hr.vszkRefresh();
            }
            Frissit();
            Allomas.ValtKezek[1].FormOpen = true;
            kesleltetes.Tick += new EventHandler(kesleltetes_Tick);
        }
                
        private void A_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hr.Hide();
            this.Hide();
            e.Cancel = true;

            Allomas.ValtKezek[1].FormOpen = false;
        }

        Timer kesleltetes = new Timer() { Interval = 500 };
        bool kesleltetesBejarat = false;
        void kesleltetes_Tick(object sender, EventArgs e)
        {
            kesleltetes.Stop();
            if (kesleltetesBejarat)
            {//bej
                SH.BejaratiJelzoBlokk.Allas = SHBlokkAllas.Lezart;
                OK1.Start2(true);
            }
            else
            {//kij
                SH.KijaratiJelzoBlokk.Allas = SHBlokkAllas.Lezart;
                OK1.Start2(false);
            }
        }

        public override void Frissit()
        {
            base.Frissit();

            #region "VÁGÁNYÚTOLDÁS" -- jelzőblokk visszazárása, OK start (OK > vgúti blokk oldása!)
            //ha kihaladt a vonat, vegye vissza a jelzőt és zárja vissza a jelzőblokkot
            if (Allomas.ValtKezek[1].Ki != null && (Allomas.Szakaszok[5].Vonat == Allomas.ValtKezek[1].Ki || Allomas.Szakaszok[6].Vonat == Allomas.ValtKezek[1].Ki || !Allomas.Vonatok.Contains(Allomas.ValtKezek[1].Ki)) && Allomas.Szakaszok[4].Vonat != Allomas.ValtKezek[1].Ki && Allomas.Szakaszok[9].Vonat != Allomas.ValtKezek[1].Ki && (Allomas.Szakaszok[4].Jelzo.Szabad || Allomas.Szakaszok[9].Jelzo.Szabad))
            {//feltételek: KI != null && már kiment az állomásról > csucsszakasz vagy állköz vagy már a szomszéd állomáson **
                Allomas.Szakaszok[4].Jelzo.Szabad = false; //jelző M!
                Allomas.Szakaszok[9].Jelzo.Szabad = false; //jelző M!
                kesleltetesBejarat = false;
                kesleltetes.Start();
            }

            //ha behaladt a vonat, vegye vissza a jelzőt és zárja vissza a jelzőblokkot
            if (Allomas.ValtKezek[1].Be != null && Allomas.FogadottVonatok.Contains(Allomas.ValtKezek[1].Be) && Allomas.Szakaszok[6].Jelzo.Szabad)
            {//feltételek: BE != null && már behaladt
                Allomas.Szakaszok[6].Jelzo.Szabad = false;
                Allomas.VisszjelTimerStart();
                kesleltetesBejarat = true;
                kesleltetes.Start();
            }
            #endregion
        }
        
        #region INDUKTOR
        private void A_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (SH != null)
                {
                    SH.InduktorTeker();
                }
            }
        }

        private void A_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (SH != null)
                {
                    SH.InduktorTekeresVege();
                }
            }
        }

        private void A_Leave(object sender, EventArgs e)
        {
            if (SH != null)
            {
                SH.InduktorTekeresVege();
            }
        }
        #endregion

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e) //NAPL
        {
            if (KoruljarasZajlik)
            {
                MessageBox.Show("Az egyik állomási vágányon éppen körüljár egy gép. Ez idő alatt nem meneszthetsz vonatot.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Naplozo napl = new Naplozo(Allomas, false, false, Ind);
            if (napl.ShowDialog() == DialogResult.OK)
            {
                Frissit();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e) //ÁLLFŐN
        {
            //TODO A_állfőn
        }

        private void pictureBox1_Click(object sender, EventArgs e) //VK1
        {
            if (this.KoruljarasZajlik)
            {
                MessageBox.Show("A váltókezelő éppen körüljárat egy gépet. Tolatás közben nem tudod elrendelni semmilyen vágányút beállítására.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Allomas.ValtKezek[1].Ki != null)
            {
                //vagy kijárat vagy ellenvonatos kijárat van >> nem nyílik meg, hanem tájékoztató MB
                MessageBox.Show("A kiválasztott váltókezelő (1) jelenleg nem rendelhető el egyetlen vágányút beállítására sem.\nAz ok a következő lehet:\nA váltókezelő már beállított vagy éppen beállít egy kijárati vágányutat.\nA következő vágányút csak a kijáró vonat leközlekedése (a vágányút feloldása) után rendelhető el.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Ind.Hibak[0]++;
            }
            else
            {
                if (Allomas.Valtok[1].Lezart && Allomas.ValtKezek[1].Be == null)
                {
                    MessageBox.Show("A kiválasztott váltókezelő (1) jelenleg nem rendelhető el egyetlen vágányút beállítására sem.\nAz ok a következő lehet:\nA váltókezelőhöz tartozó váltó le van zárva. Így csak ellenvonatos\nkijárati vágányút rendelhető el, azonban bejárati vágányutat még\nnem rendeltél el.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Ind.Hibak[1]++;
                }
                else
                {
                    Valtokezelo vk1 = new Valtokezelo(Allomas, true, Allomas.Szakaszok, Allomas.ValtKezek[1], Allomas.ValtKezek[0], true);
                    if (vk1.ShowDialog() == DialogResult.OK)
                    {
                        Frissit();
                    }
                }
            }
        }
        
        /// <summary>
        /// A körüljárás elkezdésének feltételeit vizsgálja
        /// </summary>
        void ValtoFeloldvaKoruljaras()
        {
        //    /*Akkor jut ide a kód, ha a váltót feloldotta a rendelkező egy menet leközlekedése után
        //    Ez garantálja, hogy:
        //     * A bejárati jelző Megállj-állásban van
        //     * A váltókezelő BE propertyje NULL
        //     * A váltókezelő KI propertyje vagy NULL (egy vonat van az állomáson),
        //       vagy éppen állítja be a KI vonatnak a kijáratát (két vonat van az állomáson)*/

            if (Allomas.Vonatok.Count == 1 && Allomas.Vonatok[0].KoruljarasSzukseges && !Allomas.Vonatok[0].Paros && Allomas.KoruljarasElrendelve == Allomas.Vonatok[0])
            {
                Allomas.Vonatok[0].Koruljar = true;
                this.KoruljarasZajlik = true;
                Allomas.Szakaszok[Allomas.Szakaszok.IndexOf(Allomas.Vonatok[0].Szakasz) - 1].Vonat = gep;
                SetKoruljarasTimerInterval(70 / 4 * 1000);
                koruljarasTickCounter = 0;
                koruljarasTimer.Start();
            }
            if (Allomas.Vonatok.Count == 2)
            {
                var elment = Allomas.Vonatok[0] == Allomas.KoruljarasElrendelve ? Allomas.Vonatok[1] : Allomas.Vonatok[0];
                var koruljar = Allomas.Vonatok[0] == Allomas.KoruljarasElrendelve ? Allomas.Vonatok[0] : Allomas.Vonatok[1];

                if ((Allomas.Szakaszok.IndexOf(elment.Szakasz) == 6 || Allomas.Szakaszok.IndexOf(elment.Szakasz) == -1) && elment.Paros && koruljar.KoruljarasSzukseges && !koruljar.Paros && Allomas.KoruljarasElrendelve == koruljar)
                {//két vonat van, de az egyik már kint van az állközben (az ment ki)
                    Allomas.KoruljarasElrendelve.Koruljar = true;
                    this.KoruljarasZajlik = true;
                    Allomas.Szakaszok[Allomas.Szakaszok.IndexOf(Allomas.KoruljarasElrendelve.Szakasz) - 1].Vonat = gep;
                    SetKoruljarasTimerInterval(70 / 4 * 1000);
                    koruljarasTickCounter = 0;
                    koruljarasTimer.Start();
                }
            }
        }
        //TODO A_körüljárás kódok -- koruljarasTimer_Tick ----------
        Timer koruljarasTimer = new Timer();
        int koruljarasTickCounter = 0;
        Vonat gep = new Vonat(true) { Vonatszam = "GÉP" };

        void SetKoruljarasTimerInterval(int interval)
        {
            koruljarasTimer.Interval = interval / Gyermekvasút.Modellek.Settings.SebessegOszto;
        }

        void koruljarasTimer_Tick(object sender, EventArgs e)
        {
            switch (koruljarasTickCounter)
            {
                case 0: //váltó szcs. > csucsszakasz
                    Allomas.Szakaszok[1].Vonat = gep;
                    SetKoruljarasTimerInterval(500);
                    break;
                case 1: //váltó szcs-ről lehoz
                    Allomas.Szakaszok[2].Vonat = null;
                    Allomas.Szakaszok[7].Vonat = null;
                    SetKoruljarasTimerInterval(30 / 4 * 1000 - 500);
                    break;
                case 2: //csucsszakasz > váltó szcs.
                    if (Allomas.Vonatok[0].Szakasz.Name == "K1_V1")
                    {//vonat az 1en, körüljárás a 2n
                        Allomas.Szakaszok[2].Vonat = gep;
                    }
                    else
                    {//vonat a 2n, körüljárás az 1en
                        Allomas.Szakaszok[7].Vonat = gep;
                    }
                    SetKoruljarasTimerInterval(500);
                    break;
                case 3: //csucsszakaszról lehoz
                    Allomas.Szakaszok[1].Vonat = null;
                    SetKoruljarasTimerInterval(70 / 4 * 1000 - 500);
                    break;
                case 4: //váltó szcs. > vg
                    if (Allomas.Vonatok[0].Szakasz.Name == "K1_V1")
                    {//vonat az 1en, körüljárás a 2n
                        Allomas.Szakaszok[3].Vonat = gep;
                    }
                    else
                    {//vonat a 2n, körüljárás az 1en
                        Allomas.Szakaszok[8].Vonat = gep;
                    }
                    SetKoruljarasTimerInterval(500);
                    break;
                case 5: //váltó szcs.-ről lehoz
                    Allomas.Szakaszok[2].Vonat = null;
                    Allomas.Szakaszok[7].Vonat = null;
                    SetKoruljarasTimerInterval(209 / 4 * 1000 - 500);
                    break;
                case 6: //vg > váltó szcs.
                    if (Allomas.Vonatok[0].Szakasz.Name == "K1_V1")
                    {//vonat az 1en, körüljárás a 2n
                        Allomas.Szakaszok[4].Vonat = gep;
                    }
                    else
                    {//vonat a 2n, körüljárás az 1en
                        Allomas.Szakaszok[9].Vonat = gep;
                    }
                    SetKoruljarasTimerInterval(500);
                    break;
                case 7: //vgról lehoz
                    if (Allomas.Szakaszok[8].Vonat == gep)
                    {
                        Allomas.Szakaszok[8].Vonat = null;
                    }
                    else
                    {
                        Allomas.Szakaszok[3].Vonat = null;
                    }
                    SetKoruljarasTimerInterval(70 / 4 * 1000 - 500);
                    break;
                case 8: //váltó szcs. > csucsszakasz
                    Allomas.Szakaszok[5].Vonat = gep;
                    SetKoruljarasTimerInterval(500);
                    break;
                case 9: //váltó szcs.-ről lehoz
                    Allomas.Szakaszok[4].Vonat = null;
                    Allomas.Szakaszok[9].Vonat = null;
                    SetKoruljarasTimerInterval(30 / 4 * 1000 - 500);
                    break;
                case 10: //csucsszakasz > váltó szcs.
                    if (Allomas.Vonatok[0].Szakasz.Name == "K1_V1")
                    {//vonat az 1en
                        Allomas.Szakaszok[9].Vonat = gep;
                    }
                    else
                    {//vonat a 2n
                        Allomas.Szakaszok[4].Vonat = gep;
                    }
                    SetKoruljarasTimerInterval(500);
                    break;
                case 11: //csucsszakaszról lehoz
                    Allomas.Szakaszok[5].Vonat = null;
                    SetKoruljarasTimerInterval(70 / 4 * 1000 - 500);
                    break;
                case 12: //kész
                    Allomas.Szakaszok[4].Vonat = null;
                    Allomas.Szakaszok[9].Vonat = null;
                    if (Allomas.ValtKezek[1].Ki != null && Allomas.ValtKezek[1].Ki.Virtualis)
                    {
                        Allomas.ValtKezek[1].VaganyutatNeKezdEl = true;
                        Allomas.ValtKezek[1].Bejarat = false;
                    }
                    koruljarasTimer.Stop();
                    KoruljarasVege(); //A:351
                    Frissit();
                    break;
                default:
                    break;
            }
            koruljarasTickCounter++;
        }

        void KoruljarasVege()
        {
            this.KoruljarasZajlik = false;
            Allomas.KoruljarasElrendelve.Allomas = Allomas;
            Allomas.KoruljarasElrendelve.VonatszamCsere(); //A:363
            Allomas.KoruljarasElrendelve.Koruljar = false;
            Allomas.KoruljarasElrendelve = null;
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    Allomas.AddVonat(new Vonat(new string[] { "111", "112", "113" }, Allomas.Szakaszok[6], Allomas));
        //    Frissit();
        //    button1.Enabled = false;
        //}

        private void OK1_Tick(object sender, EventArgs e)
        {
            if (OK1.Bejarat)
            {
                MessageBox.Show("A vonat már leközlekedett a bejárati vágányúton, a váltókezelő már le is zárta a bejárati jelzző blokkját, te mégsem oldottad még fel a vágányúti blokkot. (Ha a vonatnak körül kell járnia, azt csak a vágányúti blokk feloldását követően tudja megkezdeni.).\nOldd fel a vágányúti blokkot!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Ind.Hibak[2]++;
            }
            else
            {
                MessageBox.Show("A vonat már leközlekedett a kijárati vágányúton, a váltókezelő már le is zárta a kijárati jelzző blokkját, te mégsem oldottad még fel a vágányúti blokkot.\nOldd fel a vágányúti blokkot!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Ind.Hibak[2]++;
            }
        }
    }
}
