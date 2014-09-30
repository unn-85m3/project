using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm;
using TestSystem.DataFormat;

namespace TestSystem.test_system
{
    interface IEndCalculate
    {
        void onEndCalculate(IAlgorithm alg,IOutBlackBoxParam rez);
    }
}
