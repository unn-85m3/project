using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestSystem.Drawer;

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

        public IColoring CreateColoring(Form_Draw form)
        {
            return new Coloring(form);
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
                Button btn;
                ToolTip toolTip = new ToolTip();
                foreach (IPoint point in points)
                {
                    btn = new Button();
                    btn.BackColor = Color.Red;
                    btn.Height = 7;
                    btn.Width = 7;
                    
                    btn.Location = new Point((int)point.x1, (int)point.x2);
                   // toolTip.SetToolTip(btn, point.cost.ToString());
                    toolTip.SetToolTip(btn, point.x1.ToString() + " " + point.x2.ToString() + " " + point.cost.ToString());
                    form.Controls.Add(btn);
                   // g.FillEllipse(Brushes.Red, (float)point.x1, (float)point.x2, (float)5, (float)5);
                }
            }


            public void SetForm_Draw(Drawer.Form_Draw frmd)
            {
                throw new NotImplementedException();
            }
        }

    }


   
}
