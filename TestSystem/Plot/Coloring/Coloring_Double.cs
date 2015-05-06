﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using TestSystem.Drawer;

namespace TestSystem.Plot
{
    class Coloring_Double : IColoring
    {
        Form_Draw frmd;
        List<IPoint> p;
        double mult = 10000;


        public Coloring_Double() 
        {
            if (frmd != null)
                frmd.Mult_color(mult);
        }


        public Coloring_Double(Form_Draw frmd)
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
            if (points.Count > 2)
            {
                if (points.Count == 3)
                {
                    TreangleRun(points[2], points[1], points[0]);
                }
                else if (points.Count == 4)
                {


                    //TreangleRun(0);
                    //p.Remove(p[0]);
                    //TreangleRun(1);
                    //TreangleRun(points[0], points[1], points[2]);
                    //TreangleRun(p[3], p[2], p[1]);
                }
            }
            else if (points.Count == 2)
            {
                LineRun(0);
            }
            else if (points.Count == 1)
            {
                PointRun(0);
            }
            //LinearGradientBrush linGrBrush = new LinearGradientBrush(new Point((int)points[0].x1, (int)points[0].x2), new Point((int)points[1].x1, (int)points[1].x2), Color_min, Color_max);

            //    frmd.GetGraphics().FillRectangle(linGrBrush, (int)points[0].x1, (int)points[0].x2, (int)Math.Abs(points[0].x1 - points[1].x1), (int)Math.Abs(points[0].x2 - points[1].x2));
            //   linGrBrush.GammaCorrection = true;
            //   frmd.GetGraphics().FillRectangle(linGrBrush, 0, 60, 200, 50);
        }

        //private void Sort()
        //{
        //    IPoint tmp;
        //    if 
        //}

        private void PointRun(int start)
        {   
            frmd.SetImagePoint((int)p[start].x1, (int)p[start].x2, CostToColor(p[start].cost));
        }

        private void LineRun(int start)
        {
            int x1_min = (int)Math.Min(p[start + 0].x1, p[start + 1].x1);
            int x1_max = (int)Math.Max(p[start + 0].x1, p[start + 1].x1); 
            int y1_min = (int)Math.Min(p[start + 0].x2, p[start + 1].x2);
            int y1_max = (int)Math.Max(p[start + 0].x2, p[start + 1].x2);

            for (int y = y1_min; y <= y1_max; y++)
                for (int x = x1_min; x <= x1_max; x++)
                    if (InLine(x, y, 0))
                frmd.SetImagePoint(x, y, Color.FromArgb((int)ShadeBackgroundPixelLine(x, y)));
        }

        //private void LinesRun(params IPoint[] point)
        //{
        //    Point[] pt = new Point[point.Length];


        //    GraphicsPath path = new GraphicsPath();
        //    path.AddLines(pt);

        //    PathGradientBrush pthGrBrush = new PathGradientBrush(path);

        //    //pthGrBrush.CenterColor = Color.FromArgb(255, 255, 0, 0);

        //    List<Color> clr = new List<Color>();
        //    for (int i = 0; i < 4; i++)
        //    {
        //        if(p1.x1 == x1_min && p1.x2 == y1_min)
        //            clr.Add(CostToColor(p1.cost));
        //        else 
        //    }
        //    Color[] colors = clr.ToArray();


        //    pthGrBrush.SurroundColors = colors;

        //    Graphics g = frmd.GetImage();

        //    g.SmoothingMode = SmoothingMode.AntiAlias;
        //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

        //    g.FillPath(pthGrBrush, path);

        //    g.Flush();
        //}


        private void LineRun(IPoint p1, IPoint p2)
        {
            int x1_min = (int)Math.Min(p1.x1, p2.x1);
            int x1_max = (int)Math.Max(p1.x1, p2.x1);
            int y1_min = (int)Math.Min(p1.x2, p2.x2);
            int y1_max = (int)Math.Max(p1.x2, p2.x2);

            for (int y = y1_min; y <= y1_max; y++)
                for (int x = x1_min; x <= x1_max; x++)
                    if (InLine(x, y, 0))
                        frmd.SetImagePoint(x, y, Color.FromArgb((int)ShadeBackgroundPixelLine(x, y)));
        }

        private void TreangleRun(int start)
        {
            start = 0;
            int x1_min = (int)Math.Min(Math.Min(p[start + 0].x1, p[start + 1].x1), p[start + 2].x1);
            int y1_min = (int)Math.Min(Math.Min(p[start + 0].x2, p[start + 1].x2), p[start + 2].x2);
            int x1_max = (int)Math.Max(Math.Max(p[start + 0].x1, p[start + 1].x1), p[start + 2].x1);
            int y1_max = (int)Math.Max(Math.Max(p[start + 0].x2, p[start + 1].x2), p[start + 2].x2);

            for (int y = y1_min; y <= y1_max; y++)
                for (int x = x1_min; x <= x1_max; x++)
                    if (InTreangle(x, y, start))
                        frmd.SetImagePoint(x, y, Color.FromArgb((int)ShadeBackgroundPixel(x, y)));
        }

