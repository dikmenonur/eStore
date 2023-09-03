using eStore.IdentityService.Core.Entity;
using eStore.IdentityService.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.IdentityService.Core.DataSource
{
    public class SsoDataSource : ISsoDataSource
    {
        private readonly SsoDbContext DataContext;
        public SsoDataSource(SsoDbContext ssoDbContext)
        {
            this.DataContext = ssoDbContext;
        }
        public async Task<User> GetUserAsync(string alians, string password)
        {
            User getUser = new User();
            try
            {
                getUser = await this.DataContext.User.FirstOrDefaultAsync(t => (t.EMail == alians || t.PhoneNumber == alians));
                getUser = getUser.Password == password.PasswordSecurityHash() ? getUser : null;
            }
            catch (Exception)
            {

            }

            return getUser;
        }
        public async Task<long> CreateUserAsync(User user)
        {
            try
            {
                await this.DataContext.AddAsync(user);
                await this.DataContext.SaveChangesAsync();
                return user.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetUserByIdAsync(long id)
        {
            User getUser = new User();
            try
            {
                getUser = await this.DataContext.User.FirstOrDefaultAsync(t => t.Id == id);
            }
            catch (Exception)
            {

            }

            return getUser;
        }
    }
}
