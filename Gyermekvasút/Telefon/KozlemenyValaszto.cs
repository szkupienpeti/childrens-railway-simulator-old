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
    public partial class KozlemenyValaszto : Form
    {
        bool nemMegszakit;
        Index ind;
        Allomas hivoAll;
        IAllomas hivandoAll;
        bool kpFele;

        public KozlemenyValaszto(Allomas hivoAll, IAllomas hivandoAll, Index ind)
        {            
            InitializeComponent();
            this.ind = ind;
            this.hivoAll = hivoAll;
            this.hivandoAll = hivandoAll;

            if (hivoAll.KezdopontiAllomas == hivandoAll)
            {
                kpFele = true;
            }
            else if (hivoAll.VegpontiAllomas == hivandoAll)
            {
                kpFele = false;
            }
            else
            {
                throw new Exception("TELEFON: KozlemenyValaszto(IAllomas hivoAll, IAllomas hivandoAll, Index ind) konstruktor: A felhívandó állomás nem feleltethető meg a hívó állomás egyik szomszéd állomásának sem!");
            }
        }

        public KozlemenyValaszto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nemMegszakit = true;
            
            if (radioButton1.Checked) //ENGEDÉLY
            {
                //annak az ellenőrzése, hogy kért-e már engedélyt
                if ((kpFele && (((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben != null && !(hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben.Paros) || ((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben2 != null && !(hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben2.Paros))) || (!kpFele && (((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben != null && (hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben.Paros) || ((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben2 != null && (hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben2.Paros))))
                {
                    ind.Hibak[14]++;
                    MessageBox.Show("Ebbe az irányba már kértél engedélyt!\nÁllomástávolságú követési rendben a követő vonat részére engedély csak az elölhaladó vonatról kapott visszajelentés után kérhető!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                EngedelyKeres ek = new EngedelyKeres(hivoAll, hivandoAll, ind, kpFele, this);
                ek.ShowDialog();
            }
            else if (radioButton2.Checked) //INDULÁSI
            {
                //keresztnél előbb visszjel, utána indulása
                if (kpFele)
                {
                    if((hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben != null && (hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben2 != null)
                    {
                        ind.Hibak[23]++;
                        MessageBox.Show("A(z) " + (hivoAll.Szakaszok[0] as Allomaskoz).Engedelyben.Vonatszam + " számú vonatról még nem adtál visszajelentést!\nElőször visszajelentést kell adnod, csak azután adhatsz indulásiidő-közlést.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    if ((hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben != null && (hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben2 != null)
                    {
                        ind.Hibak[23]++;
                        MessageBox.Show("A(z) " + (hivoAll.Szakaszok[6] as Allomaskoz).Engedelyben.Vonatszam + " számú vonatról még nem adtál visszajelentést!\nElőször visszajelentést kell adnod, csak azután adhatsz indulásiidő-közlést.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                IndulasiAdas ia = new IndulasiAdas(hivoAll, hivandoAll, kpFele, ind, this);
                ia.ShowDialog();
            }
            else if (radioButton3.Checked) //VISSZJEL
            {
                //annak az ellenőrzése, hogy a bejárati jelzőt visszaállította-e
                if (kpFele)
                {
                    if (hivoAll.Jelzok[0].Szabad && !hivoAll.Jelzok[0].Fenyjelzo) //a fényjelző magától visszaesik, szval nem kell vizsgálni
                    {//bejárati jelző szabad -- A
                        ind.Hibak[21]++;
                        MessageBox.Show("Az A jelű bejárati jelző továbbhaladást engedélyező állásban van, így nem adhatsz visszajelentést!\nNe feledkezz meg a visszajelentés feltételeiről!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    if (hivoAll.Jelzok[5].Szabad && !hivoAll.Jelzok[5].Fenyjelzo) //a fényjelző magától visszaesik, szval nem kell vizsgálni
                    {//bejárati jelző szabad -- B
                        ind.Hibak[21]++;
                        MessageBox.Show("A B jelű bejárati jelző továbbhaladást engedélyező állásban van, így nem adhatsz visszajelentést!\nNe feledkezz meg a visszajelentés feltételeiről!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                VisszjelAdas va = new VisszjelAdas(hivoAll, hivandoAll, kpFele, ind, this);
                va.ShowDialog();
            }
        }

        private void KozlemenyValaszto_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;

            if (hivandoAll != null)
                toolStripStatusLabel1.Text += Index.GetSzomszedAllomasNev(hivoAll.Name, kpFele);
        }

        private void KozlemenyValaszto_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!nemMegszakit)
            {
                if (MessageBox.Show("Biztosan megszakítod a hívást?", "Hívás megszakítása", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {//megszakítja a hívást >> kilépés (ballon a szomszédnak)
                    //bool b = hivandoAll == hivoAll.KezdopontiAllomas;
                    //string c = b ? "true" : "false";
                    //MessageBox.Show("#SYSTEMINFO_PHONECALL_SWITCHER\nINTERRUPTED\n***\n" + hivandoAll.ToString() + "\n\n---> " + c, "#SYSTEMINFO");
                    hivandoAll.OnHivasMegszakitva(kpFele);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
