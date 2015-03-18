using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;
using TestSystem.BlackBox;

namespace TestSystem.Algorithm
{
    class Genetic_Algorithm : AbsAlgorithm
    {
        private double AX;
        private double BX;
        private double CY;
        private double DY;
        private double EZ;
        private double FZ;

        private List<double> genom_x;
        private List<double> genom_y;
        private List<double> genom_fitness;
        private int ga_size; //численность популяции
        private int ga_T; //число поколений
        //private int ga_Nc;
        //private int ga_Nm;

        static Random rnd = new Random(0);
        //Random rnd = new Random(0);

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="parameter">входные параметры ф-и Ч.Я.</param>
        /// <param name="function">Используемая ф-ция</param>
        public Genetic_Algorithm() 
        {
            this.name = "Генетический алгоритм";
            this.atributs += ""; //ЗАПОЛНИ!!!!!!!!!!!!!
        //    ga_size = 4;
        //    ga_T = 3;
        ////  ga_Nc = Nc;
        ////  ga_Nm = Nm;
        //    genom_x = new List<double>(ga_size);
        //    genom_y = new List<double>(ga_size);
        //    genom_fitness = new List<double>(ga_size);

        }

        /// <summary>
        /// Здесь находится сам алгоритм отптимизации ф-и
        /// </summary>
        /// <returns>результат работы алгоритма</returns>
        public override DataFormat.IOutBlackBoxParam Calculate()
        {
            double cost = double.MaxValue;
            int h = 0;

            AX = this.parametr.x1_min;
            BX = this.parametr.x1_max;
            CY = this.parametr.x2_min;
            DY = this.parametr.x2_max;
            EZ = this.parametr.x2_x1_min;
            FZ = this.parametr.x2_x1_max;

            h = SetAreaOfTheRegion(STEP);

            ga_size = h/4 + 4;
            ga_T = 3*h/16;//*h - 4;
            genom_x = new List<double>(ga_size);
            genom_y = new List<double>(ga_size);
            genom_fitness = new List<double>(ga_size);
            
            ga_init();
            for (int i = 0; i < ga_T; i++)
            {
                ga_crossover();
                ga_mutation();
                ga_selection();
            }

            cost = genom_fitness[0];
            genom_fitness[0] = double.MaxValue;
            return new DataFormat.OutBlackBoxParam(cost);
        }

        /// <summary>
        /// Имя алгоритма
        /// </summary>
        public override string Name
        {
            get { return name; }
        }

        private double fitness_func(int i)
        {
            double a;

            try
            {
                a = Function(genom_x[i], genom_y[i]).Cost;
            }
            catch
            {
            a = Double.MaxValue;
            }
            return a;
        }


        public int N { get { return ga_size; } set { ga_size = value; } }


        private void Sort()
        {
            int n = genom_fitness.Count;
            for (int i = 0; i < n - 1; i++)
                for (int j = i + 1; j < n; j++)
                    if (genom_fitness[i] > genom_fitness[j]) 
                        flip(i, j);
        }

        private void flip(int i, int j)
        {
            double x;
            x = genom_x[i]; genom_x[i] = genom_x[j]; genom_x[j] = x;
            x = genom_y[i]; genom_y[i] = genom_y[j]; genom_y[j] = x;
            x = genom_fitness[i]; genom_fitness[i] = genom_fitness[j]; genom_fitness[j] = x;
        }



        private bool IsInternal(double x, double y)
        {
            bool state = true;
            if (((y/x) < EZ) || ((y/x) > FZ))
                state = false;
            return state;
        }


        //public Genetic_Algorithm(int size, int T, int Nc, int Nm)
        //{
        //    ga_size = size;
        //    ga_T = T;
        //    ga_Nc = Nc;
        //    ga_Nm = Nm;
        //    genom_x = new List<double>(ga_size);
        //    genom_y = new List<double>(ga_size);
        //    genom_fitness = new List<double>(ga_size);
        //}
        
        //private void ga_initialize(double x1, double x2, double y1, double y2, double z1, double z2)
        //{
        //    AX = x1;
        //    BX = x2;
        //    CY = y1;
        //    DY = y2;
        //    EZ = z1;
        //    FZ = z2;
        //    ga_init();
        //}

        

        private void ga_init()
        {
            double lx, ly;
            lx = Math.Min(DY/EZ, BX) - Math.Max(CY/FZ, AX);
            for (int i = 0; i < ga_size; i++)
            {
                genom_x.Insert(i, Math.Max(CY / FZ, AX) + rnd.NextDouble() * lx);  
                ly = Math.Min(FZ*genom_x[i], DY) - Math.Max(EZ*genom_x[i], CY);
                genom_y.Insert(i, Math.Max(EZ * genom_x[i], CY) + rnd.NextDouble() * ly);
                genom_fitness.Insert(i, fitness_func(i));
                
            }

        }
        private void ga_crossover()//можем получить точные копии родителей, that's bad!
        {
            int bound = genom_x.Count / 2;
            int i = rnd.Next(bound);
            int j = bound + rnd.Next(genom_x.Count - bound);
           
            if (IsInternal(genom_x[i], genom_y[j]))
            {
                genom_x.Add(genom_x[i]);
                genom_y.Add(genom_y[j]);
                genom_fitness.Add(fitness_func(genom_x.Count - 1));
            }
    
            if (IsInternal(genom_x[j], genom_y[i]))
            {
                genom_x.Add(genom_x[j]);
                genom_y.Add(genom_y[i]);
                genom_fitness.Add(fitness_func(genom_x.Count - 1));
            }           
        }

        private void ga_mutation()
        {
            int i = rnd.Next(genom_x.Count);
            
            double val = Math.Min(FZ * genom_x[i], DY) - Math.Max(EZ * genom_x[i], CY);
            genom_x.Add(genom_x[i]);
            genom_y.Add(Math.Max(EZ * genom_x[i], CY) + rnd.NextDouble() * val);
            genom_fitness.Add(fitness_func(genom_x.Count - 1));
            
            val = Math.Min(genom_y[i] / EZ, BX) - Math.Max(genom_y[i] / FZ, AX);
            genom_x.Add(Math.Max(genom_y[i] / FZ, AX) + rnd.NextDouble() * val);
            genom_y.Add(genom_y[i]);
            genom_fitness.Add(fitness_func(genom_x.Count - 1));
            
        }

        private void ga_selection()
        {
           
            Sort();
            for (int i = ga_size; i < genom_x.Count; i++)
            {
                genom_x.RemoveAt(i);
                genom_y.RemoveAt(i);
                genom_fitness.RemoveAt(i);
            }
        }

    }

 }

