using eStore.Contracts.Domains.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.Contracts.Domains
{
    public abstract class EntityBase<TKey> : IEntityBase<TKey>
    {
        public TKey Id { get; set; }
    }
}
