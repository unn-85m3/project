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
        protected static IEnterBlackBoxParam _parametr;


        List<IPlace> places;

        public Place(IPoint point1, IPoint point2,IEnterBlackBoxParam parametr)
        {
            _parametr = parametr;
            Init();
        }

        /*public Place(IPoint point1,IPoint point2)
        {
            Init();
        }*/

        private void Init()
        {
            this._point1 = point1;
            this._point2 = point2;
            places = new List<IPlace>();
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
                this._point2 = value;
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
                return (s+this.length);
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


        public void Separate()
        {
            throw new NotImplementedException();
        }



    }
}
