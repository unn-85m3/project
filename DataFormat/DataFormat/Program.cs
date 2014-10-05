using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace TestSystem.DataFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            DataFormat qwe = new DataFormat();//"4-9", "45-90", "98-230");

            //int i = qwe.NewParam("bb1", "34", "x2", "23", "5465", "23", "45");
            //i = qwe.NewParam("bb2", "x1", "756", "345", "234", "56", "23");
            //i = qwe.NewParam("bb3", "546", "567", "4567.34", "656.055", "234", "444.44444");

            //DataFormat asd = new DataFormat("90-232", "23-34", "678-8900");

            //i = asd.NewParam("shaytan", "x2", "565", "443", "44", "54", "32");
            //i = asd.NewParam("shaytanama", "233", "x1", "836", "3454", "332", "67");

            String t1 = "..\\..\\..\\Tasks\\test_1.txt";
            //String t2 = "..\\..\\..\\Tasks\\test_2.txt";

            //qwe.SaveFile(t1);
            //asd.SaveFile(t2);

            qwe.OpenFile(t1);
            //asd.OpenFile(t2);


            //String ui = "x1";
            //double r = Convert.ToDouble(ui);

            //double rrr = double.Parse("23,5");
            //rrr = double.Parse("23,777-56");

            //int r = String.Compare("9", "x1");
            //r = String.Compare("9", "7456");
            //r = String.Compare("9", "x2");
            //r = String.Compare("9", "999999999999");
        }
    }
}
