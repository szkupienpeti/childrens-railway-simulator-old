using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gyermekvasút.AllomasFormok;
using Gyermekvasút.Modellek;
using System.ServiceModel;
using System.Xml;
using System.IO;

namespace Gyermekvasút
{
    public partial class Index : Form
    {//MEGÁLLAPODÁS SZERINT MINDEN SZAKASZ JELZO-JÉN ANNAK A PÁROS FELÉN LÉVŐ JELZŐT ÉRTJÜK!
        bool easteregg_GOL;
        bool g;
        bool o;
        //TODO Minden ValtKez-es állomás _open setterében inicializálni kell a ValtKezek összes tagjának .FormOpen-jét! (A, O)
        //TODO Minden ValtKezes AllomasForm _Loadjában inicializálni kell a Progbarok .Value-ját a ValtKezek .Progressbar értékéhez!

        //TODO Minden AllomasForm FormClosingjába: Hr.Hide(); this.Hide(); e.Cancel = true; -- else: ObjectDisposedException

        Ora ora = new Ora();

        AllomasForm[] allomasFormPeldanyok = new AllomasForm[7];
        public AllomasForm[] AllomasFormPeldanyok
        {
            get { return allomasFormPeldanyok; }
        }

        public bool S_open
        {
            get { return allomasFormPeldanyok[4] == null ? false : true; }
        }
        bool admin_open = false;
        public bool Admin_open
        {
            get { return admin_open; }
            set { admin_open = value; }
        }
        bool i_open = false;
        public bool I_open
        {
            get { return i_open; }
            set
            {
                i_open = value;
                allomasok[3].ValtKezek[0].FormOpen = i_open;
                allomasok[3].ValtKezek[1].FormOpen = i_open;
            }
        }
        bool h_open = false;
        public bool H_open
        {
            get { return h_open; }
            set
            {
                h_open = value;
                allomasok[5].ValtKezek[0].FormOpen = h_open;
                allomasok[5].ValtKezek[1].FormOpen = h_open;
            }
        }
        bool l_open = false;
        public bool L_open
        {
            get { return l_open; }
            set
            {
                l_open = value;
                allomasok[2].ValtKezek[0].FormOpen = l_open;
                allomasok[2].ValtKezek[1].FormOpen = l_open;
            }
        }
        bool konzol_open = false;
        public bool Konzol_open
        {
            get { return konzol_open; }
            set { konzol_open = value; }
        }
        bool u_open = false;
        public bool U_open
        {
            get { return u_open; }
            set { u_open = value; }
        }

        List<double>[] csoportHibak = new List<double>[16]; //a nulladik elem az átlag!
        public List<double>[] CsoportHibak
        {
            get { return csoportHibak; }
        }

        int[] hasznalatSzamlaloTomb = new int[15];
        public int[] HasznalatSzamlaloTomb
        {
            get { return hasznalatSzamlaloTomb; }
        }

        List<int> hibak = new List<int>();
        public List<int> Hibak
        {
            get { return hibak; }
        }

        int csoport; //hanyadik csoport használja a progit
        public int Csoport
        {
            get { return csoport; }
            set { csoport = value; }
        }

        public string RomaiCsoportString
        {
            get
            {
                switch (Csoport)
                {
                    case 1: return "I.";
                    case 2: return "II.";
                    case 3: return "III.";
                    case 4: return "IV.";
                    case 5: return "V.";
                    case 6: return "VI.";
                    case 7: return "VII.";
                    case 8: return "VIII.";
                    case 9: return "IX.";
                    case 10: return "X.";
                    case 11: return "XI.";
                    case 12: return "XII.";
                    case 13: return "XIII.";
                    case 14: return "XIV.";
                    case 15: return "XV.";
                    default: return "TESZT";
                }
            }
        }

        List<string> hibaNevek = new List<string>(); //TODO hibák nevei
        public List<string> HibaNevek
        {
            get { return hibaNevek; }
            set { hibaNevek = value; }
        }

        ServiceHost host;
        Halozat halozat;
        StartScreen startscreen;

        public static List<string> AllomasNevek = new List<string>();

        List<Allomas> allomasok = new List<Allomas>();
        public List<Allomas> Allomasok
        {
            get { return allomasok; }
        }

        static Index()
        {
            AllomasNevek.Add("Széchenyi-hegy");
            AllomasNevek.Add("Csillebérc");
            AllomasNevek.Add("Virágvölgy");
            AllomasNevek.Add("János-hegy");
            AllomasNevek.Add("Szépjuhászné");
            AllomasNevek.Add("Hárs-hegy");
            AllomasNevek.Add("Hűvösvölgy");
        }

        public static string GetSzomszedAllomasNev(string allNev, bool kpFele)
        {
            if (kpFele)
            {
                if (allNev == "Virágvölgy")
                {
                    return "Széchenyi-hegy";
                }
                try
                {
                    return AllomasNevek[AllomasNevek.IndexOf(allNev) - 1];
                }
                catch (Exception) { return "#EXCEPTION_CAUGHT"; }
            }
            else
            {
                if (allNev == "Széchenyi-hegy")
                {
                    return "Virágvölgy";
                }
                try
                {
                    return AllomasNevek[AllomasNevek.IndexOf(allNev) + 1];
                }
                catch (Exception) { return "#EXCEPTION_CAUGHT"; }
            }
        }

        #region S
        public int s_ko = 0;
        public bool s_valtovil = false;
        public bool s_zavar = false;
        public bool s_zavar_kezelve = false;
        public bool s_zavar_lejatszas = false;
        #endregion

        #region U
        public bool ejjel = false;
        public bool u_valtovil = false;
        #endregion

        public Index(StartScreen ss)
        {//OFFLINE MÓD -- ÖSSZES ÁLLOMÁS
            startscreen = ss;
            InitializeComponent();
            LoadXmlMenetrend();
            Felepit();
        } //a StartScreen hívja meg
        
