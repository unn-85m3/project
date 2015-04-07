using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Plot
{
    interface IToolFactory
    {
        public IColoring CreateColoring();
        public INormalize CreateNormalize();
    }
}
