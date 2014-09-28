using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace DataFormat
{
    class BlackBoxParameters
    {
        [DataContract]
        struct Parameter
        {
            // FUNCTION INPUT
            [DataMember(Name = "pin")]
            public double Pin;     // input pressure
            [DataMember(Name = "pout")]
            public double Pout;    // output pressure
            [DataMember(Name = "qout")]
            public double Qout;    // output quotation?
            [DataMember(Name = "tin")]
            public double Tin;     // input temperature
            [DataMember(Name = "cin")]
            public double Cin;     // input calorific
            [DataMember(Name = "din")]
            public double Din;     // input density

            // FUNCTION OUTPUT
            [DataMember(Name = "qin")]
            public double Qin;     // input quatation?
            [DataMember(Name = "tout")]
            public double Tout;    // output temperature
            [DataMember(Name = "opcosts")]
            public double OpCosts; // operating costs
            [DataMember(Name = "cacosts")]
            public double CaCosts; // capital costs
            [DataMember(Name = "expan")]
            public double Expan;   // expansion coefficient
            [DataMember(Name = "cout")]
            public double Cout;    // output calorific
            [DataMember(Name = "dout")]
            public double Dout;    // output density
        };

        List<Parameter> Params;

        public BlackBoxParameters()
        {
            Params = new List<Parameter>();
        }

        public int NewParam( // add new parameter collection
            double Pin, double Pout, double Qout, double Tin, double Cin, double Din, // "input"
            double Qin, double Tout, double OpCosts, double CaCosts, double Expan, double Cout, double Dout // "output"
            )
        {
            Parameter newParam;
            
            // "input"
            newParam.Pin = Pin;
            newParam.Pout = Pout;
            newParam.Qout = Qout;
            newParam.Tin = Tin;
            newParam.Cin = Cin;
            newParam.Din = Din;

            // "output"
            newParam.Qin = Qin;
            newParam.Tout = Tout;
            newParam.OpCosts = OpCosts;
            newParam.CaCosts = CaCosts;
            newParam.Expan = Expan;
            newParam.Cout = Cout;
            newParam.Dout = Dout;

            Params.Add(newParam);
            return Params.Count(); // return new collection's index
        }

        public void printAllParams()
        {
            int MaxI = Params.Count();
            for (int i = 0; i < MaxI; i++)
            {
                System.IO.File.WriteAllText("..\\Parameters.txt", "123");
            }
        }

        String itsFile(String fn)
        {
            if (fn.IndexOf('.') < 0)
            {
                fn += ".txt";
            }
            fn = "..\\" + fn;
            return fn;
        }

        void intoFile(String filename, String data)
        {
            filename = itsFile(filename);
            System.IO.File.WriteAllText(filename, data);
        }

        String outFile(String filename)
        {
            filename = itsFile(filename);
            String data = System.IO.File.ReadAllText(filename);
            return data;
        }

        public void SaveFile(String filename)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Parameter>));
            ser.WriteObject(stream, Params);
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            String jsondata = reader.ReadToEnd();
            intoFile(filename, jsondata);
        }

        public void OpenFile(String filename)
        {
            String jsondata = outFile(filename);
            System.Type typeofthis = this.GetType();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Parameter>));
            byte[] byteArray = Encoding.ASCII.GetBytes(jsondata);
            MemoryStream stream = new MemoryStream(byteArray);
            Params = (List<Parameter>)ser.ReadObject(stream);
        }
    }
}
