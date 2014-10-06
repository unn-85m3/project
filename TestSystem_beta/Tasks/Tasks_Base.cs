using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Tasks
{
    class Tasks_Base
    {
        private string name;
        public string Name { get { return name; } set { name = value; } }
        public Tasks_Base(string name) { Name = name; }
    }
}
