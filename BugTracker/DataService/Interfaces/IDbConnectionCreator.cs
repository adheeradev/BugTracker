using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BugTracker.DataService.Interfaces
{
    public interface IDbConnectionCreator
    {
        IDbConnection CreateIDbConnection();
    }
}