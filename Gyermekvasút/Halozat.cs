using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using Gyermekvasút.Modellek;
using System.ServiceModel.Description;
using System.Threading;
using Gyermekvasút.Hálózat;

namespace Gyermekvasút
{
    public partial class Halozat : Form
    {

        Allomas allomas;
        ServiceHost host;
        public bool crash;

        string vpAll;
        string kpAll;

        StartScreen startscreen;

        public Halozat(string allomasNeve, StartScreen ss)
        {
            startscreen = ss;

            InitializeComponent();
            switch (allomasNeve)
            {
                case "a":
                    allomas = new Allomas("Széchenyi-hegy");
                    SetLabel(null, "Virágvölgy");
                    break;
                case "u":
                    allomas = new Allomas("Csillebérc");
                    SetLabel("Széchenyi-hegy", "Virágvölgy");
                    break;
                case "l":
                    allomas = new Allomas("Virágvölgy");
                    SetLabel("Széchenyi-hegy", "János-hegy");
                    break;
                case "i":
                    allomas = new Allomas("János-hegy");
                    SetLabel("Virágvölgy", "Szépjuhászné");
                    break;
                case "s":
                    allomas = new Allomas("Szépjuhászné");
                    SetLabel("János-hegy", "Hárs-hegy");
                    break;
                case "h":
                    allomas = new Allomas("Hárs-hegy");
                    SetLabel("Szépjuhászné", "Hűvösvölgy");
                    break;
                case "o":
                    allomas = new Allomas("Hűvösvölgy");
                    SetLabel("Hárs-hegy", null);
                    break;
                default:
                    MessageBox.Show("A parancssorból beolvasott állomásnév-paraméter nem feleltethető meg egyetlen ismert állomásnévnek sem.\nA program leáll.", "Összeomlás", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.Crash = true;
                    crash = true;
                    break;
            }
        } //sets allomas.Name

        private void Halozat_Load(object sender, EventArgs e)
        {
            if (crash)
            {
                this.Close();
                return;
            }

            allomas.DebugPingChanged += allomas_DebugPingChanged;

            Uri baseAddress = new ChannelFactory<IAllomas>(allomas.Name).Endpoint.Address.Uri;
            host = new ServiceHost(allomas, baseAddress);

            // Enable metadata publishing.
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;

            host.Description.Behaviors.Add(smb);

            // Open the ServiceHost to start listening for messages. Since
            // no endpoints are explicitly configured, the runtime will create
            // one endpoint per base address for each service contract implemented
            // by the service.
            try
            {
                host.Open();
            }
            catch (AddressAlreadyInUseException)
            {
                MessageBox.Show(allomas.Name + " állomás már elindult ezen a hálózaton!\nEgy hálózaton ugyanaz az állomás csak egyszer indítható el.\nKilépés\n\nEz azért történt, mert ezen a hálózaton már elindult ez az állomás (hiba utáni újboli elindításkor is láthatod ezt az ablakot). Ha valóban így van, zárd be az adott programot. Ha elég magabiztosan mozogsz a Feladatkezelőben, esetleg lesd meg a Folyamatok között, hátha ott elkapod a kis rohadékot. Ha ezek sem segítettek, van egy ötletünk! Futtasd a HostClose.exe-t, és írd bele az adott állomás betűjelét (kisebtűvel).\nHa ez sem oldja meg, marad a klasszikus megoldás: indítsd újra a gépet! :)", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Crash = true;
                this.Close();
            }

            
            //IAllomas-ok létrehozása
            #region clientWorker
            bool kp_kapcsolva = false;
            bool vp_kapcsolva = false;

            if (vpAll == null)
                vp_kapcsolva = true;
            if (kpAll == null)
                kp_kapcsolva = true;

            while (!kp_kapcsolva || !vp_kapcsolva)
            {
                //kezdőponti
                if (!kp_kapcsolva)
                {
                    try
                    {
                        IAllomas all = new Hálózat.AllomasClient(kpAll);
                        allomas.KezdopontiAllomas = all;
                        kp_kapcsolva = true;
                        //clientWorker.ReportProgress(50, 
                    }
                    catch (Exception)
                    {
                        //clientWorker.ReportProgress(0, ex);
                    }
                }
                //végponti
                if (!vp_kapcsolva)
                {
                    try
                    {
                        IAllomas all = new Hálózat.AllomasClient(vpAll);
                        allomas.VegpontiAllomas = all;
                        vp_kapcsolva = true;
                        //clientWorker.ReportProgress(50, 
                    }
                    catch (Exception)
                    {
                        //clientWorker.ReportProgress(0, ex);
                    }
                }
            }
            #endregion

            allomas.Kesz += End;

            label2.Text = allomas.Name;
            label2.Location = new Point(this.Width / 2 - label2.Width / 2, label2.Location.Y);

            #region OLD
            //clientWorker.RunWorkerAsync();

            //allomas.SzomszedAllomasFeleledt += SzomszedAllomasFeleledt;
            //timer1.Start();
            #endregion
        }

        void allomas_DebugPingChanged()
        {
            if (allomas.DebugValaszoltak)
            {
                status.Text = "Kp. már válaszolt is";
            }
            else if (allomas.DebugPinged)
            {
                status.Text = "Vp. már megpingelt";
            }
            status.Location = new Point(499 - status.Width, status.Location.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
                
        void SetLabel(string kezdop, string vegp)
        {
            if (kezdop == null)
            {
                vpAll = vegp;
            }
            else if (vegp == null)
            {
                kpAll = kezdop;
            }
            else
            {
                kpAll = kezdop;
                vpAll = vegp;
            }
        }

        private void clientWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //kliensek létrehozása ciklusban try-catchel
            bool kp_kapcsolva = false;
            bool vp_kapcsolva = false;

            if (vpAll == null)
                vp_kapcsolva = true;
            if (kpAll == null)
                kp_kapcsolva = true;

            while (!kp_kapcsolva || !vp_kapcsolva)
            {
                //kezdőponti
                if (!kp_kapcsolva)
                {
                    try
                    {
                        IAllomas all = new Hálózat.AllomasClient(kpAll);
                        allomas.KezdopontiAllomas = all;
                        kp_kapcsolva = true;
                        //clientWorker.ReportProgress(50, 
                    }
                    catch (Exception)
                    {
                        //clientWorker.ReportProgress(0, ex);
                    }
                }
                //végponti
                if (!vp_kapcsolva)
                {
                    try
                    {
                        IAllomas all = new Hálózat.AllomasClient(vpAll);
                        allomas.VegpontiAllomas = all;
                        vp_kapcsolva = true;
                        //clientWorker.ReportProgress(50, 
                    }
                    catch (Exception)
                    {
                        //clientWorker.ReportProgress(0, ex);
                    }
                }
            }

            #region OLD
            //if (vpAll == null)
            //    allomas.VpAllEl = true;
            //if (kpAll == null)
            //    allomas.KpAllEl = true;

            //while (!allomas.KpAllEl || !allomas.VpAllEl)
            //{//még nem él minden állomás
            //    if (!allomas.KpAllEl)
            //    {//kpAll nem él
            //        allomas.KezdopontiAllomas.Ping(true);
            //    }
            //    if (!allomas.VpAllEl)
            //    {//vpAll nem él
            //        allomas.VegpontiAllomas.Ping(false);
            //    }

            //    if (allomas.KpAllEl)
            //        kpPB.Value = 100;
            //    if (allomas.VpAllEl)
            //        vpPB.Value = 100;
            //}
            #endregion
        }

        private void clientWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {//ha az egyik már csatlakozott
            //e.UserState
            throw (Exception)e.UserState;
        }

        private void clientWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            allomas.KezdopontiAllomas.Ping(true);
            allomas.VegpontiAllomas.Ping(false);

            Index ind = new Index(allomas, host, this, startscreen);
            this.Hide();
            ind.Show();
        }

        private void Halozat_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Program.Crash)
            {
                if (MessageBox.Show("Biztosan ki akarsz lépni a programból?", "Megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            if (host != null)
                host.Close();
            startscreen.Close();
        }

        #region OLD
        //public static void SzomszedAllomasFeleledt(bool kp)
        //{
        //    if (HalozatPld != null)
        //    {
        //        if (kp)
        //        {
        //            HalozatPld.kpPB.Value = 100;
        //            HalozatPld.kpPB.Style = ProgressBarStyle.Blocks;
        //            if (HalozatPld.allomas.KezdopontiAllomas != null)
        //                HalozatPld.allomas.KezdopontiAllomas.Ping(true);
        //        }
        //        else
        //        {
        //            HalozatPld.vpPB.Value = 100;
        //            HalozatPld.vpPB.Style = ProgressBarStyle.Blocks;
        //            if (HalozatPld.allomas.VegpontiAllomas != null)
        //                HalozatPld.allomas.VegpontiAllomas.Ping(false);
        //        }
        //    }
        //    if (HalozatPld.allomas.KpAllEl && HalozatPld.allomas.VpAllEl)
        //    {
        //        HalozatPld.End();
        //    }
        //}
        #endregion

        public void End()
        {
            #region OLD
            //timer1.Stop();
            //if (allomas.KezdopontiAllomas != null)
            //    allomas.KezdopontiAllomas.Ping(true);
            //if (allomas.VegpontiAllomas != null)
            //    allomas.VegpontiAllomas.Ping(false);
            #endregion

            Index ind = new Index(allomas, host, this, startscreen);
            this.Hide();
            ind.Show();
        }

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Interval = 1000 };

        private void Halozat_Shown(object sender, EventArgs e)
        {
            timer.Tick += timer_Tick;
            timer.Start(); //ez csak késleltetés a Shown-tól, hogy mindent kirajzoljon
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            if (allomas.Name == "Hűvösvölgy")
            {//start
                allomas.TryHuvosvolgy();
            }
        }

        private void Halozat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q && e.Modifiers == Keys.Control)
            {
                //quickstart
                allomas.OnKesz();
                DEBUG.Log.AddLogText("QuickStart\r\n*A Szimulátor hálózatos módban indult ugyan el, de nem várta meg, hogy az egész vonal felépüljön (CTRL + Q).");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ez a kis sarokban megbúvó felirat azt hivatott jelezni, hogy a szomszéd állomásokkal való kommunikáció melyik stádiumban van.\n\nHa szeretnéd megérteni a dolgot, hát elmagyarázom, hogyan is működik ez. Hűvösvölgy állomás addig szólongatja Hárs-hegyet, amíg az nem válaszol neki, hogy \"Oké, oké, értem, hogy téged már bekapcsoltak.\" Miután Hárs-hegy válaszolt Hűvsövölgynek, Hűvösvölgy már nem szólongatja Hárs-hegyet, Hárs-hegy viszont elkezdi szólongatni Szépjuhásznét, és így tovább. Ha a végén már Széchenyi-hegynek is szóltak, az azt jelenti, hogy minden gépet bekapcsoltak, vagyis indulhat a banzáj. Ekkor Széchenyi-hegytől elkezdve minden állomás szól a végponti szomszédjának, hogy induljon el.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        #region OLD
        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    if (allomas.KezdopontiAllomas != null)
        //    {
        //        allomas.KezdopontiAllomas.Ping(true);
        //    }
        //    else
        //    {
        //        allomas.KpAllEl = true;
        //    }
        //    if (allomas.VegpontiAllomas != null)
        //    {
        //         allomas.VegpontiAllomas.Ping(false);
        //    }
        //    else
        //    {
        //        allomas.VpAllEl = true;
        //    }
        //}
        #endregion
    }
}