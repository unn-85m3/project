﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestSystem
{
    public partial class Form1 : Form
    {
        private int numb_algorithms = 0;
        private List<Algorithm.IAlgorithm> Algorithms;


        public Form1()
        {
            Create_Algorithms();
            InitializeComponent();
        }

        private void Create_Algorithms()
        {
            Algorithms = new List<Algorithm.IAlgorithm>();
            Algorithms.Add(new Algorithm.Benchmark_Algorithm());
            Algorithms.Add(new Algorithm.Genetic_Algorithm());


            numb_algorithms = 2;
        }        
    }
}
