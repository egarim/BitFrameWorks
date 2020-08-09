using BIT.Data.DataTransfer;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BIT.Data.Transfer.RestClientNet
{
    public class FunctionClient : IFunction, IFunctionAsync
    {
        Client client;
        string Url;
        Uri resource;
        //RequestHeadersCollection Headers;

        public FunctionClient(Client client, string url, IDictionary<string, string> headers)
        {
            this.client = client;
            this.Url = url;
            resource = new Uri(Url);
            HeadersData = headers;
         
        }
       
        public FunctionClient(string url, ISerializationAdapter serializationAdapter, IDictionary<string, string> headers)
        {
            this.Url = url;
            this.client = new Client(serializationAdapter);
            resource = new Uri(Url);

            HeadersData = headers;
          
        }
        IDictionary<string, string> HeadersData;
        RequestHeadersCollection RequestHeders;
        protected virtual RequestHeadersCollection GetHeaders()
        {

            if (RequestHeders != null)
                return RequestHeders;

            RequestHeders = new RequestHeadersCollection();
            if (HeadersData == null)
                return RequestHeders;

            foreach (KeyValuePair<string, string> Current in HeadersData)
            {
                if (!RequestHeders.Contains(Current.Key))
                {
                    RequestHeders.Add(Current.Key, Current.Value);
                }

            }
            return RequestHeders;
        }


        public async Task<IDataResult> ExecuteFunctionAsync(IDataParameters Parameters)
        {
            var result = await client.PostAsync<DataResult, IDataParameters>(Parameters, resource, GetHeaders());

            return result.Body;

            ////https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#2xx_Success

        }
        public IDataResult ExecuteFunction(IDataParameters Parameters)
        {
            var TaskValue = Task.Run(async () => await ExecuteFunctionAsync(Parameters));
            TaskValue.Wait();
            return TaskValue.Result;
        }
    }
}
