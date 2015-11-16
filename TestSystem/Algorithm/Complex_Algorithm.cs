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

        private int ca_worst_percent; // процент для отражения
        private int ca_cg_recent; // процент точек для построения ЦМ
        private int ca_reflections; // количество отражений плохой точки
        private double ca_max_range; // максимальная дальность точки отражения

        public Complex_Algorithm()
        {
            h = 10;
            this.name = "Комплексный алгоритм";
            this.atributs += "на " + h + " единиц площади приходится одна точка в рассматриваемой области,\n" +
                "дальность отражения задаётся случайно.";
            points_Coord = new List<PointCoord>();
            rnd = new Random(0);
            cg = new PointCoord();
        }

        public Complex_Algorithm(double step)
            : base(step)
        {
            h = 10;
            this.name = "Комплексный алгоритм";
            this.atributs += "на " + h + " единиц площади приходится одна точка в рассматриваемой области,\n" +
                "дальность отражения задаётся случайно."; //Параметры алгоритма указывай
            points_Coord = new List<PointCoord>();
            rnd = new Random(0);
            cg = new PointCoord();
        }

        public Complex_Algorithm(List<ParametrNow> pNow)
            : base(pNow.Find(s => s.name == "step").value)
        {
            h = 10;
            this.name = "Комплексный алгоритм";
            this.atributs += "на " + h + " единиц площади приходится одна точка в рассматриваемой области,\n" +
                "дальность отражения задаётся случайно.";
            points_Coord = new List<PointCoord>();
            rnd = new Random(0);
            cg = new PointCoord();
            ParamNow = pNow;
            ca_worst_percent = (int)pNow.Find(s => s.name == "the worst percent of points").value;
            ca_cg_recent = (int)pNow.Find(s => s.name == "the percent of points for create CG").value;
            ca_reflections = (int)pNow.Find(s => s.name == "the number of reflections of bad point").value;
            ca_max_range = (double)pNow.Find(s => s.name == "the maximum range of point reflection").value;
        }

        public override List<Parametr> GetAllParam
        {
            get
            {
                return new List<Parametr> {
                    new Parametr{name = "the worst percent of points", tp = TypeParams.discrete, minValue = 0, maxValue = 10},
                    new Parametr{name = "the percent of points for create CG", tp = TypeParams.discrete, minValue = 1, maxValue = 5},
                    new Parametr{name = "the number of reflections of bad point", tp = TypeParams.discrete, minValue = 0, maxValue = 10},
                    new Parametr{name = "the maximum range of point reflection", tp = TypeParams.discrete, minValue = 1, maxValue = 5},
                    new Parametr{name = "step", tp = TypeParams.continuous, minValue = 1, maxValue = 1}
                };
            }
        }

        public override DataFormat.IOutBlackBoxParam Calculate() // алгоритм можно ускорить
        {
            double cost = double.MaxValue;
            points_Coord.Clear();

            SetNumberOfPoints();

            h = this.SetAreaOfTheRegion(STEP);
            
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

            //for (int i = 0; i < h * 5; i++)
            //{
            //    points_Coord[worstValInd] = ReflectThePoint(points_Coord[worstValInd]);
            //    worstValInd = FindMaxCostIndex();
            //    FindCG();
            //}

            if (ca_worst_percent == 0)
            {
                worstValInd = FindMaxCostIndex();
                points_Coord[worstValInd] = ReflectThePoint(points_Coord[worstValInd]);
            }
            else
            {
                int worst_count = points_Coord.Count - (int)Math.Ceiling(points_Coord.Count * (double)ca_worst_percent / 10.0);
                //if (points_Coord[0].cost < points_Coord[points_Coord.Count].cost)
                for (int i = points_Coord.Count - 1; i > worst_count; i--)
                {
                    points_Coord[i] = ReflectThePoint(points_Coord[i]);
                }
            }
             

            cost = points_Coord[FindMinCostIndex()].cost;

            return new DataFormat.OutBlackBoxParam(cost);
        }

        private void SetNumberOfPoints()
        {
            if (parametr.x1_max - parametr.x1_min == 0 ||
                parametr.x2_max - parametr.x2_min == 0 ||
                parametr.x2_x1_max - parametr.x2_x1_min == 0) // какое условие выполняется чаще? '==' или '!=' ???
            {
                // != выполняется чаще. @АГА.
            }
        }

        private PointCoord ReflectThePoint(PointCoord point)
        {
            PointCoord refPoint = new PointCoord();
            int i;
            for (i = 0; (!IsItFeasiblePoint(refPoint) || point.cost <= refPoint.cost) && i < ca_reflections; i++)
            {
                if (cg.x1 > point.x1)
                {
                    refPoint.x1 = rnd.NextDouble() * (cg.x1 - point.x1) * ca_max_range / 5 + cg.x1;
                }
                else
                {
                    refPoint.x1 = cg.x1 - rnd.NextDouble() * (point.x1 - cg.x1) * ca_max_range / 5;
                }

                if (cg.x2 > point.x2)
                {
                    refPoint.x2 = rnd.NextDouble() * (cg.x2 - point.x2) * ca_max_range / 5 + cg.x2;
                }
                else
                {
                    refPoint.x2 = cg.x2 - rnd.NextDouble() * (point.x2 - cg.x2) * ca_max_range / 5;
                }

                if (!IsItFeasiblePoint(refPoint))
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
            }

            // if (i == ca_reflections) здесь будет то что надо делать с "трудными" точками

            if (!IsItFeasiblePoint(refPoint) || point.cost < refPoint.cost)
                return point;
            return refPoint;
        }

        private bool IsItFeasiblePoint(PointCoord point)
        {
            if (point.x1 < parametr.x1_min || point.x1 > parametr.x1_max ||
                point.x2 < parametr.x2_min || point.x2 > parametr.x2_max ||
                point.x2 / point.x1 < parametr.x2_x1_min ||
                point.x2 / point.x1 > parametr.x2_x1_max)
                return false;
            return true;
        }

        private class PointComparer : IComparer<PointCoord>
        {
            public int Compare(PointCoord x, PointCoord y)
            {
                if (x.cost == y.cost)
                    return 0;
                if (x.cost < y.cost) // как сортировать
                    return 1;
                else return -1;
            }
        }

        private void FindCG()
        {
            points_Coord.Sort(new PointComparer());
            List<PointCoord> pc = new List<PointCoord>();
            int p_count = (int)Math.Ceiling(points_Coord.Count * (double)ca_cg_recent / 10.0) + 1;
            for (int i = 0; i < p_count; i++)
            {
                pc.Add(points_Coord[i]);
            }

            int cnt = 0;
            cg.x1 = 0;
            cg.x2 = 0;
            foreach (var x in pc)
            {
                cg.x1 += x.x1;
                cg.x2 += x.x2;
                cnt++;
            }
            cg.x1 /= cnt;
            cg.x2 /= cnt;
            
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
