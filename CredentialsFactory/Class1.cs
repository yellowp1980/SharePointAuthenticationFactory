using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace CredentialsFactory
{
    /// <summary>
    /// returns credentials for on-perm or online
    /// </summary>
    public enum SharePointAuthentication
    {
        SharePointOnline,
        SharePointActiveDirectory
    };

    public class SharepointCredentials
    {
        public static ICredentials CreateCredentials(string UserName, string Password, SharePointAuthentication authticationType)
        {
            ICredentials result = null;

            switch (authticationType)
            {
                case SharePointAuthentication.SharePointActiveDirectory:
                    result = new NetworkCredential(UserName, Password);
                    break;
                case SharePointAuthentication.SharePointOnline:
                    SecureString securePassword = new SecureString();
                    foreach (char c in Password)
                    {
                        securePassword.AppendChar(c);
                    }
                    result = new SharePointOnlineCredentials(UserName, securePassword);
                    break;
            }

            return result;
        }
    }
}
