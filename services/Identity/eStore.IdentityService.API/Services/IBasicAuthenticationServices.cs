using eStore.IdentityService.API.Models;

namespace eStore.IdentityService.API.Services
{
    public interface IBasicAuthenticationServices
    {
        Task<AuthenticateModel> AuthenticateTest(string username, string password);
        Task<IEnumerable<AuthenticateModel>> GetAll();
    }
}
