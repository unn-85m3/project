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

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="boxes">Список ЧЯ</param>
        /// <param name="parameters">Параметры ЧЯ</param>
        /// <param name="name">Имя задачи</param>
        public TaskPackage(List<IBlackBox> boxes, IEnterBlackBoxParam parameters, String name)
        {
            this.boxes = boxes;
            this.name = name;
            this.parameters = parameters;
        }


        /// <summary>
        /// Список ЧЯ
        /// </summary>
        public List<IBlackBox> BlackBoxes
        {
            get { return boxes; }
        }

        /// <summary>
        /// Параметры ЧЯ
        /// </summary>
        public IEnterBlackBoxParam EnterParams
        {
            get { return parameters; }
        }

        /// <summary>
        /// Имя задачи
        /// </summary>
        public string Name
        {
            get {return name; }
        }
    }
}
