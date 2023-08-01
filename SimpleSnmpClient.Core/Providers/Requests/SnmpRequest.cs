using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib.Security;
using SimpleSnmpClient.Core.Providers.Authentication;
using SimpleSnmpClient.Core.Providers.DataTypeProvider;
using SimpleSnmpClient.Core.Providers.Requests.RequestsParameters;
using System.Net;

namespace SimpleSnmpClient.Core.Providers.Requests
{
    //TODO: Maybe Seperate each request (trap bulk get set ) into each class Strategy pattern!
    public class SnmpRequest : ISnmpRequest
    {
        private readonly IAuthProviderFactory _authProviderFactory;

        public SnmpRequest(IAuthProviderFactory authProviderFactory)
        {
            _authProviderFactory = authProviderFactory;
        }

        #region SnmpV2Requests
        public ISnmpMessage GetSnmpV2Request(RequestV2MessageParam requestV2MessageParams, string oid)
        {
            List<Variable> list = new List<Variable> { new Variable(new ObjectIdentifier(oid)) };
            GetRequestMessage request = new GetRequestMessage(0, VersionCode.V2, new OctetString(requestV2MessageParams.Community), list);
            ISnmpMessage reply = request.GetResponse(requestV2MessageParams.ResponseTimeout, new IPEndPoint(IPAddress.Parse(requestV2MessageParams.IpAddress), requestV2MessageParams.Port));
            return reply;
        }

        public async Task<ISnmpMessage> GetSnmpV2RequestAsync(RequestV2MessageParam requestV2MessageParams, string oid)
        {
            List<Variable> list = new List<Variable> { new Variable(new ObjectIdentifier(oid)) };
            ISnmpMessage request = new GetRequestMessage(0, VersionCode.V2, new OctetString(requestV2MessageParams.Community), list);
            ISnmpMessage reply = await request.GetResponseAsync(new IPEndPoint(IPAddress.Parse(requestV2MessageParams.IpAddress), requestV2MessageParams.Port));
            return reply;
        }

        public async Task<ISnmpMessage> GetSnmpV2RequestAsync(RequestV2MessageParam requestV2MessageParams, string oid, CancellationToken cancellationToken)
        {
            List<Variable> list = new List<Variable> { new Variable(new ObjectIdentifier(oid)) };
            GetRequestMessage request = new GetRequestMessage(0, VersionCode.V2, new OctetString(requestV2MessageParams.Community), list);
            ISnmpMessage reply = await request.GetResponseAsync(new IPEndPoint(IPAddress.Parse(requestV2MessageParams.IpAddress), requestV2MessageParams.Port), cancellationToken);
            return reply;
        }

        public ISnmpMessage SetSnmpV2Request(RequestV2MessageParam requestV2MessageParams, string oid, string value, string dataType)
        {
            ISnmpData dt = SnmpDataTypeBuilder.BuildSnmpDataType(dataType,value);
            List<Variable> list = new List<Variable> { new Variable(new ObjectIdentifier(oid), dt) };
            SetRequestMessage request = new SetRequestMessage(0, VersionCode.V2, new OctetString(requestV2MessageParams.Community), list);
            ISnmpMessage reply = request.GetResponse(requestV2MessageParams.ResponseTimeout, new IPEndPoint(IPAddress.Parse(requestV2MessageParams.IpAddress), requestV2MessageParams.Port));
            return reply;
        }

        public async Task<ISnmpMessage> SetSnmpV2RequestAsync(RequestV2MessageParam requestV2MessageParams, string oid, string value)
        {
            List<Variable> list = new List<Variable> { new Variable(new ObjectIdentifier(oid)) };
            SetRequestMessage request = new SetRequestMessage(0, VersionCode.V2, new OctetString(requestV2MessageParams.Community), list);
            ISnmpMessage reply = await request.GetResponseAsync(new IPEndPoint(IPAddress.Parse(requestV2MessageParams.IpAddress), requestV2MessageParams.Port));
            return reply;
        }

        public async Task<ISnmpMessage> SetSnmpV2RequestAsync(RequestV2MessageParam requestV2MessageParams, string oid, string value, CancellationToken cancellationToken)
        {
            List<Variable> list = new List<Variable> { new Variable(new ObjectIdentifier(oid)) };
            SetRequestMessage request = new SetRequestMessage(0, VersionCode.V2, new OctetString(requestV2MessageParams.Community), list);
            ISnmpMessage reply = await request.GetResponseAsync(new IPEndPoint(IPAddress.Parse(requestV2MessageParams.IpAddress), requestV2MessageParams.Port), cancellationToken);
            return reply;
        }
        #endregion


