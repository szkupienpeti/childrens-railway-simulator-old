using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gyermekvasút.Modellek.Emeltyűtípusok;

namespace Gyermekvasút
{
    public partial class Emeltyu : UserControl
    {
        private EmeltyuAllas allas;
        public EmeltyuAllas Allas
        {
            get { return allas; }
            set
            {
                allas = value;

                switch (allas)
                {
                    case EmeltyuAllas.Also:
                        this.BackgroundImage = AlsoAllas;
                        break;
                    case EmeltyuAllas.Felso:
                        this.BackgroundImage = FelsoAllas;
                        break;
                    default:
                        break;
                }
            }
        }

        private Image felsoAllas;
        public Image FelsoAllas
        {
            get { return felsoAllas; }
            set
            {
                felsoAllas = value;
                Size = felsoAllas.Size;
                if (Allas == EmeltyuAllas.Felso)
                    BackgroundImage = FelsoAllas;
            }
        }

        private Image alsoAllas;
        public Image AlsoAllas
        {
            get { return alsoAllas; }
            set
            {
                alsoAllas = value;
                Size = alsoAllas.Size;
                if (Allas == EmeltyuAllas.Also)
                    BackgroundImage = AlsoAllas;
            }
        }

        private EM vezerloEM;
        public EM VezerloEM
        {
            get { return vezerloEM; }
            set
            {
                vezerloEM = value;
                this.Parent = vezerloEM;
            }
        }

        EmeltyuTipus tipus;
        public EmeltyuTipus Tipus
        {
            get { return tipus; }
            set
            {
                tipus = value;
                switch (tipus)
                {
                    case EmeltyuTipus.AEJ:
                        AlsoAllas = Gyermekvasút.Properties.Resources.Ej_lent;
                        FelsoAllas = Gyermekvasút.Properties.Resources.Ej_fent;
                        Allas = EmeltyuAllas.Also;
                        break;
                    case EmeltyuTipus.A1:
                        AlsoAllas = Gyermekvasút.Properties.Resources.A1_lent;
                        FelsoAllas = Gyermekvasút.Properties.Resources.A1_fent;
                        Allas = EmeltyuAllas.Also;
                        break;
                    case EmeltyuTipus.A2:
                        AlsoAllas = Gyermekvasút.Properties.Resources.A2_lent;
                        FelsoAllas = Gyermekvasút.Properties.Resources.A2_fent;
                        Allas = EmeltyuAllas.Felso;
                        break;
                    case EmeltyuTipus.B1:
                        AlsoAllas = Gyermekvasút.Properties.Resources.B1_lent;
                        FelsoAllas = Gyermekvasút.Properties.Resources.B1_fent;
                        Allas = EmeltyuAllas.Also;
                        break;
                    case EmeltyuTipus.B2:
                        AlsoAllas = Gyermekvasút.Properties.Resources.B2_lent;
                        FelsoAllas = Gyermekvasút.Properties.Resources.B2_fent;
                        Allas = EmeltyuAllas.Felso;
                        break;
                    case EmeltyuTipus.BEJ:
                        AlsoAllas = Gyermekvasút.Properties.Resources.Ej_lent;
                        FelsoAllas = Gyermekvasút.Properties.Resources.Ej_fent;
                        Allas = EmeltyuAllas.Also;
                        break;
                    case EmeltyuTipus.R1:
                        AlsoAllas = Gyermekvasút.Properties.Resources.R1_lent;
                        FelsoAllas = Gyermekvasút.Properties.Resources.R1_fent;
                        Allas = EmeltyuAllas.Also;
                        break;
                    case EmeltyuTipus.R2:
                        AlsoAllas = Gyermekvasút.Properties.Resources.R2_lent;
                        FelsoAllas = Gyermekvasút.Properties.Resources.R2_fent;
                        Allas = EmeltyuAllas.Also;
                        break;
                    default:
                        break;
                }
            }
        }

        public Emeltyu()
        {
            InitializeComponent();
        }

        private void Emeltyu_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                VezerloEM.Allitas(this);
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                VezerloEM.Allitas(this, true);
            }
        }

        private void Emeltyu_Load(object sender, EventArgs e)
        {

        }
    }
}