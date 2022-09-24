using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gyermekvasút
{
    public delegate void FrissuljDelegate();
    public delegate void VonatSzakaszDelegate(Vonat vonat, Szakasz szakasz);
    public delegate void VonatJelzoDelegate(Vonat vonat, Jelzo jelzo);

    public delegate void TelCsengetesDelegate(bool kpFeleHiv);
    public delegate void TelKozlemenyDelegate(bool kpFeleHiv, int kozlemenyTipus, object[] parameters);
    public delegate void TelMegszakitvaDelegate(bool kpFeleHiv);
}