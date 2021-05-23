using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace BIT.Data.Functions.RestClientNet
{
    public class HttpClientFunction : IFunction, IFunctionAsync
    {
        public HttpClient Client { get; }
        public string Url { get; }

        /// <summary>
        /// Initializes a new instance of the ApiFunction class that uses your instance of RestClient
        /// </summary>
        /// <param name="client">An instance of RestClientNet</param>
        /// <param name="url">Api Url</param>
        /// <param name="headers">Additional headers for the api request</param>
        public HttpClientFunction(HttpClient client, string url, IDictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            Client = client;
            Url = url;
        }

        /// <summary>
        /// Initializes a new instance of the ApiFunction class with default settings.
        /// </summary>
        /// <param name="url">Api Url</param>
        /// <param name="serializationAdapter">An implementation of ISerializationAdapter</param>
        /// <param name="headers">Additional headers for the api request</param>
        public HttpClientFunction(string url, IDictionary<string, string> headers)
            : this(new HttpClient(), url, headers) { }

        /// <summary>
        /// Executes the funtion asynchronous with the specified parameters
        /// </summary>
        /// <param name="Parameters">An implementation of IDataParameters</param>
        /// <returns>A task with the result of the function</returns>
        public async Task<IDataResult> ExecuteFunctionAsync(IDataParameters Parameters, CancellationToken cancellationToken = default)
        {
            var myContent = JsonConvert.SerializeObject(Parameters);
            var sc = new StringContent(myContent, System.Text.Encoding.UTF8, "application/json");
            var result = await Client.PostAsync(Url, sc, cancellationToken);
            result.EnsureSuccessStatusCode();
            var body = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DataResult>(body);
        }

        /// <summary>
        /// Executes the funtion synchronous with the specified parameters
        /// </summary>
        /// <param name="Parameters">An implementation of IDataParameters</param>
        /// <returns>The result of the function</returns>
        public IDataResult ExecuteFunction(IDataParameters Parameters)
            => throw new PlatformNotSupportedException();
    }
}
