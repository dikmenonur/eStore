using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eStore.IdentityService.Core.Entity.Enums;

namespace eStore.IdentityService.Core.Entity
{
    public class User
    {
        public long Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string EMail { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public UserType UserType { get; set; }

        public UserProperty Properties { get; set; }


        public User()
        {
        }

        public User(string Firstname, 
            string Lastname, 
            string EMail, 
            string PhoneNumber, 
            string Password, 
            UserType Type) 
        {
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.EMail = EMail;
            this.PhoneNumber = PhoneNumber;
            this.Password = Password;
            this.UserType = Type;
        }
    }
}