        private void TreangleRun(IPoint p1, IPoint p2, IPoint p3)
        {
            int x1_min = (int)Math.Min(Math.Min(p1.x1, p2.x1), p3.x1);
            int y1_min = (int)Math.Min(Math.Min(p1.x2, p2.x2), p3.x2);
            int x1_max = (int)Math.Max(Math.Max(p1.x1, p2.x1), p3.x1);
            int y1_max = (int)Math.Max(Math.Max(p1.x2, p2.x2), p3.x2);

            for (int y = y1_min; y <= y1_max; y++)
                for (int x = x1_min; x <= x1_max; x++)
                    if (InTreangle(x, y, p1, p2, p3))
                        frmd.SetImagePoint(x, y, CostToColor((int)ShadeBackgroundPixel(x, y, p1, p2, p3)));
        }

        private bool InLine(int x, int y, int start)
        {
            if (Math.Abs(Math.Sqrt(Math.Pow((p[start + 0].x1 - x), 2) + Math.Pow((p[start + 0].x2 - y), 2)) + Math.Sqrt(Math.Pow((p[start + 1].x1 - x), 2) + Math.Pow((p[start + 1].x2 - y), 2)) - Math.Sqrt(Math.Pow((p[start + 1].x1 - p[start + 0].x1), 2) + Math.Pow((p[start + 1].x2 - p[start + 0].x2), 2))) < 0.02)
                return true;
            else return false;
        }

        private bool InLine(int x, int y, IPoint p1, IPoint p2)
        {
            if (Math.Abs(Math.Sqrt(Math.Pow((p1.x1 - x), 2) + Math.Pow((p1.x2 - y), 2)) + Math.Sqrt(Math.Pow((p2.x1 - x), 2) + Math.Pow((p2.x2 - y), 2)) - Math.Sqrt(Math.Pow((p2.x1 - p1.x1), 2) + Math.Pow((p2.x2 - p1.x2), 2))) < 0.02)
                return true;
            else return false;
        }

        private bool InTreangle(int x, int y, int start)
        {
            int a = (int)((int)p[start + 0].x1 - x) * ((int)p[start + 1].x2 - (int)p[start + 0].x2) - ((int)p[start + 1].x1 - (int)p[start + 0].x1) * ((int)p[start + 0].x2 - y);
            int b = (int)((int)p[start + 1].x1 - x) * ((int)p[start + 2].x2 - (int)p[start + 1].x2) - ((int)p[start + 2].x1 - (int)p[start + 1].x1) * ((int)p[start + 1].x2 - y);
            int c = (int)((int)p[start + 2].x1 - x) * ((int)p[start + 0].x2 - (int)p[start + 2].x2) - ((int)p[start + 0].x1 - (int)p[start + 2].x1) * ((int)p[start + 2].x2 - y);

            if ((a >= 0 && b >= 0 && c >= 0) || (a <= 0 && b <= 0 && c <= 0))
            {
                return true;
            }
            else return false;
        }

        private bool InTreangle(int x, int y, IPoint p1, IPoint p2, IPoint p3)
        {
            int a = (int)((int)p1.x1 - x) * ((int)p2.x2 - (int)p1.x2) - ((int)p2.x1 - (int)p1.x1) * ((int)p1.x2 - y);
            int b = (int)((int)p2.x1 - x) * ((int)p3.x2 - (int)p2.x2) - ((int)p3.x1 - (int)p2.x1) * ((int)p2.x2 - y);
            int c = (int)((int)p3.x1 - x) * ((int)p1.x2 - (int)p3.x2) - ((int)p1.x1 - (int)p3.x1) * ((int)p3.x2 - y);

            if ((a >= 0 && b >= 0 && c >= 0) || (a <= 0 && b <= 0 && c <= 0))
            {
                return true;
            }
            else return false;
        }

