using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gyermekvasút
{
    public class Vaganyut : AzonosithatoObj
    {
        public Vaganyut(Jelzo kp, string vgutNev, bool bejaratE, Szakasz szakasz1, Szakasz szakasz2, Szakasz szakasz3, string NAME)
        {
            Kezdopont = kp;
            Nev = vgutNev;
            Bejarat = bejaratE;
            Szakaszok.Add(szakasz1);
            Szakaszok.Add(szakasz2);
            Szakaszok.Add(szakasz3);
            Name = NAME;
        }

        Jelzo kezdopont;
        public Jelzo Kezdopont
        {
            get { return kezdopont; }
            set { kezdopont = value; }
        }

        bool felepitett;
        public bool Felepitett
        {
            get { return felepitett; }
            set { felepitett = value; }
        }

        public bool KezdoJelzoSzabad
        {
            get
            {
                if (kezdopont.Szabad)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        bool jelzoMegallj;
        public bool JelzoMegallj
        {
            get { return jelzoMegallj; }
            set { jelzoMegallj = value; }
        }

        string nev;
        public string Nev
        {
            get { return nev; }
            set { nev = value; }
        }

        bool bejarat;
        public bool Bejarat
        {
            get { return bejarat; }
            set { bejarat = value; }
        }

        List<Szakasz> szakaszok = new List<Szakasz>();
        public List<Szakasz> Szakaszok
        {
            get { return szakaszok; }
            set { szakaszok = value; }
        }

        public bool BejarandóSzakaszokSzabadok
        {
            get
            {
                if (Bejarat)
                {
                    //bejárat
                    if (!Szakaszok[0].Foglalt && !Szakaszok[1].Foglalt && !Szakaszok[2].Foglalt)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    //kijárat
                    if (!Szakaszok[1].Foglalt && !Szakaszok[2].Foglalt)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
