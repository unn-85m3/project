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
        /// <summary>
        /// Structure for for storing a set of parameters.
        /// </summary>
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

        /// <summary>
        /// Fucking constructor.
        /// </summary>
        public BlackBoxParameters()
        {
            Params = new List<Parameter>();
        }

        /// <summary>
        /// Add new parameter collection.
        /// </summary>
        /// <param name="Pin">input pressure</param>
        /// <param name="Pout">output pressure</param>
        /// <param name="Qout">output quotation?</param>
        /// <param name="Tin">input temperature</param>
        /// <param name="Cin">input calorific</param>
        /// <param name="Din">input density</param>
        /// <param name="Qin">input quatation?</param>
        /// <param name="Tout">output temperature</param>
        /// <param name="OpCosts">operating costs</param>
        /// <param name="CaCosts">capital costs</param>
        /// <param name="Expan">expansion coefficient</param>
        /// <param name="Cout">output calorific</param>
        /// <param name="Dout">output density</param>
        /// <returns>new collection's index</returns>
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

        /// <summary>
        /// Print all parameter collections into text file.
        /// Not working!
        /// </summary>
        public void printAllParams()
        {
            int MaxI = Params.Count();
            for (int i = 0; i < MaxI; i++)
            {
                System.IO.File.WriteAllText("..\\Parameters.txt", "123");
            }
        }

        /// <summary>
        /// It is a file?!
        /// </summary>
        /// <param name="fn">file name</param>
        /// <returns>updated file name</returns>
        String itsFile(String fn)
        {
            if (fn.IndexOf('.') < 0)
            {
                fn += ".json";
            }
            fn = "..\\" + fn;
            return fn;
        }

        /// <summary>
        /// Write data into file.
        /// </summary>
        /// <param name="filename">file name</param>
        /// <param name="data">data</param>
        void intoFile(String filename, String data)
        {
            filename = itsFile(filename);
            System.IO.File.WriteAllText(filename, data);
        }

        /// <summary>
        /// Reading data from the file.
        /// </summary>
        /// <param name="filename">file name</param>
        /// <returns>file's data</returns>
        String outFile(String filename)
        {
            filename = itsFile(filename);
            String data = System.IO.File.ReadAllText(filename);
            return data;
        }

        /// <summary>
        /// Serialization parameter collections and save this data into json file.
        /// </summary>
        /// <param name="filename">file name</param>
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

        /// <summary>
        /// Opening file, reading, and deserializationing it. Then transfering data in collection.
        /// </summary>
        /// <param name="filename">json file name</param>
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
