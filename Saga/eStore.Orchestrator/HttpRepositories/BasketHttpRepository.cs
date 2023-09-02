using eStore.Orchestrator.HttpRepositories.Interfaces;
using eStore.Shared.DTOs.Basket;

namespace eStore.Orchestrator.HttpRepositories
{
    public class BasketHttpRepository : IBasketHttpRepository
    {
        private readonly HttpClient _httpClient;

        public BasketHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<bool> DeleteBasketAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<CartDto> GetBasketAsync(long cartId)
        {
            throw new NotImplementedException();
        }
    }
}
