using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;

namespace TestSystem.Algorithm
{
    interface ICalculateFunction
    {
        IOutBlackBoxParam Function(Double x1, Double x2);
    }
}
