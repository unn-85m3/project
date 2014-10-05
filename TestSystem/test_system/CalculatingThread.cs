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
           
        }


        public void Start()
        {
            thread = new Thread(this.Calc);
            thread.Start();
        }

        protected void Calc()
        {
            DateTime dd = DateTime.Now;

            IOutBlackBoxParam data = alg.Calculate();

            TimeSpan ime =  DateTime.Now - dd;
            if (listener != null)
            {
                listener.OnEndCalculate(alg, data, ime.Milliseconds);
            }

            
        }

        public void SetEndListener(IEndCalculate listener)
        {
            this.listener = listener;
        }
    }
}
