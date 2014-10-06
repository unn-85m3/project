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

namespace TestSystem
{
    public partial class Form1 : Form, IEndCalculate
    {
        private TestSystem.test_system.TestSystem Algorithms;
        private List<Tasks.ITaskPackage> Tasks;
        private List<IAlgorithm> Algs;


        public Form1()
        {
            InitializeComponent();
            Create_TestSystem();
        }

        /// <summary>
        /// Заглушка на алгоритмы.
        /// </summary>
        private void Create_TestSystem()
        {
            Algs = new List<IAlgorithm>();
            Algs.Add(new Algorithm.Benchmark_Algorithm(null, new BlackBoxFunction()));
            Algs.Add(new Algorithm.Non_Benchmark_Algorithm(null, new BlackBoxFunction()));

            Tasks = new List<TestSystem.Tasks.ITaskPackage>();

            List<DataFormat.DataFormat> dtf = new List<DataFormat.DataFormat>();
            dtf.Add(new DataFormat.DataFormat("/Tests/test_1.txt"));
            dtf.Add(new DataFormat.DataFormat("/Tests/test_2.txt"));
            dtf.Add(new DataFormat.DataFormat("/Tests/test_3.txt"));


            foreach (var d in dtf)
            {
                Tasks.Add(d.GetData());
                //dtf[0].OpenFile("/Tests/test_1.txt");
            }

            Algorithms = new test_system.TestSystem(Tasks, new BlackBoxFunction());
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
            for (int i = 0; i < Algorithms.Length; i++)
            {

                for (int j = 0; j < Tasks.Count; j++)
                {
                    dataGridViews[i].Rows.Add();
                    dataGridViews[i].Rows[j].Cells[0].Value = Tasks[j].Name;
                    dataGridViews[i].Rows[j].Cells[1].Value = "";
                    dataGridViews[i].Rows[j].Cells[2].Value = "";
                    dataGridViews[i].Rows[j].Cells[3].Value = "";
                    dataGridViews[i].Rows[j].Cells[4].Value = "";
                }
            }
        }

        /// <summary>
        /// Заполнение таблиц.
        /// </summary>
        private void Init_Table(Algorithm.IAlgorithm alg, Tasks.ITaskPackage task, DataFormat.IOutBlackBoxParam rez, int time)
        {
            for (int i = 0; i < Algorithms.Length; i++)
            {
                if (alg.Name == Algs[i].Name)
                for (int j = 0; j < Tasks.Count; j++)
                {
                    if (Tasks[j].Name == task.Name)
                    {
                        dataGridViews[i].Rows[j].Cells[0].Value = task.Name;
                        dataGridViews[i].Rows[j].Cells[1].Value = time;
                        dataGridViews[i].Rows[j].Cells[2].Value = "";
                        dataGridViews[i].Rows[j].Cells[3].Value = "";
                        dataGridViews[i].Rows[j].Cells[4].Value = rez.Cost;
                    }
                }
            }
        }



        public void OnEndCalculate(Algorithm.IAlgorithm alg, Tasks.ITaskPackage task, DataFormat.IOutBlackBoxParam rez, int time)
        {   
            Init_Table(alg, task, rez, time);
            //throw new NotImplementedException();
        }

        public void OnEndTask(Algorithm.IAlgorithm alg, Tasks.ITaskPackage task, DataFormat.IOutBlackBoxParam rez, int time)
        {
            Init_Table(alg, task, rez, time);
        }
    }
}
