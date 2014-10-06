using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm;
using TestSystem.Tasks;
using TestSystem.DataFormat;
using TestSystem.BlackBoxFunction;

namespace TestSystem.test_system
{
    class TestSystem:ITestSystem
    {
        protected List<IAlgorithm> algorithms;
        private List<ITaskPackage> tasks;
        private BlackBoxFunction.BlackBoxFunction function;
        private IEndCalculate listener;
        private Listener thListener;
        /// <summary>
        /// Конструктор
        /// </summary>
        public TestSystem(List<ITaskPackage> tasks, BlackBoxFunction.BlackBoxFunction function)
        {
            this.tasks = tasks;
            this.function = function;
            algorithms = new List<IAlgorithm>();
        }

        /// <summary>
        /// Добавление алгоритма
        /// </summary>
        /// <param name="algorithm">Алгоритм</param>
        public void AddAlgorithm(IAlgorithm algorithm)
        {
             algorithms.Add(algorithm);

        }

        /// <summary>
        /// Удаление алгоритма
        /// </summary>
        /// <param name="id">Идентификатор доступа</param>
        /// <returns>Возвращает удаленный алгоритм</returns>
        public IAlgorithm DelAlgorithm(int id)
        {
            IAlgorithm alg = algorithms[id];
            algorithms.RemoveAt(id);
            return alg;
        }

        /// <summary>
        /// Кол-во алгоритмов
        /// </summary>
        public int Length
        {
            get { return algorithms.Count; }
        }

        /// <summary>
        /// Тест!!!
        /// </summary>
        public void Test()
        {
            foreach(IAlgorithm alg in algorithms)
            {
                
                CalculatingThread th = new CalculatingThread(alg,tasks,function);
                thListener = new Listener(listener);
                th.SetEndListener(thListener);
                th.Start();
            }
        }


        private class Listener:IEndCalculate
        {
            IEndCalculate listener;
            public Listener(IEndCalculate listener)
            {
                this.listener = listener;
            }

            public void SetListener(IEndCalculate listener)
            {
                this.listener = listener;
            }

            public void OnEndCalculate(IAlgorithm alg, ITaskPackage task, IOutBlackBoxParam rez, int time)
            {
                if (listener!=null)
                    listener.OnEndCalculate(alg, task, rez, time);
            }

            public void OnEndTask(IAlgorithm alg, ITaskPackage task, IOutBlackBoxParam rez, int time)
            {
                if (listener != null)
                    listener.OnEndTask(alg, task, rez, time);
            }
        }


        public void SetListener(IEndCalculate listener)
        {
            this.listener = listener;
            if (thListener != null)
                thListener.SetListener(listener);
        }

       /* /// <summary>
        /// Окончание вычислений
        /// </summary>
        /// <param name="alg">Алгоритм</param>
        /// <param name="task">Задание</param>
        /// <param name="rez">Результат</param>
        /// <param name="time">Время</param>
        public void OnEndCalculate(IAlgorithm alg, ITaskPackage task, IOutBlackBoxParam rez, int time)
        {
            ///throw new NotImplementedException();
        }

        /// <summary>
        /// Окончание задания
        /// </summary>
        /// <param name="alg">Алгоритм</param>
        /// <param name="task">Задание</param>
        /// <param name="time">Время</param>
        public void OnEndTask(IAlgorithm alg,ITaskPackage task, int time)
        {
            throw new NotImplementedException();
        }*/
    }
}
