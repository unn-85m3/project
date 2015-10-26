using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm.Diagonal_Algoritm3.Lines;

namespace TestSystem.Algorithm.DiagonalAlgorithm3.Places
{
    interface IPlace
    {
        int generation { set; get; }
        int hasPoints { get; set; }
        ILine MainDiagonal { get; }
        ILine SecondDiagonal { get; }
        List<IPoint> points { get; }
        bool isSeparated { get; set; }
        IPoint GetPoint();
        int needSeparates { get; set; }

        double Area { get; }

    }
}
