using MealsOrderingApplication.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealsOrderingApplication.Data.EntitiesConfigurations
{
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Config Table Name
            builder.ToTable("Users", "Security");


            // Config Inheritance as TPC
            builder.UseTptMappingStrategy();
        }
    }
}
