using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.DTOs.OrderDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MealsOrderingApplication.Services.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Order> AddAsync<TDto>(TDto dto)
        {
            if (dto is AddOrderDTO addDto)
            {
                // Add Order Info in Order Table
                Order order = await AddOrderAsync(await MapAddDtoToEntity<TDto>(dto));

                // Add Order Details Info in Order Details Table
                OrderDetails orderDetails = await AddOrderDetailsAsync(order);

                await AddProductOrderDetailsAsync(orderDetails, addDto.ProductsId, addDto.Quantities);

                // Get Total Price for order products
                orderDetails.TotalPrice = await GetTotalPriceAsync(orderDetails);
                await _context.SaveChangesAsync();

                return await Task.FromResult(order);
            }

            throw new ArgumentException("Invalid DTO type. Expected AddOrderDTO.");
        }

        protected virtual async Task<Order> AddOrderAsync(Order order)
        {
            Order _order = order;
            await _context.Orders.AddAsync(_order);
            await _context.SaveChangesAsync();

            return _order;
        }
        protected virtual async Task<OrderDetails> AddOrderDetailsAsync(Order order)
        {
            OrderDetails orderDetails = new OrderDetails() { OrderId = order.Id, TotalPrice = 0 };
            await _context.OrderDetails.AddAsync(orderDetails);
            await _context.SaveChangesAsync();

            return orderDetails;
        }
        protected virtual async Task AddProductOrderDetailsAsync(OrderDetails orderDetails, List<int> productsId, List<int> quantities)
        {
            // Compine Products and Quantities
            Dictionary<int, int> ProductsQuantities = new Dictionary<int, int>();
            for (int i = 0; i < productsId.Count; i++)
            {
                if (ProductsQuantities.ContainsKey(productsId[i]))
                {
                    ProductsQuantities[productsId[i]] += quantities[i];
                }
                else
                {
                    ProductsQuantities.Add(productsId[i], quantities[i]);
                }
            }

            // Add Product Order Details Info in Product Order Details Table
            foreach (var item in ProductsQuantities)
            {
                await _context.ProductOrderDetails
                        .AddAsync(new ProductOrderDetails()
                        {
                            OrderDetailsId = orderDetails.Id,
                            ProductId = item.Key,
                            Quantity = item.Value,
                        });
            }
            await _context.SaveChangesAsync();
        }

        protected virtual async Task<double> GetTotalPriceAsync(OrderDetails orderDetails)
        {
            return await _context.ProductOrderDetails
                                .Where(p => p.OrderDetailsId == orderDetails.Id)
                                .Include(p => p.Product)
                                .Select(p => new { p.Product, p.Quantity })
                                .SumAsync(p => p.Product.Price * p.Quantity);
        }

        public override async Task<Order> MapAddDtoToEntity<TDto>(TDto dto)
        {
            if (dto is AddOrderDTO addDto)
            {
                return await Task.FromResult(new Order()
                {
                    Description = addDto.Description,
                    CustomerId = addDto.CustomerId,
                });
            }
            throw new ArgumentException("Invalid DTO type. Expected AddOrderDTO.");
        }

        public override Task<Order> MapUpdateDtoToEntity<TDto>(Order entity, TDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
