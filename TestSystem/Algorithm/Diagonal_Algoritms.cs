using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Algorithm
{
    class Diagonal_Algoritms : AbsAlgorithm
    {
        List<Rectangle> Rects = new List<Rectangle>();

        Random rnd;

        struct Rectangle
        {
            public PointInRect PIR, p1, p2;
            public double x1, y1, x2, y2;
            public void CreatRectangle(double x1, double y1, double x2, double y2) { this.x1 = x1; this.y1 = y1; this.x2 = x2; this.y2 = y2; }
            public void CreatRectangle(PointInRect p1, PointInRect p2) { this.p1 = p1; this.p2 = p2; }
        }


        struct PointInRect
        {
            public double x;
            public double y;
            public DataFormat.IOutBlackBoxParam cost;
            public void SetPoint(double x, double y) { this.x = x; this.y = y; }
            public void SetCost(DataFormat.OutBlackBoxParam cost) { this.cost = cost; }
        }

        public Diagonal_Algoritms()
        {
            rnd = new Random();
            this.name = "Диагональный метод";
            this.atributs += "";//Параметры алгоритма
        }

        public override DataFormat.IOutBlackBoxParam Calculate() // Доработать(переделать)
        {
            Rects.Add(new Rectangle());
            Rects[0].CreatRectangle(parametr.x1_min, parametr.x2_min, parametr.x1_max, parametr.x2_max);

            Start(Rects, 0); // НАчалось говно, хз как нормально сделать пока что




            int j = 0;
            while (j < 4)
            {
                Start(Rects, j);
                j++;
            }
            j = 0;
            while (j < 4)
            {
                Rects.RemoveAt(j);
                j++;
            }




            j = 0;
            while (j < 4 * 4)
            {
                Start(Rects, j);
                j++;
            }
            j = 0;
            while (j < 4 * 4)
            {
                Rects.RemoveAt(j);
                j++;
            }




            j = 0;
            while (j < 4 * 4 * 4)
            {
                Start(Rects, j);
                j++;
            }
            j = 0;
            while (j < 4 * 4 * 4)
            {
                Rects.RemoveAt(j);
                j++;
            }



            j = 0;
            while (j < 4 * 4 * 4 * 4)
            {
                Start(Rects, j);
                j++;
            }
            j = 0;
            while (j < 4 * 4 * 4 * 4)
            {
                Rects.RemoveAt(j);
                j++;
            }



            return GoodCost(Rects);
        }

        private DataFormat.IOutBlackBoxParam GoodCost(List<Rectangle> rect)
        {
            DataFormat.IOutBlackBoxParam cost = new DataFormat.OutBlackBoxParam(Double.MaxValue);
            foreach (var r in rect)
                if (cost.Cost > r.PIR.cost.Cost) cost = r.PIR.cost;
            return cost;
        }

        private List<Rectangle> Start(List<Rectangle> rect, int i) //Додумать
        {
            PointInRect pir = Diagonal(rect[i].x1, rect[i].y1, rect[i].x2, rect[i].y2);
            PointInRect tmp = new PointInRect();
            rect.Add(new Rectangle());
            tmp.SetPoint(rect[i].x1, rect[i].y1);
            rect[rect.Count - 1].CreatRectangle(tmp, pir);
            rect.Add(new Rectangle());
            rect[rect.Count - 1].CreatRectangle(pir.x, rect[i].y1, rect[i].x2, pir.y);
            rect.Add(new Rectangle());
            rect[rect.Count - 1].CreatRectangle(rect[i].x1, pir.y, pir.x, rect[i].y2);
            rect.Add(new Rectangle());
            tmp.SetPoint(rect[i].x2, rect[i].y2);
            rect[rect.Count - 1].CreatRectangle(pir, tmp);
            //rect.RemoveAt(i);
            return rect;
        }


        private PointInRect Diagonal(double x1, double y1, double x2, double y2) //ДОДУМАТЬ!!!!!!!
        {
            PointInRect PIR = new PointInRect();

            if (this.parametr.x2_x1_max != this.parametr.x2_x1_min) //область не является прямой
            {
                if (((x1 / y1) <= this.parametr.x2_x1_max)) // левый нижний угол ниже верхнего коэф-та компр.
                {
                    if (((x1 / y1) >= this.parametr.x2_x1_min)) // левый нижний угол выше нижнего коэф-та компр.
                    {
                        if (((x2 / y2) <= this.parametr.x2_x1_max)) // правый верхний угол ниже верхнего коэф-та компр.
                        {
                            if (((x2 / y2) >= this.parametr.x2_x1_min)) // правый верхний угол выше нижнего коэф-та компр.
                            {
                                //DiagonalX1Y1ToX2Y2Good(x1, y1, x2, y2); 
                                // вся диагональ внутри области
                                PIR.x = rnd.NextDouble() * (x2 - x1) + x1;
                                PIR.y = (x1 * y2 - x2 * y1 + (y1 - y2) * PIR.x) / (x1 - x2);
                            }
                            else
                            {
                                // Правый верхний угол ниже области
                                double tmpX2 = (x1 * y2 - x2 * y1) / ((x1 - x2) * this.parametr.x2_x1_min + y2 - y1);
                                double tmpY2 = tmpX2 * this.parametr.x2_x1_min;
                                PIR.x = rnd.NextDouble() * (tmpX2 - x1) + x1;
                                PIR.y = (x1 * tmpY2 - tmpX2 * y1 + (y1 - tmpY2) * PIR.x) / (x1 - tmpX2);
                            }
                        }
                        else
                        {
                            if (((x2 / y2) >= this.parametr.x2_x1_min))
                            {
                                // Правый верхний угол выше области
                                double tmpX2 = (x1 * y2 - x2 * y1) / ((x1 - x2) * this.parametr.x2_x1_max + y2 - y1);
                                double tmpY2 = tmpX2 * this.parametr.x2_x1_max;
                                PIR.x = rnd.NextDouble() * (tmpX2 - x1) + x1;
                                PIR.y = (x1 * tmpY2 - tmpX2 * y1 + (y1 - tmpY2) * PIR.x) / (x1 - tmpX2);
                            }
                            else
                            {
                                // Всё плохо
                            }
                        }
                    }
                    else
                    {
                        if (((x2 / y2) <= this.parametr.x2_x1_max))
                        {
                            if (((x2 / y2) >= this.parametr.x2_x1_min))
                            {
                                // нижний левый угол вне области
                                // Левый нижний угол ниже области
                                double tmpX1 = (x1 * y2 - x2 * y1) / ((x1 - x2) * this.parametr.x2_x1_min + y2 - y1);
                                double tmpY1 = tmpX1 * this.parametr.x2_x1_min;
                                PIR.x = rnd.NextDouble() * (x2 - tmpX1) + tmpX1;
                                PIR.y = (tmpX1 * y2 - x2 * tmpY1 + (tmpY1 - y2) * PIR.x) / (tmpX1 - x2);
                            }
                            else
                            {   // все плохо может быть
                                if (((x1 / y2) >= this.parametr.x2_x1_min) && ((x1 / y2) <= this.parametr.x2_x1_min))
                                {

                                    // нижний левый угол вне области
                                    // Левый нижний угол выше области
                                    double tmpX1 = (x1 * y2 - x2 * y1) / ((x1 - x2) * this.parametr.x2_x1_min + y2 - y1);
                                    double tmpY1 = tmpX1 * this.parametr.x2_x1_min;
                                    double tmpX2 = (x1 * y2 - x2 * y1) / ((x1 - x2) * this.parametr.x2_x1_min + y2 - y1);
                                    double tmpY2 = tmpX2 * this.parametr.x2_x1_min;
                                    PIR.x = rnd.NextDouble() * (tmpX2 - tmpX1) + tmpX1;
                                    PIR.y = (tmpX1 * tmpY2 - tmpX2 * tmpY1 + (tmpY1 - tmpY2) * PIR.x) / (tmpX1 - tmpX2);
                                }
                                else
                                {
                                    // Всё плохо
                                }
                            }
                        }
                        else
                        {
                            if (((x2 / y2) >= this.parametr.x2_x1_min))
                            {

                            }
                            else
                            {

                            }
                        }
                    }
                }
                else if (((x1 / y1) >= this.parametr.x2_x1_min))
                {
                    if (((x2 / y2) <= this.parametr.x2_x1_max))
                    {
                        if (((x2 / y2) >= this.parametr.x2_x1_min))
                        {
                            // нижний левый угол вне области
                            // Левый нижний угол выше области
                            double tmpX1 = (x1 * y2 - x2 * y1) / ((x1 - x2) * this.parametr.x2_x1_max + y2 - y1);
                            double tmpY1 = tmpX1 * this.parametr.x2_x1_max;
                            PIR.x = rnd.NextDouble() * (x2 - tmpX1) + tmpX1;
                            PIR.y = (tmpX1 * y2 - x2 * tmpY1 + (tmpY1 - y2) * PIR.x) / (tmpX1 - x2);
                        }
                        else
                        {
                            // нижний левый угол вне области
                            // Левый нижний угол выше области
                            double tmpX1 = (x1 * y2 - x2 * y1) / ((x1 - x2) * this.parametr.x2_x1_max + y2 - y1);
                            double tmpY1 = tmpX1 * this.parametr.x2_x1_max;
                            double tmpX2 = (x1 * y2 - x2 * y1) / ((x1 - x2) * this.parametr.x2_x1_min + y2 - y1);
                            double tmpY2 = tmpX2 * this.parametr.x2_x1_min;
                            PIR.x = rnd.NextDouble() * (tmpX2 - tmpX1) + tmpX1;
                            PIR.y = (tmpX1 * tmpY2 - tmpX2 * tmpY1 + (tmpY1 - tmpY2) * PIR.x) / (tmpX1 - tmpX2);
                        }
                    }
                    else
                    {
                        if (((x2 / y2) >= this.parametr.x2_x1_min))
                        {

                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    // Всё очень плохо
                    if (((x2 / y2) <= this.parametr.x2_x1_max))
                    {
                        if (((x2 / y2) >= this.parametr.x2_x1_min))
                        {

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (((x2 / y2) >= this.parametr.x2_x1_min))
                        {

                        }
                        else
                        {

                        }
                    }
                }
            }
            else // область является прямой
            {

            }
            PIR.cost = Function(PIR.x, PIR.y);
            return PIR;
        }

        ///// <summary>
        ///// Метод находящий точку внутри области
        ///// </summary>
        ///// <param name="x1">левый нижний угол</param>
        ///// <param name="y1">левый нижний угол</param>
        ///// <param name="x2">правый верхний угол</param>
        ///// <param name="y2">правый верхний угол</param>
        //private void DiagonalX1Y1ToX2Y2Good(double x1, double y1, double x2, double y2)
        //{
        //    PointInRect PIR;
        //    PIR.x = rnd.NextDouble() * (x2 - x1) + x1;
        //    PIR.y = (x1 * y2 - x2 * y1 + (y1 - y2) * PIR.x) / (x1 - x2);
        //}
    }
}
