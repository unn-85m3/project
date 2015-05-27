using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestSystem.test_system;
using TestSystem.BlackBox;
using TestSystem.Algorithm.Old;
using TestSystem.Algorithm.New;
using TestSystem.Algorithm;
using TestSystem.Plot;
using KSModels;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;
using TestSystem.Logger;
using TestSystem.Algorithm.Old.Diagonal_Algoritm;
using System.IO;




namespace TestSystem
{
    public partial class Form1 : Form, IEndCalculate
    {
        private TestSystem.test_system.TestSystem Algorithms;
        private List<Tasks.ITaskPackage> Tasks;
        private List<IAlgorithm> Algs;
        private int[] CompleateTask;
        private double[,,] BenchRez;
        private int PAGE = 0;
        private int MIN_NUMBER_TASK = 1, MAX_NUMBER_TASK = 2;
        private List<ILogger> log;

        protected class PlotPoint : IPoint
        {
            private double _x1;
            private double _x2;
            private double _cost;

            public PlotPoint(double x1, double x2, double cost)
            {
                this._x1 = x1;
                this._x2 = x2;
                this._cost = cost;
            }

            public double x1
            {
                get
                {
                    return _x1;
                }
                set
                {
                    _x1 = value;
                }
            }

            public double x2
            {
                get
                {
                    return _x2;
                }
                set
                {
                    _x2 = value;
                }
            }

            public double cost
            {
                get
                {
                    return _cost;
                }
                set
                {
                    _cost = value;
                }
            }
        }

        public Form1()
        {
            log = new List<ILogger>();
            InitializeComponent();
            
        }



        private void CreateTasks(List<DataFormat.DataFormat> dtf, bool array, int min, int max)
        {
            if (array)
                for (int i = min; i <= max; i++)
                {
                    dtf.Add(new DataFormat.DataFormat("/Tests/test_" + i + ".txt"));
                }
            else
            {
                dtf.Add(new DataFormat.DataFormat("/Tests/test_" + min + ".txt"));
                dtf.Add(new DataFormat.DataFormat("/Tests/test_" + max + ".txt"));
            }
        }

        private void CreateTasks(List<DataFormat.DataFormat> dtf, bool array, params int[] number_task)
        {
            for (int i = 1; i <= number_task.Length; i++)
            {
                dtf.Add(new DataFormat.DataFormat("/Tests/test_" + number_task[i - 1] + ".txt"));
            }
        }

        //private void CreateToolTip()
        //{
        //    tt = new ToolTip();
        //}

        private void AddAlg(IAlgorithm alg)
        {
            foreach (var als in Algs)
            {
                if (alg.Name == als.Name)
                {
                    alg.Vesrsion++;
                    alg.Name = alg.Name + ". V.=" + alg.Vesrsion;
                }
            }
            Algs.Add(alg);
        }

        private void AddAlg(IAlgorithm[] alg)
        {
            foreach (var a in alg)
            {
                foreach (var als in Algs)
                {
                    if (a.Name == als.Name)
                    {
                        a.Vesrsion++;
                        a.Name = a.Name + ". V.=" + a.Vesrsion;
                    }
                }
                Algs.Add(a);
            }
        }

        private void AddAlg(List<IAlgorithm> alg)
        {
            AddAlg(alg.ToArray());
        }

        private void ALGS()
        {
            double step = 1;
            double.TryParse(maskedTextBox3.Text, out step);

            Algs = new List<IAlgorithm>();

            AddAlg(new Algorithm.Old.Benchmark_Algorithm(step));
            //AddAlg(new Algorithm.Old.Diagonal_Algoritm.DiagonalAlgorithmV2(step));
            //AddAlg(new Algorithm.New.Diagonal_Algoritm.DiagonalAlgorithmV2(step));
            //AddAlg(new Algorithm.Old.Complex_Algorithm(step));
            //AddAlg(new Algorithm.New.Complex_Algorithm(step));
            AddAlg(new Algorithm.Old.Genetic_Algorithm(step));
            AddAlg(new Algorithm.New.Genetic_Algorithm(step));
        }


