using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Plot
{
    abstract class AbstractPlot:IPlot
    {
        protected List<IPoint> points;
        public AbstractPlot(List<IPoint> points)
        {
            this.points = points;
        }
        
    }
}
