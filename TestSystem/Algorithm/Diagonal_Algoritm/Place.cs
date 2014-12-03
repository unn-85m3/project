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
            rng = new Random();
            this._point1 = point1;
            this._point2 = point2;
            places = new List<IPlace>();

            if (!inThisPlace(point1))
            {
                point1.x1 = point1.x1 / 2;
                point1.x2 = point1.x2 / 2;
                point1.cost = new OutBlackBoxParam(Double.MaxValue);
            }

            if (!inThisPlace(point2))
            {
                point2.x1 = point2.x1 / 2;
                point2.x2 = point2.x2 / 2;
                point2.cost = new OutBlackBoxParam(Double.MaxValue);
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
            if(!isSeparated)
            {
                ILine line = new Line(point1, point2);
                if (point1.cost == null)
                {
                    try
                    {

                        point1.cost = calculate.Function(point1.x1, point1.x2);
                        setBest(point2);

                    }
                    catch
                    {

                        point1.x1 = point1.x1/2;
                        point1.x2 = point1.x2 / 2;
                        try
                        {
                            point1.cost = calculate.Function(point1.x1, point1.x2);
                        }catch
                        {
                            point1.cost = new OutBlackBoxParam(Double.MaxValue);
                        }
                    
                    }
                }

                if (point2.cost == null)
                {
                    try
                    {
                        point2.cost = calculate.Function(point2.x1, point2.x2);

                    }
                    catch
                    {
                        point2.cost = new OutBlackBoxParam(Double.MaxValue);
                    }
                    
                }

                if ((point2.cost != null) && (point1.cost != null))
                {
                if (point2.cost.Cost > point1.cost.Cost)
                    setBest(point1);
                else
                    setBest(point2);
                }

                IPoint p=line.GetPoint( 0.1+this.rng.NextDouble()*(line.length-0.2)/2);
               /* if (p == null)
                {
                    isSeparated = true;
                    return;
                }
                try
                    {
                        p.cost = calculate.Function(point2.x1, point2.x2);

                    }
                    catch
                    {
                        p.cost = new OutBlackBoxParam(Double.MaxValue);
                    }

                if (best.cost.Cost > p.cost.Cost)
                    setBest(p);*/
                
                
                IPlace place = new Place(point1, p);
                this.places.Add(place);
                place.parent = this;

                place = new Place(p, point2);
                this.places.Add(place);
                place.parent = this;

                IPoint p3 = new Point(point1.x1, point2.x2);
                place = new Place(p3,p);
                this.places.Add(place);
                place.parent = this;


                IPoint p4 = new Point(point2.x1, point1.x2);
                place = new Place(p, p4);
                this.places.Add(place);
                place.parent = this;

                isSeparated = true;
            }

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
            this.best = p;
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
