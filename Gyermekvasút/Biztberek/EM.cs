using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút.Modellek.Emeltyűtípusok
{
    public class EM : UserControl, IEM
    {
        public EM() { }

        public Emeltyu A1 { get; set; }
        public Emeltyu A2 { get; set; }
        public Emeltyu AEJ { get; set; }
        public Emeltyu B1 { get; set; }
        public Emeltyu B2 { get; set; }
        public Emeltyu BEJ { get; set; }

        public ValtKez VK1 { get; set; }
        public ValtKez VK2 { get; set; }

        public List<Szakasz> SZAKASZOK { get; set; }

        public PictureBox VZK1 { get; set; }
        public PictureBox VZK2 { get; set; }

        public Allomas ALLOMAS { get; set; }

        public Index Ind { get; set; }

        public virtual void Allitas(Emeltyu emeltyu, bool jobbgombos = false)
        {
            if (emeltyu == A1 && !jobbgombos)
            {
                switch (A1.Allas)
                {
                    case EmeltyuAllas.Also:
                        //alsó >> felső (szabadra állítás)
                        if (VK2.Valto.Allas && VK2.Valto.Lezart && !SZAKASZOK[6].Jelzo.Szabad)
                        {
                            if (VK2.Be == null)
                            {
                                MessageBox.Show("A bejárati vágányút beállításának elrendelését nem előzheti meg a bejárati jelző szabadra állítása!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ind.Hibak[5]++;
                            }
                            else
                            {
                                A1.Allas = EmeltyuAllas.Felso;
                            }
                        }
                        break;
                    case EmeltyuAllas.Felso:
                        //felső >> alsó (megálljra állítás)
                        if (AEJ.Allas == EmeltyuAllas.Also)
                        {
                            if (SZAKASZOK[1].Foglalt || SZAKASZOK[2].Foglalt)
                            {//vonat a megálljraejtőn vagy a váltón
                                MessageBox.Show("A bejárati jelzőt leghamarabb azután szabad visszaállítani Megállj!-állásba, hogy a behaladó vonat lehaladt a bejárati váltóról!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ind.Hibak[3]++;
                            }
                            else
                            {
                                if (!ALLOMAS.FogadottVonatok.Contains(VK2.Be))
                                {//a vonat vagy még csak engedélyben van, vagy az állomásközben
                                    MessageBox.Show("A bejárati jelzőt még nem állíthatod vissza Megállj!-állásba, mert a tőle kezdődő vágányúton még nem közlekedett le vonat (a bejárati jelzőt még meg sem haladta a vonat, amelyiknek a bejárati vágányútjának a szabad mivoltát bejelentette a váltókezelő)!\nA vonat még vagy el sem indult a szomszéd állomásról, vagy éppen az állomásközben van.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ind.Hibak[4]++;
                                }
                                else
                                {
                                    A1.Allas = EmeltyuAllas.Also;
                                    VK2.Valto.Lezart = false;
                                    VK2.ProgressBar = 0;
                                    if (VK2.Ki == null)
                                    {//nem ellenvonatos kijárat
                                        VK2.Be = null;
                                        VK2.Vagany = 0;
                                    }
                                    else
                                    {
                                        VK2.Bejarat = false;
                                        VK2.Vagany = VK2.Vagany == 1 ? 2 : 1;
                                        VK2.Be = null;
                                        VK2.ProgressBar = 0;
                                        VK2.Timer.Start();
                                        VK2.Valto.Lezart = false;
                                        VK2.Valto.AllitasAlatt = true;
                                    }
                                    VZKrefresh();
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (emeltyu == A2 && !jobbgombos)
            {
                switch (A2.Allas)
                {
                    case EmeltyuAllas.Also:
                        //alsó >> felső (megálljra állítás)
                        if (SZAKASZOK[1].Foglalt || SZAKASZOK[7].Foglalt)
                        {//vonat a megálljraejtőn vagy a váltón
                            MessageBox.Show("A bejárati jelzőt leghamarabb azután szabad visszaállítani Megállj!-állásba, hogy a behaladó vonat lehaladt a bejárati váltóról!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Ind.Hibak[3]++;
                        }
                        else
                        {
                            if (!ALLOMAS.FogadottVonatok.Contains(VK2.Be))
                            {//a vonat vagy még csak engedélyben van, vagy az állomásközben
                                MessageBox.Show("A bejárati jelzőt még nem állíthatod vissza Megállj!-állásba, mert a tőle kezdődő vágányúton még nem közlekedett le vonat (a bejárati jelzőt még meg sem haladta a vonat, amelyiknek a bejárati vágányútjának a szabad mivoltát bejelentette a váltókezelő)!\nA vonat még vagy el sem indult a szomszéd állomásról, vagy éppen az állomásközben van.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ind.Hibak[4]++;
                            }
                            else
                            {
                                A2.Allas = EmeltyuAllas.Felso;
                                VK2.Valto.Lezart = false;
                                VK2.ProgressBar = 0;
                                VK2.Valto.Allas = true;
                                if (VK2.Ki == null)
                                {//nem ellenvonatos kijárat
                                    VK2.Be = null;
                                    VK2.Vagany = 0;
                                }
                                else
                                {
                                    VK2.Bejarat = false;
                                    VK2.Vagany = VK2.Vagany == 1 ? 2 : 1;
                                    VK2.Be = null;
                                    VK2.ProgressBar = 0;
                                    VK2.Timer.Start();
                                    VK2.Valto.Lezart = false;
                                    VK2.Valto.AllitasAlatt = true;
                                }
                                VZKrefresh();
                            }
                        }
                        break;
                    case EmeltyuAllas.Felso:
                        //felső >> alsó (szabadra állítás)
                        if (!VK2.Valto.Allas && VK2.Valto.Lezart && !SZAKASZOK[6].Jelzo.Szabad)
                        {
                            if (VK2.Be == null)
                            {
                                MessageBox.Show("A bejárati vágányút beállításának elrendelését nem előzheti meg a bejárati jelző szabadra állítása!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ind.Hibak[5]++;
                            }
                            else
                            {
                                A2.Allas = EmeltyuAllas.Also;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (emeltyu == AEJ && !jobbgombos)
            {
                switch (AEJ.Allas)
                {
                    case EmeltyuAllas.Also:
                        //alsó >> felső (szabadra előjelzés)
                        if (A1.Allas == EmeltyuAllas.Felso)
                        {
                            AEJ.Allas = EmeltyuAllas.Felso;
                        }
                        break;
                    case EmeltyuAllas.Felso:
                        //felső >> alsó (megálljra előjelzés - visszavétel)
                        AEJ.Allas = EmeltyuAllas.Also;
                        break;
                    default:
                        break;
                }
            }
            else if (emeltyu == B1 && !jobbgombos)
            {
                switch (B1.Allas)
                {
                    case EmeltyuAllas.Also:
                        //alsó >> felső (szabadra állítás)
                        if (VK1.Valto.Allas && VK1.Valto.Lezart && !SZAKASZOK[1].Jelzo.Szabad)
                        {
                            if (VK1.Be == null)
                            {
                                MessageBox.Show("A bejárati vágányút beállításának elrendelését nem előzheti meg a bejárati jelző szabadra állítása!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ind.Hibak[5]++;
                            }
                            else
                            {
                                B1.Allas = EmeltyuAllas.Felso;
                            }
                        }
                        break;
                    case EmeltyuAllas.Felso:
                        //felső >> alsó (megálljra állítás)
                        if (BEJ.Allas == EmeltyuAllas.Also)
                        {
                            if (SZAKASZOK[5].Foglalt || SZAKASZOK[4].Foglalt)
                            {//vonat a megálljraejtőn vagy a váltón
                                MessageBox.Show("A bejárati jelzőt leghamarabb azután szabad visszaállítani Megállj!-állásba, hogy a behaladó vonat lehaladt a bejárati váltóról!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ind.Hibak[3]++;
                            }
                            else
                            {
                                if (!ALLOMAS.FogadottVonatok.Contains(VK1.Be))
                                {//a vonat vagy még csak engedélyben van, vagy az állomásközben
                                    MessageBox.Show("A bejárati jelzőt még nem állíthatod vissza Megállj!-állásba, mert a tőle kezdődő vágányúton még nem közlekedett le vonat (a bejárati jelzőt még meg sem haladta a vonat, amelyiknek a bejárati vágányútjának a szabad mivoltát bejelentette a váltókezelő)!\nA vonat még vagy el sem indult a szomszéd állomásról, vagy éppen az állomásközben van.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ind.Hibak[4]++;
                                }
                                else
                                {
                                    B1.Allas = EmeltyuAllas.Also;
                                    VK1.Valto.Lezart = false;
                                    VK1.ProgressBar = 0;
                                    if (VK1.Ki == null)
                                    {//nem ellenvonatos kijárat
                                        VK1.Be = null;
                                        VK1.Vagany = 0;
                                    }
                                    else
                                    {
                                        VK1.Bejarat = false;
                                        VK1.Vagany = VK1.Vagany == 1 ? 2 : 1;
                                        VK1.Be = null;
                                        VK1.ProgressBar = 0;
                                        VK1.Timer.Start();
                                        VK1.Valto.Lezart = false;
                                        VK1.Valto.AllitasAlatt = true;
                                    }
                                    VZKrefresh();
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (emeltyu == B2 && !jobbgombos)
            {
                switch (B2.Allas)
                {
                    case EmeltyuAllas.Also:
                        //alsó >> felső (megálljra állítás)
                        if (SZAKASZOK[5].Foglalt || SZAKASZOK[9].Foglalt)
                        {//vonat a megálljraejtőn vagy a váltón
                            MessageBox.Show("A bejárati jelzőt leghamarabb azután szabad visszaállítani Megállj!-állásba, hogy a behaladó vonat lehaladt a bejárati váltóról!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Ind.Hibak[3]++;
                        }
                        else
                        {
                            if (!ALLOMAS.FogadottVonatok.Contains(VK1.Be))
                            {//a vonat vagy még csak engedélyben van, vagy az állomásközben
                                MessageBox.Show("A bejárati jelzőt még nem állíthatod vissza Megállj!-állásba, mert a tőle kezdődő vágányúton még nem közlekedett le vonat (a bejárati jelzőt még meg sem haladta a vonat, amelyiknek a bejárati vágányútjának a szabad mivoltát bejelentette a váltókezelő)!\nA vonat még vagy el sem indult a szomszéd állomásról, vagy éppen az állomásközben van.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ind.Hibak[4]++;
                            }
                            else
                            {
                                B2.Allas = EmeltyuAllas.Felso;
                                VK1.Valto.Lezart = false;
                                VK1.ProgressBar = 0;
                                VK1.Valto.Allas = true;
                                if (VK1.Ki == null)
                                {//nem ellenvonatos kijárat
                                    VK1.Be = null;
                                    VK1.Vagany = 0;
                                }
                                else
                                {
                                    VK1.Bejarat = false;
                                    VK1.Vagany = VK1.Vagany == 1 ? 2 : 1;
                                    VK1.Be = null;
                                    VK1.ProgressBar = 0;
                                    VK1.Timer.Start();
                                    VK1.Valto.Lezart = false;
                                    VK1.Valto.AllitasAlatt = true;
                                }
                                VZKrefresh();
                            }
                        }
                        break;
                    case EmeltyuAllas.Felso:
                        //felső >> alsó (szabadra állítás)
                        if (!VK1.Valto.Allas && VK1.Valto.Lezart && !SZAKASZOK[1].Jelzo.Szabad)
                        {
                            if (VK1.Be == null)
                            {
                                MessageBox.Show("A bejárati vágányút beállításának elrendelését nem előzheti meg a bejárati jelző szabadra állítása!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ind.Hibak[5]++;
                            }
                            else
                            {
                                B2.Allas = EmeltyuAllas.Also;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (emeltyu == BEJ && !jobbgombos)
            {
                switch (BEJ.Allas)
                {
                    case EmeltyuAllas.Also:
                        //alsó >> felső (szabadra előjelzés)
                        if (B1.Allas == EmeltyuAllas.Felso)
                        {
                            BEJ.Allas = EmeltyuAllas.Felso;
                        }
                        break;
                    case EmeltyuAllas.Felso:
                        //felső >> alsó (megálljra előjelzés - visszavétel)
                        BEJ.Allas = EmeltyuAllas.Also;
                        break;
                    default:
                        break;
                }
            }
            if (B1.Allas == B2.Allas)
            {
                SZAKASZOK[6].Jelzo.Szabad = true;
            }
            else
            {
                SZAKASZOK[6].Jelzo.Szabad = false;
            }
            if (A1.Allas == A2.Allas)
            {
                SZAKASZOK[1].Jelzo.Szabad = true;
            }
            else
            {
                SZAKASZOK[1].Jelzo.Szabad = false;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // EM
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Name = "EM";
            this.Size = new System.Drawing.Size(277, 274);
            this.ResumeLayout(false);

        }

        private void EM_Load(object sender, EventArgs e)
        {
            
        }

        private void VZKrefresh() //VÁLTÓZÁRKULCSOK FRISSÍTÉSE
        {
            if (VK1.Valto.Lezart)
            {
                if (VK1.Valto.Allas)
                {
                    VZK1.Image = Gyermekvasút.Properties.Resources.vzk1E;
                }
                else
                {
                    VZK1.Image = Gyermekvasút.Properties.Resources.vzk1K;
                }
            }
            else
            {
                VZK1.Image = Gyermekvasút.Properties.Resources.vzkAlap;
            }

            if (VK2.Valto.Lezart)
            {
                if (VK2.Valto.Allas)
                {
                    VZK2.Image = Gyermekvasút.Properties.Resources.vzk2E;
                }
                else
                {
                    VZK2.Image = Gyermekvasút.Properties.Resources.vzk2K;
                }
            }
            else
            {
                VZK2.Image = Gyermekvasút.Properties.Resources.vzkAlap;
            }
        }
    }
}
