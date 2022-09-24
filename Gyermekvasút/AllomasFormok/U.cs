using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gyermekvasút.Biztberek;
using Gyermekvasút.Modellek;
using Gyermekvasút.Telefon;
using System.Diagnostics;

namespace Gyermekvasút.AllomasFormok
{
    public partial class U : AllomasForm, ICsilleberc
    {
        int x = 0;
        public int X
        {
            get { return x; }
            set
            {
                x = value;
                label1.Text = x.ToString();
            }
        }
        int y = 0;
        public int Y
        {
            get { return y; }
            set
            {
                y = value;
                label2.Text = y.ToString();
            }
        }

        public U()
        {
            InitializeComponent();
        }

        public U(Index index, Allomas allomas)
        {
            this.Ind = index;
            this.Allomas = allomas;
            InitializeComponent();

            VES = ves1;

            ves1.jelzoKG_A.BalfelsoZold = pictureBox1;
            ves1.jelzoKG_A.JobbfelsoVoros = pictureBox2;
            ves1.jelzoKG_A.BalalsoBe = pictureBox4;
            ves1.jelzoKG_A.JobbalsoKi = pictureBox3;

            valtoKapcsologomb2.Ablak = valto2;
            valtoKapcsologomb2.Valto = Allomas.Valtok[0];

            valtoKapcsologomb1.Ablak = valto1;
            valtoKapcsologomb1.Valto = Allomas.Valtok[1];

            foreach (Control ctrl in this.Controls)
            {
                ctrl.MouseMove += U_MouseMove;
                ctrl.MouseUp += U_MouseUp;
                if (ctrl is Kapcsologomb)
                {
                    (ctrl as Kapcsologomb).VES = ves1;

                    (ctrl as Kapcsologomb).AllomasForm = this;
                    if (ctrl is JelzoKapcsologomb)
                    {
                        (ctrl as JelzoKapcsologomb).Img = Gyermekvasút.Properties.Resources.jelzoKapcsGomb;
                        (ctrl as JelzoKapcsologomb).KpY = (ctrl as JelzoKapcsologomb).Location.Y + 32;
                        (ctrl as JelzoKapcsologomb).KpX = (ctrl as JelzoKapcsologomb).Location.X + 32.5;
                        (ctrl as JelzoKapcsologomb).BalfelsoZold.BackColor = VES.Zold;
                        (ctrl as JelzoKapcsologomb).JobbfelsoVoros.BackColor = VES.Voros;
                        (ctrl as JelzoKapcsologomb).BalalsoBe.BackColor = VES.Fekete;
                        (ctrl as JelzoKapcsologomb).JobbalsoKi.BackColor = VES.Fekete;
                        (ctrl as JelzoKapcsologomb).MinimumAllas = 0;
                        (ctrl as JelzoKapcsologomb).CelAllas = 0;
                    }
                    if (ctrl is VaganyutiKapcsoloGomb)
                    {
                        (ctrl as VaganyutiKapcsoloGomb).Vg1.BackColor = VES.Fekete;
                        (ctrl as VaganyutiKapcsoloGomb).Vg1.BackgroundImage = null;
                        (ctrl as VaganyutiKapcsoloGomb).Vg2.BackColor = VES.Fekete;
                        (ctrl as VaganyutiKapcsoloGomb).Vg2.BackgroundImage = null;
                        (ctrl as VaganyutiKapcsoloGomb).KpY = (ctrl as VaganyutiKapcsoloGomb).Location.Y + 32;
                        (ctrl as VaganyutiKapcsoloGomb).KpX = (ctrl as VaganyutiKapcsoloGomb).Location.X + 32.5;
                    }
                    if (ctrl is ValtoKapcsologomb)
                    {
                        (ctrl as ValtoKapcsologomb).KpY = (ctrl as ValtoKapcsologomb).Location.Y + 21.5;
                        (ctrl as ValtoKapcsologomb).KpX = (ctrl as ValtoKapcsologomb).Location.X + 21.5;
                    }
                    if (ctrl is VESOlomzarasGomb)
                    {
                        (ctrl as VESOlomzarasGomb).AllomasForm = this;
                    }
                }
            }

            ves1.vgutKG_A.MinimumAllas = 0;
            ves1.vgutKG_A.CelAllas = -45;
        }

