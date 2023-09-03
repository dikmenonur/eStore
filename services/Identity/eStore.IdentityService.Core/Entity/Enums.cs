using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.IdentityService.Core.Entity
{
    public class Enums
    {
        public enum UserType : int
        {
            Unknown = 0,
            User = 1,
            Admin = 2,
        }

        [Flags]
        public enum UserProperty : long
        {
            None = 0,
            Locked = 1,
            Banned = 2,
            NotVerified = 4,
            HasAcceptedTerms = 8,
        }

        public enum UserCredentialType : int
        {
            Unknown = 0,
            Email = 1,
            Username = 2,
            IdentityNumber = 3,
            CellPhone = 4

        }

        [Flags]
        public enum ApplicationProperty : long
        {
            None = 0,

            Blocked = 1
        }

        [Flags]
        public enum SegmentProperty : long
        {
            None = 0
        }
    }
}
