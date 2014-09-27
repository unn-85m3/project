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
    [DataContract]
    class cBlackBoxPropeties
    {
        struct Parameter
        {
            // FUNCTION INPUT
            [DataMember(Name = "pin")]
            double Pin;     // input pressure
            [DataMember(Name = "pout")]
            double Pout;    // output pressure
            [DataMember(Name = "qout")]
            double Qout;    // output quotation?
            [DataMember(Name = "tin")]
            double Tin;     // input temperature
            [DataMember(Name = "cin")]
            double Cin;     // input calorific
            [DataMember(Name = "din")]
            double Din;     // input density

            // FUNCTION OUTPUT
            [DataMember(Name = "qin")]
            double Qin;     // input quatation?
            [DataMember(Name = "tout")]
            double Tout;    // output temperature
            [DataMember(Name = "opcosts")]
            double OpCosts; // operating costs
            [DataMember(Name = "cacosts")]
            double CaCosts; // capital costs
            [DataMember(Name = "expan")]
            double Expan;   // expansion coefficient
            [DataMember(Name = "cout")]
            double Cout;    // output calorific
            [DataMember(Name = "dout")]
            double Dout;    // output density
        };

        Parameter[] Parameters;

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

        void Pack(String filename)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(this.GetType());
            ser.WriteObject(stream, this);
            StreamReader reader = new StreamReader(stream);
            String jsondata = reader.ReadToEnd();
            intoFile(filename, jsondata);
        }

        void UnPack(String filename)
        {
            String jsondata = outFile(filename);
            System.Type typeofthis = this.GetType();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeofthis);
            byte[] byteArray = Encoding.ASCII.GetBytes(jsondata);
            MemoryStream stream = new MemoryStream(byteArray);
            this. source = (this)ser.ReadObject(stream);
        }
    }
}
