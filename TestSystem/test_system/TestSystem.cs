using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm;

namespace TestSystem.test_system
{
    class TestSystem:ITestSystem
    {
        protected List<IAlgorithm> algorithms;
        public TestSystem()
        {
            algorithms = new List<IAlgorithm>();
        }

        public void addAlgorithm(IAlgorithm algorithm)
        {
             algorithms.Add(algorithm);
        }


        public IAlgorithm delAlgorithm(int id)
        {
            IAlgorithm alg = algorithms[id];
            algorithms.RemoveAt(id);
            return alg;
        }

        public int length
        {
            get { return algorithms.Count; }
        }




        public void Test()
        {
            throw new NotImplementedException();
        }
    }
}
