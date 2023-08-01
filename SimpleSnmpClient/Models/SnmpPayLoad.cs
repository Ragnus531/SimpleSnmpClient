using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnmpClient.Models
{
    public class SnmpPayLoad
    {
        public string? Oid { get; set; }
        public string? Value { get; set; }

        public string DataPresented
        {
            get 
            {
                return string.Join(":", new List<string>() { Oid, Value }.Where(a => !string.IsNullOrEmpty(a)).ToList());
                //if(Oid == null || string.IsNullOrEmpty(Oid))
                //    return Value; 
                //else (Value == null || string.IsNullOrEmpty(Value))
                //    return Oid;
                //else 

            }
        }

        public SnmpPayLoad(string? oid, string? value)
        {
            Oid = oid;
            Value = value;
        }

        public SnmpPayLoad()
        {

        }
    }
}
