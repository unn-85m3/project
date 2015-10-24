using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;

namespace TestSystem.Algorithm.DiagonalAlgorithm3
{
    abstract class Algorithm : AbsAlgorithm
    {
        


        protected int BenchmarkLight()
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


        protected bool isLine(double x1_min, double x1_max, double x2_min, double x2_max)
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


        protected IOutBlackBoxParam Benchmark()
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
    }
}
