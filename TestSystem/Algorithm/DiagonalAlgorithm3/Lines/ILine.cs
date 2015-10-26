using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Algorithm.Diagonal_Algoritm3.Lines
{
    interface ILine
    {
        IPoint PointStart { get; set; }
        IPoint PointEnd { get; set; }
        Double length { get; }
        IPoint GetPoint(Double len);
    }
}
