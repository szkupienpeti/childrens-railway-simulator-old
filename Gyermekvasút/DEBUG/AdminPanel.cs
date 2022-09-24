using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gyermekvasút.DEBUG;
using Gyermekvasút.Modellek;

namespace Gyermekvasút
{
    public partial class AdminPanel : Form
    {
        Index ind;
        bool halozatos;
        //Allomas allomasObjektum;
        public AdminPanel(Index index, bool halozatosE, Allomas allomas = null)
        {
            InitializeComponent();
            ind = index;
            halozatos = halozatosE;
            //allomasObjektum = allomas;
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            label1.Text += Gyermekvasút.Modellek.Settings.SebessegOszto;
            formok.DisplayMember = "Text";
            foreach (AllomasForm frm in ind.AllomasFormPeldanyok)
            {
                if (frm != null)
                    formok.Items.Add(frm);
            }

            ind.Admin_open = true;
            allomas.DisplayMember = "Name";
            if (halozatos)
            {
                Allomas all = null;
                for (int i = 0; i < ind.Allomasok.Count; i++)
                {
                    if (ind.Allomasok[i] != null)
                    {
                        all = ind.Allomasok[i];
                    }
                }
                allomas.Items.Add(all);
                allomas.SelectedItem = all;
                allomas.Enabled = false;
            }
            else
            {
                for (int i = 0; i < ind.Allomasok.Count; i++)
                {
                    allomas.Items.Add(ind.Allomasok[i]);
                }
            }
        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = allomas.SelectedItem;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            objektum.SelectedIndex = -1;
            objektum.Items.Clear();
            propertyGrid1.SelectedObject = allomas.SelectedItem;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            objektum.Items.Clear();
            Allomas Allomas = allomas.SelectedItem as Allomas;
            if (Allomas == null)
            {
                return;
            }

            if (checkBox1.Checked)
            {
                for (int i = 0; i < Allomas.Vonatok.Count; i++)
                {
                    objektum.Items.Add(Allomas.Vonatok[i]);
                }
            }

            if (checkBox2.Checked)
            {
                for (int i = 0; i < Allomas.Valtok.Count; i++)
                {
                    objektum.Items.Add(Allomas.Valtok[i]);
                }
            }

            if (checkBox3.Checked)
            {
                for (int i = 0; i < Allomas.Vaganyutak.Count; i++)
                {
                    objektum.Items.Add(Allomas.Vaganyutak[i]);
                }
            }

            if (checkBox4.Checked)
            {
                for (int i = 0; i < Allomas.Jelzok.Count; i++)
                {
                    objektum.Items.Add(Allomas.Jelzok[i]);
                }
            }

            if (checkBox5.Checked)
            {
                for (int i = 0; i < Allomas.Szakaszok.Count; i++)
                {
                    if (Allomas.Szakaszok[i] != null)
                    {
                        objektum.Items.Add(Allomas.Szakaszok[i]);
                    }
                }
            }

            if (checkBox6.Checked)
            {
                for (int i = 0; i < Allomas.ValtKezek.Count; i++)
                {
                    objektum.Items.Add(Allomas.ValtKezek[i]);
                }
            }
        }

        private void AdminPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            ind.Admin_open = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object atadando = null;
            if (objektum.SelectedIndex == -1)
            {
                if (allomas.SelectedIndex != -1)
                {
                    MessageBox.Show("Allomas objektum van kiválasztva!\nNem hívható meg metódus!");
                    return;
                }
            }
            else
            {
                atadando = objektum.SelectedItem;
            }

            if (!ind.Konzol_open && atadando != null)
            {
                Konzol k = new Konzol(atadando, ind);
                k.Show();
            }
            else
            {
                MessageBox.Show("Vagy van már nyitott konzol vagy nincs kiválasztva objektum");
            }
        }

        private void objektum_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = objektum.SelectedItem;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("HC fícsör, hamarost!", "Türelem!44!!!négyNÉGY!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = ind.AllomasFormPeldanyok[2];
        }

        private void formok_SelectedIndexChanged(object sender, EventArgs e)
        {
            formPropertyGrid.SelectedObject = formok.SelectedItem;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            propertyGrid1.Refresh();
            formPropertyGrid.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Log.LogInstance == null)
            {
                MessageBox.Show("Log.LogInstance (public static Log) == null");
            }
            else
            {
                Log.LogInstance.Show();
            }
        }
    }
}
