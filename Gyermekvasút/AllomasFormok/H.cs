using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gyermekvasút.Modellek;
using Gyermekvasút.Modellek.Emeltyűtípusok;

namespace Gyermekvasút.AllomasFormok
{
    public partial class H : AllomasForm
    {
        public RET1 RET1 { get; set; }

        public H(Index index, Allomas allomas)
        {
            this.Allomas = allomas;
            this.Ind = index;
            InitializeComponent();
            RET1 = RET;
            RET.Ind = this.Ind;

            Allomas.ValtKezek[1].Progbar = o_vgut;
            Allomas.ValtKezek[0].Progbar = s_vgut;

            o_vgut.Value = Allomas.ValtKezek[1].ProgressBar;
            s_vgut.Value = Allomas.ValtKezek[0].ProgressBar;

            RET.SZAKASZOK = Allomas.Szakaszok;
            RET.VK1 = Allomas.ValtKezek[1];
            RET.VK2 = Allomas.ValtKezek[0];
            RET.ALLOMAS = Allomas;

            RET.OK1 = OK1;
            RET.OK2 = OK2;

            OK1.Valto = Allomas.Valtok[1];
            OK2.Valto = Allomas.Valtok[0];
        }

        public H()
        {
            InitializeComponent();
        }

        private void H_Load(object sender, EventArgs e)
        {            
            Ind.H_open = true;
                        
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

        private void H_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ind.H_open = false;
            Hr.Close();
        }

        public override void Frissit()
        {
            base.Frissit();

            #region VÁGÁNYÚTOLDÁS (KIJÁRAT)
            if (Allomas.ValtKezek[1].Ki != null)
            {
                if (Allomas.ValtKezek[1].Vagany == 1 && !Allomas.Szakaszok[9].Foglalt && Allomas.Szakaszok[5].Foglalt && Allomas.Szakaszok[5].Vonat.Paros)
                {
                    OK1.Start2(false);
                    OK1.Elinditva = true;
                }
                if (Allomas.ValtKezek[1].Vagany == 2 && !Allomas.Szakaszok[9].Foglalt && Allomas.Szakaszok[5].Foglalt && Allomas.Szakaszok[5].Vonat.Paros)
                {
                    OK1.Start2(false);
                    OK1.Elinditva = true;
                }
            }
            if (Allomas.ValtKezek[0].Ki != null)
            {
                if (Allomas.ValtKezek[0].Vagany == 1 && !Allomas.Szakaszok[7].Foglalt && Allomas.Szakaszok[1].Foglalt && !Allomas.Szakaszok[1].Vonat.Paros)
                {
                    OK2.Start2(false);
                    OK2.Elinditva = true;
                }
                if (Allomas.ValtKezek[0].Vagany == 2 && !Allomas.Szakaszok[2].Foglalt && Allomas.Szakaszok[1].Foglalt && !Allomas.Szakaszok[1].Vonat.Paros)
                {
                    OK2.Start2(false);
                    OK2.Elinditva = true;
                }
            }

            if (Allomas.Valtok[0].Lezart == false)
            {//2
                OK2.Stop();
                OK2.Elinditva = false;
            }
            if (Allomas.Valtok[1].Lezart == false)
            {//1
                OK1.Stop();
                OK1.Elinditva = false;
            }
            #endregion
        }

