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
        /// Structure for storing a set of parameters.
        /// </summary>
        [DataContract]
        class Parameters
        {
            // FUNCTION INPUT
            [DataMember(Name = "black box name")]
            public String Name;
            [DataMember(Name = "input pressure")]
            public String Pin;
            [DataMember(Name = "output pressure")]
            public String Pout;
            [DataMember(Name = "output quotation")]
            public String Qout;
            [DataMember(Name = "input temperature")]
            public String Tin;
            [DataMember(Name = "input calorific")]
            public String Cin;
            [DataMember(Name = "input density")]
            public String Din;
        };

        [DataContract]
        class ChosenBBParameters : Parameters
        {
            [DataMember(Name = "next black boxes")]
            public List<Parameters> NextBBoxes;
        };

        List<ChosenBBParameters> Params;

        /// <summary>
        /// Constructor.
        /// </summary>
        public BlackBoxParameters()
        {
            Params = new List<ChosenBBParameters>();
        }

        /// <summary>
        /// Add new parameter in collection of Black Boxes.
        /// </summary>
        /// <param name="Name">name of black box</param>
        /// <param name="Pin">input pressure</param>
        /// <param name="Pout">output pressure</param>
        /// <param name="Qout">output quotation?</param>
        /// <param name="Tin">input temperature</param>
        /// <param name="Cin">input calorific</param>
        /// <param name="Din">input density</param>
        /// <returns>index of new element</returns>
        public int NewParam( // add new parameter collection
            String Name, String Pin, String Pout, String Qout, String Tin, String Cin, String Din
            )
        {
            ChosenBBParameters newParam = new ChosenBBParameters();
            
            // "input"
            newParam.Name = Name;
            newParam.Pin = Pin;
            newParam.Pout = Pout;
            newParam.Qout = Qout;
            newParam.Tin = Tin;
            newParam.Cin = Cin;
            newParam.Din = Din;

            newParam.NextBBoxes = new List<Parameters>();

            Params.Add(newParam);
            return Params.Count(); // return new collection's index
        }

        /// <summary>
        /// Add new parameter in collection of Next Black Boxes.
        /// </summary>
        /// <param name="BoxIndex">index of black box</param>
        /// <param name="Name">name of black box</param>
        /// <param name="Pin">input pressure</param>
        /// <param name="Pout">output pressure</param>
        /// <param name="Qout">output quotation?</param>
        /// <param name="Tin">input temperature</param>
        /// <param name="Cin">input calorific</param>
        /// <param name="Din">input density</param>
        /// <returns>index of new element</returns>
        public int NewNextParam(int BoxIndex, String Name, String Pin, String Pout, String Qout, String Tin, String Cin, String Din)
        {
            Parameters newNextParam = new Parameters();

            // "input"
            newNextParam.Name = Name;
            newNextParam.Pin = Pin;
            newNextParam.Pout = Pout;
            newNextParam.Qout = Qout;
            newNextParam.Tin = Tin;
            newNextParam.Cin = Cin;
            newNextParam.Din = Din;

            Params[BoxIndex].NextBBoxes.Add(newNextParam);

            return Params[BoxIndex].NextBBoxes.Count();
        }

        /// <summary>
        /// Print all parameter collections into text file.
        /// Not working!
        /// </summary>
        public void PrintAllParams()
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
        String ItsFile(String fn)
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
        void IntoFile(String filename, String data)
        {
            filename = ItsFile(filename);
            System.IO.File.WriteAllText(filename, data);
        }

        /// <summary>
        /// Reading data from the file.
        /// </summary>
        /// <param name="filename">file name</param>
        /// <returns>file's data</returns>
        String OutFile(String filename)
        {
            filename = ItsFile(filename);
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
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<ChosenBBParameters>));
            ser.WriteObject(stream, Params);
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            String jsondata = reader.ReadToEnd();
            IntoFile(filename, jsondata);
        }

        /// <summary>
        /// Opening file, reading, and deserializationing it. Then transfering data in collection.
        /// </summary>
        /// <param name="filename">json file name</param>
        public void OpenFile(String filename)
        {
            String jsondata = OutFile(filename);
            System.Type typeofthis = this.GetType();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<ChosenBBParameters>));
            byte[] byteArray = Encoding.ASCII.GetBytes(jsondata);
            MemoryStream stream = new MemoryStream(byteArray);
            Params = (List<ChosenBBParameters>)ser.ReadObject(stream);
        }
    }
}
