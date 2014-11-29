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
        private List<PointCoord> points;
        private Random rnd;
        private const int MAXPOINT = 4;
        private int bestValInd;
        private int worstValInd;
        private PointCoord cg;

        public Complex_Algorithm()
        {
            this.name = "Комплексный алгоритм";
            this.atributs += ""; //Параметры алгоритма указывай
            points = new List<PointCoord>();
            rnd = new Random();
            cg = new PointCoord();
        }

        public override DataFormat.IOutBlackBoxParam Calculate()
        {
            double cost = double.MaxValue;
            points.Clear();

            for (int i = 0; i < MAXPOINT; i++)
            {
                points.Add(RandPoint());
            }

            for (int i = 0; i < MAXPOINT / 4; i++)
            {
                points.RemoveAt(FindMaxCostIndex());
            }

            bestValInd = FindMinCostIndex();
            worstValInd = FindMaxCostIndex();
            FindCG();

            for (int i = 0; i < MAXPOINT * 12; i++)
            {
                points[worstValInd] = ReflectThePoint(points[worstValInd]);
                worstValInd = FindMaxCostIndex();
                FindCG();
            }

            cost = points[FindMinCostIndex()].cost;

            return new DataFormat.OutBlackBoxParam(cost);
        }

        private PointCoord ReflectThePoint(PointCoord point)
        {
            PointCoord refPoint = new PointCoord();
            //do {
            if (cg.x1 > point.x1)
            {
                refPoint.x1 = rnd.NextDouble() * (cg.x1 - point.x1) + cg.x1;
            }
            else
            {
                refPoint.x1 = cg.x1 - rnd.NextDouble() * (point.x1 - cg.x1);
            }

            if (cg.x2 > point.x2)
            {
                refPoint.x2 = rnd.NextDouble() * (cg.x2 - point.x2) + cg.x2;
            }
            else
            {
                refPoint.x2 = cg.x2 - rnd.NextDouble() * (point.x2 - cg.x2);
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

            if (!IsFeasiblePoint(refPoint) || point.cost < refPoint.cost)
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
            foreach (var x in points)
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
            for (int i = 0; i < points.Count; i++) //может заменить форичем?
            {
                if (minCost > points[i].cost)
                {
                    minCost = points[i].cost;
                    ind = i;
                }
            }
            return ind;
        }

        private int FindMaxCostIndex()
        {
            int ind = 0;
            double maxCost = 0;
            for (int i = 0; i < points.Count; i++) //тут тоже
            {
                if (maxCost < points[i].cost)
                {
                    maxCost = points[i].cost;
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
            
            do {
                x12pc.x1 = rnd.NextDouble() * (parametr.x1_max - parametr.x1_min) + parametr.x1_min;
                x2a_min = parametr.x2_x1_min * x12pc.x1;
                x2a_max = parametr.x2_x1_max * x12pc.x1;
            } while(x2a_max < parametr.x2_min || x2a_min > parametr.x2_max);

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
