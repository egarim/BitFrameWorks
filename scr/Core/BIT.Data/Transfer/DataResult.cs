using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BIT.Data.Transfer
{
    public class DataResult :  IDataResult
    {
        public string ResultValue2 { get; set; }
        public DateTime DateUtc { get; private set; }
        public IList<string> Errors { get; set; }
        public byte[] ResultValue  { get; set; }
        public DataResult(DateTime dateUtc, IList<string> errors, byte[] resultValue)
        {
            DateUtc = dateUtc;
            Errors = errors;
            ResultValue = resultValue;
        }
        public DataResult(IList<string> errors, byte[] resultValue)
        {
            Errors = errors;
            ResultValue = resultValue;
            DateUtc = DateTime.UtcNow;
        }
        public DataResult()
        {
        }
        //[JsonIgnore()]
        ///*public IList<string> Errors*/
        //{
        //    get
        //    {
        //        if (this.ContainsKey(nameof(Errors)))
        //            return this[nameof(Errors)] as IList<string>;
        //        else
        //            return default(IList<string>);

        //    }
        //    set
        //    {
        //        if (!this.ContainsKey(nameof(Errors)))
        //            this.Add(nameof(Errors), value);
        //        else
        //            this[nameof(Errors)] = value;

        //    }
        //}
        //[JsonIgnore()]
        //public byte[] ResultValue
        //{
        //    get
        //    {
        //        if (this.ContainsKey(nameof(ResultValue)))
        //            return this[nameof(ResultValue)] as byte[];
        //        else
        //            return Array.Empty<byte>();

        //    }
        //    set
        //    {
        //        if (!this.ContainsKey(nameof(ResultValue)))
        //            this.Add(nameof(ResultValue), value);
        //        else
        //            this[nameof(ResultValue)] = value;

        //    }
        //}

    }
}
