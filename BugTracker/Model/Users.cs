using System;
using System.Diagnostics.CodeAnalysis;

namespace BugTracker.Model
{
    [ExcludeFromCodeCoverage]
    public class User
    {
        public int Id { get; set; } 
        public string Name { get; set; }
    }
}
