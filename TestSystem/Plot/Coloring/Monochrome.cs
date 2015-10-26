using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Plot.Coloring
{
    class Monochrome:Coloring_dotNet
    {
        
        public Monochrome(Drawer.Form_Draw form):base(form)
        {
        }

        protected override System.Drawing.Color CostToColor(double cost)
        {
            
            if (cost > 255)
                cost = 255;
           // else
            //    cost /= 3;
            if (cost <= 256)
                return Color.FromArgb((int)cost, 0, 0);
            else if (cost < 0)
                return Color.FromArgb(0, 0, 0);
            else
                return Color.FromArgb(255, 255, 255);
            //return base.CostToColor(cost);
        }
    }
}
