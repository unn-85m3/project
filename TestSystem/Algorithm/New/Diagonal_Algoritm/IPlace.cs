using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Algorithm.New.Diagonal_Algoritm
{
    interface IPlace
    {
        void addPlace(IPlace place);
        IPlace getPlace(int i);
        IPlace parent { get; set; }
        int length { get; }
        void removePlace(int i);
        void removePlace(IPlace place);
        IPoint point1 { get; set; }
        IPoint point2 { get; set; }


        IPoint bestPoint { get;}

        int allLength { get; }
        List<IPlace> allPlaces { get; }
        void Separate(ICalculateFunction function);
        Boolean IsSeparated { get; }

    }
}
