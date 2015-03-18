using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;

namespace TestSystem.Algorithm.Diagonal_Algoritm
{
    class DiagonalAlgorithmV2: AbsAlgorithm
    {
        
        public DiagonalAlgorithmV2()
        {
            this.name = "Диагональный алгоритм";
            this.atributs += ""; //ЗАПОЛНИ!!!!!!
        }

        public override DataFormat.IOutBlackBoxParam Calculate()
        {
            IPoint p1=new Point(parametr.x1_min,parametr.x2_min);
            IPoint p2=new Point(parametr.x1_max,parametr.x2_max);
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
                if ((place.allPlaces.Count-1) < j)
                {
                    j = place.allPlaces.Count - 1;
                    if (place.allPlaces.Count == 0)
                        break;
                    
                }

                if (place.allPlaces.Count > 50)
                    break;
                IPlace pl= place.allPlaces[j];
                if (!pl.IsSeparated)
                {


                    pl.Separate(this);

                   // Line line = new Line(pl.point1, pl.point2);
                   /* if (line.length < 0.1)
                    {
                        pl.parent.removePlace(pl);
                    }*/
                    if(pl.bestPoint.cost==Double.MaxValue)
                    {
                        pl.parent.removePlace(pl);
                    }

                    if ((best > pl.bestPoint.cost) && (pl.bestPoint.cost > 0))
                    {
                        best = pl.bestPoint.cost;
                        bestCounter = 0;
                        bestCounterLong=0;
                    }
                    else
                    {
                        if ( Math.Abs( pl.bestPoint.cost-best)<0.1)
                        {
                            bestCounter++;
                            if (bestCounter>3)
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
            }
            return new OutBlackBoxParam(best);
        }


       




    }
}
