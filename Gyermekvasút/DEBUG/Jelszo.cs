using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút
{
    public partial class Jelszo : Form
    {
        bool indexFormClosing = false;

        public Jelszo()
        {
            InitializeComponent();
        }

        string btnText = "";
        public Jelszo(string formText, string buttonText)
        {
            InitializeComponent();
            this.Text = formText;
            this.btnText = buttonText;
        }

        public Jelszo(bool indexFormClosing)
        {
            InitializeComponent();
            this.indexFormClosing = indexFormClosing;
        }

        private void Jelszo_Load(object sender, EventArgs e)
        {
            if (indexFormClosing)
            {
                this.Text = "Bezárás megerősítése";
                button1.Text = "Bezárás megerősítése";
            }
            if (btnText != "")
            {
                button1.Text = btnText;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "nincs")
            {
                DialogResult = DialogResult.OK;
            }
            else if (maskedTextBox1.Text == "ga" || maskedTextBox1.Text == "GA")
            {
                DialogResult = DialogResult.Yes;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void Jelszo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }
    }
}
