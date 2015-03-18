using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saver
{
    interface ISaver
    {

        int Save(String name,String value);
        String Get(String name,String defaullValue);
        int Delete(String name);
    }
}
