using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.DataFormat
{
    class OutBlackBoxParam : IOutBlackBoxParam
    {
        private double cost;

        public OutBlackBoxParam(double cost) { this.cost = cost; }

        public double Cost
        {
            get { return cost; }
        }
    }
}
