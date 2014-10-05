using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;
using TestSystem.BackBoxFunction;

namespace TestSystem.Algorithm
{
    class Benchmark_Algorithm : AbsAlgorithm
    {
        private string name = "Эталонный алгоритм";

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="parameter">входные параметры ф-и Ч.Я.</param>
        /// <param name="function">Используемая ф-ция</param>
        public Benchmark_Algorithm(IEnterBlackBoxParam parameter,IFunction function) : base( parameter, function){}


        /// <summary>
        /// Здесь находится сам алгоритм отптимизации ф-и
        /// </summary>
        /// <returns>результат работы алгоритма</returns>
        public override DataFormat.IOutBlackBoxParam Calculate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Имя алгоритма
        /// </summary>
        public override string Name
        {
            get { return name; }
        }
    }
}
