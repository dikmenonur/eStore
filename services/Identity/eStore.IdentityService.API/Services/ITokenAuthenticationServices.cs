using eStore.IdentityService.Core.Entity;
using System.Security.Claims;

namespace eStore.IdentityService.API.Services
{
    public interface ITokenAuthenticationServices
    {
        Task<TokenEntity> Authenticate(User users);
        ClaimsPrincipal CheckTokenExpire(string token);
    }
}
