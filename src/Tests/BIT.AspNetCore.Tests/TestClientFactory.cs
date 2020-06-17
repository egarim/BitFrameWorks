using RestClient.Net.Abstractions;
using System;
using System.Net.Http;

namespace BIT.AspNetCore.Tests
{
    public class TestClientFactory : IHttpClientFactory, RestClient.Net.IHttpClientFactory
    {
        HttpClient _testClient;

        public TestClientFactory(HttpClient testClient)
        {
            _testClient = testClient;
        }

        public TimeSpan Timeout { get => _testClient.Timeout; set => _testClient.Timeout = value; }

        public IHeadersCollection DefaultRequestHeaders { get; } = new RequestHeadersCollection();

        public HttpClient CreateClient(string name)
        {
            return _testClient;
        }

        public void Dispose()
        {
            _testClient.Dispose();
        }
    }
}