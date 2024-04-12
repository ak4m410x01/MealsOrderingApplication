namespace MealsOrderingApplication.Domain.Entities
{
    public class OrderDetails : BaseEntity
    {
        public double TotalPrice { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
