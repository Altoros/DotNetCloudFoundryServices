using System.IdentityModel.Tokens;
using CloudFoundryServiceBroker.Interfaces;

namespace CloudFoundryServiceBroker.Common
{
    /// <summary>
    /// User credentials validator over Windows (NTLM/LDAP)
    /// </summary>
    public class WindowsCredentialsValidator : ICredentialsValidator
    {
        /// <summary>
        /// Validate username and password with Windows
        /// </summary>
        /// <param name="userName">Local or domain user</param>
        /// <param name="password">User's password</param>
        /// <returns>True in case of successfull validation</returns>
        public bool Validate(string userName, string password)
        {
            var ip = Win32Authenticator.Authenticate(userName, password);
            if (ip == null || ip.Identity == null || !ip.Identity.IsAuthenticated)
            {
                throw new SecurityTokenValidationException("Invalid credentials!");
            }
            return true;
        }
    }
}
