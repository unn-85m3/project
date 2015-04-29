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
            p = points;
            if (points.Count > 0)
            {
                if (points.Count == 1)
                {
                    frmd.SetImagePoint((int)p[0].x1, (int)p[0].x2, CostToColor(p[0].cost));
                }
                else if (points.Count == 2)
                {
                    LineRun(p[0], p[1]);
                }
                else if (points.Count > 2)
                {
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

        private void LineRun(IPoint p1, IPoint p2)
        {
            int x1_min = (int)Math.Min(p1.x1, p2.x1);
            int x1_max = (int)Math.Max(p1.x1, p2.x1);
            int y1_min = (int)Math.Min(p1.x2, p2.x2);
            int y1_max = (int)Math.Max(p1.x2, p2.x2);

            for (int y = y1_min; y <= y1_max; y++)
                for (int x = x1_min; x <= x1_max; x++)
                    if (InLine(x, y, p1, p2))
                        frmd.SetImagePoint(x, y, Color.FromArgb((int)ShadeBackgroundPixelLine(x, y, p1, p2)));
        }

        private bool InLine(int x, int y, IPoint p1, IPoint p2)
        {
            if (Math.Abs(Math.Sqrt(Math.Pow((p1.x1 - x), 2) + Math.Pow((p1.x2 - y), 2)) + Math.Sqrt(Math.Pow((p2.x1 - x), 2) + Math.Pow((p2.x2 - y), 2)) - Math.Sqrt(Math.Pow((p2.x1 - p1.x1), 2) + Math.Pow((p2.x2 - p1.x2), 2))) < 0.02)
                return true;
            else return false;
        }

        private UInt32 ShadeBackgroundPixelLine(int x, int y, IPoint p1, IPoint p2)
        {
            UInt32 pixelValue;
            pixelValue = 0xFFFFFFFF;
            int c1, c2;

            int x_min = (int)(Math.Min(p1.x1, p2.x1));
            int x_max = (int)(Math.Max(p1.x1, p2.x1));
            int y_min = (int)(Math.Min(p1.x2, p2.x2));
            int y_max = (int)(Math.Max(p1.x2, p2.x2));

            double l1 = 0.0, l2 = 1.0;

            if (x_min == x_max)
            {
                if (y_max == y_min)
                {
                    l1 = ((x - x_min) + (y - y_min));
                    l2 = 1 - l1;
                }
                else
                {
                    l1 = ((x - x_min) + (double)(y - y_min) / (y_max - y_min));
                    l2 = 1 - l1;
                }
            }
            else
            {
                if (y_max == y_min)
                {
                    l1 = ((double)(x - x_min) / (x_max - x_min) + (y - y_min));
                    l2 = 1 - l1;
                }
                else
                {
                    l1 = ((double)(x - x_min) / (x_max - x_min) + (double)(y - y_min) / (y_max - y_min)) / 2;
                    l2 = 1 - l1;
                }
            }

            c1 = (int)(p1.cost * mult);
            if (c1 > 256 * 256 * 256)
                c1 = 256 * 256 * 256 - 1;
            else if (c1 < 0)
                c1 = 0;

            c2 = (int)(p2.cost * mult);
            if (c2 > 256 * 256 * 256)
                c2 = 256 * 256 * 256 - 1;
            else if (c2 < 0)
                c2 = 0;

            pixelValue = (UInt32)0xFF000000 |
                    ((UInt32)(l1 * ((c1 & 0x00FF0000) >> 16) + l2 * ((c2 & 0x00FF0000) >> 16)) << 16) |
                    ((UInt32)(l1 * ((c1 & 0x0000FF00) >> 8) + l2 * ((c2 & 0x0000FF00) >> 8)) << 8) |
                    (UInt32)(l1 * (c1 & 0x000000FF) + l2 * (c2 & 0x000000FF));

            return pixelValue;
        }

        private Color CostToColor(double cost)
        {
            cost *= mult;
            if (cost < 256 * 256 * 256)
                return Color.FromArgb((((int)cost / 256 / 256) % 256), (((int)cost  / 256) % 256), (int)cost % 256);
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
