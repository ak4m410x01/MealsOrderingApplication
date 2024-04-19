using MealsOrderingApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealsOrderingApplication.Data.EntitiesConfigurations
{
    public class OrderProductsConfiguration : IEntityTypeConfiguration<OrderProducts>
    {
        public void Configure(EntityTypeBuilder<OrderProducts> builder)
        {
            // Config Table Name for ProductOrderDetails Entity
            builder.ToTable("OrderProducts", "Product");

            // Config Primary Key
            builder.HasKey(o => new { o.ProductId, o.OrderDetailsId });
        }
    }
}
