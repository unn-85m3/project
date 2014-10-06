using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Tasks;

namespace TestSystem.DataFormat
{
    /**
     * Этот интерфейс должен реализовывать класс, отвечающий за работу с файлами
     */
    public interface IDataFormat
    {
        /// <summary>
        /// Сохранение чя
        /// </summary>
        /// <param name="param"></param>
        void SaveData(IBlackBox param);

        /// <summary>
        /// Забор данных чя, имени задания и ограничений
        /// </summary>
        /// <returns></returns>
        ITaskPackage GetData();
    }
}
