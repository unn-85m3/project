using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;
using TestSystem.BackBoxFunction;

namespace TestSystem.Algorithm
{
    class Benchmark_Algorithm : AbsAlgorithm
    {
        private string name = "Эталонный алгоритм";
        private double h; //шаг

        public double H
        {
            get { return h; }
            set { h = value; }
        }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="parameter">входные параметры ф-и Ч.Я.</param>
        /// <param name="function">Используемая ф-ция</param>
        public Benchmark_Algorithm(IEnterBlackBoxParam parameter, IFunction function) : base(parameter, function)
        {
            this.name = "Эталонный алгоритм";
        }

        /// <summary>
        /// Здесь находится сам алгоритм оптимизации ф-и
        /// </summary>
        /// <returns>результат работы алгоритма</returns>
        public override DataFormat.IOutBlackBoxParam Calculate()
        {
            //double k = -1, cost = -1;
            int n = 0; //количество успешных вычислений функции

            for (double i = parametr.x1_min; i <= parametr.x1_max; i = i + h)
                for (double j = parametr.x2_min; j <= parametr.x2_max; j = j + h)
                    if (((j / i) <= parametr.x2_x1_max) && ((j / i) >= parametr.x2_x1_min))
                    {
                        try
                        {
                            function.Сalculate(i, j);
                            n++;
                        }
                        catch
                        {
                        }
                        /*if (n == 1)
                            cost = k;
                        else if (k < cost)
                            cost = k;*/
                    }
            return new DataFormat.OutBlackBoxParam(cost);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Имя алгоритма
        /// </summary>
        public override string Name
        {
            get { return name; }
        }
    }
}
