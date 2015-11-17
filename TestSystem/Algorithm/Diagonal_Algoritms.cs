using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Algorithm
{
    class Diagonal_Algoritms : AbsAlgorithm
    {
        #region New Realization

        public Diagonal_Algoritms()
        {
            this.name = "Диагональный алгоритм";
            this.atributs += "";//Параметры алгоритма
        }

        public Diagonal_Algoritms(double step)
            : this()
        {
            this.STEP = step;
        }

        public Diagonal_Algoritms(List<ParametrNow> paramNow)
            : this()
        {
            ParamNow = paramNow;
        }

        public Diagonal_Algoritms(List<ParametrNow> paramNow, double step)
            : this(paramNow)
        {
            this.STEP = step;
        }

        public List<Point> GetBorderPoint(Point p1, Point p2)
        {
            var outList = new List<Point>();
            var placeMin = p1;
            var placeMax = p2;


            var tmpX = (placeMax.X * placeMin.Y - placeMin.X * placeMax.Y) / (placeMin.Y - placeMax.Y + parametr.x2_x1_min * placeMax.X - parametr.x2_x1_min * placeMin.X);
            var tmpY = parametr.x2_x1_min * tmpX;
            if ((tmpX >= Math.Min(placeMin.X, placeMax.X) && tmpX <= Math.Max(placeMin.X, placeMax.X)) && (tmpY >= Math.Min(placeMin.Y, placeMax.Y) && tmpY <= Math.Max(placeMin.Y, placeMax.Y)))
            {
                outList.Add(new Point(tmpX, tmpY));
            }
            else
            {
                outList.Add(placeMax);
            }

            tmpX = (placeMax.X * placeMin.Y - placeMin.X * placeMax.Y) / (placeMin.Y -  placeMax.Y+ parametr.x2_x1_max * placeMax.X - parametr.x2_x1_max * placeMin.X);
            tmpY = parametr.x2_x1_max * tmpX;
            if ((tmpX >= Math.Min(placeMin.X, placeMax.X) && tmpX <= Math.Max(placeMin.X, placeMax.X)) && (tmpY >= Math.Min(placeMin.Y, placeMax.Y) && tmpY <= Math.Max(placeMin.Y, placeMax.Y)))
            {
                outList.Add(new Point(tmpX, tmpY));
            }
            else
            {
                outList.Add(placeMin);
            }

            return outList;
        }

        public void NormalizeLine(Line l)
        {

            var tmpX = (parametr.x1_min * parametr.x2_max - parametr.x1_max * parametr.x2_min) / (parametr.x2_max - parametr.x2_min - parametr.x2_x1_max * parametr.x1_max + parametr.x2_x1_max * parametr.x1_min);
            var tmpY = parametr.x2_x1_max * tmpX;
            var p1 = new Point(tmpX, tmpY);

            tmpX = (parametr.x1_min * parametr.x2_max - parametr.x1_max * parametr.x2_min) / (parametr.x2_max - parametr.x2_min - parametr.x2_x1_min * parametr.x1_max + parametr.x2_x1_min * parametr.x1_min);
            tmpY = parametr.x2_x1_min * tmpX;
            var p2 = new Point(tmpX, tmpY);
            l.Normalize(new Line(new Point(0, 0), p1, null), new Line(new Point(0, 0), p2, null));
        }

        private double GetParamByName(string nameParam)
        {
            var p = 0.0;
            if (ParamNow != null)
            {
                var pn = ParamNow.Find(t => t.name == nameParam);
                if (pn != null)
                    p = pn.value;
            }
            return p;
        }

        public override List<Parametr> GetAllParam
        {
            get
            {
                return new List<Parametr> { 
                new Parametr { name = "Separate", tp = TypeParams.discrete, minValue = 0, maxValue = 2 }, 
                new Parametr { name = "Diagonale", tp = TypeParams.discrete, minValue = 0, maxValue = 2 }, 
                new Parametr { name = "HillClimbibgStep", tp = TypeParams.continuous, minValue = 0, maxValue = 100 }, 
                new Parametr { name = "HilllimbibgCount", tp = TypeParams.discrete, minValue = 0, maxValue = 5 }, 
                new Parametr { name = "HowSortPlace", tp = TypeParams.continuous, minValue = 0, maxValue = 2 } 
                };
            }
        }

        public bool isInternal(Point p)
        {
            var com = p.Y/p.X;
            if (com < parametr.x2_x1_min || com > parametr.x2_x1_max)
                return false;
            return true;
        }

        #endregion

        #region Point

        public class Point
        {
            public Point(double x, double y)
            {
                X = x;
                Y = y;
                Cost = 0;
            }

            public double X
            {
                get;
                private set;
            }

            public double Y
            {
                get;
                private set;
            }

            public double Cost
            {
                get;
                set;
            }
        }

        #endregion

        #region Enums

        public enum Separated
        {
            Vertical,
            Horizontal,
            VerticalAndHorizonatal
        }

        public enum DiagonalThis
        {
            LeftBottomToRightTop,
            LeftTopToRightBottom,
            Both
        }

        public enum Sorted
        {
            ByCost,
            BySpace,
            Else
        }

        #endregion

        #region Line

        public class Line
        {
            public Point P1
            {
                get;
                private set;
            }

            public Point P2
            {
                get;
                private set;
            }

            public List<Point> Points
            {
                get { return new List<Point> { P1, P2 }; }
            }

            private Place P;

            public Line(Point p1, Point p2, Place p)
            {
                Init(p1, p2);
                P = p;
            }

            private void Init(Point p1, Point p2)
            {
                P1 = p1.X < p2.X ? p1 : p2;
                P2 = p1.X < p2.X ? p2 : p1;
            }

            public double Length()
            {
                return Math.Sqrt(Math.Pow(P1.X - P2.X, 2) + Math.Pow(P1.Y - P2.Y, 2));
            }

            public void Normalize(Line l1n, Line l2n)
            {
                var NewPoint1 = P1;
                var NewPoint2 = P2;
                var firstPoint = false;

                var tmpLine = l1n;
                var tmpX = ((P2.X * P1.Y - P1.X * P2.Y) * (tmpLine.P2.X - tmpLine.P1.X) - (tmpLine.P2.X * tmpLine.P1.Y - tmpLine.P1.X * tmpLine.P2.Y) * (P2.X - P1.X)) / 
                    ((tmpLine.P2.Y - tmpLine.P1.Y) * (P2.X - P1.X) - (P2.Y - P1.Y) * (tmpLine.P2.X - tmpLine.P1.X));
                var tmpY = (tmpX - tmpLine.P1.X) / (tmpLine.P2.X - tmpLine.P1.X);

                if ((tmpX >= Math.Min(P1.X, P2.X) && tmpX <= Math.Max(P1.X, P2.X)) && (tmpY >= Math.Min(P1.Y, P2.Y) && tmpY <= Math.Max(P1.Y, P2.Y)))
                {
                    if (!P.isInternal(P1))
                    {
                        NewPoint1 = new Point(tmpX, tmpY);
                        firstPoint = true;
                    }
                    else
                    {
                        NewPoint2 = new Point(tmpX, tmpY);
                        firstPoint = false;
                    }
                }

                tmpLine = l2n;
                tmpX = ((P2.X * P1.Y - P1.X * P2.Y) * (tmpLine.P2.X - tmpLine.P1.X) - (tmpLine.P2.X * tmpLine.P1.Y - tmpLine.P1.X * tmpLine.P2.Y) * (P2.X - P1.X)) /
                    ((tmpLine.P2.Y - tmpLine.P1.Y) * (P2.X - P1.X) - (P2.Y - P1.Y) * (tmpLine.P2.X - tmpLine.P1.X));
                tmpY = (tmpX - tmpLine.P1.X) / (tmpLine.P2.X - tmpLine.P1.X);

                if ((tmpX >= Math.Min(P1.X, P2.X) && tmpX <= Math.Max(P1.X, P2.X)) && (tmpY >= Math.Min(P1.Y, P2.Y) && tmpY <= Math.Max(P1.Y, P2.Y)))
                {
                    if (firstPoint)
                    {
                        NewPoint1 = new Point(tmpX, tmpY);
                    }
                    else
                    {
                        NewPoint2 = new Point(tmpX, tmpY);
                    }
                }


                Init(NewPoint1, NewPoint2);
            }
        }

        #endregion

        #region Place

        public class Place
        {
            private Random rnd;

            private Diagonal_Algoritms diagAlg;

            /// <summary>
            /// min
            /// </summary>
            public Point LeftBotom
            {
                get;
                private set;
            }

            /// <summary>
            /// max
            /// </summary>
            public Point RightTop
            {
                get;
                private set;
            }

            private Line LineLBtRT;
            private Line LineLTtRB;

            public Place(Point lb, Point rt, Diagonal_Algoritms da)
            {
                rnd = new Random();

                LeftBotom = new Point(Math.Min(lb.X, rt.X), Math.Min(lb.Y, rt.Y));
                RightTop = new Point(Math.Max(lb.X, rt.X), Math.Max(lb.Y, rt.Y));
                Cost = (lb.Cost > 0 && rt.Cost > 0) ? 
                    Math.Min(lb.Cost, rt.Cost) : 
                    lb.Cost > 0 ? 
                    lb.Cost : 
                    rt.Cost > 0 ? 
                    rt.Cost : 
                    0.0;
                Space = (RightTop.X - LeftBotom.X) * (RightTop.Y - LeftBotom.Y);
                diagAlg = da;
                LineLBtRT = new Line(LeftBotom, RightTop, this);
                da.NormalizeLine(LineLBtRT);
                LineLTtRB = new Line(new Point(LeftBotom.X, RightTop.Y), new Point(RightTop.X, LeftBotom.Y), this);
                da.NormalizeLine(LineLTtRB);
            }

            public bool isInternal(Point p)
            {
                if (p.X < LeftBotom.X || p.X > RightTop.X)
                    return false;
                if (p.Y < LeftBotom.Y || p.Y > RightTop.Y)
                    return false;
                return diagAlg.isInternal(p);
            }

            public List<Place> Separate(Point sp, Separated sep)
            {
                var outList = new List<Place>();
                if (sep == Separated.Vertical)
                {
                    outList.Add(new Place(LeftBotom, new Point(RightTop.X, sp.Y) { Cost = sp.Cost }, diagAlg));
                    outList.Add(new Place(new Point(LeftBotom.X, sp.Y) { Cost = sp.Cost }, RightTop, diagAlg));
                }
                else if (sep == Separated.Horizontal)
                {
                    outList.Add(new Place(LeftBotom, new Point(sp.X, RightTop.Y) { Cost = sp.Cost }, diagAlg));
                    outList.Add(new Place(new Point(sp.X, LeftBotom.Y) { Cost = sp.Cost }, RightTop, diagAlg));
                }
                else
                {
                    outList.Add(new Place(LeftBotom, sp, diagAlg));
                    outList.Add(new Place(sp, RightTop, diagAlg));
                    outList.Add(new Place(new Point(RightTop.X, LeftBotom.Y) { Cost = 0.0 }, sp, diagAlg));
                    outList.Add(new Place(sp, new Point(LeftBotom.X, RightTop.Y) { Cost = 0.0 }, diagAlg));
                }
                return outList;
            }

            public double Cost
            {
                get;
                private set;
            }

            public double Space
            {
                get;
                private set;
            }

            public static List<Place> Sort(List<Place> places, Sorted sort)
            {
                if (sort == Sorted.ByCost)
                {
                    return places.OrderBy(p => p.Cost).ToList();
                }
                else if (sort == Sorted.BySpace)
                {
                    return places.OrderBy(p => p.Space).ToList();
                }
                else
                {
                    return places.OrderBy(p => p.Space/p.Cost).ToList();
                }
            }

            public Point GetSeparatePoint(DiagonalThis diag)
            {
                if (diag == DiagonalThis.LeftBottomToRightTop)
                {
                    var points = LineLBtRT.Points;
                    var maxPoint = points.Find(t => t.X == points.Max(p => p.X));
                    var minPoint = points.Find(t => t.X == points.Min(p => p.X));

                    var retPoint = new Point(0, 0);
                    var timeout = 15;
                    var point = 0.0;


                    do
                    {
                        point = rnd.NextDouble();
                        var X = maxPoint.X - minPoint.X;
                        X = X * point + minPoint.X;
                        var Y = maxPoint.Y - minPoint.Y;
                        Y = Y * (1 - point) + minPoint.Y;
                        retPoint = new Point(X, Y);
                        timeout--;
                    }
                    while (!isInternal(retPoint) && timeout > 0);

                    if (isInternal(retPoint))
                    {
                        retPoint.Cost = GetCostInThisPoint(retPoint);
                    }
                    else
                    {
                        if (point > 0.5)
                        {
                            if (maxPoint.Cost > 0)
                                retPoint = maxPoint;
                            else
                            {
                                maxPoint.Cost = GetCostInThisPoint(maxPoint);
                                retPoint = maxPoint;
                            }
                        }
                        else
                        {
                            if (minPoint.Cost > 0)
                                retPoint = minPoint;
                            else
                            {
                                minPoint.Cost = GetCostInThisPoint(minPoint);
                                retPoint = minPoint;
                            }
                        }
                    }
                    return diagAlg.HillClim.Start(retPoint, LineLBtRT);
                }
                else if (diag == DiagonalThis.LeftTopToRightBottom)
                {

                    var points = LineLTtRB.Points;
                    var maxPoint = points.Find(t => t.X == points.Max(p => p.X));
                    var minPoint = points.Find(t => t.X == points.Min(p => p.X));

                    var retPoint = new Point(0, 0);
                    var timeout = 15;
                    var point = 0.0;

                    do
                    {
                        point = rnd.NextDouble();
                        var X = maxPoint.X - minPoint.X;
                        X = X * point + minPoint.X;
                        var Y = maxPoint.Y - minPoint.Y;
                        Y = Y * (1 - point) + minPoint.Y;
                        retPoint = new Point(X, Y);
                        timeout--;
                    }
                    while (!isInternal(retPoint) && timeout > 0);

                    if (isInternal(retPoint))
                    {
                        retPoint.Cost = GetCostInThisPoint(retPoint);
                    }
                    else
                    {
                        if (point > 0.5)
                        {
                            if (maxPoint.Cost > 0)
                                retPoint = maxPoint;
                            else
                            {
                                maxPoint.Cost = GetCostInThisPoint(maxPoint);
                                retPoint = maxPoint;
                            }
                        }
                        else
                        {
                            if (minPoint.Cost > 0)
                                retPoint = minPoint;
                            else
                            {
                                minPoint.Cost = GetCostInThisPoint(minPoint);
                                retPoint = minPoint;
                            }
                        }
                    }

                    return diagAlg.HillClim.Start(retPoint, LineLTtRB);
                }
                else
                {
                    var lbtrt = GetSeparatePoint(DiagonalThis.LeftBottomToRightTop);
                    var lttrb = GetSeparatePoint(DiagonalThis.LeftTopToRightBottom);
                    return lbtrt.Cost < lttrb.Cost ? lbtrt : lttrb;
                }
            }
            
            private double GetCostInThisPoint(Point p)
            {
                double a;
                try
                {
                    a = diagAlg.Function(p.X, p.Y).Cost;
                }
                catch
                {
                    a = Double.MaxValue;
                }
                return a;
            }
        }

        #endregion

        #region HillClimb

        public class HillClimb
        {
            public double Step { get; private set; }
            public int Number { get; private set; }
            private Diagonal_Algoritms DiagonAlg;

            public HillClimb(double step, int numb, Diagonal_Algoritms diag)
            {
                Step = step;
                Number = numb;
                DiagonAlg = diag;
            }

            public Point Start(Point p, Line l)
            {
                var n = Number;
                var point = p;
                var up = true;
                var first = true;

                while (n > 0)
                {
                    if (first)
                    {
                        if (n > 1)
                        {
                            var p1 = GetPoint(p, l.P2);
                            var p2 = GetPoint(l.P1, p);
                            p1.Cost = DiagonAlg.Function(p1.X, p1.Y).Cost;
                            p2.Cost = DiagonAlg.Function(p2.X, p2.Y).Cost;
                            if (p.Cost > 0 && p1.Cost > p.Cost && p2.Cost > p.Cost) return p;
                            if (p1.Cost > p2.Cost)
                            {
                                up = false;
                                point = p2;
                            }
                            else
                            {
                                up = true;
                                point = p1;
                            }
                        }
                        else
                        {
                            Point p1;
                            if((Math.Sqrt(Math.Pow(l.P1.X - p.X, 2) - Math.Pow(l.P1.Y - p.Y, 2))) > (Math.Sqrt(Math.Pow(l.P2.X - p.X, 2) - Math.Pow(l.P2.Y - p.Y, 2))))
                            {
                                p1 = GetPoint(p, l.P1);
                            }
                            else
                            {
                                p1 = GetPoint(p, l.P2);
                            }
                            p1.Cost = DiagonAlg.Function(p1.X, p1.Y).Cost;
                            if (p1.Cost > p.Cost)
                            {
                                return p;
                            }
                            else
                            {
                                return p1;
                            }
                        }
                        first = false;
                    }
                    if (up)
                    {
                        var p1 = GetPoint(point, l.P2);
                        p1.Cost = DiagonAlg.Function(p1.X, p1.Y).Cost;
                        if (p1.Cost > point.Cost)
                        {
                            return point;
                        }
                        else
                        {
                            point = p1;
                        }
                    }
                    else
                    {
                        var p2 = GetPoint(l.P1, point);
                        p2.Cost = DiagonAlg.Function(p2.X, p2.Y).Cost;
                        if (p2.Cost > point.Cost)
                        {
                            return point;
                        }
                        else
                        {
                            point = p2;
                        }
                    }
                    n--;
                }
                return point;
            }

            public Point GetPoint(Point start, Point end)
            {
                var x = (Step / 100 * (end.X - start.X) + start.X);
                var y = (Step / 100 * (end.Y - start.Y) + start.Y);
                return new Point(x, y);
            }
        }

        #endregion

        public override DataFormat.IOutBlackBoxParam Calculate()
        {
            if (itsLineOrPoint())
            {
                return bencmark(STEP);
            }

            List<Place> places = new List<Place>() { new Place(new Point(parametr.x1_max, parametr.x2_max), new Point(parametr.x1_min, parametr.x2_min), this) };

            var totalCalls = SetAreaOfTheRegion(STEP);

            #region convert input params
            var diagonal = GetParamByName("Diagonale");
            var diag = DiagonalThis.Both;
            if (diagonal == 1.0)
            {
                diag = DiagonalThis.LeftBottomToRightTop;
            }
            else if (diagonal == 2.0)
            {
                diag = DiagonalThis.LeftTopToRightBottom;
            }

            var separate = GetParamByName("Separate");
            var separ = Separated.VerticalAndHorizonatal;
            if (separate == 1.0)
            {
                separ = Separated.Vertical;
            }
            else if (separate == 2.0)
            {
                separ = Separated.Horizontal;
            }

            var sorted = GetParamByName("HowSortPlace");
            var sort = Sorted.Else;
            if (sorted == GetAllParam.Find(t => t.name == "HowSortPlace").minValue)
            {
                sort = Sorted.BySpace;
            }
            else if (sorted == GetAllParam.Find(t => t.name == "HowSortPlace").maxValue)
            {
                sort = Sorted.ByCost;
            }

            var HillStep = GetParamByName("HillClimbibgStep");

            var HillCount = GetParamByName("HillClimbibgCount");
            HillClim = new HillClimb(HillStep, (int)HillCount, this);

            #endregion

            var best = Double.MaxValue;

            while (totalCalls > Calls)
            {
                var ps = places[0].GetSeparatePoint(diag);
                best = Math.Min(ps.Cost, best);
                var newPlaces = places[0].Separate(ps, separ);
                places.RemoveAt(0);
                foreach (var place in newPlaces)
                {
                    if (place.Space > 1)
                    {
                        places.Add(place);
                    }
                }
                if (places.Count == 0)
                {
                    break;
                }
                Place.Sort(places, sort);
            }
            return new DataFormat.OutBlackBoxParam(best);

        }

        public HillClimb HillClim{get; private set;}

        private DataFormat.OutBlackBoxParam bencmark(double step)
        {
            var h = step;
            this.atributs += "Шаг алгоритма = " + h + " Па.";
            int n = 0; //количество успешных вычислений функции
            double cost = double.MaxValue;
            DataFormat.IOutBlackBoxParam a;

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
                            a = new DataFormat.OutBlackBoxParam(Double.MaxValue);  // Double.MaxValue;
                        }


                        //a = Function(34.946777049892944, 50.93829138975363);

                        if (n == 1)
                            cost = a.Cost;
                        else if (a.Cost < cost)
                            cost = a.Cost;

                    }
            return new DataFormat.OutBlackBoxParam(cost);
        }

        private bool itsLineOrPoint()
        {
            if (parametr.x1_max == parametr.x1_min || parametr.x2_max == parametr.x2_min || parametr.x2_x1_max == parametr.x2_x1_min)
                return true;
            return false;
        }
    }
}
