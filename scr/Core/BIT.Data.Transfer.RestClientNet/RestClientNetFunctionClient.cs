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

            resource = new Uri(Url);
            if (headers != null)
                InitHearders(headers);
        }
        public RestClientNetFunctionClient(string url, ISerializationAdapter serializationAdapter, IDictionary<string, string> headers)
        {
            this.Url = url;
            this.client = new Client(serializationAdapter);
            resource = new Uri(Url);
            if (headers != null)
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


        public async Task<IDataResult> ExecuteFunctionAsync(IDataParameters Parameters)
        {

            //https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#2xx_Success
            try
            {
                //TODO Joche continue here
                //TODO there is a design error on the client, I put a ticket here https://github.com/MelbourneDeveloper/RestClient.Net/issues/68
                //I will just handle the exception and thrown my own exception 
               

                var result = await client.PostAsync<DataResult, IDataParameters>(Parameters, resource, Headers);


                return result.Body;
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem in the api call:" + ex.Message + " " + ex.StackTrace);
            }






            ////https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#2xx_Success
            //try
            //{
            //    //TODO Joche continue here
            //    //TODO there is a design error on the client, I put a ticket here https://github.com/MelbourneDeveloper/RestClient.Net/issues/68
            //    //I will just handle the exception and thrown my own exception 
            //    client.ThrowExceptionOnFailure = false;

            //    var result = await client.PostAsync<DataResult, IDataParameters>(Parameters, resource, Headers);



            //    if (result.StatusCode >= 1 && result.StatusCode <= 199)
            //    {
            //        throw new Exception($"status code {result.StatusCode}");
            //    }
            //    if (result.StatusCode >= 300 && result.StatusCode <= 400)
            //    {
            //        throw new Exception($"status code {result.StatusCode}");
            //    }
            //    if (result.StatusCode >= 401 && result.StatusCode <= 500)
            //    {
            //        throw new Exception($"status code {result.StatusCode}");
            //    }
            //    if (result.StatusCode >= 501 && result.StatusCode <= 600)
            //    {
            //        throw new Exception($"status code {result.StatusCode}");
            //    }
            //    return result.Body;
            //}
            //catch (Exception ex)
            //{
            //    var exm = ex.Message;
            //    throw;
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
