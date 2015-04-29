using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Logger
{
    interface ILogger
    {
        bool HaveTask(String key);
        List<IPoint> GetLog(String key);
    }
}
