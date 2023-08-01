using Lextm.SharpSnmpLib.Messaging;
using SimpleSnmpClient.Core.Providers.Requests.RequestsParameters;

namespace SimpleSnmpClient.Core.Providers.Requests
{
    public interface ISnmpRequest
    {
        ISnmpMessage GetSnmpV2Request(RequestV2MessageParam requestV2MessageParams, string oid);
        ISnmpMessage SetSnmpV2Request(RequestV2MessageParam requestV2MessageParams, string oid,string value, string dataType);
        ISnmpMessage GetSnmpV3Request(RequestV3MessageParam requestV3MessageParams, string oid);
        ISnmpMessage SetSnmpV3Request(RequestV3MessageParam requestV3MessageParams, string oid,string value, string dataType);

        Task<ISnmpMessage> GetSnmpV2RequestAsync(RequestV2MessageParam requestV2MessageParams, string oid);
        Task<ISnmpMessage> GetSnmpV2RequestAsync(RequestV2MessageParam requestV2MessageParams, string oid,CancellationToken cancellationToken);

        Task<ISnmpMessage> SetSnmpV2RequestAsync(RequestV2MessageParam requestV2MessageParams, string oid, string value);
        Task<ISnmpMessage> SetSnmpV2RequestAsync(RequestV2MessageParam requestV2MessageParams, string oid, string value, CancellationToken cancellationToken);
        Task<ISnmpMessage> GetSnmpV3RequestAsync(RequestV3MessageParam requestV3MessageParams, string oid);
        Task<ISnmpMessage> GetSnmpV3RequestAsync(RequestV3MessageParam requestV3MessageParams, string oid, CancellationToken cancellationToken);
        Task<ISnmpMessage> SetSnmpV3RequestAsync(RequestV3MessageParam requestV3MessageParams, string oid, string value);
        Task<ISnmpMessage> SetSnmpV3RequestAsync(RequestV3MessageParam requestV3MessageParams, string oid, string value, CancellationToken cancellationToken);
    }
}
