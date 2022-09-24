using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút.DEBUG
{
    public partial class Log : Form
    {
        public static Log LogInstance = null;
        public static string LogText = "";
        public static void AddLogText(string add)
        {
            LogText += DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ": " +  add + "\r\n";
            if (LogInstance != null)
            {
                try
                {
                    LogInstance.Close();
                }
                catch (Exception) { }
            }
            LogInstance = new Log();
            LogInstance.textBox1.Text = "GYVSZIM Runtime Log\r\n© Szkupien Péter 2015. - Minden jog fenntartva!\r\n=== FUTÁSIDEJŰ ADATOK ===\r\n\r\n" + LogText + "\r\n\r\n----------\r\nThis form is associated with Project Cabral.";
        }

        public Log()
        {
            InitializeComponent();
            if (LogInstance != null)
            {
                LogInstance.Close();
            }
            LogInstance = this;
        }
    }
}