        private UInt32 ShadeBackgroundPixelLine(int x, int y)
        {
            UInt32 pixelValue;
            pixelValue = 0xFFFFFFFF;
            int c1, c2;

            int x_min = (int)(Math.Min(p[0].x1, p[1].x1));
            int x_max = (int)(Math.Max(p[0].x1, p[1].x1));
            int y_min = (int)(Math.Min(p[0].x2, p[1].x2));
            int y_max = (int)(Math.Max(p[0].x2, p[1].x2));

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

            c1 = (int)(p[0].cost * mult);
            if (c1 > 256 * 256 * 256)
                c1 = 256 * 256 * 256 - 1;
            else if (c1 < 0)
                c1 = 0;

            c2 = (int)(p[1].cost * mult);
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

        private UInt32 ShadeBackgroundPixel(int x, int y)
        {
            UInt32 pixelValue; //цвет пикселя с координатами (x, y)

            double l1, l2, l3;
            pixelValue = 0xFFFFFFFF; //присваиваем цвет фона - белый
            l1 = ((p[1].x2 - p[2].x2) * ((double)(x) - p[2].x1) + (p[2].x1 - p[1].x1) * ((double)(y) - p[2].x2)) /
                ((p[1].x2 - p[2].x2) * (p[0].x1 - p[2].x1) + (p[2].x1 - p[1].x1) * (p[0].x2 - p[2].x2));
            l2 = ((p[2].x2 - p[0].x2) * ((double)(x) - p[2].x1) + (p[0].x1 - p[2].x1) * ((double)(y) - p[2].x2)) /
                ((p[1].x1 - p[2].x2) * (p[0].x1 - p[2].x1) + (p[2].x1 - p[1].x1) * (p[0].x2 - p[2].x2));
            l3 = 1 - l1 - l2;
            if (l1 >= 0 && l1 <= 1 && l2 >= 0 && l2 <= 1 && l3 >= 0 && l3 <= 1)
            {
                int c1, c2, c3;

                c1 = (int)(p[0].cost * mult);
                if (c1 > 256 * 256 * 256)
                    c1 = 256 * 256 * 256 - 1;
                else if (c1 < 0)
                    c1 = 0;

                c2 = (int)(p[1].cost * mult);
                if (c2 > 256 * 256 * 256)
                    c2 = 256 * 256 * 256 - 1;
                else if (c2 < 0)
                    c2 = 0;

                c3 = (int)(p[2].cost * mult);
                if (c3 > 256 * 256 * 256)
                    c3 = 256 * 256 * 256 - 1;
                else if (c3 < 0)
                    c3 = 0;

                pixelValue = (UInt32)0xFF000000 |
                    ((UInt32)(l1 * ((c1 & 0x00FF0000) >> 16) + l2 * ((c2 & 0x00FF0000) >> 16) + l3 * ((c3 & 0x00FF0000) >> 16)) << 16) |
                    ((UInt32)(l1 * ((c1 & 0x0000FF00) >> 8) + l2 * ((c2 & 0x0000FF00) >> 8) + l3 * ((c3 & 0x0000FF00) >> 8)) << 8) |
                    (UInt32)(l1 * (c1 & 0x000000FF) + l2 * (c2 & 0x000000FF) + l3 * (c3 & 0x000000FF));
            }
            return pixelValue;
        }

        private UInt32 ShadeBackgroundPixel(int x, int y, IPoint p1, IPoint p2, IPoint p3)
        {
            UInt32 pixelValue; //цвет пикселя с координатами (x, y)

            double l1, l2, l3;
            pixelValue = 0xFFFFFFFF; //присваиваем цвет фона - белый
            l1 = ((p2.x2 - p3.x2) * ((double)(x) - p3.x1) + (p3.x1 - p2.x1) * ((double)(y) - p3.x2)) /
                ((p2.x2 - p3.x2) * (p1.x1 - p3.x1) + (p3.x1 - p2.x1) * (p1.x2 - p3.x2));
            l2 = ((p3.x2 - p1.x2) * ((double)(x) - p3.x1) + (p1.x1 - p3.x1) * ((double)(y) - p3.x2)) /
                ((p2.x1 - p3.x2) * (p1.x1 - p3.x1) + (p3.x1 - p2.x1) * (p1.x2 - p3.x2));
            l3 = 1 - l1 - l2;
            if (l1 >= 0 && l1 <= 1 && l2 >= 0 && l2 <= 1 && l3 >= 0 && l3 <= 1)
            {
                int c1, c2, c3;

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

                c3 = (int)(p3.cost * mult);
                if (c3 > 256 * 256 * 256)
                    c3 = 256 * 256 * 256 - 1;
                else if (c3 < 0)
                    c3 = 0;

                pixelValue = (UInt32)0xFF000000 |
                    ((UInt32)(l1 * ((c1 & 0x00FF0000) >> 16) + l2 * ((c2 & 0x00FF0000) >> 16) + l3 * ((c3 & 0x00FF0000) >> 16)) << 16) |
                    ((UInt32)(l1 * ((c1 & 0x0000FF00) >> 8) + l2 * ((c2 & 0x0000FF00) >> 8) + l3 * ((c3 & 0x0000FF00) >> 8)) << 8) |
                    (UInt32)(l1 * (c1 & 0x000000FF) + l2 * (c2 & 0x000000FF) + l3 * (c3 & 0x000000FF));
            }
            return pixelValue;
        }

        private Color CostToColor(double cost)
        {
            cost *= mult;
            if (cost < 256 * 256 * 256 && cost > 0)
                return Color.FromArgb((((int)cost / 256 / 256) % 256), (((int)cost / 256) % 256), (int)cost % 256);
            else if (cost <= 0)
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
