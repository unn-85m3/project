using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;
using TestSystem.BlackBox;

namespace TestSystem.Algorithm
{
    /**
     * Этот интерфейс должны наследовать все алгоритмы
     */
   public interface IAlgorithm : IAlg
    {
        /// <summary>
        /// Здесь находится сам алгоритм отптимизации ф-и
        /// </summary>
        /// <returns>результат работы алгоритма</returns>
        IOutBlackBoxParam Calculate();

        /// <summary>
        /// Установка оптимизируемой ф-и
        /// </summary>
        void SetFunction(IFunction function);

        void Refresh();
        
    }
}
