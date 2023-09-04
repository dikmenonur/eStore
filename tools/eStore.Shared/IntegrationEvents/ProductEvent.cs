using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.Shared.IntegrationEvents
{
    public record ProductEvent : IntegrationEvent
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
