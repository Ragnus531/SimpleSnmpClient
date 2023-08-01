using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using SimpleSnmpClient.Core.DTO;
using SimpleSnmpClient.Core.Providers.Requests;
using SimpleSnmpClient.Core.Providers.Requests.RequestsParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnmpClient.Core.Services.Snmp.Agent
{
    public class SnmpAgentService : ISnmpAgentService
    {
        private readonly ISnmpRequest _snmpRequest;

        public SnmpAgentService(ISnmpRequest snmpRequest)
        {
            _snmpRequest = snmpRequest;
        }

        public SnmpPayLoadDto GetSnmpV2Request(RequestV2MessageParam requestV2MessageParams, string oid)
        {
            var snmpMessage = _snmpRequest.GetSnmpV2Request(requestV2MessageParams, oid);
            return Map(snmpMessage);
        }

        public async Task<SnmpPayLoadDto> GetSnmpV2RequestAsync(RequestV2MessageParam requestV2MessageParams, string oid)
        {
            var snmpMessage = await _snmpRequest.GetSnmpV2RequestAsync(requestV2MessageParams, oid);
            return Map(snmpMessage);
        }

        public async Task<SnmpPayLoadDto> GetSnmpV2RequestAsync(RequestV2MessageParam requestV2MessageParams, string oid, CancellationToken cancellationToken)
        {
            var snmpMessage = await _snmpRequest.GetSnmpV2RequestAsync(requestV2MessageParams, oid, cancellationToken);
            return Map(snmpMessage);
        }

        public SnmpPayLoadDto GetSnmpV3Request(RequestV3MessageParam requestV3MessageParams, string oid)
        {
            var snmpMessage = _snmpRequest.GetSnmpV3Request(requestV3MessageParams, oid);
            return Map(snmpMessage);
        }

        public async Task<SnmpPayLoadDto> GetSnmpV3RequestAsync(RequestV3MessageParam requestV3MessageParams, string oid)
        {
            var snmpMessage = await _snmpRequest.GetSnmpV3RequestAsync(requestV3MessageParams, oid);
            return Map(snmpMessage);
        }

        public async Task<SnmpPayLoadDto> GetSnmpV3RequestAsync(RequestV3MessageParam requestV3MessageParams, string oid, CancellationToken cancellationToken)
        {
            var snmpMessage = await _snmpRequest.GetSnmpV3RequestAsync(requestV3MessageParams, oid, cancellationToken);
            return Map(snmpMessage);
        }

       

        public SnmpPayLoadDto SetSnmpV2Request(RequestV2MessageParam requestV2MessageParams, string oid, string value, string dataType)
        {
            var snmpMessage = _snmpRequest.SetSnmpV2Request(requestV2MessageParams, oid, value, dataType);
            return Map(snmpMessage);
        }

        public async Task<SnmpPayLoadDto> SetSnmpV2RequestAsync(RequestV2MessageParam requestV2MessageParams, string oid, string value)
        {
            var snmpMessage = await _snmpRequest.SetSnmpV2RequestAsync(requestV2MessageParams, oid,value);
            return Map(snmpMessage);
        }

        public async Task<SnmpPayLoadDto> SetSnmpV2RequestAsync(RequestV2MessageParam requestV2MessageParams, string oid, string value, CancellationToken cancellationToken)
        {
            var snmpMessage = await _snmpRequest.SetSnmpV2RequestAsync(requestV2MessageParams, oid, value,cancellationToken);
            return Map(snmpMessage);
        }

        public SnmpPayLoadDto SetSnmpV3Request(RequestV3MessageParam requestV3MessageParams, string oid, string value, string dataType)
        {
            var snmpMessage = _snmpRequest.SetSnmpV3Request(requestV3MessageParams, oid,value,dataType);
            return Map(snmpMessage);
        }

        public async Task<SnmpPayLoadDto> SetSnmpV3RequestAsync(RequestV3MessageParam requestV3MessageParams, string oid, string value)
        {
            var snmpMessage = await _snmpRequest.SetSnmpV3RequestAsync(requestV3MessageParams, oid,value);
            return Map(snmpMessage);
        }

        public async Task<SnmpPayLoadDto> SetSnmpV3RequestAsync(RequestV3MessageParam requestV3MessageParams, string oid, string value, CancellationToken cancellationToken)
        {
            var snmpMessage = await _snmpRequest.SetSnmpV3RequestAsync(requestV3MessageParams, oid, value,cancellationToken);
            return Map(snmpMessage);
        }

        public SnmpPayLoadDto Map(ISnmpMessage meesage)
        {
            IList<Variable> variables = meesage.Variables();
            var snmpPayloadDtos = variables.Select(a => new SnmpPayLoadDto(a.Id.ToString(), a.Data.ToString())).ToList();
            return snmpPayloadDtos.FirstOrDefault();
        }
    }
}
