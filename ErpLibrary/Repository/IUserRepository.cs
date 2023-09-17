using ERPLibrary.Models;

namespace ERPLibrary.Repository
{
    public interface IUserRepository
    {
        Task<UserInfoModel> GetUserInfo(int id, string role);
    }
}