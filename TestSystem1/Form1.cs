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
using TestSystem.Algorithm.Diagonal_Algoritm;


namespace TestSystem
{
    public partial class Form1 : Form, IEndCalculate
    {
        private TestSystem.test_system.TestSystem Algorithms;
        private List<Tasks.ITaskPackage> Tasks;
        private List<IAlgorithm> Algs;


        public Form1()
        {
            Create_TestSystem();
            InitializeComponent();
            CreateCheckBox();
            Start_Test();

           /// KSModels.DllBlackBoxCalculator Calc = new KSModels.DllBlackBoxCalculator("E:/GitHub/bbs/Models/11.1.КС.r1", null); // Абсолючный путь... пздц..... бред...
        }

        /// <summary>
        /// Заглушка на алгоритмы.
        /// </summary>
        private void Create_TestSystem()
        {
            Line l = new Line(new TestSystem.Algorithm.Diagonal_Algoritm.Point(-1,-1), new TestSystem.Algorithm.Diagonal_Algoritm.Point(4,4));
            IPoint p=l.GetPoint(Math.Sqrt(2));
            Algs = new List<IAlgorithm>();
            Algs.Add(new Algorithm.Benchmark_Algorithm());
            //Algs.Add(new Algorithm.Non_Benchmark_Algorithm());

            Tasks = new List<TestSystem.Tasks.ITaskPackage>();

            List<DataFormat.DataFormat> dtf = new List<DataFormat.DataFormat>();
            dtf.Add(new DataFormat.DataFormat("/Tests/test_1.txt"));
            //dtf.Add(new DataFormat.DataFormat("/Tests/test_2.txt"));
            //dtf.Add(new DataFormat.DataFormat("/Tests/test_3.txt"));
            //dtf.Add(new DataFormat.DataFormat("/Tests/test_4.txt"));
            //dtf.Add(new DataFormat.DataFormat("/Tests/test_5.txt"));
            //dtf.Add(new DataFormat.DataFormat("/Tests/test_6.txt"));
            //dtf.Add(new DataFormat.DataFormat("/Tests/test_7.txt"));
            //dtf.Add(new DataFormat.DataFormat("/Tests/test_8.txt"));
            //dtf.Add(new DataFormat.DataFormat("/Tests/test_9.txt"));
            //dtf.Add(new DataFormat.DataFormat("/Tests/test_10.txt"));


            foreach (var d in dtf)
            {
                Tasks.Add(d.GetData());
                //dtf[0].OpenFile("/Tests/test_1.txt");
            }

            Algorithms = new test_system.TestSystem(Tasks);
            Algorithms.SetListener(this);

            
            Algorithms.AddAlgorithm(Algs);
            //Algorithms.Add(new Algorithm.Benchmark_Algorithm(null,null));
            //Algorithms.Add(new Algorithm.Genetic_Algorithm(null, null));
            
            
        }

        private void Start_Test()
        {
            this.groupBoxAlgorithms.Hide();
            InitTab();
            Algorithms.Test();

        }

        /// <summary>
        /// Заглушка на создане строк в таблице
        /// </summary>
        private void Create_Rows()
        {
            for (int i = 0; i < Algorithms.GetAlgorithms.Count; i++)
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

                dataGridViews[i].Rows.Add();
                dataGridViews[i].Rows[Tasks.Count].Cells[0].Value = "Шаг метода \"" + Algorithms.GetAlgorithms[0].Step + "\"";
                dataGridViews[i].Rows[Tasks.Count].Cells[1].Value = "";
                dataGridViews[i].Rows[Tasks.Count].Cells[2].Value = "";
                dataGridViews[i].Rows[Tasks.Count].Cells[3].Value = "";
                dataGridViews[i].Rows[Tasks.Count].Cells[4].Value = "";
            }
        }

        /// <summary>
        /// Заполнение таблиц.
        /// </summary>
        private void Init_Table(Algorithm.IAlgorithm alg, Tasks.ITaskPackage task, DataFormat.IOutBlackBoxParam rez, int time)
        {
            for (int i = 0; i < Algorithms.GetAlgorithms.Count; i++)
            {
                if (alg.Name == Algs[i].Name)
                for (int j = 0; j < Tasks.Count; j++)
                {
                    if (Tasks[j].Name == task.Name)
                    {
                        //dataGridViews[i].Rows[j].Cells[0].Value = task.Name;
                        dataGridViews[i].Rows[j].Cells[1].Value = time;
                        dataGridViews[i].Rows[j].Cells[2].Value = alg.Calls;
                        //dataGridViews[i].Rows[j].Cells[3].Value = "10*i + j =";
                        dataGridViews[i].Rows[j].Cells[4].Value = rez.Cost;
                    }
                }
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
        }
    }
}
