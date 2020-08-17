using Google.Protobuf;
using RestClient.Net.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Data.Functions.RestClientNet
{
    public class ProtobufSerializationAdapter : ISerializationAdapter
    {
        public byte[] Serialize<TRequestBody>(TRequestBody value, IHeadersCollection requestHeaders)
        {
            var message = (IMessage)value as IMessage;
            if (message == null) throw new Exception("The object is not a Google Protobuf Message");
            return message.ToByteArray();
        }
        public TResponseBody Deserialize<TResponseBody>(Response response)
        {
            if (response.StatusCode == 200)
                return this.Deserialize<TResponseBody>(response.GetResponseData(), response.Headers);
            else
                throw new Exception($"the request was not succesfull the status code returned was {response.StatusCode}");
        }
        public TResponseBody Deserialize<TResponseBody>(byte[] data, IHeadersCollection responseHeaders)
        {
            var messageType = typeof(TResponseBody);
            var parserProperty = messageType.GetProperty("Parser");
            var parser = parserProperty.GetValue(parserProperty);
            var parseFromMethod = parserProperty.PropertyType.GetMethod("ParseFrom", new Type[] { typeof(byte[]) });
            var parsedObject = parseFromMethod.Invoke(parser, new object[] { data });
            return (TResponseBody)parsedObject;
        }

       
    }
}
