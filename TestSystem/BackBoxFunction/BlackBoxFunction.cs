using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;

namespace TestSystem.BackBoxFunction
{
    class BlackBoxFunction : IFunction, IOutBlackBoxParam
    {
        private Double cost;
        public IOutBlackBoxParam Calculate(double x1, double x2)
        {
            cost = Math.Sin(x1) * Math.Cos(x2);
            return this;
        }

        public double Cost
        {
            get { return cost; }
        }
    }
}
