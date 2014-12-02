﻿using System;
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

            if (Place.inPlasce(point, _parametr)&&) ;
        }



        public static Boolean inPlasce(IPoint point, IEnterBlackBoxParam parametr)
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



    }
}
