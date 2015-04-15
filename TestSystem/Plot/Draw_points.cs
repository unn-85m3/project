using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Plot
{
    class Draw_points
    {
        private Graphics g;
        List<IPoint> pt;

        public Draw_points(Graphics gr, List<IPoint> p)
        {
            g = gr;
            pt = p;
        }

        public Graphics Graph
        {
            get { return g; }
            set { g = value; }
        }

        public List<IPoint> Points
        {
            get { return pt; }
            set { pt = value; }
        }

        public void DrawPoint(int n)
        {

        }

        public void DrawPoints(int n)
        {

        }
    }
}
