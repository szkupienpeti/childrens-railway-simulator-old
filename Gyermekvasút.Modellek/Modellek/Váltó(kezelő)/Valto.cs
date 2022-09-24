using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gyermekvasút
{
    public class Valto : AzonosithatoObj
    {
        public Valto()
        {
            Allas = true;
        }

        public Valto(Szakasz E, Szakasz K, string NAME)
        {
            Allas = true;
            SzakaszE = E;
            SzakaszK = K;
            Name = NAME;
        }

        public ValtoS ConvertValtoToValtoS()
        {
            ValtoS vs = new ValtoS(SzakaszE, SzakaszK, Name);
            return vs;
        }

        bool allas;
        public bool Allas
        {
            get { return allas; }
            set { allas = value; }
        }

        bool allitasAlatt;
        public bool AllitasAlatt
        {
            get { return allitasAlatt; }
            set { allitasAlatt = value; }
        }

        bool lezart;
        public bool Lezart
        {
            get { return lezart; }
            set { lezart = value; }
        }

        Szakasz szakaszE;
        public Szakasz SzakaszE
        {
            get { return szakaszE; }
            set { szakaszE = value; }
        }

        Szakasz szakaszK;
        public Szakasz SzakaszK
        {
            get { return szakaszK; }
            set { szakaszK = value; }
        }

        bool vgutasAllitas;
        public bool VgutasAllitas
        {
            get { return vgutasAllitas; }
            set { vgutasAllitas = value; }
        }

        public bool Foglalt
        {
            get
            {
                if (szakaszE.Foglalt == false && szakaszK.Foglalt == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