        /// <summary>
        /// Заглушка на алгоритмы.
        /// </summary>
        private void Create_TestSystem()
        {

            ALGS();

            //Algs.Add(new Algorithm.Benchmark_Algorithm(step));
            //Algs.Add(new Algorithm.Non_Benchmark_Algorithm());
            //Algs.Add(new Algorithm.Diagonal_Algoritm.DiagonalAlgorithmV2(step));
            //Algs.Add(new Algorithm.Complex_Algorithm(step));
            //Algs.Add(new Algorithm.Genetic_Algorithm(step));
            //Algs.Add(new Algorithm.Benchmark_Algorithm());
            ////Algs.Add(new Algorithm.Non_Benchmark_Algorithm());
            //Algs.Add(new Algorithm.Diagonal_Algoritm.DiagonalAlgorithmV2());
            //Algs.Add(new Algorithm.Complex_Algorithm());
            //Algs.Add(new Algorithm.Genetic_Algorithm());
            //
            //foreach (IAlgorithm alg in Algs)
            //{
            //    alg.Step = step;
            //}

            if (checkBox1.Checked)
            {
                foreach (IAlgorithm alg in Algs)
                {
                    Logger.Logger logger = new Logger.Logger();
                    log.Add(logger);
                    alg.setCalculateListener(logger);
                }
            }



            CompleateTask = new int[Algs.Count+1];
            for (int i = 0; i < CompleateTask.Length; i++)
                CompleateTask[i] = 0;

            Tasks = new List<TestSystem.Tasks.ITaskPackage>();

            List<DataFormat.DataFormat> dtf = new List<DataFormat.DataFormat>();
            CreateTasks(dtf, true, MIN_NUMBER_TASK, MAX_NUMBER_TASK);
            //CreateTasks(dtf, false, 6);

            BenchRez = new double[Algs.Count, 3, dtf.Count];

            foreach (var d in dtf)
            {
                Tasks.Add(d.GetData());
                //dtf[0].OpenFile("/Tests/test_1.txt");
            }

            Algorithms = new test_system.TestSystem(Tasks);
            Algorithms.SetListener(this);

            
            Algorithms.AddAlgorithm(Algs);

            InitTab();
            //Algorithms.Test();

            //BlackBox.BlackBoxFunction fn = new BlackBox.BlackBoxFunction();
            //fn.Init(Tasks[6]);
            //Plot.Plot plot = new Plot.Plot(fn, Tasks[6].EnterParams);
            //plot.Show();
            //plot.StartCalculate();
            //plot.DoubleClick += plot_DoubleClick;
            
            //Algorithms.Add(new Algorithm.Benchmark_Algorithm(null,null));
            //Algorithms.Add(new Algorithm.Genetic_Algorithm(null, null));        
        }

        void plot_DoubleClick(object sender, EventArgs e)
        {
            IPoint point = new PlotPoint(42, 52, 0);
            ((IPlot)sender).AddPoint(point);

        }

        /// <summary>
        /// Заглушка на создане строк в таблице
        /// </summary>
        private void Create_Rows()
        {
                for (int j = 0; j < Tasks.Count; j++)
                {
                    dataGridViews[0].Rows.Add();
                    dataGridViews[0].Rows[j].Cells[0].Value = Tasks[j].Name;
                    dataGridViews[0].Rows[j].Cells[1].Value = "";
                    dataGridViews[0].Rows[j].Cells[2].Value = "";
                    dataGridViews[0].Rows[j].Cells[3].Value = "";
                    dataGridViews[0].Rows[j].Cells[4].Value = "";
                }
            for (int i = 1; i < Algorithms.Length; i++)
            {

                for (int j = 0; j < Tasks.Count; j++)
                {
                    dataGridViews[i].Rows.Add();
                    dataGridViews[i].Rows[j].Cells[0].Value = Tasks[j].Name;
                    dataGridViews[i].Rows[j].Cells[1].Value = "";
                    dataGridViews[i].Rows[j].Cells[2].Value = "";
                    dataGridViews[i].Rows[j].Cells[3].Value = "";
                    dataGridViews[i].Rows[j].Cells[4].Value = "";
                    dataGridViews[i].Rows[j].Cells[5].Value = "";
                    dataGridViews[i].Rows[j].Cells[6].Value = "";
                    dataGridViews[i].Rows[j].Cells[7].Value = "";
                }
                dataGridViews[i].Rows.Add();
                dataGridViews[i].Rows[dataGridViews[i].RowCount - 2].Cells[0].Value = "Среднее значение";
            }
        }