        Kapcsologomb allitas;
        public Kapcsologomb Allitas
        {
            get
            {
                return allitas;
            }
            set
            {
                allitas = value;
                label3.Text = allitas == null ? "NULL" : allitas.ToString();
            }
        }

        private void U_MouseMove(object sender, MouseEventArgs e)
        {
            X = this.PointToClient(Cursor.Position).X;
            Y = this.PointToClient(Cursor.Position).Y;

            if (Allitas != null)
            {
                double temp = Allitas.Alpha;
                double ideiglenesAlpha = 0; //setter lefutás késleltetése

                ideiglenesAlpha = Math.Atan((X - Allitas.KpX) / (Allitas.KpY - Y)) * 180 / Math.PI;

                if (Allitas is JelzoKapcsologomb)
                {
                    if (((Allitas as JelzoKapcsologomb).JobbraAllitas && Allitas.CelAllas < Allitas.MinimumAllas) || (!(Allitas as JelzoKapcsologomb).JobbraAllitas && Allitas.CelAllas > Allitas.MinimumAllas))
                    {
                        int temp2 = Allitas.CelAllas;
                        Allitas.CelAllas = Allitas.MinimumAllas;
                        Allitas.MinimumAllas = temp2;
                    }

                    int absKisebb = Math.Abs((Allitas as JelzoKapcsologomb).MinimumAllas) < Math.Abs((Allitas as JelzoKapcsologomb).CelAllas) ? (Allitas as JelzoKapcsologomb).MinimumAllas : (Allitas as JelzoKapcsologomb).CelAllas;
                    int absNagyobb = Math.Abs((Allitas as JelzoKapcsologomb).MinimumAllas) < Math.Abs((Allitas as JelzoKapcsologomb).CelAllas) ? (Allitas as JelzoKapcsologomb).CelAllas : (Allitas as JelzoKapcsologomb).MinimumAllas;

                    if (absKisebb * absNagyobb < 0)
                    {
                        if (((X - Allitas.KpX) * Math.Tan(Math.PI / 2 - absNagyobb * Math.PI / 180) < Allitas.KpY - Y) && (Allitas.KpY - Y > (X - Allitas.KpX) * Math.Tan(Math.PI / 2 - absKisebb * Math.PI / 180)))
                        {
                            Allitas.Image = Kapcsologomb.RotateImage(Gyermekvasút.Properties.Resources.jelzoKapcsGomb, (float)Allitas.Alpha, Allitas.KpY - Allitas.Location.Y);
                            Allitas.Alpha = ideiglenesAlpha;
                            label4.Text = Allitas.Alpha.ToString();
                        }
                        //else
                        //{
                        //    Allitas.Alpha = temp;
                        //    label4.Text = Allitas.Alpha.ToString();
                        //}
                    }
                    else
                    {
                        if (((X - Allitas.KpX) * Math.Tan(Math.PI / 2 - absNagyobb * Math.PI / 180) < Allitas.KpY - Y) && (Allitas.KpY - Y < (X - Allitas.KpX) * Math.Tan(Math.PI / 2 - absKisebb * Math.PI / 180)))
                        {
                            Allitas.Image = Kapcsologomb.RotateImage(Gyermekvasút.Properties.Resources.jelzoKapcsGomb, (float)Allitas.Alpha, Allitas.KpY - Allitas.Location.Y);
                            Allitas.Alpha = ideiglenesAlpha;
                            label4.Text = Allitas.Alpha.ToString();
                        }
                        else
                        {
                            Allitas.Alpha = temp;
                            label4.Text = Allitas.Alpha.ToString();
                        }
                    }
                }
                if (Allitas is ValtoKapcsologomb)
                {
                    //int absKisebb = Math.Abs(Allitas.MinimumAllas) < Math.Abs(Allitas.CelAllas) ? Allitas.MinimumAllas : Allitas.CelAllas;
                    //int absNagyobb = Math.Abs(Allitas.MinimumAllas) < Math.Abs(Allitas.CelAllas) ? Allitas.CelAllas : Allitas.MinimumAllas;

                    //if (((X - Allitas.KpX) * Math.Tan(Math.PI / 2 - absNagyobb * Math.PI / 180) < Allitas.KpY - Y) && (Allitas.KpY - Y < (X - Allitas.KpX) * Math.Tan(Math.PI / 2 - absKisebb * Math.PI / 180)))
                    //{
                    //    Allitas.Image = Kapcsologomb.RotateImage(Gyermekvasút.Properties.Resources.valtoKapcsGomb, (float)Allitas.Alpha, Allitas.KpY - Allitas.Location.Y);
                    //    Allitas.Alpha = ideiglenesAlpha;
                    //    label4.Text = Allitas.Alpha.ToString();
                    //}
                    //else
                    //{
                    //    Allitas.Alpha = temp;
                    //    label4.Text = Allitas.Alpha.ToString();
                    //}

                    int nagy = Allitas.CelAllas >= Allitas.MinimumAllas ? Allitas.CelAllas : Allitas.MinimumAllas;
                    int kis = Allitas.CelAllas < Allitas.MinimumAllas ? Allitas.CelAllas : Allitas.MinimumAllas;
                    double alph = Math.Atan((X - Allitas.KpX) / (Allitas.KpY - Y)) * 180 / Math.PI;

                    if (alph <= nagy && alph >= kis && nagy != kis)
                    {
                        Allitas.Image = Kapcsologomb.RotateImage(Gyermekvasút.Properties.Resources.valtoKapcsGomb, (float)Allitas.Alpha, Allitas.KpY - Allitas.Location.Y);
                        Allitas.Alpha = ideiglenesAlpha;
                        label4.Text = Allitas.Alpha.ToString();
                    }
                    //else
                    //{
                    //    Allitas.Alpha = temp;
                    //    label4.Text = Allitas.Alpha.ToString();
                    //}
                }
                if (Allitas is VaganyutiKapcsoloGomb)
                {
                    int nagy = Allitas.CelAllas >= Allitas.MinimumAllas ? Allitas.CelAllas : Allitas.MinimumAllas;
                    int kis = Allitas.CelAllas < Allitas.MinimumAllas ? Allitas.CelAllas : Allitas.MinimumAllas;
                    double alph = Math.Atan((X - Allitas.KpX) / (Allitas.KpY - Y)) * 180 / Math.PI;

                    if (alph <= nagy && alph >= kis && nagy != kis)
                    {
                        Allitas.Image = Kapcsologomb.RotateImage(Gyermekvasút.Properties.Resources.vgutiKapcsGomb, (float)Allitas.Alpha, Allitas.KpY - Allitas.Location.Y);
                        Allitas.Alpha = ideiglenesAlpha;
                        label4.Text = Allitas.Alpha.ToString();
                    }
                    //else
                    //{
                    //    Allitas.Alpha = temp;
                    //    label4.Text = Allitas.Alpha.ToString();
                    //}
                }
            }
        }

