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
    partial class Form_Draw: Form//, IEndCalculate
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
        protected int numb_point = 0;
        private static Bitmap image;
        protected List<Button> bts = new List<Button>();
        protected List<Bitmap> bms = new List<Bitmap>();
        protected List<IPoint> ptAlg = new List<IPoint>();
        protected Button button1;

        

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
            //bts = new List<Button>(numb_point);
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

        public void ClearDrawNumberPoints()
        {
            ptAlg = null;
            numb_point = 0;
            label3.Text = numb_point.ToString();
            trackBar1.Maximum = numb_point;
        }

        public void DrawAll()
        {
            DrawAxe();

            DrawLegend();

            CreateBmps();
            DrawBmps();
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

            //for (int i = 0; i < image.Size.Width; i++)
            //{
            //    image.SetPixel(i, 0, Color.Black);
            //    image.SetPixel(i, image.Size.Height-1, Color.Black);
            //}
            //for (int i = 0; i < image.Size.Height; i++)
            //{
            //    image.SetPixel(0, i, Color.Black);
            //    image.SetPixel(image.Size.Width-1, i, Color.Black);
            //}

            Graphics gr = Graphics.FromImage(image); //хз что не так...
            DrawText(gr, "X", 0, image.Size.Width - distance, distance*2, distance*2, 10);
            DrawText(gr, "Y", image.Size.Height - distance, 0, distance*2, distance*2, 10);
            pictureBox1.Image = image;
        }

        private void CreateButton()
        {
            if (ptAlg != null)
            {
                int size = 4;
                Bitmap back = new Bitmap(size, size);
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
                this.bts[last].Name = "bts" + last;
                this.bts[last].Size = new System.Drawing.Size(size, size);
                this.bts[last].TabIndex = 1;
                this.bts[last].Text = " ";
                toolTip1.SetToolTip(this.bts[last], "Стоимость полученная в этой точке: " + ptAlg[last].cost);
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

        private void CreateBmps()
        {
            if (ptAlg != null)
            {
                int best = 0; 
                
                // 
                // bts[i]
                // 
                for (int i = 0; i < numb_point; i++)
                {
                    bms.Add(new Bitmap("1.png"));
                    if (ptAlg[best].cost > ptAlg[i].cost)
                        best = i;
                    //bts[i].MakeTransparent(Color.Red);

                    DrawPlus(i);
                }
                if (numb_point > 0)
                    label4.Text = "Лучшее значение было полученно в точке №" + (best + 1) + ". И cost = " + ptAlg[best].cost;
            }
        }

        private void DrawBmps()
        {
            pictureBox1.CreateGraphics().DrawImage(image, 0, 0);
            for (int i = 0; i < bms.Count; i++)
            {
                pictureBox1.CreateGraphics().DrawImage(bms[i], 0, 0);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (bms != null)
            {
                label2.Text = trackBar1.Value.ToString();
                if (trackBar1.Value > 0)
                    label2.Text += (". Cost = " + ptAlg[trackBar1.Value - 1].cost);
                for (int i = 0; i < bts.Count; i++)
                {
                    bts[i].Visible = false;
                }
                if (radioButton1.Checked == true)
                    for (int i = 0; i < trackBar1.Value; i++)
                    {
                        bts[i].Visible = true;
                        //ShowAllPointTo(trackBar1.Value);
                    }
                else if (radioButton2.Checked == true)
                {
                    if (trackBar1.Value > 0)
                        bts[trackBar1.Value - 1].Visible = true;
                    //ShowOnePointTo(trackBar1.Value - 1);
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (bms != null)
            {
                for (int i = 0; i < bts.Count; i++)
                {
                    bts[i].Visible = false;
                }
                if (radioButton1.Checked == true)
                {
                    //ShowAllPointTo(trackBar1.Value);
                    for (int i = 0; i < trackBar1.Value; i++)
                    {
                        bts[i].Visible = true;
                    }
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (bms != null)
            {
                for (int i = 0; i < bts.Count; i++)
                {
                    bts[i].Visible = false;
                }
                if (radioButton2.Checked == true)
                {
                    //ShowOnePointTo(trackBar1.Value - 1);
                    if (trackBar1.Value > 0)
                        bts[trackBar1.Value - 1].Visible = true;
                }

            }
        }

        private void ShowOnePointTo(int numb)
        {
            for (int i = 0; i < bms.Count; i++)
            {
                bms[i].MakeTransparent(Color.Red);
            }
            bms[numb].MakeTransparent(Color.White);
            DrawBmps();
        }

        private void ShowAllPointTo(int numb)
        {
            for (int i = 0; i < numb; i++)
            {
                bms[i].MakeTransparent(Color.White);
            }
            for (int i = numb; i < bms.Count; i++)
                    bms[i].MakeTransparent(Color.Red);
            DrawBmps();
        }

        private void DrawPlus(int numb)
        {
            Graphics fig = Graphics.FromImage(bms[numb]);
            fig.FillEllipse(new SolidBrush(Color.Red), (int)ptAlg[numb].x1, (int)ptAlg[numb].x2, 5, 5);
            bms[numb].MakeTransparent(Color.Red);
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
                if (ptAlg != null)
                    for (int i = 0; i < numb_point; i++)
                    {
                        this.bts[i].Location = new System.Drawing.Point((int)((ptAlg[i].x1 - 2) * zoom), (int)((ptAlg[i].x2 - 2) * zoom));
                        this.bts[i].Size = new System.Drawing.Size(this.bts[i].Size.Width * 2, this.bts[i].Size.Height * 2);
                    }
                pictureBox1.Height *= 2;
                pictureBox1.Width *= 2;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                //pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            }

            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                zoom /= 2;
                if (ptAlg != null)
                    for (int i = 0; i < numb_point; i++)
                    {
                        this.bts[i].Location = new System.Drawing.Point((int)((ptAlg[i].x1 - 2) * zoom), (int)((ptAlg[i].x2 - 2) * zoom));
                        this.bts[i].Size = new System.Drawing.Size(this.bts[i].Size.Width / 2, this.bts[i].Size.Height / 2);
                    }
                pictureBox1.Height /= 2;
                pictureBox1.Width /= 2;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                //pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                if (ptAlg != null)
                    for (int i = 0; i < numb_point; i++)
                    {
                        this.bts[i].Location = new System.Drawing.Point((int)((ptAlg[i].x1 - 2)), (int)((ptAlg[i].x2 - 2)));
                        this.bts[i].Size = new System.Drawing.Size(4, 4);
                    }
                pictureBox1.Height = (int)(pictureBox1.Height / zoom);
                pictureBox1.Width = (int)(pictureBox1.Width / zoom);
                //pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                zoom = 1;
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
