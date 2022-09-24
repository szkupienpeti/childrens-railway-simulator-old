using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gyermekvasút.Modellek;

namespace Gyermekvasút.AllomasFormok
{
    public partial class L : AllomasForm, IViragvolgy
    {
        public L(Index index, Allomas allomas)
        {
            InitializeComponent();
            Ind = index;
            Allomas = allomas;

            EM.VZK1 = vzk1;
            EM.VZK2 = vzk2;

            EmProp = EM;

            EM.Ind = this.Ind;

            EM.SZAKASZOK = Allomas.Szakaszok;
            EM.VK1 = Allomas.ValtKezek[1];
            EM.VK2 = Allomas.ValtKezek[0];
            EM.ALLOMAS = Allomas;

            Allomas.ValtKezek[1].Progbar = i_vgut;
            Allomas.ValtKezek[0].Progbar = u_vgut;

            i_vgut.Value = Allomas.ValtKezek[1].ProgressBar;
            u_vgut.Value = Allomas.ValtKezek[0].ProgressBar;
        }

        public L()
        {
            InitializeComponent();
        }

        private void L_Load(object sender, EventArgs e)
        {
            Ind.L_open = true;
            
            if (Hr == null)
            {
                Hr = new Hr(true, true, Allomas);
            }

            if (Hr.Frissitheto)
            {
                Hr.vszkRefresh();
            }
            Frissit();
        }

        private void L_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ind.L_open = false;
            Hr.Close();
        }

        public override void Frissit()
        {
            base.Frissit();

            #region VÁLTÓZÁRKULCSOK MEGJELENÍTÉSE
            if (Allomas.Valtok[1].Lezart)
            {
                if (Allomas.Valtok[1].Allas)
                {
                    vzk1.Image = Gyermekvasút.Properties.Resources.vzk1E;
                }
                else
                {
                    vzk1.Image = Gyermekvasút.Properties.Resources.vzk1K;
                }
            }
            else
            {
                vzk1.Image = Gyermekvasút.Properties.Resources.vzkAlap;
            }

            if (Allomas.Valtok[0].Lezart)
            {
                if (Allomas.Valtok[0].Allas)
                {
                    vzk2.Image = Gyermekvasút.Properties.Resources.vzk2E;
                }
                else
                {
                    vzk2.Image = Gyermekvasút.Properties.Resources.vzk2K;
                }
            }
            else
            {
                vzk2.Image = Gyermekvasút.Properties.Resources.vzkAlap;
            }
            #endregion

            #region VÁGÁNYÚTOLDÁS
            if (Allomas.ValtKezek[1].Ki != null)
            {
                if (Allomas.ValtKezek[1].Vagany == 1 && !Allomas.Szakaszok[4].Foglalt && Allomas.Szakaszok[5].Foglalt && Allomas.Szakaszok[5].Vonat.Paros)
                {
                    Allomas.ValtKezek[1].ProgressBar = 0;
                    Allomas.ValtKezek[1].Be = null;
                    Allomas.ValtKezek[1].Ki = null;
                    Allomas.ValtKezek[1].Vagany = 0;
                    Allomas.ValtKezek[1].Valto.Lezart = false;
                    Frissit();
                }
                if (Allomas.ValtKezek[1].Vagany == 2 && !Allomas.Szakaszok[9].Foglalt && Allomas.Szakaszok[5].Foglalt && Allomas.Szakaszok[5].Vonat.Paros)
                {
                    Allomas.ValtKezek[1].ProgressBar = 0;
                    Allomas.ValtKezek[1].Be = null;
                    Allomas.ValtKezek[1].Ki = null;
                    Allomas.ValtKezek[1].Vagany = 0;
                    Allomas.ValtKezek[1].Valto.Lezart = false;
                    Allomas.ValtKezek[1].Valto.Allas = true;
                    Frissit();
                }
            }
            if (Allomas.ValtKezek[0].Ki != null)
            {
                if (Allomas.ValtKezek[0].Vagany == 1 && !Allomas.Szakaszok[2].Foglalt && Allomas.Szakaszok[1].Foglalt && !Allomas.Szakaszok[1].Vonat.Paros)
                {
                    Allomas.ValtKezek[0].ProgressBar = 0;
                    Allomas.ValtKezek[0].Be = null;
                    Allomas.ValtKezek[0].Ki = null;
                    Allomas.ValtKezek[0].Vagany = 0;
                    Allomas.ValtKezek[0].Valto.Lezart = false;
                    Frissit();
                }
                if (Allomas.ValtKezek[0].Vagany == 2 && !Allomas.Szakaszok[7].Foglalt && Allomas.Szakaszok[1].Foglalt && !Allomas.Szakaszok[1].Vonat.Paros)
                {
                    Allomas.ValtKezek[0].ProgressBar = 0;
                    Allomas.ValtKezek[0].Be = null;
                    Allomas.ValtKezek[0].Ki = null;
                    Allomas.ValtKezek[0].Vagany = 0;
                    Allomas.ValtKezek[0].Valto.Lezart = false;
                    Allomas.ValtKezek[0].Valto.Allas = true;
                    Frissit();
                }
            }
            #endregion
        }

