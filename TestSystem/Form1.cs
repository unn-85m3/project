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
using TestSystem.Algorithm;
using TestSystem.Plot;
using TestSystem.BlackBox;
using KSModels;

using TestSystem.Algorithm.Diagonal_Algoritm;


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
        private int MIN_NUMBER_TASK = 1, MAX_NUMBER_TASK = 5;



        public Form1()
        {
            InitializeComponent();
            Create_TestSystem();
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

        /// <summary>
        /// Заглушка на алгоритмы.
        /// </summary>
        private void Create_TestSystem()
        {
           
            Algs = new List<IAlgorithm>();
            Algs.Add(new Algorithm.Benchmark_Algorithm());
            //Algs.Add(new Algorithm.Non_Benchmark_Algorithm());
            Algs.Add(new Algorithm.Diagonal_Algoritm.DiagonalAlgorithmV2());
            Algs.Add(new Algorithm.Complex_Algorithm());
            Algs.Add(new Algorithm.Genetic_Algorithm());



            CompleateTask = new int[Algs.Count];
            for (int i = 0; i < CompleateTask.Length; i++)
                CompleateTask[i] = 0;

            Tasks = new List<TestSystem.Tasks.ITaskPackage>();

            List<DataFormat.DataFormat> dtf = new List<DataFormat.DataFormat>();
            CreateTasks(dtf, true, MIN_NUMBER_TASK, MAX_NUMBER_TASK);
            //CreateTasks(dtf, false, 6);

            BenchRez = new double[Algs.Count, 2, dtf.Count];

            foreach (var d in dtf)
            {
                Tasks.Add(d.GetData());
                //dtf[0].OpenFile("/Tests/test_1.txt");
            }

            Algorithms = new test_system.TestSystem(Tasks);
            Algorithms.SetListener(this);

            
            Algorithms.AddAlgorithm(Algs);

            InitTab();
           // Algorithms.Test();

            BlackBox.BlackBoxFunction fn=new BlackBox.BlackBoxFunction();
            fn.Init(Tasks[3]);
            Plot.Plot plot = new Plot.Plot(fn, Tasks[3].EnterParams);
            plot.Show();
            plot.StartCalculate();
            
            //Algorithms.Add(new Algorithm.Benchmark_Algorithm(null,null));
            //Algorithms.Add(new Algorithm.Genetic_Algorithm(null, null));        
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
                            }
                        }
                    }
                }
                else
                {
                    if (alg.Name == Algs[i].Name)
                    {
                        CompleateTask[i]++;
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

        private void Init_Table()
        {
            
            for (int i = 1; i < Algorithms.Length; i++)
            {
                double time = 0, count = 0;
                for (int j = 0; j < CompleateTask[i]; j++)
                    if (CompleateTask[0] >= CompleateTask[i])
                    {
                        dataGridViews[i].Rows[j].Cells[5].Value = (BenchRez[i, 0, j] / BenchRez[0, 0, j] - 1) * 100;
                        time += BenchRez[i, 0, j] / BenchRez[0, 0, j] - 1;
                        dataGridViews[i].Rows[j].Cells[6].Value = (BenchRez[i, 1, j] / BenchRez[0, 1, j] - 1) * 100;
                        count += BenchRez[i, 1, j] / BenchRez[0, 1, j] - 1;


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

                    }
                dataGridViews[i].Rows[dataGridViews[i].RowCount - 2].Cells[5].Value = time / Tasks.Count * 100;
                dataGridViews[i].Rows[dataGridViews[i].RowCount - 2].Cells[6].Value = count / Tasks.Count * 100;

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
            }
        }

        public void OnEndCalculate(Algorithm.IAlgorithm alg, Tasks.ITaskPackage task, DataFormat.IOutBlackBoxParam rez, int time)
        {   
            ///Init_Table(alg, task, rez, time);
            //throw new NotImplementedException();
        }

        public void OnEndTask(Algorithm.IAlgorithm alg, Tasks.ITaskPackage task, DataFormat.IOutBlackBoxParam rez, int time)
        {
            Init_Table(alg, task, rez, time);
            if (CompleateTask[0] == Tasks.Count)
                Init_Table();
         
        }

        private void dataGridViews_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            PAGE = tabControl1.SelectedIndex;
            if(PAGE != 0 && row < dataGridViews[PAGE].RowCount - 2)
                Drawer.Drawer.DrawGraphics(Tasks, Algs, PAGE, row);

        }
    }
}
