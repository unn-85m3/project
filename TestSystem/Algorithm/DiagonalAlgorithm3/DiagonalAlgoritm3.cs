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
    class DiagonalAlgoritm3:AbsAlgorithm
    {

        private List<IPlace> places;
 

        public DiagonalAlgoritm3(double step)
        {
            this.STEP = step;
            this.name = "Диагональный алгоритм";
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
            
            
            ISubAlgorithm subAlg = Factory.GetSubAlgorithm(this, this.parametr, 1);
            
            ISeparathor seperathor = Factory.GetSeparathor();
            subAlg.maxPoints = maxPoints/100*10;
            tempMaxPoints -= maxPoints / 100 * 10;
            bestPoint = subAlg.Calculate(bigPlace.MainDiagonal);

            List<IPlace> pl = null;
            pl = seperathor.Separate(bigPlace, bestPoint);
            int n = pl.Count;
            for (int i = 0; i < n;i++ )
            {
                List<IPlace> tempPl = null;
                IPoint tempPoint = null;
                subAlg.maxPoints = maxPoints / 100 * 10;
                tempMaxPoints -= maxPoints / 100 * 10;
                

                if (i % 2 == 0)
                {
                    tempPoint = subAlg.Calculate(pl[i].SecondDiagonal);
                    tempMaxPoints += subAlg.maxPoints;
                }
                else
                {
                    tempPoint = subAlg.Calculate(pl[i].MainDiagonal);
                    tempMaxPoints += subAlg.maxPoints;
                }

                pl.AddRange(seperathor.Separate(pl[i],tempPoint));
                n = pl.Count;
                if (tempMaxPoints <= 0)
                    break;



            }
           







            Clear();
            return new OutBlackBoxParam(bestPoint.cost);
        }


        private void Clear()
        {
            if(places!=null)
                places.Clear();
            places = null;
        }


        private int BenchmarkLight()
        {
            double h = STEP;
            this.atributs += "Шаг алгоритма = " + h + " Па.";
            int n = 0; //количество успешных вычислений функции
            double cost = double.MaxValue;
            int count = 0;


            for (double i = this.parametr.x1_min; i <= this.parametr.x1_max; i += h)
                for (double j = this.parametr.x2_min; j <= this.parametr.x2_max; j += h)
                    if (((j / i) <= this.parametr.x2_x1_max) && ((j / i) >= this.parametr.x2_x1_min))
                    {

                        count++;
                    }

            return count;
        }


        private IOutBlackBoxParam Benchmark()
        {
            double h = STEP;
            // this.atributs += "Шаг алгоритма = " + h + " Па.";
            int n = 0; //количество успешных вычислений функции
            double cost = double.MaxValue;
            IOutBlackBoxParam a;

            for (double i = this.parametr.x1_min; i <= this.parametr.x1_max; i += h)
                for (double j = this.parametr.x2_min; j <= this.parametr.x2_max; j += h)
                    if (((j / i) <= this.parametr.x2_x1_max) && ((j / i) >= this.parametr.x2_x1_min))
                    {
                        try
                        {
                            a = Function(i, j);
                            n++;

                        }
                        catch
                        {
                            a = new OutBlackBoxParam(Double.MaxValue);  // Double.MaxValue;
                        }


                        //a = Function(34.946777049892944, 50.93829138975363);

                        if (n == 1)
                            cost = a.Cost;
                        else if (a.Cost < cost)
                            cost = a.Cost;

                    }
            return new DataFormat.OutBlackBoxParam(cost);
        }


        private bool isLine(double x1_min, double x1_max, double x2_min, double x2_max)
        {
            if (x1_min - x1_max == 0)
            {
                return true;
            }

            if (x2_min - x2_max == 0)
            {
                return true;
            }

            return false;
        }




    }
}
