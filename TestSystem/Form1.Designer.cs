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
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 383);
            this.Name = "Form1";
            this.Text = "Form1";

        }

        private void Create_TabPages(System.Collections.Generic.List<Algorithm.IAlgorithm> Algorithms)
        {
            this.tabControl_Algorithms = new System.Windows.Forms.TabControl();
            //System.Collections.Generic.List<System.Windows.Forms.TabPage> this.tabPage = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl_Algorithms.SuspendLayout();
            // 
            // tabControl_Algorithms
            // 
            this.tabControl_Algorithms.Controls.Add(this.tabPage1);
            this.tabControl_Algorithms.Controls.Add(this.tabPage2);
            this.tabControl_Algorithms.Location = new System.Drawing.Point(13, 13);
            this.tabControl_Algorithms.Name = "tabControl_Algorithms";
            this.tabControl_Algorithms.SelectedIndex = 0;
            this.tabControl_Algorithms.Size = new System.Drawing.Size(574, 358);
            this.tabControl_Algorithms.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(566, 332);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;

            this.Controls.Add(this.tabControl_Algorithms);
            this.tabControl_Algorithms.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void Create_TabPage(string name, int tab_index)
        {
            this.tabPage1 = new System.Windows.Forms.TabPage();
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_Algorithms;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;

    }
}

