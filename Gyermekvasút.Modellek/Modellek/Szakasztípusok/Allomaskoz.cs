using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gyermekvasút.Modellek;

namespace Gyermekvasút
{
    public class Allomaskoz : Szakasz
    {
        public Allomaskoz() { }

        public Allomaskoz(int szakaszHossza, Jelzo parosOldalonLevoJelzo, string NAME, Allomas allomasBE)
        {
            Hossz = szakaszHossza;
            Jelzo = parosOldalonLevoJelzo;
            Name = NAME;

            Allomas = allomasBE;

            engedelyLejar = new Timer();
            engedelyLejar.Interval = 1000 * 60 * 10 / Gyermekvasút.Modellek.Settings.SebessegOszto; //10 perc: 1000*60*10
            engedelyLejar.Tick += engedelyLejar_Tick;
        }

        void engedelyLejar_Tick(object sender, EventArgs e)
        {
            Vonat v = Engedelyben == null ? Engedelyben2 : Engedelyben;
            if (Engedelyben2 == null)
            {
                Engedelyben = null;
            }
            else
            {
                Engedelyben2 = null;
            }
            engedelyLejar.Stop();
            Allomas.OnKozlemeny(Allomas.Szakaszok.IndexOf(this) == 0, 99, new object[] { v });
        }

        Allomas allomas;
        public Allomas Allomas
        {
            get { return allomas; }
            set { allomas = value; }
        }

        Vonat engedelyben;
        public Vonat Engedelyben
        {
            get { return engedelyben; }
            set
            {
                engedelyben = value;
            }
        }
        Vonat engedelyben2;
        public Vonat Engedelyben2
        {
            get { return engedelyben2; }
            set { engedelyben2 = value; }
        }

        Vonat utolsoVonat;
        public Vonat UtolsoVonat
        {
            get { return utolsoVonat; }
            set { utolsoVonat = value; }
        }

        Timer engedelyLejar;
        public Timer EngedelyLejar
        {
            get { return engedelyLejar; }
        }

        public void TimerStart()
        {
            EngedelyLejar.Interval = 1000 * 60 * 10 / Gyermekvasút.Modellek.Settings.SebessegOszto;
            EngedelyLejar.Start();
        }

        public void TimerStop()
        {
            EngedelyLejar.Stop();
        }
    }
}
