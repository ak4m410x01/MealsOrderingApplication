namespace MealsOrderingApplication.Domain.Entities
{
    public class OrderDetails
    {
        public double TotalPrice { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public ICollection<OrderProducts>? Products { get; set; }
    }
}
