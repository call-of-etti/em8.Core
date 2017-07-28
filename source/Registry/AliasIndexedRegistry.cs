using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoE.em8.Core.Registry
{
    public class AliasIndexedRegistry<TAlias, TItem> : IAliasIndexedRegistry<TAlias, TItem>
        where TItem : IAlias<TAlias>
    {
        readonly Dictionary<TAlias, TItem> registry = new Dictionary<TAlias, TItem>();



        public TItem this[TAlias alias] => this.Get(alias);

        public bool ContainsKey(TAlias alias) => this.registry.ContainsKey(alias);

        public bool ContainsValue(TItem item) => this.registry.ContainsValue(item);

        public TItem Get(TAlias alias) => this.registry[alias];

        public IEnumerator<TItem> GetEnumerator() => this.registry.Values.Distinct().GetEnumerator();

        public void Register(TItem item)
        {
            foreach (TAlias alias in item.Aliases)
                this.registry.Add(alias, item);
        }

        public void RegisterRange(params TItem[] items)
        {
            foreach (var item in items)
                this.Register(item);
        }

        public bool TryGet(TAlias alias, out TItem item)
        {
            if (this.ContainsKey(alias))
            {
                item = this.Get(alias);
                return true;
            }
            else
            {
                item = default(TItem);
                return false;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
