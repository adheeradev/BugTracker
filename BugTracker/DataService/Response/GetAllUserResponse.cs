using System.Collections.Generic;
using BugTracker.Model;

namespace BugTracker.DataService.Response
{
    public class GetAllUserResponse
    {
        public GetAllUserResponse()
        {
            Users = new List<User>();
        }

        public List<User> Users { get; set; }
    }
}