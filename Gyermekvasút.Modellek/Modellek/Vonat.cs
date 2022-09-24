using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.Runtime.Serialization;
using Gyermekvasút.Modellek;
using Gyermekvasút.Modellek.Properties;

namespace Gyermekvasút
{
    [DataContract]
    public class Vonat
    {
        //állomási formok példányai (.Frissit()-hez; a VZKrefresh(); kiváltja őket)
        
        public Vonat(string[] fordaBe, Szakasz szakasz, Allomas allomasBE, bool koruljarasKell = true) : this()
        {
            this.forda = fordaBe;
            timer.Tick += new EventHandler(timer_Tick);
            Vonatszam = fordaBe[0];
            Szakasz = szakasz;
            szakasz.Vonat = this;
            Allomas = allomasBE;
            KoruljarasSzukseges = koruljarasKell;
            Elindul();
        }

        public Vonat(bool gep = false) : this()
        {
            if (!gep)
            {
                timer.Tick += new EventHandler(timer_Tick);
                if (szakasz != null)
                {
                    szakasz.Vonat = this;
                }
                Elindul();
            }            
        }

        public Vonat()
        {
            this.VonatId = vonatIdCounter++;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            TimerStop();
            if (Halott)
            {
                timer.Enabled = false;
                timer.Tick -= timer_Tick;
                return;
            }

            if (Paros)
            {
                if (ElozoSzakaszrolLehoz)
                {
                    SetInterval(MaradekInterval);
                    ElozoSzakasz.Vonat = null;
                    OnVonatSzakaszExit(this, ElozoSzakasz);
                    ElozoSzakasz = null;

                    ElozoSzakaszrolLehoz = false;
                    TimerStart();
                }
                else
                {//NEM előző szakaszról lehoz
                    switch (Allomas.Szakaszok.IndexOf(Szakasz)) //melyik szakaszről lépNE tovább
                    {
                        case 0://jelző
                            if (Allomas.Szakaszok[1].Jelzo.Szabad && Allomas.Szakaszok[1].Jelzo.Menesztes)
                            {
                                BejaratiJelzoElottVarakozik = false;
                                ElozoSzakasz = Szakasz;
                                Szakasz = Allomas.Szakaszok[1];
                                Allomas.Szakaszok[1].Jelzo.VonatMeghaladta = true;
                                if (Allomas.Szakaszok[1].Jelzo.Fenyjelzo)
                                {
                                    Allomas.Szakaszok[1].Jelzo.Szabad = false;
                                    Allomas.VisszjelTimerStart();
                                }
                                MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                                ElozoSzakaszrolLehoz = true;
                                SetInterval(2000);
                                TimerStart();
                            }
                            else
                            {
                                BejaratiJelzoElottVarakozik = true;
                                SetInterval(1000);
                                TimerStart();
                            }
                            break;
                        case 1://váltó
                            if ((Szakasz as CsucsSzakasz).Valto.Allas)
                            {
                                ElozoSzakasz = Szakasz;
                                Szakasz = Allomas.Szakaszok[2];
                            }
                            else
                            {
                                ElozoSzakasz = Szakasz;
                                Szakasz = Allomas.Szakaszok[7];
                            }
                            BejaratiJelzoElottVarakozik = false;
                            MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                            ElozoSzakaszrolLehoz = true;
                            SetInterval(2000);
                            TimerStart();
                            break;
                        case 2:
                            BejaratiJelzoElottVarakozik = false;
                            ElozoSzakasz = Szakasz;
                            Szakasz = Allomas.Szakaszok[3];
                            MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                            ElozoSzakaszrolLehoz = true;
                            SetInterval(2000);
                            TimerStart();
                            break;
                        case 3://jelző
                            if (Allomas.Szakaszok[4].Jelzo.Szabad && Allomas.Szakaszok[4].Jelzo.Menesztes)
                            {
                                BejaratiJelzoElottVarakozik = false;
                                ElozoSzakasz = Szakasz;
                                Szakasz = Allomas.Szakaszok[4];
                                Allomas.Szakaszok[4].Jelzo.VonatMeghaladta = true;
                                if (Allomas.Szakaszok[4].Jelzo.Fenyjelzo) Allomas.Szakaszok[4].Jelzo.Szabad = false;
                                Szakasz.Jelzo.Menesztes = false;
                                MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                                ElozoSzakaszrolLehoz = true;
                                SetInterval(2000);
                                TimerStart();
                            }
                            else
                            {
                                SetInterval(1000);
                                TimerStart();
                            }
                            break;
                        case 4:
                            BejaratiJelzoElottVarakozik = false;
                            ElozoSzakasz = Szakasz;
                            Szakasz = Allomas.Szakaszok[5];
                            MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                            ElozoSzakaszrolLehoz = true;
                            SetInterval(2000);
                            TimerStart();
                            break;
                        case 5://belép az állomásközBE
                            BejaratiJelzoElottVarakozik = false;
                            ElozoSzakasz = Szakasz;
                            Szakasz = Allomas.Szakaszok[6];
                            MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                            ElozoSzakaszrolLehoz = true;
                            SetInterval(2000);
                            TimerStart();
                            break;
                        case 6://kilép az állomásközBŐL >> majd csak akkor, ha a szomszéd állomás lefogadja
                            break;
                        case 7:
                            BejaratiJelzoElottVarakozik = false;
                            ElozoSzakasz = Szakasz;
                            Szakasz = Allomas.Szakaszok[8];
                            MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                            ElozoSzakaszrolLehoz = true;
                            SetInterval(2000);
                            TimerStart();
                            break;
                        case 8://jelző
                            if (Allomas.Szakaszok[9].Jelzo.Szabad && Allomas.Szakaszok[9].Jelzo.Menesztes)
                            {
                                BejaratiJelzoElottVarakozik = false;
                                ElozoSzakasz = Szakasz;
                                Szakasz = Allomas.Szakaszok[9];
                                Allomas.Szakaszok[9].Jelzo.VonatMeghaladta = true;
                                if (Allomas.Szakaszok[9].Jelzo.Fenyjelzo) Allomas.Szakaszok[9].Jelzo.Szabad = false;
                                Szakasz.Jelzo.Menesztes = false;
                                MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                                ElozoSzakaszrolLehoz = true;
                                SetInterval(2000);
                                TimerStart();
                            }
                            else
                            {
                                SetInterval(1000);
                                TimerStart();
                            }
                            break;
                        case 9:
                            BejaratiJelzoElottVarakozik = false;
                            ElozoSzakasz = Szakasz;
                            Szakasz = Allomas.Szakaszok[5];
                            MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                            ElozoSzakaszrolLehoz = true;
                            SetInterval(2000);
                            TimerStart();
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {//páratlan
                if (ElozoSzakaszrolLehoz)
                {
                    SetInterval(MaradekInterval);
                    ElozoSzakasz.Vonat = null;
                    OnVonatSzakaszExit(this, ElozoSzakasz);
                    ElozoSzakasz = null;

                    elozoSzakaszrolLehoz = false;
                    TimerStart();
                }
                else
                {//NEM előző szakaszról lehoz1
                    switch (Allomas.Szakaszok.IndexOf(Szakasz))
                    {
                        case 0:
                            //kilép az állomásközBŐL (majd csak akkor, ha a szomszéd lefogadja)
                            break;
                        case 1:
                            //belép az állomásközBE
                            BejaratiJelzoElottVarakozik = false;
                            ElozoSzakasz = Szakasz;
                            Szakasz = Allomas.Szakaszok[0];
                            MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                            ElozoSzakaszrolLehoz = true;
                            SetInterval(2000);
                            TimerStart();
                            break;
                        case 2:
                            BejaratiJelzoElottVarakozik = false;
                            ElozoSzakasz = Szakasz;
                            Szakasz = Allomas.Szakaszok[1];
                            MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                            ElozoSzakaszrolLehoz = true;
                            SetInterval(2000);
                            TimerStart();
                            break;
                        case 3:
                            //jelző
                            if (Allomas.Szakaszok[3].Jelzo.Szabad && Allomas.Szakaszok[3].Jelzo.Menesztes)
                            {//szabad a jelző
                                BejaratiJelzoElottVarakozik = false;
                                ElozoSzakasz = Szakasz;
                                Szakasz = Allomas.Szakaszok[2];
                                Allomas.Szakaszok[3].Jelzo.VonatMeghaladta = true;
                                if (Allomas.Szakaszok[3].Jelzo.Fenyjelzo) Allomas.Szakaszok[3].Jelzo.Szabad = false;
                                Allomas.Szakaszok[3].Jelzo.Menesztes = false;
                                MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                                ElozoSzakaszrolLehoz = true;
                                SetInterval(MaradekInterval);
                                TimerStart();
                            }
                            else
                            {
                                SetInterval(1000);
                                TimerStart();
                            }
                            break;
                        case 4:
                            BejaratiJelzoElottVarakozik = false;
                            ElozoSzakasz = Szakasz;
                            Szakasz = Allomas.Szakaszok[3];
                            MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                            ElozoSzakaszrolLehoz = true;
                            SetInterval(2000);
                            TimerStart();
                            break;
                        case 5:
                            //váltó
                            if ((Szakasz as CsucsSzakasz).Valto.Allas)
                            {//E
                                ElozoSzakasz = Szakasz;
                                Szakasz = Allomas.Szakaszok[4];
                            }
                            else
                            {//K
                                ElozoSzakasz = Szakasz;
                                Szakasz = Allomas.Szakaszok[9];
                            }
                            BejaratiJelzoElottVarakozik = false;
                            MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                            ElozoSzakaszrolLehoz = true;
                            SetInterval(2000);
                            TimerStart();
                            break;
                        case 6:
                            //BEJÁRATI JELZŐ
                            if (Szakasz.Jelzo.Szabad && Szakasz.Jelzo.Menesztes)
                            {
                                BejaratiJelzoElottVarakozik = false;
                                ElozoSzakasz = Szakasz;
                                Szakasz = Allomas.Szakaszok[5];
                                ElozoSzakasz.Jelzo.VonatMeghaladta = true;
                                if (ElozoSzakasz.Jelzo.Fenyjelzo)
                                {
                                    ElozoSzakasz.Jelzo.Szabad = false;
                                    Allomas.VisszjelTimerStart();
                                }
                                MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                                ElozoSzakaszrolLehoz = true;
                                SetInterval(2000);
                                TimerStart();
                            }
                            else
                            {
                                BejaratiJelzoElottVarakozik = true;
                                SetInterval(1000);
                                TimerStart();
                            }
                            break;
                        case 7:
                            BejaratiJelzoElottVarakozik = false;
                            ElozoSzakasz = Szakasz;
                            Szakasz = Allomas.Szakaszok[1];
                            MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                            ElozoSzakaszrolLehoz = true;
                            SetInterval(2000);
                            TimerStart();
                            break;
                        case 8:
                            //JELZŐ
                            if (Szakasz.Jelzo.Szabad && Szakasz.Jelzo.Menesztes)
                            {
                                BejaratiJelzoElottVarakozik = false;
                                ElozoSzakasz = Szakasz;
                                Szakasz = Allomas.Szakaszok[7];
                                ElozoSzakasz.Jelzo.VonatMeghaladta = true;
                                if (ElozoSzakasz.Jelzo.Fenyjelzo) ElozoSzakasz.Jelzo.Szabad = false;
                                ElozoSzakasz.Jelzo.Menesztes = false;
                                MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                                ElozoSzakaszrolLehoz = true;
                                SetInterval(2000);
                                TimerStart();
                            }
                            else
                            {
                                SetInterval(1000);
                                TimerStart();
                            }
                            break;
                        case 9:
                            BejaratiJelzoElottVarakozik = false;
                            ElozoSzakasz = Szakasz;
                            Szakasz = Allomas.Szakaszok[8];
                            MaradekInterval = Szakasz.Hossz / 5 * 1000 - 2000;
                            ElozoSzakaszrolLehoz = true;
                            SetInterval(2000);
                            TimerStart();
                            break;
                        default:
                            break;
                    }
                }
            }
            OnFrissulj();
            #region OLD
            //TimerStop();
            //int indexOLD = Beallitasok.Beall.X0.IndexOf(Szakasz) * Beallitasok.Beall.X1.IndexOf(Szakasz) * -1;
            //    if (Convert.ToInt32(Vonatszam) % 2 == 0)
            //    {//páros vonat
            //        if (ElozoSzakaszrolLehoz)
            //        {
            //            SetInterval(MaradekInterval);
            //            if (Szakasz is K_ZaroSzakasz && (Szakasz as K_ZaroSzakasz).ParosIranybaZar == true)
            //            {
            //                //most megy kitérőbe
            //                Beallitasok.Beall.Szakaszok[0][indexOLD - 1].Vonat = null;
            //                OnVonatSzakaszExit(this, Beallitasok.Beall.Szakaszok[0][indexOLD - 1]);
            //            }
            //            else
            //            {
            //                if (kiterobolEgyenesbeVissza)
            //                {
            //                    Beallitasok.Beall.Szakaszok[1][indexOLD - 1].Vonat = null;
            //                    OnVonatSzakaszExit(this, Beallitasok.Beall.Szakaszok[1][indexOLD - 1]);
            //                    kiterobolEgyenesbeVissza = false;
            //                }
            //                else
            //                {
            //                    Beallitasok.Beall.Szakaszok[y][indexOLD - 1].Vonat = null;
            //                    OnVonatSzakaszExit(this, Beallitasok.Beall.Szakaszok[y][indexOLD - 1]);
            //                }
            //            }
            //            ElozoSzakaszrolLehoz = false;
            //            TimerStart();

            //            if (Beallitasok.Beall.Szakaszok[y][indexOLD] is CsucsSzakasz && (Beallitasok.Beall.Szakaszok[y][indexOLD] as CsucsSzakasz).ValtoParosOldalan)
            //            {
            //                if ((Beallitasok.Beall.Szakaszok[y][indexOLD] as CsucsSzakasz).Valto.Allas == true)
            //                {
            //                    y = 0;
            //                }
            //                else
            //                {
            //                    y = 1;
            //                }
            //            }
            //            else
            //            {
            //                if (Beallitasok.Beall.Szakaszok[y][indexOLD] is K_ZaroSzakasz && (Beallitasok.Beall.Szakaszok[y][indexOLD] as K_ZaroSzakasz).ParosIranybaZar == false)
            //                {
            //                    if (y == 1)
            //                    {
            //                        kiterobolEgyenesbeVissza = true;
            //                    }
            //                    y = 0;
            //                }
            //            }
            //        }
            //        else
            //        { //NEM elozoSzakaszrolLehoz
            //            if (indexOLD == Beallitasok.Beall.X0.Count - 2)
            //            {
            //                //pálya vége
            //                MessageBox.Show("A(z) " + Vonatszam + " számú vonat kilépett a nyílt vonalra Hárs-hegy állomás felé.");
            //                Beallitasok.Beall.Vonatok.Remove(this);
            //                Szakasz.Vonat = null;
            //            }
            //            else
            //            {
            //                //van tovább sín :)
            //                if (Beallitasok.Beall.Szakaszok[y][indexOLD + 1].Jelzo == null || Beallitasok.Beall.Szakaszok[y][indexOLD + 1].Jelzo.ParosIranybaNez || (Beallitasok.Beall.Szakaszok[y][indexOLD + 1].Jelzo.ParosIranybaNez == false && Beallitasok.Beall.Szakaszok[y][indexOLD + 1].Jelzo.Szabad && Beallitasok.Beall.Szakaszok[y][indexOLD + 1].Jelzo.Menesztes))
            //                {
            //                    //mehet tovább
            //                    Szakasz = Beallitasok.Beall.Szakaszok[y][indexOLD + 1];
            //                    indexOLD = Beallitasok.Beall.X0.IndexOf(Szakasz) * Beallitasok.Beall.X1.IndexOf(Szakasz) * -1; //index++

            //                    if ( Szakasz.Jelzo != null && Szakasz.Jelzo.ParosIranybaNez == false)
            //                    {
            //                        if (Szakasz.Jelzo.Fenyjelzo)
            //                        {
            //                            Szakasz.Jelzo.Szabad = false;
            //                        }
            //                        Szakasz.Jelzo.VonatMeghaladta = true;
            //                    }

            //                    Beallitasok.Beall.Szakaszok[y][indexOLD].Vonat = this;

            //                    if (Beallitasok.Beall.Szakaszok[y][indexOLD] is CsucsSzakasz && (Beallitasok.Beall.Szakaszok[y][indexOLD] as CsucsSzakasz).ValtoParosOldalan == false)
            //                    {
            //                        Menesztett = false; //gyök felől érint váltót >> kihalad
            //                    }

            //                    maradekInterval = Beallitasok.Beall.Szakaszok[y][indexOLD].Hossz / 5 * 1000 - 2000;
            //                    ElozoSzakaszrolLehoz = true;
            //                    SetInterval(2000);
            //                    TimerStart();
            //                }
            //                else
            //                {
            //                    SetInterval(1000); //mozdonyvezető reakcióideje
            //                    TimerStart(); //folyamatosan figyeli, mikor indulhat el
            //                }
            //            }
            //    }
            //}
            //    else
            //    {
            //        //páratlan vonat
            //        if (ElozoSzakaszrolLehoz)
            //        {
            //            SetInterval(MaradekInterval);
            //            if (Szakasz is K_ZaroSzakasz && (Szakasz as K_ZaroSzakasz).ParosIranybaZar == false)
            //            {
            //                //most megy kitérőbe
            //                Beallitasok.Beall.Szakaszok[0][indexOLD + 1].Vonat = null;
            //            }
            //            else
            //            {
            //                if (kiterobolEgyenesbeVissza)
            //                {
            //                    Beallitasok.Beall.Szakaszok[1][indexOLD + 1].Vonat = null;
            //                    kiterobolEgyenesbeVissza = false;
            //                }
            //                else
            //                {
            //                    Beallitasok.Beall.Szakaszok[y][indexOLD + 1].Vonat = null;
            //                }
            //            }
            //            ElozoSzakaszrolLehoz = false;
            //            TimerStart();

            //            if (Beallitasok.Beall.Szakaszok[y][indexOLD] is CsucsSzakasz && (Beallitasok.Beall.Szakaszok[y][indexOLD] as CsucsSzakasz).ValtoParosOldalan == false)
            //            {
            //                if ((Beallitasok.Beall.Szakaszok[y][indexOLD] as CsucsSzakasz).Valto.Allas == true)
            //                {
            //                    y = 0;
            //                }
            //                else
            //                {
            //                    y = 1;
            //                }
            //            }
            //            else
            //            {
            //                if (Beallitasok.Beall.Szakaszok[y][indexOLD] is K_ZaroSzakasz && (Beallitasok.Beall.Szakaszok[y][indexOLD] as K_ZaroSzakasz).ParosIranybaZar == true)
            //                {
            //                    if (y == 1)
            //                    {
            //                        kiterobolEgyenesbeVissza = true;
            //                    }
            //                    y = 0;
            //                }
            //            }
            //        }
            //        else
            //        { //NEM elozoSzakaszrolLehoz
            //            if (indexOLD == 1)
            //            {
            //                //pálya vége
            //                MessageBox.Show("A(z) " + Vonatszam + " számú vonat kilépett a nyílt vonalra János-hegy állomás felé.");
            //                Beallitasok.Beall.Vonatok.Remove(this);
            //                Szakasz.Vonat = null;
            //            }
            //            else
            //            {
            //                //van tovább sín :)
            //                if (Szakasz.Jelzo == null || Szakasz.Jelzo.ParosIranybaNez == false || (Szakasz.Jelzo.ParosIranybaNez == true && Szakasz.Jelzo.Szabad && Szakasz.Jelzo.Menesztes))
            //                {
            //                    //mehet tovább
            //                    Szakasz = Beallitasok.Beall.Szakaszok[y][indexOLD - 1];
            //                    indexOLD = Beallitasok.Beall.X0.IndexOf(Szakasz) * Beallitasok.Beall.X1.IndexOf(Szakasz) * -1; //index++

            //                    if (Szakasz.Jelzo != null && Szakasz.Jelzo.ParosIranybaNez == true)
            //                    {
            //                        if (Szakasz.Jelzo.Fenyjelzo)
            //                        {
            //                            Szakasz.Jelzo.Szabad = false;
            //                        }                                    
            //                        Szakasz.Jelzo.VonatMeghaladta = true;
            //                    }

            //                    Beallitasok.Beall.Szakaszok[y][indexOLD].Vonat = this;

            //                    if (Beallitasok.Beall.Szakaszok[y][indexOLD] is CsucsSzakasz && (Beallitasok.Beall.Szakaszok[y][indexOLD] as CsucsSzakasz).ValtoParosOldalan == true)
            //                    {
            //                        Menesztett = false; //gyök felől érint váltót >> kihalad
            //                    }

            //                    maradekInterval = Beallitasok.Beall.Szakaszok[y][indexOLD].Hossz / 5 * 1000 - 2000;
            //                    ElozoSzakaszrolLehoz = true;
            //                    SetInterval(2000);
            //                    TimerStart();
            //                }
            //                else
            //                {
            //                    SetInterval(1000); //mozdonyvezető reakcióideje
            //                    TimerStart(); //folyamatosan figyeli, mikor indulhat el
            //                }
            //            }
            //        }
            //    }
            //    OnFrissulj(); //szól az állomás(ok)nak, hogy frissüljenek
#endregion
        }
        
        public int VonatId { get; set; }
        static int vonatIdCounter;

        bool menesztett;
        [DataMember]
        public bool Menesztett
        {
            get { return menesztett; }
            set { menesztett = value; }
        }

        public string Name
        {
            get { return vonatszam; }
        }

        public Allomas Allomas { get; set; }

        Szakasz szakasz;
        public Szakasz Szakasz
        {
            get { return szakasz; }
            set
            {
                szakasz = value;
                szakasz.Vonat = this;

                OnVonatSzakaszEnter(this, Szakasz);
                OnFrissulj();
            }
        }

        bool varakozikKezelve;
        [DataMember]
        public bool VarakozikKezelve
        {
            get { return varakozikKezelve; }
            set { varakozikKezelve = value; }
        }

        bool bejaratiJelzoElottVarakozik;
        [DataMember]
        public bool BejaratiJelzoElottVarakozik
        {
            get { return bejaratiJelzoElottVarakozik; }
            set
            {
                bejaratiJelzoElottVarakozik = value;
                if (Allomas != null && BejaratiJelzoElottVarakozik && !VarakozikKezelve)
                {
                    if (Paros)
                    {
                        OnMegalltam(Allomas.Szakaszok[1].Jelzo);
                    }
                    else
                    {
                        OnMegalltam(Allomas.Szakaszok[6].Jelzo);
                    }
                    VarakozikKezelve = true;
                }
                if (!BejaratiJelzoElottVarakozik)
                {
                    VarakozikKezelve = false;
                }
            }
        }

        Szakasz elozoSzakasz;
        public Szakasz ElozoSzakasz
        {
            get { return elozoSzakasz; }
            set { elozoSzakasz = value; }
        }

        string vonatszam;
        [DataMember]
        public string Vonatszam
        {
            get
            {
                return vonatszam;
            }
            set{ vonatszam = value; }
        }
        
        string[] forda;
        [DataMember]
        public string[] Forda
        {
            get { return forda; }
            set { forda = value; }
        }

        Timer timer = new Timer();

        bool kiterobolEgyenesbeVissza;
        [DataMember]
        public bool KiterobolEgyenesbeVissza
        {
            get { return kiterobolEgyenesbeVissza; }
            set { kiterobolEgyenesbeVissza = value; }
        }

        int y = 0;
        [DataMember]
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        bool elozoSzakaszrolLehoz; //ez alapján dől el, mit csinál a timer
        [DataMember]
        public bool ElozoSzakaszrolLehoz
        {
            get { return elozoSzakaszrolLehoz; }
            set { elozoSzakaszrolLehoz = value; }
        }

        int maradekInterval;
        [DataMember]
        public int MaradekInterval
        {
            get { return maradekInterval; }
            set { maradekInterval = value; }
        }
        
        public void Elindul()
        {
            SetInterval(maradekInterval == 0 ? 100 : maradekInterval);
            TimerStart();
        }

        public void TimerStart()
        {
            if (timer == null)
            {
                timer = new Timer();
                timer.Interval = 100;
                timer.Tick += timer_Tick;
            }
            timer.Start();
        }
        public void TimerStop()
        {
            timer.Stop();
        }
        public void SetInterval(int interval)
        {
            if (timer == null)
            {
                timer = new Timer();
                timer.Tick += new EventHandler(timer_Tick);
            }
            timer.Interval = interval / Gyermekvasút.Modellek.Settings.SebessegOszto;
        }

        public override string ToString()
        {
            return Vonatszam;
        }

        public bool Paros
        {
            get
            {
                int a = 0;
                if (!int.TryParse(vonatszam, out a))
                {
                    return false;
                }
                if (a % 2 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public event FrissuljDelegate Frissulj;
        private void OnFrissulj()
        {
            if (Frissulj != null)
            {
                Frissulj();
            }
        }

        public event VonatJelzoDelegate Megalltam;
        private void OnMegalltam(Jelzo jelzo)
        {
            if (Megalltam != null)
            {
                Megalltam(this, jelzo);
            }
        }

        public event VonatSzakaszDelegate VonatSzakaszEnter;
        private void OnVonatSzakaszEnter(Vonat vonat, Szakasz szakasz)
        {
            if (VonatSzakaszEnter != null)
            {
                VonatSzakaszEnter(vonat, szakasz);
            }
        }

        public event VonatSzakaszDelegate VonatSzakaszExit;
        private void OnVonatSzakaszExit(Vonat vonat, Szakasz szakasz)
        {
            if (VonatSzakaszExit != null)
            {
                VonatSzakaszExit(vonat, szakasz);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(this.GetType().Equals(obj.GetType())))
            {
                return false;
            }
            string other = (obj as Vonat).Vonatszam;
            bool teszt1 = other.Equals(this.vonatszam);
            bool teszt2 = other == this.vonatszam;
            return other.Equals(this.vonatszam);
        }
        public override int GetHashCode()
        {
            return Vonatszam.GetHashCode();
        }
        public static bool operator== (Vonat lhs, Vonat rhs)
        {
            if (object.ReferenceEquals(lhs, rhs))
            {
                return true;
            }
            if (object.ReferenceEquals(lhs, null) ||
                object.ReferenceEquals(rhs, null))
            {
                return false;
            }
            if (lhs.Vonatszam == rhs.Vonatszam)
            {
                return true;
            }
            return false;
        }
        public static bool operator!= (Vonat lhs, Vonat rhs)
        {
            return !(lhs == rhs);
        }

        #region Körüljárás
        [DataMember]
        public bool Koruljar { get; set; }
        [DataMember]
        public bool KoruljarasSzukseges { get; set; }

        /// <summary>
        /// A vonat vonatszámát a forda szerint következőre cseréli.
        /// </summary>
        public void VonatszamCsere()
        {
            if (forda[forda.Length - 1] == vonatszam)
            {
                //vége, kihúz
                if (KoruljarasSzukseges)
                {//már körüljárt >> kitol
                    MessageBox.Show("A(z) " + vonatszam + " számú vonat szerelvénye kitol a vontatási telepre.", "Tolatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {//kihúz
                    MessageBox.Show("A(z) " + vonatszam + " számú vonat szerelvénye kihúz a vontatási telepre.", "Tolatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                VontatasiTelepreKi();
            }
            else
            {
                #region OLD
                //for (int i = 0; i < forda.Length; i++)
                //{
                //    if (forda[i] == vonatszam)
                //    {
                //        vonatszam = forda[i + 1];
                //        break;
                //    }
                //}
                #endregion
                Vonat v = null;
                //ha már kért engedélyt vagy már elrendelte a kijáratát a visszavonatnak, akkor nem hozunk létre új objektumot, hanem a meglévő virtuálisat inicializáljuk fel
                if (Allomas.Name == "Hűvösvölgy") //Vonat:861 -- NullReferenceException
                {//O >> [0]
                    if ((Allomas.Szakaszok[0] as Allomaskoz).Engedelyben2 != null && (Allomas.Szakaszok[0] as Allomaskoz).Engedelyben2.Vonatszam == Forda[FordaIndexOfVonatszam() + 1])
                    {
                        v = (Allomas.Szakaszok[0] as Allomaskoz).Engedelyben2;
                    }
                    else if ((Allomas.Szakaszok[0] as Allomaskoz).Engedelyben != null && (Allomas.Szakaszok[0] as Allomaskoz).Engedelyben.Vonatszam == Forda[FordaIndexOfVonatszam() + 1])
                    {
                        v = (Allomas.Szakaszok[0] as Allomaskoz).Engedelyben;
                    }
                    else if (Allomas.ValtKezek[0].Ki != null && Allomas.ValtKezek[0].Ki.Virtualis && Allomas.ValtKezek[0].Ki.Vonatszam == Forda[FordaIndexOfVonatszam() + 1])
                    {
                        v = Allomas.ValtKezek[0].Ki;
                    }
                }
                else
                {//A >> [6]
                    if ((Allomas.Szakaszok[6] as Allomaskoz).Engedelyben2 != null && (Allomas.Szakaszok[6] as Allomaskoz).Engedelyben2.Vonatszam == Forda[FordaIndexOfVonatszam() + 1])
                    {
                        v = (Allomas.Szakaszok[6] as Allomaskoz).Engedelyben2;
                    }
                    else if ((Allomas.Szakaszok[6] as Allomaskoz).Engedelyben != null && (Allomas.Szakaszok[6] as Allomaskoz).Engedelyben.Vonatszam == Forda[FordaIndexOfVonatszam() + 1])
                    {
                        v = (Allomas.Szakaszok[6] as Allomaskoz).Engedelyben;
                    }
                    else if (Allomas.ValtKezek[1].Ki != null && Allomas.ValtKezek[1].Ki.Virtualis && Allomas.ValtKezek[1].Ki.Vonatszam == Forda[FordaIndexOfVonatszam() + 1])
                    {
                        v = Allomas.ValtKezek[1].Ki;
                    }
                }

                if (v == null)
                {
                    v = new Vonat(true); //new instance
                }
                else
                {
                    v.Virtualis = false;
                }
                
                v.forda = this.forda;
                v.ElozoSzakasz = this.ElozoSzakasz;
                v.BejaratiJelzoElottVarakozik = this.BejaratiJelzoElottVarakozik;
                v.KiterobolEgyenesbeVissza = this.KiterobolEgyenesbeVissza;
                v.KoruljarasSzukseges = this.KoruljarasSzukseges;
                v.MaradekInterval = 0;
                v.Menesztett = this.Menesztett;
                v.timer = null;
                v.VarakozikKezelve = this.VarakozikKezelve;
                v.Virtualis = this.Virtualis;
                v.Y = this.Y;
                v.Vonatszam = this.Forda[FordaIndexOfVonatszam() + 1];

                v.Szakasz = this.Szakasz;
                this.Szakasz.Vonat = v;

                Allomas.RemoveVonat(this);
                Allomas.AddVonat(v);
                Allomas.HalottVonatok.Add(this);
                
                this.Halott = true;
                v.Elindul();
            }
        }
        #endregion

        void VontatasiTelepreKi()
        {
            //TODO Vonat_VontatasiTelepreKi()
        }

        public int FordaIndexOfVonatszam()
        {
            for (int i = 0; i < forda.Length; i++)
            {
                if (forda[i] == vonatszam)
                {
                    return i;
                }
            }

            return 0;
        }

        public bool Virtualis { get; set; }

        /// <summary>
        /// Értéke megadja, hogy ez az objektum releváns-e még a szimuláció szempontjából.
        /// IGAZ, ha egy körüljárás utáni "régi" objektum, egyébként HAMIS
        /// </summary>
        private bool Halott { get; set; }
    }
}
