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
        protected List<IEnterBlackBoxParam> parameters;
        protected String name;
        protected AbsAlgorithm(List<IEnterBlackBoxParam> parameters)
        {
            this.parameters = parameters;
        }

        public string Name
        {
            get { return name; }
        }


        public int length
        {
            get {
                if (parameters != null)
                {
                    return parameters.Count;
                }
                throw new NullReferenceException(); 
            }
        }


        public abstract IOutBlackBoxParam Calculate();


    }
}
