using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using Gyermekvasút.Modellek;

namespace Gyermekvasút
{
    public partial class S : AllomasForm
    {//MEGÁLLAPODÁS SZERINT MINDEN SZAKASZ JELZO-JÉN ANNAK A PÁROS FELÉN LÉVŐ JELZŐT ÉRTJÜK!
        Pen keret = new Pen(Color.FromArgb(132, 192, 125));
        Pen feketePen = new Pen(Color.FromArgb(0, 0, 0));
        Pen vorosPen = new Pen(Color.FromArgb(237, 28, 36));

        Pen gyuruZoldPen = new Pen(Color.FromArgb(132, 192, 125));
        Pen gombZoldPen = new Pen(Color.FromArgb(13, 236, 15));
        Pen szurkePen = new Pen(Color.FromArgb(127, 127, 127));
        Pen feherPen = new Pen(Color.FromArgb(255, 255, 202));

        SolidBrush zold = new SolidBrush(Color.FromArgb(88, 166, 82));
        SolidBrush fekete = new SolidBrush(Color.FromArgb(0, 0, 0));
        SolidBrush szurke = new SolidBrush(Color.FromArgb(127, 127, 127));
        SolidBrush voros = new SolidBrush(Color.FromArgb(237, 28, 36));
        SolidBrush gyuruZold = new SolidBrush(Color.FromArgb(132, 192, 125));
        SolidBrush feher = new SolidBrush(Color.FromArgb(255, 255, 202));
        SolidBrush gombZold = new SolidBrush(Color.FromArgb(13, 236, 15));

        PictureBox senderPB = null;
        bool villogtatas = false;

        SoundPlayer player = new SoundPlayer(@"s_zav.gyv");

        public S(Index index, Allomas allomas)
        {
            InitializeComponent();
            Ind = index;
            Allomas = allomas;

            senderPB = null;
            blinker.Enabled = true;
            blinker.Start();

            koSZ.BackColor = szurke.Color;
            szigkiSZ.BackColor = szurke.Color;
            hivoSZ.BackColor = szurke.Color;
        }

        public S()
        {
            InitializeComponent();
        }

        #region Vágányutak (első lezárások)
        void vaganyut_A_V2()
        {
            if (Allomas.Vaganyutak[4].Felepitett == false && Allomas.Vaganyutak[5].Felepitett == false && Allomas.Vaganyutak[0].Felepitett == false && Allomas.Vaganyutak[6].Felepitett == false && Allomas.Vaganyutak[7].Felepitett == false && Allomas.Vaganyutak[3].Felepitett == false)
            {
                if ((Allomas.Valtok[0] as ValtoS).Allas == true)
                {
                    Allomas.Vaganyutak[1].Felepitett = true;
                    if ((Allomas.Valtok[0] as ValtoS).AllitasAlatt == true)
                    {
                        (Allomas.Valtok[0] as ValtoS).VgutasAllitas = true; //hogy kivárja a csíkrajzolással az állítás befejezését
                    }
                    ValtoLezaras((ValtoS)(Allomas.Valtok[0]));
                }
                else
                {
                    //vágányutas állítás 2 K>>E
                    Allomas.Vaganyutak[1].Felepitett = true;
                    (Allomas.Valtok[0] as ValtoS).VgutasAllitas = true;
                    ValtoAllitas(Allomas.Valtok[0]);
                    ValtoLezaras((ValtoS)(Allomas.Valtok[0]));
                }
                SikeresKezeles();
            }
            else
            {
                if ((Allomas.Valtok[0] as ValtoS).Allas == false && (Allomas.Valtok[0] as ValtoS).Foglalt == false && (Allomas.Valtok[0] as ValtoS).Lezart == false)
                {
                    ValtoAllitas(Allomas.Valtok[0]);
                    SikeresKezeles();
                    Frissit();
                }
                else
                {
                    if (senderPB == A)
                    {
                        senderPB = V2;
                    }
                    else
                    {
                        senderPB = A;
                    }
                }
            }
            Frissit();
        }
        void vaganyut_A_V1()
        {
            if (Allomas.Vaganyutak[4].Felepitett == false && Allomas.Vaganyutak[5].Felepitett == false && Allomas.Vaganyutak[1].Felepitett == false && Allomas.Vaganyutak[6].Felepitett == false && Allomas.Vaganyutak[7].Felepitett == false && Allomas.Vaganyutak[2].Felepitett == false)
            {
                if ((Allomas.Valtok[0] as ValtoS).Allas == false)
                {
                    Allomas.Vaganyutak[0].Felepitett = true;
                    if ((Allomas.Valtok[0] as ValtoS).AllitasAlatt == true)
                    {
                        (Allomas.Valtok[0] as ValtoS).VgutasAllitas = true; //hogy kivárja a csíkrajzolással az állítás befejezését
                    }
                    ValtoLezaras((ValtoS)(Allomas.Valtok[0]));
                }
                else
                {
                    //vágányutas állítás 2 E>>K
                    Allomas.Vaganyutak[0].Felepitett = true;
                    (Allomas.Valtok[0] as ValtoS).VgutasAllitas = true;
                    ValtoAllitas(Allomas.Valtok[0]);
                    ValtoLezaras((ValtoS)(Allomas.Valtok[0]));
                }
                SikeresKezeles();
                Frissit();
            }
            else
            {
                if ((Allomas.Valtok[0] as ValtoS).Allas == true && (Allomas.Valtok[0] as ValtoS).Lezart == false && (Allomas.Valtok[0] as ValtoS).Foglalt == false)
                {
                    ValtoAllitas(Allomas.Valtok[0]);
                    SikeresKezeles();
                    Frissit();
                }
                else
                {
                    if (senderPB == A)
                    {
                        senderPB = V1;
                    }
                    else
                    {
                        senderPB = A;
                    }
                }
            }
        }
        void vaganyut_V2_ki()
        {
            if (Allomas.Vaganyutak[5].Felepitett == false && Allomas.Vaganyutak[4].Felepitett == false && Allomas.Vaganyutak[3].Felepitett == false && Allomas.Vaganyutak[0].Felepitett == false)
            {
                if ((Allomas.Valtok[1] as ValtoS).Allas == true)
                {
                    Allomas.Vaganyutak[2].Felepitett = true;
                    if ((Allomas.Valtok[1] as ValtoS).AllitasAlatt == true)
                    {
                        (Allomas.Valtok[1] as ValtoS).VgutasAllitas = true; //hogy kivárja a csíkrajzolással az állítás befejezését
                    }
                    ValtoLezaras((ValtoS)(Allomas.Valtok[1]));
                }
                else
                {
                    //vágányutas állítás
                    Allomas.Vaganyutak[2].Felepitett = true;
                    (Allomas.Valtok[1] as ValtoS).VgutasAllitas = true;
                    ValtoAllitas(Allomas.Valtok[1]);
                    ValtoLezaras((ValtoS)(Allomas.Valtok[1]));
                }
                SikeresKezeles();
                Frissit();
            }
            else
            {
                if ((Allomas.Valtok[1] as ValtoS).Allas == false && (Allomas.Valtok[1] as ValtoS).Foglalt == false && (Allomas.Valtok[1] as ValtoS).Lezart == false)
                {
                    ValtoAllitas(Allomas.Valtok[1]);
                    SikeresKezeles();
                    Frissit();
                }
                else
                {
                    if (senderPB == V2)
                    {
                        senderPB = vgutiB;
                    }
                    else
                    {
                        senderPB = V2;
                    }
                }
            }
        }
        void vaganyut_V1_ki()
        {
            if (Allomas.Vaganyutak[2].Felepitett == false && Allomas.Vaganyutak[4].Felepitett == false && Allomas.Vaganyutak[5].Felepitett == false && Allomas.Vaganyutak[1].Felepitett == false)
            {
                if ((Allomas.Valtok[1] as ValtoS).Allas == false)
                {
                    Allomas.Vaganyutak[3].Felepitett = true;
                    if ((Allomas.Valtok[1] as ValtoS).AllitasAlatt == true)
                    {
                        (Allomas.Valtok[1] as ValtoS).VgutasAllitas = true; //hogy kivárja a csíkrajzolással az állítás befejezését
                    }
                    ValtoLezaras((ValtoS)(Allomas.Valtok[1]));
                }
                else
                {
                    //vágányutas állítás
                    Allomas.Vaganyutak[3].Felepitett = true;
                    (Allomas.Valtok[1] as ValtoS).VgutasAllitas = true;
                    ValtoAllitas(Allomas.Valtok[1]);
                    ValtoLezaras((ValtoS)(Allomas.Valtok[1]));
                }
                SikeresKezeles();
                Frissit();
            }
            else
            {
                if ((Allomas.Valtok[1] as ValtoS).Allas == true && (Allomas.Valtok[1] as ValtoS).Foglalt == false && (Allomas.Valtok[1] as ValtoS).Lezart == false)
                {
                    ValtoAllitas(Allomas.Valtok[1]);
                    SikeresKezeles();
                    Frissit();
                }
                else
                {
                    if (senderPB == V1)
                    {
                        senderPB = vgutiB;
                    }
                    else
                    {
                        senderPB = V1;
                    }
                }
            }
        }
        void vaganyut_B_K2()
        {
            if (Allomas.Vaganyutak[1].Felepitett == false && Allomas.Vaganyutak[0].Felepitett == false && Allomas.Vaganyutak[5].Felepitett == false && Allomas.Vaganyutak[2].Felepitett == false && Allomas.Vaganyutak[3].Felepitett == false && Allomas.Vaganyutak[7].Felepitett == false)
            {
                if ((Allomas.Valtok[1] as ValtoS).Allas == true)
                {
                    Allomas.Vaganyutak[4].Felepitett = true;
                    if ((Allomas.Valtok[1] as ValtoS).AllitasAlatt == true)
                    {
                        (Allomas.Valtok[1] as ValtoS).VgutasAllitas = true; //hogy kivárja a csíkrajzolással az állítás befejezését
                    }
                    ValtoLezaras((ValtoS)(Allomas.Valtok[1]));
                }
                else
                {
                    //vágányuats állítás
                    Allomas.Vaganyutak[4].Felepitett = true;
                    (Allomas.Valtok[1] as ValtoS).VgutasAllitas = true;
                    ValtoAllitas(Allomas.Valtok[1]);
                    ValtoLezaras((ValtoS)(Allomas.Valtok[1]));
                }
                SikeresKezeles();
                Frissit();
            }
            else
            {
                if ((Allomas.Valtok[1] as ValtoS).Allas == false && (Allomas.Valtok[1] as ValtoS).Foglalt == false && (Allomas.Valtok[1] as ValtoS).Lezart == false)
                {
                    ValtoAllitas(Allomas.Valtok[1]);
                    SikeresKezeles();
                    Frissit();
                }
                else
                {
                    if (senderPB == B)
                    {
                        senderPB = K2;
                    }
                    else
                    {
                        senderPB = B;
                    }
                }
            }
        }
        void vaganyut_B_K1()
        {
            if (Allomas.Vaganyutak[4].Felepitett == false && Allomas.Vaganyutak[0].Felepitett == false && Allomas.Vaganyutak[1].Felepitett == false && Allomas.Vaganyutak[3].Felepitett == false && Allomas.Vaganyutak[2].Felepitett == false && Allomas.Vaganyutak[6].Felepitett == false)
            {
                if ((Allomas.Valtok[1] as ValtoS).Allas == false)
                {
                    Allomas.Vaganyutak[5].Felepitett = true;
                    if ((Allomas.Valtok[1] as ValtoS).AllitasAlatt == true)
                    {
                        (Allomas.Valtok[1] as ValtoS).VgutasAllitas = true; //hogy kivárja a csíkrajzolással az állítás befejezését
                    }
                    ValtoLezaras((ValtoS)(Allomas.Valtok[1]));
                }
                else
                {
                    //vágányutas állítás
                    Allomas.Vaganyutak[5].Felepitett = true;
                    (Allomas.Valtok[1] as ValtoS).VgutasAllitas = true;
                    ValtoAllitas(Allomas.Valtok[1]);
                    ValtoLezaras((ValtoS)(Allomas.Valtok[1]));
                }
                SikeresKezeles();
                Frissit();
            }
            else
            {
                if ((Allomas.Valtok[1] as ValtoS).Allas == true && (Allomas.Valtok[1] as ValtoS).Foglalt == false && (Allomas.Valtok[1] as ValtoS).Lezart == false)
                {
                    ValtoAllitas(Allomas.Valtok[1]);
                    SikeresKezeles();
                    Frissit();
                }
                else
                {
                    if (senderPB == B)
                    {
                        senderPB = K1;
                    }
                    else
                    {
                        senderPB = B;
                    }
                }
            }
        }
        void vaganyut_K2_ki()
        {
            if (Allomas.Vaganyutak[7].Felepitett == false && Allomas.Vaganyutak[1].Felepitett == false && Allomas.Vaganyutak[0].Felepitett == false && Allomas.Vaganyutak[5].Felepitett == false)
            {
                if ((Allomas.Valtok[0] as ValtoS).Allas == true)
                {
                    Allomas.Vaganyutak[6].Felepitett = true;
                    if ((Allomas.Valtok[0] as ValtoS).AllitasAlatt == true)
                    {
                        (Allomas.Valtok[0] as ValtoS).VgutasAllitas = true; //hogy kivárja a csíkrajzolással az állítás befejezését
                    }
                    ValtoLezaras((ValtoS)(Allomas.Valtok[0]));
                }
                else
                {
                    //vágányuats állítás
                    Allomas.Vaganyutak[6].Felepitett = true;
                    (Allomas.Valtok[0] as ValtoS).VgutasAllitas = true;
                    ValtoAllitas(Allomas.Valtok[0]);
                    ValtoLezaras((ValtoS)(Allomas.Valtok[0]));
                }
                SikeresKezeles();
                Frissit();
            }
            else
            {
                if ((Allomas.Valtok[0] as ValtoS).Allas == false && (Allomas.Valtok[0] as ValtoS).Foglalt == false && (Allomas.Valtok[0] as ValtoS).Lezart == false)
                {
                    ValtoAllitas(Allomas.Valtok[0]);
                    SikeresKezeles();
                    Frissit();
                }
                else
                {
                    if (senderPB == K2)
                    {
                        senderPB = vgutiA;
                    }
                    else
                    {
                        senderPB = K2;
                    }
                }
            }
        }
        void vaganyut_K1_ki()
        {
            if (Allomas.Vaganyutak[6].Felepitett == false && Allomas.Vaganyutak[1].Felepitett == false && Allomas.Vaganyutak[0].Felepitett == false && Allomas.Vaganyutak[4].Felepitett == false)
            {
                if ((Allomas.Valtok[0] as ValtoS).Allas == false)
                {
                    Allomas.Vaganyutak[7].Felepitett = true;
                    if ((Allomas.Valtok[0] as ValtoS).AllitasAlatt == true)
                    {
                        (Allomas.Valtok[0] as ValtoS).VgutasAllitas = true; //hogy kivárja a csíkrajzolással az állítás befejezését
                    }
                    ValtoLezaras((ValtoS)(Allomas.Valtok[0]));
                }
                else
                {
                    //vágányuats állítás
                    Allomas.Vaganyutak[7].Felepitett = true;
                    (Allomas.Valtok[0] as ValtoS).VgutasAllitas = true;
                    ValtoAllitas(Allomas.Valtok[0]);
                    ValtoLezaras((ValtoS)(Allomas.Valtok[0]));
                }
                SikeresKezeles();
                Frissit();
            }
            else
            {
                if ((Allomas.Valtok[0] as ValtoS).Allas == true && (Allomas.Valtok[0] as ValtoS).Foglalt == false && (Allomas.Valtok[0] as ValtoS).Lezart == false)
                {
                    ValtoAllitas(Allomas.Valtok[0]);
                    SikeresKezeles();
                    Frissit();
                }
                else
                {
                    if (senderPB == K1)
                    {
                        senderPB = vgutiA;
                    }
                    else
                    {
                        senderPB = K1;
                    }
                }
            }
        }
        #endregion

