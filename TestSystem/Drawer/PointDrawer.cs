using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.test_system;

namespace TestSystem.Drawer
{
    partial class Form_Draw: IEndCalculate
    {
        public void DrawPoints()
        {
           
           // Algorithms.SetListener(this);
            
        }

        public void OnEndTask(Algorithm.IAlgorithm alg, Tasks.ITaskPackage task, DataFormat.IOutBlackBoxParam rez, int time)
        {
            List<IPoint> points = new List<IPoint>();
            points = alg.points;
        }
    }
}
