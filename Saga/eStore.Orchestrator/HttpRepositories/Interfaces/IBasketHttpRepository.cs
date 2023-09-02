using eStore.Shared.DTOs.Basket;

namespace eStore.Orchestrator.HttpRepositories.Interfaces
{
    public interface IBasketHttpRepository
    {
        Task<bool> DeleteBasketAsync(string username);
        Task<CartDto> GetBasketAsync(long cartId);
    }
}
