using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Algorithm
{

    class Complex_Algorithm : AbsAlgorithm
    {
        private List<PointCoord> points;
        private Random rnd;
        private const int MAXPOINT = 4;
        
        private struct PointCoord
        {
            public Double x1;
            public Double x2;
            public Double cost;
        };

        public Complex_Algorithm()
        {
            points = new List<PointCoord>();
            rnd = new Random();
        }

        public override DataFormat.IOutBlackBoxParam Calculate()
        {
            double cost = 0;

            for (int i = 0; i < MAXPOINT; i++)
            {
                points.Add(RandPoint());
            }

            points.RemoveAt(FindMaxIndex());

            return new DataFormat.OutBlackBoxParam(cost);
        }

        private int FindMaxIndex()
        {
            int ind = 0;
            double maxCost = 0;
            for (int i = 0; i< points.Count; i++)
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

            x12pc.cost = Function(x12pc.x1, x12pc.x2).Cost;

            return x12pc;
        }

    }
}
