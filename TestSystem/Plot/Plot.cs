using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.BlackBox;
using TestSystem.DataFormat;
using TestSystem.Tasks;

namespace TestSystem.Plot
{
    class Plot:AbstractPlot
    {
        List<List<IPoint>> points;
        double h = 1;
        public Plot(IFunction function, IEnterBlackBoxParam task)
            : base(function, task)
        {
            points = new List<List<IPoint>>();
        }

        protected override List<List<IPoint>> Calculate(IFunction function, IEnterBlackBoxParam parametr)
        {
            IOutBlackBoxParam a;

            int I = 0;
            int J = 0;

            int maxI=0;
            int maxJ = 0; 
            int tempJ = 0;
            int tempI = 0;
            

            for (double i = parametr.x1_min; i <= parametr.x1_max; i += h)
            {
                for (double j = parametr.x2_min; j <= parametr.x2_max; j += h)
                {
                    if (((j / i) <= parametr.x2_x1_max) && ((j / i) >= parametr.x2_x1_min))
                    {
                        
                        //maxI = tempI;
                    }
                   tempJ++;
                   // maxJ++;
                    
                }
                if (tempJ > maxJ)
                {
                    maxJ = tempJ;
               }
                tempJ = 0;
               // tempI++;
                maxI++;
            }


            for (int i = 0; i <= maxI+1; i ++)
            {
                points.Add(new List<IPoint>());
                for (int j = 0; j <= maxJ+1; j ++)
                {
                    points[i].Add(new PlotPoint(0, 0, Double.MaxValue));
                }

            }


            for (double i = parametr.x1_min; i <= parametr.x1_max; i += h)
            {
                for (double j = parametr.x2_min; j <= parametr.x2_max; j += h)
                {
                    if (((j / i) <= parametr.x2_x1_max) && ((j / i) >= parametr.x2_x1_min))
                    {
                        try
                        {
                            a = function.Calculate(i, j);

                        }
                        catch
                        {
                            a = new OutBlackBoxParam(Double.MaxValue);  // Double.MaxValue;
                        }

                        points[I][J].x1 = i;
                        points[I][J].x2 = j;
                        points[I][J].cost = a.Cost;
                        J++;

                    }
                }
                J = 0;
                I++;
            }
            return points;
        }


        protected class PlotPoint : IPoint
        {
            private double _x1;
            private double _x2;
            private double _cost;

            public PlotPoint(double x1, double x2, double cost)
            {
                this._x1 = x1;
                this._x2 = x2;
                this._cost = cost;
            }

            public double x1
            {
                get
                {
                    return _x1;
                }
                set
                {
                    _x1 = value;
                }
            }

            public double x2
            {
                get
                {
                    return _x2;
                }
                set
                {
                    _x2 = value;
                }
            }

            public double cost
            {
                get
                {
                    return _cost;
                }
                set
                {
                    _cost = value;
                }
            }
        }
    }


    
}
