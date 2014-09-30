using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm;

namespace TestSystem.test_system
{
    interface ITestSystem
    {
        void addAlgorithm(IAlgorithm algorithm);
        IAlgorithm delAlgorithm(int id);
        int length { get; }
        void Test();
    }
}
