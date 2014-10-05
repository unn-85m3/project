using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm;

namespace TestSystem.test_system
{
    interface ITestSystem
    {
        /// <summary>
        /// Добавление алгоритма
        /// </summary>
        /// <param name="algorithm">Алгоритм</param>
        void AddAlgorithm(IAlgorithm algorithm);

        /// <summary>
        /// Удаление алгоритма
        /// </summary>
        /// <param name="id">Идентификатор доступа</param>
        /// <returns>Возвращает удаленный алгоритм</returns>
        IAlgorithm DelAlgorithm(int id);

        /// <summary>
        /// Кол-во алгоритмов
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Тест!!!
        /// </summary>
        void Test();
    }
}
