using BIT.Data.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BIT.Data.DataTransfer
{
    //HACK good post about httpclients https://stackoverflow.com/questions/4015324/how-to-make-an-http-post-web-request
    public class FunctionClientHttp : IFunction
    {
        private readonly HttpClient client;
        public string Url { get; private set; }
        public IStringSerializationService StringSerializationHelper { get; private set; }
        public FunctionClientHttp(string Url,string Token, IStringSerializationService stringSerializationHelper)
        {
            this.Url = Url;
            this.client = new HttpClient();
            this.StringSerializationHelper = stringSerializationHelper;
            client.DefaultRequestHeaders.Authorization
                        = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
        }
        IDataResult PostData(IDataParameters Parameters)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("Parameters", StringSerializationHelper.SerializeObjectToString<IDataParameters>(Parameters));
            var content = new FormUrlEncodedContent(values);

            var response =  client.PostAsync(Url, content).GetAwaiter().GetResult();

            var responseString =  response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return StringSerializationHelper.DeserializeObjectFromString<DataResult>(responseString);
        }
        public virtual  IDataResult ExecuteFunction(IDataParameters Parameters)
        {
            return  PostData(Parameters);
        }

     
    }
}
