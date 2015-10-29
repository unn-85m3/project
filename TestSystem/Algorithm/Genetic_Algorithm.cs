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
        private int ga_Nc;
        private int ga_Nm;
        private double h;

        static Random rnd = new Random(0);

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="parameter">входные параметры ф-и Ч.Я.</param>
        /// <param name="function">Используемая ф-ция</param>
        public Genetic_Algorithm() 
        {
            this.name = "Генетический алгоритм";
            this.atributs += ""; //ЗАПОЛНИ!!!!!!!!!!!!!
        }

        public Genetic_Algorithm(double step)
            : base(step)
        {
            this.name = "Генетический алгоритм";
            this.atributs += ""; //ЗАПОЛНИ!!!!!!!!!!!!!
        }

        public Genetic_Algorithm(List<ParametrNow> pNow)
            : base(pNow.Find(s => s.name == "step").value)
        {
            this.name = "Генетический алгоритм";
            this.atributs += "";
            ParamNow = pNow;
            ga_size = (int)pNow.Find(s => s.name == "population size").value;
            ga_Nc = (int)pNow.Find(s => s.name == "number of crossovers per generation").value;
            ga_Nm = (int)pNow.Find(s => s.name == "number of mutations per generation").value;
        }

        private List<Parametr> SetParams()
        {
            return new List<Parametr> {new Parametr{name = "population size", tp = TypeParams.discrete, minValue = 4, maxValue = 50},
                                       new Parametr{name = "number of crossovers per generation", tp = TypeParams.discrete, minValue = 1, maxValue = 5},
                                       new Parametr{name = "number of mutations per generation", tp = TypeParams.discrete, minValue = 1, maxValue = 2},
                                       new Parametr{name = "step", tp = TypeParams.continuous, minValue = 1, maxValue = 1} };
        }

        /// <summary>
        /// Здесь находится сам алгоритм оптимизации ф-и
        /// </summary>
        /// <returns>результат работы алгоритма</returns>
        public override DataFormat.IOutBlackBoxParam Calculate()
        {
            double cost = double.MaxValue;

            AX = this.parametr.x1_min;
            BX = this.parametr.x1_max;
            CY = this.parametr.x2_min;
            DY = this.parametr.x2_max;
            EZ = this.parametr.x2_x1_min;
            FZ = this.parametr.x2_x1_max;

            h = SetAreaOfTheRegion(STEP);

            ga_size = (int)h/4 + 4;
            ga_T = (int)(3*h/16);
            genom_x = new List<double>(ga_size);
            genom_y = new List<double>(ga_size);
            genom_fitness = new List<double>(ga_size);
            
            ga_init();
            for (int i = 0; i < ga_T; i++)
            {
                for (int j = 0; j < ga_Nc; j++)
                    ga_crossover();
                for (int k = 0; k < ga_Nm; k++)
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

        private void ga_crossover()
        {
            int n = genom_x.Count;
            int j, k;
            double r;
            List<double> rangs = new List<double>();
            Sort();

            double R = n * (n + 1) / 2;

            for (int i = 0; i < n; i++)
            {
                rangs.Add((i + 1) / R);
            }
            rangs.Reverse();

            int child = 0;
            double val_x, val_y, s;

            do
            {
                r = rnd.NextDouble();
                j = rnd.Next(n);

                do
                {
                    k = rnd.Next(n);
                }
                while (k == j);
                if ((rangs[j] > r) && (rangs[k] > r))
                {
                    if (genom_fitness[j] < genom_fitness[k])
                    {
                        s = genom_fitness[j] / genom_fitness[k];
                        val_x = (genom_x[j] + genom_x[k] * s) / (1 + s);
                        val_y = (genom_y[j] + genom_y[k] * s) / (1 + s);
                    }
                    else
                    {
                        s = genom_fitness[k] / genom_fitness[j];
                        val_x = (genom_x[k] + genom_x[j] * s) / (1 + s);
                        val_y = (genom_y[k] + genom_y[j] * s) / (1 + s); ;
                    }
                    genom_x.Add(val_x);
                    genom_y.Add(val_y);
                    genom_fitness.Add(fitness_func(genom_x.Count - 1));
                    child++;
                }
            }
            while (child != 2);      
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

        public override List<Parametr> GetAllParam
        {
            get { return SetParams(); }
        }
    }

 }

