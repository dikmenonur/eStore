using eStore.IdentityService.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eStore.IdentityService.Core.DataSource
{
    public interface ITokenDataSource
    {
        TokenEntity Authenticate(User users);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
