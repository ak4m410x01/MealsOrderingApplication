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
                Order order = await MapAddDtoToEntity<TDto>(dto);
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                // Add Order Details Info in Order Details Table
                OrderDetails orderDetails = new OrderDetails() { OrderId = order.Id, TotalPrice = 0 };
                await _context.OrderDetails.AddAsync(orderDetails);
                await _context.SaveChangesAsync();

                // Add Product Order Details Info in Product Order Details Table
                for (int i = 0; i < addDto.Quantities.Count; i++)
                {
                    await _context.ProductOrderDetails
                            .AddAsync(new ProductOrderDetails()
                            {
                                OrderDetailsId = orderDetails.Id,
                                ProductId = addDto.ProductsId[i],
                                Quantity = addDto.Quantities[i],
                            });
                }
                await _context.SaveChangesAsync();

                // Get Total Price for order products
                orderDetails.TotalPrice = await _context.ProductOrderDetails
                                .Where(p => p.OrderDetailsId == orderDetails.Id)
                                .Include(p => p.Product)
                                .Select(p => new { p.Product, p.Quantity })
                                .SumAsync(p => p.Product.Price * p.Quantity);

                await _context.SaveChangesAsync();
                return await Task.FromResult(order);
            }

            throw new ArgumentException("Invalid DTO type. Expected AddOrderDTO.");
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
