using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gyermekvasút.Modellek;
using Gyermekvasút.Telefon;
using Gyermekvasút.Hálózat;

namespace Gyermekvasút
{
    public class AllomasForm : Form
    {
        PictureBox kpTel;
        public PictureBox KpTel
        {
            get { return kpTel; }
            set { kpTel = value; }
        }

        PictureBox vpTel;
        public PictureBox VpTel
        {
            get { return vpTel; }
            set { vpTel = value; }
        }

        void vpTel_Click(object sender, EventArgs e)
        {
            KimenoHivas kh = new KimenoHivas(Allomas.VegpontiAllomas, Allomas, Ind);
            kh.ShowDialog();
        }

        void kpTel_Click(object sender, EventArgs e)
        {
            KimenoHivas kh = new KimenoHivas(Allomas.KezdopontiAllomas, Allomas, Ind);
            kh.ShowDialog();
        }

        private Index ind;
        public Index Ind
        {
            get { return ind; }
            set { ind = value; }
        }

        private Hr hr;
        public Hr Hr
        {
            get { return hr; }
            set { hr = value; }
        }

        private Allomas allomas;
        public Allomas Allomas
        {
            get { return allomas; }
            set
            {
                if (value != allomas && allomas != null)
                {
                    allomas.Megcsengettek -= FormMegcsengettek;
                    allomas.Visszacsengettek -= FormVisszacsengettek;
                    allomas.HivasMegszakitva -= FormMegszakitva;
                    allomas.Kozlemeny -= FormKozlemeny;
                    (allomas.Szakaszok[0] as Allomaskoz).EngedelyLejar.Tick -= KpFeleEngedelyLejar;
                    (allomas.Szakaszok[6] as Allomaskoz).EngedelyLejar.Tick -= VpFeleEngedelyLejar;
                }
                allomas = value;
                if (allomas != null)
                {
                    allomas.Frissulj += new FrissuljDelegate(Frissit);
                    allomas.Megcsengettek += FormMegcsengettek;
                    allomas.Visszacsengettek += FormVisszacsengettek;
                    allomas.HivasMegszakitva += FormMegszakitva;
                    allomas.Kozlemeny += FormKozlemeny;
                    (allomas.Szakaszok[0] as Allomaskoz).EngedelyLejar.Tick += KpFeleEngedelyLejar;
                    (allomas.Szakaszok[6] as Allomaskoz).EngedelyLejar.Tick += VpFeleEngedelyLejar;
                }
            }
        }

        void TelValShowDialog(string form, string text, string all, Allomas hivoAll, IAllomas fogadoAll, Index ind)
        {
            TelefonValasz tv = new TelefonValasz(form, text, all, hivoAll, fogadoAll, ind);
            tv.ShowDialog();
        }

