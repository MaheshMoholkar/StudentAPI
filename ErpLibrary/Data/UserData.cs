using ERPLibrary.DatabaseAccess;
using ERPLibrary.Models;

namespace ERPLibrary.Data;

public class UserData : IUserData
{
    private readonly ISqlDataAccess _sql;

    public UserData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    public Task<UserDataModel> Authenticate(string username, string password, string role)
    {
        if (role == "student")
        {
            return _sql.AuthenticateUser<UserDataModel, dynamic>(
            "dbo.spLoginCredentials_AuthenticateStudent",
            new { Username = username, Password = password },
            "Default"
            );
        }
        else
        {
            return _sql.AuthenticateUser<UserDataModel, dynamic>(
            "dbo.spLoginCredentials_AuthenticateTeacher",
            new { Username = username, Password = password },
            "Default"
            );
        }
    }
    public Task<UserInfoModel> GetUserInfo(int id, string role)
    {
        if (role == "student")
        {
            return _sql.GetUserInfo<UserInfoModel, dynamic>(
            "dbo.spStudentsInfo_GetInfo",
            new { StudentId = id },
            "Default"
            );
        }
        else
        {
            return _sql.GetUserInfo<UserInfoModel, dynamic>(
            "dbo.spTeachersInfo_GetInfo",
            new { TeacherId = id },
            "Default"
            );
        }
    }
}
