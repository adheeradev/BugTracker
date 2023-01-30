using System;
using System.Threading.Tasks;
using BugTracker.BusinessService.Interface;
using BugTracker.DataService.Interfaces;
using BugTracker.DataService.Response;

namespace BugTracker.BusinessService
{
    public class WorkFlowBusinessService : IWorkFlowBusinessService
    {
        private readonly IWorkFlowDataService _workFlowDataService;

        public WorkFlowBusinessService(IWorkFlowDataService workFlowDataService)
        {
            _workFlowDataService = workFlowDataService;
        }

        public async Task<GetAllWorkFlowStatusResponse> GetAllStatus()
        {
            var response = await _workFlowDataService.GetAllStatus().ConfigureAwait(false);
            return response;
        }
    }
}