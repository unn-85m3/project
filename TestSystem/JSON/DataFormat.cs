using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using TestSystem.Tasks;

namespace TestSystem.DataFormat
{ 
    class DataFormat : IDataFormat
    {
        /// <summary>
        /// Structure for storing a set of parameters.
        /// </summary>
        [DataContract]
        struct BlackBoxParam
        {
            // FUNCTION INPUT
            [DataMember(Name = "Info")]
            public String info;
            [DataMember(Name = "PIn")]
            public String pIn;
            [DataMember(Name = "POut")]
            public String pOut;
            [DataMember(Name = "QOut")]
            public String qOut;
            [DataMember(Name = "TIn")]
            public String tIn;
            [DataMember(Name = "DIn")]
            public String dIn;
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
            public List<BlackBoxParam> blackBoxes;
        };

        class EnterBlackBoxParam : IEnterBlackBoxParam
        {
            double x1min;
            double x1max;
            double x2min;
            double x2max;
            double x1x2_min;
            double x1x2_max;

            public EnterBlackBoxParam(double x1_min, double x1_max, double x2_min, double x2_max, double x1_x2_min, double x1_x2_max)
            {
                this.x1max = x1_max;
                this.x1min = x1_min;
                this.x2max = x2_max;
                this.x2min = x2_min;
                this.x1x2_max = x1_x2_max;
                this.x1x2_min = x1_x2_min;
            }

            public Double x1_min
            {
                get { return x1min; }
            }
            
            public Double x1_max
            {
                get { return x1max; }
            }

            public Double x2_min
            {
                get { return x2min; }
            }

            public Double x2_max
            {
                get { return x2max; }
            }

            public Double x2_x1_min
            {
                get { return x1x2_min; }
            }

            public Double x2_x1_max
            {
                get { return x1x2_max; }
            }
        };

        class BlackBox : IBlackBox
        {
            String info;
            String pIn;
            String pOut;
            String qOut;
            String tIn;
            String dIn;
            String cIn;

            public BlackBox(String info, String pIn, String pOut, String qOut, String tIn, String dIn, String cIn)
            {
                this.info = info;
                this.pIn = pIn;
                this.pOut = pOut;
                this.qOut = qOut;
                this.tIn = tIn;
                this.dIn = dIn;
                this.cIn = cIn;
            }

            public String Info
            {
                get { return info; }
            }

            public String PIn
            {
                get { return pIn; }
            }

            public String POut
            {
                get { return pOut; }
            }

            public String QOut
            {
                get { return qOut; }
            }

            public String TIn
            {
                get { return tIn; }
            }

            public String DIn
            {
                get { return dIn; }
            }

            public String СIn
            {
                get { return cIn; }
            }
        };

        BlackBoxVarLimitation varLimitations;
        List<IBlackBox> bbParams;
        String fileName;

        /// <summary>
        /// Fucking constructor
        /// </summary>
        public DataFormat()
        {
            varLimitations = new BlackBoxVarLimitation();
            bbParams = new List<IBlackBox>();
        }

        /// <summary>
        /// Fucking constructor
        /// </summary>
        public DataFormat(String fileName)
        {
            varLimitations = new BlackBoxVarLimitation();
            bbParams = new List<IBlackBox>();
            this.fileName = fileName;
            OpenFile(fileName);
        }

        /// <summary>
        /// Constructor. Add new limitation
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
            varLimitations.blackBoxes = new List<BlackBoxParam>();
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
            BlackBoxParam newParam = new BlackBoxParam();
            
            // "input"
            newParam.info = Convert.ToString(bbName);
            newParam.pIn = Convert.ToString(bbPin);
            newParam.pOut = Convert.ToString(bbPout);
            newParam.qOut = Convert.ToString(bbQout);
            newParam.tIn = Convert.ToString(bbTin);
            newParam.dIn = Convert.ToString(bbDin);

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
            this.fileName = filename;
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

        public void SaveData(IBlackBox param)
        {
            bbParams.Add(param);
        }

        public ITaskPackage GetData()
        {
            EnterBlackBoxParam param = new EnterBlackBoxParam(LimitMin(varLimitations.x1), LimitMax(varLimitations.x1), 
                LimitMin(varLimitations.x2), LimitMax(varLimitations.x2), comprimate_min(), comprimate_max());
            foreach(BlackBoxParam bbp in varLimitations.blackBoxes)
            {
                BlackBox bb = new BlackBox(bbp.info, bbp.pIn, bbp.pOut, bbp.qOut, bbp.tIn, bbp.dIn, "0"); //ВНЕМАТОЧНО!!!!!
                bbParams.Add(bb);
            }

            TaskPackage task = new TaskPackage(bbParams, param, fileName);
            return task;
        }

        /// <summary>
        /// Minimum Pin
        /// </summary>
        /// <param name="index">index of black box</param>
        /// <returns>minimum Pin</returns>
        public double p_in_min(int index)
        {
            String str = varLimitations.blackBoxes[index].pIn;
            double var;
            if (ItsNum(str))
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

        /// <summary>
        /// Maximum Pin
        /// </summary>
        /// <param name="index">index of black box</param>
        /// <returns>maximum Pin</returns>
        public double p_in_max(int index)
        {
            String str = varLimitations.blackBoxes[index].pIn;
            double var;
            if (ItsNum(str))
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

        /// <summary>
        /// Minimum Pout
        /// </summary>
        /// <param name="index">index of black box</param>
        /// <returns>minimum Pout</returns>
        public double p_oun_min(int index)
        {
            String str = varLimitations.blackBoxes[index].pOut;
            double var;
            if (ItsNum(str))
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

        /// <summary>
        /// Maximum Pout
        /// </summary>
        /// <param name="index">index of black box</param>
        /// <returns>maximum Pout</returns>
        public double p_out_max(int index)
        {
            String str = varLimitations.blackBoxes[index].pOut;
            double var;
            if (ItsNum(str))
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

        /// <summary>
        /// Minimum comprimate coefficient
        /// </summary>
        /// <returns>minimum comprimate coefficient</returns>
        public double comprimate_min()
        {
            return LimitMin(varLimitations.x1x2);
        }

        /// <summary>
        /// Maximum comprimate coefficient
        /// </summary>
        /// <returns>maximum comprimate coefficient</returns>
        public double comprimate_max()
        {
            return LimitMax(varLimitations.x1x2);
        }

        /// <summary>
        /// Find the minimum on the Limitation
        /// </summary>
        /// <param name="lim">limitation</param>
        /// <returns>minimum</returns>
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

        /// <summary>
        /// Find the maximum on the Limitation
        /// </summary>
        /// <param name="lim">limitation</param>
        /// <returns>maximum</returns>
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

        /// <summary>
        /// Check for numeric string
        /// </summary>
        /// <param name="str">string to check</param>
        /// <returns>true - it's numeric string, false - it's not</returns>
        bool ItsNum(String str)
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

        /// <summary>
        /// Converts a string with dots in a string with a commas
        /// </summary>
        /// <param name="withdots">string with dots</param>
        /// <returns>string with commas</returns>
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
