using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Security;
using SimpleSnmpClient.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnmpClient.Core.Providers.Authentication
{
    public class AuthProviderFactory : IAuthProviderFactory
    {
        public IPrivacyProvider CreatePrivProvider(string privProvider, string privacyPassword, IAuthenticationProvider authenticationProvider)
        {
            return MapToPrivProvider(privProvider, privacyPassword, authenticationProvider);
        }

        public IAuthenticationProvider CreateAuthProvider(string authProvider, string authPassword)
        {
            return MapToAuthProvider(authProvider, authPassword);
        }


        private IPrivacyProvider MapToPrivProvider(string privProvider, string privacyPassword, IAuthenticationProvider authenticationProvider)
        {
            if (privProvider == "AES192")
            {
                if (AESPrivacyProviderBase.IsSupported)
                {
                    return new AES192PrivacyProvider(new OctetString(privacyPassword), authenticationProvider);
                }
                else
                {
                    return new BouncyCastleAESPrivacyProvider(new OctetString(privacyPassword), authenticationProvider);
                }

            }
            else if (privProvider == "AES256")
            {
                if (AESPrivacyProviderBase.IsSupported)
                {
                    return new AES256PrivacyProvider(new OctetString(privacyPassword), authenticationProvider);
                }
                else
                {
                    return new BouncyCastleAES256PrivacyProvider(new OctetString(privacyPassword), authenticationProvider);
                }
            }
            else if (privProvider == "AES")
            {
                if (AESPrivacyProviderBase.IsSupported)
                {
                    return new AESPrivacyProvider(new OctetString(privacyPassword), authenticationProvider);
                }
                else
                {
                    return new BouncyCastleAESPrivacyProvider(new OctetString(privacyPassword), authenticationProvider);
                }
            }
            else if (privProvider == "DES")
            {
                if (DESPrivacyProvider.IsSupported)
                {
                    return new DESPrivacyProvider(new OctetString(privacyPassword), authenticationProvider);
                }
                else
                {
                    return new BouncyCastleDESPrivacyProvider(new OctetString(privacyPassword), authenticationProvider);
                }
            }
            else if (privProvider == "TripleDES")
            {
                return new TripleDESPrivacyProvider(new OctetString(privacyPassword), authenticationProvider);
            }
            else
            {
                throw new NotSupportedException($"This privacy provider {privProvider} is not supported!");
            }
        }

        private IAuthenticationProvider MapToAuthProvider(string authProvider, string authPassword)
        {
            if (authProvider == "MD5")
                return new MD5AuthenticationProvider(new OctetString(authPassword));
            else if (authProvider == "SHA1")
                return new SHA1AuthenticationProvider(new OctetString(authPassword));
            else if (authProvider == "SHA256")
                return new SHA256AuthenticationProvider(new OctetString(authPassword));
            else if (authProvider == "SHA384")
                return new SHA384AuthenticationProvider(new OctetString(authPassword));
            else if (authProvider == "SHA512")
                return new SHA512AuthenticationProvider(new OctetString(authPassword));
            else
                throw new NotSupportedException($"This auth provider {authProvider} is not supported!");
        }
    }
}
