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

            // Config Properties Constraints
            builder.Property(o => o.Address)
                   .HasMaxLength(512)
                   .IsRequired();
        }
    }
}
