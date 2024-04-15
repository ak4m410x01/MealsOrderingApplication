using MealsOrderingApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealsOrderingApplication.Data.EntitiesConfigurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            // Config Table Name for Admin Entity
            builder.ToTable("Reviews", "Product");

            // Config Properties
            builder.HasKey(r => r.Id);

            // TODO: Validate Stars value must be between 1 and 5.
            builder.Property(r => r.Stars)
                   .IsRequired();

            builder.Property(r => r.Comment)
                   .HasMaxLength(1_000)
                   .IsRequired();

            // Config Relationship

            //[Reviews => Product] Many-To-One
            builder.HasOne(r => r.Product)
                   .WithMany(p => p.Reviews)
                   .HasForeignKey(r => r.ProductId)
                   .IsRequired();

            //[Reviews => Customer] Many-To-One
            builder.HasOne(r => r.Customer)
                   .WithMany(c => c.Reviews)
                   .HasForeignKey(r => r.CustomerId)
                   .IsRequired();
        }
    }
}
