using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm;
using TestSystem.DataFormat;
using TestSystem.Tasks;

namespace TestSystem.test_system
{
    interface IEndCalculate
    {
        void OnEndCalculate(IAlgorithm alg, ITaskPackage task, IOutBlackBoxParam rez, int time);
        void OnEndTask(IAlgorithm alg,ITaskPackage task, int time);
    }
}
