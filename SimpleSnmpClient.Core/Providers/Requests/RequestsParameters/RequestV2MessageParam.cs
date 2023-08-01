namespace SimpleSnmpClient.Core.Providers.Requests.RequestsParameters
{
    public class  RequestV2MessageParam : IRequestMessageParam
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string Community { get; set; }
        public int ResponseTimeout { get; set; }

        public RequestV2MessageParam(string ipAddress, int port, string community, int responseTimeout)
        {
            IpAddress = ipAddress;
            Port = port;
            Community = community;
            ResponseTimeout = responseTimeout;
        }

        public RequestV2MessageParam()
        {

        }
    }
}
