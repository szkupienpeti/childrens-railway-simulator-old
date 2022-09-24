using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gyermekvasút.Modellek;

namespace Gyermekvasút
{
    public partial class Hr : Form
    {
        bool fel;
        List<Szakasz> szakaszok = new List<Szakasz>();
        List<VonatszamKijelzo> vszk = new List<VonatszamKijelzo>();
        bool frissitheto = false;
        public bool Frissitheto
        {
            get { return frissitheto; }
        }
        bool kezdopontBalra;

        Allomas Allomas;

        public Hr(bool felfeleNezoValtok, bool kezdopontBalraBEMENET, Allomas allomas)
        {
            InitializeComponent();
            if (allomas.Name == "Széchenyi-hegy")
            {
                panel1.BackgroundImage = Gyermekvasút.Properties.Resources.HR_A;
            }
            else if (felfeleNezoValtok)
            {
                panel1.BackgroundImage = Gyermekvasút.Properties.Resources.HR_fel;
            }
            else
            {
                panel1.BackgroundImage = Gyermekvasút.Properties.Resources.HR_le;
            }
            fel = felfeleNezoValtok;

            kezdopontBalra = kezdopontBalraBEMENET;
            Allomas = allomas;
        }

        private void Hr_Load(object sender, EventArgs e)
        {
            szakaszok = Allomas.Szakaszok;
            this.Text = Allomas.Name + " – Foglaltságok";

            VonatszamKijelzo vszk1 = null;
            if (Allomas.Name != "Széchenyi-hegy")
            {
                vszk1 = new VonatszamKijelzo(szakaszok[0]);
                vszk.Add(vszk1);
            }
            VonatszamKijelzo vszk2 = new VonatszamKijelzo(szakaszok[1]);
            vszk.Add(vszk2);
            VonatszamKijelzo vszk3 = new VonatszamKijelzo(szakaszok[2]);
            vszk.Add(vszk3);
            VonatszamKijelzo vszk4 = new VonatszamKijelzo(szakaszok[3]);
            vszk.Add(vszk4);
            VonatszamKijelzo vszk5 = new VonatszamKijelzo(szakaszok[4]);
            vszk.Add(vszk5);
            VonatszamKijelzo vszk6 = new VonatszamKijelzo(szakaszok[5]);
            vszk.Add(vszk6);
            VonatszamKijelzo vszk7 = new VonatszamKijelzo(szakaszok[6]);
            vszk.Add(vszk7);
            VonatszamKijelzo vszk8 = new VonatszamKijelzo(szakaszok[7]);
            vszk.Add(vszk8);
            VonatszamKijelzo vszk9 = new VonatszamKijelzo(szakaszok[8]);
            vszk.Add(vszk9);
            VonatszamKijelzo vszk10 = new VonatszamKijelzo(szakaszok[9]);
            vszk.Add(vszk10);

            if (!kezdopontBalra)
            {
                vszk1.Szakasz = szakaszok[6];
                vszk2.Szakasz = szakaszok[5];
                vszk3.Szakasz = szakaszok[4];
                vszk4.Szakasz = szakaszok[3];
                vszk5.Szakasz = szakaszok[2];
                vszk6.Szakasz = szakaszok[1];
                vszk7.Szakasz = szakaszok[0];
                vszk8.Szakasz = szakaszok[9];
                vszk9.Szakasz = szakaszok[8];
                vszk10.Szakasz = szakaszok[7];
            }

            for (int i = 0; i < vszk.Count; i++)
            {
                this.panel1.Controls.Add(vszk[i]);
            }

            for (int i = 0; i < vszk.Count; i++)
            {
                vszk[i].Parent = this.panel1;
                vszk[i].Size = new Size(25, 13);
            }

            if (Allomas.Name == "Széchenyi-hegy")
            {
                //vszk1 == null
                #region OLD
                //azóta tükrözve lett mindkét tengelyre)
                //vszk2.Location = new Point(42, 47);
                //vszk3.Location = new Point(153, 12);
                //vszk4.Location = new Point(258, 12);
                //vszk5.Location = new Point(363, 12);
                //vszk6.Location = new Point(477, 70);
                //vszk7.Location = new Point(569, 70);
                //vszk8.Location = new Point(153, 47);
                //vszk9.Location = new Point(258, 47);
                //vszk10.Location = new Point(363, 47);
                #endregion

                vszk2.Location = new Point(533, 40);
                vszk3.Location = new Point(422, 75);
                vszk4.Location = new Point(317, 75);
                vszk5.Location = new Point(212, 75);
                vszk6.Location = new Point(98, 17);
                vszk7.Location = new Point(6, 17);
                vszk8.Location = new Point(422, 40);
                vszk9.Location = new Point(317, 40);
                vszk10.Location = new Point(212, 40);
            }
            else if (fel)
            {
                vszk1.Location = new Point(7, 61);
                vszk2.Location = new Point(78, 61);
                vszk3.Location = new Point(186, 61);
                vszk4.Location = new Point(289, 61);
                vszk5.Location = new Point(392, 61);
                vszk6.Location = new Point(498, 61);
                vszk7.Location = new Point(568, 61);
                vszk8.Location = new Point(186, 26);
                vszk9.Location = new Point(289, 26);
                vszk10.Location = new Point(392, 26);
            }
            else
            {
                vszk1.Location = new Point(7, 26);
                vszk2.Location = new Point(78, 26);
                vszk3.Location = new Point(186, 26);
                vszk4.Location = new Point(289, 26);
                vszk5.Location = new Point(392, 26);
                vszk6.Location = new Point(498, 26);
                vszk7.Location = new Point(568, 26);
                vszk8.Location = new Point(186, 61);
                vszk9.Location = new Point(289, 61);
                vszk10.Location = new Point(392, 61);
            }
            if (vszk1 != null)
            {
                vszk1.BackColor = Color.Yellow;
                vszk1.ForeColor = Color.Black;
            }
            vszk7.BackColor = Color.Yellow;
            vszk7.ForeColor = Color.Black;

            frissitheto = true;
        }

