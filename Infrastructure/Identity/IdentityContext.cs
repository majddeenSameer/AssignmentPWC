using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.DomainModels;

namespace Infrastructure.Identity
{
    public class IdentityContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public IdentityContext(
            DbContextOptions<IdentityContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>().HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId);
            builder.Entity<UserRole>().HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId);

            builder.Entity<Application>();
            builder.Entity<UserApplication>().HasOne(x => x.Application).WithMany().HasForeignKey(x => x.ApplicationId);
            builder.Entity<UserApplication>().HasOne(x => x.User).WithMany(x => x.UserApplications).HasForeignKey(x => x.UserId);

        }
    }
}
