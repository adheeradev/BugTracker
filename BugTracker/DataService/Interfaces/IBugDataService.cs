using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BugTracker.DataService.Request;
using BugTracker.DataService.Response;

namespace BugTracker.DataService.Interfaces
{
    public interface IBugDataService
    {
        Task<GetAllBugsResponse> GetAllBugs(string status);
        Task<GetBugResponse> GetBug(int bugId);
        Task<CreateBugResponse> CreateBug(CreateBugRequest request);
        Task<UpdateBugResponse> UpdateBug(UpdateBugRequest request);
    }
}