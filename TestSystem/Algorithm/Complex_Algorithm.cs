using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Algorithm
{
    class Complex_Algorithm : AbsAlgorithm
    {
        private struct PointCoord
        {
            public Double x1;
            public Double x2;
            public Double cost;
        };
        private List<PointCoord> points_Coord;
        private Random rnd;
        private const int MAXPOINT = 4; //будет ненужно
        private int bestValInd;
        private int worstValInd;
        private PointCoord cg;
        private int h;

        public Complex_Algorithm()
        {
            this.name = "Комплексный алгоритм";
            this.atributs += "на " + 3 + " единиц площади приходится одна точка в рассматриваемой области,\n" +
                "дальность отражения задаётся случайно."; //Параметры алгоритма указывай
            points_Coord = new List<PointCoord>();
            //rnd = new Random(0);
            cg = new PointCoord();
        }

        public Complex_Algorithm(double step)
            : base(step)
        {
            this.name = "Комплексный алгоритм";
            this.atributs += "на " + 3 + " единиц площади приходится одна точка в рассматриваемой области,\n" +
                "дальность отражения задаётся случайно."; //Параметры алгоритма указывай
            points_Coord = new List<PointCoord>();
            //rnd = new Random(0);
            cg = new PointCoord();
        }

        public override DataFormat.IOutBlackBoxParam Calculate() // алгоритм можно ускорить
        {
            rnd = new Random(0);
            double cost = double.MaxValue;
            double temp = 0.0;
            points_Coord.Clear();
            double ind = 0.0;
            double hDouble = 1.0;// (double)h;

            switch (howManyD())
            {
                case 1: //x1 is one value, x2 is not one
                    ind = parametr.x2_min;
                    try
                    {
                        cost = Function(parametr.x1_min, ind).Cost;
                    }
                    catch
                    {
                        cost = double.MaxValue;
                    }
                    while (ind < parametr.x2_max)
                    {
                        try
                        {
                            temp = Function(parametr.x1_min, ind).Cost;
                        }
                        catch
                        {
                            temp = double.MaxValue;
                        }
                        if (temp < cost) cost = temp;
                        ind += hDouble;
                    }
                    return new DataFormat.OutBlackBoxParam(cost);
                case 2: //x2 is one value, x1 is not one
                    ind = parametr.x1_min;
                    try
                    {
                        cost = Function(ind, parametr.x2_min).Cost;
                    }
                    catch
                    {
                        cost = double.MaxValue;
                    }
                    while (ind < parametr.x1_max)
                    {
                        try
                        {
                            temp = Function(ind, parametr.x2_min).Cost;
                        }
                        catch
                        {
                            temp = double.MaxValue;
                        }
                        if (temp < cost) cost = temp;
                        ind += hDouble;
                    }
                    return new DataFormat.OutBlackBoxParam(cost);
                case 3: //it is one dot (x1;x2)
                    cost = Function(parametr.x1_min, parametr.x2_min).Cost;
                    return new DataFormat.OutBlackBoxParam(cost);
                case 4: //x2/x1 is one value
                    ind = parametr.x1_min;
                    try
                    {
                        cost = Function(ind, parametr.x2_x1_max * ind).Cost;
                    }
                    catch
                    {
                        cost = double.MaxValue;
                    }
                    while (ind < parametr.x1_max)
                    {
                        try
                        {
                            temp = Function(ind, parametr.x2_x1_max * ind).Cost;
                        }
                        catch
                        {
                            temp = double.MaxValue;
                        }
                        if (temp < cost) cost = temp;
                        ind += hDouble;
                    }
                    return new DataFormat.OutBlackBoxParam(cost);
                case 5: //it is one dot x1 cross x2/x1
                    cost = Function(parametr.x1_min, parametr.x1_min / parametr.x2_x1_min).Cost;
                    return new DataFormat.OutBlackBoxParam(cost);
                case 6: //or x2 cross x2/x1
                    cost = Function(parametr.x2_min / parametr.x2_x1_min, parametr.x2_min).Cost;
                    return new DataFormat.OutBlackBoxParam(cost);
                default:
                    break;
            }


            //h = this.SetAreaOfTheRegion(STEP);
            h = this.SetAreaOfTheRegion(3); //это быстрее чем шаг = 1
            //h = (int)((parametr.x1_max - parametr.x1_min + parametr.x2_max - parametr.x2_min) / 2 * ((int)(parametr.x2_x1_max - parametr.x2_x1_min) + 1)) + 4;

            if (h == 1)
            {
                cost = Function(parametr.x1_min, parametr.x2_min).Cost;
                return new DataFormat.OutBlackBoxParam(cost);
            }

            if (h < 6) // если кол-во точек <  точек которые мы хотим отражать, то просто найти лучшую без использования алгоритма.
            {
                double[] tmp = new double[h];
                for (int i = 0; i < h; i++)
                {
                    //tmp[i] = Function(x1, x2).Cost;
                }
            }

            h = Math.Max(h / 6, 10);

            for (int i = 0; i < h; i++)
            {
                points_Coord.Add(RandPoint());
            }

            for (int i = 0; i < h / 4; i++)
            {
                points_Coord.RemoveAt(FindMaxCostIndex());
            }

            bestValInd = FindMinCostIndex();
            worstValInd = FindMaxCostIndex();
            FindCG();

            for (int i = 0; i < h * 5; i++)
            {
                points_Coord[worstValInd] = ReflectThePoint(points_Coord[worstValInd]);
                worstValInd = FindMaxCostIndex();
                FindCG();
            }

            cost = points_Coord[FindMinCostIndex()].cost;

            return new DataFormat.OutBlackBoxParam(cost);
        }

        private int howManyD()
        {
            int t = 0;
            if (parametr.x1_max - parametr.x1_min == 0) t += 1;
            if (parametr.x2_max - parametr.x2_min == 0) t += 2;
            if (parametr.x2_x1_max - parametr.x2_x1_min == 0) t += 4;

            return t;
        }

        private PointCoord ReflectThePoint(PointCoord point)
        {
            PointCoord refPoint = new PointCoord();
            //do {
            if (cg.x1 > point.x1)
            {
                //refPoint.x1 = rnd.NextDouble() * (cg.x1 - point.x1) + cg.x1;
                refPoint.x1 = rnd.NextDouble() * (parametr.x1_max - cg.x1) + cg.x1 - 0.1;
            }
            else
            {
                //refPoint.x1 = cg.x1 - rnd.NextDouble() * (point.x1 - cg.x1);
                refPoint.x1 = cg.x1 - rnd.NextDouble() * (cg.x1 - parametr.x1_min) - 0.1;
            }

            if (cg.x2 > point.x2)
            {
                //refPoint.x2 = rnd.NextDouble() * (cg.x2 - point.x2) + cg.x2;
                refPoint.x2 = rnd.NextDouble() * (parametr.x2_max - cg.x2) + cg.x2 - 0.1;
            }
            else
            {
                //refPoint.x2 = cg.x2 - rnd.NextDouble() * (point.x2 - cg.x2);
                refPoint.x2 = cg.x2 - rnd.NextDouble() * (cg.x2 - parametr.x2_min) - 0.1;
            }

            if (!IsFeasiblePoint(refPoint))
            {
                refPoint.x1 -= Math.Abs(refPoint.x1 - point.x1);
                refPoint.x2 -= Math.Abs(refPoint.x2 - point.x2);
            }

            try
            {
                refPoint.cost = Function(refPoint.x1, refPoint.x2).Cost;
            }
            catch
            {
                refPoint.cost = double.MaxValue;
            }
            //} while (!IsFeasiblePoint(refPoint) || point.cost < refPoint.cost);

            if (!IsFeasiblePoint(refPoint) || point.cost < refPoint.cost) // мб ноги проблемы отсюда растут?
                return point;
            return refPoint;
        }

        private bool IsFeasiblePoint(PointCoord point)
        {
            if (point.x1 < parametr.x1_min || point.x1 > parametr.x1_max ||
                point.x2 < parametr.x2_min || point.x2 > parametr.x2_max ||
                point.x2 / point.x1 < parametr.x2_x1_min ||
                point.x2 / point.x1 > parametr.x2_x1_max)
                return false;
            return true;
        }

        private void FindCG()
        {
            int i = 0;
            cg.x1 = 0;
            cg.x2 = 0;
            foreach (var x in points_Coord)
            {
                cg.x1 += x.x1;
                cg.x2 += x.x2;
                i++;
            }
            cg.x1 /= i;
            cg.x2 /= i;
            //cg.cost = Function(cg.x1, cg.x2).Cost;
        }

        private int FindMinCostIndex()
        {
            int ind = 0;
            double minCost = double.MaxValue;
            for (int i = 0; i < points_Coord.Count; i++) //может заменить форичем?
            {
                if (minCost > points_Coord[i].cost) //Не  получиться, индекс не узнаешь. @АГА.
                {
                    minCost = points_Coord[i].cost;
                    ind = i;
                }
            }
            return ind;
        }

        private int FindMaxCostIndex()
        {
            int ind = 0;
            double maxCost = 0;
            for (int i = 0; i < points_Coord.Count; i++) //тут тоже
            {
                if (maxCost < points_Coord[i].cost)// тоже нет. @АГА.
                {
                    maxCost = points_Coord[i].cost;
                    ind = i;
                }
            }
            return ind;
        }

        private PointCoord RandPoint()
        {
            Double x2a_min;
            Double x2a_max;
            PointCoord x12pc = new PointCoord();

            do
            {
                x12pc.x1 = rnd.NextDouble() * (parametr.x1_max - parametr.x1_min) + parametr.x1_min;
                x2a_min = parametr.x2_x1_min * x12pc.x1;
                x2a_max = parametr.x2_x1_max * x12pc.x1;
            } while (x2a_max < parametr.x2_min || x2a_min > parametr.x2_max);

            if (x2a_min < parametr.x2_min) x2a_min = parametr.x2_min;
            if (x2a_max > parametr.x2_max) x2a_max = parametr.x2_max;

            x12pc.x2 = rnd.NextDouble() * (x2a_max - x2a_min) + x2a_min;

            try
            {
                x12pc.cost = Function(x12pc.x1, x12pc.x2).Cost;
            }
            catch
            {
                x12pc = RandPoint();
            }

            return x12pc;
        }

    }
}
