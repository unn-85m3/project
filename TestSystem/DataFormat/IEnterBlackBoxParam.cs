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
    interface IEnterBlackBoxParam
    {
        Double x1_min { get; }
        Double x1_max { get; }

        Double x2_min { get; }
        Double x2_max { get; }

        Double x2_x1_min { get; }
        Double x2_x1_max { get; } 
    }
}
