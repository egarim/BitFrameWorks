using Newtonsoft.Json;
using RestClient.Net.Abstractions;
using System;
using System.Text;

namespace BIT.Data.Transfer.RestClientNet
{
    public class NewtonsoftSerializationAdapter : ISerializationAdapter
    {

        #region Implementation
        public TResponseBody Deserialize<TResponseBody>(byte[] data, IHeadersCollection responseHeaders)
        {
            var markup = Encoding.UTF8.GetString(data);
            object markupAsObject = markup;

            if (typeof(TResponseBody) == typeof(string))
            {
                return (TResponseBody)markupAsObject;
            }

            return JsonConvert.DeserializeObject<TResponseBody>(markup);
        }

        public byte[] Serialize<TRequestBody>(TRequestBody value, IHeadersCollection requestHeaders)
        {
            var json = JsonConvert.SerializeObject(value);
            var binary = Encoding.UTF8.GetBytes(json);
            return binary;
        }
        #endregion
    }
}
