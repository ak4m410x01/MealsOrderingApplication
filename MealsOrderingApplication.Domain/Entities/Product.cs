﻿namespace MealsOrderingApplication.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public double Price { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<OrderProducts>? Orders { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
