using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm;

namespace TestSystem.test_system
{
    public interface ITestSystem
    {
        /// <summary>
        /// Добавление алгоритма
        /// </summary>
        /// <param name="algorithm">Алгоритм</param>
        void AddAlgorithm(IAlgorithm algorithm);

        /// <summary>
        /// Добавление алгоритма
        /// </summary>
        /// <param name="algorithm">Алгоритм</param>
         void AddAlgorithm(params IAlgorithm[] algorithm);

        /// <summary>
        /// Добавление алгоритма
        /// </summary>
        /// <param name="algorithm">Алгоритм</param>
         void AddAlgorithm(List<IAlgorithm> algorithm);

        /// <summary>
        /// Удаление алгоритма
        /// </summary>
        /// <param name="id">Идентификатор доступа</param>
        /// <returns>Возвращает удаленный алгоритм</returns>
         IAlgorithm DelAlgorithm(int id);

        /// <summary>
        /// Список алгоритмов
        /// </summary>
        /// <returns></returns>
         List<IAlg> GetAlgorithms { get; }


        ///// <summary>
        ///// Кол-во алгоритмов
        ///// </summary>
        //int Length { get; }

        /// <summary>
        /// Тест!!!
        /// </summary>
        void Test();
    }
}
