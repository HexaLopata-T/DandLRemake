using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DandLRemake
{
    interface IClonableWithLevel
    {
        object Clone(int level);
    }
}