        void ValtoVezerlesMegfordit(Valto valto)
        {
            if (valto.Allas == true)
            {
                valto.Allas = false;
            }
            else
            {
                valto.Allas = true;
            }
        }
        void ValtoAllitas(Valto valto)
        {
            if (valto.Lezart == false && valto.Foglalt == false)
            {
                //váltóállítás
                if (valto.AllitasAlatt == true)
                {
                    //visszavezérlés
                    (valto as ValtoS).TimerTick = 6 - (valto as ValtoS).TimerTick;
                    ValtoVezerlesMegfordit(valto);
                }
                else
                {
                    //"sima" váltóállítás
                    valto.AllitasAlatt = true;
                    if (valto is ValtoS)
                    {
                        (valto as ValtoS).TimerTick = 0;
                    }
                    ValtoVezerlesMegfordit(valto);
                }
                SikeresKezeles();
            }
            else
            {
                if (senderPB == valtoall && valto == Allomas.Valtok[0])
                {
                    senderPB = egyeni2;
                    UtolsoKezelesFrissit();
                }
                else
                {
                    if (senderPB == valtoall && valto == Allomas.Valtok[1])
                    {
                        senderPB = egyeni1;
                        UtolsoKezelesFrissit();
                    }
                    else
                    {
                        if (senderPB == egyeni1 || senderPB == egyeni2)
                        {
                            senderPB = valtoall;
                            UtolsoKezelesFrissit();
                        }
                    }
                }
            }
        }
        void ValtoLezaras(ValtoS valto)
        {
            valto.Lezart = true;
            if (valto == Allomas.Valtok[1])
            {
                if ((Allomas.Valtok[1] as ValtoS).AllitasAlatt == false)
                {
                    egyeni1.Image = Gyermekvasút.Properties.Resources.d55_1_lezart;
                    egyeni1.Refresh();
                }
            }
            else
            {
                if ((Allomas.Valtok[0] as ValtoS).AllitasAlatt == false)
                {
                    egyeni2.Image = Gyermekvasút.Properties.Resources.d55_2_lezart;
                    egyeni2.Refresh();
                }
            }
        }
        void ValtoFeloldas(ValtoS valto)
        {
            valto.Lezart = false;
            if (valto == Allomas.Valtok[1])
            {
                egyeni1.Image = Gyermekvasút.Properties.Resources.d55_1_alap;
                egyeni1.Refresh();
            }
            else
            {
                egyeni2.Image = Gyermekvasút.Properties.Resources.d55_2_alap;
                egyeni2.Refresh();
            }
        }

        void SikeresKezeles()
        {
            senderPB = null;
            UtolsoKezelesFrissit();
        }
        void UtolsoKezelesFrissit()
        {
            if (senderPB == null)
            {
                utolso.Refresh();
            }
            else
            {
                utolso.Image = senderPB.Image;
            }
        }

        #region Szakaszok (és szárcsíkok) kirajzolására használt függvények
        void szcs1E(Brush brush, Graphics g)
        {
            g.FillRectangle(brush, 692, 266, 25, 9);
        }
        void szcs1K(Pen pen, Graphics g)
        {
            g.DrawLine(pen, 694, 292, 707, 279);
            g.DrawLine(pen, 694, 293, 707, 280);
            g.DrawLine(pen, 695, 293, 708, 280);
            g.DrawLine(pen, 695, 294, 708, 281);
            g.DrawLine(pen, 696, 294, 709, 281);
            g.DrawLine(pen, 696, 295, 709, 282);
            g.DrawLine(pen, 697, 295, 710, 282);
            g.DrawLine(pen, 697, 296, 710, 283);
            g.DrawLine(pen, 698, 296, 711, 283);
            g.DrawLine(pen, 698, 297, 711, 284);
        }
        void szcs2E(Brush brush, Graphics g)
        {
            g.FillRectangle(brush, 243, 266, 25, 9);
        }
        void szcs2K(Pen pen, Graphics g)
        {
            g.DrawLine(pen, 248, 284, 261, 297);
            g.DrawLine(pen, 248, 283, 261, 296);
            g.DrawLine(pen, 249, 283, 262, 296);
            g.DrawLine(pen, 249, 282, 262, 295);
            g.DrawLine(pen, 250, 282, 263, 295);
            g.DrawLine(pen, 250, 281, 263, 294);
            g.DrawLine(pen, 251, 281, 264, 294);
            g.DrawLine(pen, 251, 280, 264, 293);
            g.DrawLine(pen, 252, 280, 265, 293);
            g.DrawLine(pen, 252, 279, 265, 292);
        }
        void a_K2(Brush brush, Graphics g)
        {
            g.FillRectangle(brush, 243, 266, 25, 9);
            g.FillRectangle(brush, 272, 266, 25, 9);
        }
        void a_K1(Pen pen, Graphics g)
        {
            g.DrawLine(pen, 292, 319, 279, 306);
            g.DrawLine(pen, 292, 320, 279, 307);
            g.DrawLine(pen, 291, 320, 278, 307);
            g.DrawLine(pen, 291, 321, 278, 308);
            g.DrawLine(pen, 290, 321, 277, 308);
            g.DrawLine(pen, 290, 322, 277, 309);
            g.DrawLine(pen, 289, 322, 276, 309);
            g.DrawLine(pen, 289, 323, 276, 310);
            g.DrawLine(pen, 288, 323, 275, 310);
            g.DrawLine(pen, 288, 324, 275, 311);
            szcs2K(pen, g);
        }
        void K2_V2(Brush brush, Graphics g)
        {
            for (int i = 0; i < 4; i++)
            {
                g.FillRectangle(brush, (i + 6) * 60 + 3, 266, 25, 9);
                g.FillRectangle(brush, (i + 6) * 60 + 32, 266, 25, 9);
            }
        }
        void K1_V1(Brush brush, Graphics g)
        {
            for (int i = 0; i < 4; i++)
            {
                g.FillRectangle(brush, (i + 6) * 60 + 3, 326, 25, 9);
                g.FillRectangle(brush, (i + 6) * 60 + 32, 326, 25, 9);
            }
        }
        void V2_b(Brush brush, Graphics g)
        {
            g.FillRectangle(brush, 663, 266, 25, 9);
            g.FillRectangle(brush, 692, 266, 25, 9);
        }
        void V1_b(Pen pen, Graphics g)
        {
            g.DrawLine(pen, 667, 319, 680, 306);
            g.DrawLine(pen, 667, 320, 680, 307);
            g.DrawLine(pen, 668, 320, 681, 307);
            g.DrawLine(pen, 668, 321, 681, 308);
            g.DrawLine(pen, 669, 321, 682, 308);
            g.DrawLine(pen, 669, 322, 682, 309);
            g.DrawLine(pen, 670, 322, 683, 309);
            g.DrawLine(pen, 670, 323, 683, 310);
            g.DrawLine(pen, 671, 323, 684, 310);
            g.DrawLine(pen, 671, 324, 684, 311);
            szcs1K(pen, g);
        }
        #endregion

        public override void Frissit()
        {
            for (int i = 0; i < Allomas.Vaganyutak.Count; i++)
            {
                if (Allomas.Vaganyutak[i].Szakaszok[0].Foglalt == false && Allomas.Vaganyutak[i].Szakaszok[1].Foglalt == false && Allomas.Vaganyutak[i].Szakaszok[2].Foglalt && Allomas.Vaganyutak[i].Felepitett && Allomas.Vaganyutak[i].Kezdopont.VonatMeghaladta)
                {
                    if (Allomas.Vaganyutak[i].Kezdopont == Allomas.Jelzok[0] || Allomas.Vaganyutak[i].Kezdopont == Allomas.Jelzok[2] || Allomas.Vaganyutak[i].Kezdopont == Allomas.Jelzok[1])
                    {
                        //páros (2 - A)
                        parosVisszaVillanto.Vgut = Allomas.Vaganyutak[i];
                        if (parosVisszaVillanto.Startolhato)
                        {
                            parosVisszaVillanto.Start();
                            parosVisszaVillanto.Startolhato = false;
                        }
                    }
                    else
                    {
                        //páratlan (1 - O)
                        paratlanVisszaVillanto.Vgut = Allomas.Vaganyutak[i];
                        if (paratlanVisszaVillanto.Startolhato)
                        {
                            paratlanVisszaVillanto.Start();
                            paratlanVisszaVillanto.Startolhato = false;
                        }
                    }
                }
            }

            //these fields are to avoid marshal-by-reference runtime exceptions
            var sKoInt = Ind.s_ko;
            var sKo = Convert.ToString(sKoInt);
            switch (sKo.Length)
            {
                case 1: koSZ.Text = "0000" + sKo;
                    break;
                case 2: koSZ.Text = "000" + sKo;
                    break;
                case 3: koSZ.Text = "00" + sKo;
                    break;
                case 4: koSZ.Text = "0" + sKo;
                    break;
                case 5: koSZ.Text = sKo;
                    break;
                default:
                    break;
            }
            panel2.Refresh();
            pictureBox3.Refresh();
            A.Refresh();
            vgutiA.Refresh();
            B.Refresh();
            vgutiB.Refresh();
            K1.Refresh();
            K2.Refresh();
            V1.Refresh();
            V2.Refresh();
            UtolsoKezelesFrissit();
            if (Ind.s_valtovil == true)
            {
                valtovil.Image = Gyermekvasút.Properties.Resources.d55_valtovil_vilagit;
            }
            else
            {
                valtovil.Image = Gyermekvasút.Properties.Resources.d55_valtovil_alap;
            }

            if ((Allomas.Valtok[1] as ValtoS).Lezart == true && (Allomas.Valtok[1] as ValtoS).AllitasAlatt == false)
            {
                egyeni1.Image = Gyermekvasút.Properties.Resources.d55_1_lezart;
            }
            else
            {
                egyeni1.Image = Gyermekvasút.Properties.Resources.d55_1_alap;
            }

            if ((Allomas.Valtok[0] as ValtoS).Lezart == true && (Allomas.Valtok[0] as ValtoS).AllitasAlatt == false)
            {
                egyeni2.Image = Gyermekvasút.Properties.Resources.d55_2_lezart;
            }
            else
            {
                egyeni2.Image = Gyermekvasút.Properties.Resources.d55_2_alap;
            }

            if (Allomas.Vaganyutak[1].JelzoMegallj || Allomas.Vaganyutak[0].JelzoMegallj || Allomas.Vaganyutak[4].JelzoMegallj || Allomas.Vaganyutak[5].JelzoMegallj || Allomas.Vaganyutak[7].JelzoMegallj || Allomas.Vaganyutak[6].JelzoMegallj || Allomas.Vaganyutak[3].JelzoMegallj || Allomas.Vaganyutak[2].JelzoMegallj)
            {
                Ind.s_zavar = true;
            }
            else
            {
                Ind.s_zavar = false;
            }

            if (Ind.s_zavar)
            {
                //zavar
                zavar.Image = Gyermekvasút.Properties.Resources.d55_zavar_vilagit;
                if (Ind.s_zavar_kezelve == false)
                {
                    if (Ind.s_zavar_lejatszas == false)
                    {
                        player.PlayLooping();
                        Ind.s_zavar_lejatszas = true;
                    }
                }
            }
            else
            {
                zavar.Image = Gyermekvasút.Properties.Resources.d55_zavar_alap;
                if (Ind.s_zavar_lejatszas)
                {
                    player.Stop();
                }
                Ind.s_zavar_lejatszas = false;
                Ind.s_zavar_kezelve = false;
            }

            base.Frissit();
        }

