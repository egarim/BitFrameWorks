using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BIT.Data.DataTransfer
{

    public class DataResult : IDataResult
    {
        public DataResult()
        {
            this.Errors = new Dictionary<string, string>();
            this.AdditionalValues = new Dictionary<string, object>();
            this.DateUtc = DateTime.UtcNow;
        }
        public DateTime DateUtc { get; set; }
        public IDictionary<string,string> Errors { get; set; }
     
        public string MemberName { get; set; }
        public byte[] ResultValue { get; set; }
        public IDictionary<string, object> AdditionalValues { get; set; }
    
    }
}
