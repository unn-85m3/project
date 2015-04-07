﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.BlackBox;
using TestSystem.Drawer;
using TestSystem.Tasks;

namespace TestSystem.Plot
{
    abstract class AbstractPlot:Form_Draw, IPlot
    {
        IToolFactory factory;
        public AbstractPlot(IFunction function, ITaskPackage task)
        {
            
        }


        public abstract void AddPoint(IPoint point);

        public abstract void DeletePoint(IPoint point);

        public abstract void Clear();
    }
}
