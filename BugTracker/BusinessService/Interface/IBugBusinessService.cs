using System.Threading.Tasks;
using BugTracker.DataService.Request;
using BugTracker.DataService.Response;

namespace BugTracker.BusinessService.Interface
{
    public interface IBugBusinessService
    {
        Task<GetAllBugsResponse> GetBugs(string status);
        Task<GetBugResponse> GetBug(int bugId);
        Task<CreateBugResponse> CreateBug(CreateBugRequest request);
        Task<UpdateBugResponse> UpdateBug(UpdateBugRequest request);
    }
}