using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.DataService.Interfaces;
using BugTracker.DataService.Request;
using BugTracker.DataService.Response;
using BugTracker.Model;
using Dapper;

namespace BugTracker.DataService
{
    public class BugDataService : IBugDataService
    {
        private readonly IDbConnectionCreator _dbConnectionCreator;

        public BugDataService(IDbConnectionCreator dbConnectionCreator)
        {
            _dbConnectionCreator = dbConnectionCreator;
        }
        public async Task<GetAllBugsResponse> GetAllBugs(string status)
        {
            GetAllBugsResponse response;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Status", status, DbType.String, ParameterDirection.Input);
                response = await ExecuteGetAllBugs(parameters).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing GetAllBugs method!", ex.InnerException);
            }

            return response;
        }

        protected internal virtual async Task<GetAllBugsResponse> ExecuteGetAllBugs(DynamicParameters parameters)
        {
            var response = new GetAllBugsResponse();

            using var connection = _dbConnectionCreator.CreateIDbConnection();
            var queryResult = await connection.QueryAsync<Bug>(
                "dbo.[Get_All_Bugs]",
                parameters,
                commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            response.Bugs = queryResult.ToList();

            return response;
        }

        public async Task<GetBugResponse> GetBug(int bugId)
        {
            GetBugResponse response;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@BugId", bugId, DbType.Int32, ParameterDirection.Input);
                response = await ExecuteGetBug(parameters).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing GetBug method!", ex.InnerException);
            }

            return response;
        }

        protected internal virtual async Task<GetBugResponse> ExecuteGetBug(DynamicParameters parameters)
        {
            var response = new GetBugResponse();

            using var connection = _dbConnectionCreator.CreateIDbConnection();
            var queryResult = await connection.QuerySingleAsync<Bug>(
                "dbo.[Get_Bug]",
                parameters,
                commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            response.Bug = queryResult;

            return response;
        }

        public async Task<CreateBugResponse> CreateBug(CreateBugRequest request)
        {
            CreateBugResponse response;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Title", request.Title, DbType.String, ParameterDirection.Input);
                parameters.Add("@Description", request.Description, DbType.String, ParameterDirection.Input);
                parameters.Add("@AssignedUserId", request.AssignedUserId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@StatusId", request.StatusId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@OpenedDate", request.OpenedDate, DbType.DateTime, ParameterDirection.Input);
                response = await ExecuteCreateBug(parameters).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing CreateBug method!", ex.InnerException);
            }

            return response;
        }

        protected internal virtual async Task<CreateBugResponse> ExecuteCreateBug(DynamicParameters parameters)
        {
            var response = new CreateBugResponse();

            using var connection = _dbConnectionCreator.CreateIDbConnection();
            var queryResult = await connection.QueryFirstOrDefaultAsync<Bug>(
                "dbo.[Create_Bug]",
                parameters,
                commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            response.Bug = queryResult;

            return response;
        }

        public async Task<UpdateBugResponse> UpdateBug(UpdateBugRequest request)
        {
            UpdateBugResponse response;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@BugId", request.BugId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@Title", request.Title, DbType.String, ParameterDirection.Input);
                parameters.Add("@Description", request.Description, DbType.String, ParameterDirection.Input);
                parameters.Add("@AssignedUserId", request.AssignedUserId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@StatusId", request.StatusId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@OpenedDate", request.OpenedDate, DbType.DateTime, ParameterDirection.Input);
                response = await ExecuteUpdateBug(parameters).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing UpdateBug method!", ex.InnerException);
            }

            return response;
        }

        protected internal virtual async Task<UpdateBugResponse> ExecuteUpdateBug(DynamicParameters parameters)
        {
            var response = new UpdateBugResponse();

            using var connection = _dbConnectionCreator.CreateIDbConnection();
            var queryResult = await connection.QueryFirstOrDefaultAsync<Bug>(
                "dbo.[Update_Bug]",
                parameters,
                commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            response.Bug = queryResult;

            return response;
        }
    }
}