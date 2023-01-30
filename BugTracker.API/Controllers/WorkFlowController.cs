using System.Threading.Tasks;
using BugTracker.BusinessService.Interface;
using BugTracker.DataService.Response;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.API.Controllers
{
    public class WorkFlowController : Controller
    {
        private readonly IWorkFlowBusinessService _workFlowBusinessService;

        public WorkFlowController(IWorkFlowBusinessService workFlowBusinessService)
        {
            _workFlowBusinessService = workFlowBusinessService;
        }

        [HttpGet("GetAllWorkFlowStatus")]
        [ProducesResponseType(200, Type = typeof(GetAllWorkFlowStatusResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetAllWorkFlowStatus()
        {
            var response = await _workFlowBusinessService.GetAllStatus().ConfigureAwait(false);

            return Ok(response);
        }
    }
}
