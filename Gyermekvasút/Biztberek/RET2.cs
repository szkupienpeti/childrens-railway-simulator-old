using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gyermekvasút.Modellek;
using Gyermekvasút.Modellek.Emeltyűtípusok;

namespace Gyermekvasút.Biztberek
{
    public partial class RET2 : EM //egy váltóhoz két reteszemeltyű
    {
        public RET2()
        {
            InitializeComponent();            
        }

        public Emeltyu R1E { get; set; }
        public Emeltyu R1K { get; set; }
        public Emeltyu R2E { get; set; }
        public Emeltyu R2K { get; set; }
        public OldasKenyszerito OK2 { get; set; }
        public Emeltyu B { get; set; }
        public Emeltyu C { get; set; }
        public AllomasForm AllForm { get; set; }
        public Emeltyu TJ { get; set; }

        public event FrissuljDelegate ValtoFeloldva;
        public void OnValtoFeloldva()
        {
            if (ValtoFeloldva != null)
            {
                ValtoFeloldva();
            }
        }

        public override void Allitas(Emeltyu emeltyu, bool jobbgombos = false)
        {
            if (!jobbgombos)
            {
                switch (emeltyu.Tipus)
                {
                    case EmeltyuTipus.AEJ:
                        #region AEJ
                        if (emeltyu.Allas == EmeltyuAllas.Also)
                        {//ALSÓ >> FELSŐ (szabadra előjelzés)
                            if (A1.Allas == EmeltyuAllas.Felso)
                            {
                                AEJ.Allas = EmeltyuAllas.Felso;
                            }
                        }
                        else
                        {//FELSŐ >> ALSÓ (megálljra előjelzés -- visszaállítás)
                            AEJ.Allas = EmeltyuAllas.Also;
                        }
                        break;
                        #endregion
                    case EmeltyuTipus.A1:
                        #region A1
                        if (emeltyu.Allas == EmeltyuAllas.Also)
                        {//ALSÓ >> FELSŐ -- szabadra állítás
                            if (VK2.Valto.Allas && VK2.Valto.Lezart && TJ.Allas == EmeltyuAllas.Also && C.Allas == EmeltyuAllas.Also && B.Allas == EmeltyuAllas.Also)
                            {
                                if (VK2.Be == null)
                                {
                                    MessageBox.Show("A bejárati vágányút beállításának elrendelését nem előzheti meg a bejárati jelző szabadra állítása!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ind.Hibak[5]++;
                                }
                                else
                                {
                                    if (ALLOMAS.FogadottVonatok.Contains(VK2.Be))
                                    {
                                        MessageBox.Show("A bejárati vágányúton már leközlekedett a vonat, a bejárati jelzőt már vissza is állítottad Megállj!-állásba.\nNem állíthatod újra szabadra a jelzőt, mert egy vágányúton csak egy vonat közlekedhet le!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        Ind.Hibak[6]++;
                                    }
                                    else
                                    {
                                        A1.Allas = EmeltyuAllas.Felso;
                                    }
                                }
                            }
                        }
                        else
                        {//FELSŐ >> ALSÓ -- megálljba állítás
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
                                        OK2.Start2(true);
                                        OK2.Elinditva = true;
                                    }
                                }
                            }
                        }
                        break;
                        #endregion
                    case EmeltyuTipus.A2:
                        #region A2
                        if (emeltyu.Allas == EmeltyuAllas.Also)
                        {//ALSÓ >> FELSŐ -- megálljba állítás
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
                                    OK2.Start2(true);
                                    OK2.Elinditva = true;
                                }
                            }
                        }
                        else
                        {//FELSŐ >> ALSÓ -- szabadra állítás
                            if (!VK2.Valto.Allas && VK2.Valto.Lezart && TJ.Allas == EmeltyuAllas.Also)
                            {
                                if (VK2.Be == null)
                                {
                                    MessageBox.Show("A bejárati vágányút beállításának elrendelését nem előzheti meg a bejárati jelző szabadra állítása!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ind.Hibak[5]++;
                                }
                                else
                                {
                                    if (ALLOMAS.FogadottVonatok.Contains(VK2.Be))
                                    {
                                        MessageBox.Show("A bejárati vágányúton már leközlekedett a vonat, a bejárati jelzőt már vissza is állítottad Megállj!-állásba.\nNem állíthatod újra szabadra a jelzőt, mert egy vágányúton csak egy vonat közlekedhet le!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        Ind.Hibak[6]++;
                                    }
                                    else
                                    {
                                        A2.Allas = EmeltyuAllas.Also;
                                    }
                                }
                            }
                        }
                        break;
                        #endregion
                    case EmeltyuTipus.R1E: //FTH FELÉ VEZETŐ VÁLTÓ!
                        #region R1E
                        if (emeltyu.Allas == EmeltyuAllas.Also)
                        {//ALSÓ >> FELSŐ: feloldás                         
                            emeltyu.Allas = EmeltyuAllas.Felso;
                            ALLOMAS.Valtok[1].Lezart = false;
                        }
                        else
                        {//FELSő >> ALSÓ: lezárás E irányba
                            if (ALLOMAS.Valtok[1].Allas)
                            {
                                MessageBox.Show("A váltó nem zárható le, mert nincs bejelentett vágányút, a szabványos állás pedig az oldott egyenes.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ind.Hibak[9]++;
                            }
                        }
                        break;
                        #endregion
                    case EmeltyuTipus.R1K: //FTH FELÉ VEZETŐ VÁLTÓ!
                        #region R1K
                        if (emeltyu.Allas == EmeltyuAllas.Also)
                        {//ALSÓ >> FELSŐ: lezárás K irányba
                            if (!ALLOMAS.Valtok[1].Allas)
                            {
                                MessageBox.Show("A váltó nem zárható le, mert nincs bejelentett vágányút, a szabványos állás pedig az oldott egyenes.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ind.Hibak[9]++;
                            }
                        }
                        else
                        {//FELSő >> ALSÓ: feloldás
                            emeltyu.Allas = EmeltyuAllas.Felso;
                            ALLOMAS.Valtok[1].Lezart = false;
                        }
                        break;
                        #endregion
                    case EmeltyuTipus.R2E:
                        #region R2E
                        if (emeltyu.Allas == EmeltyuAllas.Also)
                        {//ALSÓ >> FELSŐ: feloldás vonat után                            
                            if (A1.Allas == EmeltyuAllas.Also && A2.Allas == EmeltyuAllas.Felso && B.Allas == EmeltyuAllas.Also)
                            {//szerkezeti függés (bejárati jelző alapban)
                                if (VK2.Be != null && (SZAKASZOK[3].Vonat == VK2.Be || SZAKASZOK[4].Vonat == VK2.Be || SZAKASZOK[5].Vonat == VK2.Be || SZAKASZOK[6].Vonat == VK2.Be || (!ALLOMAS.Vonatok.Contains(VK2.Be) && ALLOMAS.FogadottVonatok.Contains(VK2.Be))))
                                {//BEJÁRAT utáni feloldás: vonat megérkezett, bejárat alapban
                                    emeltyu.Allas = EmeltyuAllas.Felso;
                                    VK2.Valto.Lezart = false;
                                    OnValtoFeloldva();
                                    VK2.ProgressBar = 0;
                                    if (VK2.Ki == null)
                                    {//NEM ellenvonatos vgút
                                        VK2.Be = null;
                                        VK2.Vagany = 0;
                                    }
                                    else
                                    {//ELLENVONATOS vgút
                                        if (VK2.Ki.Virtualis == false)
                                        {//egyébként a körüljárás után
                                            if (!VK2.Be.Forda.Contains(VK2.Ki.Vonatszam))
                                            {//ha nem az érkező vonatból (körüljárás NÉLKÜL) forduló vonatnak a kijárata, csak akkor cserél vágányt
                                                VK2.Vagany = VK2.Vagany == 1 ? 2 : 1;
                                            }
                                            VK2.Bejarat = false;
                                        }
                                    }
                                    OK2.Stop();
                                    OK2.Elinditva = false;
                                }
                                else if (VK2.Ki != null && (VK2.Ki == SZAKASZOK[0].Vonat || !ALLOMAS.Vonatok.Contains(VK2.Ki)))
                                {//KIJÁRAT UTÁNI FELOLDÁS: vonat a nyíltvonalon vagy már lefogadta a következő állomás
                                    emeltyu.Allas = EmeltyuAllas.Felso;
                                    VK2.Valto.Lezart = false;
                                    OnValtoFeloldva();
                                    VK2.ProgressBar = 0;
                                    OK2.Stop();
                                    OK2.Elinditva = false;
                                    VK2.Ki = null;
                                }
                                else
                                {//nem oldható fel
                                    MessageBox.Show("A váltó nem oldható fel, mert a vágányúton, amelyben érintett, még nem közlekedett le vonat.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ind.Hibak[7]++;
                                }
                            }//szerkezeti függés vége >> nincs else ág
                        }
                        else
                        {//FELSŐ >> ALSÓ: váltó lezárása E irányba
                            if (VK2.Valto.Allas)
                            {//szerkezeti függés
                                if (VK2.Valto.AllitasAlatt)
                                {//állítás alatt
                                    MessageBox.Show("A váltó nem zárható le, mert a váltókezelő éppen egy olyan vágányutat állít be, amelyben a váltó érintett.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ind.Hibak[8]++;
                                }
                                else
                                {
                                    if (VK2.Be == null && VK2.Ki == null)
                                    {//indokolatlan a lezárás
                                        MessageBox.Show("A váltó nem zárható le, mert nincs bejelentett vágányút, a szabványos állás pedig az oldott egyenes.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        Ind.Hibak[9]++;
                                    }
                                    else
                                    {//lezárás
                                        VK2.Valto.Lezart = true;
                                        emeltyu.Allas = EmeltyuAllas.Also;
                                    }
                                }
                            }
                        }
                        break;
                        #endregion
                    case EmeltyuTipus.R2K:
                        #region R2K
                        if (emeltyu.Allas == EmeltyuAllas.Also)
                        {//ALSÓ >> FELSŐ: váltó lezárása K irányba
                            if (!VK2.Valto.Allas)
                            {//szerkezeti függés
                                if (VK2.Valto.AllitasAlatt)
                                {//állítás alatt
                                    MessageBox.Show("A váltó nem zárható le, mert a váltókezelő éppen egy olyan vágányutat állít be, amelyben a váltó érintett.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ind.Hibak[8]++;
                                }
                                else
                                {
                                    if (VK2.Be == null && VK2.Ki == null)
                                    {//indokolatlan a lezárás
                                        MessageBox.Show("A váltó nem zárható le, mert nincs bejelentett vágányút, a szabványos állás pedig az oldott egyenes.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        Ind.Hibak[9]++;
                                    }
                                    else
                                    {//lezárás
                                        VK2.Valto.Lezart = true;
                                        emeltyu.Allas = EmeltyuAllas.Felso;
                                    }
                                }
                            }
                        }
                        else
                        {//FELSŐ >> ALSÓ: váltó feloldása (vonat után)
                            if (A1.Allas == EmeltyuAllas.Also && A2.Allas == EmeltyuAllas.Felso && C.Allas == EmeltyuAllas.Also)
                            {//szerkezeti függés (bejárati jelző alapban)
                                if (VK2.Be != null && (SZAKASZOK[8].Vonat == VK2.Be || SZAKASZOK[9].Vonat == VK2.Be || SZAKASZOK[5].Vonat == VK2.Be || SZAKASZOK[6].Vonat == VK2.Be || (!ALLOMAS.Vonatok.Contains(VK2.Be) && ALLOMAS.FogadottVonatok.Contains(VK2.Be))))
                                {//BEJÁRAT utáni feloldás: vonat megérkezett, bejárat alapban
                                    emeltyu.Allas = EmeltyuAllas.Also;
                                    VK2.Valto.Lezart = false;
                                    OnValtoFeloldva();
                                    VK2.Valto.Allas = true;
                                    VK2.ProgressBar = 0;
                                    if (VK2.Ki == null)
                                    {//NEM ellenvonatos vgút
                                        VK2.Be = null;
                                        VK2.Vagany = 0;
                                    }
                                    else
                                    {//ELLENVONATOS vgút
                                        if (VK2.Ki.Virtualis == false)
                                        {//egyébként a körüljárás után
                                            if (!VK2.Be.Forda.Contains(VK2.Ki.Vonatszam))
                                            {//ha nem az érkező vonatból (körüljárás NÉLKÜL) forduló vonatnak a kijárata, csak akkor cserél vágányt
                                                VK2.Vagany = VK2.Vagany == 1 ? 2 : 1;
                                            }
                                            VK2.Bejarat = false;
                                        }
                                    }
                                    OK2.Stop();
                                    OK2.Elinditva = false;
                                }
                                else if (VK2.Ki != null && (VK2.Ki == SZAKASZOK[0].Vonat || !ALLOMAS.Vonatok.Contains(VK2.Ki)))
                                {//KIJÁRAT UTÁNI FELOLDÁS: vonat a nyíltvonalon vagy már lefogadta a következő állomás
                                    emeltyu.Allas = EmeltyuAllas.Also;
                                    VK2.Valto.Lezart = false;
                                    OnValtoFeloldva();
                                    VK2.Valto.Allas = true;
                                    VK2.ProgressBar = 0;
                                    OK2.Stop();
                                    OK2.Elinditva = false;
                                    VK2.Ki = null;
                                }
                                else
                                {//nem oldható fel
                                    MessageBox.Show("A váltó nem oldható fel, mert a vágányúton, amelyben érintett, még nem közlekedett le vonat.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ind.Hibak[7]++;
                                }
                            }
                        }
                        break;
                        #endregion
                    case EmeltyuTipus.TJ1:
                        #region TJ1
                        if (emeltyu.Allas == EmeltyuAllas.Also)
                        {//ALSÓ >> FELSŐ: tolatás eng.
                            if (A1.Allas == EmeltyuAllas.Also && A2.Allas == EmeltyuAllas.Felso && C.Allas == EmeltyuAllas.Also && B.Allas == EmeltyuAllas.Also)
                            {//szerkezeti függés
                                //TODO tolatásjelző
                                emeltyu.Allas = EmeltyuAllas.Felso;
                            }
                        }
                        else
                        {//FELSŐ >> ALSÓ: tolatás tilt
                            emeltyu.Allas = EmeltyuAllas.Also;
                        }
                        break;
                        #endregion
                    case EmeltyuTipus.B:
                        #region B
                        if (emeltyu.Allas == EmeltyuAllas.Also)
                        {//ALSÓ >> FELSŐ: szabadra állítás
                            if (VK2.Valto.Allas && VK2.Valto.Lezart && C.Allas == EmeltyuAllas.Also && A1.Allas == EmeltyuAllas.Also && A2.Allas == EmeltyuAllas.Felso && TJ.Allas == EmeltyuAllas.Also)
                            {//szerkezeti függés
                                if (VK2.Ki == null || VK2.Be != null)
                                {//még nem rendelte el
                                    MessageBox.Show("A kijárati vágányút beállításának elrendelését nem előzheti meg a kijárati jelző szabadra állítása!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ind.Hibak[5]++;
                                }
                                else
                                {
                                    if (!ALLOMAS.Vonatok.Contains(VK2.Ki) || ALLOMAS.Szakaszok[0].Vonat == VK2.Ki)
                                    {//már kihaladt, újra szabadba akarja állítani
                                        MessageBox.Show("A kijárati vágányúton már leközlekedett a vonat, a kijárati jelzőt már vissza is állítottad Megállj!-állásba.\nNem állíthatod újra szabadra a jelzőt, mert egy vágányúton csak egy vonat közlekedhet le!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        Ind.Hibak[6]++;
                                    }
                                    else
                                    {
                                        B.Allas = EmeltyuAllas.Felso;
                                        ALLOMAS.Szakaszok[3].Jelzo.Szabad = true;
                                    }
                                }
                            }
                        }
                        else
                        {//FELSŐ >> ALSÓ: megálljba állítás (KIJ után)
                            if (ALLOMAS.Szakaszok[2].Vonat == VK2.Ki)
                            {//a vonat még nem haladt le a váltóról -- éppen a váltó-kij. j. között van == még nem hasznáélta fel az egész vágányutat
                                MessageBox.Show("A kijárati jelzőt leghamarabb azután szabad visszaállítani Megállj!-állásba, hogy a kihaladó vonat lehaladt a kijárati váltóról!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ind.Hibak[3]++;
                            }
                            else
                            {
                                if (ALLOMAS.Vonatok.Contains(VK2.Ki) && ALLOMAS.Szakaszok[0].Vonat != VK2.Ki && ALLOMAS.Szakaszok[1].Vonat != VK2.Ki)
                                {//a vonat még nem közlekedett le a vgúton: nem ment el a másik állomásra, nincs az állközben és a CsucsSzakaszon sem
                                    MessageBox.Show("A kijárati jelzőt még nem állíthatod vissza Megállj!-állásba, mert a tőle kezdődő vágányúton még nem közlekedett le vonat (a kijárati jelzőt még meg sem haladta a vonat, amelyiknek a kijárati vágányútjának a szabad mivoltát bejelentette a váltókezelő)!\nA vonat még vagy meg sem érkezett az állomásra vagy éppen az állomáson áll.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ind.Hibak[4]++;
                                }
                                else
                                {
                                    emeltyu.Allas = EmeltyuAllas.Also;
                                    ALLOMAS.Szakaszok[3].Jelzo.Szabad = false;
                                    OK2.Start2(false);
                                    OK2.Elinditva = true;
                                }
                            }
                        }
                        break;
                        #endregion
                    case EmeltyuTipus.C:
                        #region C
                        if (emeltyu.Allas == EmeltyuAllas.Also)
                        {//ALSÓ >> FELSŐ: szabadra állítás
                            if (!VK2.Valto.Allas && VK2.Valto.Lezart && B.Allas == EmeltyuAllas.Also && A1.Allas == EmeltyuAllas.Also && A2.Allas == EmeltyuAllas.Felso && TJ.Allas == EmeltyuAllas.Also)
                            {//szerkezeti függés
                                if (VK2.Ki == null || VK2.Be != null)
                                {//még nem rendelte el
                                    MessageBox.Show("A kijárati vágányút beállításának elrendelését nem előzheti meg a kijárati jelző szabadra állítása!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ind.Hibak[5]++;
                                }
                                else
                                {
                                    if (!ALLOMAS.Vonatok.Contains(VK2.Ki) || ALLOMAS.Szakaszok[0].Vonat == VK2.Ki)
                                    {//már kihaladt, újra szabadba akarja állítani
                                        MessageBox.Show("A kijárati vágányúton már leközlekedett a vonat, a kijárati jelzőt már vissza is állítottad Megállj!-állásba.\nNem állíthatod újra szabadra a jelzőt, mert egy vágányúton csak egy vonat közlekedhet le!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        Ind.Hibak[6]++;
                                    }
                                    else
                                    {
                                        emeltyu.Allas = EmeltyuAllas.Felso;
                                        ALLOMAS.Szakaszok[8].Jelzo.Szabad = true;
                                    }
                                }
                            }
                        }
                        else
                        {//FELSŐ >> ALSÓ: megálljba állítás (KIJ után)
                            if (ALLOMAS.Szakaszok[7].Vonat == VK2.Ki)
                            {//a vonat még nem haladt le a váltóról -- éppen a váltó-kij. j. között van == még nem hasznáélta fel az egész vágányutat
                                MessageBox.Show("A kijárati jelzőt leghamarabb azután szabad visszaállítani Megállj!-állásba, hogy a kihaladó vonat lehaladt a kijárati váltóról!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ind.Hibak[3]++;
                            }
                            else
                            {
                                if (ALLOMAS.Vonatok.Contains(VK2.Ki) && ALLOMAS.Szakaszok[0].Vonat != VK2.Ki && ALLOMAS.Szakaszok[1].Vonat != VK2.Ki)
                                {//a vonat még nem közlekedett le a vgúton: nem ment el a másik állomásra, nincs az állközben és a CsucsSzakaszon sem
                                    MessageBox.Show("A kijárati jelzőt még nem állíthatod vissza Megállj!-állásba, mert a tőle kezdődő vágányúton még nem közlekedett le vonat (a kijárati jelzőt még meg sem haladta a vonat, amelyiknek a kijárati vágányútjának a szabad mivoltát bejelentette a váltókezelő)!\nA vonat még vagy meg sem érkezett az állomásra vagy éppen az állomáson áll.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ind.Hibak[4]++;
                                }
                                else
                                {
                                    emeltyu.Allas = EmeltyuAllas.Also;
                                    ALLOMAS.Szakaszok[8].Jelzo.Szabad = false;
                                    OK2.Start2(false);
                                    OK2.Elinditva = true;
                                }
                            }
                        }
                        break;
                        #endregion
                    default:
                        break;
                }
            }
            if (A1.Allas == A2.Allas)
            {
                SZAKASZOK[1].Jelzo.Szabad = true;
            }
            else
            {
                SZAKASZOK[1].Jelzo.Szabad = false;
            }
            if (AllForm != null)
            {
                AllForm.Frissit();
            }
        }
    }
}
