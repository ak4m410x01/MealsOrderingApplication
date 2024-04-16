namespace MealsOrderingApplication.Domain.Entities
{
    public class Review : BaseEntity
    {
        public ushort Stars { get; set; } = default!;
        public string Comment { get; set; } = default!;

        public int ProductId { get; set; } = default!;
        public Product? Product { get; set; }

        public string CustomerId { get; set; } = default!;
        public Customer? Customer { get; set; }
    }
}
