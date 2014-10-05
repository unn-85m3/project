using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.DataFormat
{
    /**
     * Этот интерфейс должен реализовывать класс, отвечающий за работу с файлами
     */
    interface IDataFormat
    {
        void SaveData(IEnterBlackBoxParam param);
        List<IEnterBlackBoxParam> GetData();
    }
}
