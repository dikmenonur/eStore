﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.Shared.Configurations
{
    public interface IRuntimeSettings
    {
        public static RuntimeSettings Default { get; internal set; }

        public string RootDomain { get; set; }

        public string DefaultProfilePicture { get; set; }

        public string PublicUrlPattern { get; set; }

        public string CookieDomain { get; set; }

        public string CookieName { get; set; }

        public int CookieExpireMinutes { get; set; }

        public string EncryptionKey { get; set; }

        public string EncryptionIV { get; set; }

    }
}
