using BIT.Data.Functions;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BIT.Data.Functions.RestClientNet
{
    /// <summary>
    /// Invokes an implementation of IFunction on the server side by using RestClientNet
    /// </summary>
    public class ApiFunction : IFunction, IFunctionAsync
    {
        Client client;
        string Url;
        Uri resource;


        /// <summary>
        /// Initializes a new instance of the ApiFunction class that uses your instance of RestClient
        /// </summary>
        /// <param name="client">An instance of RestClientNet</param>
        /// <param name="url">Api Url</param>
        /// <param name="headers">Additional headers for the api request</param>
        public ApiFunction(Client client, string url, IDictionary<string, string> headers)
        {
            this.client = client;
            this.Url = url;
            resource = new Uri(Url);
            HeadersData = headers;
         
        }
        /// <summary>
        /// Initializes a new instance of the ApiFunction class with default settings.
        /// </summary>
        /// <param name="url">Api Url</param>
        /// <param name="serializationAdapter">An implementation of ISerializationAdapter</param>
        /// <param name="headers">Additional headers for the api request</param>
        public ApiFunction(string url, ISerializationAdapter serializationAdapter, IDictionary<string, string> headers) 
            : this(new Client(serializationAdapter),url,headers)
        {
                    
        }
        IDictionary<string, string> HeadersData;
        RequestHeadersCollection RequestHeders;

        /// <summary>
        /// Build the request headers, override this function if you want to manipulate the content of the headers
        /// </summary>
        /// <returns>Defaults headers including the custom headers passed in this class constructor</returns>
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

        /// <summary>
        /// Executes the funtion asynchronous with the specified parameters
        /// </summary>
        /// <param name="Parameters">An implementation of IDataParameters</param>
        /// <returns>A task with the result of the function</returns>
        public async Task<IDataResult> ExecuteFunctionAsync(IDataParameters Parameters)
        {
            var result = await client.PostAsync<DataResult, IDataParameters>(Parameters, resource, GetHeaders());

            return result.Body;

          

        }
        /// <summary>
        /// Executes the funtion synchronous with the specified parameters
        /// </summary>
        /// <param name="Parameters">An implementation of IDataParameters</param>
        /// <returns>The result of the function</returns>
        public IDataResult ExecuteFunction(IDataParameters Parameters)
        {
            var TaskValue = Task.Run(async () => await ExecuteFunctionAsync(Parameters));
            TaskValue.Wait();
            return TaskValue.Result;
        }
    }
}
