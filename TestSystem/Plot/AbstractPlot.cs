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
    class AbstractPlot:Form_Draw, IPlot
    {
        private static Bitmap image;
        IToolFactory factory;
        protected List<IPoint> userPoints;
        List<List<IPoint>> points;
        private IFunction function;
        private IEnterBlackBoxParam param;
        IColoring coloring;
        INormalize normalize;
        Boolean isNormal = false;
        List<Control> pointsObjs;

        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AbstractPlot));
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 341);
            this.pictureBox1.Size = new System.Drawing.Size(1000, 1000);
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // AbstractPlot
            // 
            this.ClientSize = new System.Drawing.Size(695, 460);
            this.Name = "AbstractPlot";
            this.Shown += new System.EventHandler(this.AbstractPlot_Shown);
            this.Click += new System.EventHandler(this.AbstractPlot_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public AbstractPlot():base()
        {
            InitializeComponent();
        }

        public AbstractPlot(IFunction function, IEnterBlackBoxParam param):base()
        {
            DrawLegeng();
            userPoints = new List<IPoint>();
            this.function = function;
            this.param = param;
            factory = ToolFactory.Create();
            this.Click += AbstractPlot_Click;
            pointsObjs = new List<Control>();
            //this.Paint += AbstractPlot_Paint;
            InitializeComponent();
        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //ClickToForm();
        }

        void AbstractPlot_Click(object sender, EventArgs e)
        {
            //ClickToForm();
        }

        protected void ClickToForm()
        {
            this.ClearBitmap();
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
            //ClickToForm();
        }

        protected virtual List<List<IPoint>> Calculate(IFunction function, IEnterBlackBoxParam parameters) { throw new NotImplementedException(); }
        protected virtual IPoint CreatePoint(Double x1, Double x2, Double cost) { throw new NotImplementedException(); }

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

        private void AbstractPlot_Shown(object sender, EventArgs e)
        {
            ClickToForm();
        }
    }
}
