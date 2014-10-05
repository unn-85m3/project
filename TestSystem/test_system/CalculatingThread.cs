using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestSystem.Algorithm;
using TestSystem.DataFormat;
using TestSystem.Tasks;
using TestSystem.BackBoxFunction;

namespace TestSystem.test_system
{
    class CalculatingThread
    {
        IEndCalculate listener;
        Thread thread;
        IAlgorithm alg;
        BlackBoxFunction function;
        List<ITaskPackage> tasks;

      /// <summary>
        /// Инициализация потока для алгоритма
        /// </summary>
        /// <param name="alg">Алгоритм</param>
        /// <param name="tasks">Задания</param>
        /// <param name="function">Оптимизируемая функция</param>
        /// 
        public CalculatingThread(IAlgorithm alg,List<ITaskPackage> tasks,BlackBoxFunction function)
        {
            this.alg = alg;           
            this.tasks = tasks;
            this.function = function;
           
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
            int I=0;
            int n=tasks.Count;
            DateTime dd=DateTime.MinValue;
            TimeSpan ime = TimeSpan.MinValue;
            IOutBlackBoxParam data = null;
            foreach(ITaskPackage task in tasks)
            {
                alg.EnterParam=task.EnterParams;
                int BlackBoxesCount = task.BlackBoxes.Count;

                for (int i = 0; i < BlackBoxesCount;i++ )
                {

                    dd = DateTime.Now;
                    function.Init(task.BlackBoxes[i]);
                    data = alg.Calculate();
                    ime = DateTime.Now - dd;
                    if (listener != null)
                    {
                        listener.OnEndCalculate(alg, task, data, ime.Milliseconds);
                    }
                    I++;
                }
                listener.OnEndTask(alg, task, data, ime.Milliseconds);
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
