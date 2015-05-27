using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Plot
{
    interface IPlot
    {
        void AddPoint(IPoint point);

        void DeletePoint(IPoint point);

        void Clear();
    }
}
