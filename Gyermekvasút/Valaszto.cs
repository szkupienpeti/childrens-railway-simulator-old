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
    public partial class Valaszto : Form
    {
        public Valaszto()
        {
            InitializeComponent();
        }

        public Valaszto(string formText, string labelText, ListViewItem[] listViewItems, ColumnHeader[] columnHeaders)
        {
            InitializeComponent();
            this.Text = formText;
            label1.Text = labelText;
            listView1.Columns.Clear();
            foreach (var item in columnHeaders)
            {
                item.Width = -1;
                listView1.Columns.Add(item);
            }
            foreach (var item in listViewItems)
            {
                listView1.Items.Add(item);
            }
        }

        public ListViewItem SelectedListViewItem { get; set; }

        private void Valaszto_Load(object sender, EventArgs e)
        {

        }

        private bool closable = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Válassz egy menetrendet!", "Menetrend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (listView1.SelectedItems.Count > 1)
            {
                MessageBox.Show("Csak egy menetrendet válassz!", "Menetrend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                closable = true;
                this.Close();
            }
        }

        private void Valaszto_FormClosed(object sender, FormClosedEventArgs e)
        {
            SelectedListViewItem = listView1.SelectedItems[0];
        }

        private void Valaszto_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !closable;
        }
    }
}
