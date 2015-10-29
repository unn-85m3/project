using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm.Diagonal_Algoritm3;
using TestSystem.Algorithm.DiagonalAlgorithm3.Places;
using TestSystem.Algorithm.DiagonalAlgorithm3.Separathors;
using TestSystem.Algorithm.DiagonalAlgorithm3.SubAlgorithms;
using TestSystem.DataFormat;

namespace TestSystem.Algorithm.DiagonalAlgorithm3
{
    class DiagonalAlgorithm3_1:Algorithm,IComparer<IPlace>
    {
         private List<IPlace> places;
        private int maxPoints=0;
        IPoint best = null;

        public DiagonalAlgorithm3_1(double step)
        {
            this.STEP = step;
            this.name = "Диагональный алгоритм 3.1";
        }

        private void Init()
        {
            places = new List<IPlace>();
            this.best = null;
        }

        public override IOutBlackBoxParam Calculate()
        {
            Init();
            IOutBlackBoxParam best = null;
            IPoint bestPoint;
            int maxPoints = this.BenchmarkLight();
            this.maxPoints = maxPoints;
            int tempMaxPoints = maxPoints;

            if (isLine(parametr.x1_min, parametr.x1_max, parametr.x2_min, parametr.x2_max))
            {
                return Benchmark();
            }

            IPlace bigPlace = Factory.GetPlace(new Point(parametr.x1_min, parametr.x2_min), new Point(parametr.x1_max, parametr.x2_max));

            FirstPlace firstPlace = new FirstPlace(bigPlace, this.parametr);
            
            ISubAlgorithm subAlg = Factory.GetSubAlgorithm(this, this.parametr, 1);
            
            ISeparathor seperathor = Factory.GetSeparathor();
            subAlg.maxPoints = maxPoints/100*10;
            tempMaxPoints -= maxPoints / 100 * 10;
            bestPoint = subAlg.Calculate(firstPlace.MainDiagonal);

            List<IPlace> pl = null;
            pl = seperathor.Separate(firstPlace, bestPoint);
            if (pl.Count == 0)
            {
                firstPlace.generation = 1;
                bestPoint = subAlg.Calculate(firstPlace.SecondDiagonal);
                pl = seperathor.Separate(firstPlace, bestPoint);

            }
            int n = pl.Count;
            for (int i = 0; i < n;i++ )
            {
                if (pl.Count == i)
                    break;
                if ((pl[i]!=null) && (!pl[i].isSeparated))
                {
                    List<IPlace> tempPl = null;
                    IPoint tempPoint = null;
                    subAlg.maxPoints = maxPoints / 100 * 5;
                    tempMaxPoints -= maxPoints / 100 * 5;
                    firstPlace.SetPlace(pl[i]);

                    if (pl[i].generation%2 == 0)
                    {
                        tempPoint = subAlg.Calculate(firstPlace.SecondDiagonal);
                        tempMaxPoints += subAlg.maxPoints;
                    }
                    else
                    {
                        tempPoint = subAlg.Calculate(firstPlace.MainDiagonal);
                        tempMaxPoints += subAlg.maxPoints;
                    }



                    pl.AddRange(seperathor.Separate(firstPlace, tempPoint));
                    n = pl.Count;
                    if (tempMaxPoints <= 0)
                        break;
                    pl.RemoveAt(i);
                    pl.Sort(this);
                    i = 0;
                }


            }
           

            Clear();
            return new OutBlackBoxParam(this.best.cost);
        }

        

        public override IOutBlackBoxParam Function(double x1, double x2)
        {
            maxPoints--;
            if (best == null)
            {
                best = new Point(x1, x2);
                best.cost = double.MaxValue;
            }

            if (maxPoints >= 0)
            {

                IOutBlackBoxParam par = base.Function(x1, x2);
                if (par.Cost < best.cost)
                {
                    best.x1 = x1;
                    best.x2 = x2;
                    best.cost = par.Cost;
                }

                return par;
            }
            else
                return null;
        }


        private void Clear()
        {
            if(places!=null)
                places.Clear();
            places = null;
            
        }


       


        public int Compare(IPlace x, IPlace y)
        {
            IPoint xPoint=x.GetPoint();
            IPoint yPoint= y.GetPoint();
            if (xPoint.cost==0)
            {
                IOutBlackBoxParam pr=Function(xPoint.x1, xPoint.x2);
                if (pr == null)
                    return 0;
                xPoint.cost = pr.Cost;
                IPoint p = BestPlacePoint(x);
                if (p != null)
                    if (xPoint.cost != Double.MaxValue)
                        xPoint.cost = (p.cost + xPoint.cost) / 2;
                    else
                        xPoint.cost = p.cost;

            }

            if (yPoint.cost == 0)
            {

                IOutBlackBoxParam pr = Function(xPoint.x1, xPoint.x2);
                if (pr == null)
                    return 0;

                yPoint.cost = pr.Cost;

                IPoint p = BestPlacePoint(x);
                if (p != null)
                    if (yPoint.cost != Double.MaxValue)
                        yPoint.cost = (p.cost + yPoint.cost) / 2;
                    else
                        yPoint.cost = p.cost;

            }

            if (xPoint.cost < yPoint.cost)
                return -1;

            if (xPoint.cost > yPoint.cost)
                return 1;

           
                return 0;
        }


        public IPoint BestPlacePoint(IPlace place)
        {
            IPoint tempPoint = null;
            foreach (IPoint point in place.points)
            {
                if (tempPoint == null)
                    tempPoint = point;
                else
                {
                    if ((tempPoint.cost < point.cost) && (point.cost != 0) && (point.cost != Double.MaxValue))
                        tempPoint = point;
                }
            }

            if ((tempPoint.cost == 0)||(tempPoint.cost==Double.MaxValue))
                tempPoint = null;
            return tempPoint;
        }


        public override List<Parametr> GetAllParam
        {
            get { throw new NotImplementedException(); }
        }
    }
    
}
