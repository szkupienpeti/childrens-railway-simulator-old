using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút
{
    public partial class StartScreen : Form
    {
        string[] args;
        public StartScreen()
        {
            InitializeComponent();
        }

        public StartScreen(string[] args)
        {
            InitializeComponent();
            this.args = args;
        }

        private void StartScreen_Load(object sender, EventArgs e)
        {
            label8.Text += Environment.OSVersion.ToString();
            label9.Text += Environment.MachineName;
            label10.Text += Environment.UserName;

            timer1.Start();

            allapot.Location = new Point(this.Width - allapot.Width, allapot.Location.Y);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool end = false;
            RegistryKey rkey = Registry.CurrentUser.OpenSubKey("Software\\Gyermekvasút", true);
            timer1.Stop();

            switch (allapot.Text)
            {
                case "Fájlok betöltése":
                    allapot.Text = "Forrásfájlok ellenőrzése (DLL)";
                    allapot.Location = new Point(this.Width - allapot.Width, allapot.Location.Y);
                    if (!File.Exists("Gyermekvasút.Modellek.dll"))
                    {
                        MessageBox.Show("Nem található forrásfájl!\n* Gyermekvasút.Modellek.dll\n\nA program leáll!", "Összeomlás", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    break;
                case "Forrásfájlok ellenőrzése (DLL)":
                    allapot.Text = "Forrásfájlok ellenőrzése (PDB)";
                    allapot.Location = new Point(this.Width - allapot.Width, allapot.Location.Y);
                    if (!File.Exists("Gyermekvasút.Modellek.pdb"))
                    {
                        MessageBox.Show("Nem található forrásfájl!\n* Gyermekvasút.Modellek.pdb\n\nA program leáll!", "Összeomlás", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    break;
                case "Forrásfájlok ellenőrzése (PDB)":
                    allapot.Text = "Forrásfájlok ellenőrzése (GYV)";
                    allapot.Location = new Point(this.Width - allapot.Width, allapot.Location.Y);
                    if (!File.Exists("s_zav.gyv"))
                    {
                        MessageBox.Show("Nem található forrásfájl!\n* s_zav.gyv\n\nA program leáll!", "Összeomlás", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    break;
                case "Forrásfájlok ellenőrzése (GYV)":
                    allapot.Text = "Hálózati konfigurációs fájl ellenőrzése";
                    allapot.Location = new Point(this.Width - allapot.Width, allapot.Location.Y);
                    if (!File.Exists("Gyermekvasút.exe.config"))
                    {
                        MessageBox.Show("Nem található a hálózati konfigurációs fájl!\n* Gyermekvasút.exe.config\n\nA program leáll!", "Összeomlás", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    break;
                case "Hálózati konfigurációs fájl ellenőrzése":
                    allapot.Text = "Éves naptár ellenőrzése";
                    allapot.Location = new Point(this.Width - allapot.Width, allapot.Location.Y);
                    if (!File.Exists("EvesNaptar.gyv"))
                    {
                        MessageBox.Show("Nem található az éves naptár!\n* EvesNaptar.gyv\n\nA program leáll!", "Összeomlás", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    break;
                case "Éves naptár ellenőrzése":
                    allapot.Text = "LCSV adatbázis ellenőrzése";
                    allapot.Location = new Point(this.Width - allapot.Width, allapot.Location.Y);
                    if (!File.Exists("LCSV.gyv"))
                    {
                        MessageBox.Show("Nem található az LCSV adatbázis!\n* LCSV.gyv\n\nA program leáll!", "Összeomlás", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    break;
                case "LCSV adatbázis ellenőrzése":
                    allapot.Text = "Telepítettség ellenőrzése";
                    allapot.Location = new Point(this.Width - allapot.Width, allapot.Location.Y);
                    if (rkey == null)
                    {
                        MessageBox.Show("A program nincs megfelelően telepítve!\n\nA program leáll!", "Összeomlás", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    else
                    {
                        rkey.SetValue("GYVSzimPath", System.Reflection.Assembly.GetExecutingAssembly().Location);
                        rkey.SetValue("LastRunDateTime", DateTime.Now.ToString());
                        if (Environment.GetCommandLineArgs().Length == 1)
                        {
                            rkey.SetValue("LastRunCmdLineArgs", "NULL");
                        }
                        else
                        {
                            rkey.SetValue("LastRunCmdLineArgs", Environment.GetCommandLineArgs()[1]);
                        }                        
                        rkey.SetValue("LastRunCrash", "NULL");
                        rkey.SetValue("LastRunGenErrorLog", "NULL");
                        rkey.Close();
                    }
                    break;
                case "Telepítettség ellenőrzése":
                    allapot.Text = "Licenszellenőrzés";
                    allapot.Location = new Point(this.Width - allapot.Width, allapot.Location.Y);

                    int ev = 0, ho = 0, nap = 0;
                    DateTime licenseDate = DateTime.MinValue;
                    try
                    {
                        string value = (string)rkey.GetValue("License");
                        int temp = Convert.ToInt32(value.Substring(0, 4));
                        ev = ((temp + 4999) / 3 + 1999) / 3;
                        temp = Convert.ToInt32(value.Substring(4, 3));
                        ho = (int)Math.Sqrt(temp - 4);
                        temp = Convert.ToInt32(value.Substring(7, 3));
                        nap = (int)Math.Sqrt(temp - 19);
                        licenseDate = new DateTime(ev, ho, nap);
                        
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("A felhasználóhoz tartozó licenszkulcs nem megfelelő!\n\nA program leáll!", "Összeomlás", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }

                    DateTime now = DateTime.Now;
                    if (!(DateTime.Compare(licenseDate, now) > 0))
                    {//lejárt
                        MessageBox.Show("A felhasználóhoz tartozó licenszkulcs lejárt!\n\nA program leáll!", "Összeomlás", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    break;

                case "Licenszellenőrzés":
                    allapot.Text = "Ablak megnyitása";
                    allapot.Location = new Point(this.Width - allapot.Width, allapot.Location.Y);
                    break;

                case "Ablak megnyitása":
                    end = true;

                    if (args.Length == 0)
                    {
                        //Application.Run(new Index());
                        Index index = new Index(this);
                        index.Show();
                        //az alapértelmezett form az index legyen
                        this.Hide();
                    }
                    else
                    {
                        //Application.Run(new Halozat(args[0]));
                        Halozat halozat = new Halozat(args[0], this);
                        halozat.Show();
                        //az alapértelmezett form a halozat legyen
                        this.Hide();
                    }
                    break;
                default:
                    break;
            }

            if (!end)
                timer1.Start();
        }
    }
}
