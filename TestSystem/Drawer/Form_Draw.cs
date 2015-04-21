using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestSystem.Plot;
using TestSystem.test_system;
using System.Windows.Input;

namespace TestSystem.Drawer
{
    class Form_Draw: Form//, IEndCalculate
    {
        protected PictureBox pictureBox1;
        protected PictureBox pictureBox2;
        protected Panel panel1;
        protected Panel panel2;
        protected Panel panel3;
        protected Panel panel4;
        protected TrackBar trackBar1;
        protected RadioButton radioButton1;
        protected RadioButton radioButton2;
        private Label label3;
        private Label label2;
        private Label label1;
        private ToolTip toolTip1;


        protected int X = 100, Y = 100;
        protected double mult_color = 1, mult_point = 1, zoom = 1;
        protected int numb_point = 10;
        private static Bitmap image;
        protected List<Button> bts = new List<Button>();
        protected List<IPoint> ptAlg = new List<IPoint>();
        protected Button button1;

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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(535, 396);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(491, 412);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(510, 85);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(160, 340);
            this.panel2.TabIndex = 2;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(160, 340);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.trackBar1);
            this.panel3.Location = new System.Drawing.Point(13, 429);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(393, 59);
            this.panel3.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(359, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "10";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "0";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "0";
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.trackBar1.Enabled = false;
            this.trackBar1.Location = new System.Drawing.Point(3, 2);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(380, 45);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // radioButton2
            // 
            this.radioButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButton2.AutoSize = true;
            this.radioButton2.Enabled = false;
            this.radioButton2.Location = new System.Drawing.Point(6, 33);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(193, 17);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.Text = "Отображать лишь текущую точку";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Enabled = false;
            this.radioButton1.Location = new System.Drawing.Point(6, 5);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(249, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Отображать все точки с первой по текущую";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.radioButton1);
            this.panel4.Controls.Add(this.radioButton2);
            this.panel4.Location = new System.Drawing.Point(410, 429);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(260, 59);
            this.panel4.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.Location = new System.Drawing.Point(509, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Draw";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form_Draw
            // 
            this.ClientSize = new System.Drawing.Size(685, 500);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form_Draw";
            this.Resize += new System.EventHandler(this.Form_Draw_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void UpdateComponent(int x, int y)
        {
            //this.pictureBox1.Size = new System.Drawing.Size(this.Size.Width, this.Size.Height);
            image = new Bitmap(x, y);
            pictureBox1.Image = image;
        }

        //public void ClearBitmap()
        //{
        //    if (X*Y < 65535) // && Y < 65535)
        //    {
        //        X = Convert.ToInt32(X);
        //        Y = Convert.ToInt32(Y);
        //        image = new Bitmap(X, Y);
        //    }
        //    else { X /= 2; Y /= 2; ClearBitmap(); }
        //}

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

        protected void DrawLegend()
        {
            Bitmap img = new Bitmap(panel2.Size.Width, panel2.Size.Height);

            Graphics gr = Graphics.FromImage(img);

            LinearGradientBrush linGrBrushW = new LinearGradientBrush(new Point(0, 0), new Point(0, panel2.Size.Height / 4), Color.White, Color.Red);

            //linGrBrush.GammaCorrection = true;
            gr.FillRectangle(linGrBrushW, 0, 0, panel2.Size.Width / 2, panel2.Size.Height / 4);

            LinearGradientBrush linGrBrushR = new LinearGradientBrush(new Point(0, panel2.Size.Height / 4-1), new Point(0, panel2.Size.Height / 2+1), Color.Red, Color.Green);

            //linGrBrush.GammaCorrection = true;
            gr.FillRectangle(linGrBrushR, 0, panel2.Size.Height / 4-1, panel2.Size.Width / 2, panel2.Size.Height / 4+1);

            LinearGradientBrush linGrBrushG = new LinearGradientBrush(new Point(0, panel2.Size.Height / 2-2), new Point(0, 3 * panel2.Size.Height / 4), Color.Green, Color.Blue);

            //linGrBrush.GammaCorrection = true;
            gr.FillRectangle(linGrBrushG, 0, panel2.Size.Height / 2-1, panel2.Size.Width / 2, panel2.Size.Height / 4);

            LinearGradientBrush linGrBrushB = new LinearGradientBrush(new Point(0, 3 * panel2.Size.Height / 4-2), new Point(0, panel2.Size.Height), Color.Blue, Color.Black);

            //linGrBrush.GammaCorrection = true;
            gr.FillRectangle(linGrBrushB, 0, 3 * panel2.Size.Height / 4 - 2, panel2.Size.Width / 2, panel2.Size.Height / 4);

            DrawText(gr, "Exception", panel2.Size.Width / 2, 0, 120, 20, 12);

            DrawText(gr, ((256 * 256 * 256 - 1) / mult_color).ToString(), panel2.Size.Width / 2, panel2.Size.Height / 4 - 10, 120, 20, 12);

            DrawText(gr, ((256 * 256 - 1) / mult_color).ToString(), panel2.Size.Width / 2, panel2.Size.Height / 2 - 10, 120, 20, 12); 

            DrawText(gr, (255 / mult_color).ToString(), panel2.Size.Width / 2, 3 * panel2.Size.Height / 4 - 10, 120, 20, 12); 

            DrawText(gr, 0.ToString(), panel2.Size.Width / 2, panel2.Size.Height - 20, 120, 20, 12);

            pictureBox2.Image = img;
        }

        private void DrawText(Graphics g, string str, int x, int y, int width, int height, int FrontSize)
        {
            RectangleF rectf = new RectangleF(x, y, width, height);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawString(str, new Font("Tahoma", FrontSize), Brushes.Black, rectf);

            g.Flush();
        }

        public Form_Draw()
        {
            InitializeComponent();

            image = new Bitmap(1000, 1000);

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
            DrawLegend();
        }

        public void Mult_point(double mult)
        {
            mult_point = mult;
        }

        //public void draw()
        //{
        //    IColoring clr = new Coloring_Double(this);
        //    List<IPoint> c = new List<IPoint>();
        //    IPoint p = new PlotPoint(20, 20, 255);
        //    c.Add(p);
        //    p = new PlotPoint(350, 350, 255 * 255);
        //    c.Add(p);
        //    p = new PlotPoint(20, 350, 255 * 255 * 255);
        //    c.Add(p);
        //    clr.ColoringSurface(c);

        //    c = new List<IPoint>();
        //    p = new PlotPoint(350, 20, 255);
        //    c.Add(p);
        //    p = new PlotPoint(360, 130, 255 * 255);
        //    c.Add(p);
        //    clr.ColoringSurface(c);

        //    c = new List<IPoint>();
        //    p = new PlotPoint(150, 20, 255);
        //    c.Add(p);
        //    p = new PlotPoint(180, 20, 255 * 255);
        //    c.Add(p);
        //    p = new PlotPoint(150, 120, 0);
        //    c.Add(p);
        //    p = new PlotPoint(180, 120, 255);
        //    c.Add(p);
        //    clr.ColoringSurface(c);

        //    c = new List<IPoint>();
        //    p = new PlotPoint(300, 20, 0);
        //    c.Add(p);
        //    clr.ColoringSurface(c);
        //}

        public Graphics GetGraphics()
        {
            return pictureBox1.CreateGraphics();
        }

        public Graphics GetImage()
        {
            return Graphics.FromImage(image);
        }

        public void SetDrawNumberPoint(List<IPoint> pt)
        {
            ptAlg = pt;
            numb_point = pt.Count;
            label3.Text = numb_point.ToString();
            trackBar1.Maximum = numb_point;
            bts = new List<Button>(numb_point);
            CreateButtons();

            //DrawAxe();
        }

        public void SetDrawNumberPoint(IPoint pt)
        {
            ptAlg.Add(pt);
            numb_point = ptAlg.Count;
            label3.Text = numb_point.ToString();
            trackBar1.Maximum = numb_point;
            CreateButton();

            //DrawAxe();
        }

        public void DrawAll()
        {
            DrawAxe();

            DrawLegend();
        }

        private void DrawAxe()
        {
            int distance = 5;
            for (int i = 0; i < image.Size.Width; i++)
            {
                image.SetPixel(i, distance, Color.Black);
            }
            for (int i = 0; i < image.Size.Height; i++)
            {
                image.SetPixel(distance, i, Color.Black);
            }
            for (int i = 0; i < distance; i++)
            {
                image.SetPixel(i + image.Size.Width - distance, i, Color.Black);
                image.SetPixel(i + image.Size.Width - distance, 2 * distance - i, Color.Black);
            }
            for (int i = 0; i < distance; i++)
            {
                image.SetPixel(i, i + image.Size.Height - distance, Color.Black);
                image.SetPixel(2 * distance - i, i + image.Size.Height - distance, Color.Black);
            }

            for (int i = 0; i < image.Size.Width; i++)
            {
                image.SetPixel(i, 0, Color.Black);
                image.SetPixel(i, image.Size.Height-1, Color.Black);
            }
            for (int i = 0; i < image.Size.Height; i++)
            {
                image.SetPixel(0, i, Color.Black);
                image.SetPixel(image.Size.Width-1, i, Color.Black);
            }

            Graphics gr = Graphics.FromImage(image); //хз что не так...
            DrawText(gr, "X", 0, image.Size.Width - distance, distance, distance, 10);
            DrawText(gr, "Y", image.Size.Height - distance, 0, distance, distance, 10);
            pictureBox1.Image = image;
        }

        private void CreateButton()
        {
            if(ptAlg != null)
            {
                int size = 4;
                Bitmap back = new Bitmap(size,size);
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        back.SetPixel(i, j, Color.Red);

                int last = bts.Count;
                    bts.Add(new Button());
                    this.panel1.Controls.Add(this.bts[last]);
                    // 
                    // bts[i]
                    // 
                    this.bts[last].Visible = false;
                    this.bts[last].Location = new System.Drawing.Point((int)(ptAlg[last].x1) - 2, (int)(ptAlg[last].x2) - 2);
                    this.bts[last].Name = "bts"+last;
                    this.bts[last].Size = new System.Drawing.Size(size, size);
                    this.bts[last].TabIndex = 1;
                    this.bts[last].Text = " ";
                    toolTip1.SetToolTip(this.bts[last], "Стоимость полученная в этой точке: "+ptAlg[last].cost);
                    this.bts[last].UseVisualStyleBackColor = true;
                    this.bts[last].BackgroundImage = back;
                    //this.bts[i].BackColor = Color.Red;
                    this.bts[last].BringToFront();
                
            }
        }

        private void CreateButtons()
        {
            if (ptAlg != null)
            {
                int size = 4;
                Bitmap back = new Bitmap(size, size);
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        back.SetPixel(i, j, Color.Red);
                for (int i = 0; i < numb_point; i++)
                {
                    bts.Add(new Button());
                    this.panel1.Controls.Add(this.bts[i]);
                    // 
                    // bts[i]
                    // 
                    this.bts[i].Visible = false;
                    this.bts[i].Location = new System.Drawing.Point((int)(ptAlg[i].x1) - 2, (int)(ptAlg[i].x2) - 2);
                    this.bts[i].Name = "bts" + i;
                    this.bts[i].Size = new System.Drawing.Size(size, size);
                    this.bts[i].TabIndex = 1;
                    this.bts[i].Text = " ";
                    toolTip1.SetToolTip(this.bts[i], "Стоимость полученная в этой точке: " + ptAlg[i].cost);
                    this.bts[i].UseVisualStyleBackColor = true;
                    this.bts[i].BackgroundImage = back;
                    //this.bts[i].BackColor = Color.Red;
                    this.bts[i].BringToFront();
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (bts != null)
            {
                label2.Text = trackBar1.Value.ToString();
                for (int i = 0; i < bts.Count; i++)
                {
                    bts[i].Visible = false;
                }
                if (radioButton1.Checked == true)
                    for (int i = 0; i < trackBar1.Value; i++)
                    {
                        bts[i].Visible = true;
                    }
                else if (radioButton2.Checked == true)
                {
                    bts[trackBar1.Value].Visible = true;
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (bts != null)
            {
                for (int i = 0; i < bts.Count; i++)
                {
                    bts[i].Visible = false;
                }
                if (radioButton1.Checked == true)
                    for (int i = 0; i < trackBar1.Value; i++)
                    {
                        bts[i].Visible = true;
                    }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (bts != null)
            {
                for (int i = 0; i < bts.Count; i++)
                {
                    bts[i].Visible = false;
                }
                if (radioButton2.Checked == true)
                {
                    bts[trackBar1.Value].Visible = true;
                }
            }
        }

        private void Form_Draw_Resize(object sender, EventArgs e)
        {
            if (this.ClientSize.Width < 685) this.ClientSize = new System.Drawing.Size(685, this.ClientSize.Height);
            if (this.ClientSize.Height < 500) this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, 500);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                zoom *= 2;
                pictureBox1.Height *= 2;
                pictureBox1.Width *= 2;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                if (ptAlg != null)
                    for (int i = 0; i < numb_point; i++)
                    {
                        this.bts[i].Location = new System.Drawing.Point((this.bts[i].Location.X) * 2, (this.bts[i].Location.Y) * 2);
                        this.bts[i].Size = new System.Drawing.Size(this.bts[i].Size.Width * 2, this.bts[i].Size.Height * 2);
                    }
            }

            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                zoom /= 2;
                pictureBox1.Height /= 2;
                pictureBox1.Width /= 2;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                if (ptAlg != null)
                    for (int i = 0; i < numb_point; i++)
                    {
                        this.bts[i].Location = new System.Drawing.Point((this.bts[i].Location.X) / 2, (this.bts[i].Location.Y) / 2);
                        this.bts[i].Size = new System.Drawing.Size(this.bts[i].Size.Width / 2, this.bts[i].Size.Height / 2);
                    }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                pictureBox1.Height = (int)(pictureBox1.Height / zoom);
                pictureBox1.Width = (int)(pictureBox1.Width / zoom);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                if (ptAlg != null)
                    for (int i = 0; i < numb_point; i++)
                    {
                        this.bts[i].Location = new System.Drawing.Point((int)((this.bts[i].Location.X) / zoom), (int)((this.bts[i].Location.Y) / zoom));
                        this.bts[i].Size = new System.Drawing.Size((int)(this.bts[i].Size.Width / zoom), (int)(this.bts[i].Size.Height / zoom));
                    }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

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
