using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BugTracker.DataService.Response;

namespace BugTracker.BusinessService.Interface
{
    public interface IWorkFlowBusinessService
    {
        Task<GetAllWorkFlowStatusResponse> GetAllStatus();
    }
}
