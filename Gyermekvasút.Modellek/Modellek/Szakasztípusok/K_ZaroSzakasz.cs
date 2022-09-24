using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gyermekvasút
{
    public class K_ZaroSzakasz : Szakasz
    {//MEGÁLLAPODÁS SZERINT MINDEN SZAKASZ JELZO-JÉN ANNAK A PÁROS FELÉN LÉVŐ JELZŐT ÉRTJÜK!
        public K_ZaroSzakasz(int szakaszHossza, Jelzo parosOldalonLevoJelzo, bool parosIranybaZarE, string NAME)
        {
            Hossz = szakaszHossza;
            Jelzo = parosOldalonLevoJelzo;
            ParosIranybaZar = parosIranybaZarE;
            Name = NAME;
        }

        bool parosIranybaZar;
        public bool ParosIranybaZar
        {
            get { return parosIranybaZar; }
            set { parosIranybaZar = value; }
        }
    }
}
