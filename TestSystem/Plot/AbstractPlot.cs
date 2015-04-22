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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Size = new System.Drawing.Size(1000, 1000);
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button1
            // 
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AbstractPlot
            // 
            this.ClientSize = new System.Drawing.Size(685, 500);
            this.Name = "AbstractPlot";
            this.Shown += new System.EventHandler(this.AbstractPlot_Shown);
            this.Click += new System.EventHandler(this.AbstractPlot_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public AbstractPlot():base()
        {
            InitializeComponent();
        }

        public AbstractPlot(IFunction function, IEnterBlackBoxParam param):base()
        {
            DrawLegend();
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
            //this.ClearBitmap();
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
            //Paint();
        }


        private void addToList(List<IPoint> targetPoints,IPoint point)
        {
            if ((point.x1 != 0) && (point.x2 != 0))
            {
                targetPoints.Add(point);
            }
        }

        private void Paint()
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

                                addToList(focusPoints, points[i][j]);
                                addToList(focusPoints, points[i][j + 1]);
                               // focusPoints.Add(points[i][j]);
                               // focusPoints.Add(points[i][j + 1]);
                            }



                        }
                        else
                        {
                            addToList(focusPoints, points[i][j]);
                           /// focusPoints.Add(points[i][j]);
                        }


                        if (i < maxI - 1)
                        {
                            addToList(focusPoints, points[i + 1][j]);
                            //focusPoints.Add(points[i + 1][j]);
                        }

                        if ((i < maxI - 1) && (j < maxJ - 1))
                        {
                            addToList(focusPoints, points[i + 1][j + 1]);
                            //focusPoints.Add(points[i + 1][j + 1]);
                        }

                        if (focusPoints.Count>0)
                            coloring.ColoringSurface(focusPoints);

                    }
                }
            }
        }

        public void StartCalculate() // Я изменил слегка(что-бы масштабирование работало нормально)
        {
            StartCalc(100);
        }

        private void StartCalc(double mult)
        {
            if ((int)(param.x1_max * param.x2_max * mult) < 65536)
            {
                coloring = factory.CreateColoring(this);
                normalize = factory.CreateNormalize(this, mult);
                X = (int)(X * mult);
                Y = (int)(Y * mult);
                UpdateComponent(X, Y);
                //X *= (int)mult_point; // МАсштабирование!!!!!!!!!!!!!
                //Y *= (int)mult_point;
                points = Calculate(function, param);
                Paint();
                //SetDrawNumberPoint(points[0]); //Изменить список точек на верные.

                //ClickToForm();
            }
            else StartCalc(mult/2);
        }

        protected virtual List<List<IPoint>> Calculate(IFunction function, IEnterBlackBoxParam parameters) { throw new NotImplementedException(); }
        protected virtual IPoint CreatePoint(Double x1, Double x2, Double cost) { throw new NotImplementedException(); }

        public void AddPoint(IPoint value)
        {

            IPoint point = CreatePoint(value.x1, value.x2, value.cost);
            normalize.Normalization(point);
            SetDrawNumberPoint(point);
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

        private void button1_Click(object sender, EventArgs e)
        {
            StartCalculate();
            DrawAll();
            trackBar1.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            button1.Enabled = false;
        }
    }
}