        #region SnmpV3Requests
        public ISnmpMessage GetSnmpV3Request(RequestV3MessageParam requestV3MessageParams, string oid)
        {
            IAuthenticationProvider auth = _authProviderFactory.CreateAuthProvider(requestV3MessageParams.AuthProvider, requestV3MessageParams.AuthPassword);
            IPrivacyProvider priv = _authProviderFactory.CreatePrivProvider(requestV3MessageParams.PrivProvider, requestV3MessageParams.PrivPassword, auth);


            Discovery discovery = Messenger.GetNextDiscovery(SnmpType.GetRequestPdu);
            ReportMessage report = discovery.GetResponse(requestV3MessageParams.DiscoveryTimeout, new IPEndPoint(IPAddress.Parse(requestV3MessageParams.IpAddress), requestV3MessageParams.Port));


            ISnmpMessage request = new GetRequestMessage(VersionCode.V3, Messenger.NextMessageId, Messenger.NextRequestId, new OctetString(requestV3MessageParams.Username)
                                                        , new OctetString(requestV3MessageParams.Context), new List<Variable> { new Variable(new ObjectIdentifier(oid)) }
                                                        , priv, Messenger.MaxMessageSize, report);


            ISnmpMessage reply = request.GetResponse(requestV3MessageParams.ResponseTimeout, new IPEndPoint(IPAddress.Parse(requestV3MessageParams.IpAddress), requestV3MessageParams.Port));
            return reply;
        }

        public async Task<ISnmpMessage> GetSnmpV3RequestAsync(RequestV3MessageParam requestV3MessageParams, string oid)
        {
            IAuthenticationProvider auth = _authProviderFactory.CreateAuthProvider(requestV3MessageParams.AuthProvider, requestV3MessageParams.AuthPassword);
            IPrivacyProvider priv = _authProviderFactory.CreatePrivProvider(requestV3MessageParams.PrivProvider, requestV3MessageParams.PrivPassword, auth);


            Discovery discovery = Messenger.GetNextDiscovery(SnmpType.GetRequestPdu);
            ReportMessage report = discovery.GetResponse(requestV3MessageParams.DiscoveryTimeout, new IPEndPoint(IPAddress.Parse(requestV3MessageParams.IpAddress), requestV3MessageParams.Port));

            
            ISnmpMessage request = new GetRequestMessage(VersionCode.V3, Messenger.NextMessageId, Messenger.NextRequestId, new OctetString(requestV3MessageParams.Username)
                                                                      , new OctetString(requestV3MessageParams.Context), new List<Variable> { new Variable(new ObjectIdentifier(oid)) }
                                                                      , priv, Messenger.MaxMessageSize, report);

            ISnmpMessage reply = await request.GetResponseAsync(new IPEndPoint(IPAddress.Parse(requestV3MessageParams.IpAddress), requestV3MessageParams.Port));
            return reply;
        }

        public async Task<ISnmpMessage> GetSnmpV3RequestAsync(RequestV3MessageParam requestV3MessageParams, string oid, CancellationToken cancellationToken)
        {
            IAuthenticationProvider auth = _authProviderFactory.CreateAuthProvider(requestV3MessageParams.AuthProvider, requestV3MessageParams.AuthPassword);
            IPrivacyProvider priv = _authProviderFactory.CreatePrivProvider(requestV3MessageParams.PrivProvider, requestV3MessageParams.PrivPassword, auth);


            Discovery discovery = Messenger.GetNextDiscovery(SnmpType.GetRequestPdu);
            ReportMessage report = discovery.GetResponse(requestV3MessageParams.DiscoveryTimeout, new IPEndPoint(IPAddress.Parse(requestV3MessageParams.IpAddress), requestV3MessageParams.Port));

            ISnmpMessage request = new GetRequestMessage(VersionCode.V3, Messenger.NextMessageId, Messenger.NextRequestId, new OctetString(requestV3MessageParams.Username)
                                                                      , new OctetString(requestV3MessageParams.Context), new List<Variable> { new Variable(new ObjectIdentifier(oid)) }
                                                                      , priv, Messenger.MaxMessageSize, report);

            ISnmpMessage reply = await request.GetResponseAsync(new IPEndPoint(IPAddress.Parse(requestV3MessageParams.IpAddress), requestV3MessageParams.Port), cancellationToken);
            return reply;
        }



