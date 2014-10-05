using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;

namespace TestSystem.Tasks
{
    class TaskPackage:ITaskPackage
    {
        private List<IBlackBox> boxes;
        private String name;
        private IEnterBlackBoxParam parameters;
        public TaskPackage(List<IBlackBox> boxes, IEnterBlackBoxParam parameters, String name)
        {
            this.boxes = boxes;
            this.name = name;
            this.parameters = parameters;
        }



        public List<IBlackBox> BlackBoxes
        {
            get { return boxes; }
        }

        public IEnterBlackBoxParam EnterParams
        {
            get { return parameters; }
        }

        public string Name
        {
            get {return name; }
        }
    }
}
