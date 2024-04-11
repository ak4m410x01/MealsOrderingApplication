namespace MealsOrderingApplication.Domain.Entities
{
    public class Meal : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public double Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
