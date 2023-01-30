using System;

namespace BugTracker.DataService.Request
{
    public class UpdateBugRequest
    {
        public int BugId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? StatusId { get; set; }
        public DateTime? OpenedDate { get; set; }
        public int? AssignedUserId { get; set; }
    }
}