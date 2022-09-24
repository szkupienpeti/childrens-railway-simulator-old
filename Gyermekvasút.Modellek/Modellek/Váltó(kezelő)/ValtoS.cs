using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gyermekvasút
{
    public class ValtoS : Valto
    {
        public ValtoS(Szakasz E, Szakasz K, string NAME)
        {
            Allas = true;
            SzakaszE = E;
            SzakaszK = K;
            Name = NAME;
        }

        int timerTick;
        public int TimerTick
        {
            get { return timerTick; }
            set { timerTick = value; }
        }

        bool szcsE;
        public bool SzcsE
        {
            get { return szcsE; }
            set { szcsE = value; }
        }

        bool szcsK;
        public bool SzcsK
        {
            get { return szcsK; }
            set { szcsK = value; }
        }

        bool valtoallMouseMove;
        public bool ValtoallMouseMove
        {
            get { return valtoallMouseMove; }
            set { valtoallMouseMove = value; }
        }
    }
}
