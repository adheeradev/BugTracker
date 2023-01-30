using System.Collections.Generic;
using BugTracker.Model;

namespace BugTracker.DataService.Response
{
    public class GetAllBugsResponse
    {
        public GetAllBugsResponse()
        {
            Bugs = new List<Bug>();
        }

        public List<Bug> Bugs { get; set; }
    }
}