        /// <summary>
        /// Заполнение таблиц.
        /// </summary>
        private void Init_Table(Algorithm.IAlgorithm alg, Tasks.ITaskPackage task, DataFormat.IOutBlackBoxParam rez, int time)
        {
            for (int i = 0; i < Algorithms.Length; i++)
            {
                if (i == 0)
                {
                    if (alg.Name == Algs[i].Name)
                    {
                        CompleateTask[i]++;
                        CompleateTask[CompleateTask.Length - 1]++;
                        for (int j = 0; j < Tasks.Count; j++)
                        {
                            if (dataGridViews[i].Rows[j].Cells[0].Value.ToString() == task.Name)
                            {
                                //dataGridViews[i].Rows[j].Cells[0].Value = task.Name;
                                dataGridViews[i].Rows[j].Cells[1].Value = time;
                                BenchRez[i, 0, j] = time;
                                dataGridViews[i].Rows[j].Cells[2].Value = alg.Calls;
                                //dataGridViews[i].Rows[j].Cells[3].Value = "10*i + j =";
                                dataGridViews[i].Rows[j].Cells[4].Value = rez.Cost;
                                BenchRez[i, 1, j] = rez.Cost;
                                BenchRez[i, 2, j] = alg.Calls;
                            }
                        }
                    }
                }
                else
                {
                    if (alg.Name == Algs[i].Name)
                    {
                        CompleateTask[i]++;
                        CompleateTask[CompleateTask.Length - 1]++;
                        for (int j = 0; j < Tasks.Count; j++)
                        {
                            if (dataGridViews[i].Rows[j].Cells[0].Value.ToString() == task.Name)
                            {
                                //dataGridViews[i].Rows[j].Cells[0].Value = task.Name;
                                dataGridViews[i].Rows[j].Cells[1].Value = time;
                                dataGridViews[i].Rows[j].Cells[2].Value = alg.Calls;
                                //dataGridViews[i].Rows[j].Cells[3].Value = "10*i + j =";
                                dataGridViews[i].Rows[j].Cells[4].Value = rez.Cost;
                                //if (CompleateTask[0] >= CompleateTask[i])
                                //{
                                //    Init_Table(i, j);
                                //}
                                //else 
                                //    Init_Table(i, CompleateTask[0]);
                            }
                            if (Tasks[j].Name == task.Name)
                            {
                                BenchRez[i, 0, j] = time;
                                BenchRez[i, 1, j] = rez.Cost;
                                BenchRez[i, 2, j] = alg.Calls;
                            }
                        }
                    }
                }
            }
        }

        private void Init_Table(int i, int j)
        {
                    if (CompleateTask[0] >= CompleateTask[i])
                    {
                        dataGridViews[i].Rows[j].Cells[5].Value = (BenchRez[i, 0, j] / BenchRez[0, 0, j] - 1) * 100;
                        dataGridViews[i].Rows[j].Cells[6].Value = (BenchRez[i, 1, j] / BenchRez[0, 1, j] - 1) * 100;
                        dataGridViews[i].Rows[j].Cells[7].Value = (BenchRez[i, 2, j] / BenchRez[0, 2, j] - 1) * 100;
                    }
        }

        //private void Init_Table()
        //{
        //    for (int i = 1; i < Algorithms.Length; i++)
        //    {
        //        double time = 0, count = 0;

        //        for (int j = 0; j < Tasks.Count; j++)
        //            if (BenchRez[i, 0, j] != 0)
        //            {
        //                for (int k = 0; k < Tasks.Count; k++)
        //                {
        //                    if (Tasks[j].Name == dataGridViews[i].Rows[k].Cells[0].Value.ToString())
        //                    {
        //                        if (CompleateTask[0] >= CompleateTask[i])
        //                        {
        //                            dataGridViews[i].Rows[j].Cells[5].Value = (BenchRez[i, 0, j] / BenchRez[0, 0, j] - 1) * 100;
        //                            time += BenchRez[i, 0, j] / BenchRez[0, 0, j] - 1;
        //                            dataGridViews[i].Rows[j].Cells[6].Value = (BenchRez[i, 1, j] / BenchRez[0, 1, j] - 1) * 100;
        //                            count += BenchRez[i, 1, j] / BenchRez[0, 1, j] - 1;
        //                        }
        //                    }
        //                }
        //            }
        //            else break;
        //        for (int k = 0; k < Tasks.Count; k++)
        //        {
        //            if ("Среднее значение" == dataGridViews[i].Rows[k].Cells[0].Value.ToString())
        //            {
        //                dataGridViews[i].Rows[k].Cells[5].Value = 100 * time / Tasks.Count;
        //                dataGridViews[i].Rows[k].Cells[6].Value = 100 * count / Tasks.Count;
        //            }
        //        }
        //    }
        //}


