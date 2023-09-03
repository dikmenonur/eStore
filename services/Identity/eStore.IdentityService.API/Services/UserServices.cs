using eStore.IdentityService.Core.DataSource;
using eStore.IdentityService.Core.Entity;

namespace eStore.IdentityService.API.Services
{
    public class UserServices : IUserServices
    {
        private readonly ISsoDataSource _ssoDataSource;

        public UserServices(ISsoDataSource ssoDataSource)
        {
            this._ssoDataSource = ssoDataSource;
        }

        public async Task<long> CreateUserAsync(User user)
        {
            return await this._ssoDataSource.CreateUserAsync(user);
        }

        public async Task<User> GetUserAsync(string alians, string password)
        {
            return await this._ssoDataSource.GetUserAsync(alians, password);
        }

        public async Task<User> GetUserByIdAsync(long id)
        {
            return await this._ssoDataSource.GetUserByIdAsync(id);
        }
    }
}
