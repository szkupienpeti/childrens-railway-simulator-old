using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút.Modellek.Emeltyűtípusok
{
    public partial class RET1 : EM //egy váltóhoz egy reteszemeltyű
    {
        public RET1()
        {
            InitializeComponent();
        }
        
        public Emeltyu R1 { get; set; }
        public Emeltyu R2 { get; set; }
        public OldasKenyszerito OK1 { get; set; }
        public OldasKenyszerito OK2 { get; set; }

        public AllomasForm AllForm { get; set; }

        public override void Allitas(Emeltyu emeltyu, bool jobbgombos = false)
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
                                    OK2.Start2(true);
                                    OK2.Elinditva = true;
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
                                OK2.Start2(true);
                                OK2.Elinditva = true;
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
                                if (ALLOMAS.FogadottVonatok.Contains(VK1.Be))
                                {
                                    MessageBox.Show("A bejárati vágányúton már leközlekedett a vonat, a bejárati jelzőt már vissza is állítottad Megállj!-állásba.\nNem állíthatod újra szabadra a jelzőt, mert egy vágányúton csak egy vonat közlekedhet le!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ind.Hibak[6]++;
                                }
                                else
                                {
                                    B1.Allas = EmeltyuAllas.Felso;
                                }
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
                                    OK1.Start2(true);
                                    OK1.Elinditva = true;
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
                                OK1.Start2(true);
                                OK1.Elinditva = true;
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
                                if (ALLOMAS.FogadottVonatok.Contains(VK1.Be))
                                {
                                    MessageBox.Show("A bejárati vágányúton már leközlekedett a vonat, a bejárati jelzőt már vissza is állítottad Megállj!-állásba.\nNem állíthatod újra szabadra a jelzőt, mert egy vágányúton csak egy vonat közlekedhet le!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ind.Hibak[6]++;
                                }
                                else
                                {
                                    B2.Allas = EmeltyuAllas.Also;
                                }
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
            else if (emeltyu == R1)
            {
                if (jobbgombos) //a jobb egérgomb hívja meg >> ÜRESBEN MOZGATÁS
                {
                    switch (R1.Allas)
                    {
                        case EmeltyuAllas.Also:
                            //alsóból felsőbe
                            if (!VK1.Valto.Lezart)
                            {
                                emeltyu.Allas = EmeltyuAllas.Felso;
                            }
                            break;
                        case EmeltyuAllas.Felso:
                            if (!VK1.Valto.Lezart)
                            {
                                emeltyu.Allas = EmeltyuAllas.Also;
                            }
                            break;
                        default:
                            break;
                    }
                }
                else //a bal egérgomb hívja meg >> KICSAPPANTÁS
                {
                    switch (R1.Allas)
                    {
                        case EmeltyuAllas.Also:
                            if (VK1.Valto.Lezart)
                            {//alsó >> felső (lezárt) = egyenes váltó feloldása
                                if (B1.Allas == EmeltyuAllas.Also && B2.Allas == EmeltyuAllas.Felso)
                                {
                                    if (VK1.Be != null && (SZAKASZOK[3].Vonat == VK1.Be || SZAKASZOK[2].Vonat == VK1.Be || SZAKASZOK[1].Vonat == VK1.Be || SZAKASZOK[0].Vonat == VK1.Be || (!ALLOMAS.Vonatok.Contains(VK1.Be) && ALLOMAS.FogadottVonatok.Contains(VK1.Be))))
                                    {//BEJÁRAT UTÁNI FELOLDÁS - a vonat már megérkezett, bejárat alapban
                                        emeltyu.Allas = EmeltyuAllas.Felso;
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
                                            VK1.Valto.AllitasAlatt = true;

                                        }
                                        OK1.Stop();
                                        OK1.Elinditva = false;
                                        VK1.Be = null;
                                    }
                                    else if (VK1.Ki != null && (VK1.Ki == SZAKASZOK[6].Vonat || (!ALLOMAS.Vonatok.Contains(VK1.Ki) && ALLOMAS.FogadottVonatok.Contains(VK1.Ki))))
                                    {//KIJÁRAT UTÁNI FELOLDÁS - a vonat vagy kint van a nyíltvonalon, vagy már lefogadta a szomszéd állomás
                                        emeltyu.Allas = EmeltyuAllas.Felso;
                                        VK1.Valto.Lezart = false;
                                        VK1.ProgressBar = 0;
                                        OK1.Stop();
                                        OK1.Elinditva = false;
                                        VK1.Ki = null;
                                    }
                                    else
                                    {//a váltó nem oldható fel
                                        MessageBox.Show("A váltó nem oldható fel, mert a vágányúton, amelyben érintett, még nem közlekedett le vonat.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        Ind.Hibak[7]++;
                                    }
                                }
                            }
                            else
                            {//alsó >> felső (oldott) = váltó lezárása kitérőbe
                                if (!VK1.Valto.Allas)
                                {
                                    if (VK1.Valto.AllitasAlatt)
                                    {//éppen állítja be a vágányutat
                                        MessageBox.Show("A váltó nem zárható le, mert a váltókezelő éppen egy olyan vágányutat állít be, amelyben a váltó érintett.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        Ind.Hibak[8]++;
                                    }
                                    else
                                    {
                                        if (VK1.Be == null && VK1.Ki == null)
                                        {//a lezárás indokolatlan
                                            MessageBox.Show("A váltó nem zárható le, mert nincs bejelentett vágányút, a szabványos állás pedig az oldott egyenes.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            Ind.Hibak[9]++;
                                        }
                                        else
                                        {
                                            VK1.Valto.Lezart = true;
                                            emeltyu.Allas = EmeltyuAllas.Felso;
                                        }
                                    }
                                }
                            }
                            break;
                        case EmeltyuAllas.Felso:
                            if (VK1.Valto.Lezart)
                            {//felső >> alsó (lezárt) = kitérő váltó feloldása
                                if (B1.Allas == EmeltyuAllas.Also && B2.Allas == EmeltyuAllas.Felso)
                                {
                                    if (VK1.Be != null && (SZAKASZOK[8].Vonat == VK1.Be || SZAKASZOK[7].Vonat == VK1.Be || SZAKASZOK[1].Vonat == VK1.Be || SZAKASZOK[0].Vonat == VK1.Be || (!ALLOMAS.Vonatok.Contains(VK1.Be) && ALLOMAS.FogadottVonatok.Contains(VK1.Be))))
                                    {//BEJÁRAT UTÁNI FELOLDÁS - a vonat már megérkezett, bejárat alapban
                                        emeltyu.Allas = EmeltyuAllas.Also;
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
                                            VK1.Valto.AllitasAlatt = true;
                                        }
                                        OK1.Stop();
                                        OK1.Elinditva = false;
                                        VK1.Be = null;
                                    }
                                    else if (VK1.Ki != null && (VK1.Ki == SZAKASZOK[6].Vonat || (!ALLOMAS.Vonatok.Contains(VK1.Ki) && ALLOMAS.FogadottVonatok.Contains(VK1.Ki))))
                                    {//KIJÁRAT UTÁNI FELOLDÁS - a vonat vagy kint van a nyíltvonalon, vagy már lefogadta a szomszéd állomás
                                        emeltyu.Allas = EmeltyuAllas.Also;
                                        VK1.Valto.Lezart = false;
                                        VK1.Valto.Allas = true;
                                        VK1.ProgressBar = 0;
                                        OK1.Stop();
                                        OK1.Elinditva = false;
                                        VK1.Ki = null;
                                    }
                                    else
                                    {//a váltó nem oldható fel
                                        MessageBox.Show("A váltó nem oldható fel, mert a vágányúton, amelyben érintett, még nem közlekedett le vonat.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        Ind.Hibak[7]++;
                                    }
                                }
                            }
                            else
                            {//felső >> alsó (oldott) = váltó lezárása egyenesbe
                                if (VK1.Valto.Allas)
                                {
                                    if (VK1.Valto.AllitasAlatt)
                                    {//éppen állítja be a vágányutat
                                        MessageBox.Show("A váltó nem zárható le, mert a váltókezelő éppen egy olyan vágányutat állít be, amelyben a váltó érintett.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        Ind.Hibak[8]++;
                                    }
                                    else
                                    {
                                        if (VK1.Be == null && VK1.Ki == null)
                                        {//a lezárás indokolatlan
                                            MessageBox.Show("A váltó nem zárható le, mert nincs bejelentett vágányút, a szabványos állás pedig az oldott egyenes.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            Ind.Hibak[9]++;
                                        }
                                        else
                                        {
                                            VK1.Valto.Lezart = true;
                                            emeltyu.Allas = EmeltyuAllas.Also;
                                        }
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (emeltyu == R2)
            {
                if (jobbgombos) //a jobb egérgomb hívja meg >> ÜRESBEN MOZGATÁS
                {
                    switch (R2.Allas)
                    {
                        case EmeltyuAllas.Also:
                            if (!VK2.Valto.Lezart)
                            {
                                emeltyu.Allas = EmeltyuAllas.Felso;
                            }
                            break;
                        case EmeltyuAllas.Felso:
                            if (!VK2.Valto.Lezart)
                            {
                                emeltyu.Allas = EmeltyuAllas.Also;
                            }
                            break;
                        default:
                            break;
                    }
                }
                else //a bal egérgomb hívja meg >> KICSAPPANTÁS
                {
                    switch (R2.Allas)
                    {
                        case EmeltyuAllas.Also:
                            if (VK2.Valto.Lezart)
                            {//alsó >> felső (lezárt) = egyenes váltó feloldása
                                if (A1.Allas == EmeltyuAllas.Also && A2.Allas == EmeltyuAllas.Felso)
                                {
                                    if (VK2.Be != null && (SZAKASZOK[3].Vonat == VK2.Be || SZAKASZOK[4].Vonat == VK2.Be || SZAKASZOK[5].Vonat == VK2.Be || SZAKASZOK[6].Vonat == VK2.Be || (!ALLOMAS.Vonatok.Contains(VK2.Be) && ALLOMAS.FogadottVonatok.Contains(VK2.Be))))
                                    {//BEJÁRAT UTÁNI FELOLDÁS - a vonat már megérkezett, bejárat alapban
                                        emeltyu.Allas = EmeltyuAllas.Felso;
                                        VK2.Valto.Lezart
                                            = false;
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
                                            VK2.Valto.AllitasAlatt = true;
                                        }
                                        OK2.Stop();
                                        OK2.Elinditva = false;
                                        VK2.Be = null;
                                    }
                                    else if (VK2.Ki != null && (VK2.Ki == SZAKASZOK[0].Vonat || (!ALLOMAS.Vonatok.Contains(VK2.Ki) && ALLOMAS.FogadottVonatok.Contains(VK2.Ki))))
                                    {//KIJÁRAT UTÁNI FELOLDÁS - a vonat vagy kint van a nyíltvonalon, vagy már lefogadta a szomszéd állomás
                                        emeltyu.Allas = EmeltyuAllas.Felso;
                                        VK2.Valto.Lezart = false;
                                        VK2.ProgressBar = 0;
                                        OK2.Stop();
                                        OK2.Elinditva = false;
                                        VK2.Ki = null;
                                    }
                                    else
                                    {//a váltó nem oldható fel
                                        MessageBox.Show("A váltó nem oldható fel, mert a vágányúton, amelyben érintett, még nem közlekedett le vonat.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        Ind.Hibak[7]++;
                                    }
                                }
                            }
                            else
                            {//alsó >> felső (oldott) = váltó lezárása kitérőbe
                                if (!VK2.Valto.Allas)
                                {
                                    if (VK2.Valto.AllitasAlatt)
                                    {//éppen állítja be a vágányutat
                                        MessageBox.Show("A váltó nem zárható le, mert a váltókezelő éppen egy olyan vágányutat állít be, amelyben a váltó érintett.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        Ind.Hibak[8]++;
                                    }
                                    else
                                    {
                                        if (VK2.Be == null && VK2.Ki == null)
                                        {//a lezárás indokolatlan
                                            MessageBox.Show("A váltó nem zárható le, mert nincs bejelentett vágányút, a szabványos állás pedig az oldott egyenes.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            Ind.Hibak[9]++;
                                        }
                                        else
                                        {
                                            VK2.Valto.Lezart = true;
                                            emeltyu.Allas = EmeltyuAllas.Felso;
                                        }
                                    }
                                }
                            }
                            break;
                        case EmeltyuAllas.Felso:
                            if (VK2.Valto.Lezart)
                            {//felső >> alsó (lezárt) = kitérő váltó feloldása
                                if (A1.Allas == EmeltyuAllas.Also && A2.Allas == EmeltyuAllas.Felso)
                                {
                                    if (VK2.Be != null && (SZAKASZOK[8].Vonat == VK2.Be || SZAKASZOK[9].Vonat == VK2.Be || SZAKASZOK[5].Vonat == VK2.Be || SZAKASZOK[6].Vonat == VK2.Be || (!ALLOMAS.Vonatok.Contains(VK2.Be) && ALLOMAS.FogadottVonatok.Contains(VK2.Be))))
                                    {//BEJÁRAT UTÁNI FELOLDÁS - a vonat már megérkezett, bejárat alapban
                                        emeltyu.Allas = EmeltyuAllas.Also;
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
                                            VK2.Valto.AllitasAlatt = true;
                                        }
                                        OK2.Stop();
                                        OK2.Elinditva = false;
                                        VK2.Be = null;
                                    }
                                    else if (VK2.Ki != null && (VK2.Ki == SZAKASZOK[0].Vonat || (!ALLOMAS.Vonatok.Contains(VK2.Ki) && ALLOMAS.FogadottVonatok.Contains(VK2.Ki))))
                                    {
                                        emeltyu.Allas = EmeltyuAllas.Also;
                                        VK2.Valto.Lezart = false;
                                        VK2.Valto.Allas = true;
                                        VK2.ProgressBar = 0;
                                        OK2.Stop();
                                        OK2.Elinditva = false;
                                        VK2.Ki = null;
                                    }
                                    else
                                    {//a váltó nem oldható fel
                                        MessageBox.Show("A váltó nem oldható fel, mert a vágányúton, amelyben érintett, még nem közlekedett le vonat.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        Ind.Hibak[7]++;
                                    }
                                }
                            }
                            else
                            {//felső >> alsó (oldott) = váltó lezárása egyenesbe
                                if (VK2.Valto.Allas)
                                {
                                    if (VK2.Valto.AllitasAlatt)
                                    {//éppen állítja be a vágányutat
                                        MessageBox.Show("A váltó nem zárható le, mert a váltókezelő éppen egy olyan vágányutat állít be, amelyben a váltó érintett.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        Ind.Hibak[8]++;
                                    }
                                    else
                                    {
                                        if (VK2.Be == null && VK2.Ki == null)
                                        {//a lezárás indokolatlan
                                            MessageBox.Show("A váltó nem zárható le, mert nincs bejelentett vágányút, a szabványos állás pedig az oldott egyenes.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            Ind.Hibak[9]++;
                                        }
                                        else
                                        {
                                            VK2.Valto.Lezart = true;
                                            emeltyu.Allas = EmeltyuAllas.Also;
                                        }
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            if (B1.Allas == B2.Allas)
            {
                SZAKASZOK[6].Jelzo.Szabad = true;
            }
            else
            {
                SZAKASZOK[6].Jelzo.Szabad = false;
                ALLOMAS.VisszjelTimerStart();
            }
            if (A1.Allas == A2.Allas)
            {
                SZAKASZOK[1].Jelzo.Szabad = true;
            }
            else
            {
                SZAKASZOK[1].Jelzo.Szabad = false;
                ALLOMAS.VisszjelTimerStart();
            }
            
            //RETESZEMELTYŰK ÁLLÁSÁNAK VISSZAJELENTÉSE
            if (VK1.Valto.Lezart)
            {
                if (VK1.Valto.Allas)
                {
                    VZK1.Image = Gyermekvasút.Properties.Resources.RET_állásE;
                }
                else
                {
                    VZK1.Image = Gyermekvasút.Properties.Resources.RET_állásK;
                }
            }
            else
            {
                VZK1.Image = Gyermekvasút.Properties.Resources.RET_állás0;
            }

            if (VK2.Valto.Lezart)
            {
                if (VK2.Valto.Allas)
                {
                    VZK2.Image = Gyermekvasút.Properties.Resources.RET_állásE;
                }
                else
                {
                    VZK2.Image = Gyermekvasút.Properties.Resources.RET_állásK;
                }
            }
            else
            {
                VZK2.Image = Gyermekvasút.Properties.Resources.RET_állás0;
            }
            if (AllForm != null)
            {
                AllForm.Frissit();
            }
        }        
    }
    public class OldasKenyszerito : Timer
    {
        Valto valto;
        public Valto Valto
        {
            get { return valto; }
            set { valto = value; }
        }

        bool elinditva;
        public bool Elinditva
        {
            get { return elinditva; }
            set { elinditva = value; }
        }

        bool bejarat;
        public bool Bejarat
        {
            get { return bejarat; }
            set { bejarat = value; }
        }

        public void Start2(bool bejaratE)
        {
            base.Start();
            Bejarat = bejaratE;
        }
    }
}
