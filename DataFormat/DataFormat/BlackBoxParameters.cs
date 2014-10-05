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
        struct BlackBox
        {
            // FUNCTION INPUT
            [DataMember(Name = "Black box name")]
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

        /// <summary>
        /// Structure for all black boxes' parameters.
        /// </summary>
        [DataContract]
        struct BlackBoxVarLimitation
        {
            [DataMember(Name = "x1")]
            public String X1;
            [DataMember(Name = "x2")]
            public String X2;
            [DataMember(Name = "x1/x2")]
            public String X1X2;

            [DataMember(Name = "black boxes")]
            public List<BlackBox> BlackBoxes;
        };

        //List<BlackBoxVarLimitation> Params;
        BlackBoxVarLimitation Params;

        public BlackBoxParameters()
        {
        }

        /// <summary>
        /// Constructor. Add new situation
        /// </summary>
        /// <param name="x1">limit variable X1</param>
        /// <param name="x2">limit variable X2</param>
        /// <param name="x1x2">limit variable X1/X2</param>
        public BlackBoxParameters(String x1, String x2, String x1x2)
        {
            //Params = new List<BlackBoxVarLimitation>();
            Params = new BlackBoxVarLimitation();
            Params.X1 = Convert.ToString(x1);
            Params.X2 = Convert.ToString(x2);
            Params.X1X2 = Convert.ToString(x1x2);
            Params.BlackBoxes = new List<BlackBox>();
        }

        /// <summary>
        /// Add new situation
        /// </summary>
        /// <param name="x1">limit variable X1</param>
        /// <param name="x2">limit variable X2</param>
        /// <param name="x1x2">limit variable X1/X2</param>
        /// <returns>index of new situation</returns>
        //public int NewLimit(String x1, String x2, String x1x2)
        //{
        //    BlackBoxVarLimitation NewLim = new BlackBoxVarLimitation();

        //    NewLim.X1 = Convert.ToString(x1);
        //    NewLim.X2 = Convert.ToString(x2);
        //    NewLim.X1X2 = Convert.ToString(x1x2);

        //    Params.Add(NewLim);

        //    return Params.Count();
        //}

        /// <summary>
        /// Add new black box.
        /// </summary>
        /// <param name="Name">name of black box</param>
        /// <param name="Pin">input pressure</param>
        /// <param name="Pout">output pressure</param>
        /// <param name="Qout">output quotation?</param>
        /// <param name="Tin">input temperature</param>
        /// <param name="Cin">input calorific</param>
        /// <param name="Din">input density</param>
        /// <returns>index of new black box</returns>
        public int NewParam( // add new parameter collection
            //int Index, 
            String Name, String Pin, String Pout, String Qout, String Tin, String Cin, String Din
            )
        {
            BlackBox newParam = new BlackBox();
            
            // "input"
            newParam.Name = Convert.ToString(Name);
            newParam.Pin = Convert.ToString(Pin);
            newParam.Pout = Convert.ToString(Pout);
            newParam.Qout = Convert.ToString(Qout);
            newParam.Tin = Convert.ToString(Tin);
            newParam.Cin = Convert.ToString(Cin);
            newParam.Din = Convert.ToString(Din);

            //Params[Index].NextBBoxes.Add(newParam);
            Params.BlackBoxes.Add(newParam);
            //return Params[Index].NextBBoxes.Count(); // return new collection's index
            return Params.BlackBoxes.Count(); // return new collection's index
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
        //public int NewNextParam(int BoxIndex, String Name, String Pin, String Pout, String Qout, String Tin, String Cin, String Din)
        //{
        //    BlackBox newNextParam = new BlackBox();

        //    // "input"
        //    newNextParam.Name = Name;
        //    newNextParam.Pin = Pin;
        //    newNextParam.Pout = Pout;
        //    newNextParam.Qout = Qout;
        //    newNextParam.Tin = Tin;
        //    newNextParam.Cin = Cin;
        //    newNextParam.Din = Din;

        //    Params[BoxIndex].NextBBoxes.Add(newNextParam);

        //    return Params[BoxIndex].NextBBoxes.Count();
        //}

        /// <summary>
        /// Print all parameter collections into text file.
        /// Not working!
        /// </summary>
        //public void PrintAllParams()
        //{
        //    int MaxI = Params.Count();
        //    for (int i = 0; i < MaxI; i++)
        //    {
        //        System.IO.File.WriteAllText("..\\Parameters.txt", "123");
        //    }
        //}

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
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<BlackBoxVarLimitation>));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(BlackBoxVarLimitation));
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
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<BlackBoxVarLimitation>));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(BlackBoxVarLimitation));
            byte[] byteArray = Encoding.ASCII.GetBytes(jsondata);
            MemoryStream stream = new MemoryStream(byteArray);
            //Params = (List<BlackBoxVarLimitation>)ser.ReadObject(stream);
            Params = (BlackBoxVarLimitation)ser.ReadObject(stream);
        }
    }
}
