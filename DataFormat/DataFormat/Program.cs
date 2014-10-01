using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace DataFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            BlackBoxParameters qwe = new BlackBoxParameters();

            qwe.NewParam("bb1", "1", "2", "3", "4", "5", "6");

            qwe.NewParam("bb2", "6", "5", "4", "3", "2", "1");

            qwe.NewNextParam(1, "bb3", "237", "823", "5459", "223410", "12341", "122");

            qwe.NewNextParam(1, "bb4", "74", "348", "649", "1045", "1561", "1342");

            qwe.SaveFile("tratata");

            BlackBoxParameters qweqwe = new BlackBoxParameters();

            qweqwe.OpenFile("tratata");


        }
    }
}
