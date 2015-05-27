using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Algorithm.New.Diagonal_Algoritm
{
    class Line:ILine
    {
        private IPoint _poinStart;
        private IPoint _pointEnd;
        public Line(IPoint pointStart,IPoint pointEnd)
        {
            this._poinStart = pointStart;
            this._pointEnd = pointEnd;
        }
        public IPoint PointStart
        {
            get
            {
                return this._poinStart;
            }
            set
            {
                this._poinStart = value;
            }
        }

        public IPoint PointEnd
        {
            get
            {
                return this._pointEnd;
            }
            set
            {
                this._pointEnd = value;
            }
        }

        public double length
        {
            get
            {
                return _length(PointStart.x1, PointStart.x2, PointEnd.x1, PointEnd.x2);
            }
                //return Math.Sqrt(Math.Pow(PointStart.x - PointEnd.x1, 2)*Math.Pow(PointStart.x2-PointEnd.x2,2)  ); }

        }


        private Double _length(Double x10,Double x20,Double x11,Double x21)
        {
            if (x10 == x11)
            {
                return Math.Abs(x20 - x21);

            }

            if(x20==x21)
            {
                return Math.Abs(x10 - x11);
            }
            return Math.Sqrt(Math.Pow(x10 - x11, 2) * Math.Pow(x20 - x21, 2));
        }

        public IPoint GetPoint(double leng)
        {
            Double len = leng;
            Double L = length;
            if (len <= L)
            {
                Double k = len/L;
                Point p = new Point();
                p.x1 = (k * (PointEnd.x1 - PointStart.x1) + PointStart.x1); /// (1 + k);
                p.x2 = (k * (PointEnd.x2-PointStart.x2) + PointStart.x2); /// (1 + k);
                return p;
            }
            else return null;
        }

        private Double k
        {
            get
            {
                return ax2/ax1;
            }
        }

        private Double b
        {
            get
            {
                return (PointStart.x2 - k * PointStart.x1) / ax1;
            }
        }

        private Double ax1
        {
            get
            {
                return PointEnd.x1 - PointStart.x1;
            }
        }

        private Double ax2
        {
            get
            {
                return PointEnd.x2 - PointStart.x2;
            }
        }
    }
}