        void FormKozlemeny(bool kpFeleHiv, int kozlemenyTipus, object[] parameters)
        {
            string s = "";
            string all = Index.GetSzomszedAllomasNev(Allomas.Name, kpFeleHiv);
            switch (kozlemenyTipus)
            {
                case 0: //Engedélykérés -- azonos ir.
                case 1: //Engedélykérés -- ellenk. ir. VAN
                case 2: //Engedélykérés -- ellenk. ir. VOLT
                    var ev = kpFeleHiv ? new EngedelyValasz(kpFeleHiv, kozlemenyTipus, parameters, Allomas, Allomas.VegpontiAllomas) : new EngedelyValasz(kpFeleHiv, kozlemenyTipus, parameters, Allomas, Allomas.KezdopontiAllomas);
                    ev.ShowDialog();
                    break;
                case 3: //Engedélyadás -- azonos
                    if (kpFeleHiv)
                    {
                        (Allomas.Szakaszok[0] as Allomaskoz).Engedelyben = parameters[0] as Vonat;
                        //(Allomas.Szakaszok[0] as Allomaskoz).TimerStart();
                    }
                    else
                    {
                        (Allomas.Szakaszok[6] as Allomaskoz).Engedelyben = parameters[0] as Vonat;
                        //(Allomas.Szakaszok[6] as Allomaskoz).TimerStart();
                    }
                    s = "Vonatot nem indítok. A(z) " + (parameters[0] as Vonat).Vonatszam + " számú vonat\n" + all + " állomásra jöhet.";
                    if (kpFeleHiv)
                    {
                        TelValShowDialog("Engedélyadás", s, all, Allomas, Allomas.KezdopontiAllomas, Ind);
                    }
                    else
                    {
                        TelValShowDialog("Engedélyadás", s, all, Allomas, Allomas.VegpontiAllomas, Ind);
                    }
                    break;
                case 4: //Engedélyadás -- ellenk.
                    if (kpFeleHiv)
                    {
                        if ((Allomas.Szakaszok[0] as Allomaskoz).Engedelyben != null)
                        {//use engedelyben2
                            (Allomas.Szakaszok[0] as Allomaskoz).Engedelyben2 = parameters[1] as Vonat;
                        }
                        else
                        {//use engedelyben
                            (Allomas.Szakaszok[0] as Allomaskoz).Engedelyben = parameters[1] as Vonat;
                        }
                    }
                    else
                    {
                        if ((Allomas.Szakaszok[6] as Allomaskoz).Engedelyben != null)
                        {//use engedelyben2
                            (Allomas.Szakaszok[6] as Allomaskoz).Engedelyben2 = parameters[1] as Vonat;
                        }
                        else
                        {//use engedelyben
                            (Allomas.Szakaszok[6] as Allomaskoz).Engedelyben = parameters[1] as Vonat;
                        }
                    }
                    s = "Vonatot nem indítok. Ha ott a(z) " + (parameters[0] as Vonat).Vonatszam + " számú vonat,\na(z) " + (parameters[1] as Vonat).Vonatszam + " számú vonat " + all + " állomásra jöhet.";
                    if (kpFeleHiv)
                    {
                        TelValShowDialog("Engedélyadás", s, all, Allomas, Allomas.KezdopontiAllomas, Ind);
                    }
                    else
                    {
                        TelValShowDialog("Engedélyadás", s, all, Allomas, Allomas.VegpontiAllomas, Ind);
                    }
                    break;
                case 5: //Engedélymegtagadás
                    s = "A(z) " + (parameters[0] as Vonat).Vonatszam + " számú vonatot\n" + parameters[1] + "\nmiatt nem fogadom.\nEngedélyt kb. " + parameters[2].ToString() + " perc múlva adok.";
                    if (kpFeleHiv)
                    {
                        TelValShowDialog("Engedélymegtagadás", s, all, Allomas, Allomas.KezdopontiAllomas, Ind);
                    }
                    else
                    {
                        TelValShowDialog("Engedélymegtagadás", s, all, Allomas, Allomas.VegpontiAllomas, Ind);
                    }
                    break;
                case 6: //Indulási adás
                    var kv = kpFeleHiv ? new KozlemenyVetel(kpFeleHiv, Allomas, Allomas.VegpontiAllomas, parameters, kozlemenyTipus) : new KozlemenyVetel(kpFeleHiv, Allomas, Allomas.KezdopontiAllomas, parameters, kozlemenyTipus);
                    Allomas.IndulasitKaptam.Add((parameters[0] as Vonat).Vonatszam);
                    kv.ShowDialog();
                    break;
                case 7: //Indulási vétel
                    s = "A(z) " + (parameters[0] as Vonat).Vonatszam + " számú vonat indulásiidő-közlését\n" + Allomas.Name + " állomásról vettem.";
                    if (kpFeleHiv)
                    {
                        TelValShowDialog("Indulásiidő-közlés vétele", s, all, Allomas, Allomas.KezdopontiAllomas, Ind);
                    }
                    else
                    {
                        TelValShowDialog("Indulásiidő-közlés vétele", s, all, Allomas, Allomas.VegpontiAllomas, Ind);
                    }
                    break;
                case 8: //Visszjel adás
                    var kvetel = kpFeleHiv ? new KozlemenyVetel(kpFeleHiv, Allomas, Allomas.VegpontiAllomas, parameters, kozlemenyTipus) : new KozlemenyVetel(kpFeleHiv, Allomas, Allomas.KezdopontiAllomas, parameters, kozlemenyTipus);
                    if (kpFeleHiv)
                    {//[6]
                        if ((Allomas.Szakaszok[6] as Allomaskoz).Engedelyben2 != null)
                        {
                            (Allomas.Szakaszok[6] as Allomaskoz).UtolsoVonat = (Allomas.Szakaszok[6] as Allomaskoz).Engedelyben;
                            (Allomas.Szakaszok[6] as Allomaskoz).Engedelyben = (Allomas.Szakaszok[6] as Allomaskoz).Engedelyben2;
                            (Allomas.Szakaszok[6] as Allomaskoz).Engedelyben2 = null;
                        }
                        else
                        {
                            (Allomas.Szakaszok[6] as Allomaskoz).UtolsoVonat = (Allomas.Szakaszok[6] as Allomaskoz).Engedelyben;
                            (Allomas.Szakaszok[6] as Allomaskoz).Engedelyben = null;
                        }
                    }
                    else
                    {
                        if ((Allomas.Szakaszok[0] as Allomaskoz).Engedelyben2 != null)
                        {
                            (Allomas.Szakaszok[0] as Allomaskoz).UtolsoVonat = (Allomas.Szakaszok[0] as Allomaskoz).Engedelyben;
                            (Allomas.Szakaszok[0] as Allomaskoz).Engedelyben = (Allomas.Szakaszok[0] as Allomaskoz).Engedelyben2;
                            (Allomas.Szakaszok[0] as Allomaskoz).Engedelyben2 = null;
                        }
                        else
                        {
                            (Allomas.Szakaszok[0] as Allomaskoz).UtolsoVonat = (Allomas.Szakaszok[0] as Allomaskoz).Engedelyben;
                            (Allomas.Szakaszok[0] as Allomaskoz).Engedelyben = null;
                        }
                    }
                    kvetel.ShowDialog();
                    break;
                case 9: //Visszjel vétel
                    s = "A(z) " + (parameters[0] as Vonat).Vonatszam + " számú vonat visszajelentését\n" + Allomas.Name + " állomásról vettem.";
                    if (kpFeleHiv)
                    {
                        TelValShowDialog("Visszajelentés vétele", s, all, Allomas, Allomas.KezdopontiAllomas, Ind);
                    }
                    else
                    {
                        TelValShowDialog("Visszajelentés vétele", s, all, Allomas, Allomas.VegpontiAllomas, Ind);
                    }
                    break;
//OPERATÍV KÓDOK -- 80+ (Állomás lezárása)
                case 80: //kezdőponti állomás csere
                    //Allomas.KezdopontiAllomas = (IAllomas)parameters[0];                    
                    Index.AllomasNevek.Remove((string)parameters[0]);
                    Allomas.Szakaszok[0].Hossz = (int)parameters[1];
                    IAllomas ujKp = new Hálózat.AllomasClient(Index.GetSzomszedAllomasNev(allomas.Name, true));
                    allomas.KezdopontiAllomas = ujKp;
                    break;
                case 81: //végponti állomás csere
                    //Allomas.VegpontiAllomas = (IAllomas)parameters[0];
                    Index.AllomasNevek.Remove((string)parameters[0]);
                    Allomas.Szakaszok[6].Hossz = (int)parameters[1];
                    IAllomas ujVp = new Hálózat.AllomasClient(Index.GetSzomszedAllomasNev(allomas.Name, false));
                    allomas.VegpontiAllomas = ujVp;
                    break;
//RENDKÍVÜLI KÓDOK -- 90+
                case 97: //visszjel szükséges_Tick
                    List<Vonat> visszjelKell = new List<Vonat>();
                    bool parosVan = false, paratlanVan = false;
                    foreach (Vonat item in allomas.FogadottVonatok)
                    {
                        if (!allomas.VisszjeltAdtam.Contains(item.Vonatszam))
                        {//visszjel szükséges
                            if (item.Paros)
                            {
                                //páros vonatról kell viszjelt adnom, amit már fogadtam ==> engedelyben == item (paros)
                                //ha engedelyben2 != null ==> kereszt lesz ==> nem kell visszjelt adnom
                                if ((Allomas.Szakaszok[0] as Allomaskoz).Engedelyben2 == null || ((Allomas.Szakaszok[0] as Allomaskoz).Engedelyben2 != null && Allomas.IndulasiSzukseges.Contains((Allomas.Szakaszok[0] as Allomaskoz).Engedelyben2)))
                                {//akkor is kell a visszjel, ha az ellenvonat már elindult
                                    parosVan = true;
                                    visszjelKell.Add(item);
                                }
                            }
                            else
                            {//~
                                if ((Allomas.Szakaszok[6] as Allomaskoz).Engedelyben2 == null || ((Allomas.Szakaszok[6] as Allomaskoz).Engedelyben2 != null && Allomas.IndulasiSzukseges.Contains((Allomas.Szakaszok[6] as Allomaskoz).Engedelyben2)))
                                {
                                    paratlanVan = true;
                                    visszjelKell.Add(item);
                                }
                            }
                        }
                    }
                    //vonatkeresés vége --
                    if (visszjelKell.Count != 0)
                    {
                        if (parosVan)
                        {
                            if (paratlanVan)
                            {//páros ÉS páratlan IS van -- A és B bej. j.-k IS érintettek
                                bool a = false, b = false;
                                if (allomas.Jelzok[0].Szabad && !allomas.Jelzok[0].Fenyjelzo) a = true;
                                if (allomas.Jelzok[5].Szabad && !allomas.Jelzok[5].Fenyjelzo) b = true;
                                Vonat paros = null, paratlan = null;
                                if (visszjelKell[0].Paros)
                                {
                                    paros = visszjelKell[0];
                                    paratlan = visszjelKell[1];
                                }
                                else
                                {
                                    paros = visszjelKell[1];
                                    paratlan = visszjelKell[0];
                                }
                                if (a && b)
                                {
                                    MessageBox.Show("A(z) " + paros.Vonatszam + " és a(z) " + paratlan.Vonatszam + " számú vonatok már leközlekedett a bejárati vágányútjukon, azaz megérkeztek az állomásra (minden járművük a biztonsági határjelzőkön belül ért), az A és a B jelű bejárati jelzőket mégsem állítottad vissza Megállj!-állásba!\nÁllítsd vissza az A és a B jelű bejárati jelzőket Megállj!-állásba, majd adj visszajelentést " + Index.GetSzomszedAllomasNev(allomas.Name, true) + " és " + Index.GetSzomszedAllomasNev(allomas.Name, false) + " állomásoknak!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ind.Hibak[26] += 2;
                                }
                                else
                                {
                                    if (a)
                                    {//csak a
                                        MessageBox.Show("A(z) " + paros.Vonatszam + " és a(z) " + paratlan.Vonatszam + " számú vonatok már leközlekedett a bejárati vágányútjukon, azaz megérkeztek az állomásra (minden járművük a biztonsági határjelzőkön belül ért), az A jelű bejárati jelzőt mégsem állítottad vissza Megállj!-állásba!\nÁllítsd vissza az A jelű bejárati jelzőt Megállj!-állásba, majd adj visszajelentést " + Index.GetSzomszedAllomasNev(allomas.Name, true) + " és " + Index.GetSzomszedAllomasNev(allomas.Name, false) + " állomásoknak!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        Ind.Hibak[26] += 2;
                                    }
                                    else
                                    {
                                        if (b)
                                        {//csak b
                                            MessageBox.Show("A(z) " + paros.Vonatszam + " és a(z) " + paratlan.Vonatszam + " számú vonatok már leközlekedett a bejárati vágányútjukon, azaz megérkeztek az állomásra (minden járművük a biztonsági határjelzőkön belül ért), a B jelű bejárati jelzőt mégsem állítottad vissza Megállj!-állásba!\nÁllítsd vissza a B jelű bejárati jelzőt Megállj!-állásba, majd adj visszajelentést " + Index.GetSzomszedAllomasNev(allomas.Name, true) + " és " + Index.GetSzomszedAllomasNev(allomas.Name, false) + " állomásoknak!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            Ind.Hibak[26] += 2;
                                        }
                                        else
                                        {//egyik sem
                                            MessageBox.Show("A(z) " + paros.Vonatszam + " és a(z) " + paratlan.Vonatszam + " számú vonatok már leközlekedett a bejárati vágányútjukon, azaz megérkeztek az állomásra (minden járművük a biztonsági határjelzőkön belül ért), az A és a B jelű bejárati jelzőket már vissza is állítottad Megállj!-állásba, visszajelentést mégsem adtál a vonatokról!\nAdj visszajelentést " + Index.GetSzomszedAllomasNev(allomas.Name, true) + " és " + Index.GetSzomszedAllomasNev(allomas.Name, false) + " állomásoknak!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            Ind.Hibak[26] += 2;
                                        }
                                    }
                                }
                            }
                            else
                            {//csak páros van -- érintett bej. j.: A
                                if (allomas.Jelzok[0].Szabad && !allomas.Jelzok[0].Fenyjelzo)
                                {//bej. j. szabadban
                                    MessageBox.Show("A(z) " + visszjelKell[0].Vonatszam + " számú vonat már leközlekedett a bejárati vágányútján, azaz megérkezett az állomásra (minden járműve a biztonsági határjelzőn belül ért), az A jelű bejárati jelzőt mégsem állítottad vissza Megállj!-állásba!\nÁllítsd vissza az A jelű bejárati jelzőt Megállj!-állásba, majd adj visszajelentést " + Index.GetSzomszedAllomasNev(allomas.Name, true) + " állomásnak!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ind.Hibak[26]++;
                                }
                                else
                                {//már visszaállította
                                    MessageBox.Show("A(z) " + visszjelKell[0].Vonatszam + " számú vonat már leközlekedett a bejárati vágányútján, azaz megérkezett az állomásra (minden járműve a biztonsági határjelzőn belül ért), az A jelű bejárati jelzőt már vissza is állítottad Megállj!-állásba, visszajelentést mégsem adtál a vonatról!\nAdj visszajelentést " + Index.GetSzomszedAllomasNev(allomas.Name, true) + " állomásnak!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ind.Hibak[26]++;
                                }
                            }
                        }
                        else if(paratlanVan)
                        {//csak páratlan van -- érintett bej. j.: B
                            if (allomas.Jelzok[5].Szabad && !allomas.Jelzok[5].Fenyjelzo)
                            {//bej. j. szabadban
                                MessageBox.Show("A(z) " + visszjelKell[0].Vonatszam + " számú vonat már leközlekedett a bejárati vágányútján, azaz megérkezett az állomásra (minden járműve a biztonsági határjelzőn belül ért), a B jelű bejárati jelzőt mégsem állítottad vissza Megállj!-állásba!\nÁllítsd vissza a B jelű bejárati jelzőt Megállj!-állásba, majd adj visszajelentést " + Index.GetSzomszedAllomasNev(allomas.Name, false) + " állomásnak!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ind.Hibak[26]++;
                            }
                            else
                            {//már visszaállította
                                MessageBox.Show("A(z) " + visszjelKell[0].Vonatszam + " számú vonat már leközlekedett a bejárati vágányútján, azaz megérkezett az állomásra (minden járműve a biztonsági határjelzőn belül ért), a B jelű bejárati jelzőt már vissza is állítottad Megállj!-állásba, visszajelentést mégsem adtál a vonatról!\nAdj visszajelentést " + Index.GetSzomszedAllomasNev(allomas.Name, false) + " állomásnak!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ind.Hibak[26]++;
                            }
                        }
                    }                    
                    break;
                case 98: //indulási szükséges_Tick
                    if (allomas.IndulasiSzukseges.Count == 1)
                    {
                        MessageBox.Show("A(z) " + allomas.IndulasiSzukseges[0].Vonatszam + " számú vonatot már elmenesztetted, mégsem adtál róla indulásiidő-közlést!\nA vonatok indulási idejét a menesztést követően azonnal közölni kell a szomszéd állomás rendelkezőjével.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ind.Hibak[24]++;
                    }
                    else
                    {
                        string seged = "";
                        for (int i = 0; i < allomas.IndulasiSzukseges.Count - 1; i++)
                        {
                            seged += allomas.IndulasiSzukseges[i].Vonatszam + ", ";
                        }
                        seged += allomas.IndulasiSzukseges[allomas.IndulasiSzukseges.Count - 1].Vonatszam;
                        MessageBox.Show("A következő vonatokat már elmenesztetted, mégsem adtál róluk indulásiidő-közlést: " + seged + " A vonatok indulási idejét az indulást követően azonnal közölni kell a szomszéd állomás rendelkezőjével.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ind.Hibak[24]++;
                    }
                    break;
                case 99: //engedély lejárt: object[] 1 elemű: a vonat, aminek lejárt az engedélye, //a bool azt adja meg, hogy az Allomaskoz, ahol lejárt az engedély, az a Szakaszok listában a [0]-e
                    ind.Hibak[25]++;
                    if ((parameters[0] as Vonat).Paros)
                    {//páros
                        if (kpFeleHiv)
                        {//Allomas.Szakaszok.IndexOf(Allomaskoz) == 0
                            MessageBox.Show("A(z) " + (parameters[0] as Vonat).Vonatszam + " számú vonat engedélye " + Index.GetSzomszedAllomasNev(Allomas.Name, true) + " és " + Allomas.Name + " állomások között lejárt.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {//Allomas.Szakaszok.IndexOf(Allomaskoz) == 6
                            MessageBox.Show("A(z) " + (parameters[0] as Vonat).Vonatszam + " számú vonat engedélye " + Allomas.Name + " és " + Index.GetSzomszedAllomasNev(Allomas.Name, false) + " állomások között lejárt.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {//páratlan
                        if (kpFeleHiv)
                        {//Allomas.Szakaszok.IndexOf(Allomaskoz) == 0
                            MessageBox.Show("A(z) " + (parameters[0] as Vonat).Vonatszam + " számú vonat engedélye " + Allomas.Name + " és " + Index.GetSzomszedAllomasNev(Allomas.Name, true) + " állomások között lejárt.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {//Allomas.Szakaszok.IndexOf(Allomaskoz) == 6
                            MessageBox.Show("A(z) " + (parameters[0] as Vonat).Vonatszam + " számú vonat engedélye " + Index.GetSzomszedAllomasNev(Allomas.Name, false) + " és " + Allomas.Name + " állomások között lejárt.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void KpFeleEngedelyLejar(object sender, EventArgs e)
        {

        }

        public void VpFeleEngedelyLejar(object sender, EventArgs e)
        {

        }

        public void FormStarted()
        {
            if (kpTel != null)
                kpTel.Click += kpTel_Click;
            if (vpTel != null)
                vpTel.Click += vpTel_Click;
        }

        public virtual void Frissit()
        {
            if (Hr != null && Hr.Frissitheto)
            {
                Hr.vszkRefresh();
            }

            bool allA = Allomas.Name == "Széchenyi-hegy";

            //ha a vágányokon van vonat, és még nincs benne a fogadottVonatok listában, addolja! (vágányok indexei: [3], [8])
            //visszajelentésre figyelmeztetés timer kezelése
            if (Allomas.Szakaszok[3].Vonat != null && Allomas.Szakaszok[3].Vonat.Vonatszam != "GÉP" && !Allomas.FogadottVonatok.Contains(Allomas.Szakaszok[3].Vonat) && Allomas.Szakaszok[3].Vonat != Allomas.Szakaszok[2].Vonat && Allomas.Szakaszok[3].Vonat != Allomas.Szakaszok[4].Vonat)
            {
                if (!(Allomas.Vegallomas && allA == Allomas.Szakaszok[3].Vonat.Paros))
                {//ha A-n vagyunk, és a vonat páros, akkor már megfordult, ha O-n vagyunk, és páratlan, akkor is
                    Allomas.FogadottVonatok.Add(Allomas.Szakaszok[3].Vonat);
                    if (Allomas.Vegallomas && !Allomas.Szakaszok[3].Vonat.KoruljarasSzukseges)
                    {//nem kell körüljárnia
                        Allomas.Szakaszok[3].Vonat.Allomas = Allomas;
                        Allomas.Szakaszok[3].Vonat.VonatszamCsere();
                        Frissit();
                    }
                }                
            }
            if (Allomas.Szakaszok[8].Vonat != null && Allomas.Szakaszok[8].Vonat.Vonatszam != "GÉP" && !Allomas.FogadottVonatok.Contains(Allomas.Szakaszok[8].Vonat) && Allomas.Szakaszok[8].Vonat != Allomas.Szakaszok[7].Vonat && Allomas.Szakaszok[8].Vonat != Allomas.Szakaszok[9].Vonat)
            {
                if (!(Allomas.Vegallomas && allA == Allomas.Szakaszok[8].Vonat.Paros))
                {
                    Allomas.FogadottVonatok.Add(Allomas.Szakaszok[8].Vonat);
                    if (Allomas.Vegallomas && !Allomas.Szakaszok[8].Vonat.KoruljarasSzukseges)
                    {//nem kell körüljárnia
                        Allomas.Szakaszok[8].Vonat.Allomas = Allomas;
                        Allomas.Szakaszok[8].Vonat.VonatszamCsere();
                        Frissit();
                    }
                }
            }                
        }

        public void FormMegcsengettek(bool kpFelolHivnak)
        {
            if (kpFelolHivnak)
            {//kp >> vp (2 hosszú)
                BejovoHivas bh = new BejovoHivas(allomas.KezdopontiAllomas, allomas, Ind);
                bh.ShowDialog();
            }
            else
            {//vp >> kp (1 hosszú)
                BejovoHivas bh = new BejovoHivas(allomas.VegpontiAllomas, allomas, Ind);
                bh.ShowDialog();
            }
        }

        public void FormVisszacsengettek(bool kpFelolHivnak)
        {
            if (!kpFelolHivnak)
            {//kp >> vp (visszacsengetés: vp >> kp)
                KozlemenyValaszto kv = new KozlemenyValaszto(allomas, allomas.VegpontiAllomas, Ind);
                kv.ShowDialog();
            }
            else
            {//vp >> kp (visszacsengetés: kp >> vp)
                KozlemenyValaszto kv = new KozlemenyValaszto(allomas, allomas.KezdopontiAllomas, Ind);
                kv.ShowDialog();
            }
        }

        public void FormMegszakitva(bool kpFeleHiv)
        {
            ind.notifyIcon1.ShowBalloonTip(100, "Telefon", Index.GetSzomszedAllomasNev(Allomas.Name, !kpFeleHiv) + " állomás befejezte a hívást", ToolTipIcon.None);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AllomasForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.KeyPreview = true;
            this.Name = "AllomasForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AllomasForm_KeyDown);
            this.ResumeLayout(false);

        }

        private void AllomasForm_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Z && e.Modifiers == (Keys.Alt | Keys.Control))
            //{
            //    MessageBox.Show("Test");
            //}
        }

        public void AllomasFormAllomasLezar(Label allnevLabel, Label allhivoLabel)
        {
            if (Allomas.Lezarva)
            {
                MessageBox.Show("Ezt az állomást már lezártad.", "Hoppá", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Biztosan lezárod az állomást?\n\nEzt csak akkor tedd, ha nincs elég gyerek az összes állomáshoz. Az állomás lezárását semmilyen módon nem lehet visszavonni!", "Lezárás megerősítése", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    Jelszo jelsz = new Jelszo("Lezárás megerősítése", "Lezárás");
                    if (jelsz.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Allomas.AllomasLezar();
                        allnevLabel.BackColor = System.Drawing.Color.Red;
                        allhivoLabel.Text = "LEZÁRT ÁLLOMÁS!";
                        allhivoLabel.BackColor = System.Drawing.Color.Red;
                        allhivoLabel.Location = new System.Drawing.Point(this.Width / 2 - allhivoLabel.Width / 2, allhivoLabel.Location.Y);
                        this.Text += " -- LEZÁRVA!";
                        foreach (Control ctrl in this.Controls)
                        {
                            ctrl.Enabled = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Jelszóval védett tartalom. Helytelen jelszó.\n\nNe piszkáld!", "Helytelen jelszó", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }
    }
}
