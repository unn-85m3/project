using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm.Diagonal_Algoritm3.Lines;
using TestSystem.DataFormat;

namespace TestSystem.Algorithm.DiagonalAlgorithm3.SubAlgorithms
{
    class Dihotomy:ISubAlgorithm
    {
        ICalculateFunction function;
        int _maxPoints = 0;
        IEnterBlackBoxParam _parametr;
        Random rng;
        int generation;

        public Dihotomy(ICalculateFunction function, IEnterBlackBoxParam parameters, int generation)
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
            }
        }

        public IPoint Calculate(ILine line)
        {
            double left = 0;
            double right = line.length;
            double c = 0;
            double eps = line.length/20;
            double e = 0.1;
            IPoint best=null;

            int i = 0;
            while(i<eps/2)
            {

                c = Math.Abs(left - right) / 2;
                double rL=Random(left,left+ c);
                double rR = Random(right-c, right);

                IPoint tempL=line.GetPoint(rL);
                IPoint tempR = line.GetPoint(rR);
               

                if(tempL.cost==0)
                    _Calculate(tempL);
                if(tempR.cost==0)
                    _Calculate(tempR);

                

                if (best==null)
                    best = tempL;
                if (best.cost > tempL.cost)
                    best = tempL;
                
                    if (best.cost > tempR.cost)
                        best = tempR;

                if (tempL.cost <= tempR.cost)
                {
                   // right -= c;
                    right = rR;
                    
                    
                }
                else
                {
                   // left += c;
                    left = rL;
                    
                }

                c = Math.Abs(left - right) / 2;
                if (maxPoints <= 0)
                    break;
                if (Math.Abs(left - right) <= e)
                    break;
                i++;
            }

   
            return best;

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

                IOutBlackBoxParam pr = function.Function(point.x1, point.x2);
                if (pr != null)
                {
                    point.cost = pr.Cost;
                    maxPoints--;
                }
                else
                    return null;

            }
            return null;
        }


        private double Random(double left, double right)
        {
            return rng.NextDouble()*(right-left)+left;
        }



        public int hasPoints
        {
            get { throw new NotImplementedException(); }
        }
    }
}
