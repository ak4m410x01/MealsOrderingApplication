namespace MealsOrderingApplication.Domain.Entities
{
    public class OrderProducts
    {
        public int Quantity { get; set; }

        public int ProductId { get; set; } = default!;
        public Product? Product { get; set; }

        public int OrderDetailsId { get; set; } = default!;
        public OrderDetails? OrderDetails { get; set; }
    }
}
