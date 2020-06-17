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

        public TResponseBody Deserialize<TResponseBody>(Response response)
        {
            if (response.StatusCode == 200)
                return this.Deserialize<TResponseBody>(response.GetResponseData(), response.Headers);
            else
                throw new Exception($"the request was not succesfull the status code returned was {response.StatusCode}");
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
