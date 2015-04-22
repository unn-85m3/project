namespace TestSystem
{
    partial class Form1
    {
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTipTabPages = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.buttonShow = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(13, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Export Excel";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.buttonShow);
            this.panel1.Controls.Add(this.maskedTextBox2);
            this.panel1.Controls.Add(this.maskedTextBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonStart);
            this.panel1.Location = new System.Drawing.Point(727, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 239);
            this.panel1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(13, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Open BD";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Enabled = false;
            this.buttonStart.Location = new System.Drawing.Point(13, 211);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 25);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start Tests";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Location = new System.Drawing.Point(727, 247);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(100, 68);
            this.panel2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 7);
            this.label1.MaximumSize = new System.Drawing.Size(90, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 52);
            this.label1.TabIndex = 2;
            this.label1.Text = "Пожалуйста укажите диапозон Тестов:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 70);
            this.label2.MaximumSize = new System.Drawing.Size(90, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Стартовый номер теста:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 127);
            this.label3.MaximumSize = new System.Drawing.Size(90, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Итоговый номер теста:";
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maskedTextBox1.Location = new System.Drawing.Point(13, 99);
            this.maskedTextBox1.Mask = "00000";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.PromptChar = ' ';
            this.maskedTextBox1.Size = new System.Drawing.Size(75, 20);
            this.maskedTextBox1.TabIndex = 5;
            this.maskedTextBox1.Text = "1";
            this.maskedTextBox1.ValidatingType = typeof(int);
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maskedTextBox2.Location = new System.Drawing.Point(13, 156);
            this.maskedTextBox2.Mask = "00000";
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.PromptChar = ' ';
            this.maskedTextBox2.Size = new System.Drawing.Size(75, 20);
            this.maskedTextBox2.TabIndex = 6;
            this.maskedTextBox2.Text = "2";
            this.maskedTextBox2.ValidatingType = typeof(int);
            // 
            // buttonShow
            // 
            this.buttonShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShow.Location = new System.Drawing.Point(13, 182);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(75, 23);
            this.buttonShow.TabIndex = 7;
            this.buttonShow.Text = "Show Table";
            this.buttonShow.UseVisualStyleBackColor = true;
            this.buttonShow.Click += new System.EventHandler(this.buttonShow_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 326);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(800, 365);
            this.Name = "Form1";
            this.Text = "Test system";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Создание вкладок и таблиц на них.
        /// </summary>
        private void InitTab()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPages = new System.Windows.Forms.TabPage[Algorithms.Length];
            this.dataGridViews = new System.Windows.Forms.DataGridView[Algorithms.Length];

            //tt = new System.Collections.Generic.List<System.Windows.Forms.ToolTip>();///////////////////////////////////////////////////////////////////

            this.tabControl1.SuspendLayout();

            for (int i = 0; i < Algorithms.Length; i++)
            {
                tabPages[i] = new System.Windows.Forms.TabPage();
                dataGridViews[i] = new System.Windows.Forms.DataGridView();

                ((System.ComponentModel.ISupportInitialize)(this.dataGridViews[i])).BeginInit();

                this.tabPages[i].SuspendLayout();


                this.tabControl1.Controls.Add(this.tabPages[i]);
            }
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(1, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(this.Size.Width - 137, this.Size.Height - 41);
            //this.toolTipTabPages.SetToolTip(this.tabControl1, "Результаты работы алгоритмов");
            this.tabControl1.ShowToolTips = true;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPages
            // 
            for (int i = 0; i < Algorithms.Length; i++)
            {
                this.tabPages[i].Controls.Add(this.dataGridViews[i]);
                this.tabPages[i].Location = new System.Drawing.Point(1, 1);
                this.tabPages[i].Name = "tabPage1";
                this.tabPages[i].Padding = new System.Windows.Forms.Padding(3);
                this.tabPages[i].Size = new System.Drawing.Size(this.tabControl1.Size.Width, this.tabControl1.Size.Height);
                this.tabPages[i].TabIndex = 0;
                this.tabPages[i].Text = Algs[i].Name;
                this.tabPages[i].UseVisualStyleBackColor = true;
                //this.toolTipTabPages.SetToolTip(this.tabPages[i], Algs[i].Atributs);
                this.tabPages[i].ToolTipText = Algs[i].Atributs;
                //tt.Add(new System.Windows.Forms.ToolTip());
                //tt[i].SetToolTip(tabPages[i], Algs[i].Atributs);
            }
            // 
            // dataGridViews
            // 
            for (int i = 0; i < Algorithms.Length; i++)
            {
                this.dataGridViews[i].Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
                this.dataGridViews[i].ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                this.dataGridViews[i].Location = new System.Drawing.Point(1, 1);
                this.dataGridViews[i].Name = "dataGridView1";
                this.dataGridViews[i].Size = new System.Drawing.Size(this.tabPages[i].Size.Width - 1, this.tabPages[i].Size.Height - 1);
                this.dataGridViews[i].TabIndex = 0;
                this.dataGridViews[i].CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViews_CellDoubleClick);
            }


            dataGridViews[0].Columns.Add("Task", "Задача");
            this.dataGridViews[0].Columns[0].ToolTipText = "Имена файлов примеров.";
            dataGridViews[0].Columns.Add("Time", "t, мсек.");
            this.dataGridViews[0].Columns[1].ToolTipText = "Время работы алгоритма на данном примере.";
            dataGridViews[0].Columns.Add("Func", "Кол-во вызовов ф-ции");
            this.dataGridViews[0].Columns[2].ToolTipText = "Колличество вызовов функции расчета.";
            dataGridViews[0].Columns.Add("BB", "Кол-во вызовов ЧЯ");
            this.dataGridViews[0].Columns[3].ToolTipText = "Колличество вызовов функции расчета черного ящика.";
            dataGridViews[0].Columns.Add("Cost", "Стоимость");
            this.dataGridViews[0].Columns[4].ToolTipText = "Затраты на эксплуатацию и модернизацию.";

            foreach (System.Windows.Forms.DataGridViewColumn column in dataGridViews[0].Columns)
            {
                column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            }
            //dataGridViews[0].SortedColumn. = false;


            for (int i = 1; i < Algorithms.Length; i++)
            {
                dataGridViews[i].Columns.Add("Task", "Задача");
                this.dataGridViews[i].Columns[0].ToolTipText = "Имена файлов примеров.";
                dataGridViews[i].Columns.Add("Time", "t, мсек.");
                this.dataGridViews[i].Columns[1].ToolTipText = "Время работы алгоритма на данном примере.";
                dataGridViews[i].Columns.Add("Func", "Кол-во вызовов ф-ции");
                this.dataGridViews[i].Columns[2].ToolTipText = "Колличество вызовов функции расчета.";
                dataGridViews[i].Columns.Add("BB", "Кол-во вызовов ЧЯ");
                this.dataGridViews[i].Columns[3].ToolTipText = "Колличество вызовов функции расчета черного ящика.";
                dataGridViews[i].Columns.Add("Cost", "Стоимость");
                this.dataGridViews[i].Columns[4].ToolTipText = "Затраты на эксплуатацию и модернизацию.";
                dataGridViews[i].Columns.Add("TimePercent", "% t, мсек.");
                this.dataGridViews[i].Columns[5].ToolTipText = "< 0, результат \"лучше\" чем в эталонном алгоритме.";
                dataGridViews[i].Columns.Add("CostPercent", "% Стоимость"); 
                this.dataGridViews[i].Columns[6].ToolTipText = "< 0, результат \"лучше\" чем в эталонном алгоритме.";

                foreach (System.Windows.Forms.DataGridViewColumn column in dataGridViews[i].Columns)
                {
                    column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
                }
            }


            this.tabControl1.ResumeLayout(false);
            for (int i = 0; i < Algorithms.Length; i++)
            {
                this.tabPages[i].ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.dataGridViews[i])).EndInit();
            }

            this.Controls.Add(this.tabControl1);
            this.ResumeLayout(false);

            Create_Rows();
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage[] tabPages;
        private System.Windows.Forms.DataGridView[] dataGridViews;
        private System.Windows.Forms.ToolTip toolTipTabPages;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Button buttonShow;
    }
}

