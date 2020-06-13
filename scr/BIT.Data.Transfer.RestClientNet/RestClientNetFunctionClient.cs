using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BIT.Data.Transfer.RestClientNet
{
    public class RestClientNetFunctionClient : IFunctionClient
    {
        Client client;
        string Url;
        Uri resource;
        RequestHeadersCollection Headers;

        public RestClientNetFunctionClient(Client client, string url, IDictionary<string, string> headers)
        {
            this.client = client;
            this.Url = url;
            resource = new Uri(Url);
            Headers = new RequestHeadersCollection();
            foreach (KeyValuePair<string, string> Current in headers)
            {
                Headers.Add(Current.Key, Current.Value);
            }
         
        }
        public RestClientNetFunctionClient(string url)
        {
            this.Url = url;
            this.client = new Client(new NewtonsoftSerializationAdapter());
        }
        public RestClientNetFunctionClient(string url, ISerializationAdapter serializationAdapter)
        {
            this.Url = url;
            this.client = new Client(serializationAdapter);
        }
        public async Task<IDataResult> ExecuteFunction(IDataParameters Parameters)
        {


          
            Response<DataResult> Result = await client.PostAsync<DataResult, IDataParameters>(Parameters, resource, Headers);
            return Result.Body;
        }
    }
}
