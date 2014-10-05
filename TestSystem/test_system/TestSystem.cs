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

        /// <summary>
        /// Конструктор
        /// </summary>
        public TestSystem()
        {
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
                
                CalculatingThread th = new CalculatingThread(alg,null);
                th.SetEndListener(this);
                th.Start();
            }
        }

        /// <summary>
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
        }
    }
}
