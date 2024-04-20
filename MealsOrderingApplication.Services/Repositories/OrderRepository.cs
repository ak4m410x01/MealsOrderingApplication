using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.DTOs.OrderDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Orders;
using Microsoft.EntityFrameworkCore.Storage;

namespace MealsOrderingApplication.Services.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository, IOrdersFilter
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IQueryable<Order>> GetAllAsync()
        {
            IQueryable<Order> orders = await base.GetAllAsync();
            orders = await Task.FromResult(_context.Orders
                .Include(o => o.OrderDetails)
                .AsQueryable());

            return orders;
        }
        public override async Task<Order> AddAsync<TDto>(TDto dto)
        {
            if (dto is AddOrderDTO addDto)
            {
                IDbContextTransaction transaction = _context.Database.BeginTransaction();

                try
                {
                    // Add Order Info in Order Table
                    Order order = await AddOrderAsync(await MapAddDtoToEntity<TDto>(dto));

                    // Add Order Details Info in Order Details Table
                    OrderDetails orderDetails = await AddOrderDetailsAsync(order);

                    await AddOrderProductsAsync(orderDetails, addDto.ProductsId, addDto.Quantities);

                    // Add Total Price for order products
                    orderDetails.TotalPrice = await GetTotalPriceAsync(orderDetails);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return await Task.FromResult(order);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                }
            }

            throw new ArgumentException("Invalid DTO type. Expected AddOrderDTO.");
        }

        public override async Task<Order?> GetByIdAsync(object id)
        {
            Order? order = await base.GetByIdAsync(id);

            if (order is not null)
            {
                await _context.Entry(order)
                              .Reference(o => o.OrderDetails)
                              .Query()
                              .Include(p => p.Products)
                              .LoadAsync();
            }

            return order;
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

                    IQueryable<OrderProducts> productOrderDetails = _context.OrderProducts.Where(p => p.OrderDetailsId == orderDetails.OrderId).AsQueryable();
                    _context.OrderProducts.RemoveRange(productOrderDetails);

                    // Update Product Order Details
                    await AddOrderProductsAsync(orderDetails, updateDto.ProductsId, updateDto.Quantities!);

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
        protected virtual async Task AddOrderProductsAsync(OrderDetails orderDetails, List<int> productsId, List<int> quantities)
        {
            // Compine Products and Quantities
            Dictionary<int, int> productsQuantities = CompineProductsQuantities(productsId, quantities);

            // Add Product Order Details Info in Product Order Details Table
            foreach (var item in productsQuantities)
            {
                await _context.OrderProducts
                        .AddAsync(new OrderProducts()
                        {
                            OrderDetailsId = orderDetails.OrderId,
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
            return await _context.OrderProducts
                                .Where(p => p.OrderDetailsId == orderDetails.OrderId)
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

        public virtual async Task<IQueryable<Order>> FilterByCustomerAsync(IQueryable<Order> orders, string customerId)
        {
            return await Task.FromResult(orders.Where(o => o.CustomerId == customerId));
        }
    }
}
