using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;
using TestSystem.BlackBox;
using TestSystem.Algorithm.Diagonal_Algoritm;
using System.Data.OleDb;
using Saver;

namespace TestSystem.Algorithm
{
    /// <summary>
    /// От него наследуются все алгоритмы.
    /// Уэтого класса нет конструктора без параметра. Это сделано во избежние ошибок !!!
    /// </summary>
    abstract class AbsAlgorithm : IAlgorithm, ICalculateFunction
    {
        protected IEnterBlackBoxParam parametr; ///парамтры, в рамках которых проводится оптимизация
        protected string name = "Имя";/// имя алгоритма+имя автора
        protected string atributs = "Параметры алгоритма: ";// Параметры алгоритма
        private IFunction _function;///функция для оптимизации
        private int calls;
        public static Double STEP = 1;
        private List<IPoint> _points;
        private Saver.Saver saver;


       
        //
        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="parametr">входные параметры ф-и Ч.Я.</param>
        /// <param name="function">Используемая ф-ция</param>

        protected AbsAlgorithm()
        {
            calls = 0;
            _points = new List<IPoint>();
            
        }

        /// <summary>
        /// Имя алгоритма
        /// </summary>
        public virtual string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Параметры алгоритма (различные параметры, такие как шаг или колличество итераций) (нечто вроде версии алгоритма)
        /// </summary>
        public string Atributs
        {
            get
            {
                return atributs;
            }
        }


        /// <summary>
        /// через эту ф-ю можно изменить входные данные алгоритма
        /// </summary>
        public IEnterBlackBoxParam EnterParam
        {
            get { return parametr; }
            set { parametr = value; }
        }


        public void SetFunction(IFunction function)
        {
            this.function = function;
        }

        private IFunction function
        {
            set { _function = value; }
            get { return _function; }
        }


        public List<IPoint> points
        {
            get
            {

                return _points;
            }
        }

        public IOutBlackBoxParam Function(Double x1, Double x2, Int32 id = 0)
        {
            calls++;
            IOutBlackBoxParam p = function.Calculate(x1, x2);
            Point point = new Point(x1, x2, id);
            point.cost = p.Cost;
            this._points.Add(point);
            return p;
        }

        public IOutBlackBoxParam Function(Double x1, Double x2)
        {
            calls++;
            IOutBlackBoxParam p=function.Calculate(x1, x2);
            Point point = new Point(x1, x2);
            point.cost = p.Cost;
            this._points.Add(point);
            return p;
        }

        /// <summary>
        /// Здесь находится сам алгоритм отптимизации ф-и
        /// </summary>
        /// <returns>результат работы алгоритма</returns>
        public abstract IOutBlackBoxParam Calculate();




        public int Calls
        {
            get { return calls; }
        }

        public void Refresh()
        {
            calls = 0;
        }

        public int SetAreaOfTheRegion(Double h)
        {
            int n = 0;
            for (double i = this.parametr.x1_min; i <= this.parametr.x1_max; i += h)
                for (double j = this.parametr.x2_min; j <= this.parametr.x2_max; j += h)
                    if (((j / i) <= this.parametr.x2_x1_max) && ((j / i) >= this.parametr.x2_x1_min))
                    {
                        n++;
                    }
            return n;
        }
    }
}
