using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;

namespace TestSystem.Algorithm.Diagonal_Algoritm
{
    class Point:IPoint
    {
        private Double _x;
        private Double _y;
        private IOutBlackBoxParam _cost=null;
        public Point()
        {

        }


        public Point(Double x1, Double x2)
        {
            this.x1 = x1;
            this.x2 = x2;
        }


        public double x1
        {
            get
            {
                return _x;
            }
            set
            {
                this._x = value;
            }
        }

        public double x2
        {
            get
            {
                return _y;
            }
            set
            {
                this._y = value;
            }
        }



        public IOutBlackBoxParam cost
        {
            get
            {
                return _cost;
            }
            set
            {
                this._cost = value;
            }
        }
    }
}
