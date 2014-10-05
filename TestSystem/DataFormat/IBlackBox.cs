using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.DataFormat
{
    interface IBlackBox
    {
        String Info{ get; }
        String TIn { get; }
        String DIn { get; }
        String PIn { get; }
        String POut { get; }
        String QOut { get; }

    }
}
