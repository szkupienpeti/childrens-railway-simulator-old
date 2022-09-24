using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Gyermekvasút.DEBUG
{
    public partial class Konzol : Form
    {
        object o;
        Index ind;

        public Konzol(object obj, Index index)
        {
            InitializeComponent();
            o = obj;
            label3.Text += o.ToString() + " (" + o.GetType().ToString() + ")";
            ind = index;
        }

        private void Konzol_Load(object sender, EventArgs e)
        {
            ind.Konzol_open = true;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            bool done = false;
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text == "")
                {
                    return;
                }
                else if (textBox1.Text.ToLower() == "help")
                {
                    label2.Text += ("Ebben a konzolban futásidőben lehet objektumok metódusait név alapján meghívni.\nMetódusnak paramétert (még) nem tudsz beadni, a visszatérést ToString()-elve írja ki a konzol.\nHa void a visszatérés, elkap egy NullReferenceException-t (null.ToString())." + "\n<KIMENET>");
                    textBox1.Text = "";
                    return;
                }
                else if (textBox1.Text.ToLower() == "ga")
                {
                    label2.Text += (":)" + "\n<KIMENET>");
                    textBox1.Text = "";
                    return;
                }
                try
                {
                    o.GetType().GetMethod(textBox1.Text).Invoke(o, null);
                    done = true;
                    label2.Text += (o.GetType().GetMethod(textBox1.Text).Invoke(o, null).ToString() + "\n<KIMENET>");
                    textBox1.Text = "";
                }
                catch (NullReferenceException ex)
                {
                    if (done)
                    {
                        label2.Text += ("A metódust (" + textBox1.Text + ") sikeresen meghívtuk, de vagy nem tért vissza semmivel\n(void) vagy valami baj mégis volt, mert egy NullRefEx-t elkaptunk a kiíratáskor." + "\n<KIMENET>");
                        textBox1.Text = "";
                        if (showmb.Checked) ShowExDetMb(ex);
                    }
                    else
                    {
                        label2.Text += ("HIBA: ismeretlen parancs (NullReferenceException): " + textBox1.Text + "\n<KIMENET>");
                        textBox1.Text = "";
                        if (showmb.Checked) ShowExDetMb(ex);
                    }
                }
                catch (TargetException ex)
                {
                    label2.Text += ("HIBA: ismeretlen parancs (TargetException): " + textBox1.Text + "\n<KIMENET>");
                    textBox1.Text = "";
                    if (showmb.Checked) ShowExDetMb(ex);
                }
                catch (TargetParameterCountException ex)
                {
                    label2.Text += ("HIBA: helytelen argumentumok (TargetParameterCountException): " + textBox1.Text + "\n<KIMENET>");
                    textBox1.Text = "";
                    if (showmb.Checked) ShowExDetMb(ex);
                }
                catch (TargetInvocationException ex)
                {
                    label2.Text += ("HIBA: a metódus meghívása nem kezelt kivételt váltott ki (TargetInvocationException): " + textBox1.Text + "\n<KIMENET>");
                    textBox1.Text = "";
                    if (showmb.Checked) ShowExDetMb(ex);
                }
                catch (Exception ex)
                {
                    label2.Text += ("HIBA: ismeretlen eredetű hiba (" + ex.GetType().ToString() + "): " + textBox1.Text + "\n<KIMENET>");
                    textBox1.Text = "";
                    if (showmb.Checked) ShowExDetMb(ex);
                }
                //scroll to bottom
                using (Control c = new Control() { Parent = panel1, Dock = DockStyle.Bottom })
                {
                    panel1.ScrollControlIntoView(c);
                    c.Parent = null;
                }
            }
        }

        void ShowExDetMb(Exception ex)
        {
            MessageBox.Show("Details of caught exception\n\n*GetType: " + ex.GetType().ToString() + "\n*ToString: " + ex.ToString() + "\n\nInnerEx:\n" + ex.InnerException, "Exception details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Konzol_FormClosed(object sender, FormClosedEventArgs e)
        {
            ind.Konzol_open = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (!(o is AzonosithatoObj))
            {
                MessageBox.Show("Konzol.o (System.Object) is NOT AzonosithatoObj\n\nTip: A Vonat osztálynak nem őse az AzonosithatoObj osztály. Meghívható metódusok: Elindul, VonatszamCsere");
                return;
            }
            string methodString = "";
            for (int i = 0; i < (o as AzonosithatoObj).Methods.Count; i++)
            {
                methodString += (o as AzonosithatoObj).Methods[i] + "\n";
            }
            MessageBox.Show(methodString, "Metódusok", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