        public void ExportToExcel(/*DataGridView grid*/)
        {
            ApplicationClass Excel = new ApplicationClass();
            XlReferenceStyle RefStyle = Excel.ReferenceStyle;
            Excel.Visible = true;
            Workbook wb = null;
            String TemplatePath = System.Windows.Forms.Application.StartupPath + @"\Экспорт данных.xlt";
            try
            {
                wb = Excel.Workbooks.Add(TemplatePath); // !!! 
            }
            catch (System.Exception ex)
            {
                throw new Exception("Не удалось загрузить шаблон для экспорта " + TemplatePath + "\n" + ex.Message);
            }
            /*Worksheet ws1 = wb.Worksheets.get_Item(1) as Worksheet;
            for (int j = 0; j < grid.Columns.Count; ++j)
            {
                (ws1.Cells[1, j + 1] as Range).Value2 = grid.Columns[j].HeaderText;
                for (int i = 0; i < grid.Rows.Count; ++i)
                {
                    object Val = grid.Rows[i].Cells[j].Value;
                    if (Val != null)
                        (ws1.Cells[i + 2, j + 1] as Range).Value2 = Val.ToString();
                }
            }
            ws1.Columns.EntireColumn.AutoFit();*/

            for (int k = 0; k < Algorithms.Length; k++)
            {
                Worksheet ws2 = wb.Worksheets.get_Item(k + 1) as Worksheet;
                for (int j = 0; j < dataGridViews[k].Columns.Count; ++j)
                {
                    (ws2.Cells[1, j + 1] as Range).Value2 = dataGridViews[k].Columns[j].HeaderText;
                    for (int i = 0; i < dataGridViews[k].Rows.Count; ++i)
                    {
                        object Val = dataGridViews[k].Rows[i].Cells[j].Value;
                        if (Val != null)
                            (ws2.Cells[i + 2, j + 1] as Range).Value2 = Val.ToString();
                    }
                }
                ws2.Columns.EntireColumn.AutoFit();
            }
            /* Worksheet ws2 = wb.Worksheets.get_Item(2) as Worksheet;
             for (int j = 0; j < grid.Columns.Count; ++j)
             {
                 (ws2.Cells[1, j + 1] as Range).Value2 = grid.Columns[j].HeaderText;
                 for (int i = 0; i < grid.Rows.Count; ++i)
                 {
                     object Val = grid.Rows[i].Cells[j].Value;
                     if (Val != null)
                         (ws2.Cells[i + 2, j + 1] as Range).Value2 = Val.ToString();
                 }
             }
             ws2.Columns.EntireColumn.AutoFit();

             Worksheet ws3 = wb.Worksheets.get_Item(3) as Worksheet;
             for (int j = 0; j < grid.Columns.Count; ++j)
             {
                 (ws3.Cells[1, j + 1] as Range).Value2 = grid.Columns[j].HeaderText;
                 for (int i = 0; i < grid.Rows.Count; ++i)
                 {
                     object Val = grid.Rows[i].Cells[j].Value;
                     if (Val != null)
                         (ws3.Cells[i + 2, j + 1] as Range).Value2 = Val.ToString();
                 }
             }
             ws3.Columns.EntireColumn.AutoFit();

             Worksheet ws4 = wb.Worksheets.get_Item(4) as Worksheet;
             for (int j = 0; j < grid.Columns.Count; ++j)
             {
                 (ws4.Cells[1, j + 1] as Range).Value2 = grid.Columns[j].HeaderText;
                 for (int i = 0; i < grid.Rows.Count; ++i)
                 {
                     object Val = grid.Rows[i].Cells[j].Value;
                     if (Val != null)
                         (ws1.Cells[i + 2, j + 1] as Range).Value2 = Val.ToString();
                 }
             }
             ws1.Columns.EntireColumn.AutoFit();*/


            Excel.ReferenceStyle = RefStyle;
            ReleaseExcel(Excel as Object);
        }

