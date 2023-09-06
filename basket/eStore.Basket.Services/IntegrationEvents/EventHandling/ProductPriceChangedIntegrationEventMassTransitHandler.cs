using eStore.Shared.IntegrationEvents;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.Basket.Services.IntegrationEvents.EventHandling
{
    public class ProductPriceChangedIntegrationEventMassTransitHandler : IConsumer<ProductEvent>
    {
        public Task Consume(ConsumeContext<ProductEvent> context)
        {
            Console.WriteLine("Consume Product with price change " + context.Message.Price);
            Console.WriteLine("Consume product price changes succesfully");
            return Task.CompletedTask;
        }
    }
}
