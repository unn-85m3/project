using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Algorithm.Diagonal_Algoritm
{
    interface IPlace
    {
        void addPlace(IPlace place);
        IPlace getPlace(int i);
        int length { get; }
        void removePlace(int i);
        IPoint point1 { get; set; }
        IPoint point2 { get; set; }

        int allLength { get; }
        List<IPlace> allPlaces { get; }
    }
}
