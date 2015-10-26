using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm.DiagonalAlgorithm3.Places;

namespace TestSystem.Algorithm.DiagonalAlgorithm3.Separathors
{
    interface ISeparathor
    {
        /**
         * делитель на области. Его реализуют все сепараторы
         * 
         */
        List<IPlace> Separate(IPlace place, IPoint point);
    }
}
