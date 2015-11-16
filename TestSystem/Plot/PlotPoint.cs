using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Plot
{
    class PlotPoint : IPoint
    {
        private double _x1;
        private double _x2;
        private double _cost;

        public PlotPoint(double x1, double x2, double cost)
        {
            this._x1 = x1;
            this._x2 = x2;
            this._cost = cost;
        }
        public int CompareTo(object obj)
        {
            if (obj is IPoint)
            {
                var o = (IPoint)obj;
                if (o.cost > this.cost)
                {
                    return -1;
                }
                else if (o.cost < this.cost)
                {
                    return 1;
                }
                else return 0;
            }
            else throw new Exception("not IPoint");
        }

        public double x1
        {
            get
            {
                return _x1;
            }
            set
            {
                _x1 = value;
            }
        }

        public double x2
        {
            get
            {
                return _x2;
            }
            set
            {
                _x2 = value;
            }
        }

        public double cost
        {
            get
            {
                return _cost;
            }
            set
            {
                _cost = value;
            }
        }
    }
}
