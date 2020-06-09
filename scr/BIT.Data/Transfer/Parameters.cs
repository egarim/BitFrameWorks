using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BIT.Data.Transfer
{
    public class Parameters : Dictionary<string, object>, IParams
    {

        [JsonIgnore()]
        public string MemberName
        {
            get
            {
                if (this.ContainsKey(nameof(MemberName)))
                    return this[nameof(MemberName)]?.ToString();
                else
                    return string.Empty;

            }
            set
            {
                if (!this.ContainsKey(nameof(MemberName)))
                    this.Add(nameof(MemberName), value);
                else
                    this[nameof(MemberName)] = value;

            }
        }
        [JsonIgnore()]
        public byte[] ParametersValue
        {
            get
            {
                if (this.ContainsKey(nameof(ParametersValue)))
                    return this[nameof(ParametersValue)] as byte[];
                else
                    return Array.Empty<byte>();

            }
            set
            {
                if (!this.ContainsKey(nameof(ParametersValue)))
                    this.Add(nameof(ParametersValue), value);
                else
                    this[nameof(ParametersValue)] = value;

            }
        }

    }
}
