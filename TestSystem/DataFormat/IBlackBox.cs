using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.DataFormat
{
    public interface IBlackBox
    {
        /// <summary>
        /// Информация о ЧЯ
        /// </summary>
        String Info{ get; }

        /// <summary>
        /// Температура на входе
        /// </summary>
        String TIn { get; }

        /// <summary>
        /// Плотность по воздуху
        /// </summary>
        String DIn { get; }

        /// <summary>
        /// Теплопроводная способность
        /// </summary>
        String СIn { get; }

        /// <summary>
        /// Входное давление
        /// </summary>
        String PIn { get; }

        /// <summary>
        /// Выходное давление
        /// </summary>
        String POut { get; }

        /// <summary>
        /// Объем на выходе
        /// </summary>
        String QOut { get; }

    }
}
