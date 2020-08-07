using BIT.Data.DataTransfer;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BIT.Data.Transfer.RestClientNet
{
    public class RestClientNetFunctionClient : IFunction, IFunctionAsync
    {
        Client client;
        string Url;
        Uri resource;
        //RequestHeadersCollection Headers;

        public RestClientNetFunctionClient(Client client, string url, IDictionary<string, string> headers)
        {
            this.client = client;
            this.Url = url;
            resource = new Uri(Url);
            Headers = headers;
            //if (headers != null)
            //    InitHearders(headers);

        }
        //public RestClientNetFunctionClient(string url, IDictionary<string, string> headers)
        //{
        //    this.Url = url;
        //    this.client = new Client(new NewtonsoftSerializationAdapter());

        //    resource = new Uri(Url);
        //    if (headers != null)
        //        InitHearders(headers);
        //}
        public RestClientNetFunctionClient(string url, ISerializationAdapter serializationAdapter, IDictionary<string, string> headers)
        {
            this.Url = url;
            this.client = new Client(serializationAdapter);
            resource = new Uri(Url);

            Headers = headers;
            //if (headers != null)
            //    InitHearders(headers);
        }
        IDictionary<string, string> Headers;
        private RequestHeadersCollection GetHeaders()
        {


            var NewHeaders = new RequestHeadersCollection();
            if (Headers == null)
                return NewHeaders;

            foreach (KeyValuePair<string, string> Current in Headers)
            {
                if (!NewHeaders.Contains(Current.Key))
                {
                    NewHeaders.Add(Current.Key, Current.Value);
                }

            }
            return NewHeaders;
        }


        public async Task<IDataResult> ExecuteFunctionAsync(IDataParameters Parameters)
        {
            var result = await client.PostAsync<DataResult, IDataParameters>(Parameters, resource, GetHeaders());

            return result.Body;





            ////https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#2xx_Success
            //try
            //{
            //    //TODO Joche continue here
            //    //TODO there is a design error on the client, I put a ticket here https://github.com/MelbourneDeveloper/RestClient.Net/issues/68
            //    //I will just handle the exception and thrown my own exception 


            //    var result = await client.PostAsync<DataResult, IDataParameters>(Parameters, resource, Headers);


            //    return result.Body;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("There was a problem in the api call:" + ex.Message + " " + ex.StackTrace);
            //}
        }
        public IDataResult ExecuteFunction(IDataParameters Parameters)
        {
            var TaskValue = Task.Run(async () => await ExecuteFunctionAsync(Parameters));
            TaskValue.Wait();
            return TaskValue.Result;
        }
    }
}
