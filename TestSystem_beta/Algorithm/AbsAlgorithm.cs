using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;

namespace TestSystem.Algorithm
{
    abstract class AbsAlgorithm:IAlgorithm
    {
        protected List<IEnterBlackBoxParam> parameters;
        protected AbsAlgorithm(List<IEnterBlackBoxParam> parameters)
        {
            this.parameters = parameters;
        }
    }
}
