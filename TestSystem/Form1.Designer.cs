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
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 338);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        private void CreateCheckBox()
        {
            this.groupBoxAlgorithms = new System.Windows.Forms.GroupBox();
            this.checkBoxsAlgorithms = new System.Windows.Forms.CheckBox[Algorithms.GetAlgorithms.Count];
            this.groupBoxAlgorithms.SuspendLayout();
            this.SuspendLayout();
            // 
            //groupBoxAlgorithms
            // 
            //this.groupBoxAlgorithms.Controls.Add(this.checkBoxsAlgorithms);
            //this.groupBoxAlgorithms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            //| System.Windows.Forms.AnchorStyles.Left)
            //| System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAlgorithms.Location = new System.Drawing.Point(12, 12);
            this.groupBoxAlgorithms.Name = "groupBoxAlgorithms";
            this.groupBoxAlgorithms.Size = new System.Drawing.Size(210, 250);
            this.groupBoxAlgorithms.TabIndex = 0;
            this.groupBoxAlgorithms.TabStop = false;
            this.groupBoxAlgorithms.Text = "Algorithms";
            // 
            //checkBoxsAlgorithms
            // 
            for (int i = 0; i < Algorithms.GetAlgorithms.Count; i++)
            {
                this.checkBoxsAlgorithms[i] = new System.Windows.Forms.CheckBox();
                this.groupBoxAlgorithms.Controls.Add(this.checkBoxsAlgorithms[i]);
                this.checkBoxsAlgorithms[i].AutoSize = true;
                this.checkBoxsAlgorithms[i].Location = new System.Drawing.Point(7, 20 + i * 20);
                this.checkBoxsAlgorithms[i].Name = "checkBox" + Algorithms.GetAlgorithms[i].Name;
                this.checkBoxsAlgorithms[i].Size = new System.Drawing.Size(80, 17);
                this.checkBoxsAlgorithms[i].TabIndex = 0;
                this.checkBoxsAlgorithms[i].Text = Algorithms.GetAlgorithms[i].Name;
                this.checkBoxsAlgorithms[i].UseVisualStyleBackColor = true;
            }
            this.Controls.Add(this.groupBoxAlgorithms);
            this.groupBoxAlgorithms.ResumeLayout(false);
            this.groupBoxAlgorithms.PerformLayout();
        }


        /// <summary>
        /// Создание вкладок и таблиц на них.
        /// </summary>
        private void InitTab()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPages = new System.Windows.Forms.TabPage[Algorithms.GetAlgorithms.Count];
            this.dataGridViews = new System.Windows.Forms.DataGridView[Algorithms.GetAlgorithms.Count];
            this.tabControl1.SuspendLayout();

            for (int i = 0; i < Algorithms.GetAlgorithms.Count; i++)
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
            this.tabControl1.TabIndex = 0;
            // 
            // tabPages
            // 
            for (int i = 0; i < Algorithms.GetAlgorithms.Count; i++)
            {
                this.tabPages[i].Controls.Add(this.dataGridViews[i]);
                this.tabPages[i].Location = new System.Drawing.Point(1, 1);
                this.tabPages[i].Name = "tabPage1";
                this.tabPages[i].Padding = new System.Windows.Forms.Padding(3);
                this.tabPages[i].Size = new System.Drawing.Size(this.tabControl1.Size.Width, this.tabControl1.Size.Height);
                this.tabPages[i].TabIndex = 0;
                this.tabPages[i].Text = Algs[i].Name;
                this.tabPages[i].UseVisualStyleBackColor = true;
            }
            // 
            // dataGridViews
            // 
            for (int i = 0; i < Algorithms.GetAlgorithms.Count; i++)
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

            for (int i = 0; i < Algorithms.GetAlgorithms.Count; i++)
            {
                dataGridViews[i].Columns.Add("Task", "Задача");
                dataGridViews[i].Columns.Add("Time", "t, мсек.");
                dataGridViews[i].Columns.Add("Func", "Кол-во вызовов ф-ции");
                dataGridViews[i].Columns.Add("BB", "Кол-во вызовов ЧЯ");
                dataGridViews[i].Columns.Add("Cost", "Стоимость");
            }


            this.tabControl1.ResumeLayout(false);
            for (int i = 0; i < Algorithms.GetAlgorithms.Count; i++)
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
        private System.Windows.Forms.GroupBox groupBoxAlgorithms;
        private System.Windows.Forms.CheckBox[] checkBoxsAlgorithms;
    }
}

