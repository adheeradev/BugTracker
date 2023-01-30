using System.Text;
using System.Threading.Tasks;
using BugTracker.DataService.Response;

namespace BugTracker.DataService.Interfaces
{
    public interface IWorkFlowDataService
    {
        Task<GetAllWorkFlowStatusResponse> GetAllStatus();
    }
}