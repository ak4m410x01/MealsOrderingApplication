namespace MealsOrderingApplication.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public OrderDetails OrderDetails { get; set; }
    }
}
