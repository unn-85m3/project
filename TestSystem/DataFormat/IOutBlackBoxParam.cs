﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.DataFormat
{
    /**
     * интерфейс, наследуемый классом, содержащим выходные параметры чёрного ящика
     */
    interface IOutBlackBoxParam
    {
        Double Cost { get; }
    }
}
