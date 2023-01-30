using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BugTracker.DataService.Request;
using BugTracker.DataService.Response;

namespace BugTracker.DataService.Interfaces
{
    public interface IUserDataService
    {
        Task<AddUserResponse> AddUser(AddUserRequest request);
        Task<GetUserResponse> GetUser(int userId);
        Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request);
        Task<GetAllUserResponse> GetAllUsers();
    }
}
