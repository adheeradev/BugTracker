using System.Threading.Tasks;
using BugTracker.BusinessService.Interface;
using BugTracker.DataService.Request;
using BugTracker.DataService.Response;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.API.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserBusinessService _userBusinessService;

        public UserController(IUserBusinessService userBusinessService)
        {
            _userBusinessService = userBusinessService;
        }

        [HttpPost("AddUser")]
        [ProducesResponseType(200, Type = typeof(AddUserResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest request)
        {
            if (request == null)
                return BadRequest($"{nameof(request)} is invalid");

            if (string.IsNullOrEmpty(request.Name))
                return BadRequest($"{nameof(request.Name)} is invalid");

            var response = await _userBusinessService.AddUser(request).ConfigureAwait(false);

            return Ok(response);
        }

        [HttpGet("GetAllUsers")]
        [ProducesResponseType(200, Type = typeof(GetAllUserResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userBusinessService.GetAllUsers().ConfigureAwait(false);

            return Ok(response);
        }

        [HttpGet("GetUser")]
        [ProducesResponseType(200, Type = typeof(GetAllUserResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetUser([FromQuery] int userId)
        {
            var response = await _userBusinessService.GetUser(userId).ConfigureAwait(false);

            return Ok(response);
        }

        [HttpPut("UpdateUser")]
        [ProducesResponseType(200, Type = typeof(UpdateUserResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            if (request == null)
                return BadRequest($"{nameof(request)} is invalid");

            if (string.IsNullOrEmpty(request.Name))
                return BadRequest($"{nameof(request.Name)} is invalid");

            if (request.UserId < 0)
                return BadRequest($"{nameof(request.UserId)} is invalid");

            var response = await _userBusinessService.UpdateUser(request).ConfigureAwait(false);

            return Ok(response);
        }
    }
}
