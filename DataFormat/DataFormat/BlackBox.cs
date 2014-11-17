using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.DataFormat
{
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
}
