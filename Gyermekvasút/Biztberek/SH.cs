using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút.Biztberek
{
    public partial class SH : UserControl
    {
        public SH()
        {
            InitializeComponent();
            induktorTimer.Tick += induktorTimer_Tick;
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
        public void InduktorTeker()
        {
            #region Megjelenés
            induktorTimer.Start();
            #endregion
            if (blokkbillLehuzva == BejaratiJelzoBlokkbillentyu) //~ RET2 jelző szabadba állítás
            {
                if (blokkbillLehuzva.Blokk.Allas == SHBlokkAllas.Lezart)
                {//feloldás
                    
                }
            }
            else if (blokkbillLehuzva == VaganyutiBlokkbillentyu) //~ RET2 váltó feloldás
            {
                if (blokkbillLehuzva.Blokk.Allas == SHBlokkAllas.Lezart)
                {//feloldás

                }
            }
            else if (blokkbillLehuzva == KijaratiJelzoBlokkbillentyu) //~ RET2 jelző szabadba állítás
            {
                if (blokkbillLehuzva.Blokk.Allas == SHBlokkAllas.Lezart)
                {//feloldás

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
        }
        Timer induktorTimer = new Timer() { Interval = 30 };

        public void Csengetes() //~ RET2 váltó lezárás
        {

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
            //sender: a _Clickben rögtön ezt a fgv-t hívja, nem változtat propertyt
            if (sender.Lehuzva)//old
            {//lent>fent
                sender.Lehuzva = false;
                blokkbillLehuzva = null;
            }
            else
            {//fent>lent
                if (sender.Blokk.Tipus == SHBlokkTipus.Vaganyuti || (sender.Blokk.Tipus == SHBlokkTipus.BejaratiJelzo && Iranykallantyu.Allas == SHIranykallantyuAllas.Bejarat) || (sender.Blokk.Tipus == SHBlokkTipus.KijaratiJelzo && Iranykallantyu.Allas == SHIranykallantyuAllas.Kijarat))
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
    }
}
