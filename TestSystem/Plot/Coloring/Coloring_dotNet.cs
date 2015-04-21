using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Drawer;

namespace TestSystem.Plot
{
    class Coloring_dotNet : IColoring
    {
        Form_Draw frmd;
        List<IPoint> p;
        double mult = 10000;

        public Coloring_dotNet() 
        {
            if (frmd != null)
                frmd.Mult_color(mult);
        }


        public Coloring_dotNet(Form_Draw frmd)
        {
            this.frmd = frmd;
            frmd.Mult_color(mult);
        }

        public void SetForm_Draw(Form_Draw frmd)
        {
            this.frmd = frmd;
            frmd.Mult_color(mult);
            //frmd.ClearBitmap();
        }

        public void ColoringSurface(List<IPoint> points)
        {
            if (points.Count > 0)
            {
                if (points.Count == 1 && points[0].x1 != 0 && points[0].x2 != 0)
                {
                    p = points;
                    List<PointF> point = new List<PointF>();
                    point.Add(new PointF((float)p[0].x1, (float)p[0].x2));
                    PointF[] pt = point.ToArray();

                    GraphicsPath path = new GraphicsPath();
                    path.AddLines(pt);

                    PathGradientBrush pthGrBrush = new PathGradientBrush(path);

                    //pthGrBrush.CenterColor = Color.FromArgb(255, 255, 0, 0);

                    List<Color> clr = new List<Color>();
                    clr.Add(CostToColor(p[0].cost));
                    Color[] colors = clr.ToArray();


                    pthGrBrush.SurroundColors = colors;

                    Graphics g = frmd.GetImage();

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    g.FillPath(pthGrBrush, path);

                    g.Flush();
                }
                else if (points[0].x1 != 0)
                {
                    p = points;
                    List<PointF> point = new List<PointF>();
                    for (int i = 0; i < p.Count; i++)
                        point.Add(new PointF((float)p[i].x1, (float)p[i].x2));
                    PointF[] pt = point.ToArray();

                    GraphicsPath path = new GraphicsPath();
                    path.AddLines(pt);

                    PathGradientBrush pthGrBrush = new PathGradientBrush(path);

                    //pthGrBrush.CenterColor = Color.FromArgb(255, 255, 0, 0);

                    List<Color> clr = new List<Color>();
                    for (int i = 0; i < p.Count; i++)
                        clr.Add(CostToColor(p[i].cost));
                    Color[] colors = clr.ToArray();


                    pthGrBrush.SurroundColors = colors;

                    Graphics g = frmd.GetImage();

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    g.FillPath(pthGrBrush, path);

                    g.Flush();
                }
            }
        }

        private Color CostToColor(double cost)
        {
            cost *= mult;
            if (cost < 256 * 256 * 256)
                return Color.FromArgb((int)cost % 256, (((int)cost  / 256) % 256), ((int)cost / 256 / 256) % 256);
            else if (cost < 0)
                return Color.FromArgb(0, 0, 0);
            else
                return Color.FromArgb(255, 255, 255);
        }

        public double MultColor
        {
            get
            {
                return mult;
            }
            set
            {
                mult = value;
            }
        }
    }
}
