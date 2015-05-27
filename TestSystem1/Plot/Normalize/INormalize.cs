using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Plot
{
    interface INormalize
    {
        void Normalization(IPoint point);
        double Zoom();
    }
}
