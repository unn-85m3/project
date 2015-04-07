﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Plot
{
    interface IToolFactory
    {
        IColoring CreateColoring();
        INormalize CreateNormalize();
    }
}
