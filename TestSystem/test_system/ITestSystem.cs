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
        void AddAlgorithm(IAlgorithm algorithm);
        IAlgorithm DelAlgorithm(int id);
        int Length { get; }
        void Test();
    }
}
