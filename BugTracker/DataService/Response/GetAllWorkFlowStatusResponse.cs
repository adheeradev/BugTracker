using System.Collections.Generic;
using BugTracker.Model;

namespace BugTracker.DataService.Response
{
    public class GetAllWorkFlowStatusResponse
    {
        public GetAllWorkFlowStatusResponse()
        {
            WorkFlowStatus = new List<WorkFlowStatus>();
        }

        public List<WorkFlowStatus> WorkFlowStatus { get; set; }
    }
}