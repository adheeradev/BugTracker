using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BugTracker.DataService.Request;
using BugTracker.DataService.Response;
using BugTracker.Model;

namespace BugTracker.BusinessService.Interface
{
    public interface IUserBusinessService
    {
        Task<GetAllUserResponse> GetAllUsers();
        Task<GetUserResponse> GetUser(int userId);
        Task<AddUserResponse> AddUser(AddUserRequest request);
        Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request);
    }
}
