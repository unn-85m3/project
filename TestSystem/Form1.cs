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

namespace TestSystem
{
    public partial class Form1 : Form, IEndCalculate
    {
        private TestSystem.test_system.TestSystem Algorithms;
        private List<Tasks.ITaskPackage> Tasks;


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
            DataFormat.DataFormat dtf = new DataFormat.DataFormat();
            dtf.OpenFile("/Tests/test_1.txt");
            Tasks.Add(dtf.GetData());
            Algorithms = new test_system.TestSystem(Tasks, new BlackBoxFunction.BlackBoxFunction());
            Algorithms.SetListener(this);

            //Algorithms.Add(new Algorithm.Benchmark_Algorithm(null,null));
            //Algorithms.Add(new Algorithm.Genetic_Algorithm(null, null));
            //Create_Tasks();
            //InitTab();
            //Init_Table();
        }

        /// <summary>
        /// Заглушка на задания
        /// </summary>
        //private void Create_Tasks()
        //{
        //    Tasks = new List<Tasks.Tasks_Base>();
        //    Tasks.Add(new Tasks.Tasks_Base("Задача 1"));
        //    Tasks.Add(new Tasks.Tasks_Base("Задача 2"));
        //    Tasks.Add(new Tasks.Tasks_Base("Задача 3"));
        //    Tasks.Add(new Tasks.Tasks_Base("Задача 4"));
        //}

        /// <summary>
        /// Заполнение таблиц.
        /// </summary>
        //private void Init_Table()
        //{
        //    for (int i = 0; i < Algorithms.Count; i++)
        //    {
        //        dataGridViews[i].Columns.Add("Task", "Задача");
        //        dataGridViews[i].Columns.Add("Time", "t, мин.");
        //        dataGridViews[i].Columns.Add("Func", "Кол-во вызовов ф-ции");
        //        dataGridViews[i].Columns.Add("BB", "Кол-во вызовов ЧЯ");
        //        dataGridViews[i].Columns.Add("Cost", "Стоимость");
        //        for (int j = 0; j < Tasks.Count; j++)
        //        {
        //            dataGridViews[i].Rows.Add();
        //            dataGridViews[i].Rows[j].Cells[0].Value = Tasks[j].Name;
        //            //dataGridViews[i].Rows[j].Cells[1].Value = "";
        //            //dataGridViews[i].Rows[j].Cells[2].Value = "";
        //            //dataGridViews[i].Rows[j].Cells[3].Value = "";
        //            //dataGridViews[i].Rows[j].Cells[4].Value = "";
        //        }
        //    }
        //}



        public void OnEndCalculate(Algorithm.IAlgorithm alg, Tasks.ITaskPackage task, DataFormat.IOutBlackBoxParam rez, int time)
        {
            throw new NotImplementedException();
        }

        public void OnEndTask(Algorithm.IAlgorithm alg, Tasks.ITaskPackage task, DataFormat.IOutBlackBoxParam rez, int time)
        {
            throw new NotImplementedException();
        }
    }
}
