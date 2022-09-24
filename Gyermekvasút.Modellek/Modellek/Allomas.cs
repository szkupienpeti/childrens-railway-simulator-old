using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Gyermekvasút.Hálózat;
using System.Windows.Forms;

namespace Gyermekvasút.Modellek
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class Allomas : IAllomas
    {
        public static DateTime Ido;

        public string AllomasLogText = "";
        public void AllomasLogTextAdd(string add)
        {
            AllomasLogText += "[" + DateTime.Now.ToShortTimeString() + "] [" + Allomas.Ido.ToShortTimeString() + "] " + add + "\r\n";
        }
        public Allomas()
        {
            pingTimer.Tick += pingTimer_Tick;

            IndulasiTimerCreateAndInit();
            VisszjelTimerCreateAndInit();
        }

        void IndulasiTimerCreateAndInit()
        {
            indulasiTimer = new Timer();
            indulasiTimer.Interval = 120000; //2 perc
            indulasiTimer.Tick += indulasiTimer_Tick;
        }
        void VisszjelTimerCreateAndInit()
        {
            VisszjelTimer = new Timer();
            VisszjelTimer.Interval = 120000; //2 perc
            VisszjelTimer.Tick += VisszjelTimer_Tick;
        }

        public Allomas(string name) : this()
        {
            this.Name = name;
        }

        public Allomas(bool fenyjelzosAllomas, int vaganyHossz, bool elsoVaganyEgyenes, int kezdopontiAllomaskozHossza, int vegpontiAllomaskozHossza, IAllomas kp_fele = null, IAllomas vp_fele = null) : this()
        {
            AllomasFeltolt(fenyjelzosAllomas, vaganyHossz, elsoVaganyEgyenes, kezdopontiAllomaskozHossza, vegpontiAllomaskozHossza);
            KezdopontiAllomas = kp_fele;
            VegpontiAllomas = vp_fele;
            this.ElsoVaganyEgyenes = elsoVaganyEgyenes;
        }

        void VisszjelTimer_Tick(object sender, EventArgs e)
        {
            this.OnKozlemeny(false, 97, new object[] { });
        }

        void indulasiTimer_Tick(object sender, EventArgs e)
        {
            this.OnKozlemeny(false, 98, new object[] { });
        }

        public bool ElsoVaganyEgyenes { get; set; }
        public void AllomasFeltolt(bool fenyjelzosAllomas, int vaganyHossz, bool elsoVaganyEgyenes, int kezdopontiAllomaskozHossza, int vegpontiAllomaskozHossza)
        {
            Jelzok.Add(new Jelzo(false, true, "A", fenyjelzosAllomas));
            Jelzok.Add(new Jelzo(true, "K1", fenyjelzosAllomas));
            Jelzok.Add(new Jelzo(true, "K2", fenyjelzosAllomas));
            Jelzok.Add(new Jelzo(false, "V1", fenyjelzosAllomas));
            Jelzok.Add(new Jelzo(false, "V2", fenyjelzosAllomas));
            Jelzok.Add(new Jelzo(true, true, "B", fenyjelzosAllomas));

            if (elsoVaganyEgyenes)
            {
                Szakaszok.Add(new Allomaskoz()); //Széchenyi-hegy felé
                Szakaszok.Add(null); //CsucsSzakasz A felé (2-es váltó)
                Szakaszok.Add(new Szakasz(70, null, "a_K1"));
                Szakaszok.Add(new Szakasz(vaganyHossz, Jelzok[1], "K1_V1"));
                Szakaszok.Add(new Szakasz(70, Jelzok[3], "V1_b"));
                Szakaszok.Add(null); //CsucsSzakasz O felé (1-es váltó)
                Szakaszok.Add(new Allomaskoz()); //Hűvösvölgy felé
                Szakaszok.Add(new K_ZaroSzakasz(70, null, true, "a_K2"));
                Szakaszok.Add(new Szakasz(vaganyHossz, Jelzok[2], "K2_V2"));
                Szakaszok.Add(new K_ZaroSzakasz(70, Jelzok[4], false, "V2_b"));
            }
            else
            {
                Szakaszok.Add(new Allomaskoz()); //Széchenyi-hegy felé
                Szakaszok.Add(null); //CsucsSzakasz A felé (2-es váltó)
                Szakaszok.Add(new Szakasz(70, null, "a_K2"));
                Szakaszok.Add(new Szakasz(vaganyHossz, Jelzok[2], "K2_V2"));
                Szakaszok.Add(new Szakasz(70, Jelzok[4], "V2_b"));
                Szakaszok.Add(null); //CsucsSzakasz O felé (1-es váltó)
                Szakaszok.Add(new Allomaskoz()); //Hűvösvölgy felé
                Szakaszok.Add(new K_ZaroSzakasz(70, null, true, "a_K1"));
                Szakaszok.Add(new Szakasz(vaganyHossz, Jelzok[1], "K1_V1"));
                Szakaszok.Add(new K_ZaroSzakasz(70, Jelzok[3], false, "V1_b"));
            }

            Valtok.Add(new Valto(Szakaszok[2], Szakaszok[7], "Valto2"));
            Valtok.Add(new Valto(Szakaszok[4], Szakaszok[9], "Valto1"));

            Szakaszok[1] = new CsucsSzakasz(30, Jelzok[0], Valtok[0], true, "A_a");
            Szakaszok[5] = new CsucsSzakasz(30, null, Valtok[1], false, "b_B");

            ValtKezek.Add(new ValtKez(elsoVaganyEgyenes, Valtok[0], "ValtKez2"));
            ValtKezek.Add(new ValtKez(elsoVaganyEgyenes, valtok[1], "ValtKez1"));

            foreach (var valtkez in ValtKezek)
            {
                valtkez.Frissulj += OnFrissulj;
            }

            Szakaszok[0] = new Allomaskoz(kezdopontiAllomaskozHossza, null, "állköz A felé", this);
            Szakaszok[6] = new Allomaskoz(vegpontiAllomaskozHossza, Jelzok[5], "állköz O felé", this);
        }

        #region Listák
        List<Szakasz> szakaszok = new List<Szakasz>();
        public List<Szakasz> Szakaszok
        {
            get { return szakaszok; }
        }

        List<Valto> valtok = new List<Valto>();
        public List<Valto> Valtok
        {
            get { return valtok; }
        }

        List<Jelzo> jelzok = new List<Jelzo>();
        public List<Jelzo> Jelzok
        {
            get { return jelzok; }
        }

        List<Vaganyut> vaganyutak = new List<Vaganyut>();
        public List<Vaganyut> Vaganyutak
        {
            get { return vaganyutak; }
        }

        List<ValtKez> valtKezek = new List<ValtKez>();
        public List<ValtKez> ValtKezek
        {
            get { return valtKezek; }
        }

        List<Vonat> vonatok = new List<Vonat>();
        public List<Vonat> Vonatok
        {
            get { return vonatok; }
        }

        List<Vonat> fogadottVonatok = new List<Vonat>();
        public List<Vonat> FogadottVonatok
        {
            get { return fogadottVonatok; }
        }

        List<string> indulasitKaptam = new List<string>();
        public List<string> IndulasitKaptam
        {
            get { return indulasitKaptam; }
        }

        List<Vonat> indulasiSzukseges = new List<Vonat>();
        public List<Vonat> IndulasiSzukseges
        {
            get { return indulasiSzukseges; }
        }

        List<DateTime> indulasiSzuksegesIdo = new List<DateTime>();
        public List<DateTime> IndulasiSzuksegesIdo
        {
            get { return indulasiSzuksegesIdo; }
        }

        List<Vonat> halottVonatok = new List<Vonat>();
        public List<Vonat> HalottVonatok
        {
            get { return halottVonatok; }
        }

        public List<Vonat> VonatokEsEngedelybenLevoVonatok
        {
            get
            {
                List<Vonat> l = new List<Vonat>();
                foreach (var item in Vonatok)
                {
                    l.Add(item);
                }
                if ((Szakaszok[0] as Allomaskoz).Engedelyben != null && !l.Contains((Szakaszok[0] as Allomaskoz).Engedelyben))
                    l.Add((Szakaszok[0] as Allomaskoz).Engedelyben);
                if ((Szakaszok[0] as Allomaskoz).Engedelyben2 != null && !l.Contains((Szakaszok[0] as Allomaskoz).Engedelyben2))
                    l.Add((Szakaszok[0] as Allomaskoz).Engedelyben2);
                if ((Szakaszok[6] as Allomaskoz).Engedelyben != null && !l.Contains((Szakaszok[6] as Allomaskoz).Engedelyben))
                    l.Add((Szakaszok[6] as Allomaskoz).Engedelyben);
                if ((Szakaszok[6] as Allomaskoz).Engedelyben2 != null && !l.Contains((Szakaszok[6] as Allomaskoz).Engedelyben2))
                    l.Add((Szakaszok[6] as Allomaskoz).Engedelyben2);

                return l;
            }
        }
        #endregion

        Timer indulasiTimer;
        public Timer IndulasiTimer
        {
            get { return indulasiTimer; }
            set { indulasiTimer = value; }
        }
        bool indulasiTimerIsRunning = false;
        public bool IndulasiTimerIsRunning
        {
            get { return indulasiTimerIsRunning; }
            set { indulasiTimerIsRunning = value; }
        }
        public void IndulasiTimerStart()
        {
            if (!indulasiTimerIsRunning)
            {
                indulasiTimerIsRunning = true;
                indulasiTimer.Start();
            }
        }
        public void IndulasiTimerStop()
        {
            if (indulasiTimerIsRunning)
            {
                indulasiTimerIsRunning = false;
                indulasiTimer.Stop();
            }
        }
        public void IndulasiTimerSetInterval(int interval)
        {
            indulasiTimer.Interval = interval;
        }

        public Timer VisszjelTimer { get; set; }
        public bool VisszjelTimerIsRunning { get; set; }
        public void VisszjelTimerStart()
        {//akkor jut ide a kód, ha egy állomáson egy vonat megérkezése után visszaállítják megálljba a bejárati jelzőt
            //fényjelzős állomásnál akkor indul a timer, amikor a vonat meglépi a bejáratot --

            //MÉG +++ kell vizsgálni azt, hogy kereszt lesz-e az állomáson, mert ha van már ha itt-tel engedély kérve abba az irányba
            //ahonnan a vonat behaladt, akkor nem kell elindítani a timert, hiszen a visszjelt elég az indulásival egyszerer adni
            //===> ezt az AllomasForm_FormKozlemeny 97-es kódjában vizsgáljuk a foreach ciklusban
            //ha az Engedelyben2 már elindult az allomasról (IndulasiSzukseges.Contains), akkor már anyázhat a visszjelért
            if (!VisszjelTimerIsRunning)
            {
                VisszjelTimer.Start();
                VisszjelTimerIsRunning = true;
            }
        }
        public void VisszjelTimerStop()
        {
            if (VisszjelTimerIsRunning)
            {
                VisszjelTimer.Stop();
                VisszjelTimerIsRunning = false;
            }
        }
        public void VisszjelTimerSetInterval(int interval)
        {
            VisszjelTimer.Interval = interval;
        }

        List<string> visszjeltAdtam = new List<string>();
        public List<string> VisszjeltAdtam
        {
            get { return visszjeltAdtam; }
        }

        public void VonatElkuld(Vonat vonat)
        {
            #region OLD
            //if (vonat.Paros)
            //{//[0]
            //    if ((Szakaszok[0] as Allomaskoz).Engedelyben != null && (Szakaszok[0] as Allomaskoz).Engedelyben.Vonatszam == vonat.Vonatszam)
            //    {//Engedelyben
            //        AddVonat((Szakaszok[0] as Allomaskoz).Engedelyben);
            //        (Szakaszok[0] as Allomaskoz).Engedelyben.BejaratiJelzoElottVarakozik = false;
            //        (Szakaszok[0] as Allomaskoz).Engedelyben.VarakozikKezelve = false;
            //        (Szakaszok[0] as Allomaskoz).Engedelyben.Virtualis = false;
            //        (Szakaszok[0] as Allomaskoz).Engedelyben.Allomas = this;
            //        (Szakaszok[0] as Allomaskoz).Engedelyben.Elindul();

            //        Szakaszok[0].Vonat = (Szakaszok[0] as Allomaskoz).Engedelyben;
            //        (Szakaszok[0] as Allomaskoz).Engedelyben.Szakasz = Szakaszok[0];
            //    }
            //    else if ((Szakaszok[0] as Allomaskoz).Engedelyben2 != null && (Szakaszok[0] as Allomaskoz).Engedelyben2.Vonatszam == vonat.Vonatszam)
            //    {//Engedelyben2
            //        AddVonat((Szakaszok[0] as Allomaskoz).Engedelyben2);
            //        (Szakaszok[0] as Allomaskoz).Engedelyben2.BejaratiJelzoElottVarakozik = false;
            //        (Szakaszok[0] as Allomaskoz).Engedelyben2.VarakozikKezelve = false;
            //        (Szakaszok[0] as Allomaskoz).Engedelyben2.Virtualis = false;
            //        (Szakaszok[0] as Allomaskoz).Engedelyben2.Allomas = this;
            //        (Szakaszok[0] as Allomaskoz).Engedelyben2.Elindul();

            //        Szakaszok[0].Vonat = (Szakaszok[0] as Allomaskoz).Engedelyben2;
            //        (Szakaszok[0] as Allomaskoz).Engedelyben2.Szakasz = Szakaszok[0];
            //    }
            //    else
            //    {
            //        AddVonat(vonat);
            //        vonat.BejaratiJelzoElottVarakozik = false;
            //        vonat.VarakozikKezelve = false;
            //        vonat.Elindul();

            //        Szakaszok[0].Vonat = vonat;
            //        vonat.Szakasz = Szakaszok[0];
            //    }
            //}
            //else
            //{//[6]
            //    if ((Szakaszok[6] as Allomaskoz).Engedelyben != null && (Szakaszok[6] as Allomaskoz).Engedelyben.Vonatszam == vonat.Vonatszam)
            //    {//Engedelyben
            //        AddVonat((Szakaszok[6] as Allomaskoz).Engedelyben);
            //        (Szakaszok[6] as Allomaskoz).Engedelyben.BejaratiJelzoElottVarakozik = false;
            //        (Szakaszok[6] as Allomaskoz).Engedelyben.VarakozikKezelve = false;
            //        (Szakaszok[6] as Allomaskoz).Engedelyben.Virtualis = false;
            //        (Szakaszok[6] as Allomaskoz).Engedelyben.Allomas = this;
            //        (Szakaszok[6] as Allomaskoz).Engedelyben.Elindul();

            //        Szakaszok[6].Vonat = (Szakaszok[6] as Allomaskoz).Engedelyben;
            //        (Szakaszok[6] as Allomaskoz).Engedelyben.Szakasz = Szakaszok[6];
            //    }
            //    else if ((Szakaszok[6] as Allomaskoz).Engedelyben2 != null && (Szakaszok[6] as Allomaskoz).Engedelyben2.Vonatszam == vonat.Vonatszam)
            //    {//Engedelyben2
            //        AddVonat((Szakaszok[6] as Allomaskoz).Engedelyben2);
            //        (Szakaszok[6] as Allomaskoz).Engedelyben2.BejaratiJelzoElottVarakozik = false;
            //        (Szakaszok[6] as Allomaskoz).Engedelyben2.VarakozikKezelve = false;
            //        (Szakaszok[6] as Allomaskoz).Engedelyben2.Virtualis = false;
            //        (Szakaszok[6] as Allomaskoz).Engedelyben2.Allomas = this;
            //        (Szakaszok[6] as Allomaskoz).Engedelyben2.Elindul();

            //        Szakaszok[6].Vonat = (Szakaszok[6] as Allomaskoz).Engedelyben2;
            //        (Szakaszok[6] as Allomaskoz).Engedelyben2.Szakasz = Szakaszok[6];
            //    }
            //    else
            //    {
            //        AddVonat(vonat);
            //        vonat.BejaratiJelzoElottVarakozik = false;
            //        vonat.VarakozikKezelve = false;
            //        vonat.Elindul();

            //        Szakaszok[6].Vonat = vonat;
            //        vonat.Szakasz = Szakaszok[6];
            //    }
            //}
            #endregion

            AddVonat(vonat);
            vonat.BejaratiJelzoElottVarakozik = false;
            vonat.VarakozikKezelve = false;
            
            if (vonat.Paros)
            {
                Szakaszok[0].Vonat = vonat;
                vonat.Szakasz = Szakaszok[0];
            }
            else
            {
                Szakaszok[6].Vonat = vonat;
                vonat.Szakasz = Szakaszok[6];
            }

            vonat.MaradekInterval = vonat.Szakasz.Hossz / 5 * 1000;
            vonat.Elindul();

            AllomasLogTextAdd("VonatElkuld?" + vonat.Vonatszam);
        }
        public void VonatFogad(Vonat vonat)
        {
            if (vonat.Paros)
            {
                Szakaszok[6].Vonat = null;
            }
            else
	        {
                Szakaszok[0].Vonat = null;
	        }
            RemoveVonat(vonat);
            OnFrissulj();

            AllomasLogTextAdd("VonatFogad?" + vonat.Vonatszam);
        }

        public event FrissuljDelegate Frissulj;
        private void OnFrissulj()
        {
            if (Frissulj != null)
            {
                Frissulj();
            }
        }

        public event VonatJelzoDelegate VonatMegallt;
        private void OnVonatMegallt(Vonat vonat, Jelzo jelzo)
        {
            if (VonatMegallt != null)
            {
                VonatMegallt(vonat, jelzo);
            }

            AllomasLogTextAdd("VonatMegallt?" + vonat.Vonatszam + "?" + jelzo.Name);
        }

        public void AddVonat(Vonat vonat)
        {
            vonat.Allomas = this;
            Vonatok.Add(vonat);
            vonat.Frissulj += OnFrissulj;
            vonat.VonatSzakaszEnter += vonat_VonatSzakaszEnter;
            vonat.VonatSzakaszExit += vonat_VonatSzakaszExit;
            vonat.Megalltam += vonat_Megalltam;
            vonat.Menesztett = false;
        }
        public void RemoveVonat(Vonat vonat)
        {
            Vonatok.Remove(vonat);
            vonat.Frissulj -= OnFrissulj;
            vonat.VonatSzakaszEnter -= vonat_VonatSzakaszEnter;
            vonat.VonatSzakaszExit -= vonat_VonatSzakaszExit;
            vonat.Megalltam -= vonat_Megalltam;
        }

        private void vonat_Megalltam(Vonat vonat, Jelzo jelzo)
        {
            OnVonatMegallt(vonat, jelzo);
        }
        private void vonat_VonatSzakaszExit(Vonat vonat, Szakasz szakasz)
        {
            if (szakasz == Szakaszok[0] && vonat.Paros)
            {//A felé eső állközről jön le, a vonat O felé megy (páros) >> bejött a vége az állomásra
                KezdopontiAllomas.VonatFogad(vonat);
            }
            if (szakasz == Szakaszok[6] && !vonat.Paros)
            {//O felé eső állközről jön le, a vonat a felé megy (páratlan) >> bejött a vége az állomásra
                VegpontiAllomas.VonatFogad(vonat);
            }
        }
        private void vonat_VonatSzakaszEnter(Vonat vonat, Szakasz szakasz)
        {
            //TODO: Hibakezelés, ha nincs hálózat
            try
            {
                if (szakasz == Szakaszok[0] && !vonat.Paros)
                {//állköz A felé, a vonat is A felé (páratlan)
                    KezdopontiAllomas.VonatElkuld(vonat);
                }
                else if (szakasz == Szakaszok[6] && vonat.Paros)
                {//állköz O felé, a vonat is O felé (páros)                
                    VegpontiAllomas.VonatElkuld(vonat);
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }
        
        public string Name { get; set; }

        #region Körüljárás
        public bool Vegallomas
        {
            get
            {
                if (Name == "Széchenyi-hegy" || Name == "Hűvösvölgy")
                    return true;
                else
                return false;
            }
        }
        public string KoruljarasElrendelesSzuksegesVonatszam { get; set; }
        public Vonat KoruljarasElrendelve { get; set; }
        #endregion

        //Ezek változnak, ha lezárunk egy állomást (telefon)
        private IAllomas kezdopontiAllomas;
        public IAllomas KezdopontiAllomas
        {
            get { return kezdopontiAllomas; }
            set { kezdopontiAllomas = value; }
        }
        private IAllomas vegpontiAllomas;
        public IAllomas VegpontiAllomas
        {
            get { return vegpontiAllomas; }
            set { vegpontiAllomas = value; }
        }
        
        public IAsyncResult BeginVonatElkuld(Vonat vonat, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }
        public void EndVonatElkuld(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
        public IAsyncResult BeginVonatFogad(Vonat vonat, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }
        public void EndVonatFogad(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }

        //EVENTS -- Telefon
        public event TelCsengetesDelegate Megcsengettek;
        public void OnMegcsengettek(bool kpFeleHiv)
        {
            if (Megcsengettek != null)
            {
                Megcsengettek(kpFeleHiv);
            }
        }
        public event TelCsengetesDelegate Visszacsengettek;
        public void OnVisszacsengettek(bool kpFeleHiv)
        {
            if (Visszacsengettek != null)
            {
                Visszacsengettek(kpFeleHiv);
            }
        }
        public event TelKozlemenyDelegate Kozlemeny;
        public void OnKozlemeny(bool kpFeleHiv, int kozlemenyTipus, object[] parameters)
        {
            #region OLD
            //if (kozlemenyTipus == 100)
            //{

            //    if (kpFeleHiv) VpAllEl = true;
            //    else KpAllEl = true;
            //    return;
            //}
            #endregion

            if (kozlemenyTipus == 100)
            {//indulj el!
                OnKesz();
                if (Name == "Hűvösvölgy")
                {
                    return;
                }
                VegpontiAllomas.OnKozlemeny(false, 100, new object[] { });
            }

            if (Kozlemeny != null)
            {
                Kozlemeny(kpFeleHiv, kozlemenyTipus, parameters);
            }
        }
        public event TelMegszakitvaDelegate HivasMegszakitva;
        public void OnHivasMegszakitva(bool kpFeleHiv)
        {
            if (HivasMegszakitva != null)
            {
                HivasMegszakitva(kpFeleHiv);
            }
        }

        #region Hálózat - csak akkor induljon el, ha minden állomás él
        bool seged_A_done = false;

        #region Pingelés kijelzése
        public bool DebugValaszoltak { get; set; }
        public bool DebugPinged { get; set; }
        public event FrissuljDelegate DebugPingChanged;
        public void OnDebugPingChanged()
        {
            if (DebugPingChanged != null)
            {
                DebugPingChanged();
            }
        }
        #endregion

        public void Ping(bool parameter)
        {
            #region OLD
            //if (kpFelePingel)
            //{
            //    if (!VpAllEl)
            //    {
            //        VegpontiAllomas.Ping(false);
            //        VpAllEl = true;
            //    }                
            //}
            //else
            //{
            //    if (!KpAllEl)
            //    {
            //        KezdopontiAllomas.Ping(true);
            //        KpAllEl = true;
            //    }
            //}
            #endregion
            if (parameter)
            {//válasz a pingemre
                SzomszedEl = true;
                //if (_DebugValaszoltak == false)
                //    MessageBox.Show("válaszoltak a pingre, nem pingelek tovább");

                DebugValaszoltak = true;
            }
            else
            {//megpingeltek engem
                //if (_DebugPinged == false)
                //    MessageBox.Show("megpingeltek engem");

                DebugPinged = true;

                if (VegpontiAllomas != null)
                    VegpontiAllomas.Ping(true);

                if (Name == "Széchenyi-hegy" && !seged_A_done)
                {
                    OnKesz();
                    VegpontiAllomas.OnKozlemeny(false, 100, new object[] { });
                    seged_A_done = true;
                    return;
                }                               
                pingTimer.Start();                
            }
            OnDebugPingChanged();
        }
        Timer pingTimer = new Timer() { Interval = 100 };
        public void TryHuvosvolgy()
        {
            pingTimer.Start();
        }
        void pingTimer_Tick(object sender, EventArgs e)
        {
            if (SzomszedEl == false)
            {
                try
                {
                    KezdopontiAllomas.Ping(false);
                }
                catch (Exception) { }
            }
            else
            {
                pingTimer.Stop();
            }
        }
        #region OLD
        //bool kpAllEl;
        //public bool KpAllEl
        //{
        //    get { return kpAllEl; }
        //    set
        //    {
        //        kpAllEl = value;
        //        if (value)
        //            SzomszedAllomasFeleledt(true);
        //    }
        //}

        //bool vpAllEl;
        //public bool VpAllEl
        //{
        //    get { return vpAllEl; }
        //    set
        //    {
        //        vpAllEl = value;
        //        if (value)
        //            SzomszedAllomasFeleledt(false);
        //    }
        //}
        #endregion
        bool szomszedEl;
        public bool SzomszedEl
        {
            get { return szomszedEl; }
            set
            {
                szomszedEl = value;
            }
        }
        public event FrissuljDelegate Kesz;
        public void OnKesz()
        {
            if (Kesz != null)
            {
                Kesz();
            }
        }

        #endregion

        #region NotImplemented
        public IAsyncResult BeginOnMegcsengettek(bool kpFeleHiv, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndOnMegcsengettek(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginOnVisszacsengettek(bool kpFeleHiv, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndOnVisszacsengettek(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginOnKozlemeny(bool kpFeleHiv, int kozlemenyTipus, object[] parameters, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndOnKozlemeny(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginOnHivasMegszakitva(bool kpFeleHiv, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndOnHivasMegszakitva(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginPing(bool kpFelePingel, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndPing(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
        #endregion

        public bool Lezarva { get; private set; }
        public void AllomasLezar()
        {
            Lezarva = true;
            int osszhossz = Szakaszok[0].Hossz + Szakaszok[1].Hossz + Szakaszok[2].Hossz + Szakaszok[3].Hossz + Szakaszok[4].Hossz + Szakaszok[5].Hossz + Szakaszok[6].Hossz;
            KezdopontiAllomas.OnKozlemeny(true, 81, new object[] { this.Name, osszhossz });
            VegpontiAllomas.OnKozlemeny(false, 80, new object[] { this.Name, osszhossz });
        }
    }
}
