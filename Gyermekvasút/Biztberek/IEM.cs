using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gyermekvasút.Modellek.Emeltyűtípusok
{
    interface IEM
    {
        void Allitas(Emeltyu emeltyu, bool jobbgombos = false);
    }
}