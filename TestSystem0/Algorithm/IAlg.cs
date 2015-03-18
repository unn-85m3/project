using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;

namespace TestSystem.Algorithm
{
    public interface IAlg
    {
        /// <summary>
        /// через эту ф-ю можно изменить входные данные алгоритма
        /// </summary>
        IEnterBlackBoxParam EnterParam { get; set; }

        /// <summary>
        /// Имя алгоритма
        /// </summary>
        String Name { get; }

        int Calls { get; }

        string Step { get; set; }
        
    }
}
