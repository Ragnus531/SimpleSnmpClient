using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnmpClient.Data
{
    public static class Providers
    {
        public static IEnumerable<string> AUTHENTICATIONPROVIDERS = new string[]
        {
            "MD5", "SHA1","SHA256","SHA384","SHA512"
        };

        public static IEnumerable<string> PRIVACYPROVIDER = new string[]
        {
            "AES192", "AES256","AES","DES","TripleDES"
        };

        public static IEnumerable<string> DATATYPES = new string[]
        {
            "OctetString","Counter32", "Counter64","Gauge32","Unsigned32","Integer32"
        };
    }
}