        #region GRAFIKA (gombrajzolás, jelzőrajzolás)
        void GombRajzol(Graphics g, int x, int y, SolidBrush gyuruBrush, Pen gyuruPen, SolidBrush gombBrush, Pen gombPen)
        {
            g.DrawEllipse(feketePen, x * 60 + 18, y * 60 + 18, 24, 24);
            g.DrawEllipse(gyuruPen, x * 60 + 19, y * 60 + 19, 22, 22);
            g.FillEllipse(gyuruBrush, x * 60 + 19, y * 60 + 19, 22, 22);
            g.DrawEllipse(feketePen, x * 60 + 22, y * 60 + 22, 16, 16);
            g.DrawEllipse(gombPen, x * 60 + 23, y * 60 + 23, 14, 14);
            g.FillEllipse(gombBrush, x * 60 + 23, y * 60 + 23, 14, 14);
        }
        void JelzoRajzol(bool jobbraNezE, bool hivoVanE, bool vorosE, bool zoldE, bool hivoE, Graphics g)
        {
            if (jobbraNezE == true)
            {
                g.DrawLine(feketePen, 3, 48, 3, 52);
                g.DrawLine(feketePen, 4, 50, 19, 50);
                if (hivoVanE == true)
                {
                    g.DrawEllipse(feketePen, 9, 46, 8, 8);
                    if (hivoE == true)
                    {
                        g.FillEllipse(feher, 9, 46, 8, 8);
                    }
                    else
                    {
                        g.FillEllipse(szurke, 9, 46, 8, 8);
                    }
                    g.DrawEllipse(feketePen, 9, 46, 8, 8);
                }
                g.FillRectangle(fekete, 23, 45, 24, 11);
                g.DrawLine(feketePen, 22, 46, 22, 54);
                g.DrawLine(feketePen, 21, 47, 21, 53);
                g.DrawLine(feketePen, 20, 48, 20, 52);
                g.DrawLine(feketePen, 47, 46, 47, 54);
                g.DrawLine(feketePen, 48, 47, 48, 53);
                g.DrawLine(feketePen, 49, 48, 49, 52);
                if (zoldE == true)
                {
                    g.FillEllipse(gombZold, 24, 46, 8, 8);
                }
                else
                {
                    g.FillEllipse(szurke, 24, 46, 8, 8);
                }
                if (vorosE == true)
                {
                    g.FillEllipse(voros, 38, 46, 8, 8);
                }
                else
                {
                    g.FillEllipse(szurke, 38, 46, 8, 8);
                }
                g.DrawLine(feketePen, 24, 50, 24, 51);
                g.DrawLine(feketePen, 38, 50, 38, 51);
            }
            else
            {
                //balra néző jelző
                g.DrawLine(feketePen, 56, 11, 56, 7);
                g.DrawLine(feketePen, 55, 9, 40, 9);
                if (hivoVanE == true)
                {
                    if (hivoE == true)
                    {
                        g.FillEllipse(feher, 44, 5, 8, 8);
                    }
                    else
                    {
                        g.FillEllipse(szurke, 44, 5, 8, 8);
                    }
                    g.DrawEllipse(feketePen, 44, 5, 8, 8);
                }
                g.FillRectangle(fekete, 13, 4, 24, 11);
                g.DrawLine(feketePen, 37, 13, 37, 5);
                g.DrawLine(feketePen, 38, 12, 38, 6);
                g.DrawLine(feketePen, 39, 11, 39, 7);
                g.DrawLine(feketePen, 12, 13, 12, 5);
                g.DrawLine(feketePen, 11, 12, 11, 6);
                g.DrawLine(feketePen, 10, 11, 10, 7);
                if (zoldE == true)
                {
                    g.FillEllipse(gombZold, 28, 5, 8, 8);
                }
                else
                {
                    g.FillEllipse(szurke, 28, 5, 8, 8);
                }
                if (vorosE == true)
                {
                    g.FillEllipse(voros, 13, 5, 8, 8);
                }
                else
                {
                    g.FillEllipse(szurke, 13, 5, 8, 8);
                }
                g.DrawLine(feketePen, 13, 9, 13, 10);
                g.DrawLine(feketePen, 28, 9, 28, 10);
            }
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            //Ind.S_open = true;
            if (Hr == null)
            {
                Hr = new Hr(false, true, Allomas);
            }

            if (Hr.Frissitheto)
            {
                Hr.vszkRefresh();
            }
            
            Frissit();
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            #region Kockák állandó része (háttér, fekete)
            //KOCKÁK KERETE, HÁTTERE
            e.Graphics.FillRectangle(zold, 0, 0, (sender as PictureBox).Width, (sender as PictureBox).Height);

            for (int i = 0; i < (sender as PictureBox).Width / 60; i++) // szélesség
            {
                e.Graphics.DrawLine(keret, i * 60, 0, i * 60, (sender as PictureBox).Height);
                e.Graphics.DrawLine(keret, (i + 1) * 60 - 1, 0, (i + 1) * 60 - 1, (sender as PictureBox).Height);
            }
            for (int j = 0; j < (sender as PictureBox).Height / 60; j++) // magasság
            {
                e.Graphics.DrawLine(keret, 0, j * 60, (sender as PictureBox).Width, j * 60);
                e.Graphics.DrawLine(keret, 0, (j + 1) * 60 - 1, (sender as PictureBox).Width, (j + 1) * 60 - 1);
            }
            // VÁGÁNYKOCKÁK
            for (int i = 0; i < 16; i++)
            {
                e.Graphics.FillRectangle(fekete, i * 60 + 1, 264, 58, 13);
            }
            for (int i = 5; i < 11; i++)
            {
                e.Graphics.FillRectangle(fekete, i * 60 + 1, 324, 58, 13);
            }
            //FERDE (váltó-vágány)
            for (int i = 0; i < 14; i++)
            {
                e.Graphics.DrawLine(feketePen, 661, 323 + i, 683 + i, 301);
            }
            for (int i = 0; i < 14; i++)
            {
                e.Graphics.DrawLine(feketePen, 263 + i, 301, 298, 336 - i);
            }
            //VÁLTÓ (K rész)
            for (int i = 0; i < 12; i++)
            {
                e.Graphics.DrawLine(feketePen, 686 + i, 298, 707 + i, 277);
            }
            e.Graphics.DrawLine(feketePen, 698, 298, 718, 278);
            e.Graphics.DrawLine(feketePen, 699, 298, 718, 279);
            for (int i = 0; i < 12; i++)
            {
                e.Graphics.DrawLine(feketePen, 241 + i, 277, 262 + i, 298);
            }
            e.Graphics.DrawLine(feketePen, 241, 279, 260, 298);
            e.Graphics.DrawLine(feketePen, 241, 278, 261, 298);
            #endregion

            #region Szakaszok (foglaltság, első lezárás) >> vörös||fehér||szürke)

            if (Allomas.Szakaszok[2].Foglalt)
            {
                a_K2(voros, e.Graphics);
            }
            else
            {
                if ((Allomas.Vaganyutak[6].Felepitett || Allomas.Vaganyutak[1].Felepitett) && (Allomas.Valtok[0] as ValtoS).VgutasAllitas == false)
                {
                    a_K2(feher, e.Graphics);
                }
                else
                {
                    a_K2(szurke, e.Graphics);
                }
            }

            if (Allomas.Szakaszok[7].Foglalt)
            {
                a_K1(vorosPen, e.Graphics);
            }
            else
            {
                if ((Allomas.Vaganyutak[7].Felepitett || Allomas.Vaganyutak[0].Felepitett) && (Allomas.Valtok[0] as ValtoS).VgutasAllitas == false)
                {
                    a_K1(feherPen, e.Graphics);
                }
                else
                {
                    a_K1(szurkePen, e.Graphics);
                }
            }

            if (Allomas.Szakaszok[3].Foglalt)
            {
                K2_V2(voros, e.Graphics);
            }
            else
            {
                if (((Allomas.Vaganyutak[1].Felepitett || Allomas.Vaganyutak[6].Felepitett) && (Allomas.Valtok[0] as ValtoS).VgutasAllitas == false) || ((Allomas.Vaganyutak[4].Felepitett || Allomas.Vaganyutak[2].Felepitett) && (Allomas.Valtok[1] as ValtoS).VgutasAllitas == false))
                {
                    K2_V2(feher, e.Graphics);
                }
                else
                {
                    K2_V2(szurke, e.Graphics);
                }
            }

            if (Allomas.Szakaszok[8].Foglalt)
            {
                K1_V1(voros, e.Graphics);
            }
            else
            {
                if (((Allomas.Vaganyutak[0].Felepitett || Allomas.Vaganyutak[7].Felepitett) && (Allomas.Valtok[0] as ValtoS).VgutasAllitas == false) || ((Allomas.Vaganyutak[5].Felepitett || Allomas.Vaganyutak[3].Felepitett) && (Allomas.Valtok[1] as ValtoS).VgutasAllitas == false))
                {
                    K1_V1(feher, e.Graphics);
                }
                else
                {
                    K1_V1(szurke, e.Graphics);
                }
            }

            if (Allomas.Szakaszok[4].Foglalt)
            {
                V2_b(voros, e.Graphics);
            }
            else
            {
                if ((Allomas.Vaganyutak[2].Felepitett || Allomas.Vaganyutak[4].Felepitett) && (Allomas.Valtok[1] as ValtoS).VgutasAllitas == false)
                {
                    V2_b(feher, e.Graphics);
                }
                else
                {
                    V2_b(szurke, e.Graphics);
                }
            }

            if (Allomas.Szakaszok[9].Foglalt)
            {
                V1_b(vorosPen, e.Graphics);
            }
            else
            {
                if ((Allomas.Vaganyutak[3].Felepitett || Allomas.Vaganyutak[5].Felepitett) && (Allomas.Valtok[1] as ValtoS).VgutasAllitas == false)
                {
                    V1_b(feherPen, e.Graphics);
                }
                else
                {
                    V1_b(szurkePen, e.Graphics);
                }
            }

            #endregion

            #region Váltó szárcsíkok
            if ((Allomas.Valtok[0] as ValtoS).Foglalt == false && (Allomas.Valtok[0] as ValtoS).SzcsE == true)
            {
                //2E szcs. (állítás)
                szcs2E(feher, e.Graphics);
            }
            if ((Allomas.Valtok[0] as ValtoS).Foglalt == false && (Allomas.Valtok[0] as ValtoS).SzcsK == true)
            {
                //2K szcs. (állítás)
                szcs2K(feherPen, e.Graphics);
            }

            if ((Allomas.Valtok[1] as ValtoS).Foglalt == false && (Allomas.Valtok[1] as ValtoS).SzcsE == true)
            {
                //1E szcs. (állítás)
                szcs1E(feher, e.Graphics);
            }
            if ((Allomas.Valtok[1] as ValtoS).Foglalt == false && (Allomas.Valtok[1] as ValtoS).SzcsK == true)
            {
                //1K szcs. (állítás)
                szcs1K(feherPen, e.Graphics);
            }
            #endregion

            #region Talppontok
            //K2
            if (Allomas.Vaganyutak[6].Felepitett && Allomas.Vaganyutak[6].KezdoJelzoSzabad == false && Allomas.Vaganyutak[6].JelzoMegallj == false && Allomas.Jelzok[2].VonatMeghaladta == false)
            {
                e.Graphics.FillEllipse(feher, 363, 245, 8, 8);
            }
            else
            {
                e.Graphics.FillEllipse(szurke, 363, 245, 8, 8);
            }
            e.Graphics.DrawEllipse(feketePen, 363, 245, 8, 8);

            //K1           
            if (Allomas.Vaganyutak[7].Felepitett && Allomas.Vaganyutak[7].KezdoJelzoSzabad == false && Allomas.Vaganyutak[7].JelzoMegallj == false && Allomas.Jelzok[1].VonatMeghaladta == false)
            {
                e.Graphics.FillEllipse(feher, 363, 305, 8, 8);
            }
            else
            {
                e.Graphics.FillEllipse(szurke, 363, 305, 8, 8);
            }
            e.Graphics.DrawEllipse(feketePen, 363, 305, 8, 8);

            //B
            if (((Allomas.Vaganyutak[5].Felepitett && Allomas.Vaganyutak[5].KezdoJelzoSzabad == false && Allomas.Vaganyutak[5].JelzoMegallj == false) || (Allomas.Vaganyutak[4].Felepitett && Allomas.Vaganyutak[4].KezdoJelzoSzabad == false && Allomas.Vaganyutak[4].JelzoMegallj == false)) && Allomas.Jelzok[5].VonatMeghaladta == false)
            {
                e.Graphics.FillEllipse(feher, 843, 245, 8, 8);
            }
            else
            {
                e.Graphics.FillEllipse(szurke, 843, 245, 8, 8);
            }
            e.Graphics.DrawEllipse(feketePen, 843, 245, 8, 8);

            //V1
            if (Allomas.Vaganyutak[3].Felepitett && Allomas.Vaganyutak[3].KezdoJelzoSzabad == false && Allomas.Vaganyutak[3].JelzoMegallj == false && Allomas.Jelzok[3].VonatMeghaladta == false)
            {
                e.Graphics.FillEllipse(feher, 588, 346, 8, 8);
            }
            else
            {
                e.Graphics.FillEllipse(szurke, 588, 346, 8, 8);
            }
            e.Graphics.DrawEllipse(feketePen, 588, 346, 8, 8);

            //V2
            if (Allomas.Vaganyutak[2].Felepitett && Allomas.Vaganyutak[2].KezdoJelzoSzabad == false && Allomas.Vaganyutak[2].JelzoMegallj == false && Allomas.Jelzok[4].VonatMeghaladta == false)
            {
                e.Graphics.FillEllipse(feher, 588, 286, 8, 8);
            }
            else
            {
                e.Graphics.FillEllipse(szurke, 588, 286, 8, 8);
            }
            e.Graphics.DrawEllipse(feketePen, 588, 286, 8, 8);

            //A
            e.Graphics.DrawLine(feketePen, 113, 288, 115, 288);
            e.Graphics.DrawLine(feketePen, 113, 292, 115, 292);
            e.Graphics.DrawLine(feketePen, 112, 289, 112, 291);
            e.Graphics.DrawLine(feketePen, 116, 289, 116, 291);
            if (((Allomas.Vaganyutak[1].Felepitett && Allomas.Vaganyutak[1].KezdoJelzoSzabad == false && Allomas.Vaganyutak[1].JelzoMegallj == false) || (Allomas.Vaganyutak[0].Felepitett && Allomas.Vaganyutak[0].KezdoJelzoSzabad == false && Allomas.Vaganyutak[0].JelzoMegallj == false)) && Allomas.Jelzok[0].VonatMeghaladta == false)
            {
                e.Graphics.FillEllipse(feher, 108, 286, 8, 8);
            }
            else
            {
                e.Graphics.FillEllipse(szurke, 108, 286, 8, 8);
            }
            e.Graphics.DrawEllipse(feketePen, 108, 286, 8, 8);
            #endregion

            #region     Előjelzők visszajelentése
            //AEJ
            e.Graphics.FillEllipse(szurke, 88, 286, 8, 8);
            e.Graphics.DrawEllipse(feketePen, 88, 286, 8, 8);
            //BEJ
            e.Graphics.FillEllipse(szurke, 863, 245, 8, 8);
            e.Graphics.DrawEllipse(feketePen, 863, 245, 8, 8);
            #endregion

            //UTOLSÓ KEZELÉS
            e.Graphics.DrawRectangle(vorosPen, 899, 0, 60, 60);
        }

