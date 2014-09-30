using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestSystem.Algorithm;
using TestSystem.DataFormat;

namespace TestSystem.test_system
{
    class CalculatingThread
    {
        IEndCalculate listener;
        Thread thread;
        IAlgorithm alg;

        public CalculatingThread(IAlgorithm alg)
        {
            this.alg = alg;
            thread = new Thread(this.calc);
            
            
        }


        public void Start()
        {
            thread.Start();
        }

        protected void calc()
        {
            IOutBlackBoxParam data = alg.Calculate();
            if (listener != null)
            {
                listener.onEndCalculate(alg, data);
            }
        }

        public void setEndListener(IEndCalculate listener)
        {
            this.listener = listener;
        }
    }
}