        private void pictureBox3_Click(object sender, EventArgs e)//NAPL
        {
            Naplozo napl = new Naplozo(Allomas, true, true, Ind);
            if (napl.ShowDialog() == DialogResult.OK)
            {
                Frissit();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)//VK2
        {
            if (Allomas.ValtKezek[0].Ki != null)
            {
                //vagy kijárat vagy ellenvonatos kijárat van >> nem nyílik meg, hanem tájékoztató MB
                MessageBox.Show("A kiválasztott váltókezelő (2) jelenleg nem rendelhető el egyetlen vágányút beállítására sem.\nAz ok a következő lehet:\nA váltókezelő már beállított vagy éppen beállít egy kijárati vágányutat.\nA következő vágányút csak a kijáró vonat leközlekedése (a vágányút feloldása) után rendelhető el.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Ind.Hibak[0]++;
            }
            else
            {
                if (Allomas.Valtok[0].Lezart && Allomas.ValtKezek[0].Be == null)
                {
                    MessageBox.Show("A kiválasztott váltókezelő (2) jelenleg nem rendelhető el egyetlen vágányút beállítására sem.\nAz ok a következő lehet:\nA váltókezelőhöz tartozó váltó le van zárva. Így csak ellenvonatos\nkijárati vágányút rendelhető el, azonban bejárati vágányutat még\nnem rendeltél el.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Ind.Hibak[1]++;
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
        }

        private void pictureBox4_Click(object sender, EventArgs e)//VK1
        {
            if (Allomas.ValtKezek[1].Ki != null)
            {
                //vagy kijárat vagy ellenvonatos kijárat van >> nem nyílik meg, hanem tájékoztató MB
                MessageBox.Show("A kiválasztott váltókezelő (1) jelenleg nem rendelhető el egyetlen vágányút beállítására sem.\nAz ok a következő lehet:\nA váltókezelő már beállított vagy éppen beállít egy kijárati vágányutat.\nA következő vágányút csak a kijáró vonat leközlekedése (a vágányút feloldása) után rendelhető el.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Ind.Hibak[0]++;
            }
            else
            {
                if (Allomas.Valtok[1].Lezart && Allomas.ValtKezek[1].Be == null)
                {
                    MessageBox.Show("A kiválasztott váltókezelő (1) jelenleg nem rendelhető el egyetlen vágányút beállítására sem.\nAz ok a következő lehet:\nA váltókezelőhöz tartozó váltó le van zárva. Így csak ellenvonatos\nkijárati vágányút rendelhető el, azonban bejárati vágányutat még\nnem rendeltél el.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Ind.Hibak[1]++;
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
        }

        #region RETESZ HELP
        private void R2_MouseEnter(object sender, EventArgs e)
        {
            ret2pb.BackColor = SystemColors.Info;
            ret2label.BackColor = SystemColors.Info;
            ret2label.Text = "A reteszemeltyű használata\nHárs-hegy állomáson:\n* BAL kattintás:\n  kicsappantás, átállítás\n* JOBB kattintás:\n  üres állásban mozgatás";
        }

        private void R2_MouseLeave(object sender, EventArgs e)
        {
            ret2pb.BackColor = SystemColors.Control;
            ret2label.Text = "";
            ret2label.BackColor = SystemColors.Control;
        }

        private void R1_MouseEnter(object sender, EventArgs e)
        {
            ret1pb.BackColor = SystemColors.Info;
            ret1label.BackColor = SystemColors.Info;
            ret1label.Text = "A reteszemeltyű használata\nHárs-hegy állomáson:\n* BAL kattintás:\n  kicsappantás, átállítás\n* JOBB kattintás:\n  üres állásban mozgatás";
        }

        private void R1_MouseLeave(object sender, EventArgs e)
        {
            ret1pb.BackColor = SystemColors.Control;
            ret1label.Text = "";
            ret1label.BackColor = SystemColors.Control;
        }
        #endregion

        #region OldasKenyszerito
        private void OK2_Tick(object sender, EventArgs e)
        {
            if (OK2.Bejarat)
            {
                MessageBox.Show("Az A jelű bejárati jelzőt már visszaállítottad Megállj-állásba, ám a 2-es váltót nem oldottad még fel. A váltó szabványos állása az oldott egyenes. A váltókat vonatmentes időben szabványos állásban kell tartani.\nNe feledkezz meg a váltó feloldásáról!\nAmíg nem oldod fel a váltót, a váltókezelőt csak ellenvonatos kijárati vágányút beállítására fogod tudni elrendelni!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Ind.Hibak[2]++;
            }
            else
            {
                MessageBox.Show("A vonat már leközlekedett a vágányúton, ám a 2-es váltót nem oldottad még fel. A váltó szabványos állása az oldott egyenes. A váltókat vonatmentes időben szabványos állásban kell tartani.\nNe feledkezz meg a váltó feloldásáról!\nAmíg nem oldod fel a váltót, a váltókezelőt nem fogod tudni elrendelni semmilyen vágányút beállítására!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Ind.Hibak[2]++;
            }
        }

        private void OK1_Tick(object sender, EventArgs e)
        {
            if (OK1.Bejarat)
            {
                MessageBox.Show("A B jelű bejárati jelzőt már visszaállítottad Megállj-állásba, ám az 1-es váltót nem oldottad még fel. A váltó szabványos állása az oldott egyenes. A váltókat vonatmentes időben szabványos állásban kell tartani.\nNe feledkezz meg a váltó feloldásáról!\nAmíg nem oldod fel a váltót, a váltókezelőt csak ellenvonatos kijárati vágányút beállítására fogod tudni elrendelni!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Ind.Hibak[2]++;
            }
            else
            {
                MessageBox.Show("A vonat már leközlekedett a vágányúton, ám az 1-es váltót nem oldottad még fel. A váltó szabványos állása az oldott egyenes. A váltókat vonatmentes időben szabványos állásban kell tartani.\nNe feledkezz meg a váltó feloldásáról!\nAmíg nem oldod fel a váltót, a váltókezelőt nem fogod tudni elrendelni semmilyen vágányút beállítására!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Ind.Hibak[2]++;
            }
        }
        #endregion

        #region DEBUG
        private void button1_Click(object sender, EventArgs e)
        {
            Allomas.AddVonat(new Vonat(new string[]{"217"}, Allomas.Szakaszok[6], Allomas));
            Frissit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Allomas.AddVonat(new Vonat(new string[] { "112", "113", "114" }, Allomas.Szakaszok[0], Allomas));
            Frissit();
        }
        #endregion

        private void H_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hr.Hide();
            this.Hide();
            e.Cancel = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Allomas.AddVonat(new Vonat(new string[] { "112", "113", "114" }, Allomas.Szakaszok[3], Allomas));
            Frissit();
        }

        private void H_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z && e.Modifiers == (Keys.Alt | Keys.Control))
            {
                AllomasFormAllomasLezar(label3, label6);
            }
        }
    }
}
