using AccountManagement.Domain.RoleAgg;
using AccountManagement.Domain.UserAgg;
using AccountManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore
{
    public class AccountContext:DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(RoleMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}