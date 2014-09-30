using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;

namespace TestSystem.Algorithm
{
    interface ICalculateListener
    {
        void onCalculate(IOutBlackBoxParam param);
    }
}
