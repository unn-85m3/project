using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.DataFormat
{
    /**
     * интерфейс, наследуемый классом, содержащим выходные параметры чёрного ящика
     */
    public interface IEnterBlackBoxParam
    {

        /// <summary>
        /// Левая граница X1
        /// </summary>
        Double x1_min { get; }
        /// <summary>
        /// Правая граница X1
        /// </summary>
        Double x1_max { get; }

        /// <summary>
        /// Левая граница X2
        /// </summary>
        Double x2_min { get; }
        /// <summary>
        /// Правая граница X2
        /// </summary>
        Double x2_max { get; }

        /// <summary>
        /// Левая граница X2/X1
        /// </summary>
        Double x2_x1_min { get; }
        /// <summary>
        /// Правая граница X2/X1
        /// </summary>
        Double x2_x1_max { get; } 
    }
}
