using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;

namespace TestSystem.Algorithm.New.Diagonal_Algoritm
{
    class DiagonalAlgorithmV2: AbsAlgorithm
    {
        
        public DiagonalAlgorithmV2()
        {
            this.name = "Диагональный алгоритм";
            this.atributs += ""; //ЗАПОЛНИ!!!!!!
        }

        public DiagonalAlgorithmV2(double step)
            : base(step)
        {
            this.name = "Диагональный алгоритм";
            this.atributs += ""; //ЗАПОЛНИ!!!!!!
        }


        private int Benchmark()
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


        private bool isLine(double x1_min, double x1_max,double x2_min,double x2_max)
        {
            if (x1_min - x1_max==0)
            {
                return true;
            }

            if (x2_min - x2_max == 0)
            {
                return true;
            }

            return false;
        }

        public override DataFormat.IOutBlackBoxParam Calculate()
        {
            if (isLine(parametr.x1_min, parametr.x1_max, parametr.x2_min, parametr.x2_max))
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
            else
            {

                int count = Benchmark();
                IPoint p1 = new Point(parametr.x1_min, parametr.x2_min);
                IPoint p2 = new Point(parametr.x1_max, parametr.x2_max);
                IPlace place = new Place(p1, p2, parametr);



                Double best;
                int bestCounter = 0;
                int bestCounterLong = 0;
                int n = this.SetAreaOfTheRegion(STEP);

                place.Separate(this);
                best = place.bestPoint.cost;

                int j = 0;
                for (int i = 0; i < n; i++)
                {


                    if ((place.allPlaces.Count - 1) < j)
                    {
                        j = place.allPlaces.Count - 1;
                        if (place.allPlaces.Count == 0)
                            break;

                    }

                    if (place.allPlaces.Count > 50)
                        break;
                    IPlace pl = place.allPlaces[j];
                    if (!pl.IsSeparated)
                    {


                        pl.Separate(this);

                        // Line line = new Line(pl.point1, pl.point2);
                        /* if (line.length < 0.1)
                         {
                             pl.parent.removePlace(pl);
                         }*/
                        if (pl.bestPoint.cost == Double.MaxValue)
                        {
                            pl.parent.removePlace(pl);
                        }

                        if ((best > pl.bestPoint.cost) && (pl.bestPoint.cost > 0))
                        {
                            best = pl.bestPoint.cost;
                            bestCounter = 0;
                            bestCounterLong = 0;
                        }
                        else
                        {
                            if (Math.Abs(pl.bestPoint.cost - best) < 0.1)
                            {
                                bestCounter++;
                                if (bestCounter > 3)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        i--;
                    }
                    j++;
                    bestCounterLong++;

                    if (bestCounterLong > 3)
                        break;

                    if (this.Calls > count)
                        break;
                }
                return new OutBlackBoxParam(best);
            }
        }


       




    }
}
