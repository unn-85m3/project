using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.BlackBox;
using TestSystem.DataFormat;

namespace TestSystem.Algorithm.Diagonal_Algoritm
{
    interface IPoint
    {
        Double x { get; set; }
        Double y { get; set; }
        IOutBlackBoxParam cost { get; set;}

    }
}
