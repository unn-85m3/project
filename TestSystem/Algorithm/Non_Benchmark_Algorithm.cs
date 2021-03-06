﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;
using TestSystem.BlackBox;

namespace TestSystem.Algorithm
{
    class Non_Benchmark_Algorithm : AbsAlgorithm
    {
        private double h = 1; //шаг

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
        public Non_Benchmark_Algorithm()
        {
            this.name = "Неэталонный алгоритм";
            this.atributs += "Шаг алгоритма = " + h + " Па.";//Изменять при внесении изменений
        }

        /// <summary>
        /// Здесь находится сам алгоритм оптимизации ф-и
        /// </summary>
        /// <returns>результат работы алгоритма</returns>
        public override DataFormat.IOutBlackBoxParam Calculate()
        {
            //double k = -1, cost = -1;
            int n = 0; //количество успешных вычислений функции
            double cost = double.MinValue;
            IOutBlackBoxParam a;

            for (double i = this.parametr.x1_min; i <= this.parametr.x1_max; i = i + h)
                for (double j = this.parametr.x2_min; j <= this.parametr.x2_max; j = j + h)
                    if (((j / i) <= this.parametr.x2_x1_max) && ((j / i) >= this.parametr.x2_x1_min))
                    {
                        try
                        {
                             a = Function(i, j);
                            
                            n++;
                            if (n == 1)
                                cost = a.Cost;
                            else if (a.Cost > cost)
                                cost = a.Cost;
                        }
                        catch
                        {
                        }
                        
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
