using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Algorithm.Diagonal_Algoritm3;
using TestSystem.Algorithm.DiagonalAlgorithm3.Places;

namespace TestSystem.Algorithm.DiagonalAlgorithm3.Separathors
{
    class Separathor:ISeparathor
    {

        public List<IPlace> Separate(IPlace place, IPoint point, int howSeparate)
        {
            List<IPlace> places = new List<IPlace>();
            IPlace tempPlace;
            if ((!place.isSeparated) && (point.cost != Double.MaxValue))
            {
                howSeparate = 0;
                switch (howSeparate)
                {
                    case 0:
                        tempPlace = CreatePlace(place.points[0], point, place);
                        if (tempPlace != null)
                            places.Add(tempPlace);

                        IPoint tempPoint1 = new Point(point.x1, place.points[0].x2);
                        IPoint tempPoint2 = new Point(place.points[3].x1, point.x2);
                        tempPlace = CreatePlace(tempPoint1, tempPoint2, place);
                        if (tempPlace != null)
                            places.Add(tempPlace);


                        tempPoint1 = new Point(place.points[2].x1, point.x2);
                        tempPoint2 = new Point(point.x1, place.points[1].x2);
                        tempPlace = CreatePlace(tempPoint1, tempPoint2, place);
                        if (tempPlace != null)
                            places.Add(tempPlace);


                        tempPlace = CreatePlace(point, place.points[0], place);
                        if (tempPlace != null)
                            places.Add(tempPlace);

                        place.isSeparated = true;
                        break;
                    //case 1:
                        

                    //    tempPoint1 = new Point(point.x1, place.points[2].x2);
                    //    tempPlace = CreatePlace(place.points[0], tempPoint1, place);
                    //    if (tempPlace != null)
                    //        places.Add(tempPlace);


                    //    tempPoint1 = new Point(point.x1, place.points[2].x2);
                    //    tempPlace = CreatePlace(tempPoint1, place.points[1], place);
                    //    if (tempPlace != null)
                    //        places.Add(tempPlace);

                    //    place.isSeparated = true;
                    //    break;
                    //case 2:
                    //    break;
                }
            }

            return places;
        }


        public List<IPlace> Separate(IPlace place, IPoint point)
        {
            List<IPlace> places = new List<IPlace>();
            IPlace tempPlace;
            if ((!place.isSeparated) && (point.cost != Double.MaxValue))
            {
                tempPlace = CreatePlace(place.points[0], point, place);
                if (tempPlace != null)
                    places.Add(tempPlace);

                IPoint tempPoint1 = new Point(point.x1, place.points[0].x2);
                IPoint tempPoint2 = new Point(place.points[3].x1, point.x2);
                tempPlace = CreatePlace(tempPoint1, tempPoint2, place);
                if (tempPlace != null)
                    places.Add(tempPlace);


                tempPoint1 = new Point(place.points[2].x1, point.x2);
                tempPoint2 = new Point(point.x1, place.points[1].x2);
                tempPlace = CreatePlace(tempPoint1, tempPoint2, place);
                if (tempPlace != null)
                    places.Add(tempPlace);


                tempPlace = CreatePlace(point, place.points[0], place);
                if (tempPlace != null)
                    places.Add(tempPlace);

                place.isSeparated = true;
            }

            return places;
        }

        private IPlace CreatePlace(IPoint ul,IPoint dr,IPlace parentPlace)
        {
            
            IPlace place= new Place(ul, dr);
            if (parentPlace.needSeparates > 1)
            {
                place.needSeparates = parentPlace.needSeparates - 1;
                place.generation = parentPlace.generation + 1;
            }
            if (place.Area <= 1)
                return null;
            return place;
        }

    }
}
