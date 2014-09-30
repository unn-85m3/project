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
            DateTime dd = DateTime.Now;

            IOutBlackBoxParam data = alg.Calculate();

            TimeSpan ime =  DateTime.Now - dd;
            if (listener != null)
            {
                listener.onEndCalculate(alg, data, ime.Milliseconds);
            }

            
        }

        public void setEndListener(IEndCalculate listener)
        {
            this.listener = listener;
        }
    }
}
