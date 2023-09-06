using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.Basket.Core.Entity
{
    public class CartItem
    {
        public int Id { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public long BasketId { get; set; }

        [ForeignKey(nameof(BasketId))]
        public Cart? Basket { get; set; }
    }
}
