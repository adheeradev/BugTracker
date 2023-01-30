using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.DataService.Interfaces;
using BugTracker.DataService.Response;
using BugTracker.Model;
using Dapper;

namespace BugTracker.DataService
{
    public class WorkFlowDataService : IWorkFlowDataService
    {
        private readonly IDbConnectionCreator _dbConnectionCreator;

        public WorkFlowDataService(IDbConnectionCreator dbConnectionCreator)
        {
            _dbConnectionCreator = dbConnectionCreator;
        }
        public async Task<GetAllWorkFlowStatusResponse> GetAllStatus()
        {
            GetAllWorkFlowStatusResponse response;

            try
            {
                var parameters = new DynamicParameters();
                response = await ExecuteGetAllStatus(parameters).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing GetAllStatus method!", ex.InnerException);
            }

            return response;
        }

        protected internal async Task<GetAllWorkFlowStatusResponse> ExecuteGetAllStatus(DynamicParameters parameters)
        {
            var response = new GetAllWorkFlowStatusResponse();

            using var connection = _dbConnectionCreator.CreateIDbConnection();
            var queryResult = await connection.QueryAsync<WorkFlowStatus>(
                "dbo.[Get_All_WorkFlowStatus]",
                parameters,
                commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            response.WorkFlowStatus = queryResult.ToList();

            return response;
        }
    }
}