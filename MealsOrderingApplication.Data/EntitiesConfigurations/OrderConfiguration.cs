using MealsOrderingApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealsOrderingApplication.Data.EntitiesConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Config Table Name for Order Entity
            builder.ToTable("Orders", "Product");

            // Config Primary Key
            builder.HasKey(o => o.Id);

            // Config Properties Constraints
            builder.Property(o => o.Description)
                   .HasMaxLength(255)
                   .IsRequired(false);

            // Config Relationship

            //[Customer => Order]
            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.CustomerId)
                   .IsRequired();

            //[Driver => Order]
            builder.HasOne(o => o.Driver)
                   .WithMany(d => d.Orders)
                   .HasForeignKey(o => o.DriverId)
                   .IsRequired(false);

        }
    }
}
