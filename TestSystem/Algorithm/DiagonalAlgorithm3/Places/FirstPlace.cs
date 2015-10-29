using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm.Diagonal_Algoritm3.Lines;
using TestSystem.DataFormat;

namespace TestSystem.Algorithm.DiagonalAlgorithm3.Places
{
    class FirstPlace:IPlace
    {
        protected IPlace place;
        protected ILine _mainDiagonal;
        protected ILine _secondDiagonal;
        protected IEnterBlackBoxParam _parametr;
        public FirstPlace(IPlace place, IEnterBlackBoxParam param)
        {
            this.place = place;
            this._parametr = param;
            NormalizeLine(place.MainDiagonal);
            NormalizeLine(place.SecondDiagonal);
        }

        public FirstPlace(IEnterBlackBoxParam param)
        {
            this._parametr = param;
        }


        public void SetPlace(IPlace place)
        {
            this.place = place;
            _mainDiagonal = null;
            _secondDiagonal = null;
        }

        public int generation
        {
            get
            {
                return place.generation;
            }
            set
            {
                place.generation = value;
            }
        }

        public int hasPoints
        {
            get
            {
                return place.hasPoints;
            }
            set
            {
                place.hasPoints=value;
            }
        }

        public Diagonal_Algoritm3.Lines.ILine MainDiagonal
        {
            get {

            if (_mainDiagonal == null)
            {
                NormalizeLine(place.MainDiagonal);
                _mainDiagonal = place.MainDiagonal;

            }

            return _mainDiagonal;
            }
        }

        public Diagonal_Algoritm3.Lines.ILine SecondDiagonal
        {
            get {

                if (_secondDiagonal == null)
                {
                    NormalizeLine(place.SecondDiagonal);
                        _secondDiagonal = place.SecondDiagonal;

                }

                return _secondDiagonal;
            
            }
        }


        private void NormalizeLine(ILine line)
        {
            NormalizePoint(line.PointEnd);
            NormalizePoint(line.PointStart);
        }


        private void NormalizePoint(IPoint point)
        {
            if (point != null)
            {
                if (point.x2 / point.x1 > (_parametr.x2_x1_max))
                {
                   // _parametr.
                    //double tempX2 = point.x2;
                    point.x2 = (_parametr.x2_x1_max) * point.x1;
                    //point.x1 = Math.Pow(_parametr.x2_x1_max, -1) * tempX2;
                }

                if (point.x2 / point.x1 < _parametr.x2_x1_min)
                {
                    //double tempX2 = point.x2;
                    point.x2 = (_parametr.x2_x1_min) * point.x1;
                    //point.x1 = Math.Pow(_parametr.x2_x1_min, -1) * tempX2;
                }

                /*if (point.x1 <= _parametr.x1_min)
                {
                    point.x1 = _parametr.x1_min + 0.1;
                }

                if (point.x2 <= _parametr.x2_min)
                {
                    point.x2 = _parametr.x2_min + 0.1;
                }

                if (point.x2 >= _parametr.x2_max)
                {
                    point.x2 = _parametr.x2_max - 0.1;
                }*/


            }

        }

        public List<IPoint> points
        {
            get { return place.points; }
        }

        public bool isSeparated
        {
            get
            {
                return place.isSeparated;
            }
            set
            {
                place.isSeparated = value;
            }
        }

        public IPoint GetPoint()
        {
            return place.GetPoint();
        }

        public int needSeparates
        {
            get
            {
                return place.needSeparates;
            }
            set
            {
                place.needSeparates = value;
            }
        }

        public double Area
        {
            get { return place.Area; }
        }

        public int CompareTo(object obj) //sort
        {
            if (!(obj is IPlace)) return -1;
            var o = (IPlace)obj;
            var thisCost = points.Min(t => t.cost);
            var objCost = o.points.Min(t => t.cost);
            var th = this.Area / thisCost;
            var ob = o.Area / objCost;
            if (th < ob)
                return -1;
            else if (th > ob)
                return 1;
            else
                return 0;
        }
    }
}
