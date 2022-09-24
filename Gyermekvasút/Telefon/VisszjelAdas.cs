using Gyermekvasút.Hálózat;
using Gyermekvasút.Modellek;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút.Telefon
{
    public partial class VisszjelAdas : Form
    {
        Index ind;
        Allomas hivoAll;
        IAllomas fogadoAll;
        bool kpFele;
        bool nemIksz;
        KozlemenyValaszto kozlval;

        public VisszjelAdas()
        {
            InitializeComponent();
        }

        public VisszjelAdas(Allomas hivoAll, IAllomas fogadoAll, bool kpFele, Index ind, KozlemenyValaszto kv)
        {
            InitializeComponent();

            this.hivoAll = hivoAll;
            this.fogadoAll = fogadoAll;
            this.kpFele = kpFele;
            this.ind = ind;
            kozlval = kv;

            toolStripStatusLabel1.Text += Index.GetSzomszedAllomasNev(hivoAll.Name, kpFele);
            label4.Text = "számú vonat megérkezett " + hivoAll.Name + " állomásra.";

            //a bejárati jelző megállj állását a KozlVal ellenőrzi -- kivévce fényjelzőknél, ott lehet szabadban, mert a vonat után úgyis visszaesik magától --!
            foreach (Vonat v in hivoAll.FogadottVonatok)
            {//megérkezett az állomásra
                if (!hivoAll.VisszjeltAdtam.Contains(v.Vonatszam) && v.Paros == kpFele)
                {//még nem adott visszjelt
                    comboBox1.Items.Add(v);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {//ok
            try
            {
                if (comboBox1.SelectedIndex == -1)
                {//nincs vonat kiválasztva
                    kimenet.BackColor = Color.Red;
                    kimenet.Text = "✘ Adj meg egy vonatot!";
                }
                else
                {
                    kimenet.Text = "";
                    kimenet.BackColor = SystemColors.Control;
                    hivoAll.VisszjeltAdtam.Add((comboBox1.SelectedItem as Vonat).Vonatszam);

                    //UtolsoVonat, Engedelyben, Engedelyben2 _SET_ (hivoAll)
                    if (kpFele)
                    {
                        if ((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben2 != null)
                        {
                            (hivoAll.Szakaszok[0] as Allomaskoz).UtolsoVonat = (hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben;
                            (hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben = (hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben2;
                            (hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben2 = null;
                        }
                        else
                        {
                            (hivoAll.Szakaszok[0] as Allomaskoz).UtolsoVonat = (hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben;
                            (hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben = null;
                        }
                    }
                    else
                    {
                        if ((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben2 != null)
                        {
                            (hivoAll.Szakaszok[6] as Allomaskoz).UtolsoVonat = (hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben;
                            (hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben = (hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben2;
                            (hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben2 = null;
                        }
                        else
                        {
                            (hivoAll.Szakaszok[6] as Allomaskoz).UtolsoVonat = (hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben;
                            (hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben = null;
                        }
                    }

                    object[] parameters = new object[1];
                    parameters[0] = comboBox1.SelectedItem as Vonat;
                    fogadoAll.OnKozlemeny(kpFele, 8, parameters);

                    nemIksz = true;
                    ind.notifyIcon1.ShowBalloonTip(100, "Telefon", "Várakozás " + Index.GetSzomszedAllomasNev(hivoAll.Name, kpFele) + " állomás válaszára", ToolTipIcon.None);
                    this.Close();
                }

                //allomas.visszjeltimer STOP --
                bool stop = true;
                foreach (Vonat item in hivoAll.FogadottVonatok)
                {
                    if (!hivoAll.VisszjeltAdtam.Contains(item.Vonatszam))
                    {
                        stop = false;
                    }
                }
                if (stop)
                {
                    hivoAll.VisszjelTimerStop();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ezt az ablakot látod, az azt jelenti, hogy volt ugyan valami belső hiba a programban, de mi hősiesen megvédtünk tőle (ha már kijavítani nem tudtuk).\nEz az ablak számításaink szerint akkor jelenhet meg, ha egy végállomáson körüljárás közben próbálsz visszajelentést adni, de pontosan mi sem tudjuk, miért történik ez ilyenkor. Kérlek, szólj egy ifinek, hogy ez az ablak megjelent, és meséld el neki részletesen, hogy mi is történt pontosan (valóban körüljárás közbeni visszajelentés-kísérlet okozta-e, és ha igen, hol volt éppen a GÉP az adott pillanatban)!\n\nAz a művelet, ami ezt a hibát okozta, nem biztos, hogy megtörtént (ha visszajelentést adtál volna, inkább próbáld meg újra visszajelenteni a vonatot; ha nem lesz ott a legördülő listában, akkor minden oké.)", "Valami rosszul sült el", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Gyermekvasút.DEBUG.Log.AddLogText("VisszjelAdas exception-t dobott. Adatok: " + ex.ToString() + " --- " + ex.Data + " --- " + ex.Message);
            }
        }

        private void VisszjelAdas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!nemIksz)
            {
                if (MessageBox.Show("Biztosan megszakítod a hívást?", "Hívás megszakítása", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {//megszakítja a hívást >> kilépés (ballon a szomszédnak)
                    fogadoAll.OnHivasMegszakitva(kpFele);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                kozlval.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {//help
            MessageBox.Show("Ha nem találsz valamit, annak az oka a következő lehet:\n*Csak olyan vonatról adhatsz visszajelentést, amely már megérkezett az állomásodra és a bejárati jelzőt már visszaállítottad Megállj-állásba (Visszajelentés feltételei). Egy vonatról csak egy visszajelentés adható.", "Segítség", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
