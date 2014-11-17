using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.DataFormat
{
    class EnterBlackBoxParam : IEnterBlackBoxParam
    {
        double x1min;
        double x1max;
        double x2min;
        double x2max;
        double x1x2_min;
        double x1x2_max;

        public EnterBlackBoxParam(double x1_min, double x1_max, double x2_min, double x2_max, double x1_x2_min, double x1_x2_max)
        {
            this.x1max = x1_max;
            this.x1min = x1_min;
            this.x2max = x2_max;
            this.x2min = x2_min;
            this.x1x2_max = x1_x2_max;
            this.x1x2_min = x1_x2_min;
        }

        public Double x1_min
        {
            get { return x1min; }
        }

        public Double x1_max
        {
            get { return x1max; }
        }

        public Double x2_min
        {
            get { return x2min; }
        }

        public Double x2_max
        {
            get { return x2max; }
        }

        public Double x2_x1_min
        {
            get { return x1x2_min; }
        }

        public Double x2_x1_max
        {
            get { return x1x2_max; }
        }
    };
}
