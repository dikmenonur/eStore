using eStore.Contracts.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.Basket.Core.Entity
{
    public class Cart : EntityBase<long>
    {
        public string UserName { get; set; }
        public List<CartItem>? BasketItems { get; set; } = new();

        public void AddItem(CartItem item)
        {
            BasketItems.Add(item);
        }

    }
}
