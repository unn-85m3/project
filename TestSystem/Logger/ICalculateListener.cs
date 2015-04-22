using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Logger
{
    interface ICalculateListener
    {
        public void onCalculate(double x1, double x2, double cost);
    }
}