        private void ReleaseExcel(object excel)
        {
            // Уничтожение объекта Excel.
            Marshal.ReleaseComObject(excel);
            // Вызываем сборщик мусора для немедленной очистки памяти
            GC.GetTotalMemory(true);
        }


        /////////////////////////////////////////////////////






        private void Init_Table()
        {
            
            for (int i = 1; i < Algorithms.Length; i++)
            {
                double time = 0, count = 0, call = 0;
                for (int j = 0; j < CompleateTask[i]; j++)
                    if (CompleateTask[0] >= CompleateTask[i])
                    {
                        dataGridViews[i].Rows[j].Cells[5].Value = (BenchRez[i, 0, j] / BenchRez[0, 0, j] - 1) * 100;
                        time += BenchRez[i, 0, j] / BenchRez[0, 0, j] - 1;
                        dataGridViews[i].Rows[j].Cells[6].Value = (BenchRez[i, 1, j] / BenchRez[0, 1, j] - 1) * 100;
                        count += BenchRez[i, 1, j] / BenchRez[0, 1, j] - 1;
                        dataGridViews[i].Rows[j].Cells[7].Value = (BenchRez[i, 2, j] / BenchRez[0, 2, j] - 1) * 100;
                        call += BenchRez[i, 2, j] / BenchRez[0, 2, j] - 1;

                        if ((BenchRez[i, 0, j] / BenchRez[0, 0, j] - 1) == 0) dataGridViews[i].Rows[j].Cells[5].Style.BackColor = Color.Yellow;
                        else
                        {
                            if ((BenchRez[i, 0, j] / BenchRez[0, 0, j] - 1) * 100 > 0)
                            {
                                dataGridViews[i].Rows[j].Cells[5].Style.BackColor = Color.Red;
                            }
                            else dataGridViews[i].Rows[j].Cells[5].Style.BackColor = Color.GreenYellow;
                        }

                        if ((BenchRez[i, 1, j] / BenchRez[0, 1, j] - 1) == 0) dataGridViews[i].Rows[j].Cells[6].Style.BackColor = Color.Yellow;
                        else
                        {
                            if ((BenchRez[i, 1, j] / BenchRez[0, 1, j] - 1) * 100 > 0)
                            {
                                dataGridViews[i].Rows[j].Cells[6].Style.BackColor = Color.Red;
                            }
                            else dataGridViews[i].Rows[j].Cells[6].Style.BackColor = Color.GreenYellow;
                        }

                        if ((BenchRez[i, 2, j] / BenchRez[0, 2, j] - 1) == 0) dataGridViews[i].Rows[j].Cells[7].Style.BackColor = Color.Yellow;
                        else
                        {
                            if ((BenchRez[i, 2, j] / BenchRez[0, 2, j] - 1) * 100 > 0)
                            {
                                dataGridViews[i].Rows[j].Cells[7].Style.BackColor = Color.Red;
                            }
                            else dataGridViews[i].Rows[j].Cells[7].Style.BackColor = Color.GreenYellow;
                        }

                    }
                dataGridViews[i].Rows[dataGridViews[i].RowCount - 2].Cells[5].Value = time / Tasks.Count * 100;
                dataGridViews[i].Rows[dataGridViews[i].RowCount - 2].Cells[6].Value = count / Tasks.Count * 100;
                dataGridViews[i].Rows[dataGridViews[i].RowCount - 2].Cells[7].Value = call / Tasks.Count * 100;

                if (time / Tasks.Count == 0) dataGridViews[i].Rows[dataGridViews[i].RowCount - 2].Cells[5].Style.BackColor = Color.Yellow;
                else
                {
                    if (time / Tasks.Count > 0)
                    {
                        dataGridViews[i].Rows[dataGridViews[i].RowCount - 2].Cells[5].Style.BackColor = Color.Red;
                    }
                    else dataGridViews[i].Rows[dataGridViews[i].RowCount - 2].Cells[5].Style.BackColor = Color.GreenYellow;
                }

                if (count / Tasks.Count == 0) dataGridViews[i].Rows[dataGridViews[i].RowCount - 2].Cells[6].Style.BackColor = Color.Yellow;
                else
                {
                    if (count / Tasks.Count > 0)
                    {
                        dataGridViews[i].Rows[dataGridViews[i].RowCount - 2].Cells[6].Style.BackColor = Color.Red;
                    }
                    else dataGridViews[i].Rows[dataGridViews[i].RowCount - 2].Cells[6].Style.BackColor = Color.GreenYellow;
                }

                if (call / Tasks.Count == 0) dataGridViews[i].Rows[dataGridViews[i].RowCount - 2].Cells[7].Style.BackColor = Color.Yellow;
                else
                {
                    if (call / Tasks.Count > 0)
                    {
                        dataGridViews[i].Rows[dataGridViews[i].RowCount - 2].Cells[7].Style.BackColor = Color.Red;
                    }
                    else dataGridViews[i].Rows[dataGridViews[i].RowCount - 2].Cells[7].Style.BackColor = Color.GreenYellow;
                }
            }
        }

