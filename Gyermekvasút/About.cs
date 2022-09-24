using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Gyermekvasút
{
    partial class About : Form
    {
        public static bool IsShown { get; set; }

        public About()
        {
            InitializeComponent();
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void About_Load(object sender, EventArgs e)
        {
            IsShown = true;

            if (Program.MenetrendKeszitok.Length == 0)
            {
                menetrend.Text = "Ez itt a menetrend készítőinek a neve, de jelenleg sajnos nicns menetrend betöltve.";
            }
            else if (Program.MenetrendKeszitok.Length == 1)
            {
                menetrend.Text = "A betöltött menetrendet " + Program.MenetrendKeszitok[0] + " készítette.";
            }
            else if (Program.MenetrendKeszitok.Length > 1)
            {
                menetrend.Text = "A betöltött menetrendet " + Program.MenetrendKeszitok[0] + " készítette, majd " + Program.MenetrendKeszitok[1] + " digitalizálta.";
            }
            else
            {
                menetrend.Text = "Itt a menetrend készítőinek kellene megjelenniük, de ez valami belső hiba miatt nem sikerült.";
            }
        }

        private void About_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsShown = false;
        }
    }
}
