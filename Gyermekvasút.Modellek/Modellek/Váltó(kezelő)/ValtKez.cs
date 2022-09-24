using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút
{
    public class ValtKez : AzonosithatoObj
    {
        public ValtKez(bool elsoVaganyEgyenesBEMENET, Valto valtoBEMENET, string NAME_be, bool retesz = false)
        {
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            elsoVaganyEgyenes = elsoVaganyEgyenesBEMENET;
            //timer.Interval = 6000 / Gyermekvasút.Modellek.Settings.SebessegOszto; //vágányút-beállítás ideje - tizedérték (progressbar.value növelése miatt)
            timer.Interval = 6000;
            valto = valtoBEMENET;
            name = NAME_be;
            ReteszesLezaras = retesz;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (ProgressBar == 9)
            {
                ProgressBar = 10;
            }
            else if (ProgressBar == 10)
            {//bejelentés
                timer.Stop();
                Valto.AllitasAlatt = false;
                if ((elsoVaganyEgyenes && vagany == 1) || (!elsoVaganyEgyenes && vagany == 2))
                {//E
                    valto.Allas = true;
                }
                else
                {//K
                    valto.Allas = false;
                }
                if (!ReteszesLezaras)
                {
                    valto.Lezart = true;
                }
                if ((be != null && ki == null) || (be != null && ki != null && bejarat))
                {
                    MessageBox.Show("Rendelkező pajtás!\nA(z) " + be.Vonatszam + " számú vonat bejárata szabad a(z) " + VaganyString + " vágányra.", "Vágányút-beállítás bejelentése", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if ((be == null && ki != null) || (be != null && ki != null && !bejarat))
                {
                    MessageBox.Show("Rendelkező pajtás!\nA(z) " + ki.Vonatszam + " számú vonat kijárata szabad a(z) " + VaganyString + " vágányról.", "Vágányút-beállítás bejelentése", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (valto.Lezart)
                {
                    valto.Lezart = false;
                }                
                ProgressBar++;
            }
            OnFrissulj();
        }

        public event FrissuljDelegate Frissulj;
        private void OnFrissulj()
        {
            if (Frissulj != null)
            {
                Frissulj();
            }
        }

        bool formOpen;
        public bool FormOpen
        {
            get { return formOpen; }
            set { formOpen = value; }
        }

        Timer timer;
        public Timer Timer
        {
            get { return timer; }
            set { timer = value; }
        }

        int vagany;
        public int Vagany
        {
            get { return vagany; }
            set { vagany = value; }
        }
        Vonat be;
        public Vonat Be
        {
            get { return be; }
            set { be = value; }
        }
        Vonat ki;
        public Vonat Ki
        {
            get { return ki; }
            set { ki = value; }
        }
        Valto valto;
        public Valto Valto
        {
            get { return valto; }
            set { valto = value; }
        }
        bool elsoVaganyEgyenes;
        public bool ElsoVaganyEgyenes
        {
            get { return elsoVaganyEgyenes; }
            set { elsoVaganyEgyenes = value; }
        }

        int progressBar;
        public int ProgressBar
        {
            get { return progressBar; }
            set
            {
                progressBar = value;
                if (Progbar != null)
                    progbar.Value = progressBar;
            }
        }

        ToolStripProgressBar progbar;
        public ToolStripProgressBar Progbar
        {
            get { return progbar; }
            set { progbar = value; }
        }

        public bool VaganyutatNeKezdEl { get; set; }

        bool bejarat; //ha ellenvonattal jár ki, és a be-nek és a ki-nek is van értéke, ez mutatja meg, hogy a bejárat beállításánál tart-e vagy már a kijáratnál
        public bool Bejarat
        {
            get { return bejarat; }
            set
            {
                bejarat = value;
                if (Be != null && Ki != null && bejarat == false)
                {//bejárt a vonat, váltunk kijáratra
                    if (VaganyutatNeKezdEl)
                    {
                        VaganyutatNeKezdEl = false;
                        return;
                    }
                    Be = null;
                    ProgressBar = 0;
                    Valto.AllitasAlatt = true;
                    Timer.Start();
                }
            }
        }

        public string VaganyString
        {
            get
            {
                if (vagany == 1)
                {
                    return "I.";
                }
                else if (vagany == 0)
                {
                    return "NULLA";
                }
                else
                {
                    return "II.";
                }
            }
        }

        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public override string ToString()
        {
            return Name;
        }

        bool reteszesLezaras;
        public bool ReteszesLezaras
        {
            get { return reteszesLezaras; }
            set { reteszesLezaras = value; }
        }

        public static string MasikvaganyStringMegad(string vaganyString)
        {
            if (vaganyString == "I.")
            {
                return "II.";
            }
            else
            {
                return "I.";
            }
        }

        public bool SzechenyiHegy { get; set; }
    }
}