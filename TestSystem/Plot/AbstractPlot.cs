using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestSystem.BlackBox;
using TestSystem.DataFormat;
using TestSystem.Drawer;
using TestSystem.Tasks;

namespace TestSystem.Plot
{
    abstract class AbstractPlot:Form, IPlot
    {
        IToolFactory factory;
        protected List<IPoint> userPoints;
        List<List<IPoint>> points;
        private IFunction function;
        private IEnterBlackBoxParam param;
        IColoring coloring;
        INormalize normalize;
        Boolean isNormal = false;
        List<Control> pointsObjs;
        public AbstractPlot(IFunction function, IEnterBlackBoxParam param)
        {
            userPoints = new List<IPoint>();
            this.function = function;
            this.param = param;
            factory = ToolFactory.Create();
            this.Click += AbstractPlot_Click;
            pointsObjs = new List<Control>();
            //this.Paint += AbstractPlot_Paint;
        }

        void AbstractPlot_Click(object sender, EventArgs e)
        {
            if (points != null)
            {
                int maxI = points.Count;
                int maxJ = points[0].Count;
                List<IPoint> focusPoints;
                if (!isNormal)
                    for (int i = 0; i < maxI; i++)
                    {
                        for (int j = 0; j < maxJ; j++)
                        {
                            normalize.Normalization(points[i][j]);
                        }

                    }

                isNormal = true;
                for (int i = 0; i < maxI; i++)
                {
                    for (int j = 0; j < maxJ; j++)
                    {
                        focusPoints = new List<IPoint>();
                        if (points[i].Count != 1)
                        {
                            if (j < maxJ - 1)
                            {
                                focusPoints.Add(points[i][j]);
                                focusPoints.Add(points[i][j + 1]);
                            }



                        }
                        else
                        {
                            focusPoints.Add(points[i][j]);
                        }


                        if (i < maxI - 1)
                        {
                            focusPoints.Add(points[i + 1][j]);
                        }

                        if ((i < maxI - 1) && (j < maxJ - 1))
                        {
                            focusPoints.Add(points[i + 1][j + 1]);
                        }

                        coloring.ColoringSurface(focusPoints);

                    }
                }
            }
        }

        void AbstractPlot_Paint(object sender, PaintEventArgs e)
        {
            if (points != null)
            {
                int maxI = points.Count;
                int maxJ = points[0].Count;
                List<IPoint> focusPoints;
                for (int i = 0; i < maxI; i++)
                {
                    for (int j = 0; j < maxJ; j++)
                    {
                        normalize.Normalization(points[i][j]);
                    }

                }
                for (int i = 0; i < maxI; i++)
                {
                    for (int j = 0; j < maxJ; j++)
                    {
                        focusPoints = new List<IPoint>();
                        if (points[i].Count != 1)
                        {
                            if (j < maxJ - 1)
                            {
                                focusPoints.Add(points[i][j]);
                                focusPoints.Add(points[i][j + 1]);
                            }



                        }
                        else
                        {
                            focusPoints.Add(points[i][j]);
                        }


                        if (i < maxI - 1)
                        {
                            focusPoints.Add(points[i + 1][j]);
                        }

                        if ((i < maxI - 1) && (j < maxJ - 1))
                        {
                            focusPoints.Add(points[i + 1][j + 1]);
                        }

                        coloring.ColoringSurface(focusPoints);

                    }
                }
            }
        }

        public void StartCalculate()
        {
            coloring = factory.CreateColoring(this);
            normalize = factory.CreateNormalize(this);
            points = Calculate(function, param);
            
            
            
            
        }

        protected abstract List<List<IPoint>> Calculate(IFunction function, IEnterBlackBoxParam parameters);
        protected abstract IPoint CreatePoint(Double x1, Double x2, Double cost);

        public void AddPoint(IPoint value)
        {

            IPoint point = CreatePoint(value.x1,value.x2,value.cost);
            normalize.Normalization(point);
            Button btn = new Button();
            btn.BackColor = Color.Red;
            btn.Height = 7;
            btn.Width = 7;
            ToolTip toolTip = new ToolTip();
            btn.Location = new Point((int)point.x1, (int)point.x2); 
            toolTip.SetToolTip(btn,point.x1.ToString()+" "+point.x2.ToString()+" "+point.cost.ToString());
            this.Controls.Add(btn);
            pointsObjs.Add(btn);
            userPoints.Add(point);
        }

        private IPoint tempFind;

        public void DeletePoint(IPoint point)
        {
            tempFind = point;
           int index= userPoints.FindIndex(0,match);
           userPoints.Remove(point);
           this.Controls.Remove(pointsObjs[index]);
           pointsObjs.RemoveAt(index);
        }

        private bool match(IPoint obj)
        {
            return (obj==tempFind);
        }
        public void Clear()
        {
            userPoints.Clear();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AbstractPlot
            // 
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Name = "AbstractPlot";
            this.ResumeLayout(false);

        }
    }
}
