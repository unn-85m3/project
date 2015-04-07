using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestSystem.Plot
{
    interface IToolFactory
    {
        public IColoring CreateColoring(Form form);
        public INormalize CreateNormalize(Form form);
    }
}
