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
            BlackBoxParameters qwe = new BlackBoxParameters("4-9", "45-90", "98-230");

            int i = qwe.NewParam("bb1", "34", "x2", "23", "5465", "23", "45");
            i = qwe.NewParam("bb2", "x1", "756", "345", "234", "56", "23");
            i = qwe.NewParam("bb3", "546", "567", "4567.34", "656.055", "234", "444.44444");

            BlackBoxParameters asd = new BlackBoxParameters("90-232", "23-34", "678-8900");

            i = asd.NewParam("shaytan", "x2", "565", "443", "44", "54", "32");
            i = asd.NewParam("shaytanama", "233", "x1", "836", "3454", "332", "67");

            String t1 = "task1";
            String t2 = "task2";

            qwe.SaveFile(t1);
            asd.SaveFile(t2);

            qwe.OpenFile(t2);
            asd.OpenFile(t1);
        }
    }
}
