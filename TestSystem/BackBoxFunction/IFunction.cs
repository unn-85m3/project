using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;

namespace TestSystem.BackBoxFunction
{
    interface IFunction
    {
        IOutBlackBoxParam Calculate(Double x1,Double x2);
    }
}
