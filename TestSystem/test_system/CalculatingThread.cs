using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestSystem.Algorithm;
using TestSystem.DataFormat;
using TestSystem.Tasks;

namespace TestSystem.test_system
{
    class CalculatingThread
    {
        IEndCalculate listener;
        Thread thread;
        IAlgorithm alg;
        ITaskPackage task;

        public CalculatingThread(IAlgorithm alg,ITaskPackage task)
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
                listener.OnEndCalculate(alg,task, data, ime.Milliseconds);
            }

            
        }

        public void SetEndListener(IEndCalculate listener)
        {
            this.listener = listener;
        }
    }
}
