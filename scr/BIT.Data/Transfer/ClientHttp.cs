using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BIT.Data.Transfer
{
    //HACK good post about httpclients https://stackoverflow.com/questions/4015324/how-to-make-an-http-post-web-request
    public class ClientHttp : ClientBase
    {
        private readonly HttpClient client;
        public string Url { get; private set; }
        public ClientHttp(string Url,string Token)
        {
            this.Url = Url;
            this.client = new HttpClient();
            client.DefaultRequestHeaders.Authorization
                        = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
        }
        async void  PostData(Dictionary<string, string> values)
        {

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(Url, content);

            var responseString = await response.Content.ReadAsStringAsync();
        }
        public override IResult ExecuteFunction(IParams Parameters)
        {
            throw new NotImplementedException();
        }

        public override void ExecuteMethod(IParams Parameters)
        {
            throw new NotImplementedException();
        }
    }
}
