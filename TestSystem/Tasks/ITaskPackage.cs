using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;

namespace TestSystem.Tasks
{
    public interface ITaskPackage
    {
        /// <summary>
        /// Список ЧЯ
        /// </summary>
        List<IBlackBox> BlackBoxes { get; }

        /// <summary>
        /// Параметры ЧЯ
        /// </summary>
        IEnterBlackBoxParam EnterParams { get; }

        /// <summary>
        /// Имя задачи
        /// </summary>
        String Name { get; }
    }
}