        private void U_Load(object sender, EventArgs e)
        {
            if (Allomas.Lezarva)
            {
                label8.Text = "Ez az állomás le van zárva.";
                foreach (Control item in this.Controls)
                {
                    item.Enabled = false;
                }
            }

            Ind.U_open = true;
            
            if (Hr == null)
            {
                Hr = new Hr(false, true, Allomas);
            }

            if (Hr.Frissitheto)
            {
                Hr.vszkRefresh();
            }
            Frissit();

            #region OLD
            //ves1.vgutKG_A.Vg1 = pictureBox6;
            //ves1.vgutKG_A.Vg2 = pictureBox5;

            //ves1.vgutKG_L.Vg1 = pictureBox12;
            //ves1.vgutKG_L.Vg2 = pictureBox11;

            //ves1.jelzoKG_L.BalfelsoZold = pictureBox10;
            //ves1.jelzoKG_L.JobbfelsoVoros = pictureBox9;
            //ves1.jelzoKG_L.BalalsoBe = pictureBox8;
            //ves1.jelzoKG_L.JobbalsoKi = pictureBox7;
                      
            //ves1.vgutKG_L.MinimumAllas = 0;
            //ves1.vgutKG_L.CelAllas = 45;
            #endregion

            if (Ind.ejjel)
            {
                ejjel.Image = Gyermekvasút.Properties.Resources.VESvisszjelVilagit;
                nappal.Image = Gyermekvasút.Properties.Resources.VESvisszjelSotet;
            }
            else
            {
                ejjel.Image = Gyermekvasút.Properties.Resources.VESvisszjelSotet;
                nappal.Image = Gyermekvasút.Properties.Resources.VESvisszjelVilagit;
            }

            if (!Ind.u_valtovil)
            {
                valtovil.Image = Gyermekvasút.Properties.Resources.VESvaltovil_sotet;
            }
            else
            {
                valtovil.Image = Gyermekvasút.Properties.Resources.VESvaltovil_vilagit;
            }
        }

