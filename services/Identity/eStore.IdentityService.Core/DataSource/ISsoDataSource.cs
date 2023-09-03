using eStore.IdentityService.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.IdentityService.Core.DataSource
{
    public interface ISsoDataSource
    {
        Task<User> GetUserAsync(string alians, string password);
        Task<User> GetUserByIdAsync(long id);
        Task<long> CreateUserAsync(User user);
    }
}
