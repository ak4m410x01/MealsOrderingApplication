using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealsOrderingApplication.Data.EntitiesConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            // Config Table Name
            builder.ToTable("Roles", "Security");

            builder.HasData(LoadRoles());
        }

        private IEnumerable<IdentityRole> LoadRoles()
        {
            return new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "User".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
            };
        }
    }
}
