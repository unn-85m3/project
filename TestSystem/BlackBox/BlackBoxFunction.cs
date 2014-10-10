using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;
using System.Globalization;
using KSModels;

namespace TestSystem.BlackBox
{
    class BlackBoxFunction : IFunction, IOutBlackBoxParam
    {
        private Double cost;
        IBlackBox blackBox;
        DllBlackBoxCalculator function;
        NumberFormatInfo provider;

        public BlackBoxFunction()
        {
            provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            /// provider.NumberGroupSeparator = ".";
            provider.NumberGroupSizes = new int[] { 2 };
            function = new DllBlackBoxCalculator("C:/Users/Ирина Рыжова/Source/Repos/project/TestSystem/bbs_dll/Models/11.1.КС.r1",null);
        }

        public void Init(IBlackBox blackBox)
        {
            this.blackBox = blackBox;
            
            
           
        }


        private Double Parametr(String x,Double x1,Double x2)
        {
            switch (x)
            {
                case "x1":
                    return x1;

                    break;
                case "x2":
                    return x2;

                    break;

                default:
                    return Convert.ToDouble(x, provider);

                    break;
            }
        }

        public IOutBlackBoxParam Calculate(double x1, double x2)
        {
            Double PIn=Parametr(blackBox.PIn,x1,x2);
            Double POut=Parametr(blackBox.POut,x1,x2);
            Double QOut = Convert.ToDouble(blackBox.QOut,provider);
            Double TIn = Convert.ToDouble(blackBox.TIn, provider);
            Double СIn = Convert.ToDouble(blackBox.СIn, provider);
            Double DIn = Convert.ToDouble(blackBox.DIn, provider);

            Double qin1=0;
            Double tout1=0;
            Double EZ=0;
            Double KZ=0;
            Double Expand=0;
            Double cout1=0;
            Double dout1=0;

                this.function.Calculate(PIn, POut, QOut, TIn, СIn, DIn, out qin1, out tout1, out  EZ, out  KZ, out  Expand, out  cout1, out  dout1);
            cost = EZ;
            
            /// cost = (Math.Sin(x1) * Math.Cos(x2) + Math.Max(x1, x2)) + Math.Cos(x1) + Math.Sin(x2);
            
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Cost
        {
            get { return cost; }
        }
    }
}
