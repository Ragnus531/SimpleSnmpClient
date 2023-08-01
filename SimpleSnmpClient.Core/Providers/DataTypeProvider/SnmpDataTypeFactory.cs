using Lextm.SharpSnmpLib;
using SimpleSnmpClient.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnmpClient.Core.Providers.DataTypeProvider
{
    //TODO: Apex Legends more important == change to interface and put that into DI later one
    public static class SnmpDataTypeBuilder
    {
        public static ISnmpData BuildSnmpDataType(string dataType, string data)
        {
            ISnmpData snmpData = null;
            switch(dataType)
            {
                case "OctetString":
                    snmpData = CreateOctetString(data);
                    break;
                case "Counter32":
                    snmpData = CreateCounter32(long.Parse(data));
                    break;
                case "Counter64":
                    snmpData = CreateCounter64(ulong.Parse(data));
                    break;
                case "Gauge32":
                    snmpData =  CreateGauge32(long.Parse(data));
                    break;
                case "Unsigned32":
                    snmpData = CreateUnsigned32(long.Parse(data));
                    break;
                case "Integer32":
                    snmpData = CreateInteger32(int.Parse(data));
                    break;
            }
            return snmpData;
        }

        private static ISnmpData CreateCounter32(long value)
        {
            return new Counter32(value);
        }

        private static ISnmpData CreateCounter64(ulong value)
        {
            return new Counter64(value);
        }

        private static ISnmpData CreateGauge32(long value)
        {
            return new Gauge32(value);
        }

        private static ISnmpData CreateUnsigned32(long value)
        {
            return new Gauge32(value); // IMPORTANT: return Gauge32 for Unsigned32 case as workaround of RFC 1442 time entities.
        }

        private static ISnmpData CreateInteger32(int value)
        {
            return new Integer32(value);
        }

        private static ISnmpData CreateOctetString(string value)
        {
            return new OctetString(value);
        }

        private static ISnmpData CreateTimeTicks(uint value)
        {
            return new TimeTicks(value);
        }
    }
}
