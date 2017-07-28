using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoE.em8.Core.Registry
{
    public interface IRegistry<TId,TIt>
    {
        TIt this[TId id] { get; }
        bool Contains(TId id);
        TIt Get(TId id);
        bool IsRegistered(TIt item);
        void Register(TId id, TIt item);
    }
}
