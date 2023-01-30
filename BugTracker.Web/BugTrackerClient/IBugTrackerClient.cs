using System.Net.Http;

namespace BugTracker.Web.BugTrackerClient
{
    public interface IBugTrackerClient
    {
        HttpClient CreateHttpClient();
    }
}