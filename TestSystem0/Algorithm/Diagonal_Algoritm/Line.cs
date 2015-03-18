using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Algorithm.Diagonal_Algoritm
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
                return _length(PointStart.x, PointStart.y, PointEnd.x, PointEnd.y);
            }
                //return Math.Sqrt(Math.Pow(PointStart.x - PointEnd.x, 2)*Math.Pow(PointStart.y-PointEnd.y,2)  ); }

        }


        private Double _length(Double x0,Double y0,Double x1,Double y1)
        {
            return Math.Sqrt(Math.Pow(x0 - x1, 2) * Math.Pow(y0 - y1, 2));
        }

        public IPoint GetPoint(double leng)
        {
            Double len = leng * leng;
            Double L = length;
            if (len <= L)
            {
                Double k = len / (L - (L-len));
                Point p = new Point();
                p.x = (k * PointEnd.x + PointStart.x) / (1 + k);
                p.y = (k * PointEnd.y + PointStart.y) / (1 + k);
                return p;
            }
            else return null;
        }

        private Double k
        {
            get
            {
                return ay/ax;
            }
        }

        private Double b
        {
            get
            {
                return (PointStart.y - k * PointStart.x) / ax;
            }
        }

        private Double ax
        {
            get
            {
                return PointEnd.x - PointStart.x;
            }
        }

        private Double ay
        {
            get
            {
                return PointEnd.y - PointStart.y;
            }
        }
    }
}
