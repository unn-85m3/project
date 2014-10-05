using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm;
using TestSystem.DataFormat;
using TestSystem.Tasks;

namespace TestSystem.test_system
{
    interface IEndCalculate
    {
        /// <summary>
        /// Окончание вычислений
        /// </summary>
        /// <param name="alg">Алгоритм</param>
        /// <param name="task">Задание</param>
        /// <param name="rez">Результат</param>
        /// <param name="time">Время</param>
        void OnEndCalculate(IAlgorithm alg, ITaskPackage task, IOutBlackBoxParam rez, int time);

        /// <summary>
        /// Окончание задания
        /// </summary>
        /// <param name="alg">Алгоритм</param>
        /// <param name="task">Задание</param>
        /// <param name="time">Время</param>
        void OnEndTask(IAlgorithm alg,ITaskPackage task, int time);
    }
}