        private void pictureBox3_Click(object sender, EventArgs e)//napl
        {
            Naplozo napl = new Naplozo(Allomas, true, true, Ind);
            if (napl.ShowDialog() == DialogResult.OK)
            {
                Frissit();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Allomas.ValtKezek[0].Ki != null)
            {
                //vagy kijárat vagy ellenvonatos kijárat van >> nem nyílik meg, hanem tájékoztató MB
                MessageBox.Show("A kiválasztott váltókezelő (1) jelenleg nem rendelhető el egyetlen vágányút beállítására sem.\nAz ok a következő lehet:\nA váltókezelő már beállított vagy éppen beállít egy kijárati vágányutat.\nA következő vágányút csak a kijáró vonat leközlekedése (a vágányút feloldása) után rendelhető el.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Ind.Hibak[0]++;
            }
            else
            {
                Valtokezelo vk2 = new Valtokezelo(Allomas, false, Allomas.Szakaszok, Allomas.ValtKezek[0], Allomas.ValtKezek[1]);
                if (vk2.ShowDialog() == DialogResult.OK)
                {
                    Frissit();
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (Allomas.ValtKezek[1].Ki != null)
            {
                //vagy kijárat vagy ellenvonatos kijárat van >> nem nyílik meg, hanem tájékoztató MB
                MessageBox.Show("A kiválasztott váltókezelő (1) jelenleg nem rendelhető el egyetlen vágányút beállítására sem.\nAz ok a következő lehet:\nA váltókezelő már beállított vagy éppen beállít egy kijárati vágányutat.\nA következő vágányút csak a kijáró vonat leközlekedése (a vágányút feloldása) után rendelhető el.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Ind.Hibak[0]++;
            }
            else
            {
                Valtokezelo vk1 = new Valtokezelo(Allomas, true, Allomas.Szakaszok, Allomas.ValtKezek[1], Allomas.ValtKezek[0]);
                if (vk1.ShowDialog() == DialogResult.OK)
                {
                    Frissit();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Allomas.AddVonat(new Vonat(new string[]{"122", "123", "124"}, Allomas.Szakaszok[3], Allomas));
            Frissit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Allomas.AddVonat(new Vonat(new string[]{"132", "133", "134"}, Allomas.Szakaszok[0], Allomas));
            Frissit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Allomas.AddVonat(new Vonat(new string[]{"217"}, Allomas.Szakaszok[6], Allomas));
            Frissit();
        }

        public Modellek.Emeltyűtípusok.EM EmProp { get; set; }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void L_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hr.Hide();
            this.Hide();
            e.Cancel = true;
        }

        private void L_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z && e.Modifiers == (Keys.Alt | Keys.Control))
            {
                AllomasFormAllomasLezar(label3, label6);
            }
        }
    }
}