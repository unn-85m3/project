﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;
using TestSystem.BlackBox;

namespace TestSystem.Algorithm
{
    /// <summary>
    /// От него наследуются все алгоритмы.
    /// Уэтого класса нет конструктора без параметра. Это сделано во избежние ошибок !!!
    /// </summary>
    abstract class AbsAlgorithm : IAlgorithm
    {
        protected IEnterBlackBoxParam parametr; ///парамтры, в рамках которых проводится оптимизация
        protected string name = "Имя";/// имя алгоритма+имя автора
        protected string atributs = "Параметры алгоритма: ";// Параметры алгоритма
        private IFunction _function;///функция для оптимизации
        private int calls;


        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="parametr">входные параметры ф-и Ч.Я.</param>
        /// <param name="function">Используемая ф-ция</param>
        protected AbsAlgorithm()
        {
            calls = 0;
        }

        /// <summary>
        /// Имя алгоритма
        /// </summary>
        public virtual string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Параметры алгоритма (различные параметры, такие как шаг или колличество итераций) (нечто вроде версии алгоритма)
        /// </summary>
        public string Atributs
        {
            get
            {
                return atributs;
            }
        }


        /// <summary>
        /// через эту ф-ю можно изменить входные данные алгоритма
        /// </summary>
        public IEnterBlackBoxParam EnterParam
        {
            get { return parametr; }
            set { parametr = value; }
        }


        public void SetFunction(IFunction function)
        {
            this.function = function;
        }

        private IFunction function
        {
            set { _function = value; }
            get { return _function; }
        }


        public IOutBlackBoxParam Function(Double x1, Double x2)
        {
            calls++;
            return function.Calculate(x1, x2);
        }

        /// <summary>
        /// Здесь находится сам алгоритм отптимизации ф-и
        /// </summary>
        /// <returns>результат работы алгоритма</returns>
        public abstract IOutBlackBoxParam Calculate();




        public int Calls
        {
            get { return calls; }
        }

        public void Refresh()
        {
            calls = 0;
        }

        protected int SetAreaOfTheRegion(int h) //я просто оставлю это здесь
        {
            int n = 0;
            for (double i = this.parametr.x1_min; i <= this.parametr.x1_max; i += h)
                for (double j = this.parametr.x2_min; j <= this.parametr.x2_max; j += h)
                    if (((j / i) <= this.parametr.x2_x1_max) && ((j / i) >= this.parametr.x2_x1_min))
                    {
                        n++;
                    }
            return n;
        }
    }
}
