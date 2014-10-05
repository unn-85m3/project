using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace TestSystem.DataFormat
{ 
    class DataFormat : IDataFormat
    {
        /// <summary>
        /// Structure for storing a set of parameters.
        /// </summary>
        [DataContract]
        struct BlackBox
        {
            // FUNCTION INPUT
            [DataMember(Name = "Info")]
            public String info;
            [DataMember(Name = "PIn")]
            public String pIn;
            [DataMember(Name = "POut")]
            public String pOut;
            [DataMember(Name = "QOut")]
            public double qOut;
            [DataMember(Name = "TIn")]
            public double tIn;
            [DataMember(Name = "DIn")]
            public double dIn;
        };

        /// <summary>
        /// Structure for all black boxes' parameters.
        /// </summary>
        [DataContract]
        struct BlackBoxVarLimitation
        {
            [DataMember(Name = "x1")]
            public String x1;
            [DataMember(Name = "x2")]
            public String x2;
            [DataMember(Name = "x2/x1")]
            public String x1x2;

            [DataMember(Name = "black boxes")]
            public List<BlackBox> blackBoxes;
        };

        struct EnterBlackBoxParam : IEnterBlackBoxParam
        {
            public String info;
            public String pin;
            public String pout;
            public double qout;
            public double tin;
            public double din;

            public double x1min;
            public double x1max;
            public double x2min;
            public double x2max;
            public double compremMin;
            public double compremMax;
        };

        BlackBoxVarLimitation varLimitations;
        List<IEnterBlackBoxParam> bbParams;

        public DataFormat()
        {
            varLimitations = new BlackBoxVarLimitation();
            bbParams = new List<IEnterBlackBoxParam>();
        }

        /// <summary>
        /// Constructor. Add new situation
        /// </summary>
        /// <param name="x1">limit variable X1</param>
        /// <param name="x2">limit variable X2</param>
        /// <param name="x1x2">limit variable X1/X2</param>
        public DataFormat(String x1, String x2, String x1x2)
        {
            varLimitations = new BlackBoxVarLimitation();
            varLimitations.x1 = Convert.ToString(x1);
            varLimitations.x2 = Convert.ToString(x2);
            varLimitations.x1x2 = Convert.ToString(x1x2);
            varLimitations.blackBoxes = new List<BlackBox>();
        }

        /// <summary>
        /// Add new black box.
        /// </summary>
        /// <param name="bbName">name of black box</param>
        /// <param name="bbPin">input pressure</param>
        /// <param name="bbPout">output pressure</param>
        /// <param name="bbQout">output quotation?</param>
        /// <param name="bbTin">input temperature</param>
        /// <param name="bbCin">input calorific</param>
        /// <param name="bbDin">input density</param>
        /// <returns>index of new black box</returns>
        public int NewParam(String bbName, String bbPin, String bbPout, String bbQout, String bbTin, String bbCin, String bbDin)
        {
            BlackBox newParam = new BlackBox();
            
            // "input"
            newParam.info = Convert.ToString(bbName);
            newParam.pIn = Convert.ToString(bbPin);
            newParam.pOut = Convert.ToString(bbPout);
            newParam.qOut = Convert.ToDouble(bbQout);
            newParam.tIn = Convert.ToDouble(bbTin);
            newParam.dIn = Convert.ToDouble(bbDin);

            varLimitations.blackBoxes.Add(newParam);
            return varLimitations.blackBoxes.Count(); // return new collection's index
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
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(BlackBoxVarLimitation));
            ser.WriteObject(stream, varLimitations);
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
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(BlackBoxVarLimitation));
            byte[] byteArray = Encoding.ASCII.GetBytes(jsondata);
            MemoryStream stream = new MemoryStream(byteArray);
            varLimitations = (BlackBoxVarLimitation)ser.ReadObject(stream);
        }

        public void SaveData(IEnterBlackBoxParam param)
        {
            bbParams.Add(param);
        }

        public List<IEnterBlackBoxParam> GetData()
        {
            return bbParams;
        }

        public double p_in_min(int index)
        {
            String str = varLimitations.blackBoxes[index].pIn;
            double var;
            if (IsNum(str))
            {
                str = DotToComma(str);
                var = Convert.ToDouble(str);
            }
            else
            {
                char[] c = str.ToCharArray();
                if (c[1] == '1')
                {
                    return LimitMin(varLimitations.x1);
                }
                else
                {
                    return LimitMin(varLimitations.x2);
                }
            }
            return var;
        }

        public double p_in_max(int index)
        {
            String str = varLimitations.blackBoxes[index].pIn;
            double var;
            if (IsNum(str))
            {
                str = DotToComma(str);
                var = Convert.ToDouble(str);
            }
            else
            {
                char[] c = str.ToCharArray();
                if (c[1] == '1')
                {
                    return LimitMax(varLimitations.x1);
                }
                else
                {
                    return LimitMax(varLimitations.x2);
                }
            }
            return var;
        }

        public double p_oun_min(int index)
        {
            String str = varLimitations.blackBoxes[index].pOut;
            double var;
            if (IsNum(str))
            {
                str = DotToComma(str);
                var = Convert.ToDouble(str);
            }
            else
            {
                char[] c = str.ToCharArray();
                if (c[1] == '1')
                {
                    return LimitMin(varLimitations.x1);
                }
                else
                {
                    return LimitMin(varLimitations.x2);
                }
            }
            return var;
        }

        public double p_out_max(int index)
        {
            String str = varLimitations.blackBoxes[index].pOut;
            double var;
            if (IsNum(str))
            {
                str = DotToComma(str);
                var = Convert.ToDouble(str);
            }
            else
            {
                char[] c = str.ToCharArray();
                if (c[1] == '1')
                {
                    return LimitMax(varLimitations.x1);
                }
                else
                {
                    return LimitMax(varLimitations.x2);
                }
            }
            return var;
        }

        public double comprimate_min()
        {
            return LimitMin(varLimitations.x1x2);
        }

        public double comprimate_max()
        {
            return LimitMax(varLimitations.x1x2);
        }

        double LimitMin(String lim)
        {
            char[] c = lim.ToCharArray();
            String str = "";
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == '-')
                {
                    i = c.Length - 1;
                }
                else
                {
                    if (c[i] == '.')
                    {
                        str += ',';
                    }
                    else
                    {
                        str += c[i];
                    }
                }
            }
            double cmin = Convert.ToDouble(str);
            return cmin;
        }

        double LimitMax(String lim)
        {
            char[] c = lim.ToCharArray();
            String str = "";
            Boolean start = false;
            for (int i = 0; i < c.Length; i++)
            {
                if (start)
                {
                    if (c[i] == '.')
                    {
                        str += ',';
                    }
                    else
                    {
                        str += c[i];
                    }
                }
                else
                {
                    if (c[i] == '-')
                    {
                        start = true;
                    }
                }
            }
            double cmax = Convert.ToDouble(str);
            return cmax;
        }

        bool IsNum(String str)
        {
            if (String.Compare("9", str) < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        String DotToComma(String withdots)
        {
            String withcommas;
            char[] c = withdots.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == '.')
                {
                    c[i] = ',';
                }
            }
            withcommas = Convert.ToString(c);
            return withcommas;
        }
    }
}
