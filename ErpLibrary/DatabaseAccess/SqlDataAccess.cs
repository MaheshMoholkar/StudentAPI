using Dapper;
using ERPLibrary.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ERPLibrary.DatabaseAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;
    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<UserDataModel> AuthenticateUser<T, U>(string storedProcedure, U parameters, string connectionId)
    {
        string connectionString = _config.GetConnectionString(connectionId)!;

        using SqlConnection connection = new(connectionString);
        await connection.OpenAsync();

        var userData = await connection.QueryFirstOrDefaultAsync<UserDataModel>(
            storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure);

        connection.Close(); 

        return userData;
    }
    public async Task<UserInfoModel> GetUserInfo<T ,U>(string storedProcedure, U parameters, string connectionId)
    {
        string connectionString = _config.GetConnectionString(connectionId)!;

        using SqlConnection connection = new(connectionString);
        await connection.OpenAsync();

        var userInfo = await connection.QueryFirstOrDefaultAsync<UserInfoModel>(
            storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure);

        connection.Close();

        return userInfo;
    }
}
