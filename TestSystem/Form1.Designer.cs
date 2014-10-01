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
            this.ResumeLayout(false);

        }

        private void Create_TabPages(System.Collections.Generic.List<Algorithm.IAlgorithm> Algorithms)
        {
            this.tabControl_Algorithms = new System.Windows.Forms.TabControl();
            this.tabPages = new System.Windows.Forms.TabPage[Algorithms.Count];
            this.tabControl_Algorithms.SuspendLayout();
            // 
            // tabControl_Algorithms
            // 
            for (int i = 0; i < Algorithms.Count; i++)
            {
                this.tabPages[i] = new System.Windows.Forms.TabPage();
                this.tabControl_Algorithms.Controls.Add(this.tabPages[i]);
            }
            this.tabControl_Algorithms.Location = new System.Drawing.Point(13, 13);
            this.tabControl_Algorithms.Name = "tabControl_Algorithms";
            this.tabControl_Algorithms.SelectedIndex = 0;
            this.tabControl_Algorithms.Size = new System.Drawing.Size(574, 358);
            this.tabControl_Algorithms.TabIndex = 0;
            // 
            // tabPages
            // 
            for (int i = 0; i < Algorithms.Count; i++)
            {
                this.tabPages[i].Location = new System.Drawing.Point(4, 22);
                this.tabPages[i].Name = Algorithms[i].Name;
                this.tabPages[i].Padding = new System.Windows.Forms.Padding(3);
                //this.tabPages[i].Size = new System.Drawing.Size(566, 332);
                this.tabPages[i].TabIndex = i;
                this.tabPages[i].Text = Algorithms[i].Name;
                this.tabPages[i].UseVisualStyleBackColor = true;
            }

            this.Controls.Add(this.tabControl_Algorithms);
            this.tabControl_Algorithms.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_Algorithms;
        private System.Windows.Forms.TabPage[] tabPages;

    }
}

