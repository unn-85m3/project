using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestSystem.Drawer;

namespace TestSystem.Plot
{
    class Normalize:INormalize
    {
        Form form;
        int N = 0;
        Double X1;
        Double X2;

        Double mX1=100;
        Double mX2=100;

        public Normalize(Form_Draw form, Double koef = 10)
        {
            this.form = form;
            mX1 = mX2 = koef;
            form.Zoom_draw((int)koef);
            form.Mult_point(koef);
        }

        public void Normalization(IPoint point)
        {
            if (N == 0)
            {
                X1 = point.x1 * (-1);
                X2 = point.x2 * (-1);
            }
            else
            {
               
            }

            point.x1 *= mX1;
            point.x2 *= mX2;
            point.x1 = Math.Round(point.x1);
            point.x2 = Math.Round(point.x2);
            /* point.x1 += X1;
             point.x2 += X2;
             
             point.x1+=100;
             point.x2 +=100;*/
            N++;
        }
    }
}
