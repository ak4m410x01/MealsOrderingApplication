namespace MealsOrderingApplication.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public ICollection<Meal> Meals { get; set; }
    }
}
