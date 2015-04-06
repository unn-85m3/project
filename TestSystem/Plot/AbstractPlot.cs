using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Drawer;

namespace TestSystem.Plot
{
    abstract class AbstractPlot:Form_Draw, IPlot
    {
        protected List<IPoint> points;
        public AbstractPlot(List<IPoint> points)
        {
            this.points = points;
        }


        public abstract void AddPoint(IPoint point);

        public abstract void DeletePoint(IPoint point);

        public abstract void Clear();
    }
}
