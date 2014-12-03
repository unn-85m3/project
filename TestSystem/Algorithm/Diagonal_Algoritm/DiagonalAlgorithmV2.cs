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
        }

        public override DataFormat.IOutBlackBoxParam Calculate()
        {
            IPoint p1=new Point(parametr.x1_min,parametr.x2_min);
            IPoint p2=new Point(parametr.x1_max,parametr.x2_max);
            IPlace place = new Place(p1, p2, parametr);
            Double best;
           
            place.Separate(this);
            best = place.bestPoint.cost.Cost;
            int j = 0;
            for (int i = 0; i < 20; i++)
            {
                IPlace pl= place.allPlaces[j];
                if (!pl.IsSeparated)
                {
                    pl.Separate(this);
                    if ((best > pl.bestPoint.cost.Cost) && (pl.bestPoint.cost.Cost > 0))
                        best = pl.bestPoint.cost.Cost;
                }
                else
                {
                    i--;
                }
                j++;
            }
            return new OutBlackBoxParam(best);
        }


       




    }
}
