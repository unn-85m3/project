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
        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox2;

        protected int X = 100, Y = 100;
        double mult_color = 1;

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(410, 360);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 360);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(420, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(160, 360);
            this.panel2.TabIndex = 2;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(140, 360);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // Form_Draw
            // 
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form_Draw";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void UpdateComponent()
        {
            //this.pictureBox1.Size = new System.Drawing.Size(this.Size.Width, this.Size.Height);
            image = new Bitmap(100, 100);
            pictureBox1.Image = image;
        }

        public void ClearBitmap()
        {
            image = new Bitmap(X, Y);
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
                //Size_draw(x, y);
                pictureBox1.Size = new System.Drawing.Size(X, Y);
            }
        }

        protected void DrawLegeng()
        {
            Bitmap img = new Bitmap(panel2.Size.Width, panel2.Size.Height);

            Graphics gr = Graphics.FromImage(img);

            LinearGradientBrush linGrBrushW = new LinearGradientBrush(new Point(0, 0), new Point(0, panel2.Size.Height / 4), Color.White, Color.Red);

            //linGrBrush.GammaCorrection = true;
            gr.FillRectangle(linGrBrushW, 0, 0, panel2.Size.Width / 2, panel2.Size.Height / 4);

            LinearGradientBrush linGrBrushR = new LinearGradientBrush(new Point(0, panel2.Size.Height / 4), new Point(0, panel2.Size.Height / 2), Color.Red, Color.Green);

            //linGrBrush.GammaCorrection = true;
            gr.FillRectangle(linGrBrushR, 0, panel2.Size.Height / 4, panel2.Size.Width / 2, panel2.Size.Height / 4);

            LinearGradientBrush linGrBrushG = new LinearGradientBrush(new Point(0, panel2.Size.Height / 2 - 1), new Point(0, 3 * panel2.Size.Height / 4), Color.Green, Color.Blue);

            //linGrBrush.GammaCorrection = true;
            gr.FillRectangle(linGrBrushG, 0, panel2.Size.Height / 2 - 1, panel2.Size.Width / 2, panel2.Size.Height / 4);

            LinearGradientBrush linGrBrushB = new LinearGradientBrush(new Point(0, 3 * panel2.Size.Height / 4-1), new Point(0, panel2.Size.Height), Color.Blue, Color.Black);

            //linGrBrush.GammaCorrection = true;
            gr.FillRectangle(linGrBrushB, 0, 3 * panel2.Size.Height / 4 - 1, panel2.Size.Width / 2, panel2.Size.Height / 4);

            DrawText(gr, "Exception", panel2.Size.Width / 2, 0, 120, 20);

            DrawText(gr, ((256 * 256 * 256 - 1) / mult_color).ToString(), panel2.Size.Width / 2, panel2.Size.Height / 4 - 10, 120, 20);

            DrawText(gr, ((256 * 256 - 1) / mult_color).ToString(), panel2.Size.Width / 2, panel2.Size.Height / 2 - 10, 120, 20); 

            DrawText(gr, (255 / mult_color).ToString(), panel2.Size.Width / 2, 3 * panel2.Size.Height / 4 - 10, 120, 20); 

            DrawText(gr, 0.ToString(), panel2.Size.Width / 2, panel2.Size.Height - 20, 120, 20);

            pictureBox2.Image = img;
        }

        private void DrawText(Graphics g, string str, int x, int y, int width, int height)
        {
            RectangleF rectf = new RectangleF(x, y, width, height);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawString(str, new Font("Tahoma", 12), Brushes.Black, rectf);

            g.Flush();
        }

        public Form_Draw()
        {
            InitializeComponent();

            image = new Bitmap(1000, 1000);

            DrawLegeng();

        }

        //public void Size_draw(double x, double y)
        //{
        //    if (X < x) X = (int)(x + 1);
        //    if (Y < y) Y = (int)(y + 1);
        //}

        public void Zoom_draw(int zoom)
        {
            X *= (int)zoom;
            Y *= (int)zoom;
        }

        public void Mult_color(double mult)
        {
            mult_color = mult; 
            DrawLegeng();
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
