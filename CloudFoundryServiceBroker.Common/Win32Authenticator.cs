using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;

namespace CloudFoundryServiceBroker.Common
{
    static class Win32Authenticator
    {
        #region Const

        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_LOGON_NETWORK = 3;
        public const int LOGON32_LOGON_BATCH = 4;
        public const int LOGON32_LOGON_SERVICE = 5;
        public const int LOGON32_LOGON_UNLOCK = 7;
        public const int LOGON32_LOGON_NETWORK_CLEARTEXT = 8;
        public const int LOGON32_LOGON_NEW_CREDENTIALS = 9;
        public const int LOGON32_PROVIDER_DEFAULT = 0;
        public const int LOGON32_PROVIDER_WINNT35 = 1;

        #endregion

        #region Import

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(string lpszUsername,
                                            string lpszDomain,
                                            string lpszPassword,
                                            int dwLogonType,
                                            int dwLogonProvider,
                                            out IntPtr phToken);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public extern static bool CloseHandle(IntPtr handle);

        #endregion

        public static IPrincipal Authenticate(String domainUser, String password)
        {
            IntPtr userToken;
            var creds = new DomainAuthCredentials(domainUser, password);

            if (!LogonUser(creds.Username, creds.Domain, creds.Password, LOGON32_LOGON_BATCH, LOGON32_PROVIDER_DEFAULT, out userToken))
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                throw new SecurityException("Error while authenticating user", error);
            }

            var identity = new WindowsIdentity(userToken);

            if (userToken != IntPtr.Zero)
                CloseHandle(userToken);

            return ConvertWindowsIdentityToGenericPrincipal(identity);
        }

        private static IPrincipal ConvertWindowsIdentityToGenericPrincipal(WindowsIdentity windowsIdentity)
        {
            if (windowsIdentity == null)
                return null;

            // Identity in format DOMAIN\Username
            var identity = new GenericIdentity(windowsIdentity.Name);

            var groupNames = new string[0];
            if (windowsIdentity.Groups != null)
            {
                // Array of Group-Names in format DOMAIN\Group
                groupNames = windowsIdentity.Groups.Select(gId => gId.Translate(typeof(NTAccount))).Select(gNt => gNt.ToString()).ToArray();
            }

            var genericPrincipal = new GenericPrincipal(identity, groupNames);
            return genericPrincipal;
        }
    }
}
