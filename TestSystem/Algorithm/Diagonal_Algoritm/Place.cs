using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestSystem.DataFormat;

namespace TestSystem.Algorithm.Diagonal_Algoritm
{
    class Place:IPlace
    {
        IPoint _point1;
        IPoint _point2;
        Random rng;
        Boolean isSeparated = false;
        IPlace _parent=null;
        IPoint best;
        protected static IEnterBlackBoxParam _parametr;


        List<IPlace> places;

        public Place(IPoint point1, IPoint point2,IEnterBlackBoxParam parametr)
        {
            _parametr = parametr;

            Init(point1, point2);
        }

       public Place(IPoint point1,IPoint point2)
        {
            Init(point1, point2);
        }

       private void Init(IPoint point1, IPoint point2)
        {
            rng = new Random(0);
            this._point1 = point1;
            this._point2 = point2;
            places = new List<IPlace>();

            if (!inThisPlace(point1))
            {
                
                point1.cost = Double.MaxValue;
            }

            if (!inThisPlace(point2))
            {
                
                point2.cost = Double.MaxValue;
            }
        }
        public void addPlace(IPlace place)
        {
            places.Add(place);
        }

        public IPlace getPlace(int i)
        {
            return places[i];
        }

        public int length
        {
            get { return places.Count; }
        }

        public void removePlace(int i)
        {
            this.places.RemoveAt(i);
        }

        public void removePlace(IPlace place)
        {
            this.places.Remove(place);
        }


        public IPoint point1
        {
            get
            {
                return this._point1;
            }
            set
            {
                this._point1 = value;
            }
        }

        public IPoint point2
        {
            get
            {
                return this._point2;
            }
            set
            {
                this._point2 = value;
            }
        }


        public int allLength
        {
            get
            {
                int s = 0;
                foreach (IPlace element in places)
                {
                    
                    s += element.allLength;
                }
                    s += this.length;
                return s;
            }
        }

        public List<IPlace> allPlaces
        {
            get
            {
                List<IPlace> p = new List<IPlace>();
                
                foreach (IPlace element in places)
                {

                    List<IPlace> pl = element.allPlaces;
                    p.Add(element);
                    foreach(IPlace val in pl)
                    {
                        if (!val.IsSeparated)
                        p.Add(val);
                    }
                }
                return (p);
            }
        }




        public Boolean inThisPlace(IPoint point)
        {

            if (Place.inPlace(point, _parametr) && inThis(point))
                return true;
            return false;
        }


        private Boolean inThis(IPoint point)
        {
            Double minX1;
            Double maxX1;

            Double maxX2;
            Double minX2;

            

            if(this._point1.x1>this._point2.x1)
            {
                minX1=this._point2.x1;
                maxX1=this._point1.x1;
            }else
            {
                maxX1=this._point2.x1;
                minX1=this._point1.x1;
            }



            if(this._point1.x2>this._point2.x2)
            {
                minX2=this._point2.x2;
                maxX2=this._point1.x2;
            }else
            {
                maxX2=this._point2.x2;
                minX2=this._point1.x2;
            }



            if((point.x1>=minX1)&&(point.x1<=maxX1)&&(point.x2>=minX2)&&(point.x2<=maxX2))
            {
                return true;
            }

            return false;

        }



        private void Calculate(IPoint point, ICalculateFunction calculate)
        {
            if (point != null)
            {
                if (point.x1 > _parametr.x1_max)
                {
                    point.x1 = _parametr.x1_max;
                }

                if (point.x1 < _parametr.x1_min)
                {
                    point.x1 = _parametr.x1_min;
                }

                if (point.x2 < _parametr.x2_min)
                {
                    point.x2 = _parametr.x2_min;
                }

                if (point.x2 > _parametr.x2_max)
                {
                    point.x2 = _parametr.x2_max;
                }

                try
                {

                    point.cost = calculate.Function(point.x1, point.x2).Cost;


                }
                catch
                {

                    point.cost = Double.MaxValue;

                }
            }

            setBest(point);
        }
        //79875530342@ya.ru 
        /**
         * прислать през. проекта
         */

        public static Boolean inPlace(IPoint point, IEnterBlackBoxParam parametr)
        {

            if((parametr.x1_min<=point.x1)&&(parametr.x1_max>=point.x1))
            {

                if((parametr.x2_min<=point.x2)&&(parametr.x2_max>=point.x2))
                {

                    if (((point.x2 / point.x1) <= parametr.x2_x1_max) && ((point.x2 / point.x1) >= parametr.x2_x1_min))
                     return true;
                }
            }

            return false;


        }


