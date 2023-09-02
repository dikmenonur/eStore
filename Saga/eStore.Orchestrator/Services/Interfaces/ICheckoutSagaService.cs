namespace eStore.Orchestrator.Services.Interfaces
{
    public interface ICheckoutSagaService
    {
        Task<bool> CheckoutOrder(long cartId);
    }
}
