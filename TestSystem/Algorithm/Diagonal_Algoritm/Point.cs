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


        public Point(Double x, Double y)
        {
            this.x = x;
            this.y = y;
        }


        public double x
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

        public double y
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
                this._cost = cost;
            }
        }
    }
}
