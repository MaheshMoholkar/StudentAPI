using ERPLibrary.Models;

namespace ERPLibrary.Data
{
    public interface IUserData
    {
        Task<UserDataModel> Authenticate(string username, string password, string role);
        Task<UserInfoModel> GetUserInfo(int id, string role);
    }
}