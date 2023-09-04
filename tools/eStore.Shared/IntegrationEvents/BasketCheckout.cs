using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.Shared.IntegrationEvents
{
    public record BasketCheckout : IntegrationEvent
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string DocumentId { get; set; }
        public float TotalPrice { get; set; }
    }
}
