namespace eStore.IdentityService.API.Models
{
    public class AuthenticateModel
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }

}