        public Index(Allomas allomas, ServiceHost host, Halozat hal, StartScreen ss) //a Halozat form hívja meg
        {//HÁLÓZATOS MÓD -- CSAK EGY ÁLLOMÁS
            InitializeComponent();
            this.host = host;
            LoadXmlMenetrend();
            HalozatFelepit(allomas);
            halozat = hal;
            startscreen = ss;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AllomasFormPeldanyok[4].Show();
            if (AllomasFormPeldanyok[4].Hr != null && !AllomasFormPeldanyok[4].Hr.Visible)
                AllomasFormPeldanyok[4].Hr.Show(AllomasFormPeldanyok[4]);
        }

        XmlDocument mrdoc = new XmlDocument();
        void LoadXmlMenetrend()
        {
            //get .gyvmr files
            string folder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string filter = "*.gyvmr";
            string[] files = Directory.GetFiles(folder, filter);

            string menetrendPath = "";
            if (files.Length == 0)
            {
                MessageBox.Show("A program mappájában nem található menetrendfájl (.gyvmr), így a program menetrend betöltése nélkül indul el.", "Menetrend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Program.Menetrend = "nincs";
                Program.Ido = DateTime.Now;
                Gyermekvasút.Modellek.Settings.SebessegOszto = 1;
            }
            else if (files.Length == 1)
            {
                menetrendPath = files[0];
                //Program.Menetrend = files[0].Split('\\')[files[0].Split('\\').Length - 1].Split('.')[0];
                Program.Menetrend = files[0].Split('\\')[files[0].Split('\\').Length - 1].Substring(0, files[0].Split('\\')[files[0].Split('\\').Length - 1].Length - 6);
                mrStatusLabelString = "Menetrend: " + Program.Menetrend;
            }
            else
            {
                string[] menetrendKodok = new string[files.Length];
                for (int i = 0; i < menetrendKodok.Length; i++)
                {
                    menetrendKodok[i] = files[i].Split('\\')[files[i].Split('\\').Length - 1];
                }
                ListViewItem[] lvis = new ListViewItem[menetrendKodok.Length];
                for (int i = 0; i < menetrendKodok.Length; i++)
                {
                    lvis[i] = new ListViewItem(menetrendKodok[i]);
                }
                ColumnHeader[] chs = new ColumnHeader[1] { new ColumnHeader("Menetrendi kód") };
                Valaszto mrVal = new Valaszto("Mednetrend", "Válassz egy menetrendet!", lvis, chs);
                mrVal.ShowDialog();
                string pathEnd = mrVal.SelectedListViewItem.SubItems[0].Text;
                for (int i = 0; i < menetrendKodok.Length; i++)
                {
                    if (menetrendKodok[i] == pathEnd)
                    {
                        menetrendPath = files[i];
                        Program.Menetrend = pathEnd.Split('.')[0];
                        mrStatusLabelString = "Menetrend: " + Program.Menetrend;
                        break;
                    }
                }
            }

            //menetrendPath-ban van a .gyvmr fájl elérési útja
            mrdoc.Load(menetrendPath.Split('\\')[menetrendPath.Split('\\').Length - 1]);

            //Keszitok
            Program.MenetrendKeszitok = new string[mrdoc.DocumentElement.SelectNodes("About")[0].SelectNodes("Keszito").Count];
            for (int i = 0; i < Program.MenetrendKeszitok.Length; i++)
            {
                Program.MenetrendKeszitok[i] = mrdoc.DocumentElement.SelectNodes("About")[0].SelectNodes("Keszito")[i].Attributes[0].Value;
            }

            XmlNode general = mrdoc.DocumentElement.SelectNodes("Altalanos")[0];
            //KezdesiIdo
            Program.Ido = Convert.ToDateTime(general["KezdesiIdo"].Attributes[0].Value);
            //SebessegOszto
            Gyermekvasút.Modellek.Settings.SebessegOszto = Convert.ToInt32(general["Gyorsitas"].Attributes[0].Value);
            timer1.Interval = 60000 / Gyermekvasút.Modellek.Settings.SebessegOszto;
        }

        public static Index IndInstance = null;
        private void Index_Load(object sender, EventArgs e)
        {
            IndInstance = this;
            owl.Hide();
            ora.Show();
            ora.RefreshTime();

            string today = DateTime.Today.ToString("d"); //yyyy.mm.dd.
            StreamReader sr = new StreamReader("EvesNaptar.gyv");
            string[] naptar = sr.ReadToEnd().Split(';');
            sr.Close();
            for (int i = 0; i < naptar.Length; i++)
            {
                naptar[i] = naptar[i].Replace("\r", "").Replace("\n", "");
            }
            for (int i = 0; i < naptar.Length; i += 2)
            {
                if (naptar[i] == today)
                {
                    Csoport = Convert.ToInt32(naptar[i + 1]);
                    break;
                }
            }

            HibaNevek.Add("ValtKezElrendeltKijarat");
            HibaNevek.Add("ValtKezLezartValto");
            HibaNevek.Add("ValtoFeloldas");
            HibaNevek.Add("JelzoVaganyutReszbenFelhasznalva");
            HibaNevek.Add("JelzoVaganyutNincsFelhasznalva");
            HibaNevek.Add("JelzoElrendelesNelkul");
            HibaNevek.Add("JelzoUjraSzabadraAllitas");
            HibaNevek.Add("ValtoFeloldasFelhasznalatlanVaganyut");
            HibaNevek.Add("ValtoLezarasVaganyutBeallitas");
            HibaNevek.Add("ValtoLezarasElrendelesNelkul");
            HibaNevek.Add("SzamlalosOlomzarasGomb");
            HibaNevek.Add("Kenyszeroldas");
            HibaNevek.Add("TelHelytelenCsengetes");
            HibaNevek.Add("TelHelytelenVisszacsengetes");
            HibaNevek.Add("TelEngDupla");
            HibaNevek.Add("TelEngAllTav");
            HibaNevek.Add("TelEngVanIndulasiHiany");
            HibaNevek.Add("TelEngAzonosSzembe");
            HibaNevek.Add("MenesztesEngNelkul");
            HibaNevek.Add("MenesztesSzembe");
            HibaNevek.Add("MenesztesVorosbeValto");
            HibaNevek.Add("TelVisszjelJelzoSzabad");
            HibaNevek.Add("TelEngAzonosEllenvonatVolt");
            HibaNevek.Add("TelIndKeresztVisszjelHiany");
            HibaNevek.Add("TelIndHiany");
            HibaNevek.Add("TelEngLejart");
            HibaNevek.Add("TelVisszjelHiany");
            HibaNevek.Add("TelEngMasikVaganyut");
            HibaNevek.Add("TelEngKoruljaras");

            for (int i = 0; i < CsoportHibak.Length; i++)
            {
                CsoportHibak[i] = new List<double>();
            }

            for (int i = 0; i < HibaNevek.Count; i++) //feltöltés nullákkal
            {
                Hibak.Add(0);
                CsoportHibak[0].Add(0);
            }

            XmlDocument doc = new XmlDocument();
            doc.Load("LCSV.gyv");
            XmlNodeList nodelist = doc.DocumentElement.SelectNodes("Csoport");
            
            foreach (XmlNode node in nodelist)
            {
                HasznalatSzamlaloTomb[Convert.ToInt32(node.Attributes["Csoportszam"].Value) - 1] = Convert.ToInt32(node.Attributes["HasznalatSzamlalo"].Value);
                for (int i = 0; i < HibaNevek.Count; i++)
                {
                    CsoportHibak[Convert.ToInt32(node.Attributes["Csoportszam"].Value)].Add(Convert.ToInt32(node.Attributes[HibaNevek[i]].Value));
                }
            }            

            double tempErtek = 0;
            int tempHasznalat = 0;
            for (int i = 0; i < HasznalatSzamlaloTomb.Length; i++)
            {
                tempHasznalat += HasznalatSzamlaloTomb[i];
            }
            for (int i = 0; i < HibaNevek.Count; i++)
            {//minden hibán végigmegy, és kiszámítja az átlagot
                for (int j = 1; j <= 15; j++)
                {//csoportokon végigmegy
                    tempErtek += CsoportHibak[j][i];
                }
                if (tempHasznalat == 0)
                {
                    CsoportHibak[0][i] = 0;
                }
                else
                {
                    CsoportHibak[0][i] = tempErtek / tempHasznalat;
                }
                tempErtek = 0;
            }

            ido.Text = mrStatusLabelString + " • Idő: " + Program.Ido.ToShortTimeString() + " • Felhasználó csoport: " + RomaiCsoportString + " csoport";
            timer1.Enabled = true;
            timer1.Start();
            notifyIcon1.ShowBalloonTip(200, "Üdvözlet a szimulátorban!", "Ha bármi kérdésed van, tedd fel az ifivezetődnek!\n\nHajrá! :)", ToolTipIcon.Info);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AllomasFormPeldanyok[3].Show();
            if (AllomasFormPeldanyok[3].Hr != null && !AllomasFormPeldanyok[3].Hr.Visible)
                AllomasFormPeldanyok[3].Hr.Show(AllomasFormPeldanyok[3]);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Program.Ido = Program.Ido.AddMinutes(1);
            ido.Text = mrStatusLabelString + " • Idő: " + Program.Ido.ToShortTimeString() + " • Felhasználó csoport: " + RomaiCsoportString + " csoport";
            ora.RefreshTime();
            Allomas.Ido = Program.Ido;
        }

        private void Index_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.G && e.Modifiers == (Keys.Alt | Keys.Control))
            {
                g = true;
                Timer t = new Timer();
                t.Tick += new EventHandler(t_Tick);
                t.Interval = 1000;
                t.Start();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.A && g && e.Modifiers == (Keys.Alt | Keys.Control))
            {
                Jelszo j = new Jelszo();
                DialogResult dr = j.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    if (admin_open)
                    {
                        MessageBox.Show("Védett tartalom: már meg van nyitva!", "Védett tartalom", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        bool halozatos = host == null ? false : true;
                        AdminPanel ap = new AdminPanel(this, halozatos);
                        ap.Show();
                    }
                }
                else if (dr == DialogResult.Yes)
                {
                    MessageBox.Show("Nem rossz, nem rossz; de azért ne piszkáld! ;)\n[Miket nem ír ez a program... :o]", "Védett tartalom", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("Védett tartalom: helytelen jelszó!\nNe piszkáld!", "Védett tartalom", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.O && g)
            {
                o = true;
                Timer t2 = new Timer();
                t2.Tick += t2_Tick;
                t2.Interval = 1000;
                t2.Start();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.L && o)
            {
                if (easteregg_GOL)
                {
                    return;
                }

                easteregg_GOL = true;
                GameOfLife gol = new GameOfLife();
                gol.Show();
                gol.Close();
                
                e.Handled = true;
            }
        }

        void t2_Tick(object sender, EventArgs e)
        {
            (sender as Timer).Stop();
            o = false;
        }

        void t_Tick(object sender, EventArgs e)
        {
            (sender as Timer).Stop();
            g = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AllomasFormPeldanyok[2].Show();
            if (AllomasFormPeldanyok[2].Hr != null && !AllomasFormPeldanyok[2].Hr.Visible)
                AllomasFormPeldanyok[2].Hr.Show(AllomasFormPeldanyok[2]);
        }

        void Felepit()
        {
            Allomas szechenyihegy = new Allomas(false, 104, false, 100, 1700) { Name = "Széchenyi-hegy" };
            allomasok.Add(szechenyihegy);
            Allomas csilleberc = new Allomas(true, 100, true, 1700, 1300) { Name = "Csillebérc" };
            allomasok.Add(csilleberc);
            Allomas viragvolgy = new Allomas(false, 74, true, 1300, 1500) { Name = "Virágvölgy" };
            allomasok.Add(viragvolgy);
            Allomas janoshegy = new Allomas(false, 58, true, 1500, 2200) { Name = "János-hegy" };
            allomasok.Add(janoshegy);
            Allomas szepjuhaszne = new Allomas(true, 111, false, 2200, 2000) { Name = "Szépjuhászné" };
            allomasok.Add(szepjuhaszne);
            Allomas harshegy = new Allomas(false, 119, true, 2000, 2500) { Name = "Hárs-hegy" };
            allomasok.Add(harshegy);
            Allomas huvosvolgy = new Allomas(false, 209, true, 2500, 100) { Name = "Hűvösvölgy" };
            allomasok.Add(huvosvolgy);

            Type[] formok = new Type[] { typeof(A), typeof(U), typeof(L), typeof(I), typeof(S), typeof(H), typeof(O) };
            for (int i = 0; i < formok.Length; i++)
            {//feltöltjük az AllomasFormPeldanyok-at, meghívjuk a FormStarted-ot >> a btn_Click-ben már nem kell viszgálni, hogy null-e
                AllomasFormPeldanyok[i] = (AllomasForm)Activator.CreateInstance(formok[i], new object[] { this, allomasok[i] });
                AllomasFormPeldanyok[i].FormStarted();
            }

            for (int i = 0; i < Allomasok.Count; i++)
            {
                Allomasok[i].VonatMegallt += new VonatJelzoDelegate(Index_VonatMegallt);

                if (i == 0)
                {
                    Allomasok[0].VegpontiAllomas = Allomasok[1];
                }
                else if (i == Allomasok.Count - 1)
                {
                    Allomasok[Allomasok.Count - 1].KezdopontiAllomas = Allomasok[Allomasok.Count - 2];
                }
                else
                {
                    Allomasok[i].KezdopontiAllomas = Allomasok[i - 1];
                    Allomasok[i].VegpontiAllomas = Allomasok[i + 1];
                }
            }
            //Szépjuhászné: Vaganyutak
            szepjuhaszne.Vaganyutak.Add(new Vaganyut(szepjuhaszne.Jelzok[0], "Bejárat János-hegy állomás felől az I. vágányra", true, szepjuhaszne.Szakaszok[1], szepjuhaszne.Szakaszok[7], szepjuhaszne.Szakaszok[8], "vgut_s_A_V1"));
            szepjuhaszne.Vaganyutak.Add(new Vaganyut(szepjuhaszne.Jelzok[0], "Bejárat János-hegy állomás felől a II. vágányra", true, szepjuhaszne.Szakaszok[1], szepjuhaszne.Szakaszok[2], szepjuhaszne.Szakaszok[3], "vgut_s_A_V2"));
            szepjuhaszne.Vaganyutak.Add(new Vaganyut(szepjuhaszne.Jelzok[4], "Kijárat Hárs-hegy állomás felé a II. vágányról", false, szepjuhaszne.Szakaszok[3], szepjuhaszne.Szakaszok[4], szepjuhaszne.Szakaszok[5], "vgut_s_V2_ki"));
            szepjuhaszne.Vaganyutak.Add(new Vaganyut(szepjuhaszne.Jelzok[3], "Kijárat Hárs-hegy állomás felé az I. vágányról", false, szepjuhaszne.Szakaszok[8], szepjuhaszne.Szakaszok[9], szepjuhaszne.Szakaszok[5], "vgut_s_V1_ki"));
            szepjuhaszne.Vaganyutak.Add(new Vaganyut(szepjuhaszne.Jelzok[5], "Bejárat Hárs-hegy állomás felől a II. vágányra", true, szepjuhaszne.Szakaszok[5], szepjuhaszne.Szakaszok[4], szepjuhaszne.Szakaszok[3], "vgut_s_B_K2"));
            szepjuhaszne.Vaganyutak.Add(new Vaganyut(szepjuhaszne.Jelzok[5], "Beajárat Hárs-hegy állomás felől az I. vágányra", true, szepjuhaszne.Szakaszok[5], szepjuhaszne.Szakaszok[9], szepjuhaszne.Szakaszok[8], "vgut_s_B_K1"));
            szepjuhaszne.Vaganyutak.Add(new Vaganyut(szepjuhaszne.Jelzok[2], "Kijárat János-hegy állomás felé a II. vágányról", false, szepjuhaszne.Szakaszok[3], szepjuhaszne.Szakaszok[2], szepjuhaszne.Szakaszok[1], "vgut_s_K2_ki"));
            szepjuhaszne.Vaganyutak.Add(new Vaganyut(szepjuhaszne.Jelzok[1], "Kijárat János-hegy állomás felé az I. vágányról", false, szepjuhaszne.Szakaszok[8], szepjuhaszne.Szakaszok[7], szepjuhaszne.Szakaszok[1], "vgut_s_K1_ki"));
            szepjuhaszne.ValtKezek.Clear();
            szepjuhaszne.Valtok[0] = szepjuhaszne.Valtok[0].ConvertValtoToValtoS();
            szepjuhaszne.Valtok[1] = szepjuhaszne.Valtok[1].ConvertValtoToValtoS();
            (szepjuhaszne.Szakaszok[1] as CsucsSzakasz).Valto = szepjuhaszne.Valtok[0];
            (szepjuhaszne.Szakaszok[5] as CsucsSzakasz).Valto = szepjuhaszne.Valtok[1];
            for (int i = 2; i < 6; i++)
            {
                if (i != 4)
                {
                    for (int j = 1; j < 5; j++)
                    {
                        allomasok[i].Jelzok[j].Szabad = true;
                    }
                }
            }
            harshegy.ValtKezek[0].ReteszesLezaras = true;
            harshegy.ValtKezek[1].ReteszesLezaras = true;
            huvosvolgy.ValtKezek[0].ReteszesLezaras = true;
            huvosvolgy.ValtKezek[1].ReteszesLezaras = true;

            huvosvolgy.Szakaszok[3].Jelzo.Name = "B";
            huvosvolgy.Szakaszok[8].Jelzo.Name = "C";

            szechenyihegy.Szakaszok[6].Jelzo.Name = "A";
            szechenyihegy.Szakaszok[4].Jelzo.Name = "C";
            szechenyihegy.Szakaszok[9].Jelzo.Name = "B";

            szechenyihegy.ValtKezek[0].SzechenyiHegy = true;
            szechenyihegy.ValtKezek[1].SzechenyiHegy = true;

            //TODO ahol a kijárati jelzők nevei (U) nem V1-szerűek, ott ezt be kell állítani (~O)

            //Vonatok
            XmlNodeList vonatok = mrdoc.DocumentElement.SelectNodes("Vonatok")[0].SelectNodes("Vonat");
            foreach (XmlNode item in vonatok)
            {
                Vonat v = new Vonat(false);
                //Vonatszam, KoruljarasSzukseges
                foreach (XmlAttribute attr in item.Attributes)
                {
                    if (attr.Name == "Vonatszam")
                    {
                        v.Vonatszam = attr.Value;
                    }
                    else if (attr.Name == "Koruljar")
                    {
                        v.KoruljarasSzukseges = Convert.ToBoolean(attr.Value);
                    }
                }
                //Allomas, Szakasz
                foreach (XmlAttribute attr in item.SelectNodes("Letrehozas")[0].Attributes)
                {
                    if (attr.Name == "Allomas")
                    {
                        //string >> Allomas object
                        Allomasok[AllomasNevek.IndexOf(attr.Value)].AddVonat(v);
                        Allomasok[AllomasNevek.IndexOf(attr.Value)].VisszjeltAdtam.Add(v.Vonatszam);
                    }
                    else if (attr.Name == "Vagany")
                    {
                        if ((attr.Value == "1" && v.Allomas.ElsoVaganyEgyenes) || (attr.Value == "2" && v.Allomas.ElsoVaganyEgyenes == false))
                        {//[3]
                            v.Szakasz = v.Allomas.Szakaszok[3];
                            v.Szakasz.Vonat = v;
                        }
                        else if ((attr.Value == "2" && v.Allomas.ElsoVaganyEgyenes) || (attr.Value == "1" && v.Allomas.ElsoVaganyEgyenes == false))
                        {//[8]
                            v.Szakasz = v.Allomas.Szakaszok[8];
                            v.Szakasz.Vonat = v;
                        }
                    }
                }
                //Forda
                List<string> tempForda = new List<string>();
                foreach (XmlNode fordulo in item.SelectNodes("Forda")[0].SelectNodes("Fordulo"))
                {
                    tempForda.Add(fordulo.Attributes[0].Value);
                }
                v.Forda = new string[tempForda.Count];
                for (int i = 0; i < tempForda.Count; i++)
                {
                    v.Forda[i] = tempForda[i];
                }
                //Elindul()
                v.Elindul();
            }
        }

        string mrStatusLabelString = "";

        void HalozatFelepit(Allomas allomas)
        {
            for (int i = 0; i < 7; i++)
            {
                Allomasok.Add(null);
            }
            foreach (var button in this.Controls)
            {
                if (button is Button)
                {
                    (button as Button).Enabled = false;
                }
            }

            btnL.BackgroundImage = Gyermekvasút.Properties.Resources.L_icon_kép2FF;
            btnI.BackgroundImage = Gyermekvasút.Properties.Resources.I_icon_képFF;
            btnS.BackgroundImage = Gyermekvasút.Properties.Resources.S_icon_képFF;
            btnH.BackgroundImage = Gyermekvasút.Properties.Resources.H_icon_képFF;
            btnU.BackgroundImage = Gyermekvasút.Properties.Resources.U_icon_képFF;
            btnO.BackgroundImage = Gyermekvasút.Properties.Resources.O_icon_képFF;
            btnA.BackgroundImage = Gyermekvasút.Properties.Resources.A_icon_képFF;

            switch (allomas.Name)
            {
                case "Széchenyi-hegy":
                    allomas.AllomasFeltolt(false, 104, false, 100, 1700);
                    allomas.ValtKezek[0].SzechenyiHegy = true;
                    allomas.ValtKezek[1].SzechenyiHegy = true;
                    allomas.VonatMegallt += new VonatJelzoDelegate(Index_VonatMegallt);
                    allomasok[0] = allomas;
                    btnA.Enabled = true;
                    btnA.BackgroundImage = Gyermekvasút.Properties.Resources.A_icon_kép;
                    allomas.Szakaszok[6].Jelzo.Name = "A";
                    allomas.Szakaszok[4].Jelzo.Name = "C";
                    allomas.Szakaszok[9].Jelzo.Name = "B";
                    AllomasFormPeldanyok[0] = new A(this, allomasok[0]);
                    AllomasFormPeldanyok[0].FormStarted();
                    break;
                case "Csillebérc":
                    allomas.AllomasFeltolt(true, 100, true, 1700, 1300);
                    allomas.VonatMegallt += new VonatJelzoDelegate(Index_VonatMegallt);
                    allomasok[1] = allomas;
                    btnU.Enabled = true;
                    btnU.BackgroundImage = Gyermekvasút.Properties.Resources.U_icon_kép;
                    AllomasFormPeldanyok[1] = new U(this, allomasok[1]);
                    AllomasFormPeldanyok[1].FormStarted();
                    break;
                case "Virágvölgy":
                    allomas.AllomasFeltolt(false, 70, true, 1300, 1500);
                    allomas.VonatMegallt += new VonatJelzoDelegate(Index_VonatMegallt);
                    allomasok[2] = allomas;
                    btnL.Enabled = true;
                    btnL.BackgroundImage = Gyermekvasút.Properties.Resources.L_icon_kép2;
                    for (int j = 1; j < 5; j++)
                    {
                        allomas.Jelzok[j].Szabad = true;
                    }
                    AllomasFormPeldanyok[2] = new L(this, allomasok[2]);
                    AllomasFormPeldanyok[2].FormStarted();
                    break;
                case "János-hegy":
                    allomas.AllomasFeltolt(false, 58, true, 1500, 2200);
                    allomas.VonatMegallt += new VonatJelzoDelegate(Index_VonatMegallt);
                    allomasok[3] = allomas;
                    btnI.Enabled = true;
                    btnI.BackgroundImage = Gyermekvasút.Properties.Resources.I_icon_kép;
                    for (int j = 1; j < 5; j++)
                    {
                        allomas.Jelzok[j].Szabad = true;
                    }
                    AllomasFormPeldanyok[3] = new I(this, allomasok[3]);
                    AllomasFormPeldanyok[3].FormStarted();
                    break;
                case "Szépjuhászné":
                    allomas.AllomasFeltolt(true, 111, false, 2200, 2000);
                    allomas.VonatMegallt += new VonatJelzoDelegate(Index_VonatMegallt);
                    allomasok[4] = allomas;
                    btnS.Enabled = true;
                    btnS.BackgroundImage = Gyermekvasút.Properties.Resources.S_icon_kép2Z;
                    allomas.Vaganyutak.Add(new Vaganyut(allomas.Jelzok[0], "Bejárat János-hegy állomás felől az I. vágányra", true, allomas.Szakaszok[1], allomas.Szakaszok[7], allomas.Szakaszok[8], "vgut_s_A_V1"));
                    allomas.Vaganyutak.Add(new Vaganyut(allomas.Jelzok[0], "Bejárat János-hegy állomás felől a II. vágányra", true, allomas.Szakaszok[1], allomas.Szakaszok[2], allomas.Szakaszok[3], "vgut_s_A_V2"));
                    allomas.Vaganyutak.Add(new Vaganyut(allomas.Jelzok[4], "Kijárat Hárs-hegy állomás felé a II. vágányról", false, allomas.Szakaszok[3], allomas.Szakaszok[4], allomas.Szakaszok[5], "vgut_s_V2_ki"));
                    allomas.Vaganyutak.Add(new Vaganyut(allomas.Jelzok[3], "Kijárat Hárs-hegy állomás felé az I. vágányról", false, allomas.Szakaszok[8], allomas.Szakaszok[9], allomas.Szakaszok[5], "vgut_s_V1_ki"));
                    allomas.Vaganyutak.Add(new Vaganyut(allomas.Jelzok[5], "Bejárat Hárs-hegy állomás felől a II. vágányra", true, allomas.Szakaszok[5], allomas.Szakaszok[4], allomas.Szakaszok[3], "vgut_s_B_K2"));
                    allomas.Vaganyutak.Add(new Vaganyut(allomas.Jelzok[5], "Beajárat Hárs-hegy állomás felől az I. vágányra", true, allomas.Szakaszok[5], allomas.Szakaszok[9], allomas.Szakaszok[8], "vgut_s_B_K1"));
                    allomas.Vaganyutak.Add(new Vaganyut(allomas.Jelzok[2], "Kijárat János-hegy állomás felé a II. vágányról", false, allomas.Szakaszok[3], allomas.Szakaszok[2], allomas.Szakaszok[1], "vgut_s_K2_ki"));
                    allomas.Vaganyutak.Add(new Vaganyut(allomas.Jelzok[1], "Kijárat János-hegy állomás felé az I. vágányról", false, allomas.Szakaszok[8], allomas.Szakaszok[7], allomas.Szakaszok[1], "vgut_s_K1_ki"));
                    allomas.ValtKezek.Clear();
                    allomas.Valtok[0] = allomas.Valtok[0].ConvertValtoToValtoS();
                    allomas.Valtok[1] = allomas.Valtok[1].ConvertValtoToValtoS();
                    (allomas.Szakaszok[1] as CsucsSzakasz).Valto = allomas.Valtok[0];
                    (allomas.Szakaszok[5] as CsucsSzakasz).Valto = allomas.Valtok[1];
                    AllomasFormPeldanyok[4] = new S(this, allomasok[4]);
                    AllomasFormPeldanyok[4].FormStarted();
                    break;
                case "Hárs-hegy":
                    allomas.AllomasFeltolt(false, 209, true, 2000, 2500);
                    allomas.VonatMegallt += new VonatJelzoDelegate(Index_VonatMegallt);
                    allomasok[5] = allomas;
                    btnH.Enabled = true;
                    btnH.BackgroundImage = Gyermekvasút.Properties.Resources.H_icon_kép;
                    for (int j = 1; j < 5; j++)
                    {
                        allomas.Jelzok[j].Szabad = true;
                    }
                    allomas.ValtKezek[0].ReteszesLezaras = true;
                    allomas.ValtKezek[1].ReteszesLezaras = true;
                    AllomasFormPeldanyok[5] = new H(this, allomasok[5]);
                    AllomasFormPeldanyok[5].FormStarted();
                    break;
                case "Hűvösvölgy":
                    allomas.AllomasFeltolt(false, 209, true, 2500, 100);
                    allomas.VonatMegallt += new VonatJelzoDelegate(Index_VonatMegallt);
                    allomasok[6] = allomas;
                    btnO.Enabled = true;
                    allomas.Szakaszok[3].Jelzo.Name = "B";
                    allomas.Szakaszok[8].Jelzo.Name = "C";
                    allomas.ValtKezek[0].ReteszesLezaras = true;
                    allomas.ValtKezek[1].ReteszesLezaras = true;
                    AllomasFormPeldanyok[6] = new O(this, allomasok[6]);
                    AllomasFormPeldanyok[6].FormStarted();
                    btnO.BackgroundImage = Gyermekvasút.Properties.Resources.O_icon_kép;
                    break;
                default:
                    MessageBox.Show("A parancssorból beolvasott állomásnév-paraméter nem feleltethető meg egyetlen ismert állomásnévnek sem.\nA program leáll.", "Összeomlás", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    break;
            }

            //Vonatok
            XmlNodeList vonatok = mrdoc.DocumentElement.SelectNodes("Vonatok")[0].SelectNodes("Vonat");
            foreach (XmlNode item in vonatok)
            {
                bool rosszAllomas = false;
                foreach (XmlNode node in item.ChildNodes)
                {
                    bool cancel = false;
                    if (node.Name == "Letrehozas")
                    {
                        foreach (XmlAttribute attr in node.Attributes)
                        {
                            if (attr.Name == "Allomas" && attr.Value != allomas.Name)
                            {
                                rosszAllomas = true;
                                cancel = true;
                                break;
                            }
                        }
                        if (cancel) break;
                    }
                }

                if (rosszAllomas == false)
                {
                    Vonat v = new Vonat(false);
                    //Vonatszam, KoruljarasSzukseges
                    foreach (XmlAttribute attr in item.Attributes)
                    {
                        if (attr.Name == "Vonatszam")
                        {
                            v.Vonatszam = attr.Value;
                        }
                        else if (attr.Name == "Koruljar")
                        {
                            v.KoruljarasSzukseges = Convert.ToBoolean(attr.Value);
                        }
                    }
                    //Allomas, Szakasz
                    foreach (XmlAttribute attr in item.SelectNodes("Letrehozas")[0].Attributes)
                    {
                        if (attr.Name == "Allomas")
                        {//string >> Allomas object
                            Allomasok[AllomasNevek.IndexOf(attr.Value)].AddVonat(v);
                            Allomasok[AllomasNevek.IndexOf(attr.Value)].VisszjeltAdtam.Add(v.Vonatszam);
                        }
                        else if (attr.Name == "Vagany")
                        {
                            if ((attr.Value == "1" && v.Allomas.ElsoVaganyEgyenes) || (attr.Value == "2" && v.Allomas.ElsoVaganyEgyenes == false))
                            {//[3]
                                v.Szakasz = v.Allomas.Szakaszok[3];
                                v.Szakasz.Vonat = v;
                            }
                            else if ((attr.Value == "2" && v.Allomas.ElsoVaganyEgyenes) || (attr.Value == "1" && v.Allomas.ElsoVaganyEgyenes == false))
                            {//[8]
                                v.Szakasz = v.Allomas.Szakaszok[8];
                                v.Szakasz.Vonat = v;
                            }
                        }
                    }
                    //Forda
                    List<string> tempForda = new List<string>();
                    foreach (XmlNode fordulo in item.SelectNodes("Forda")[0].SelectNodes("Fordulo"))
                    {
                        tempForda.Add(fordulo.Attributes[0].Value);
                    }
                    v.Forda = new string[tempForda.Count];
                    for (int i = 0; i < tempForda.Count; i++)
                    {
                        v.Forda[i] = tempForda[i];
                    }
                    //Elindul()
                    v.Elindul();
                }
            }
        }

        void Index_VonatMegallt(Vonat vonat, Jelzo jelzo)
        {
            notifyIcon1.ShowBalloonTip(2000, "Figyelmeztetés", "A(z) " + vonat.Vonatszam + " számú vonat megállt a(z) " + jelzo.ToString() + " jelű bejárati jelző előtt.", ToolTipIcon.Warning);
        }

        private void Index_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.Crash)
            {
                Bezaras();
                return;
            }
            if (MessageBox.Show("Biztosan bezárod a programot? Ezt csak jelszó megadásával teheted meg!\nNem: Szimuláció folytatása\nIgen: jelszó megadása, majd kilépés", "Bezárás megerősítése", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Jelszo jelsz = new Jelszo(true);
                DialogResult dr = jelsz.ShowDialog();
                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    MessageBox.Show("A megadott jelszó helytelen!\nNe piszkáld!", "Helytelen jelszó", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dr == DialogResult.Yes)
                {
                    e.Cancel = true;
                    MessageBox.Show("Nem rossz, nem rossz; de azért a megadott jelszó helytelen! ;)\n[Miket nem ír ez a program... :o]", "Helytelen jelszó", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {//bezárás
                    Bezaras();
                }
            }
        }

        void Bezaras()
        {
            XmlDocument doc = new XmlDocument();

            XmlComment comment1 = doc.CreateComment("LCSV pontverseny adatbázisa – program által generált kód!");
            XmlComment comment2 = doc.CreateComment("Kérlek, légy korrekt, és ne javíts bele kézzel a dokumentumba! Köszi! :) P");
            doc.AppendChild(comment1);
            doc.AppendChild(comment2);

            XmlNode root = doc.CreateElement("LCSV");
            doc.AppendChild(root);

            XmlNode temp = null;
            for (int i = 1; i <= 15; i++)
            {//mindig az i-edik csoportról van szó
                temp = doc.CreateElement("Csoport");
                XmlAttribute csopszam = doc.CreateAttribute("Csoportszam");
                csopszam.Value = i.ToString();
                temp.Attributes.Append(csopszam);

                XmlAttribute hasznalatSzamlalo = doc.CreateAttribute("HasznalatSzamlalo");
                if (i == Csoport)
                {//ez a csoport töltötte ki >> ++
                    hasznalatSzamlalo.Value = (HasznalatSzamlaloTomb[i - 1] + 1).ToString();
                }
                else
                {
                    hasznalatSzamlalo.Value = HasznalatSzamlaloTomb[i - 1].ToString();
                }
                temp.Attributes.Append(hasznalatSzamlalo);

                XmlAttribute tempAttr = null;
                if (i == Csoport)
                {
                    for (int j = 0; j < HibaNevek.Count; j++)
                    {
                        tempAttr = doc.CreateAttribute(HibaNevek[j]);
                        tempAttr.Value = (Hibak[j] + CsoportHibak[i][j]).ToString();
                        temp.Attributes.Append(tempAttr);
                        tempAttr = null;
                    }
                }
                else
                {//a _Loadkor betöltött adatok visszatöltése
                    for (int j = 0; j < HibaNevek.Count; j++)
                    {
                        tempAttr = doc.CreateAttribute(HibaNevek[j]);
                        tempAttr.Value = CsoportHibak[i][j].ToString();
                        temp.Attributes.Append(tempAttr);
                        tempAttr = null;
                    }
                }

                root.AppendChild(temp);
                temp = null;
            }
            doc.Save("LCSV.gyv");

            string mbs = "";
            if (Hibak.Count != HibaNevek.Count)
            {
                MessageBox.Show("Valami hiba van, de csúnyán!\n\n*Hibak.Count != HibaNevek.Count", "Nem kicsit, nagyon");
            }
            else
            {
                for (int i = 0; i < Hibak.Count; i++)
                {
                    mbs += HibaNevek[i] + ": " + Hibak[i].ToString() + "\r\n";
                }
                Gyermekvasút.DEBUG.Log.AddLogText(mbs);
                //mbs += "\n***ÁTLAG***\n\n";
                //for (int i = 0; i < CsoportHibak[0].Count; i++)
                //{
                //    mbs += HibaNevek[i] + ": " + CsoportHibak[0][i].ToString() + "\n";
                //}
                //MessageBox.Show(mbs, "Eredmény", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Index_FormClosed(object sender, FormClosedEventArgs e)
        {
            #region RunLog
            string executingDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            DirectoryInfo dirinf = new DirectoryInfo(Path.Combine(executingDirectory, "RunLog"));
            dirinf.Create();
            using (StreamWriter sw = new StreamWriter(Path.Combine(dirinf.FullName, "RunLog_" + DateTime.Now.ToString().Replace(".", "").Replace(":", "").Replace(" ", "_") + ".log")))
            {
                sw.WriteLine("=== GVSZIM RunLog ===");
                sw.WriteLine("Időpont : " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                sw.WriteLine("Környezet: Gép {" + Environment.MachineName + "} Felhasználó {" + Environment.UserName + "}");
                sw.WriteLine("Csoport: " + RomaiCsoportString);
                sw.WriteLine("Menetrend: " + Program.Menetrend);
                sw.WriteLine();

                foreach (Allomas item in Allomasok)
                {
                    if (item != null)
                    {
                        sw.WriteLine(item.Name);
                        sw.WriteLine(item.AllomasLogText);
                    }
                }

                sw.Write("\r\n\r\nGYVSZIM Runtime Log tartalma:\r\n");
                sw.Write(Gyermekvasút.DEBUG.Log.LogText);
                sw.WriteLine("Vége a futásidőben naplózott adatok kivonatának");
            }            
            #endregion

            if (host != null)
            {
                host.Close();
                Program.Crash = true;
                halozat.Close();
            }
            startscreen.Close();
        }

        private void btnH_Click(object sender, EventArgs e)
        {
            AllomasFormPeldanyok[5].Show();
            if (AllomasFormPeldanyok[5].Hr != null && !AllomasFormPeldanyok[5].Hr.Visible)
                AllomasFormPeldanyok[5].Hr.Show(AllomasFormPeldanyok[5]);
        }

        private void Index_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawLine(new Pen(Color.Blue), 10, 120, 622, 120);
        }

        private void btnU_Click(object sender, EventArgs e)
        {
            AllomasFormPeldanyok[1].Show();
            if (AllomasFormPeldanyok[1].Hr != null && !AllomasFormPeldanyok[1].Hr.Visible)
                AllomasFormPeldanyok[1].Hr.Show(AllomasFormPeldanyok[1]);
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            AllomasFormPeldanyok[0].Show();
            if (AllomasFormPeldanyok[0].Hr != null && !AllomasFormPeldanyok[0].Hr.Visible)
                AllomasFormPeldanyok[0].Hr.Show(AllomasFormPeldanyok[0]);
        }

        private void btnO_Click(object sender, EventArgs e)
        {
            AllomasFormPeldanyok[6].Show();
            if (AllomasFormPeldanyok[6].Hr != null && !AllomasFormPeldanyok[6].Hr.Visible)
                AllomasFormPeldanyok[6].Hr.Show(AllomasFormPeldanyok[6]);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        int elozoX = 0, elozoY = 0;
        bool owlShown = false;
        bool seged = false;
        private void label3_DoubleClick(object sender, EventArgs e)
        {
            
            if (owlShown == false)
            {
                DialogResult dr = MessageBox.Show("Találtál egy húsvéti tojást!\nLehetőséged nyílik megreptetni a csillebérci baglyot!\nMeg szeretnéd reptetni a csillebérci baglyot?\n(Tipp: ENTER-rel válassz! ;)", "Gratulálunk!", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.No)
                {
                    owlShown = true;
                    return;
                }

                owlShown = true;
                owl.Show();
                this.MouseMove += Index_MouseMove;
                this.DoubleClick += Index_DoubleClick;
                foreach (Control item in this.Controls)
                {
                    item.MouseMove += Index_MouseMove;
                    item.DoubleClick += Index_DoubleClick;
                }
            }
        }

        void Index_DoubleClick(object sender, EventArgs e)
        {
            owl.Hide();
            foreach (Control item in this.Controls)
            {
                item.MouseMove -= Index_MouseMove;
                item.DoubleClick -= Index_DoubleClick;
            }
            this.MouseMove -= Index_MouseMove;
            this.DoubleClick -= Index_DoubleClick;
        }

        void Index_MouseMove(object sender, MouseEventArgs e)
        {
            if (seged)
                owl.Location = new Point(owl.Location.X - elozoX + this.PointToClient(Cursor.Position).X, owl.Location.Y + this.PointToClient(Cursor.Position).Y - elozoY);
            else
                seged = true;

            elozoX = this.PointToClient(Cursor.Position).X;
            elozoY = this.PointToClient(Cursor.Position).Y;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Vadkan v = new Vadkan();
            v.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!About.IsShown)
            {
                About ab = new About();
                ab.Show();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }
    }
}
