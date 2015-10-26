using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm.DiagonalAlgorithm3.Places;
using TestSystem.Algorithm.DiagonalAlgorithm3.Separathors;
using TestSystem.Algorithm.DiagonalAlgorithm3.SubAlgorithms;
using TestSystem.DataFormat;

namespace TestSystem.Algorithm.DiagonalAlgorithm3
{
    class Factory
    {
        public static ISeparathor GetSeparathor()
        {
            return new Separathor();
        }


        public static ISubAlgorithm GetSubAlgorithm(ICalculateFunction function,IEnterBlackBoxParam param,int generation)
        {
            return new HillClimbing(function, param, generation);
        }

        /**
         * плоскость задаётся верхней левой и нижней правой точками
         * 
         */
        public static IPlace GetPlace(IPoint ul,IPoint dr)
        {
            return new Place(ul, dr);
        }


    }
}
