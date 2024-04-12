using MealsOrderingApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealsOrderingApplication.Data.EntitiesConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // Ignore This Table From Migrations
            builder.ToTable("Customers", "User");

            // Config Primary Key
            builder.HasKey(o => o.Id);

            // Config Properties Constraints
            builder.Property(o => o.FirstName)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(o => o.LastName)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(o => o.Email)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(o => o.Username)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(o => o.Password)
                   .HasMaxLength(512)
                   .IsRequired();

            builder.Property(o => o.Phone)
                   .HasMaxLength(11)
                   .IsRequired();

            builder.Property(o => o.Address)
                   .HasMaxLength(512)
                   .IsRequired();
        }
    }
}