        public void OnEndCalculate(Algorithm.IAlgorithm alg, Tasks.ITaskPackage task, DataFormat.IOutBlackBoxParam rez, int time)
        {   
            ///Init_Table(alg, task, rez, time);
            //throw new NotImplementedException();
        }

        public void OnEndTask(Algorithm.IAlgorithm alg, Tasks.ITaskPackage task, DataFormat.IOutBlackBoxParam rez, int time)
        {
            /*if (lg == null)
                lg = new Logger.Logger();*/
            
           /* for(int i = 0; i < alg.points.Count; i++)
            {
                lg.onCalculate(alg.points[i].x1, alg.points[i].x2, alg.points[i].cost, task.Name); //ЭТО ВЕРНО?!
            }*/

            if(checkBox2.Checked)
                SaveTxt(alg, task.Name);

            Init_Table(alg, task, rez, time);
            if (CompleateTask[0] == Tasks.Count)
                Init_Table();
            
        }

        private void SaveTxt(Algorithm.IAlgorithm alg, string taskName)
        {
            string file = (@"Результаты\" + alg.Name + "_" + taskName.Substring(7) + ".txt");
            using (StreamWriter write_text = new StreamWriter(file, false))
            {
                for (int i = 0; i < alg.points.Count; i++)
                    write_text.WriteLine("x = " + alg.points[i].x1 + " y = " + alg.points[i].x2 + " cost = " + alg.points[i].cost); //Записываем в файл текст из текстового поля
            }
        }

        private void dataGridViews_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(buttonStart.Enabled == false)
                if (CompleateTask[CompleateTask.Length - 1] >= Algs.Count * Tasks.Count)
                    buttonStart.Enabled = true;

            int row = e.RowIndex;
            PAGE = tabControl1.SelectedIndex;
            if(PAGE != 0 && row < dataGridViews[PAGE].RowCount - 2)
                Drawer.Drawer.DrawGraphics(Tasks, Algs, PAGE, row,log);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            Algorithms.Test();
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            bool test = false;
            int.TryParse(maskedTextBox1.Text, out MIN_NUMBER_TASK);

            int.TryParse(maskedTextBox2.Text, out MAX_NUMBER_TASK);

            if (MIN_NUMBER_TASK > 60 || MIN_NUMBER_TASK < 1 || MAX_NUMBER_TASK > 60 || MAX_NUMBER_TASK < 1 || MAX_NUMBER_TASK < MIN_NUMBER_TASK)
            {
                MessageBox.Show("Введено не верное число(а)! Числа должны быть в диапазоне от 1 до 60. Начальное значение < Конечного");
            }
            else test = true;

            if (test)
            {
                buttonShow.Enabled = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                maskedTextBox1.Enabled = false;
                maskedTextBox2.Enabled = false; 
                maskedTextBox3.Enabled = false;
                buttonStart.Enabled = true;
                Create_TestSystem();
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //if (CompleateTask[CompleateTask.Length - 1] >= Algs.Count * Tasks.Count)
            //    buttonStart.Enabled = true;
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("C:/Users/Ирина Рыжова/Desktop/project 1/TestSystem/bin/Release/Result Exel/Result.accdb", "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