        private void utolso_Paint(object sender, PaintEventArgs e)
        {
            if (senderPB == null)
            {
                e.Graphics.FillRectangle(zold, 0, 0, (sender as PictureBox).Width, (sender as PictureBox).Height);

                for (int i = 0; i < (sender as PictureBox).Width / 60; i++) // szélesség
                {
                    e.Graphics.DrawLine(keret, i * 60, 0, i * 60, (sender as PictureBox).Height);
                    e.Graphics.DrawLine(keret, (i + 1) * 60 - 1, 0, (i + 1) * 60 - 1, (sender as PictureBox).Height);
                }
                for (int j = 0; j < (sender as PictureBox).Height / 60; j++) // magasság
                {
                    e.Graphics.DrawLine(keret, 0, j * 60, (sender as PictureBox).Width, j * 60);
                    e.Graphics.DrawLine(keret, 0, (j + 1) * 60 - 1, (sender as PictureBox).Width, (j + 1) * 60 - 1);
                }
            }
            else
            {
                if (senderPB == A)
                {
                    A_Paint(sender, e);
                }
                else
                {
                    if (senderPB == vgutiA)
                    {
                        vgutiA_Paint(sender, e);
                    }
                    else
                    {
                        if (senderPB == K2)
                        {
                            K2_Paint(sender, e);
                        }
                        else
                        {
                            if (senderPB == K1)
                            {
                                K1_Paint(sender, e);
                            }
                            else
                            {
                                if (senderPB == V2)
                                {
                                    V2_Paint(sender, e);
                                }
                                else
                                {
                                    if (senderPB == V1)
                                    {
                                        V1_Paint(sender, e);
                                    }
                                    else
                                    {
                                        if (senderPB == vgutiB)
                                        {
                                            vgutiB_Paint(sender, e);
                                        }
                                        else
                                        {
                                            if (senderPB == B)
                                            {
                                                B_Paint(sender, e);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void valtovil_Click(object sender, EventArgs e)
        {
            if (senderPB == valtovil)
            {
                senderPB = null;
                UtolsoKezelesFrissit();
            }
            else
            {
                if (senderPB == valtoall)
                {
                    if (Ind.s_valtovil == false)
                    {
                        //alap >> világít
                        Ind.s_valtovil = true;
                        valtovil.Image = Gyermekvasút.Properties.Resources.d55_valtovil_vilagit;
                    }
                    else
                    {
                        //világít >> alap
                        Ind.s_valtovil = false;
                        valtovil.Image = Gyermekvasút.Properties.Resources.d55_valtovil_alap;
                    }
                    SikeresKezeles();
                }
                else
                {
                    senderPB = valtovil;
                    UtolsoKezelesFrissit();
                }
            }
        }

        private void jelzoall_Click(object sender, EventArgs e)
        {
            if (senderPB == jelzoall)
            {
                senderPB = null;
                UtolsoKezelesFrissit();
            }
            else
            {
                if (senderPB == A && ((Allomas.Vaganyutak[0].Felepitett && Allomas.Vaganyutak[0].JelzoMegallj == false && Allomas.Vaganyutak[0].BejarandóSzakaszokSzabadok) || (Allomas.Vaganyutak[1].Felepitett && Allomas.Vaganyutak[1].JelzoMegallj == false && Allomas.Vaganyutak[1].BejarandóSzakaszokSzabadok)) && (Allomas.Valtok[0] as ValtoS).AllitasAlatt == false)
                {
                    Allomas.Jelzok[0].Szabad = true;
                    SikeresKezeles();
                    Frissit();
                }
                else
                {
                    if (senderPB == K2 && Allomas.Vaganyutak[6].Felepitett && Allomas.Vaganyutak[6].JelzoMegallj == false && Allomas.Vaganyutak[6].BejarandóSzakaszokSzabadok && (Allomas.Valtok[0] as ValtoS).AllitasAlatt == false)
                    {
                        Allomas.Jelzok[2].Szabad = true;
                        SikeresKezeles();
                        Frissit();
                    }
                    else
                    {
                        if (senderPB == K1 && Allomas.Vaganyutak[7].Felepitett && Allomas.Vaganyutak[7].JelzoMegallj == false && Allomas.Vaganyutak[7].BejarandóSzakaszokSzabadok && (Allomas.Valtok[0] as ValtoS).AllitasAlatt == false)
                        {
                            Allomas.Jelzok[1].Szabad = true;
                            SikeresKezeles();
                            Frissit();
                        }
                        else
                        {
                            if (senderPB == V2 && Allomas.Vaganyutak[2].Felepitett && Allomas.Vaganyutak[2].JelzoMegallj == false && Allomas.Vaganyutak[2].BejarandóSzakaszokSzabadok && (Allomas.Valtok[1] as ValtoS).AllitasAlatt == false)
                            {
                                Allomas.Jelzok[4].Szabad = true;
                                SikeresKezeles();
                                Frissit();
                            }
                            else
                            {
                                if (senderPB == V1 && Allomas.Vaganyutak[3].Felepitett && Allomas.Vaganyutak[3].JelzoMegallj == false && Allomas.Vaganyutak[3].BejarandóSzakaszokSzabadok && (Allomas.Valtok[1] as ValtoS).AllitasAlatt == false)
                                {
                                    Allomas.Jelzok[3].Szabad = true;
                                    SikeresKezeles();
                                    Frissit();
                                }
                                else
                                {
                                    if (senderPB == B && ((Allomas.Vaganyutak[5].Felepitett && Allomas.Vaganyutak[5].JelzoMegallj == false && Allomas.Vaganyutak[5].BejarandóSzakaszokSzabadok) || (Allomas.Vaganyutak[4].Felepitett && Allomas.Vaganyutak[4].JelzoMegallj == false && Allomas.Vaganyutak[4].BejarandóSzakaszokSzabadok)) && (Allomas.Valtok[1] as ValtoS).AllitasAlatt == false)
                                    {
                                        Allomas.Jelzok[5].Szabad = true;
                                        SikeresKezeles();
                                        Frissit();
                                    }
                                    else
                                    {
                                        senderPB = jelzoall;
                                        UtolsoKezelesFrissit();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void valtoall_Click(object sender, EventArgs e)
        {
            if (senderPB == valtoall)
            {
                senderPB = null;
                UtolsoKezelesFrissit();
            }
            else
            {
                if (senderPB == valtovil)
                {
                    if (Ind.s_valtovil == false)
                    {
                        //alap >> világít
                        Ind.s_valtovil = true;
                        valtovil.Image = Gyermekvasút.Properties.Resources.d55_valtovil_vilagit;
                    }
                    else
                    {
                        //világít >> alap
                        Ind.s_valtovil = false;
                        valtovil.Image = Gyermekvasút.Properties.Resources.d55_valtovil_alap;
                    }
                    SikeresKezeles();
                }
                else
                {
                    if (senderPB == egyeni1)
                    {
                        //váltóállítás 1
                        ValtoAllitas(Allomas.Valtok[1]);
                    }
                    else if (senderPB == egyeni2)
                    {
                        //váltóállítás 2
                        ValtoAllitas(Allomas.Valtok[0]);
                    }
                    else
                    {
                        senderPB = valtoall;
                        UtolsoKezelesFrissit();
                    }
                }
            }
        }

        private void torles_Click(object sender, EventArgs e)
        {
            if (senderPB == torles)
            {
                senderPB = null;
                UtolsoKezelesFrissit();
            }
            else
            {
                if (senderPB == A && ((Allomas.Vaganyutak[0].Felepitett && Allomas.Vaganyutak[0].KezdoJelzoSzabad == false && !Allomas.Vaganyutak[0].JelzoMegallj) || (Allomas.Vaganyutak[1].Felepitett && Allomas.Vaganyutak[1].KezdoJelzoSzabad == false && !Allomas.Vaganyutak[1].JelzoMegallj)))
                {
                    Allomas.Vaganyutak[1].Felepitett = false;
                    Allomas.Vaganyutak[0].Felepitett = false;
                    ValtoFeloldas((ValtoS)(Allomas.Valtok[0]));
                    SikeresKezeles();
                    Frissit();
                }
                else
                {
                    if (senderPB == K2 && Allomas.Vaganyutak[6].Felepitett && Allomas.Vaganyutak[6].KezdoJelzoSzabad == false && !Allomas.Vaganyutak[6].JelzoMegallj)
                    {
                        Allomas.Vaganyutak[6].Felepitett = false;
                        ValtoFeloldas((ValtoS)(Allomas.Valtok[0]));
                        SikeresKezeles();
                        Frissit();
                    }
                    else
                    {
                        if (senderPB == K1 && Allomas.Vaganyutak[7].Felepitett && Allomas.Vaganyutak[7].KezdoJelzoSzabad == false && !Allomas.Vaganyutak[7].JelzoMegallj)
                        {
                            Allomas.Vaganyutak[7].Felepitett = false;
                            ValtoFeloldas((ValtoS)(Allomas.Valtok[0]));
                            SikeresKezeles();
                            Frissit();
                        }
                        else
                        {
                            if (senderPB == V2 && Allomas.Vaganyutak[2].Felepitett && Allomas.Vaganyutak[2].KezdoJelzoSzabad == false && !Allomas.Vaganyutak[2].JelzoMegallj)
                            {
                                Allomas.Vaganyutak[2].Felepitett = false;
                                ValtoFeloldas((ValtoS)(Allomas.Valtok[1]));
                                SikeresKezeles();
                                Frissit();
                            }
                            else
                            {
                                if (senderPB == V1 && Allomas.Vaganyutak[3].Felepitett && Allomas.Vaganyutak[3].KezdoJelzoSzabad == false && !Allomas.Vaganyutak[3].JelzoMegallj)
                                {
                                    Allomas.Vaganyutak[3].Felepitett = false;
                                    ValtoFeloldas((ValtoS)(Allomas.Valtok[1]));
                                    SikeresKezeles();
                                    Frissit();
                                }
                                else
                                {
                                    if (senderPB == B && ((Allomas.Vaganyutak[5].Felepitett && Allomas.Vaganyutak[5].KezdoJelzoSzabad == false && !Allomas.Vaganyutak[5].JelzoMegallj) || (Allomas.Vaganyutak[4].Felepitett && Allomas.Vaganyutak[4].KezdoJelzoSzabad == false && !Allomas.Vaganyutak[4].JelzoMegallj)))
                                    {
                                        Allomas.Vaganyutak[4].Felepitett = false;
                                        Allomas.Vaganyutak[5].Felepitett = false;
                                        ValtoFeloldas((ValtoS)(Allomas.Valtok[1]));
                                        SikeresKezeles();
                                        Frissit();
                                    }
                                    else
                                    {
                                        senderPB = torles;
                                        UtolsoKezelesFrissit();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void egyeni2_Click(object sender, EventArgs e)
        {
            if (senderPB == egyeni2)
            {
                senderPB = null;
                UtolsoKezelesFrissit();
            }
            else
            {
                if (senderPB == valtoall)
                {
                    //váltóállítás 2
                    ValtoAllitas(Allomas.Valtok[0]);
                }
                else
                {
                    senderPB = egyeni2;
                    UtolsoKezelesFrissit();
                }
            }
        }

        private void egyeni1_Click(object sender, EventArgs e)
        {
            if (senderPB == egyeni1)
            {
                senderPB = null;
                UtolsoKezelesFrissit();
            }
            else
            {
                if (senderPB == valtoall)
                {
                    //váltóállítás 1
                    ValtoAllitas(Allomas.Valtok[1]);
                }
                else
                {
                    senderPB = egyeni1;
                    UtolsoKezelesFrissit();
                }
            }
        }

        private void blinker_Tick(object sender, EventArgs e)
        {
            if (villogtatas == true)
            {
                villogtatas = false;
            }
            else
            {
                villogtatas = true;
            }

            if ((Allomas.Valtok[1] as ValtoS).AllitasAlatt == true)
            {
                if ((Allomas.Valtok[1] as ValtoS).TimerTick <= 5)
                {
                    if (villogtatas == true)
                    {
                        if ((Allomas.Valtok[1] as ValtoS).Allas)
                        {
                            //E szcs.
                            (Allomas.Valtok[1] as ValtoS).SzcsE = true;
                            (Allomas.Valtok[1] as ValtoS).SzcsK = false;
                        }
                        else
                        {
                            //K szcs.
                            (Allomas.Valtok[1] as ValtoS).SzcsK = true;
                            (Allomas.Valtok[1] as ValtoS).SzcsE = false;
                        }
                    }
                    else
                    {
                        //egyik szcs. sem
                        (Allomas.Valtok[1] as ValtoS).SzcsK = false;
                        (Allomas.Valtok[1] as ValtoS).SzcsE = false;
                    }
                    (Allomas.Valtok[1] as ValtoS).TimerTick++;
                }
                if ((Allomas.Valtok[1] as ValtoS).TimerTick == 6)
                {
                    (Allomas.Valtok[1] as ValtoS).TimerTick = 0;
                    (Allomas.Valtok[1] as ValtoS).AllitasAlatt = false;
                    (Allomas.Valtok[1] as ValtoS).VgutasAllitas = false;
                    (Allomas.Valtok[1] as ValtoS).SzcsK = false;
                    (Allomas.Valtok[1] as ValtoS).SzcsE = false;
                    //egyik szcs. sem
                }
            }
            if ((Allomas.Valtok[0] as ValtoS).AllitasAlatt == true)
            {
                if ((Allomas.Valtok[0] as ValtoS).TimerTick <= 5)
                {
                    if (villogtatas == true)
                    {
                        if ((Allomas.Valtok[0] as ValtoS).Allas)
                        {
                            //E szcs.
                            (Allomas.Valtok[0] as ValtoS).SzcsE = true;
                            (Allomas.Valtok[0] as ValtoS).SzcsK = false;
                        }
                        else
                        {
                            //K szcs.
                            (Allomas.Valtok[0] as ValtoS).SzcsK = true;
                            (Allomas.Valtok[0] as ValtoS).SzcsE = false;
                        }
                    }
                    else
                    {
                        //egyik szcs. sem
                        (Allomas.Valtok[0] as ValtoS).SzcsK = false;
                        (Allomas.Valtok[0] as ValtoS).SzcsE = false;
                    }
                    (Allomas.Valtok[0] as ValtoS).TimerTick++;
                }
                if ((Allomas.Valtok[0] as ValtoS).TimerTick == 6)
                {
                    (Allomas.Valtok[0] as ValtoS).TimerTick = 0;
                    (Allomas.Valtok[0] as ValtoS).AllitasAlatt = false;
                    (Allomas.Valtok[0] as ValtoS).VgutasAllitas = false;
                    (Allomas.Valtok[0] as ValtoS).SzcsK = false;
                    (Allomas.Valtok[0] as ValtoS).SzcsE = false;
                    //egyik szcs. sem
                }
            }
            Frissit();
        }

        private void A_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(keret, 0, 0, 59, 59);
            e.Graphics.FillRectangle(zold, 1, 1, 58, 58);
            e.Graphics.FillRectangle(fekete, 1, 24, 58, 13);

            if (Allomas.Szakaszok[1].Foglalt)
            {
                //vörös
                e.Graphics.FillRectangle(voros, 3, 26, 25, 9);
                e.Graphics.FillRectangle(voros, 32, 26, 25, 9);
            }
            else if ((Allomas.Vaganyutak[0].Felepitett || Allomas.Vaganyutak[1].Felepitett || Allomas.Vaganyutak[6].Felepitett || Allomas.Vaganyutak[7].Felepitett) && (Allomas.Valtok[0] as ValtoS).VgutasAllitas == false)
            {
                //vágányút >> fehér
                e.Graphics.FillRectangle(feher, 3, 26, 25, 9);
                e.Graphics.FillRectangle(feher, 32, 26, 25, 9);
            }
            else
            {
                //szürke
                e.Graphics.FillRectangle(szurke, 3, 26, 25, 9);
                e.Graphics.FillRectangle(szurke, 32, 26, 25, 9);
            }

            GombRajzol(e.Graphics, 0, 0, gyuruZold, gyuruZoldPen, voros, vorosPen);
            if (Allomas.Jelzok[0].Szabad)
            {
                JelzoRajzol(true, true, false, true, false, e.Graphics);
            }
            else
            {
                if (Allomas.Vaganyutak[0].JelzoMegallj || Allomas.Vaganyutak[1].JelzoMegallj)
                {
                    if (villogtatas == true)
                    {
                        JelzoRajzol(true, true, true, true, false, e.Graphics);
                    }
                    else
                    {
                        JelzoRajzol(true, true, true, false, false, e.Graphics);
                    }
                }
                else
                {
                    JelzoRajzol(true, true, true, false, false, e.Graphics);
                }
            }
            //A betű
            e.Graphics.DrawLine(feketePen, 29, 5, 26, 13);
            e.Graphics.DrawLine(feketePen, 29, 5, 32, 13);
            e.Graphics.DrawLine(feketePen, 28, 9, 30, 9);
        }

        private void A_Click(object sender, EventArgs e)
        {
            if (senderPB == A)
            {
                senderPB = null;
                UtolsoKezelesFrissit();
            }
            else
            {
                if (senderPB == V2)
                {
                    vaganyut_A_V2();
                }
                else
                {
                    if (senderPB == V1)
                    {
                        vaganyut_A_V1();
                    }
                    else
                    {
                        if (senderPB == torles && ((Allomas.Vaganyutak[1].Felepitett && Allomas.Vaganyutak[1].KezdoJelzoSzabad == false && !Allomas.Vaganyutak[1].JelzoMegallj) || (Allomas.Vaganyutak[0].Felepitett && Allomas.Vaganyutak[0].KezdoJelzoSzabad == false && !Allomas.Vaganyutak[0].JelzoMegallj)))
                        {
                            Allomas.Vaganyutak[1].Felepitett = false;
                            Allomas.Vaganyutak[0].Felepitett = false;
                            SikeresKezeles();
                            Frissit();
                            ValtoFeloldas((ValtoS)(Allomas.Valtok[0]));
                        }
                        else
                        {
                            if (senderPB == jelzoall && ((Allomas.Vaganyutak[0].Felepitett && Allomas.Vaganyutak[0].JelzoMegallj == false && Allomas.Vaganyutak[0].BejarandóSzakaszokSzabadok) || (Allomas.Vaganyutak[1].Felepitett && Allomas.Vaganyutak[1].JelzoMegallj == false && Allomas.Vaganyutak[1].BejarandóSzakaszokSzabadok)) && (Allomas.Valtok[0] as ValtoS).AllitasAlatt == false)
                            {
                                Allomas.Jelzok[0].Szabad = true;
                                SikeresKezeles();
                                Frissit();
                            }
                            else
                            {
                                if (senderPB == megallj && ((Allomas.Vaganyutak[0].Felepitett && Allomas.Vaganyutak[0].KezdoJelzoSzabad) || (Allomas.Vaganyutak[1].Felepitett && Allomas.Vaganyutak[1].KezdoJelzoSzabad)))
                                {
                                    Allomas.Jelzok[0].Szabad = false;
                                    if (Allomas.Vaganyutak[0].Felepitett)
                                    {
                                        Allomas.Vaganyutak[0].JelzoMegallj = true;
                                    }
                                    else
                                    {
                                        Allomas.Vaganyutak[1].JelzoMegallj = true;
                                    }
                                    Ind.s_zavar = true;
                                    SikeresKezeles();
                                    Frissit();
                                }
                                else
                                {
                                    if (senderPB == K2 && (Allomas.Valtok[0] as ValtoS).Allas == false)
                                    {
                                        ValtoAllitas(Allomas.Valtok[0]);
                                        SikeresKezeles();
                                    }
                                    else
                                    {
                                        if (senderPB == K1 && (Allomas.Valtok[0] as ValtoS).Allas == true)
                                        {
                                            ValtoAllitas(Allomas.Valtok[0]);
                                            SikeresKezeles();
                                            Frissit();
                                        }
                                        else
                                        {
                                            senderPB = A;
                                            UtolsoKezelesFrissit();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void vgutiA_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(keret, 0, 0, 59, 59);
            e.Graphics.FillRectangle(zold, 1, 1, 58, 58);
            e.Graphics.FillRectangle(fekete, 1, 24, 58, 13);

            if (Allomas.Szakaszok[1].Foglalt)
            {
                //vörös
                e.Graphics.FillRectangle(voros, 3, 26, 25, 9);
            }
            else if ((Allomas.Vaganyutak[0].Felepitett || Allomas.Vaganyutak[1].Felepitett || Allomas.Vaganyutak[6].Felepitett || Allomas.Vaganyutak[7].Felepitett) && (Allomas.Valtok[0] as ValtoS).VgutasAllitas == false)
            {
                //vágányút >> fehér
                e.Graphics.FillRectangle(feher, 3, 26, 25, 9);
            }
            else
            {
                //szürke
                e.Graphics.FillRectangle(szurke, 3, 26, 25, 9);
            }

            if (Allomas.Szakaszok[7].Foglalt || Allomas.Szakaszok[2].Foglalt)
            {
                e.Graphics.FillRectangle(voros, 32, 26, 25, 9);
            }
            else if (((Allomas.Vaganyutak[6].Felepitett || Allomas.Vaganyutak[1].Felepitett) || (Allomas.Vaganyutak[0].Felepitett || Allomas.Vaganyutak[7].Felepitett)) && (Allomas.Valtok[0] as ValtoS).VgutasAllitas == false)
            {
                e.Graphics.FillRectangle(feher, 32, 26, 25, 9);
            }
            else
            {
                e.Graphics.FillRectangle(szurke, 32, 26, 25, 9);
            }
            GombRajzol(e.Graphics, 0, 0, gyuruZold, gyuruZoldPen, gombZold, gombZoldPen);
            //a betű
            e.Graphics.DrawLine(feketePen, 27, 8, 27, 8);
            e.Graphics.DrawLine(feketePen, 28, 7, 30, 7);
            e.Graphics.DrawLine(feketePen, 31, 8, 31, 13);
            e.Graphics.DrawLine(feketePen, 28, 10, 30, 10);
            e.Graphics.DrawLine(feketePen, 27, 11, 27, 12);
            e.Graphics.DrawLine(feketePen, 30, 12, 31, 12);
            e.Graphics.DrawLine(feketePen, 28, 13, 29, 13);
        }

        private void vgutiA_Click(object sender, EventArgs e)
        {
            if (senderPB == vgutiA)
            {
                senderPB = null;
                UtolsoKezelesFrissit();
            }
            else
            {
                if (senderPB == K2)
                {
                    vaganyut_K2_ki();
                }
                else
                {
                    if (senderPB == K1)
                    {
                        vaganyut_K1_ki();
                    }
                    else
                    {
                        senderPB = vgutiA;
                        UtolsoKezelesFrissit();
                    }
                }
            }
        }

        private void K2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(keret, 0, 0, 59, 59);
            e.Graphics.FillRectangle(zold, 1, 1, 58, 58);
            e.Graphics.FillRectangle(fekete, 1, 24, 58, 13);

            if (Allomas.Szakaszok[2].Foglalt)
            {
                //vörös
                e.Graphics.FillRectangle(voros, 3, 26, 25, 9);
            }
            else if ((Allomas.Vaganyutak[1].Felepitett || Allomas.Vaganyutak[6].Felepitett) && (Allomas.Valtok[0] as ValtoS).VgutasAllitas == false)
            {
                //vágányút >> fehér
                e.Graphics.FillRectangle(feher, 3, 26, 25, 9);
            }
            else
            {
                //szürke
                e.Graphics.FillRectangle(szurke, 3, 26, 25, 9);
            }

            if (Allomas.Szakaszok[3].Foglalt)
            {
                e.Graphics.FillRectangle(voros, 32, 26, 25, 9);
            }
            else if (((Allomas.Vaganyutak[1].Felepitett || Allomas.Vaganyutak[6].Felepitett) && (Allomas.Valtok[0] as ValtoS).VgutasAllitas == false) || ((Allomas.Vaganyutak[4].Felepitett || Allomas.Vaganyutak[2].Felepitett) && (Allomas.Valtok[1] as ValtoS).VgutasAllitas == false))
            {
                e.Graphics.FillRectangle(feher, 32, 26, 25, 9);
            }
            else
            {
                e.Graphics.FillRectangle(szurke, 32, 26, 25, 9);
            }

            GombRajzol(e.Graphics, 0, 0, gyuruZold, gyuruZoldPen, voros, vorosPen);
            if (Allomas.Jelzok[2].Szabad)
            {
                JelzoRajzol(false, false, false, true, false, e.Graphics);
            }
            else
            {
                if (Allomas.Vaganyutak[6].JelzoMegallj)
                {
                    if (villogtatas == true)
                    {
                        JelzoRajzol(false, false, false, true, false, e.Graphics);
                    }
                    else
                    {
                        JelzoRajzol(false, false, false, false, false, e.Graphics);
                    }
                }
                else
                {
                    JelzoRajzol(false, false, false, false, false, e.Graphics);
                }
            }
            //K2 betűk
            e.Graphics.DrawLine(feketePen, 25, 46, 25, 54);
            e.Graphics.DrawLine(feketePen, 29, 46, 26, 50);
            e.Graphics.DrawLine(feketePen, 26, 50, 29, 54);
            e.Graphics.DrawLine(feketePen, 31, 47, 31, 48);
            e.Graphics.DrawLine(feketePen, 32, 46, 34, 46);
            e.Graphics.DrawLine(feketePen, 35, 47, 35, 49);
            e.Graphics.DrawLine(feketePen, 35, 49, 31, 53);
            e.Graphics.DrawLine(feketePen, 31, 54, 35, 54);
        }

        private void K2_Click(object sender, EventArgs e)
        {
            if (senderPB == K2)
            {
                senderPB = null;
                UtolsoKezelesFrissit();
            }
            else
            {
                if (senderPB == B)
                {
                    vaganyut_B_K2();
                }
                else
                {
                    if (senderPB == vgutiA)
                    {
                        vaganyut_K2_ki();
                    }
                    else
                    {
                        if (senderPB == torles && Allomas.Vaganyutak[6].Felepitett && Allomas.Vaganyutak[6].KezdoJelzoSzabad == false && !Allomas.Vaganyutak[6].JelzoMegallj)
                        {
                            Allomas.Vaganyutak[6].Felepitett = false;
                            ValtoFeloldas((ValtoS)(Allomas.Valtok[0]));
                            SikeresKezeles();
                            Frissit();
                        }
                        else
                        {
                            if (senderPB == jelzoall && Allomas.Vaganyutak[6].Felepitett && Allomas.Vaganyutak[6].JelzoMegallj == false && Allomas.Vaganyutak[6].BejarandóSzakaszokSzabadok && (Allomas.Valtok[0] as ValtoS).AllitasAlatt == false)
                            {
                                Allomas.Jelzok[2].Szabad = true;
                                SikeresKezeles();
                                Frissit();
                            }
                            else
                            {
                                if (senderPB == megallj && Allomas.Vaganyutak[6].Felepitett && Allomas.Vaganyutak[6].KezdoJelzoSzabad)
                                {
                                    Allomas.Jelzok[2].Szabad = false;
                                    Allomas.Vaganyutak[6].JelzoMegallj = true;
                                    Ind.s_zavar = true;
                                    SikeresKezeles();
                                    Frissit();
                                }
                                else
                                {
                                    if (senderPB == A && (Allomas.Valtok[0] as ValtoS).Allas == false)
                                    {
                                        ValtoAllitas(Allomas.Valtok[0]);
                                        SikeresKezeles();
                                        Frissit();
                                    }
                                    else
                                    {
                                        senderPB = K2;
                                        UtolsoKezelesFrissit();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void K1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(keret, 0, 0, 59, 59);
            e.Graphics.FillRectangle(zold, 1, 1, 58, 58);
            e.Graphics.FillRectangle(fekete, 1, 24, 58, 13);

            if (Allomas.Szakaszok[7].Foglalt)
            {
                //vörös
                e.Graphics.FillRectangle(voros, 3, 26, 25, 9);
            }
            else if ((Allomas.Vaganyutak[0].Felepitett || Allomas.Vaganyutak[7].Felepitett) && (Allomas.Valtok[0] as ValtoS).VgutasAllitas == false)
            {
                //vágányút >> fehér
                e.Graphics.FillRectangle(feher, 3, 26, 25, 9);
            }
            else
            {
                //szürke
                e.Graphics.FillRectangle(szurke, 3, 26, 25, 9);
            }

            if (Allomas.Szakaszok[8].Foglalt)
            {
                e.Graphics.FillRectangle(voros, 32, 26, 25, 9);
            }
            else if (((Allomas.Vaganyutak[0].Felepitett || Allomas.Vaganyutak[7].Felepitett) && (Allomas.Valtok[0] as ValtoS).VgutasAllitas == false) || ((Allomas.Vaganyutak[5].Felepitett || Allomas.Vaganyutak[3].Felepitett) && (Allomas.Valtok[1] as ValtoS).VgutasAllitas == false))
            {
                e.Graphics.FillRectangle(feher, 32, 26, 25, 9);
            }
            else
            {
                e.Graphics.FillRectangle(szurke, 32, 26, 25, 9);
            }

            GombRajzol(e.Graphics, 0, 0, gyuruZold, gyuruZoldPen, voros, vorosPen);
            if (Allomas.Jelzok[1].Szabad)
            {
                JelzoRajzol(false, false, false, true, false, e.Graphics);
            }
            else
            {
                if (Allomas.Vaganyutak[7].JelzoMegallj)
                {
                    if (villogtatas)
                    {
                        JelzoRajzol(false, false, false, true, false, e.Graphics);
                    }
                    else
                    {
                        JelzoRajzol(false, false, false, false, false, e.Graphics);
                    }
                }
                else
                {
                    JelzoRajzol(false, false, false, false, false, e.Graphics);
                }
            }
            //K1 betűk
            e.Graphics.DrawLine(feketePen, 25, 46, 25, 54);
            e.Graphics.DrawLine(feketePen, 29, 46, 26, 50);
            e.Graphics.DrawLine(feketePen, 26, 50, 29, 54);
            e.Graphics.DrawLine(feketePen, 31, 49, 34, 46);
            e.Graphics.DrawLine(feketePen, 34, 46, 34, 54);
        }

        private void K1_Click(object sender, EventArgs e)
        {
            if (senderPB == K1)
            {
                senderPB = null;
                UtolsoKezelesFrissit();
            }
            else
            {
                if (senderPB == B)
                {
                    vaganyut_B_K1();
                }
                else
                {
                    if (senderPB == vgutiA)
                    {
                        vaganyut_K1_ki();
                    }
                    else
                    {
                        if (senderPB == torles && Allomas.Vaganyutak[7].Felepitett && Allomas.Vaganyutak[7].KezdoJelzoSzabad == false && !Allomas.Vaganyutak[7].JelzoMegallj)
                        {
                            Allomas.Vaganyutak[7].Felepitett = false;
                            ValtoFeloldas((ValtoS)(Allomas.Valtok[0]));
                            SikeresKezeles();
                            Frissit();
                        }
                        else
                        {
                            if (senderPB == jelzoall && Allomas.Vaganyutak[7].Felepitett && Allomas.Vaganyutak[7].BejarandóSzakaszokSzabadok && Allomas.Vaganyutak[7].JelzoMegallj == false && (Allomas.Valtok[0] as ValtoS).AllitasAlatt == false)
                            {
                                Allomas.Jelzok[1].Szabad = true;
                                SikeresKezeles();
                                Frissit();
                            }
                            else
                            {
                                if (senderPB == megallj && Allomas.Vaganyutak[7].Felepitett && Allomas.Vaganyutak[7].KezdoJelzoSzabad)
                                {
                                    Allomas.Jelzok[1].Szabad = false;
                                    Allomas.Vaganyutak[7].JelzoMegallj = true;
                                    Ind.s_zavar = true;
                                    SikeresKezeles();
                                    Frissit();
                                }
                                else
                                {
                                    if (senderPB == A && (Allomas.Valtok[0] as ValtoS).Allas == true)
                                    {
                                        ValtoAllitas(Allomas.Valtok[0]);
                                        SikeresKezeles();
                                        Frissit();
                                    }
                                    else
                                    {
                                        senderPB = K1;
                                        UtolsoKezelesFrissit();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void V2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(keret, 0, 0, 59, 59);
            e.Graphics.FillRectangle(zold, 1, 1, 58, 58);
            e.Graphics.FillRectangle(fekete, 1, 24, 58, 13);

            if (Allomas.Szakaszok[3].Foglalt)
            {
                //vörös
                e.Graphics.FillRectangle(voros, 3, 26, 25, 9);
            }
            else if (((Allomas.Vaganyutak[1].Felepitett || Allomas.Vaganyutak[6].Felepitett) && (Allomas.Valtok[0] as ValtoS).VgutasAllitas == false) || ((Allomas.Vaganyutak[4].Felepitett || Allomas.Vaganyutak[2].Felepitett) && (Allomas.Valtok[1] as ValtoS).VgutasAllitas == false))
            {
                //vágányút >> fehér
                e.Graphics.FillRectangle(feher, 3, 26, 25, 9);
            }
            else
            {
                //szürke
                e.Graphics.FillRectangle(szurke, 3, 26, 25, 9);
            }

            if (Allomas.Szakaszok[4].Foglalt)
            {
                e.Graphics.FillRectangle(voros, 32, 26, 25, 9);
            }
            else if ((Allomas.Vaganyutak[2].Felepitett || Allomas.Vaganyutak[4].Felepitett) && (Allomas.Valtok[1] as ValtoS).VgutasAllitas == false)
            {
                e.Graphics.FillRectangle(feher, 32, 26, 25, 9);
            }
            else
            {
                e.Graphics.FillRectangle(szurke, 32, 26, 25, 9);
            }

            GombRajzol(e.Graphics, 0, 0, gyuruZold, gyuruZoldPen, voros, vorosPen);
            if (Allomas.Jelzok[4].Szabad)
            {
                JelzoRajzol(true, false, false, true, false, e.Graphics);
            }
            else
            {
                if (Allomas.Vaganyutak[2].JelzoMegallj)
                {
                    if (villogtatas)
                    {
                        JelzoRajzol(true, false, false, true, false, e.Graphics);
                    }
                    else
                    {
                        JelzoRajzol(true, false, false, false, false, e.Graphics);
                    }
                }
                else
                {
                    JelzoRajzol(true, false, false, false, false, e.Graphics);
                }
            }
            //V2 betűk
            e.Graphics.DrawLine(feketePen, 24, 5, 27, 13);
            e.Graphics.DrawLine(feketePen, 27, 13, 30, 5);
            e.Graphics.DrawLine(feketePen, 32, 6, 32, 7);
            e.Graphics.DrawLine(feketePen, 33, 5, 35, 5);
            e.Graphics.DrawLine(feketePen, 36, 6, 36, 8);
            e.Graphics.DrawLine(feketePen, 36, 8, 32, 12);
            e.Graphics.DrawLine(feketePen, 32, 13, 36, 13);
        }

        private void V2_Click(object sender, EventArgs e)
        {
            if (senderPB == V2)
            {
                senderPB = null;
                UtolsoKezelesFrissit();
            }
            else
            {
                if (senderPB == vgutiB)
                {
                    vaganyut_V2_ki();
                }
                else
                {
                    if (senderPB == A)
                    {
                        vaganyut_A_V2();
                    }
                    else
                    {
                        if (senderPB == torles && Allomas.Vaganyutak[2].Felepitett && Allomas.Vaganyutak[2].KezdoJelzoSzabad == false && !Allomas.Vaganyutak[2].JelzoMegallj)
                        {
                            Allomas.Vaganyutak[2].Felepitett = false;
                            ValtoFeloldas((ValtoS)(Allomas.Valtok[1]));
                            SikeresKezeles();
                            Frissit();
                        }
                        else
                        {
                            if (senderPB == jelzoall && Allomas.Vaganyutak[2].Felepitett && Allomas.Vaganyutak[2].BejarandóSzakaszokSzabadok && Allomas.Vaganyutak[2].JelzoMegallj == false && (Allomas.Valtok[1] as ValtoS).AllitasAlatt == false)
                            {
                                Allomas.Jelzok[4].Szabad = true;
                                SikeresKezeles();
                                Frissit();
                            }
                            else
                            {
                                if (senderPB == megallj && Allomas.Vaganyutak[2].Felepitett && Allomas.Vaganyutak[2].KezdoJelzoSzabad)
                                {
                                    Allomas.Jelzok[4].Szabad = false;
                                    Allomas.Vaganyutak[2].JelzoMegallj = true;
                                    Ind.s_zavar = true;
                                    SikeresKezeles();
                                    Frissit();
                                }
                                else
                                {
                                    if (senderPB == B && (Allomas.Valtok[1] as ValtoS).Allas == false)
                                    {
                                        ValtoAllitas(Allomas.Valtok[1]);
                                        SikeresKezeles();
                                        Frissit();
                                    }
                                    else
                                    {
                                        senderPB = V2;
                                        UtolsoKezelesFrissit();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void V1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(keret, 0, 0, 59, 59);
            e.Graphics.FillRectangle(zold, 1, 1, 58, 58);
            e.Graphics.FillRectangle(fekete, 1, 24, 58, 13);

            if (Allomas.Szakaszok[8].Foglalt)
            {
                //vörös
                e.Graphics.FillRectangle(voros, 3, 26, 25, 9);
            }
            else if (((Allomas.Vaganyutak[0].Felepitett || Allomas.Vaganyutak[7].Felepitett) && (Allomas.Valtok[0] as ValtoS).VgutasAllitas == false) || ((Allomas.Vaganyutak[5].Felepitett || Allomas.Vaganyutak[3].Felepitett) && (Allomas.Valtok[1] as ValtoS).VgutasAllitas == false))
            {
                //vágányút >> fehér
                e.Graphics.FillRectangle(feher, 3, 26, 25, 9);
            }
            else
            {
                //szürke
                e.Graphics.FillRectangle(szurke, 3, 26, 25, 9);
            }

            if (Allomas.Szakaszok[9].Foglalt)
            {
                e.Graphics.FillRectangle(voros, 32, 26, 25, 9);
            }
            else if ((Allomas.Vaganyutak[3].Felepitett || Allomas.Vaganyutak[5].Felepitett) && (Allomas.Valtok[1] as ValtoS).VgutasAllitas == false)
            {
                e.Graphics.FillRectangle(feher, 32, 26, 25, 9);
            }
            else
            {
                e.Graphics.FillRectangle(szurke, 32, 26, 25, 9);
            }

            GombRajzol(e.Graphics, 0, 0, gyuruZold, gyuruZoldPen, voros, vorosPen);
            if (Allomas.Jelzok[3].Szabad)
            {
                JelzoRajzol(true, false, false, true, false, e.Graphics);
            }
            else
            {
                if (Allomas.Vaganyutak[3].JelzoMegallj)
                {
                    if (villogtatas)
                    {
                        JelzoRajzol(true, false, false, true, false, e.Graphics);
                    }
                    else
                    {
                        JelzoRajzol(true, false, false, false, false, e.Graphics);
                    }
                }
                else
                {
                    JelzoRajzol(true, false, false, false, false, e.Graphics);
                }
            }
            //V1 betűk
            e.Graphics.DrawLine(feketePen, 24, 5, 27, 13);
            e.Graphics.DrawLine(feketePen, 27, 13, 30, 5);
            e.Graphics.DrawLine(feketePen, 32, 8, 35, 5);
            e.Graphics.DrawLine(feketePen, 35, 5, 35, 13);
        }

        private void V1_Click(object sender, EventArgs e)
        {
            if (senderPB == V1)
            {
                senderPB = null;
                UtolsoKezelesFrissit();
            }
            else
            {
                if (senderPB == A)
                {
                    vaganyut_A_V1();
                }
                else
                {
                    if (senderPB == vgutiB)
                    {
                        vaganyut_V1_ki();
                    }
                    else
                    {
                        if (senderPB == torles && Allomas.Vaganyutak[3].Felepitett && Allomas.Vaganyutak[3].KezdoJelzoSzabad == false && !Allomas.Vaganyutak[3].JelzoMegallj)
                        {
                            Allomas.Vaganyutak[3].Felepitett = false;
                            ValtoFeloldas((ValtoS)(Allomas.Valtok[1]));
                            SikeresKezeles();
                            Frissit();
                        }
                        else
                        {
                            if (senderPB == jelzoall && Allomas.Vaganyutak[3].Felepitett && Allomas.Vaganyutak[3].BejarandóSzakaszokSzabadok && Allomas.Vaganyutak[3].JelzoMegallj == false && (Allomas.Valtok[1] as ValtoS).AllitasAlatt == false)
                            {
                                Allomas.Jelzok[3].Szabad = true;
                                SikeresKezeles();
                                Frissit();
                            }
                            else
                            {
                                if (senderPB == megallj && Allomas.Vaganyutak[3].Felepitett && Allomas.Vaganyutak[3].KezdoJelzoSzabad)
                                {
                                    Allomas.Jelzok[3].Szabad = false;
                                    Allomas.Vaganyutak[3].JelzoMegallj = true;
                                    Ind.s_zavar = true;
                                    SikeresKezeles();
                                    Frissit();
                                }
                                else
                                {
                                    if (senderPB == B && (Allomas.Valtok[1] as ValtoS).Allas == true)
                                    {
                                        ValtoAllitas(Allomas.Valtok[1]);
                                        SikeresKezeles();
                                        Frissit();
                                    }
                                    else
                                    {
                                        senderPB = V1;
                                        UtolsoKezelesFrissit();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void vgutiB_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(keret, 0, 0, 59, 59);
            e.Graphics.FillRectangle(zold, 1, 1, 58, 58);
            e.Graphics.FillRectangle(fekete, 1, 24, 58, 13);

            if (Allomas.Szakaszok[4].Foglalt || Allomas.Szakaszok[9].Foglalt)
            {
                //vörös
                e.Graphics.FillRectangle(voros, 3, 26, 25, 9);
            }
            else if ((Allomas.Vaganyutak[5].Felepitett || Allomas.Vaganyutak[3].Felepitett || Allomas.Vaganyutak[4].Felepitett || Allomas.Vaganyutak[2].Felepitett) && (Allomas.Valtok[1] as ValtoS).VgutasAllitas == false)
            {
                //vágányút >> fehér
                e.Graphics.FillRectangle(feher, 3, 26, 25, 9);
            }
            else
            {
                //szürke
                e.Graphics.FillRectangle(szurke, 3, 26, 25, 9);
            }

            if (Allomas.Szakaszok[5].Foglalt)
            {
                e.Graphics.FillRectangle(voros, 32, 26, 25, 9);
            }
            else if ((Allomas.Vaganyutak[5].Felepitett || Allomas.Vaganyutak[4].Felepitett || Allomas.Vaganyutak[2].Felepitett || Allomas.Vaganyutak[3].Felepitett) && (Allomas.Valtok[1] as ValtoS).VgutasAllitas == false)
            {
                e.Graphics.FillRectangle(feher, 32, 26, 25, 9);
            }
            else
            {
                e.Graphics.FillRectangle(szurke, 32, 26, 25, 9);
            }

            GombRajzol(e.Graphics, 0, 0, gyuruZold, gyuruZoldPen, gombZold, gombZoldPen);
            //b betű
            e.Graphics.DrawLine(feketePen, 27, 46, 27, 54);
            e.Graphics.DrawLine(feketePen, 28, 50, 29, 49);
            e.Graphics.DrawLine(feketePen, 29, 49, 30, 49);
            e.Graphics.DrawLine(feketePen, 28, 53, 29, 54);
            e.Graphics.DrawLine(feketePen, 29, 54, 30, 54);
            e.Graphics.DrawLine(feketePen, 31, 50, 31, 53);
        }

        private void vgutiB_Click(object sender, EventArgs e)
        {
            if (senderPB == vgutiB)
            {
                senderPB = null;
                UtolsoKezelesFrissit();
            }
            else
            {
                if (senderPB == V2)
                {
                    vaganyut_V2_ki();
                }
                else
                {
                    if (senderPB == V1)
                    {
                        vaganyut_V1_ki();
                    }
                    else
                    {
                        senderPB = vgutiB;
                        UtolsoKezelesFrissit();
                    }
                }
            }
        }

        private void B_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(keret, 0, 0, 59, 59);
            e.Graphics.FillRectangle(zold, 1, 1, 58, 58);
            e.Graphics.FillRectangle(fekete, 1, 24, 58, 13);

            if (Allomas.Szakaszok[5].Foglalt)
            {
                //vörös
                e.Graphics.FillRectangle(voros, 3, 26, 25, 9);
                e.Graphics.FillRectangle(voros, 32, 26, 25, 9);
            }
            else if ((Allomas.Vaganyutak[5].Felepitett || Allomas.Vaganyutak[4].Felepitett || Allomas.Vaganyutak[2].Felepitett || Allomas.Vaganyutak[3].Felepitett) && (Allomas.Valtok[1] as ValtoS).VgutasAllitas == false)
            {
                //vágányút >> fehér
                e.Graphics.FillRectangle(feher, 3, 26, 25, 9);
                e.Graphics.FillRectangle(feher, 32, 26, 25, 9);
            }
            else
            {
                //szürke
                e.Graphics.FillRectangle(szurke, 3, 26, 25, 9);
                e.Graphics.FillRectangle(szurke, 32, 26, 25, 9);
            }

            GombRajzol(e.Graphics, 0, 0, gyuruZold, gyuruZoldPen, voros, vorosPen);
            if (Allomas.Jelzok[5].Szabad)
            {
                JelzoRajzol(false, true, false, true, false, e.Graphics);
            }
            else
            {
                if (Allomas.Vaganyutak[4].JelzoMegallj || Allomas.Vaganyutak[5].JelzoMegallj)
                {
                    if (villogtatas)
                    {
                        JelzoRajzol(false, true, true, true, false, e.Graphics);
                    }
                    else
                    {
                        JelzoRajzol(false, true, true, false, false, e.Graphics);
                    }
                }
                else
                {
                    JelzoRajzol(false, true, true, false, false, e.Graphics);
                }
            }
            //B betű
            e.Graphics.DrawLine(feketePen, 27, 46, 27, 54);
            e.Graphics.DrawLine(feketePen, 27, 54, 30, 54);
            e.Graphics.DrawLine(feketePen, 28, 50, 30, 50);
            e.Graphics.DrawLine(feketePen, 28, 46, 30, 46);
            e.Graphics.DrawLine(feketePen, 31, 47, 31, 49);
            e.Graphics.DrawLine(feketePen, 31, 51, 31, 53);
        }

        private void B_Click(object sender, EventArgs e)
        {
            if (senderPB == B)
            {
                senderPB = null;
                UtolsoKezelesFrissit();
            }
            else
            {
                if (senderPB == K2)
                {
                    vaganyut_B_K2();
                }
                else
                {
                    if (senderPB == K1)
                    {
                        vaganyut_B_K1();
                    }
                    else
                    {
                        if (senderPB == torles && ((Allomas.Vaganyutak[5].Felepitett && Allomas.Vaganyutak[5].KezdoJelzoSzabad == false && !Allomas.Vaganyutak[5].JelzoMegallj) || (Allomas.Vaganyutak[4].Felepitett && Allomas.Vaganyutak[4].KezdoJelzoSzabad == false && !Allomas.Vaganyutak[4].JelzoMegallj)))
                        {
                            Allomas.Vaganyutak[4].Felepitett = false;
                            Allomas.Vaganyutak[5].Felepitett = false;
                            ValtoFeloldas((ValtoS)(Allomas.Valtok[1]));
                            SikeresKezeles();
                            Frissit();
                        }
                        else
                        {
                            if (senderPB == jelzoall && ((Allomas.Vaganyutak[5].Felepitett && Allomas.Vaganyutak[5].JelzoMegallj == false && Allomas.Vaganyutak[5].BejarandóSzakaszokSzabadok) || (Allomas.Vaganyutak[4].Felepitett && Allomas.Vaganyutak[4].JelzoMegallj == false && Allomas.Vaganyutak[4].BejarandóSzakaszokSzabadok)) && (Allomas.Valtok[1] as ValtoS).AllitasAlatt == false)
                            {
                                Allomas.Jelzok[5].Szabad = true;
                                SikeresKezeles();
                                Frissit();
                            }
                            else
                            {
                                if (senderPB == megallj && ((Allomas.Vaganyutak[5].Felepitett && Allomas.Vaganyutak[5].KezdoJelzoSzabad) || (Allomas.Vaganyutak[4].Felepitett && Allomas.Vaganyutak[4].KezdoJelzoSzabad)))
                                {
                                    Allomas.Jelzok[5].Szabad = false;
                                    if (Allomas.Vaganyutak[5].Felepitett)
                                    {
                                        Allomas.Vaganyutak[5].JelzoMegallj = true;
                                    }
                                    else
                                    {
                                        Allomas.Vaganyutak[4].JelzoMegallj = true;
                                    }
                                    Ind.s_zavar = true;
                                    SikeresKezeles();
                                    Frissit();
                                }
                                else
                                {
                                    if (senderPB == V2 && (Allomas.Valtok[1] as ValtoS).Allas == false)
                                    {
                                        ValtoAllitas(Allomas.Valtok[1]);
                                        SikeresKezeles();
                                        Frissit();
                                    }
                                    else
                                    {
                                        if (senderPB == V1 && (Allomas.Valtok[1] as ValtoS).Allas == true)
                                        {
                                            ValtoAllitas(Allomas.Valtok[1]);
                                            SikeresKezeles();
                                            Frissit();
                                        }
                                        else
                                        {
                                            senderPB = B;
                                            UtolsoKezelesFrissit();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void megallj_Click(object sender, EventArgs e)
        {
            if (senderPB == megallj)
            {
                senderPB = null;
                UtolsoKezelesFrissit();
            }
            else
            {
                if (senderPB == A && ((Allomas.Vaganyutak[0].Felepitett && Allomas.Vaganyutak[0].KezdoJelzoSzabad) || (Allomas.Vaganyutak[1].Felepitett && Allomas.Vaganyutak[1].KezdoJelzoSzabad)))
                {
                    Allomas.Jelzok[0].Szabad = false;
                    if (Allomas.Vaganyutak[0].Felepitett)
                    {
                        Allomas.Vaganyutak[0].JelzoMegallj = true;
                    }
                    else
                    {
                        Allomas.Vaganyutak[1].JelzoMegallj = true;
                    }
                    Ind.s_zavar = true;
                    SikeresKezeles();
                    Frissit();
                }
                else
                {
                    if (senderPB == K2 && Allomas.Vaganyutak[6].Felepitett && Allomas.Vaganyutak[6].KezdoJelzoSzabad)
                    {
                        Allomas.Jelzok[2].Szabad = false;
                        Allomas.Vaganyutak[6].JelzoMegallj = true;
                        Ind.s_zavar = true;
                        SikeresKezeles();
                        Frissit();
                    }
                    else
                    {
                        if (senderPB == K1 && Allomas.Vaganyutak[7].Felepitett && Allomas.Vaganyutak[7].KezdoJelzoSzabad)
                        {
                            Allomas.Jelzok[1].Szabad = false;
                            Allomas.Vaganyutak[7].JelzoMegallj = true;
                            Ind.s_zavar = true;
                            SikeresKezeles();
                            Frissit();
                        }
                        else
                        {
                            if (senderPB == V2 && Allomas.Vaganyutak[2].Felepitett && Allomas.Vaganyutak[2].KezdoJelzoSzabad)
                            {
                                Allomas.Jelzok[4].Szabad = false;
                                Allomas.Vaganyutak[2].JelzoMegallj = true;
                                Ind.s_zavar = true;
                                SikeresKezeles();
                                Frissit();
                            }
                            else
                            {
                                if (senderPB == V1 && Allomas.Vaganyutak[3].Felepitett && Allomas.Vaganyutak[3].KezdoJelzoSzabad)
                                {
                                    Allomas.Jelzok[3].Szabad = false;
                                    Allomas.Vaganyutak[3].JelzoMegallj = true;
                                    Ind.s_zavar = true;
                                    SikeresKezeles();
                                    Frissit();
                                }
                                else
                                {
                                    if (senderPB == B && ((Allomas.Vaganyutak[5].Felepitett && Allomas.Vaganyutak[5].KezdoJelzoSzabad) || (Allomas.Vaganyutak[4].Felepitett && Allomas.Vaganyutak[4].KezdoJelzoSzabad)))
                                    {
                                        Allomas.Jelzok[5].Szabad = false;
                                        if (Allomas.Vaganyutak[5].Felepitett)
                                        {
                                            Allomas.Vaganyutak[5].JelzoMegallj = true;
                                        }
                                        else
                                        {
                                            Allomas.Vaganyutak[4].JelzoMegallj = true;
                                        }
                                        Ind.s_zavar = true;
                                        SikeresKezeles();
                                        Frissit();
                                    }
                                    else
                                    {
                                        senderPB = megallj;
                                        UtolsoKezelesFrissit();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void zavar_Click(object sender, EventArgs e)
        {
            if (senderPB == zavar)
            {
                senderPB = null;
                UtolsoKezelesFrissit();
            }
            else
            {
                senderPB = zavar;
                UtolsoKezelesFrissit();
            }
            if (Ind.s_zavar)
            {
                if (Ind.s_zavar_lejatszas == true)
                {
                    player.Stop();
                    Ind.s_zavar_lejatszas = false;
                    Ind.s_zavar_kezelve = true;
                    SikeresKezeles();
                }
            }
        }

        private void hivo_Click(object sender, EventArgs e)
        {
            SikeresKezeles();
            SzamlaloFigy szf = new SzamlaloFigy();
            szf.ShowDialog();
            Ind.Hibak[10]++;
        }

        private void valtoall_MouseMove(object sender, MouseEventArgs e)
        {
            if ((Allomas.Valtok[1] as ValtoS).Lezart == false && (Allomas.Valtok[1] as ValtoS).AllitasAlatt == false && (Allomas.Valtok[1] as ValtoS).Foglalt == false)
            {
                (Allomas.Valtok[1] as ValtoS).ValtoallMouseMove = true;
                if ((Allomas.Valtok[1] as ValtoS).Allas == true)
                {
                    (Allomas.Valtok[1] as ValtoS).SzcsE = true;
                }
                else
                {
                    (Allomas.Valtok[1] as ValtoS).SzcsK = true;
                }
                Frissit();
            }
            if ((Allomas.Valtok[0] as ValtoS).Lezart == false && (Allomas.Valtok[0] as ValtoS).AllitasAlatt == false && (Allomas.Valtok[0] as ValtoS).Foglalt == false)
            {
                (Allomas.Valtok[0] as ValtoS).ValtoallMouseMove = true;
                if ((Allomas.Valtok[0] as ValtoS).Allas == true)
                {
                    (Allomas.Valtok[0] as ValtoS).SzcsE = true;
                }
                else
                {
                    (Allomas.Valtok[0] as ValtoS).SzcsK = true;
                }
                Frissit();
            }
        }

        private void valtoall_MouseLeave(object sender, EventArgs e)
        {
            if ((Allomas.Valtok[1] as ValtoS).ValtoallMouseMove)
            {
                (Allomas.Valtok[1] as ValtoS).ValtoallMouseMove = false;
                (Allomas.Valtok[1] as ValtoS).SzcsE = false;
                (Allomas.Valtok[1] as ValtoS).SzcsK = false;
                Frissit();
            }
            if ((Allomas.Valtok[0] as ValtoS).ValtoallMouseMove)
            {
                (Allomas.Valtok[0] as ValtoS).ValtoallMouseMove = false;
                (Allomas.Valtok[0] as ValtoS).SzcsE = false;
                (Allomas.Valtok[0] as ValtoS).SzcsK = false;
                Frissit();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Allomasfonok allfon = new Allomasfonok(Allomas, this, Ind);
            allfon.szjn = this;
            if (allfon.ShowDialog() == DialogResult.OK)
            {
                Frissit();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Naplozo napl = new Naplozo(Allomas, false, false, Ind);
            if (napl.ShowDialog() == DialogResult.OK)
            {
                Frissit();
            }
        }

        private void s_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Ind.S_open = false;
            Hr.Close();
        }

        private void parosVisszaVillanto_Tick(object sender, EventArgs e)
        {
            (sender as VgutVisszavillanto).Stop();
            (sender as VgutVisszavillanto).Startolhato = true;
            (sender as VgutVisszavillanto).Vgut.Felepitett = false;
            Frissit();
            (sender as VgutVisszavillanto).Vgut.Kezdopont.VonatMeghaladta = false;

            if ((sender as VgutVisszavillanto).Vgut.Kezdopont == Allomas.Jelzok[0] || (sender as VgutVisszavillanto).Vgut.Kezdopont == Allomas.Jelzok[2] || (sender as VgutVisszavillanto).Vgut.Kezdopont == Allomas.Jelzok[1])
            {
                ValtoFeloldas((ValtoS)(Allomas.Valtok[0]));
            }
            else
            {
                ValtoFeloldas((ValtoS)(Allomas.Valtok[1]));
            }

            (sender as VgutVisszavillanto).Vgut = null;
            Frissit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Allomas.AddVonat(new Vonat(new string[]{"112"}, Allomas.Szakaszok[3], Allomas));
            Frissit();
        }

        private void S_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hr.Hide();
            this.Hide();
            e.Cancel = true;
        }

        private void S_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z && e.Modifiers == (Keys.Alt | Keys.Control))
            {
                AllomasFormAllomasLezar(label3, label6);
            }
        }
    }

    public class D55Felirat : Label
    {
        public D55Felirat()
        {
            BackColor = System.Drawing.Color.FromArgb(88, 166, 82);
        }
    }
    public class VgutVisszavillanto : Timer
    {
        Vaganyut vgut;
        public Vaganyut Vgut
        {
            get { return vgut; }
            set { vgut = value; }
        }

        bool startolhato = true;
        public bool Startolhato
        {
            get { return startolhato; }
            set { startolhato = value; }
        }
    }
}
