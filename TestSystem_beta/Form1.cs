using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestSystem.BackBoxFunction;
using TestSystem.test_system;
using TestSystem.Tasks;

namespace TestSystem
{
    public partial class Form1 : Form,IEndCalculate
    {
        private List<Algorithm.IAlgorithm> Algorithms;
        private List<Tasks.Tasks_Base> Tasks;


        public Form1()
        {
            InitializeComponent();
            Create_Algorithms();
        }

        /// <summary>
        /// Заглушка на алгоритмы.
        /// </summary>
        private void Create_Algorithms()
        {
            BlackBoxFunction function = new BlackBoxFunction();
            DataFormat.DataFormat data = new DataFormat.DataFormat();
            data.OpenFile("Tests/test_1.txt");
            ITaskPackage package = data.GetData();
            List<ITaskPackage> tasks=new List<ITaskPackage>();
            tasks.Add(package);
            test_system.TestSystem system = new test_system.TestSystem(tasks, function);
            Algorithms = new List<Algorithm.IAlgorithm>();
            system.AddAlgorithm(new Algorithm.Benchmark_Algorithm(null, function));

            system.SetListener(this);




            system.Test();
        }


        public void OnEndCalculate(Algorithm.IAlgorithm alg, ITaskPackage task, DataFormat.IOutBlackBoxParam rez, int time)
        {
           
        }

        public void OnEndTask(Algorithm.IAlgorithm alg, ITaskPackage task, DataFormat.IOutBlackBoxParam rez, int time)
        {

        }

        /// <summary>
        /// Заглушка на задания
        /// </summary>
        private void Create_Tasks()
        {
            Tasks = new List<Tasks.Tasks_Base>();
            Tasks.Add(new Tasks.Tasks_Base("Задача 1"));
            Tasks.Add(new Tasks.Tasks_Base("Задача 2"));
            Tasks.Add(new Tasks.Tasks_Base("Задача 3"));
            Tasks.Add(new Tasks.Tasks_Base("Задача 4"));
        }

        /// <summary>
        /// Заполнение таблиц.
        /// </summary>
        private void Init_Table()
        {
            for (int i = 0; i < Algorithms.Count; i++)
            {
                dataGridViews[i].Columns.Add("Task", "Задача");
                dataGridViews[i].Columns.Add("Time", "t, мин.");
                dataGridViews[i].Columns.Add("Func", "Кол-во вызовов ф-ции");
                dataGridViews[i].Columns.Add("BB", "Кол-во вызовов ЧЯ");
                dataGridViews[i].Columns.Add("Cost", "Стоимость");
                for (int j = 0; j < Tasks.Count; j++)
                {
                    dataGridViews[i].Rows.Add();
                    dataGridViews[i].Rows[j].Cells[0].Value = Tasks[j].Name;
                    //dataGridViews[i].Rows[j].Cells[1].Value = "";
                    //dataGridViews[i].Rows[j].Cells[2].Value = "";
                    //dataGridViews[i].Rows[j].Cells[3].Value = "";
                    //dataGridViews[i].Rows[j].Cells[4].Value = "";
                }
            }
        }




    }
}
