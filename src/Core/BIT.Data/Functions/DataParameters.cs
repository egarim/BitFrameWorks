using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BIT.Data.Functions
{
    public class DataParameters : IDataParameters
    {
        public string MemberName { get; set; }
        public byte[] ParametersValue { get; set; }
        public DateTime DateUtc { get; set; }
        public IDictionary<string, object> AdditionalValues { get; set; }
        public DataParameters()
        {
            DateUtc = DateTime.UtcNow;
            this.AdditionalValues = new Dictionary<string, object>();
        }
    }
}
