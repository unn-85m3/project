﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;
using TestSystem.BackBoxFunction;

namespace TestSystem.Algorithm
{
    /// <summary>
    /// От него наследуются все алгоритмы.
    /// Уэтого класса нет конструктора без параметра. Это сделано во избежние ошибок !!!
    /// </summary>
    abstract class AbsAlgorithm:IAlgorithm
    {
        protected IEnterBlackBoxParam parameter; ///парамтры, в рамках которых проводится оптимизация
        private string name = "Имя";/// имя алгоритма+имя автора
        protected IFunction function;///функция для оптимизации

     


        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="parameter">входные параметры ф-и Ч.Я.</param>
        /// <param name="function">Используемая ф-ция</param>
        protected AbsAlgorithm(IEnterBlackBoxParam parameter,IFunction function)
        {
            this.parameter = parameter;
            this.function = function;
            
        }

        public virtual string Name
        {
            get { return name; }
        }

        /// <summary>
        /// через эту ф-ю можно изменить входные данные алгоритма
        /// </summary>
        public IEnterBlackBoxParam EnterParam
        {
            get { return parameter; }
            set { parameter = value; }
        }

        /// <summary>
        /// Здесь находится сам алгоритм отптимизации ф-и
        /// </summary>
        /// <returns>результат работы алгоритма</returns>
        public abstract IOutBlackBoxParam Calculate();



        
    }
}
