using eStore.IdentityService.Core.DataSource;
using eStore.IdentityService.Core.Entity;
using System.Security.Claims;

namespace eStore.IdentityService.API.Services
{
    public class TokenAuthenticationServices : ITokenAuthenticationServices
    {
        private readonly ITokenDataSource _tokenDataSource;
        public TokenAuthenticationServices(ITokenDataSource tokenDataSource)
        {
            this._tokenDataSource = tokenDataSource;

        }

        public async Task<TokenEntity> Authenticate(User users)
        {
            var tokesModel = this._tokenDataSource.Authenticate(users);
            // authentication successful so return user details without password
            return tokesModel;
        }

        public ClaimsPrincipal CheckTokenExpire(string token)
        {
            var tokesModel = this._tokenDataSource.GetPrincipalFromExpiredToken(token);
            return tokesModel;
        }
    }
}
