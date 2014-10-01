﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataFormat;

namespace TestSystem.Algorithm
{
    /**
     * Этот интерфейс должны наследовать все алгоритмы
     */
    interface IAlgorithm
    {

        IEnterBlackBoxParam enterParam { set; }
        IOutBlackBoxParam Calculate();
        String Name {get; }
        
    }
}
