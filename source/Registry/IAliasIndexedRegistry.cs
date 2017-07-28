using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoE.em8.Core.Registry
{
    public interface IAliasIndexedRegistry<TAlias, TItem> : IEnumerable<TItem>
        where TItem : IAlias<TAlias>
    {
        TItem this[TAlias alias] { get; }
        bool ContainsKey(TAlias alias);
        bool ContainsValue(TItem item);
        TItem Get(TAlias alias);
        void Register(TItem item);
        bool TryGet(TAlias alias, out TItem item);
    }
}
