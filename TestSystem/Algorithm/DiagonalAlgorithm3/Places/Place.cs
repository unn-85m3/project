using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm.Diagonal_Algoritm3;
using TestSystem.Algorithm.Diagonal_Algoritm3.Lines;

namespace TestSystem.Algorithm.DiagonalAlgorithm3.Places
{
    class Place:IPlace
    {
        private bool _isSeparate = false;
        private int _generation = 0;
        private int _hasPoints = 0;
        private List<IPoint> _points;
        private ILine _mainDiagonal;
        private ILine _secondDiagonal;
        private Random _rnd;
        private IPoint _rndPoint;
        private int _needSeperate = 1;


        public Place(IPoint upLeft, IPoint downRight)
        {
            IPoint p3 = new Point(upLeft.x1, downRight.x2);
            IPoint p4 = new Point(downRight.x1, upLeft.x2);
            _points = new List<IPoint>();
            _points.Add(upLeft);
            _points.Add(downRight);
            _points.Add(p3);
            _points.Add(p4);
            _mainDiagonal = new Line(upLeft, downRight);
            _secondDiagonal = new Line(p3, p4);
            _rnd = new Random(0);

        }

        private void NormalizePoint(IPoint point)
        {

        }

        public int generation
        {
            get
            {
                return _generation;
            }
            set
            {
                _generation = value;
            }
        }

        public int hasPoints
        {
            get
            {
                return _hasPoints;
            }
            set
            {
                _hasPoints = value;
            }
        }

        public ILine MainDiagonal
        {
            get { return _mainDiagonal; }
        }

        public ILine SecondDiagonal
        {
            get { return _secondDiagonal; }
        }

        public bool isSeparated
        {
            get
            {
                return this._isSeparate;
            }
            set
            {
                _isSeparate = value;
            }
        }


        public List<IPoint> points
        {
            get { return _points; }
        }


        public double Area
        {
            get {

                return Math.Abs(_points[0].x1 - points[3].x1) * Math.Abs(points[3].x2-points[1].x2);
            
            }
        }


        

        public virtual IPoint GetPoint()
        {
            if (_rndPoint == null)
            {

             

                    _rndPoint = MainDiagonal.GetPoint(_rnd.NextDouble() * (MainDiagonal.length));
         

                    IPoint temp = MainDiagonal.GetPoint(_rnd.NextDouble() * (SecondDiagonal.length));
                    generation = 1;

                    if (temp.cost < _rndPoint.cost)
                    {
                        _rndPoint = temp;
                        generation = 2;
                    }
            }


            return _rndPoint;
        }

        
        public int needSeparates
        {
            get
            {
                return _needSeperate;
            }
            set
            {
                _needSeperate = value;
            }
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
