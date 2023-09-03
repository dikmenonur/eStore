using static eStore.IdentityService.Core.Entity.Enums;

namespace eStore.IdentityService.API.Models
{
    public class UserModel
    {
        public long Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string EMail { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public UserType UserType { get; set; }

    }
}
