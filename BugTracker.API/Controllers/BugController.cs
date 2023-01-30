using System.Threading.Tasks;
using BugTracker.BusinessService.Interface;
using BugTracker.DataService.Request;
using BugTracker.DataService.Response;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.API.Controllers
{
    public class BugController : Controller
    {
        private readonly IBugBusinessService _bugBusinessService;

        public BugController(IBugBusinessService bugBusinessService)
        {
            _bugBusinessService = bugBusinessService;
        }

        [HttpPost("CreateBug")]
        [ProducesResponseType(200, Type = typeof(CreateBugResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CreateBug([FromBody] CreateBugRequest request)
        {
            if (request == null)
                return BadRequest($"{nameof(request)} is invalid");

            if (string.IsNullOrEmpty(request.Title))
                return BadRequest($"{nameof(request.Title)} is invalid");

            var response = await _bugBusinessService.CreateBug(request).ConfigureAwait(false);

            return Ok(response);
        }

        [HttpGet("GetAllBugs")]
        [ProducesResponseType(200, Type = typeof(GetAllBugsResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetAllBugs([FromQuery] string status = null)
        {
            var response = await _bugBusinessService.GetBugs(status).ConfigureAwait(false);

            return Ok(response);
        }

        [HttpGet("GetBug")]
        [ProducesResponseType(200, Type = typeof(GetBugResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetBug([FromQuery] int bugId)
        {
            if (bugId < 0)
                return BadRequest($"{nameof(bugId)} is invalid");

            var response = await _bugBusinessService.GetBug(bugId).ConfigureAwait(false);

            return Ok(response);
        }

        [HttpPut("UpdateBug")]
        [ProducesResponseType(200, Type = typeof(UpdateBugResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateBug([FromBody] UpdateBugRequest request)
        {
            if (request.BugId < 0)
                return BadRequest($"{nameof(request.BugId)} is invalid");

            var response = await _bugBusinessService.UpdateBug(request).ConfigureAwait(false);

            return Ok(response);
        }
    }
}
