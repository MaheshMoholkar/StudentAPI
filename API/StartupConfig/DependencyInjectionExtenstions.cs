using API.Constants;
using ERPLibrary.Data;
using ERPLibrary.DatabaseAccess;
using ERPLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.StartupConfig
{
    public static class DependencyInjectionExtensions
    {
        public static void AddStandardServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
            builder.Services.AddSingleton<IUserData, UserData>();
            builder.Services.AddSingleton<IUserRepository, UserRepository>();
        }

        public static void AddHealthServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks()
                .AddSqlServer(builder.Configuration.GetConnectionString("Default"));
        }

        public static void AddAuthServices(this WebApplicationBuilder builder)
        {
            var authenticationIssuer = Environment.GetEnvironmentVariable("Issuer");
            var authenticationAudience = Environment.GetEnvironmentVariable("Audience");
            var authenticationSecretKey = Environment.GetEnvironmentVariable("SecretKey");

            builder.Services.AddAuthorization(opts =>
            {
                opts.AddPolicy(PolicyConstants.Admin, policy =>
                {
                    policy.RequireClaim("Role", "admin");
                });
                opts.AddPolicy(PolicyConstants.Teacher, policy =>
                {
                    policy.RequireClaim("Role", "teacher");
                });
                opts.AddPolicy(PolicyConstants.Student, policy =>
                {
                    policy.RequireClaim("Role", "student");
                });
                opts.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });
            builder.Services.AddAuthentication("Bearer").AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = authenticationIssuer,
                    ValidAudience = authenticationAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(authenticationSecretKey)
                    )
                };
            });
        }
    }
}
