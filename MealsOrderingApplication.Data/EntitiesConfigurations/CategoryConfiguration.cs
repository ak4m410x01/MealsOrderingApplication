using MealsOrderingApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealsOrderingApplication.Data.EntitiesConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Config Table Name for Category Entity
            builder.ToTable("Categories", "Product");

            // Config Primary Key
            builder.HasKey(c => c.Id);

            // Config Properties Constraints
            builder.Property(c => c.Name)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(c => c.Description)
                   .HasMaxLength(5_000)
                   .IsRequired(false);
        }
    }
}
