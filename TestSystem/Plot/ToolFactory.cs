using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestSystem.Plot
{
    class ToolFactory:IToolFactory
    {
        private static IToolFactory FACTORY = new ToolFactory();
        protected ToolFactory()
        {
        }



        public static IToolFactory Create()
        {
            return FACTORY;
        }

        public IColoring CreateColoring(System.Windows.Forms.Form form)
        {
            return new TempColoring(form);
        }

        public INormalize CreateNormalize(System.Windows.Forms.Form form)
        {
            return new Normalize(form);
        }


        protected class TempColoring : IColoring
        {
            Form form;
            public TempColoring(Form form)
            {
                this.form = form;
            }

            public void ColoringSurface(List<IPoint> points)
            {
                Graphics g = form.CreateGraphics();
                foreach (IPoint point in points)
                {

                    g.FillEllipse(Brushes.Red, (float)point.x1, (float)point.x2, (float)5, (float)5);
                }
            }


            public void SetForm_Draw(Drawer.Form_Draw frmd)
            {
                throw new NotImplementedException();
            }
        }

    }


   
}
