using System.Data;
using System.Data.SqlClient;
using BugTracker.DataService.Interfaces;

namespace BugTracker.DataService
{
    public class MsSqlDbConnectionCreator : IDbConnectionCreator
    {
        private readonly string _connectionString;

        public MsSqlDbConnectionCreator(string connectionString) => this._connectionString = connectionString;

        public IDbConnection CreateIDbConnection() => (IDbConnection)new SqlConnection(this._connectionString);
    }
}