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
using KSModels;


namespace TestSystem
{
    public partial class Form1 : Form, IEndCalculate
    {
        private TestSystem.test_system.TestSystem Algorithms;
        private List<Tasks.ITaskPackage> Tasks;
        private List<IAlgorithm> Algs;
        private int[] CompleateTask;
        private double[,,] BenchRez;
        private int MIN_NUMBER_TASK = 1, MAX_NUMBER_TASK = 60;


        public Form1()
        {
            InitializeComponent();
            Create_TestSystem();

           /// KSModels.DllBlackBoxCalculator Calc = new KSModels.DllBlackBoxCalculator("E:/GitHub/bbs/Models/11.1.КС.r1", null); // Абсолючный путь... пздц..... бред...
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

        /// <summary>
        /// Заглушка на алгоритмы.
        /// </summary>
        private void Create_TestSystem()
        {
            Algs = new List<IAlgorithm>();
            Algs.Add(new Algorithm.Benchmark_Algorithm());
            Algs.Add(new Algorithm.Non_Benchmark_Algorithm());

            CompleateTask = new int[Algs.Count];
            for (int i = 0; i < CompleateTask.Length; i++)
                CompleateTask[i] = 0;

            Tasks = new List<TestSystem.Tasks.ITaskPackage>();

            List<DataFormat.DataFormat> dtf = new List<DataFormat.DataFormat>();
            CreateTasks(dtf, true, MIN_NUMBER_TASK, MAX_NUMBER_TASK);

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
            Algorithms.Test();
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
                            if (Tasks[j].Name == task.Name)
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
                            if (Tasks[j].Name == task.Name)
                            {
                                //dataGridViews[i].Rows[j].Cells[0].Value = task.Name;
                                dataGridViews[i].Rows[j].Cells[1].Value = time;                                
                                BenchRez[i, 0, j] = time;
                                dataGridViews[i].Rows[j].Cells[2].Value = alg.Calls;
                                //dataGridViews[i].Rows[j].Cells[3].Value = "10*i + j =";
                                dataGridViews[i].Rows[j].Cells[4].Value = rez.Cost;
                                BenchRez[i, 1, j] = rez.Cost;
                                //if (CompleateTask[0] >= CompleateTask[i])
                                //{
                                //    Init_Table(i, j);
                                //}
                                //else 
                                //    Init_Table(i, CompleateTask[0]);
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
                        dataGridViews[i].Rows[j].Cells[5].Value = BenchRez[i, 0, j] / BenchRez[0, 0, j];
                        dataGridViews[i].Rows[j].Cells[6].Value = BenchRez[i, 1, j] / BenchRez[0, 1, j];
                    }
        }

        private void Init_Table()
        {
            double time = 0, count = 0;
            for (int i = 1; i < Algorithms.Length; i++)
            {
                for (int j = 0; j < Tasks.Count; j++)
                    if (CompleateTask[0] >= CompleateTask[i])
                    {
                        dataGridViews[i].Rows[j].Cells[5].Value = BenchRez[i, 0, j] / BenchRez[0, 0, j];
                        time += BenchRez[i, 0, j] / BenchRez[0, 0, j];
                        dataGridViews[i].Rows[j].Cells[6].Value = BenchRez[i, 1, j] / BenchRez[0, 1, j];
                        count += BenchRez[i, 1, j] / BenchRez[0, 1, j];
                    }
                dataGridViews[i].Rows[dataGridViews[i].RowCount - 2].Cells[5].Value = time / Tasks.Count;
                dataGridViews[i].Rows[dataGridViews[i].RowCount - 2].Cells[6].Value = count / Tasks.Count;
            }
        }


        public void OnEndCalculate(Algorithm.IAlgorithm alg, Tasks.ITaskPackage task, DataFormat.IOutBlackBoxParam rez, int time)
        {   
           /// Init_Table(alg, task, rez, time);
            //throw new NotImplementedException();
        }

        public void OnEndTask(Algorithm.IAlgorithm alg, Tasks.ITaskPackage task, DataFormat.IOutBlackBoxParam rez, int time)
        {
            Init_Table(alg, task, rez, time);
            if (CompleateTask[0] == Tasks.Count)
                Init_Table();
        }
    }
}
