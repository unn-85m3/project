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
        Double p_in_min { get; }
        Double p_in_max { get; }

        Double p_out_min { get; }
        Double p_out_max { get; }

        Double comprimate_min { get; }
        Double comprimate_max { get; } 
    }
}
