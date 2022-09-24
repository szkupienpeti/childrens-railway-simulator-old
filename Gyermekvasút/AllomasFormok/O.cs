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
    public partial class O : AllomasForm
    {
        public bool KoruljarasZajlik { get; set; }

        public RET2 RET2 { get; set; }

        public O()
        {
            InitializeComponent();
        }
        public O(Index index, Allomas allomas)
        {
            this.Allomas = allomas;
            this.Ind = index;

            InitializeComponent();
            
            RET2 = ret2;
            RET2.Ind = this.Ind;

            Allomas.ValtKezek[0].Progbar = h_vgut;

            h_vgut.Value = Allomas.ValtKezek[0].ProgressBar;

            RET2.SZAKASZOK = Allomas.Szakaszok;
            RET2.VK2 = Allomas.ValtKezek[0];
            RET2.ALLOMAS = Allomas;

            RET2.OK2 = OK2;

            OK2.Valto = Allomas.Valtok[0];

            RET2.AllForm = this;
            RET2.ValtoFeloldva += ValtoFeloldvaKoruljaras;

            koruljarasTimer.Tick += koruljarasTimer_Tick;
        }
        
        private void O_Load(object sender, EventArgs e)
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
            Allomas.ValtKezek[0].FormOpen = true;
        }

        private void O_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hr.Hide();
            this.Hide();
            e.Cancel = true;

            Allomas.ValtKezek[0].FormOpen = false;
        }

        public override void Frissit()
        {
            base.Frissit();

            #region VÁGÁNYÚTOLDÁS
            if (Allomas.ValtKezek[0].Ki != null)
            {
                if (Allomas.ValtKezek[0].Vagany == 1 && !Allomas.Szakaszok[7].Foglalt && Allomas.Szakaszok[1].Foglalt && !Allomas.Szakaszok[1].Vonat.Paros)
                {
                    OK2.Start2(false);
                    OK2.Elinditva = true;
                }
                if (Allomas.ValtKezek[0].Vagany == 2 && !Allomas.Szakaszok[2].Foglalt && Allomas.Szakaszok[1].Foglalt && !Allomas.Szakaszok[1].Vonat.Paros)
                {
                    OK2.Start2(false);
                    OK2.Elinditva = true;
                }
            }

            if (Allomas.Valtok[0].Lezart == false)
            {//2
                OK2.Stop();
                OK2.Elinditva = false;
            }
            #endregion
        }

        private void pictureBox3_Click(object sender, EventArgs e) //NAPL
        {
            if (KoruljarasZajlik)
            {
                MessageBox.Show("Az egyik állomási vágányon éppen körüljár egy gép. Ez idő alatt nem meneszthetsz vonatot.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Naplozo napl = new Naplozo(Allomas, false, true, Ind);
            if (napl.ShowDialog() == DialogResult.OK)
            {
                Frissit();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e) //VK2
        {
            if (this.KoruljarasZajlik)
            {
                MessageBox.Show("A váltókezelő éppen körüljárat egy gépet. Tolatás közben nem tudod elrendelni semmilyen vágányút beállítására.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Allomas.ValtKezek[0].Ki != null)
            {
                //vagy kijárat vagy ellenvonatos kijárat van >> nem nyílik meg, hanem tájékoztató MB
                MessageBox.Show("A kiválasztott váltókezelő (2) jelenleg nem rendelhető el egyetlen vágányút beállítására sem.\nAz ok a következő lehet:\nA váltókezelő már beállított vagy éppen beállít egy kijárati vágányutat.\nA következő vágányút csak a kijáró vonat leközlekedése (a vágányút feloldása) után rendelhető el.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Ind.Hibak[0]++;
            }
            else
            {
                if (Allomas.Valtok[0].Lezart && Allomas.ValtKezek[0].Be == null)
                {
                    MessageBox.Show("A kiválasztott váltókezelő (2) jelenleg nem rendelhető el egyetlen vágányút beállítására sem.\nAz ok a következő lehet:\nA váltókezelőhöz tartozó váltó le van zárva. Így csak ellenvonatos\nkijárati vágányút rendelhető el, azonban bejárati vágányutat még\nnem rendeltél el.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Ind.Hibak[1]++;
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
        }

        private void OK2_Tick(object sender, EventArgs e)
        {
            if (OK2.Bejarat)
            {
                MessageBox.Show("Az A jelű bejárati jelzőt már visszaállítottad Megállj-állásba, ám a 2-es váltót nem oldottad még fel. A váltó szabványos állása az oldott egyenes. A váltókat vonatmentes időben szabványos állásban kell tartani.\nNe feledkezz meg a váltó feloldásáról!\nAmíg nem oldod fel a váltót, a váltókezelőt csak ellenvonatos kijárati vágányút beállítására fogod tudni elrendelni!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Ind.Hibak[2]++;
            }
            else
            {
                MessageBox.Show("A vonat már leközlekedett a vágányúton, ám a 2-es váltót nem oldottad még fel. A váltó szabványos állása az oldott egyenes. A váltókat vonatmentes időben szabványos állásban kell tartani.\nNe feledkezz meg a váltó feloldásáról!\nAmíg nem oldod fel a váltót, a váltókezelőt nem fogod tudni elrendelni semmilyen vágányút beállítására!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Ind.Hibak[2]++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Allomas.AddVonat(new Vonat(new string[]{"111"}, Allomas.Szakaszok[3], Allomas));
            Frissit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Allomas.AddVonat(new Vonat(new string[]{"113"}, Allomas.Szakaszok[8], Allomas));
            Frissit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Allomas.AddVonat(new Vonat(new string[]{"222", "223", "224"}, Allomas.Szakaszok[0], Allomas));
            Frissit();
        }

        /// <summary>
        /// A körüljárás elkezdésének feltételeit vizsgálja
        /// </summary>
        void ValtoFeloldvaKoruljaras()
        {
            /*Akkor jut ide a kód, ha a váltót feloldotta a rendelkező egy menet leközlekedése után
            Ez garantálja, hogy:
             * A bejárati jelző Megállj-állásban van
             * A váltókezelő BE propertyje NULL
             * A váltókezelő KI propertyje vagy NULL (egy vonat van az állomáson),
               vagy éppen állítja be a KI vonatnak a kijáratát (két vonat van az állomáson)*/
            
            if (Allomas.Vonatok.Count == 1 && Allomas.Vonatok[0].KoruljarasSzukseges && Allomas.Vonatok[0].Paros && Allomas.KoruljarasElrendelve == Allomas.Vonatok[0])
            {
                Allomas.Vonatok[0].Koruljar = true;
                this.KoruljarasZajlik = true;
                Allomas.Szakaszok[Allomas.Szakaszok.IndexOf(Allomas.Vonatok[0].Szakasz) + 1].Vonat = gep;
                SetKoruljarasTimerInterval(70 / 4 * 1000);
                koruljarasTickCounter = 0;
                koruljarasTimer.Start();
            }
            if (Allomas.Vonatok.Count == 2)
            {
                var elment = Allomas.Vonatok[0] == Allomas.KoruljarasElrendelve ? Allomas.Vonatok[1] : Allomas.Vonatok[0];
                var koruljar = Allomas.Vonatok[0] == Allomas.KoruljarasElrendelve ? Allomas.Vonatok[0] : Allomas.Vonatok[1];

                if ((Allomas.Szakaszok.IndexOf(elment.Szakasz) == 0 || Allomas.Szakaszok.IndexOf(elment.Szakasz) == -1) && !elment.Paros && koruljar.KoruljarasSzukseges && koruljar.Paros && Allomas.KoruljarasElrendelve == koruljar)
                {//két vonat van, de az egyik már kint van az állközben (az ment ki)
                    Allomas.KoruljarasElrendelve.Koruljar = true;
                    this.KoruljarasZajlik = true;
                    Allomas.Szakaszok[Allomas.Szakaszok.IndexOf(Allomas.KoruljarasElrendelve.Szakasz) + 1].Vonat = gep;
                    SetKoruljarasTimerInterval(70 / 4 * 1000);
                    koruljarasTickCounter = 0;
                    koruljarasTimer.Start();
                }
            }
        }

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
                    Allomas.Szakaszok[5].Vonat = gep;
                    SetKoruljarasTimerInterval(500);
                    break;
                case 1: //váltó szcs-ről lehoz
                    Allomas.Szakaszok[4].Vonat = null;
                    Allomas.Szakaszok[9].Vonat = null;
                    SetKoruljarasTimerInterval(30 / 4 * 1000 - 500);
                    break;
                case 2: //csucsszakasz > váltó szcs.
                    if (Allomas.Vonatok[0].Szakasz.Name == "K1_V1")
                    {//vonat az 1en, körüljárás a 2n
                        Allomas.Szakaszok[9].Vonat = gep;
                    }
                    else
                    {//vonat a 2n, körüljárás az 1en
                        Allomas.Szakaszok[4].Vonat = gep;
                    }
                    SetKoruljarasTimerInterval(500);
                    break;
                case 3: //csucsszakaszról lehoz
                    Allomas.Szakaszok[5].Vonat = null;
                    SetKoruljarasTimerInterval(70 / 4 * 1000 - 500);
                    break;
                case 4: //váltó szcs. > vg
                    if (Allomas.Vonatok[0].Szakasz.Name == "K1_V1")
                    {//vonat az 1en, körüljárás a 2n
                        Allomas.Szakaszok[8].Vonat = gep;
                    }
                    else
                    {//vonat a 2n, körüljárás az 1en
                        Allomas.Szakaszok[3].Vonat = gep;
                    }
                    SetKoruljarasTimerInterval(500);
                    break;
                case 5: //váltó szcs.-ről lehoz
                    Allomas.Szakaszok[4].Vonat = null;
                    Allomas.Szakaszok[9].Vonat = null;
                    SetKoruljarasTimerInterval(209 / 4 * 1000 - 500);
                    break;
                case 6: //vg > váltó szcs.
                    if (Allomas.Vonatok[0].Szakasz.Name == "K1_V1")
                    {//vonat az 1en, körüljárás a 2n
                        Allomas.Szakaszok[7].Vonat = gep;
                    }
                    else
                    {//vonat a 2n, körüljárás az 1en
                        Allomas.Szakaszok[2].Vonat = gep;
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
                    Allomas.Szakaszok[1].Vonat = gep;
                    SetKoruljarasTimerInterval(500);
                    break;
                case 9: //váltó szcs.-ről lehoz
                    Allomas.Szakaszok[2].Vonat = null;
                    Allomas.Szakaszok[7].Vonat = null;
                    SetKoruljarasTimerInterval(30 / 4 * 1000 - 500);
                    break;
                case 10: //csucsszakasz > váltó szcs.
                    if (Allomas.Vonatok[0].Szakasz.Name == "K1_V1")
                    {//vonat az 1en
                        Allomas.Szakaszok[2].Vonat = gep;
                    }
                    else
                    {//vonat a 2n
                        Allomas.Szakaszok[7].Vonat = gep;
                    }
                    SetKoruljarasTimerInterval(500);
                    break;
                case 11: //csucsszakaszról lehoz
                    Allomas.Szakaszok[1].Vonat = null;
                    SetKoruljarasTimerInterval(70 / 4 * 1000 - 500);
                    break;
                case 12: //kész
                    Allomas.Szakaszok[2].Vonat = null;
                    Allomas.Szakaszok[7].Vonat = null;                    
                    if (Allomas.ValtKezek[0].Ki != null && Allomas.ValtKezek[0].Ki.Virtualis)
                    {
                        Allomas.ValtKezek[0].Bejarat = false;
                        
                    }
                    koruljarasTimer.Stop();
                    KoruljarasVege();
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
            Allomas.KoruljarasElrendelve.VonatszamCsere(); //ez valahogy exceptiont dob, ha visszjelt adok közben -- ???
            Allomas.KoruljarasElrendelve.Koruljar = false;
            Allomas.KoruljarasElrendelve = null;
        }
    }
}
