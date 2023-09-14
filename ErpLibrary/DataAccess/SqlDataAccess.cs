using Microsoft.Extensions.Configuration;

namespace ErpLibrary.DataAccess;

public class SqlDataAccess
{
    private readonly IConfiguration _config;
    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }
}
