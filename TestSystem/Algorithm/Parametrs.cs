using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Algorithm
{
    enum TypeParams //этот enum говорит о том какого рода переменная
    {

        discrete,

        continuous

    }



    struct Parametr //это структура для параметров
    {

        public string name; //описание параметра
        public TypeParams tp;

        public double minValue; //диапозон значение
        public double maxValue;

        string ToString()
        {
            return "name = " + name + "\tminValue = " + minValue + " , maxValue = " + maxValue;
        }

    }



    struct ParametrNow //это параметры для проверки
    {

        public string name;

        public double value;

        string ToString()
        {
            return "name = " + name + ",\tvalue = " + value;
        }
    }
}
