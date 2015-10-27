using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm;
using TestSystem.Tasks;
using TestSystem.DataFormat;
using TestSystem.BlackBox;

namespace TestSystem.test_system
{
    class TestSystem:ITestSystem
    {
        protected List<IAlgorithm> algorithms;
        private List<ITaskPackage> tasks;
       // private BlackBoxFunction function;
        private IEndCalculate listener;
        private Listener thListener;

        private List<List<Dictionary<List<ParametrNow>, DataFormat.IOutBlackBoxParam>>> ArrayBestParamsInAlgs;
        /// <summary>
        /// Конструктор
        /// </summary>
        public TestSystem(List<ITaskPackage> tasks)//, BlackBoxFunction function)
        {
            this.tasks = tasks;
           // this.function = function;
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
        /// Добавление алгоритма
        /// </summary>
        /// <param name="algorithm">Алгоритм</param>
        public void AddAlgorithm(params IAlgorithm[] algorithm)
        {
            foreach (var alg in algorithm)
                algorithms.Add(alg);
        }

        /// <summary>
        /// Добавление алгоритма
        /// </summary>
        /// <param name="algorithm">Алгоритм</param>
        public void AddAlgorithm(List<IAlgorithm> algorithm)
        {
            foreach (var alg in algorithm)
                algorithms.Add(alg);
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
            ArrayBestParamsInAlgs = new List<List<Dictionary<List<ParametrNow>, DataFormat.IOutBlackBoxParam>>>();
            foreach(AbsAlgorithm alg in algorithms)
            {
                ArrayBestParamsInAlgs.Add(new List<Dictionary<List<ParametrNow>, DataFormat.IOutBlackBoxParam>>());
                BlackBoxFunction function = new BlackBoxFunction();
                alg.SetFunction(function);
                CalculatingThread th = new CalculatingThread(alg, tasks, function);
                thListener = new Listener(listener);
                th.SetEndListener(thListener);
                th.Start();
                while (th.isAlive)
                {
                }
                if (alg is Optimizate)
                {
                    ArrayBestParamsInAlgs[ArrayBestParamsInAlgs.Count - 1] = ((Optimizate)alg).GetNowListParams;
                }
                else
                {
                    ArrayBestParamsInAlgs[ArrayBestParamsInAlgs.Count - 1].Add(new Dictionary<List<ParametrNow>, DataFormat.IOutBlackBoxParam>());
                    ArrayBestParamsInAlgs[ArrayBestParamsInAlgs.Count - 1][ArrayBestParamsInAlgs[ArrayBestParamsInAlgs.Count - 1].Count - 1].Add(alg.ParamNow, (new OutBlackBoxParam(0)));
                }
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


        public List<List<Dictionary<List<ParametrNow>, DataFormat.IOutBlackBoxParam>>> ArrayBestParams()
        {
            return ArrayBestParamsInAlgs;
        }
    }
}
