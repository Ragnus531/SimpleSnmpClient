using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnmpClient.Core.DTO
{
    public class SnmpProfileDto
    {
        // Snmp V2 
        public string V2IpAddress { get; set; }
        public int V2Port { get; set; }
        public string V2Community { get; set; }
        public int V2ResponseTimeout { get; set; }


        // Snmp V3
        public string V3IpAddress { get; set; }
        public int V3Port { get; set; }
        public string V3UserName { get; set; }
        public string V3Context { get; set; }
        public string V3AuthPassword { get; set; }
        public string V3AuthProvider { get; set; }
        public string V3PrivPassword { get; set; }
        public string V3PrivProvider { get; set; }
        public int V3DiscoveryTimeout { get; set; }
        public int V3ResponseTimeout { get; set; }

        // Get/Set
        public string GetOid { get; set; }
        public string SetOid { get; set; }
        public string SetValue { get; set; }

        public IEnumerable<SnmpPayLoadDto> Logs { get; set; }
    }
}
