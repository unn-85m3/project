using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm;

namespace TestSystem.test_system
{
    class TestSystem:ITestSystem,IEndCalculate
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
            foreach(IAlgorithm alg in algorithms)
            {
                
                CalculatingThread th = new CalculatingThread(alg);
                th.setEndListener(this);
                th.Start();
            }
        }

        public void onEndCalculate(IAlgorithm alg, DataFormat.IOutBlackBoxParam rez, int time)
        {
            ///throw new NotImplementedException();
        }


        public void onEndAlgorithm(IAlgorithm alg, int time)
        {
            throw new NotImplementedException();
        }
    }
}
