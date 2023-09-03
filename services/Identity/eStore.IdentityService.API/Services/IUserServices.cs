using eStore.IdentityService.Core.Entity;

namespace eStore.IdentityService.API.Services
{
    public interface IUserServices
    {
        Task<User> GetUserAsync(string alians, string password);
        Task<User> GetUserByIdAsync(long id);
        Task<long> CreateUserAsync(User user);
    }
}
