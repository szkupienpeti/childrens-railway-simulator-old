using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gyermekvasút
{
    public class Jelzo : AzonosithatoObj
    {//MEGÁLLAPODÁS SZERINT MINDEN SZAKASZ JELZO-JÉN ANNAK A PÁROS FELÉN LÉVŐ JELZŐT ÉRTJÜK!

        public Jelzo(bool parosIranybaNezE, string NAME, bool fenyjelzoE)
        {
            ParosIranybaNez = parosIranybaNezE;
            Name = NAME;
            fenyjelzo = fenyjelzoE;
        }

        public Jelzo(bool parosIranybaNezE, bool menesztes, string NAME, bool fenyjelzoE)
        {
            ParosIranybaNez = parosIranybaNezE;
            Menesztes = menesztes;
            Name = NAME;
            fenyjelzo = fenyjelzoE;
        }

        bool szabad;
        public bool Szabad
        {
            get { return szabad; }
            set { szabad = value; }
        }

        bool parosIranybaNez;
        public bool ParosIranybaNez
        {
            get { return parosIranybaNez; }
            set { parosIranybaNez = value; }
        }

        bool menesztes;
        public bool Menesztes
        {
            get { return menesztes; }
            set { menesztes = value; }
        }

        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        bool vonatMeghaladta;
        public bool VonatMeghaladta
        {
            get { return vonatMeghaladta; }
            set { vonatMeghaladta = value; }
        }

        public override string ToString()
        {
            return Name;
        }

        bool fenyjelzo;
        public bool Fenyjelzo
        {
            get { return fenyjelzo; }
            set { fenyjelzo = value; }
        }
    }
}
