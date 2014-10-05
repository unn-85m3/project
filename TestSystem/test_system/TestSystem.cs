using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm;
using TestSystem.Tasks;
using TestSystem.DataFormat;

namespace TestSystem.test_system
{
    class TestSystem:ITestSystem,IEndCalculate
    {
        protected List<IAlgorithm> algorithms;
        public TestSystem()
        {
            algorithms = new List<IAlgorithm>();
        }

        public void AddAlgorithm(IAlgorithm algorithm)
        {
             algorithms.Add(algorithm);
        }


        public IAlgorithm DelAlgorithm(int id)
        {
            IAlgorithm alg = algorithms[id];
            algorithms.RemoveAt(id);
            return alg;
        }

        public int Length
        {
            get { return algorithms.Count; }
        }




        public void Test()
        {
            foreach(IAlgorithm alg in algorithms)
            {
                
                CalculatingThread th = new CalculatingThread(alg,null);
                th.SetEndListener(this);
                th.Start();
            }
        }

        public void OnEndCalculate(IAlgorithm alg, ITaskPackage task, IOutBlackBoxParam rez, int time)
        {
            ///throw new NotImplementedException();
        }


        public void OnEndTask(IAlgorithm alg,ITaskPackage task, int time)
        {
            throw new NotImplementedException();
        }
    }
}
