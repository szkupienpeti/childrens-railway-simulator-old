using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gyermekvasút
{
    public class CsucsSzakasz : Szakasz
    {//MEGÁLLAPODÁS SZERINT MINDEN SZAKASZ JELZO-JÉN ANNAK A PÁROS FELÉN LÉVŐ JELZŐT ÉRTJÜK!
        public CsucsSzakasz(int szakaszHossza, Jelzo parosOldalonLevoJelzo, Valto valtoBemenet, bool valtoParosOldalanVanE, string NAME)
        {
            Hossz = szakaszHossza;
            Jelzo = parosOldalonLevoJelzo;
            Valto = valtoBemenet;
            ValtoParosOldalan = valtoParosOldalanVanE;
            Name = NAME;
        }

        Valto valto;
        public Valto Valto
        {
            get { return valto; }
            set { valto = value; }
        }

        bool valtoParosOldalan;
        public bool ValtoParosOldalan
        {
            get { return valtoParosOldalan; }
            set { valtoParosOldalan = value; }
        }
    }
}
