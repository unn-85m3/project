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
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 338);
            this.Name = "Form1";
            this.Text = "Form1";
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
            this.tabControl1.Size = new System.Drawing.Size(this.Size.Width - 17, this.Size.Height - 41);
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
    }
}

