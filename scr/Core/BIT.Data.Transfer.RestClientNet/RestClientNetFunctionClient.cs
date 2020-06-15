using BIT.Data.DataTransfer;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BIT.Data.Transfer.RestClientNet
{
    public class RestClientNetFunctionClient : IFunction
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
            if (headers != null) 
                InitHearders(headers);

        }
        public RestClientNetFunctionClient(string url, IDictionary<string, string> headers)
        {
            this.Url = url;
            this.client = new Client(new NewtonsoftSerializationAdapter());
            InitHearders(headers);
        }
        public RestClientNetFunctionClient(string url, ISerializationAdapter serializationAdapter, IDictionary<string, string> headers)
        {
            this.Url = url;
            this.client = new Client(serializationAdapter);
            InitHearders(headers);
        }
        private void InitHearders(IDictionary<string, string> headers)
        {
            Headers = new RequestHeadersCollection();
            foreach (KeyValuePair<string, string> Current in headers)
            {
                Headers.Add(Current.Key, Current.Value);
            }
        }

     
        public async Task<IDataResult> ExecuteFunction(IDataParameters Parameters)
        {

            Response<DataResult> Result = await client.PostAsync<DataResult, IDataParameters>(Parameters, resource, Headers);
            return Result.Body;
        }
    }
}
