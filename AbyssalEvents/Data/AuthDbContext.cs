using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Abyssal_Events.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var superAdminRoleId = "821baf45-95cc-455e-8314-9fd5391e0ddf";
            var adminRoleId = "1036334c-216c-445e-b850-5181ad0e618f";
            var userRoleId = "39400434-5a87-4e29-90eb-8b2799f4da1c";

            var roles = new List<IdentityRole> { 
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                     Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);

            var superAdminId = "cdd4608f-4144-4009-9c61-a6df0f5f9f9c";

            var superAdminUser = new IdentityUser { 
                UserName = "superadmin@abyss.com",
                Email = "superadmin@abyss.com",
                NormalizedEmail = "superadmin@abyss.com".ToUpper(),
                NormalizedUserName = "superadmin@abyss.com".ToUpper(),
                Id = superAdminId,
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "Abyss#123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                },
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
