using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Algorithm
{
    class Genetic_Algorithm : AbsAlgorithm
    {
        private string name = "Генетический алгоритм";

        public override DataFormat.IOutBlackBoxParam Calculate()
        {
            throw new NotImplementedException();
        }

        public override string Name
        {
            get { return this.name; }
        }
    }
}
