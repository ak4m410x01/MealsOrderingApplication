using MealsOrderingApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealsOrderingApplication.Data.EntitiesConfigurations
{
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            // Ignore This Table From Migrations
            builder.ToTable("OrderDetails", "Product");

            // Config Primary Key
            builder.HasKey(o => o.Id);

            // Config Properties Constraints

            // Config Relationship

            //[Order => OrderDetails]
            builder.HasOne(o => o.Order)
                   .WithOne(c => c.OrderDetails)
                   .HasForeignKey<OrderDetails>(o => o.OrderId)
                   .IsRequired();

            //[Order => Product]
            builder.HasMany(o => o.Products)
                   .WithMany(c => c.OrderDetails)
                   .UsingEntity<ProductOrderDetails>();


        }
    }
}
