namespace SimpleSnmpClient.Core.Providers.Requests.RequestsParameters
{
    public class RequestV3MessageParam : IRequestMessageParam
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string AuthPassword { get; set; }
        public string AuthProvider { get; set; }

        public string PrivPassword { get; set; }
        public string PrivProvider { get; set; }

        public string Username { get; set; }

        public string Context { get; set; } = string.Empty;

        public int DiscoveryTimeout  { get; set; }
        public int ResponseTimeout { get; set; }


        public RequestV3MessageParam(string ipAddress, int port, string authPassword, string authProvider
                                    ,string privPassword, string privProvider, string username, string context, int discoveryTimeout, int responseTimeout)
        {
            IpAddress = ipAddress;
            Port = port;
            AuthPassword = authPassword;
            AuthProvider = authProvider;
            PrivPassword = privPassword;
            PrivProvider = privProvider;
            Username = username;
            Context = context;
            DiscoveryTimeout = discoveryTimeout;
            ResponseTimeout = responseTimeout;
        }
        public RequestV3MessageParam()
        {

        }
    }
}
