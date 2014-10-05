using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;
using TestSystem.BackBoxFunction;

namespace TestSystem.Algorithm
{
    class Benchmark_Algorithm : AbsAlgorithm
    {
        private string name = "Эталонный алгоритм";

        public Benchmark_Algorithm(IEnterBlackBoxParam parameter,IFunction function) : base( parameter, function){}

        public override DataFormat.IOutBlackBoxParam Calculate()
        {
            throw new NotImplementedException();
        }

        public override string Name
        {
            get { return name; }
        }
    }
}
