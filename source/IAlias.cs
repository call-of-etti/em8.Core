using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoE.em8.Core
{
    public interface IAlias<T>
    {
        List<T> Aliases { get; }
    }
}
