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
    public interface IOutBlackBoxParam
    {
        /// <summary>
        /// Стоимость
        /// </summary>
        Double Cost { get; }
    }
}
