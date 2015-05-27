using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.BlackBox;
using TestSystem.DataFormat;

namespace TestSystem
{
    public interface IPoint
    {
        Double x1 { get; set; }
        Double x2 { get; set; }
        Double cost { get; set;}

    }
}
