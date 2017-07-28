using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoE.em8.Core.Registry
{
    public abstract class ARegistry<TId, TIt> : IRegistryEnumerable<TId, TIt>
    {
        Dictionary<TId, TIt> registry = new Dictionary<TId, TIt>();

        public ARegistry()
        {
        }

        public TIt this[TId id] => this.Get(id);


        public bool Contains(TId id)
        {
            return this.registry.ContainsKey(id);
        }

        public TIt Get(TId id) => this.registry[id];

        public IEnumerator<KeyValuePair<TId, TIt>> GetEnumerator() => ((IEnumerable<KeyValuePair<TId, TIt>>)registry).GetEnumerator();

        public bool IsRegistered(TIt item)
        {
            return this.registry.ContainsValue(item);
        }

        public void Register(TId id, TIt item)
        {
            this.registry.Add(id, item);
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<KeyValuePair<TId, TIt>>)registry).GetEnumerator();
    }
}