        public void Separate(ICalculateFunction calculate)
        {
            if (!isSeparated)
            {
                ILine line = new Line(point1, point2);

                if (line.length > 0)
                {
                   
                    IPoint p3 = new Point(point1.x1, point2.x2);
                    IPoint p4 = new Point(point2.x1, point1.x2);


                    IPoint p = this.GetPoint(line, calculate);

                    line = new Line(p3, p4);
                    IPoint pn = GetPoint(line, calculate);

                    if(p.cost>pn.cost)
                    {
                        p = pn;
                    }

                    ILine lenLine = line;

                    IPlace place;

                    lenLine.PointStart = point1;
                    lenLine.PointEnd = p;
                    if (lenLine.length > 0.1)
                    {
                        place = new Place(point1, p);
                        this.places.Add(place);
                        place.parent = this;
                    }

                    lenLine.PointStart = point2;
                    lenLine.PointEnd = p;
                    if (lenLine.length > 0.1)
                    {
                        place = new Place(p, point2);
                        this.places.Add(place);
                        place.parent = this;
                    }

                    

                    lenLine.PointStart = p3;
                    lenLine.PointEnd = p;
                    if (lenLine.length > 0.1)
                    {
                        place = new Place(p3, p);
                        this.places.Add(place);
                        place.parent = this;
                    }


                    lenLine.PointStart = p;
                    lenLine.PointEnd = p4;
                    if (lenLine.length > 0.1)
                    {
                        place = new Place(p, p4);
                        this.places.Add(place);
                        place.parent = this;
                    }
  
                    isSeparated = true;
                }
                else
                {


                    Calculate(point1, calculate);
                    
                }

            }
            

        }



       /* protected IPoint GetPoint(ILine line, ICalculateFunction calculate)
        {
            if (line.PointStart.cost == null)
            {
                Calculate(line.PointStart, calculate);
            }

            if (line.PointEnd.cost == null)
            {
                Calculate(line.PointStart, calculate);
            }

            Double step = line.length / 40;
            if (step < 0.1)
                step = 0.1;
            if (step > 5)
                step = 5;


            Double lPoint = this.rng.NextDouble() * (line.length);
            IPoint point = line.GetPoint(lPoint);


            Calculate(point, calculate);
            
                return point;
           
            


        }*/

        protected IPoint GetPoint(ILine line, ICalculateFunction calculate)
        {
            if (line.PointStart.cost == null)
            {
                Calculate(line.PointStart, calculate);
            }

            if (line.PointEnd.cost == null)
            {
                Calculate(line.PointStart, calculate);
            }
            
            Double step = line.length/40;
            if (step < 0.1)
                step = 0.1;
            if (step > 5)
                step = 5;


            Double lPoint = this.rng.NextDouble() * (line.length);
            IPoint point = line.GetPoint(lPoint);


            Calculate(point, calculate);
            if (line.length < 0.1)
            {
                return point;
            }
            Double llPoint = lPoint - step;
            IPoint left = line.GetPoint(llPoint);
            Double rlPoint = lPoint + step;
            IPoint right = line.GetPoint(rlPoint);
            
            IPoint main = point;
            Boolean isLef=true;

            if (right == null) right = main;
            if (left == null) left = main;
            Calculate(left, calculate);
            Calculate(right, calculate);
            
            if(left.cost<right.cost)
            {
                main=left;
            }else
            {
                   main=right;
                   isLef = false;
            }

            if (main.cost > point.cost)
            {
                return point;
            }



            while(true)
            {
                if (isLef)
                {
                    llPoint -= step;
                    left = line.GetPoint(llPoint);
                    if (left == null) return main;
                    Calculate(left,calculate);
                    if (left.cost < main.cost)
                    {
                        main = left;
                    }
                    else break;
                }
                else
                {
                    rlPoint += step;
                    right = line.GetPoint(rlPoint);
                    if (right == null) return main;
                    Calculate(right, calculate);
                    if (right.cost < main.cost)
                    {
                        main = right;
                    }
                    else break;
                }
            }

            return main;


        }
        

       


        public IPlace parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }

        private void setBest(IPoint p)
        {
            if ((best != null) && (best.cost != null)&&(p!=null))
            {
                if (best.cost > p.cost)
                    this.best = p;
            }
            else
            {
                this.best = p;
            }
            
        }

        public IPoint bestPoint
        {
            get { return best; }
        }


        public bool IsSeparated
        {
            get { return isSeparated; }
        }
    }
}
