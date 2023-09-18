using ERPLibrary.Models;

namespace ERPLibrary.DatabaseAccess
{
    public interface ISqlDataAccess
    {
        Task<UserDataModel> AuthenticateUser<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
        Task<UserInfoModel> GetUserInfo<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
    }
}