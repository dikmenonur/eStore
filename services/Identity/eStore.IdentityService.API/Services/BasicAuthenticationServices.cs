using eStore.IdentityService.API.Models;

namespace eStore.IdentityService.API.Services
{
    public class BasicAuthenticationServices : IBasicAuthenticationServices
    {
        private List<AuthenticateModel> _users = new List<AuthenticateModel>
        {
            new AuthenticateModel {
                Username = "test",
                Password = "test"
            }
        };

        public async Task<AuthenticateModel> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            return user.WithoutPassword();
        }

        public async Task<IEnumerable<AuthenticateModel>> GetAll()
        {
            return await Task.Run(() => _users.WithoutPasswords());
        }
    }
}
