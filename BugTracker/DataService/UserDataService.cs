using System;
using System.Collections.Generic;
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
    public class UserDataService : IUserDataService
    {
        private readonly IDbConnectionCreator _dbConnectionCreator;

        public UserDataService(IDbConnectionCreator dbConnectionCreator)
        {
            _dbConnectionCreator = dbConnectionCreator;
        }
        public async Task<AddUserResponse> AddUser(AddUserRequest request)
        {
            AddUserResponse response;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Name", request.Name, DbType.String, ParameterDirection.Input);
                response = await ExecuteAddUser(parameters).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing AddUser method!", ex.InnerException);
            }

            return response;
        }
        protected internal async Task<AddUserResponse> ExecuteAddUser(DynamicParameters parameters)
        {
            var response = new AddUserResponse();

            using var connection = _dbConnectionCreator.CreateIDbConnection();
            var queryResult = await connection.QueryFirstOrDefaultAsync<User>(
                "dbo.[Add_User]",
                parameters,
                commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            response.User = queryResult;

            return response;
        }
        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request)
        {
            UpdateUserResponse response;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Name", request.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("@UserId", request.UserId, DbType.Int32, ParameterDirection.Input);
                response = await ExecuteUpdateUser(parameters).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing UpdateUser method!", ex.InnerException);
            }

            return response;
        }
        protected internal async Task<UpdateUserResponse> ExecuteUpdateUser(DynamicParameters parameters)
        {
            var response = new UpdateUserResponse();

            using var connection = _dbConnectionCreator.CreateIDbConnection();
            var queryResult = await connection.QueryFirstOrDefaultAsync<User>(
                "dbo.[Update_User]",
                parameters,
                commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            response.User = queryResult;

            return response;
        }
        public async Task<GetUserResponse> GetUser(int userId)
        {
            GetUserResponse response;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId, DbType.Int32, ParameterDirection.Input);
                response = await ExecuteGetUser(parameters).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing GetUser method!", ex.InnerException);
            }

            return response;
        }
        protected internal async Task<GetUserResponse> ExecuteGetUser(DynamicParameters parameters)
        {
            var response = new GetUserResponse();

            using var connection = _dbConnectionCreator.CreateIDbConnection();
            var queryResult = await connection.QuerySingleAsync<User>(
                "dbo.[Get_User]",
                parameters,
                commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            response.User = queryResult;

            return response;
        }
        public async Task<GetAllUserResponse> GetAllUsers()
        {
            GetAllUserResponse response;

            try
            {
                var parameters = new DynamicParameters();
                response = await ExecuteGetAllUsers(parameters).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing GetAllUsers method!", ex.InnerException);
            }

            return response;
        }
        protected internal async Task<GetAllUserResponse> ExecuteGetAllUsers(DynamicParameters parameters)
        {
            var response = new GetAllUserResponse();

            using var connection = _dbConnectionCreator.CreateIDbConnection();
            var queryResult = await connection.QueryAsync<User>(
                "dbo.[Get_All_Users]",
                parameters,
                commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            response.Users = queryResult.ToList();

            return response;
        }
    }
}