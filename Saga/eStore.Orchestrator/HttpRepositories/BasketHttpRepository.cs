using eStore.Orchestrator.HttpRepositories.Interfaces;

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

    }
}
