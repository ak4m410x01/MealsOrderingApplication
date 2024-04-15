using MealsOrderingApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealsOrderingApplication.Data.EntitiesConfigurations
{
    public class ProductOrderDetailsConfiguration : IEntityTypeConfiguration<ProductOrderDetails>
    {
        public void Configure(EntityTypeBuilder<ProductOrderDetails> builder)
        {
            // Config Table Name for ProductOrderDetails Entity
            builder.ToTable("ProductOrderDetails", "Product");

            // Config Primary Key
            builder.HasKey(o => new { o.ProductId, o.OrderDetailsId });
        }
    }
}
