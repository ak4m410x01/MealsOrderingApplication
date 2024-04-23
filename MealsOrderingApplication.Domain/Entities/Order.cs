namespace MealsOrderingApplication.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string? Description { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public string CustomerId { get; set; } = default!;
        public Customer? Customer { get; set; }

        public string? DriverId { get; set; }
        public Driver? Driver { get; set; }

        public OrderDetails? OrderDetails { get; set; }
    }
}
