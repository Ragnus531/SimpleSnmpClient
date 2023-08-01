using Lextm.SharpSnmpLib.Security;

namespace SimpleSnmpClient.Core.Providers.Authentication
{
    public interface IAuthProviderFactory
    {
        IPrivacyProvider CreatePrivProvider(string privProvider, string privacyPassword, IAuthenticationProvider authenticationProvider);
        IAuthenticationProvider CreateAuthProvider(string authProvider, string authPassword);
    }
}
