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
    abstract class AbsAlgorithm:IAlgorithm
    {
        protected IEnterBlackBoxParam parametr; ///парамтры, в рамках которых проводится оптимизация
        protected string name = "Имя";/// имя алгоритма+имя автора
        protected IFunction function;///функция для оптимизации

     
        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="parametr">входные параметры ф-и Ч.Я.</param>
        /// <param name="function">Используемая ф-ция</param>
        protected AbsAlgorithm(IEnterBlackBoxParam parametr,IFunction function)
        {
            this.parametr = parametr;
            this.function = function;
            
        }

        /// <summary>
        /// Имя алгоритма
        /// </summary>
        public virtual string Name
        {
            get { return name; }
        }

        /// <summary>
        /// через эту ф-ю можно изменить входные данные алгоритма
        /// </summary>
        public IEnterBlackBoxParam EnterParam
        {
            get { return parametr; }
            set { parametr = value; }
        }

        /// <summary>
        /// Здесь находится сам алгоритм отптимизации ф-и
        /// </summary>
        /// <returns>результат работы алгоритма</returns>
        public abstract IOutBlackBoxParam Calculate();



        
    }
}
