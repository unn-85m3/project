using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Logger
{
    class Logger : ILogger, ICalculateListener
    {
        private class Point : IPoint
        {
            private Double _x1;
            private Double _x2;
            private Double _cost;

            public int CompareTo(object obj)
            {
                if (obj is IPoint)
                {
                    var o = (IPoint)obj;
                    if (o.cost > this.cost)
                    {
                        return -1;
                    }
                    else if (o.cost < this.cost)
                    {
                        return 1;
                    }
                    else return 0;
                }
                else throw new Exception("not IPoint");
            }
            public Double x1
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
            public Double x2
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
            public Double cost
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

            public Point(double x1, double x2, double cost)
            {
                _x1 = x1;
                _x2 = x2;
                _cost = cost;
            }
        }

        private Dictionary<String, List<IPoint>> points;
       
        public Logger()
        {
            points = new Dictionary<String, List<IPoint>>();
        }




        

        public void onCalculate(double x1, double x2, double cost, string name)
        {
            Point point = new Point(x1, x2, cost);
            List<IPoint> newPoints;
            if (!points.ContainsKey(name))
            {
                newPoints = new List<IPoint>();
                newPoints.Add(point);
                this.points.Add(name, newPoints);
            }
            else
            {
                points.TryGetValue(name, out newPoints);
                newPoints.Add(point);
            }
        }

        public bool HaveTask(string key)
        {
            return points.ContainsKey(key);
        }

        public List<IPoint> GetLog(string key)
        {
            List<IPoint> points;
            this.points.TryGetValue(key, out points);
            return points;
        }
    }
}
