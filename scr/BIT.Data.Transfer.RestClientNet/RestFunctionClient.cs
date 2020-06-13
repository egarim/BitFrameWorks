using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Threading.Tasks;

namespace BIT.Data.Transfer.RestClientNet
{
    public class RestFunctionClient : IFunctionClient
    {
        Client client;
        string Url;
        Uri resource;
        public RestFunctionClient(Client client, string url)
        {
            this.client = client;
            this.Url = url;
            resource = new Uri(Url);
        }
        public RestFunctionClient(string url)
        {
            this.Url = url;
            this.client = new Client(new NewtonsoftSerializationAdapter());
        }
        public RestFunctionClient(string url, ISerializationAdapter serializationAdapter)
        {
            this.Url = url;
            this.client = new Client(serializationAdapter);
        }
        public async Task<IDataResult> ExecuteFunction(IDataParameters Parameters)
        {


          
            Response<DataResult> Result = await client.PostAsync<DataResult, IDataParameters>(Parameters, resource);
            return Result.Body;
        }
    }
}
