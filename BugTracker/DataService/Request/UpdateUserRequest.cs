using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.DataService.Request
{
    public class UpdateUserRequest
    {
        public UpdateUserRequest(string name, int userId)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException($"{name} cannot be empty when updating a user");
            }

            if (userId < 0)
            {
                throw new ArgumentOutOfRangeException($"{userId} cannot be less than zero");
            }

            UserId = userId;
            Name = name;;
        }
        public string Name { get; }
        public int UserId { get; }
    }
}