using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;

namespace TestSystem.Algorithm.Diagonal_Algoritm3
{
    class Point:IPoint
    {
        private Double _x;
        private Double _y;
        private IOutBlackBoxParam _cost=null;
        private Double _cost_d=0;
        private int _id;
        public Point()
        {

        }

        public Point(Double x1, Double x2)
        {
            this.x1 = x1;
            this.x2 = x2;
            this._id = 0;
        }


        public Point(Double x1, Double x2, int id)
        {
            this.x1 = x1;
            this.x2 = x2;
            this._id = id;
        }

        /// <summary>
        /// Set Of Points
        /// </summary>
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
            }
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

        public Double cost
        {
            get
            {
                return _cost_d;
            }
            set
            {
                
                _cost_d = value;
            }
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
       
    }
}
