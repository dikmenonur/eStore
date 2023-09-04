using eStore.Basket.Data.DataSource;
using eStore.Basket.Services.IntegrationEvents.Event;
using eStore.Basket.Services.IntegrationEvents.EventHandling;
using eStore.Basket.Services.IntegrationEvents.MessageSubcribers;
using eStore.Shared.Common;
using eStore.Shared.Configurations;
using eStore.Shared.IntegrationEvents;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<BasketDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<ISerializeService, SerializeService>();
        builder.Services.AddScoped<IBasketDataSource, BasketDataSource>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
              
            });

        //builder.Services.AddGrpcClient<StockProtoService.StockProtoServiceClient>(options =>
        //{
        //    options.Address = new Uri("http://localhost:5174"); // Replace with the actual gRPC server URL
        //});
        //builder.Services.AddTransient<StockItemGrpcService>();

        builder.Services.AddTransient<IIntegrationEventHandler<ProductPriceChangedIntegrationEvent>, ProductPriceChangedIntegrationEventHandler>();

        // Extract settings
        var settings = builder.Configuration.GetSection("EventBusSettings").Get<EventBusSettings>();
        if (settings == null || string.IsNullOrEmpty(settings.HostAddress))
            throw new ArgumentNullException("EventBusSettings is not configured!");

        var mqConnection = new Uri(settings.HostAddress);


        // Configure MassTransit
        builder.Services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
        builder.Services.AddMassTransit(config =>
        {
            config.AddConsumersFromNamespaceContaining<ProductPriceChangedIntegrationEventMassTransitHandler>();
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(mqConnection);
                cfg.ConfigureEndpoints(ctx);
            });
            //submit fanout
            config.AddRequestClient<ProductEvent>();
        });

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        //subcribe to other serivces
        // Instantiate the class

        // Call the method to subscribe to orders
        var productUpdate = new ProductPriceChangedIntegrationEventSubcribe();
        productUpdate.Subscribe();

        var productDelete = new ProductDeleteIntegrationEventSubscribe();
        productDelete.Subscribe();

        app.Run();
    }
}