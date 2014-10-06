using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
//using Exception;
using TestSystem.DataFormat;

namespace TestSystem.BackBoxFunction
{
    class BlackBoxFunction : IFunction, IOutBlackBoxParam
    {
        private Double cost;
        IBlackBox blackBox;
        NumberFormatInfo provider;

        public BlackBoxFunction()
        {
            provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
           /// provider.NumberGroupSeparator = ".";
            provider.NumberGroupSizes = new int[] { 2 };
        }

        public void Init(IBlackBox blackBox)
        {
            this.blackBox = blackBox;
        }

        public IOutBlackBoxParam Calculate(double x1, double x2)
        {
            if ((x2 - x1) < 0) throw new Exception();
            else
            {

                Double a = Convert.ToDouble(blackBox.TIn, provider);
                cost = ((Math.Sin(x1) * Math.Cos(x2) + Math.Max(x1, x2)) / Math.Sqrt(x2 - x1) )+a;
            }
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
