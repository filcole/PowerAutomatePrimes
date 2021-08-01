using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PrimeCount.Tests
{
    internal class TestRequest : PrimeCount.IScriptContext
    {
        public string CorrelationId => throw new NotImplementedException();

        public string OperationId => throw new NotImplementedException();

        public HttpRequestMessage Request => new HttpRequestMessage
        {
            Content = new StringContent("{ UpTo: 100}")
        };

        public ILogger Logger => throw new NotImplementedException();

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class TestResponse
    {
        [Fact]
        public void ValidRequest_shouldgive_ValidResponse()
        {
            var myScript = new Script
            {
                Context = new TestRequest()
            };

            var response = myScript.ExecuteAsync().Result;

            Assert.Equal("{\"NumPrimes\":25}", response.Content.ReadAsStringAsync().Result);
        }
    }
}