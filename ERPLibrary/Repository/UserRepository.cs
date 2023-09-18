using ERPLibrary.Data;
using ERPLibrary.Models;

namespace ERPLibrary.Repository;

public class UserRepository : IUserRepository
{
    private readonly IUserData _data;

    public UserRepository(IUserData data)
    {
        _data = data;
    }

    public Task<UserInfoModel> GetUserInfo(int id, string role)
    {
        return _data.GetUserInfo(id, role);
    }
}
