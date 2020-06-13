using BIT.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BIT.Data.Transfer
{
    //HACK good post about httpclients https://stackoverflow.com/questions/4015324/how-to-make-an-http-post-web-request
    public class FunctionClientHttp : IFunctionClient
    {
        private readonly HttpClient client;
        public string Url { get; private set; }
        public IStringSerializationHelper StringSerializationHelper { get; private set; }
        public FunctionClientHttp(string Url,string Token,IStringSerializationHelper stringSerializationHelper)
        {
            this.Url = Url;
            this.client = new HttpClient();
            this.StringSerializationHelper = stringSerializationHelper;
            client.DefaultRequestHeaders.Authorization
                        = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
        }
        async Task<IDataResult>  PostData(IDataParameters Parameters)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("Parameters", StringSerializationHelper.SerializeObjectToString<IDataParameters>(Parameters));
            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(Url, content);

            var responseString = await response.Content.ReadAsStringAsync();
            return StringSerializationHelper.DeserializeObjectFromString<DataResult>(responseString);
        }
        public virtual async Task<IDataResult> ExecuteFunction(IDataParameters Parameters)
        {
            return await PostData(Parameters);
        }

     
    }
}
