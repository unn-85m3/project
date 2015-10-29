using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm.Diagonal_Algoritm3.Lines;
using TestSystem.DataFormat;

namespace TestSystem.Algorithm.DiagonalAlgorithm3.SubAlgorithms
{
    class HillClimbing:ISubAlgorithm
    {
        ICalculateFunction function;
        int _maxPoints = 0;
        IEnterBlackBoxParam _parametr;
        Random rng;
        int generation;
        private int _hasPoints;

        public HillClimbing(ICalculateFunction function, IEnterBlackBoxParam parameters, int generation)
        {
            rng = new Random(0); 
            this.function = function;
            this._parametr = parameters;
            this.generation = generation;
        }

        public int maxPoints
        {
            get
            {
                return _maxPoints;
            }
            set
            {
                _maxPoints = value;
                _hasPoints = value;
            }
        }

        public IPoint Calculate(ILine line)//edit
        {
          
            double step;
           
                step = line.length / maxPoints;

               /* if (step < 0.2)
                    step = 0.2;*/


            Double lPoint = this.rng.NextDouble() * (line.length);
            IPoint point = line.GetPoint(lPoint);


            _Calculate(point);
            if (line.length < 0.1)
            {
                return point;
            }
            Double llPoint = lPoint - step;
            IPoint left = line.GetPoint(llPoint);
            Double rlPoint = lPoint + step;
            IPoint right = line.GetPoint(rlPoint);

            IPoint main = point;
            Boolean isLef = true;

            if (right == null) right = main;
            if (left == null) left = main;
            _Calculate(left);
            _Calculate(right);

            if (left.cost < right.cost)
            {
                main = left;
            }
            else
            {
                main = right;
                isLef = false;
            }

            if (main.cost > point.cost)
            {
                return point;
            }


            double tempStep = step;

            while (true)
            {
                
                if (isLef)
                {
                    llPoint -= tempStep;
                    tempStep += tempStep * 0.1;
                    left = line.GetPoint(llPoint);
                    if (left == null) return main;
                    IPoint flagP = _Calculate(left);
                    if (flagP == null)
                        break;
                    if (left.cost == Double.MaxValue)
                        break;
                }
                else
                {
                    rlPoint += tempStep;
                    tempStep += tempStep * 0.1;
                    right = line.GetPoint(rlPoint);
                    if (right == null) return main;
                    IPoint flagP=_Calculate(right);
                    if (flagP == null)
                        break;
                    if (right.cost == Double.MaxValue)
                        break;
                    
  
                }

                if (_maxPoints <= 0)
                    return main;
            }

            return main;
        }


        private IPoint _Calculate(IPoint point)
        {
            if (point != null)
            {
                if (point.x1 > _parametr.x1_max)
                {
                    point.x1 = _parametr.x1_max;
                }

                if (point.x1 < _parametr.x1_min)
                {
                    point.x1 = _parametr.x1_min;
                }

                if (point.x2 < _parametr.x2_min)
                {
                    point.x2 = _parametr.x2_min;
                }

                if (point.x2 > _parametr.x2_max)
                {
                    point.x2 = _parametr.x2_max;
                }

                IOutBlackBoxParam par=function.Function(point.x1, point.x2);
                if (par != null)
                {
                    point.cost = par.Cost;
                    maxPoints--;
                }
                else
                    return null;

            }
            return null;
        }




        public int hasPoints
        {
            get { return _hasPoints; }
        }

        
    }
}
