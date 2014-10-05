using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;

namespace TestSystem.Tasks
{
    interface ITaskPackage
    {
       List<IBlackBox> BlackBoxes{get;}
       IEnterBlackBoxParam EnterParams { get; }
       String Name { get; }
    }
}