        private void U_MouseUp(object sender, MouseEventArgs e)
        {
            if (Allitas != null)
            {
                if (Allitas is JelzoKapcsologomb)
                {
                    #region JELZOKAPCSOLOGOMB KÓDJA

                    if ((Allitas as JelzoKapcsologomb).JobbraAllitas)
                    {
                        if (Allitas.Alpha >= (Allitas as JelzoKapcsologomb).CelAllas - 10)
                        {
                            Allitas.Image = Kapcsologomb.RotateImage(Gyermekvasút.Properties.Resources.jelzoKapcsGomb, (float)(Allitas as JelzoKapcsologomb).CelAllas, Allitas.KpY - Allitas.Location.Y);
                            Allitas.Alpha = (Allitas as JelzoKapcsologomb).CelAllas;
                            label4.Text = Allitas.Alpha.ToString();
                        }
                    }
                    else
                    {
                        if (Allitas.Alpha <= (Allitas as JelzoKapcsologomb).CelAllas + 10)
                        {
                            Allitas.Image = Kapcsologomb.RotateImage(Gyermekvasút.Properties.Resources.jelzoKapcsGomb, (float)(Allitas as JelzoKapcsologomb).CelAllas, Allitas.KpY - Allitas.Location.Y);
                            Allitas.Alpha = (Allitas as JelzoKapcsologomb).CelAllas;
                            label4.Text = Allitas.Alpha.ToString();
                        }
                    }

                    #region VAGANYUTIKAPCSOLOGOMB ENGEDÉLYEZÉSE/TILTÁSA
                    if (Allitas.Alpha == 0)
                    {//jelző alapban >> vgút mozgatható
                        if (ves1.vgutKG_A == ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb))
                        {//A felé
                            if (ves1.valtoKG_2.Valto.Allas == true)
                            {//2E
                                if (ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).Alpha == 0)
                                {//0 fok >> -45
                                    ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).CelAllas = -45;
                                    ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).MinimumAllas = 0;
                                }
                                else
                                {//-45 fok >> 0
                                    ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).CelAllas = 0;
                                    ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).MinimumAllas = -45;
                                }
                            }
                            else
                            {//2K
                                if (ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).Alpha == 0)
                                {//0 fok >> 45
                                    ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).CelAllas = 45;
                                    ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).MinimumAllas = 0;
                                }
                                else
                                {//45 fok >> 0
                                    ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).CelAllas = 0;
                                    ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).MinimumAllas = 45;
                                }
                            }
                        }
                        else
                        {//L felé
                            if (ves1.valtoKG_1.Valto.Allas == true)
                            {//1E
                                if (ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).Alpha == 0)
                                {//0 fok >> 45
                                    ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).CelAllas = 45;
                                    ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).MinimumAllas = 0;
                                }
                                else
                                {//45 fok >> 0
                                    ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).CelAllas = 0;
                                    ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).MinimumAllas = 45;
                                }
                            }
                            else
                            {//1K
                                if (ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).Alpha == 0)
                                {//0 fok >> -45
                                    ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).CelAllas = -45;
                                    ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).MinimumAllas = 0;
                                }
                                else
                                {//-45 fok >> 0
                                    ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).CelAllas = 0;
                                    ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).MinimumAllas = -45;
                                }
                            }
                        }
                    }
                    else
                    {//jelző nincs alapban >> vgút nem mozgatható (ha a jelző nincs alapban, akkor a vgúti kapcsgomb -45 vagy 45, tehát egész (int))
                        ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).MinimumAllas = (int)ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).Alpha;
                        ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).CelAllas = (int)ves1.VgutiKG_megad(Allitas as JelzoKapcsologomb).Alpha;
                    }
                    #endregion
                    #endregion
                }
                if (Allitas is ValtoKapcsologomb)
                {
                    #region VALTOKAPCSOLOGOMB KÓDJA

                    if (Allitas.CelAllas == Allitas.MinimumAllas)
                    {

                    }
                    else if (Allitas.Alpha <= -80)
                    {
                        Allitas.Image = Kapcsologomb.RotateImage(Allitas.Img, -90, Allitas.KpY - Allitas.Location.Y);
                        Allitas.Alpha = -90;
                        label4.Text = "-90";
                    }
                    else if (Allitas.Alpha >= -10)
                    {
                        Allitas.Image = Allitas.Img;
                        Allitas.Alpha = 0;
                        label4.Text = "0";
                    }

                    #region VAGANYUTIKAPCSOLOGOMB ENGEDÉLYEZÉSE/TILTÁSA
                    if (Allitas.Alpha == 0)
                    {//E
                        //VGÚTI KG ENGEDÉLYEZÉSE
                        if (Allitas == ves1.valtoKG_1)
                        {
                            ves1.vgutKG_L.CelAllas = 45;
                            ves1.vgutKG_L.MinimumAllas = 0;
                        }
                        else if (Allitas == ves1.valtoKG_2)
                        {
                            ves1.vgutKG_A.CelAllas = -45;
                            ves1.vgutKG_A.MinimumAllas = 0;
                        }
                    }
                    else if (Allitas.Alpha == -90)
                    {//K
                        //VGÚTI KG ENGEDÉLYEZÉSE
                        if (Allitas == ves1.valtoKG_1)
                        {
                            ves1.vgutKG_L.CelAllas = -45;
                            ves1.vgutKG_L.MinimumAllas = 0;
                        }
                        else if (Allitas == ves1.valtoKG_2)
                        {
                            ves1.vgutKG_A.CelAllas = 45;
                            ves1.vgutKG_A.MinimumAllas = 0;
                        }
                    }
                    else
                    {//félállás
                        //VGÚTI KG LETILTÁSA
                        if (Allitas == ves1.valtoKG_1)
                        {
                            ves1.vgutKG_L.CelAllas = 0;
                            ves1.vgutKG_L.MinimumAllas = 0;
                        }
                        else if (Allitas == ves1.valtoKG_2)
                        {
                            ves1.vgutKG_A.CelAllas = 0;
                            ves1.vgutKG_A.MinimumAllas = 0;
                        }
                    }
                    #endregion
                    #endregion
                }
                if (Allitas is VaganyutiKapcsoloGomb)
                {
                    #region VGUTIKAPCSOLOGOMB KÓDJA
                    if (Allitas.CelAllas == 45 && Allitas.Alpha >= 35)
                    {//45-re ugrik
                        Allitas.Image = Kapcsologomb.RotateImage(Allitas.Img, 45, Allitas.KpY - Allitas.Location.Y);
                        Allitas.Alpha = 45;
                        label4.Text = "45";
                    }
                    if (Allitas.CelAllas == -45 && Allitas.Alpha <= -35)
                    {//-45-re ugrik
                        Allitas.Image = Kapcsologomb.RotateImage(Allitas.Img, -45, Allitas.KpY - Allitas.Location.Y);
                        Allitas.Alpha = -45;
                        label4.Text = "-45";
                    }
                    if (Allitas.Alpha > -10 && Allitas.Alpha < 10)
                    {//0-ra ugrik
                        Allitas.Image = Allitas.Img;
                        Allitas.Alpha = 0;
                        label4.Text = "0";
                    }

                    #region VALTOKAPCSOLOGOMB ENGEDÉLYEZÉSE/TILTÁSA
                    if (Allitas.Alpha == 0)
                    {
                        if (ves1.ValtoKG_megad(Allitas as VaganyutiKapcsoloGomb).Alpha == 0)
                        {//0
                            ves1.ValtoKG_megad(Allitas as VaganyutiKapcsoloGomb).MinimumAllas = 0;
                            ves1.ValtoKG_megad(Allitas as VaganyutiKapcsoloGomb).CelAllas = -90;
                        }
                        else
                        {//-90
                            ves1.ValtoKG_megad(Allitas as VaganyutiKapcsoloGomb).MinimumAllas = -90;
                            ves1.ValtoKG_megad(Allitas as VaganyutiKapcsoloGomb).CelAllas = 0;
                        }
                    }
                    else
                    {
                        ves1.ValtoKG_megad(Allitas as VaganyutiKapcsoloGomb).MinimumAllas = (int)ves1.ValtoKG_megad(Allitas as VaganyutiKapcsoloGomb).Alpha;
                        ves1.ValtoKG_megad(Allitas as VaganyutiKapcsoloGomb).CelAllas = (int)ves1.ValtoKG_megad(Allitas as VaganyutiKapcsoloGomb).Alpha;
                    }
                    #endregion

                    #region JELZOKAPCSOLOGOMB ENGEDÉLYEZÉSE/TILTÁSA
                    if (Allitas.Alpha == 45 || Allitas.Alpha == -45)
                    {
                        if (ves1.JelzoKG_megad(Allitas as VaganyutiKapcsoloGomb).Alpha == 0)
                        {
                            ves1.JelzoKG_megad(Allitas as VaganyutiKapcsoloGomb).MinimumAllas = -30;
                            ves1.JelzoKG_megad(Allitas as VaganyutiKapcsoloGomb).CelAllas = 30;
                        }
                    }
                    else
                    {
                        ves1.JelzoKG_megad(Allitas as VaganyutiKapcsoloGomb).MinimumAllas = 0;
                        ves1.JelzoKG_megad(Allitas as VaganyutiKapcsoloGomb).CelAllas = 0;
                    }
                    #endregion

                    //TODO: kirajzolja a csíkot
                    #endregion
                }
                Allitas = null;
            }
        }

        private void vesGomb1_Click(object sender, EventArgs e)
        {
            if (!Ind.ejjel)
            {
                ejjel.Image = Gyermekvasút.Properties.Resources.VESvisszjelVilagit;
                nappal.Image = Gyermekvasút.Properties.Resources.VESvisszjelSotet;
            }
            else
            {
                ejjel.Image = Gyermekvasút.Properties.Resources.VESvisszjelSotet;
                nappal.Image = Gyermekvasút.Properties.Resources.VESvisszjelVilagit;
            }
            Ind.ejjel = !Ind.ejjel;
        }

        private void U_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ind.U_open = false;
            Hr.Close();
        }

        private void vesGomb2_Click(object sender, EventArgs e)
        {
            if (Ind.u_valtovil)
            {
                valtovil.Image = Gyermekvasút.Properties.Resources.VESvaltovil_sotet;
            }
            else
            {
                valtovil.Image = Gyermekvasút.Properties.Resources.VESvaltovil_vilagit;
            }
            Ind.u_valtovil = !Ind.u_valtovil;
        }

        private void sr2_Click(object sender, EventArgs e)
        {

        }

        private void sr2_DoubleClick(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            if (Allomas.Valtok[0].Allas)
            {
                label5.Text = "E";
            }
            else
            {
                label5.Text = "K";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Alpha: " + jelzoKapcsologomb1.Alpha.ToString() + "\nCelAllas: " + jelzoKapcsologomb1.CelAllas.ToString() + "\nMinimumAllas: " + jelzoKapcsologomb1.MinimumAllas.ToString());
        }
                
        private void U_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hr.Hide();
            this.Hide();
            e.Cancel = true;
        }

        VES ves;
        public VES VES
        {
            get
            {
                return ves;
            }
            set
            {
                ves = value;
            }
        }

        private void U_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z && e.Modifiers == (Keys.Alt | Keys.Control))
            {
                AllomasFormAllomasLezar(label3, label6);
            }
        }
    }
}
