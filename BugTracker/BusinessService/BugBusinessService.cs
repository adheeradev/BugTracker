using System.Threading.Tasks;
using BugTracker.BusinessService.Interface;
using BugTracker.DataService.Interfaces;
using BugTracker.DataService.Request;
using BugTracker.DataService.Response;

namespace BugTracker.BusinessService
{
    public class BugBusinessService : IBugBusinessService
    {
        private readonly IBugDataService _bugDataService;

        public BugBusinessService(IBugDataService bugDataService)
        {
            _bugDataService = bugDataService;
        }

        public async Task<GetAllBugsResponse> GetBugs(string status)
        {
            var response = await _bugDataService.GetAllBugs(status).ConfigureAwait(false);
            return response;
        }

        public async Task<GetBugResponse> GetBug(int bugId)
        {
            var response = await _bugDataService.GetBug(bugId);
            return response;
        }

        public async Task<CreateBugResponse> CreateBug(CreateBugRequest request)
        {
            var response = await _bugDataService.CreateBug(request).ConfigureAwait(false);
            return response;
        }

        public async Task<UpdateBugResponse> UpdateBug(UpdateBugRequest request)
        {
            var response = await _bugDataService.UpdateBug(request).ConfigureAwait(false);
            return response;
        }
    }
}