        public void vszkRefresh()
        {
            for (int i = 0; i < vszk.Count; i++)
            {
                if (vszk[i].Szakasz.Foglalt == false)
                {
                    vszk[i].Text = "";
                }
                else
                {
                    if (vszk[i].Szakasz.Vonat.Paros == kezdopontBalra) //merre megy a vonat a formon (balra vagy jobbra) esetleges nyilakhoz kellhet
                    {// >>>
                        if (vszk[i].Szakasz.Vonat.Koruljar)
                        {
                            vszk[i].Text = "------";
                        }
                        else
                        {
                            vszk[i].Text = vszk[i].Szakasz.Vonat.Vonatszam;
                        }
                    }
                    else
                    {// <<<
                        if (vszk[i].Szakasz.Vonat.Koruljar)
                        {
                            vszk[i].Text = "------";
                        }
                        else
                        {
                            vszk[i].Text = vszk[i].Szakasz.Vonat.Vonatszam;
                        }
                    }                    
                }
            }

            if (vszk[0].Szakasz is Allomaskoz)
            {
                if (vszk[0].Szakasz.Vonat != null && vszk[0].Szakasz.Vonat.BejaratiJelzoElottVarakozik)
                {
                    vszk[0].ForeColor = Color.Red;
                }
                else
                {
                    vszk[0].ForeColor = Color.Black;
                }
            }
            if (vszk[6].Szakasz is Allomaskoz)
            {
                if (vszk[6].Szakasz.Vonat != null && vszk[6].Szakasz.Vonat.BejaratiJelzoElottVarakozik)
                {
                    vszk[6].ForeColor = Color.Red;
                }
                else
                {
                    vszk[6].ForeColor = Color.Black;
                }
            }
            if (vszk[5].Szakasz is Allomaskoz)
            {
                if (vszk[5].Szakasz.Vonat != null && vszk[5].Szakasz.Vonat.BejaratiJelzoElottVarakozik)
                {
                    vszk[5].ForeColor = Color.Red;
                }
                else
                {
                    vszk[5].ForeColor = Color.Black;
                }
            }
        }

        private void Hr_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }

    public class VonatszamKijelzo : Label
    {
        public VonatszamKijelzo() { }

        public VonatszamKijelzo(Szakasz szakaszBE)
        {
            szakasz = szakaszBE;
            BackColor = Color.Black;
            ForeColor = Color.Yellow;
            AutoSize = true;
        }

        Szakasz szakasz;
        public Szakasz Szakasz
        {
            get { return szakasz; }
            set { szakasz = value; }
        }

        /*public override string Text
        {
            get
            {
                if (Szakasz.Foglalt == true)
                {
                    return Szakasz.Vonat.Vonatszam;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                base.Text = value;
            }
        }*/
    }
}
