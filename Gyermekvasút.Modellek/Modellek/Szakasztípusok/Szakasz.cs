using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gyermekvasút
{
    public class Szakasz : AzonosithatoObj
    {//MEGÁLLAPODÁS SZERINT MINDEN SZAKASZ JELZO-JÉN ANNAK A PÁROS FELÉN LÉVŐ JELZŐT ÉRTJÜK!

        public Szakasz() { }

        public Szakasz(int szakaszHossza, Jelzo parosOldalonLevoJelzo, string NAME)
        {
            Hossz = szakaszHossza;
            Jelzo = parosOldalonLevoJelzo;
            Name = NAME;
        }

        int hossz;
        public int Hossz
        {
            get { return hossz; }
            set { hossz = value; }
        }

        Vonat vonat;
        public Vonat Vonat
        {
            get { return vonat; }
            set { vonat = value; }
        }

        Jelzo jelzo;
        public Jelzo Jelzo
        {
            get { return jelzo; }
            set { jelzo = value; }
        }

        public bool Foglalt
        {
            get
            {
                if (vonat == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        bool lezart;
        public bool Lezart
        {
            get { return lezart; }
            set { lezart = value; }
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

        /*bool vgutbanOldott;
        public bool VgutbanOldott
        {
            get { return vgutbanOldott; }
            set { vgutbanOldott = value; }
        }*/
    }
}
