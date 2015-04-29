using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;
using TestSystem.BlackBox;
using TestSystem.Algorithm.Diagonal_Algoritm;

namespace TestSystem.Algorithm
{
    /**
     * Этот интерфейс должны наследовать все алгоритмы
     */
    public interface IAlgorithm
    {

        void setCalculateListener(Logger.ICalculateListener listener);

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
        /// Шаг алгоритма
        /// </summary>
        double Step { get; set; }

        /// <summary>
        /// Параметры алгоритма (различные параметры, такие как шаг или колличество итераций) (нечто вроде версии алгоритма)
        /// </summary>
        string Atributs { get; }

        /// <summary>
        /// Установка оптимизируемой ф-и
        /// </summary>
        void SetFunction(IFunction function);


        List<IPoint> points { get; }

        int Calls { get; }
        void Refresh();

    }
}
