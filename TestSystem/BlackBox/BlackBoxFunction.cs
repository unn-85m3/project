using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;
using System.Globalization;
using TestSystem.Tasks;
using KSModels;

namespace TestSystem.BlackBox
{
    class BlackBoxFunction : IFunction, IOutBlackBoxParam
    {
        private Double cost;
        ITaskPackage task;
        NumberFormatInfo provider;
        private static String url = "C:/bbs_dll/Models/";
        private List<DllBlackBoxCalculator> functions;


        public BlackBoxFunction()
        {
            provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            /// provider.NumberGroupSeparator = ".";
            provider.NumberGroupSizes = new int[] { 2 };
           
          ///  function = new DllBlackBoxCalculator("C:/Users/Ирина Рыжова/Source/Repos/project/TestSystem/bbs_dll/Models/11.1.КС.r1",null);
        }

        public void Init(ITaskPackage task)
        {
            this.task = task;
            int i = 0;
            functions = new List<DllBlackBoxCalculator>();
            foreach(IBlackBox blackbox in task.BlackBoxes)
            {
                
                if (blackbox.Info != "Узел")
                {
                    functions.Add(new DllBlackBoxCalculator(url + blackbox.Info, blackbox.Info));
                    i++;
                }


            }

        }


        private Double Parametr(String x,Double x1,Double x2)
        {
            switch (x)
            {
                case "x1":
                    return x1;

                
                case "x2":
                    return x2;

                

                default:
                    return Convert.ToDouble(x, provider);
     
            }
        }

        public IOutBlackBoxParam Calculate(double x1, double x2)
        {
            cost = 0;
            int i = 0;
            foreach (IBlackBox blackBox in task.BlackBoxes)
            {
                if (blackBox.Info != "Узел")
                {
                    this.functions[i].PIn = Parametr(blackBox.PIn, x1, x2);
                    this.functions[i].POut = Parametr(blackBox.POut, x1, x2);
                    this.functions[i].QOut = Convert.ToDouble(blackBox.QOut, provider);
                    this.functions[i].TIn = Convert.ToDouble(blackBox.TIn, provider);
                    this.functions[i].CIn = Convert.ToDouble(blackBox.СIn, provider);
                    this.functions[i].DIn = Convert.ToDouble(blackBox.DIn, provider);
                    this.functions[i].Calculate();
                    cost += this.functions[i].EZ;
                    i++;
                }
            }

            OutBlackBoxParam param = new OutBlackBoxParam(cost);
            return param;
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
