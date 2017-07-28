using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoE.em8.Core.Registry
{
    public interface IRegistryEnumerable<TId, TIt> :  IRegistry<TId, TIt>, IEnumerable<KeyValuePair<TId, TIt>>
    {
    }
}
