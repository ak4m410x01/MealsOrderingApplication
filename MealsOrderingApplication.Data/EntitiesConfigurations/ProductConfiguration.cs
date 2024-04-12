using MealsOrderingApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealsOrderingApplication.Data.EntitiesConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Ignore This Table From Migrations
            builder.ToTable("Products", "Product");

            // Config Primary Key
            builder.HasKey(p => p.Id);

            // Config Properties Constraints
            builder.Property(p => p.Description)
                   .HasMaxLength(5_000)
                   .IsRequired(false);

            // Config [Meals => Category] Relationship
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .IsRequired();

            // Config Inheritance as TPC
            builder.UseTptMappingStrategy();
        }
    }

}
