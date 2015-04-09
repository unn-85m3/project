using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestSystem.Drawer;

namespace TestSystem.Plot
{
    interface IToolFactory
    {
        IColoring CreateColoring(Form_Draw form);
        INormalize CreateNormalize(Form form);
    }
}
