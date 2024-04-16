using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.DTOs.OrderDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MealsOrderingApplication.Services.Repositories
{
    public class OrderRepository(ApplicationDbContext context) : BaseRepository<Order>(context), IOrderRepository
    {
        public override async Task<Order> AddAsync<TDto>(TDto dto)
        {
            if (dto is AddOrderDTO addDto)
            {
                // Add Order Info in Order Table
                Order order = await AddOrderAsync(await MapAddDtoToEntity<TDto>(dto));

                // Add Order Details Info in Order Details Table
                OrderDetails orderDetails = await AddOrderDetailsAsync(order);

                await AddProductOrderDetailsAsync(orderDetails, addDto.ProductsId, addDto.Quantities);

                // Add Total Price for order products
                orderDetails.TotalPrice = await GetTotalPriceAsync(orderDetails);
                await _context.SaveChangesAsync();

                return await Task.FromResult(order);
            }

            throw new ArgumentException("Invalid DTO type. Expected AddOrderDTO.");
        }

        public override async Task<Order> UpdateAsync<TDto>(Order entity, TDto dto)
        {
            if (dto is UpdateOrderDTO updateDto)
            {
                // Update Order Info
                entity = await MapUpdateDtoToEntity<TDto>(entity, dto);

                if (updateDto.ProductsId is not null)
                {
                    // Retrieve OrderDetails if is null throw NullReferenceException
                    OrderDetails? orderDetails = await _context.OrderDetails
                                    .SingleOrDefaultAsync(o => o.OrderId == entity.Id) ??
                                    throw new NullReferenceException(nameof(orderDetails));

                    IQueryable<ProductOrderDetails> productOrderDetails = _context.ProductOrderDetails.Where(p => p.OrderDetailsId == orderDetails.Id).AsQueryable();
                    _context.ProductOrderDetails.RemoveRange(productOrderDetails);

                    // Update Product Order Details
                    await AddProductOrderDetailsAsync(orderDetails, updateDto.ProductsId, updateDto.Quantities!);

                    // Update Total Price for order products
                    orderDetails.TotalPrice = await GetTotalPriceAsync(orderDetails);
                }

                await _context.SaveChangesAsync();

                return await Task.FromResult(entity);
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateOrderDTO.");
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
            OrderDetails orderDetails = new() { OrderId = order.Id, TotalPrice = 0 };
            await _context.OrderDetails.AddAsync(orderDetails);
            await _context.SaveChangesAsync();

            return orderDetails;
        }
        protected virtual async Task AddProductOrderDetailsAsync(OrderDetails orderDetails, List<int> productsId, List<int> quantities)
        {
            // Compine Products and Quantities
            Dictionary<int, int> productsQuantities = CompineProductsQuantities(productsId, quantities);

            // Add Product Order Details Info in Product Order Details Table
            foreach (var item in productsQuantities)
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

        protected virtual Dictionary<int, int> CompineProductsQuantities(List<int> productsId, List<int> quantities)
        {
            // Compine Products and Quantities
            Dictionary<int, int> productsQuantities = [];
            for (int i = 0; i < productsId.Count; i++)
            {
                if (productsQuantities.ContainsKey(productsId[i]))
                {
                    productsQuantities[productsId[i]] += quantities[i];
                }
                else
                {
                    productsQuantities.Add(productsId[i], quantities[i]);
                }
            }
            return productsQuantities;
        }
        protected virtual async Task<double> GetTotalPriceAsync(OrderDetails orderDetails)
        {
            return await _context.ProductOrderDetails
                                .Where(p => p.OrderDetailsId == orderDetails.Id)
                                .Include(p => p.Product)
                                .Select(p => new { p.Product, p.Quantity })
                                .SumAsync(p => p.Product!.Price * p.Quantity);
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

        public override async Task<Order> MapUpdateDtoToEntity<TDto>(Order entity, TDto dto)
        {
            if (dto is UpdateOrderDTO updateDto)
            {
                if (updateDto.Description is not null)
                    entity.Description = updateDto.Description;

                return await Task.FromResult(entity);
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateOrderDTO.");
        }
    }
}
