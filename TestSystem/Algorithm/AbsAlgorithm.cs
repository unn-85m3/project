using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;

namespace TestSystem.Algorithm
{

    class AbsAlgorithm:IAlgorithm
    {
        protected IEnterBlackBoxParam parameter;
        protected String name;
        protected AbsAlgorithm(IEnterBlackBoxParam parameter=null)
        {
            this.parameter = parameter;
        }

        public string Name
        {
            get { return name; }
        }


        public abstract IOutBlackBoxParam Calculate();



        public IEnterBlackBoxParam enterParam
        {
            set { parameter=value; }
        }
    }
}
