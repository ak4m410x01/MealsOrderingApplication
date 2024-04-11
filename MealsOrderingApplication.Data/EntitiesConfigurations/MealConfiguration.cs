using MealsOrderingApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealsOrderingApplication.Data.EntitiesConfigurations
{
    public class MealConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            // Config Table Name
            builder.ToTable("Meals", "Products");

            // Config Primary Key
            builder.HasKey(m => m.Id);

            // Config Properties Constraints
            builder.Property(m => m.Name)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(m => m.Description)
                   .HasMaxLength(5_000)
                   .IsRequired(false);

            // Config [Meals => Category] Relationship
            builder.HasOne(m => m.Category)
                   .WithMany(c => c.Meals)
                   .HasForeignKey(m => m.CategoryId)
                   .IsRequired();
        }
    }

}
