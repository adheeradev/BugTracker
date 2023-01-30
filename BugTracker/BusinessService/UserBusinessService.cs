using System;
using System.Data;
using System.Threading.Tasks;
using BugTracker.BusinessService.Interface;
using BugTracker.DataService.Interfaces;
using BugTracker.DataService.Request;
using BugTracker.DataService.Response;

namespace BugTracker.BusinessService
{
    public class UserBusinessService : IUserBusinessService
    {
        private readonly IUserDataService _userDataService;

        public UserBusinessService(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public async Task<GetAllUserResponse> GetAllUsers()
        {
            var response = await _userDataService.GetAllUsers().ConfigureAwait(false);
            return response;
        }

        public async Task<GetUserResponse> GetUser(int userId)
        {
            var response = await _userDataService.GetUser(userId).ConfigureAwait(false);
            return response;
        }


        public async Task<AddUserResponse> AddUser(AddUserRequest request)
        {
            var response =await _userDataService.AddUser(request).ConfigureAwait(false);
            return response;
        }

        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request)
        {
            var response =await _userDataService.UpdateUser(request).ConfigureAwait(false);
            return response;
        }
    }
}