        public ISnmpMessage SetSnmpV3Request(RequestV3MessageParam requestV3MessageParams, string oid, string value, string dataType)
        {
            ISnmpData dt = SnmpDataTypeBuilder.BuildSnmpDataType(dataType, value);

            IAuthenticationProvider auth = _authProviderFactory.CreateAuthProvider(requestV3MessageParams.AuthProvider, requestV3MessageParams.AuthPassword);
            IPrivacyProvider priv = _authProviderFactory.CreatePrivProvider(requestV3MessageParams.PrivProvider, requestV3MessageParams.PrivPassword, auth);


            Discovery discovery = Messenger.GetNextDiscovery(SnmpType.GetRequestPdu);
            ReportMessage report = discovery.GetResponse(requestV3MessageParams.DiscoveryTimeout, new IPEndPoint(IPAddress.Parse(requestV3MessageParams.IpAddress), requestV3MessageParams.Port));

            ISnmpMessage request = new SetRequestMessage(VersionCode.V3, Messenger.NextMessageId, Messenger.NextRequestId, new OctetString(requestV3MessageParams.Username)
                                        , new OctetString(requestV3MessageParams.Context), new List<Variable> { new Variable(new ObjectIdentifier(oid), dt) }
                                        , priv, Messenger.MaxMessageSize, report);

            ISnmpMessage reply = request.GetResponse(requestV3MessageParams.ResponseTimeout, new IPEndPoint(IPAddress.Parse(requestV3MessageParams.IpAddress), requestV3MessageParams.Port));
            return reply;
        }

        public async Task<ISnmpMessage> SetSnmpV3RequestAsync(RequestV3MessageParam requestV3MessageParams, string oid, string value)
        {
            IAuthenticationProvider auth = _authProviderFactory.CreateAuthProvider(requestV3MessageParams.AuthProvider, requestV3MessageParams.AuthPassword);
            IPrivacyProvider priv = _authProviderFactory.CreatePrivProvider(requestV3MessageParams.PrivProvider, requestV3MessageParams.PrivPassword, auth);


            Discovery discovery = Messenger.GetNextDiscovery(SnmpType.GetRequestPdu);
            ReportMessage report = await discovery.GetResponseAsync(new IPEndPoint(IPAddress.Parse(requestV3MessageParams.IpAddress), requestV3MessageParams.Port));

            ISnmpMessage request = new SetRequestMessage(VersionCode.V3, Messenger.NextMessageId, Messenger.NextRequestId, new OctetString(requestV3MessageParams.Username)
                                       , new OctetString(requestV3MessageParams.Context), new List<Variable> { new Variable(new ObjectIdentifier(oid), new OctetString(value)) }
                                       , priv, Messenger.MaxMessageSize, report);

            ISnmpMessage reply = request.GetResponse(requestV3MessageParams.ResponseTimeout, new IPEndPoint(IPAddress.Parse(requestV3MessageParams.IpAddress), requestV3MessageParams.Port));
            return reply;
        }

        public async Task<ISnmpMessage> SetSnmpV3RequestAsync(RequestV3MessageParam requestV3MessageParams, string oid, string value, CancellationToken cancellationToken)
        {
            IAuthenticationProvider auth = _authProviderFactory.CreateAuthProvider(requestV3MessageParams.AuthProvider, requestV3MessageParams.AuthPassword);
            IPrivacyProvider priv = _authProviderFactory.CreatePrivProvider(requestV3MessageParams.PrivProvider, requestV3MessageParams.PrivPassword, auth);


            Discovery discovery = Messenger.GetNextDiscovery(SnmpType.GetRequestPdu);
            ReportMessage report = await discovery.GetResponseAsync(new IPEndPoint(IPAddress.Parse(requestV3MessageParams.IpAddress), requestV3MessageParams.Port));

            ISnmpMessage request = new SetRequestMessage(VersionCode.V3, Messenger.NextMessageId, Messenger.NextRequestId, new OctetString(requestV3MessageParams.Username)
                                       , new OctetString(requestV3MessageParams.Context), new List<Variable> { new Variable(new ObjectIdentifier(oid), new OctetString(value)) }
                                       , priv, Messenger.MaxMessageSize, report);

            ISnmpMessage reply = await request.GetResponseAsync(new IPEndPoint(IPAddress.Parse(requestV3MessageParams.IpAddress), requestV3MessageParams.Port), cancellationToken);
            return reply;
        }

        #endregion
    }
}
