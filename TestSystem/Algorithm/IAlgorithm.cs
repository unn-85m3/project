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
   public interface IAlgorithm
    {
        /// <summary>
        /// через эту ф-ю можно изменить входные данные алгоритма
        /// </summary>
        IEnterBlackBoxParam EnterParam { get; set; }

        /// <summary>
        /// Здесь находится сам алгоритм отптимизации ф-и
        /// </summary>
        /// <returns>результат работы алгоритма</returns>
        IOutBlackBoxParam Calculate();

        /// <summary>
        /// Имя алгоритма
        /// </summary>
        String Name {get; }

        /// <summary>
        /// Установка оптимизируемой ф-и
        /// </summary>
        void SetFunction(IFunction function);
        
    }
}
