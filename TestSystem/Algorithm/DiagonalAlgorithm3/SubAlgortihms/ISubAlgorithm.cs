using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm.Diagonal_Algoritm3;
using TestSystem.Algorithm.Diagonal_Algoritm3.Lines;

namespace TestSystem.Algorithm.DiagonalAlgorithm3.SubAlgorithms
{
    /**
     * 
     * Его реализуют все алгоритмы одномерной оптимизации (Хилл климбинг)
     */
    interface ISubAlgorithm
    {
        /**
         * максимальное число точек, которое может бросить алгоритм
         * 
         * возвращает оставшееся колличество точек
         */
        int maxPoints { set; get; }

        /**
         * рассчёт
         */
        IPoint Calculate(ILine line);

        IPoint Calculate(ILine line, double step);
    }
}
