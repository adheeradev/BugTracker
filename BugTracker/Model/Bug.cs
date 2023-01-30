using System;
using System.Diagnostics.CodeAnalysis;

namespace BugTracker.Model
{
    [ExcludeFromCodeCoverage]
    public class Bug
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AssignedToUserId { get; set; }  
        public int? StatusId { get; set; }
        public DateTime? OpenedDate { get; set; }
        public string Status { get; set; }
        public string AssignedToUserName { get; set; }
    }
}
