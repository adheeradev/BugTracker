using System;
using System.Net.Http;

namespace BugTracker.Web.BugTrackerClient
{
    public class BugTrackerClient : IBugTrackerClient
    {
        private readonly string _baseUri;

        public BugTrackerClient(string baseUri)
        {
            _baseUri = baseUri;
        }
        public HttpClient CreateHttpClient()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            var httpClient = new HttpClient(clientHandler);
            httpClient.BaseAddress = new Uri(_baseUri);
            return httpClient;
        }
    }
}