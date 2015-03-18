using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TestSystem.Algorithm.Diagonal_Algoritm;
//using System.Windows.Forms.

namespace TestSystem.Plot
{
    class Plot3D:Form
    {

        List<Point3D> _points;
        List<IPoint> points;
        List<PointF> _pointsF;
        ChartArea area;
        Chart chart;

        public Plot3D(List<IPoint> points)
        {
            _points = new List<Point3D>();
            _pointsF = new List<PointF>();
            this.points = points;
            foreach (IPoint point in points)
            {
                _points.Add(new Point3D((float)point.x1, (float)point.x2, (float)point.cost.Cost));
               // _pointsF.Add(new PointF());
            }
            
            
           // List<Point3D> 
            InitializeComponent();
            
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // _3DPlot
            // 
            this.ClientSize = new System.Drawing.Size(289, 261);
            this.Name = "3DPlot";
            this.ResumeLayout(false);
            this.Width = 500;
            this.Height = 500;


            area = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            Legend legend = new System.Windows.Forms.DataVisualization.Charting.Legend();
            Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
            chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(chart)).BeginInit();
            SuspendLayout();
            //	
            //	chart
            //	
            area.Name = "ChartArea";
            chart.ChartAreas.Add(area);
            chart.ChartAreas["ChartArea"].Area3DStyle.Rotation = 30;
            chart.ChartAreas["ChartArea"].Area3DStyle.Inclination = 10;
            legend.Name = "Legend";
            chart.Legends.Add(legend);
            chart.Location = new System.Drawing.Point(12, 9);
            chart.Name = "chart";
            series.ChartArea = "ChartArea";
            series.Legend = "Legend";
            series.Name = "Series";
            //			series.ChartType			=	System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart.Series.Add(series);
            chart.Size = new System.Drawing.Size(300, 300);
            chart.TabIndex = 0;
            chart.Text = "chart";
            chart.PostPaint += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ChartPaintEventArgs>(chart_PostPaint);
            chart.Width = this.Width;
            chart.Height = this.Height;

            
           // point = e.ChartGraphics.GetAbsolutePoint(point3dmas[0].PointF);

           // chart1.Series[0].Points.AddXY(point3dmas[0].PointF.X, point3dmas[0].PointF.Y);

            Controls.Add(chart);

            
            
            
            

            


        }



        




        private void chart_PostPaint(object sender, System.Windows.Forms.DataVisualization.Charting.ChartPaintEventArgs e)
        {
            if (sender is System.Windows.Forms.DataVisualization.Charting.Chart)
            {
                System.Windows.Forms.DataVisualization.Charting.Chart area = (System.Windows.Forms.DataVisualization.Charting.Chart)sender;
                if (area.Name == "chart")
                {

                    float depth = 10;		//	(float)area.GetSeriesDepth(chart.Series[0]);
                    float zpos = 10;		//	(float)area.GetSeriesZPosition(chart.Series[0]);
                   // this.area.TransformPoints(_points.ToArray());

                    int i = 0;
                    Graphics graph = e.ChartGraphics.Graphics;
                   // _pointsF.Add(new PointF());
                    foreach (Point3D _point in _points)
                   {
                       _pointsF.Add( e.ChartGraphics.GetAbsolutePoint(_points[i].PointF));
                       if (i != 0)
                       {

                           graph.DrawLine(new Pen(Color.Black, 1), _pointsF[i-1], _pointsF[i]);
                          // DrawArrow(graph, Color.Black, _pointsF[i - 1], _pointsF[i], points[i].cost.Cost);
                       }
                       i++;
                   }
                    chart.Update();

                    int n = points.Count;

                    
                  /* DrawArrow(graph, Color.Black, ptF3, ptF2, 22.5);
                   DrawArrow(graph, Color.Black, ptF6, ptF5, 22.5);*/
                    /*ptF1 = 
                    ptF3 = e.ChartGraphics.GetAbsolutePoint(pt3d[1].PointF);
                    ptF4 = e.ChartGraphics.GetAbsolutePoint(pt3d[2].PointF);
                    ptF6 = e.ChartGraphics.GetAbsolutePoint(pt3d[3].PointF);
                    ptF2.X = ptF3.X;
                    ptF2.Y = ptF1.Y;
                    ptF5.X = ptF6.X;
                    ptF5.Y = ptF4.Y;
                    Graphics graph = e.ChartGraphics.Graphics;
                    graph.DrawLine(new Pen(Color.Black, 1), ptF1, ptF2);
                    graph.DrawLine(new Pen(Color.Black, 1), ptF2, ptF3);
                    graph.DrawLine(new Pen(Color.Black, 1), ptF4, ptF5);
                    graph.DrawLine(new Pen(Color.Black, 1), ptF5, ptF6);
                    DrawArrow(graph, Color.Black, ptF3, ptF2, 22.5);
                    DrawArrow(graph, Color.Black, ptF6, ptF5, 22.5);*/
                }
            }
        }



        /// <summary>	This Method will draw an arrow head on at the end of a line segment as defined by two points, p1 and p2	</summary>
        private void DrawArrow(Graphics graph, Color brushcolor, PointF p1, PointF p2, double angle_deg)
        {
            double rad = angle_deg * Math.PI / 180;
            double dx = p1.X - p2.X;
            double dy = p1.Y - p2.Y;
            double c = Math.Sqrt((Math.Pow(dx, 2) + Math.Pow(dy, 2)));
            double pixels = 12;
            double line_length = (1 / (c / pixels));
            PointF arrow_segment1 = System.Drawing.Point.Empty;
            PointF arrow_segment2 = System.Drawing.Point.Empty;
            arrow_segment1.X = p1.X - (float)((dx * Math.Cos(rad) - dy * Math.Sin(rad)) * line_length);
            arrow_segment1.Y = p1.Y - (float)((dy * Math.Cos(rad) + dx * Math.Sin(rad)) * line_length);
            arrow_segment2.X = p1.X - (float)((dx * Math.Cos(-rad) - dy * Math.Sin(-rad)) * line_length);
            arrow_segment2.Y = p1.Y - (float)((dy * Math.Cos(-rad) + dx * Math.Sin(-rad)) * line_length);
            PointF[] arrowhead = { p1, arrow_segment1, arrow_segment2 };
            SolidBrush brush = new SolidBrush(brushcolor);
            graph.FillPolygon(brush, arrowhead);
        }	











    }
}
