﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Plot
{
    interface IColoring
    {
        /**
         * передаётся 5 точек, 5я - центральная
         **/
        void ColoringSurface(List<IPoint> points);
    }
}