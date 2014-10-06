using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Exception;
using TestSystem.DataFormat;

namespace TestSystem.BackBoxFunction
{
    class BlackBoxFunction : IFunction, IOutBlackBoxParam
    {
        private Double cost;
        IBlackBox blackBox;

        public void Init(IBlackBox blackBox)
        {
            this.blackBox = blackBox;
        }

        public IOutBlackBoxParam Calculate(double x1, double x2)
        {
            if (( x2-x1) < 0) throw new Exception();
            else
            {
                cost = (Math.Sin(x1) * Math.Cos(x2) + Math.Max(x1, x2)) / Math.Sqrt(x2-x1);
            }
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Cost
        {
            get { return cost; }
        }
    }
}
