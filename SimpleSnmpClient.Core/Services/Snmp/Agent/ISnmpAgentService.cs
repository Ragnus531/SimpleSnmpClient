using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using SimpleSnmpClient.Core.DTO;
using SimpleSnmpClient.Core.Providers.Requests.RequestsParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnmpClient.Core.Services.Snmp.Agent
{
    public interface ISnmpAgentService
    {
        SnmpPayLoadDto GetSnmpV2Request(RequestV2MessageParam requestV2MessageParams, string oid);
        SnmpPayLoadDto SetSnmpV2Request(RequestV2MessageParam requestV2MessageParams, string oid, string value,string dataType);
        SnmpPayLoadDto GetSnmpV3Request(RequestV3MessageParam requestV3MessageParams, string oid);
        SnmpPayLoadDto SetSnmpV3Request(RequestV3MessageParam requestV3MessageParams, string oid, string value, string dataType);

        Task<SnmpPayLoadDto> GetSnmpV2RequestAsync(RequestV2MessageParam requestV2MessageParams, string oid);
        Task<SnmpPayLoadDto> GetSnmpV2RequestAsync(RequestV2MessageParam requestV2MessageParams, string oid, CancellationToken cancellationToken);

        Task<SnmpPayLoadDto> SetSnmpV2RequestAsync(RequestV2MessageParam requestV2MessageParams, string oid, string value);
        Task<SnmpPayLoadDto> SetSnmpV2RequestAsync(RequestV2MessageParam requestV2MessageParams, string oid, string value, CancellationToken cancellationToken);
        Task<SnmpPayLoadDto> GetSnmpV3RequestAsync(RequestV3MessageParam requestV3MessageParams, string oid);
        Task<SnmpPayLoadDto> GetSnmpV3RequestAsync(RequestV3MessageParam requestV3MessageParams, string oid, CancellationToken cancellationToken);
        Task<SnmpPayLoadDto> SetSnmpV3RequestAsync(RequestV3MessageParam requestV3MessageParams, string oid, string value);
        Task<SnmpPayLoadDto> SetSnmpV3RequestAsync(RequestV3MessageParam requestV3MessageParams, string oid, string value, CancellationToken cancellationToken);

        SnmpPayLoadDto Map(ISnmpMessage meesage);
    }
}
