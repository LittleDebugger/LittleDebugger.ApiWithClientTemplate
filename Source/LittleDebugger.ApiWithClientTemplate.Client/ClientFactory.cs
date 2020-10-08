using LittleDebugger.ApiWithClientTemplate.Client.Contracts;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LittleDebugger.ApiWithClientTemplate.Client
{
    public static class ClientFactory
    {
        public static IExampleClient CreateExampleClient(string baseUrl, HttpClient http, Func<Task<string>> retrieveAuthorizationToken)
        {
            return new ExampleClient(baseUrl, http)
            {
                RetrieveAuthorizationToken = retrieveAuthorizationToken
            };
        }
    }
}
