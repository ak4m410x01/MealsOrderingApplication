using MealsOrderingApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealsOrderingApplication.Data.EntitiesConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // Config Table Name for Customer Entity
            builder.ToTable("Customers", "User");

            // Config Properties Constraints
            builder.Property(o => o.Location)
                   .HasMaxLength(512)
                   .IsRequired();
        }
    }
}
