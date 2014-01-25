using System;

namespace CloudFoundryServiceBroker.Common
{
    class DomainAuthCredentials
    {
        public DomainAuthCredentials(String domainUser, String password)
        {
            Username = domainUser;
            Password = password;
            Domain = ".";

            if (!domainUser.Contains(@"\"))
                return;

            var tokens = domainUser.Split(new [] { '\\' }, 2);
            Domain = tokens[0];
            Username = tokens[1];
        }

        public DomainAuthCredentials()
        {
            Domain = String.Empty;
        }

        #region Properties

        public String Domain { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }

        #endregion
    }
}
