namespace MealsOrderingApplication.Domain.Entities
{
    public class ProductOrderDetails : BaseEntity
    {
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int OrderDetailsId { get; set; }
        public OrderDetails OrderDetails { get; set; }
    }
}
