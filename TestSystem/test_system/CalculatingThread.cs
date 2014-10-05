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


        /// <summary>
        /// Инициализация потока для алгоритма
        /// </summary>
        /// <param name="alg">Алгоритм</param>
        /// <param name="tasks">Задания</param>
        public CalculatingThread(IAlgorithm alg,List<ITaskPackage> tasks)
        {
            this.alg = alg;           
        }


        /// <summary>
        /// Запуск потока
        /// </summary>
        public void Start()
        {
            thread = new Thread(this.Calc);
            thread.Start();
        }


        /// <summary>
        /// Прогон заданий на этом алгориме
        /// </summary>
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

        /// <summary>
        /// Установка слушателя
        /// </summary>
        /// <param name="listener"></param>
        public void SetEndListener(IEndCalculate listener)
        {
            this.listener = listener;
        }
    }
}
