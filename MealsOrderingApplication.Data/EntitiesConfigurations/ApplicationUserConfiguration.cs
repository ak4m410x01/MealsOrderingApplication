using MealsOrderingApplication.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealsOrderingApplication.Data.EntitiesConfigurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Config Table Name for ApplicationUser Entity
            builder.ToTable("Users", "Security");

            // Config Properties
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.LastName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Email)
                   .IsRequired();

            builder.Property(x => x.UserName)
                   .IsRequired();

            builder.Property(x => x.PasswordHash)
                   .IsRequired();

            builder.Property(x => x.PhoneNumber)
                   .HasMaxLength(11)
                   .IsRequired(false);

            // Config Inheritance as TPC
            builder.UseTptMappingStrategy();
        }
    }
}
