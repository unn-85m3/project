using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestSystem.Plot;
using TestSystem.test_system;

namespace TestSystem.Drawer
{
    class Form_Draw: Form//, IEndCalculate
    {
        private static Bitmap image;
        protected System.Windows.Forms.PictureBox pictureBox1;

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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(this.Size.Width, this.Size.Height);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;

            this.Controls.Add(this.pictureBox1);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }

        public void UpdateComponent()
        {
            this.pictureBox1.Size = new System.Drawing.Size(this.Size.Width, this.Size.Height);
            image = new Bitmap(1000, 1000);
            pictureBox1.Image = image;
        }

        public void ClearBitmap()
        {
            image = new Bitmap(1000, 1000);
        }

        public void SetImage(Image img)
        {
            pictureBox1.Image = img;
        }

        public void SetImagePoint(int x, int y, Color clr)
        {
            if (x < image.Size.Width && y < image.Size.Height && x > 0 && y > 0)
            {
                image.SetPixel(x, y, clr);
                pictureBox1.Image = image;
            }
        }

        public Form_Draw()
        {
            InitializeComponent();

            image = new Bitmap(1000, 1000);
        }

        public void draw()
        {
            IColoring clr = new Coloring(this);
            List<IPoint> c = new List<IPoint>();
            IPoint p = new PlotPoint(20, 20, 255);
            c.Add(p);
            p = new PlotPoint(350, 350, 255 * 255);
            c.Add(p);
            p = new PlotPoint(20, 350, 255 * 255 * 255);
            c.Add(p);
            clr.ColoringSurface(c);

            c = new List<IPoint>();
            p = new PlotPoint(350, 20, 255);
            c.Add(p);
            p = new PlotPoint(360, 130, 255 * 255);
            c.Add(p);
            clr.ColoringSurface(c);

            c = new List<IPoint>();
            p = new PlotPoint(150, 20, 255);
            c.Add(p);
            p = new PlotPoint(180, 20, 255 * 255);
            c.Add(p);
            p = new PlotPoint(150, 120, 0);
            c.Add(p);
            p = new PlotPoint(180, 120, 255);
            c.Add(p);
            clr.ColoringSurface(c);

            c = new List<IPoint>();
            p = new PlotPoint(300, 20, 0);
            c.Add(p);
            clr.ColoringSurface(c);
        }

        public Graphics GetGraphics()
        {
            return pictureBox1.CreateGraphics();
        }

        //public void OnEndTask(Algorithm.IAlgorithm alg, Tasks.ITaskPackage task, DataFormat.IOutBlackBoxParam rez, int time)
        //{
        //    //Graphics g = pictureBox1.CreateGraphics();
        //    //LinearGradientBrush linGrBrush = new LinearGradientBrush(new Point(0, 10), new Point(200, 10), Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 0, 0, 255));

        //    //Pen pen = new Pen(linGrBrush);

        //    //g.DrawLine(pen, 0, 10, 200, 10);
        //    //g.FillEllipse(linGrBrush, 0, 30, 200, 100);
        //    //g.FillRectangle(linGrBrush, 0, 155, 500, 30);

            
        //}
    }
}
