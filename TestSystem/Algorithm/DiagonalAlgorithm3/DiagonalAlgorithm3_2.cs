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
    class DiagonalAlgorithm3_2 : Algorithm, IComparer<IPlace>
    {
        private List<IPlace> places;

        public DiagonalAlgorithm3_2()
        {
            this.name = "Диагональный алгоритм 3.2";
        }

        public DiagonalAlgorithm3_2(double step)
        {
            this.STEP = step;
            this.name = "Диагональный алгоритм 3.2";
        }


        private void Init()
        {
            places = new List<IPlace>();
        }


        public override IOutBlackBoxParam Calculate()
        {
            Init();
            IOutBlackBoxParam best = null;
            IPoint bestPoint;
            int maxPoints = this.BenchmarkLight();
            int tempMaxPoints = maxPoints;

            if (isLine(parametr.x1_min, parametr.x1_max, parametr.x2_min, parametr.x2_max))
            {
                return Benchmark();
            }

            IPlace bigPlace = Factory.GetPlace(new Point(parametr.x1_min, parametr.x2_min), new Point(parametr.x1_max, parametr.x2_max));

            FirstPlace firstPlace = new FirstPlace(bigPlace, this.parametr);

            ISubAlgorithm subAlg = Factory.GetSubAlgorithm(this, this.parametr, 1);

            ISeparathor seperathor = Factory.GetSeparathor();
            subAlg.maxPoints = maxPoints / 100 * 10;
            tempMaxPoints -= maxPoints / 100 * 10;
            bestPoint = subAlg.Calculate(firstPlace.MainDiagonal);

            List<IPlace> pl = null;
            pl = seperathor.Separate(firstPlace, bestPoint);
            int n = pl.Count;
            for (int i = 0; i < n; i++)
            {
                if ((pl[i] != null) && (!pl[i].isSeparated))
                {
                    List<IPlace> tempPl = null;
                    IPoint tempPoint = null;
                    subAlg.maxPoints = maxPoints / 100 * 5;
                    tempMaxPoints -= maxPoints / 100 * 5;
                    firstPlace.SetPlace(pl[i]);

                    if (pl[i].generation % 2 == 0)
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

                    pl.Sort(this);
                    i = 0;
                }


            }

            Clear();
            return new OutBlackBoxParam(bestPoint.cost);
        }


        private List<IPlace> Down(List<IPlace> places, int maxPoints, int levels=1)
        {
            ISeparathor separathor=Factory.GetSeparathor();
            ISubAlgorithm alg = Factory.GetSubAlgorithm(this, this.parametr, 1);
            List<IPlace> tempPlaces = places;
            int tempMaxPoints = maxPoints;

            for (int i = 0; i < levels;i++ )
            {
                double middle = MiddleArea(places);
                
                tempPlaces.Sort(this);
                alg.maxPoints = tempMaxPoints;
                IPoint best = alg.Calculate(tempPlaces[0].MainDiagonal);
                tempPlaces = separathor.Separate(tempPlaces[0], best);
                separathor = null;
                alg = null;
            }
            return tempPlaces;

        }


        private double MiddleArea(List<IPlace> places)
        {
            double middle = 0;
            foreach(IPlace place in places)
            {
                middle += place.Area;
            }

            middle /= places.Count;
            return middle;
        }




        private void Clear()
        {
            if (places != null)
                places.Clear();
            places = null;
        }





        private void CalculatePoints(List<IPlace> places)
        {
            foreach (IPlace place in places)
            {
                if (!place.isSeparated)
                {
                    IPoint tempPoint = place.GetPoint();
                    Function(tempPoint.x1, tempPoint.x2);
                }
            }
        }








        public int Compare(IPlace x, IPlace y)
        {
            if (x.GetPoint().cost < y.GetPoint().cost)
                return 1;

            if (x.GetPoint().cost > y.GetPoint().cost)
                return -1;


            return 0;
        }







    }
}
