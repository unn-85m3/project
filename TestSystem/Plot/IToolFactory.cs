using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestSystem.Plot
{
    interface IToolFactory
    {
        IColoring CreateColoring(Form form);
        INormalize CreateNormalize(Form form);
    }
}
