using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoE.em8.Core
{
    // TODO: Specifiy how deep this one copies, rename to state this fact in the name
    public interface ICloneable<T>
    {
        T Clone();
    }
}
