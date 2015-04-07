using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Plot
{
    class ToolFactory:IToolFactory
    {
        private static IToolFactory FACTORY = new ToolFactory();
        protected ToolFactory()
        {
        }



        public static IToolFactory Create()
        {
            return FACTORY;
        }

        public IColoring CreateColoring(System.Windows.Forms.Form form)
        {
            throw new NotImplementedException();
        }

        public INormalize CreateNormalize(System.Windows.Forms.Form form)
        {
            throw new NotImplementedException();
        }
    }
}
