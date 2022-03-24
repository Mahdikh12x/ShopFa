using AccountManagement.Application;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Application.Contract.User;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Domain.UserAgg;
using AccountManagement.Infrastructure.EFCore;
using AccountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Infrastructure.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services,string connectionString)
        {
            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddTransient<IUserApplication,UserApplication>();
            services.AddTransient<IUserRepository,UserRepository>();

            services.AddDbContext<AccountContext>(x => x.UseSqlServer(connectionString));
        }
    }
}