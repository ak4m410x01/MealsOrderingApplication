using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.OrderDTO;
using MealsOrderingApplication.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            IQueryable<Order> orders = await _unitOfWork.Orders.GetAllAsync();

            return Ok(orders.Select(o => new
            {
                o.Id,
                o.Description,
                o.CustomerId,
                o.CreatedAt,
            }));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddOrderDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Customer? customer = await _unitOfWork.Customers.GetByIdAsync(dto.CustomerId);
            if (customer is null)
                return NotFound(new { error = "No Customers found with this Id" });

            if (dto.ProductsId.Count != dto.Quantities.Count)
                return BadRequest(new { error = "Quantities and ProductsId lists must have the same length." });

            if (dto.ProductsId.Count <= 0)
                return BadRequest(new { error = "Must be at least one item in ProductsId" });


            for (int i = 0; i < dto.ProductsId.Count; i++)
            {
                if (dto.ProductsId[i] <= 0)
                    return NotFound(new { error = $"ProductId must be greater than 0." });

                if ((await _unitOfWork.Products.GetByIdAsync(dto.ProductsId[i])) is null)
                    return NotFound(new { error = $"No Products found with this Id = {dto.ProductsId[i]}" });
            }

            for (int i = 0; i < dto.Quantities.Count; i++)
            {
                if (dto.Quantities[i] <= 0)
                    return NotFound(new { error = $"Quantity must be greater than 0." });
            }

            try
            {
                Order order = await _unitOfWork.Orders.AddAsync(dto);
                await _unitOfWork.CompleteAsync();

                return Ok(new
                {
                    order.Id,
                    order.Description,
                    order.CustomerId,
                    order.CreatedAt,
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest("ProductId Already exists!");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Order? order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order is null)
                return NotFound(new { error = "No Orders found with this Id" });


            return Ok(new
            {
                order.Id,
                order.Description,
                order.CustomerId,
                order.CreatedAt,
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateOrderDTO dto)
        {
            Order? order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order is null)
                return NotFound(new { error = "No Orders found with this Id" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (dto.ProductsId is not null && dto.Quantities is not null)
            {
                if (dto.ProductsId.Count != dto.Quantities.Count)
                    return BadRequest(new { error = "Quantities and ProductsId lists must have the same length." });

                if (dto.ProductsId.Count <= 0)
                    return BadRequest(new { error = "Must be at least one item in ProductsId" });


                for (int i = 0; i < dto.ProductsId.Count; i++)
                {
                    if (dto.ProductsId[i] <= 0)
                        return NotFound(new { error = $"ProductId must be greater than 0." });

                    if ((await _unitOfWork.Products.GetByIdAsync(dto.ProductsId[i])) is null)
                        return NotFound(new { error = $"No Products found with this Id = {dto.ProductsId[i]}" });
                }

                for (int i = 0; i < dto.Quantities.Count; i++)
                {
                    if (dto.Quantities[i] <= 0)
                        return NotFound(new { error = $"Quantity must be greater than 0." });
                }
            }
            else if ((dto.ProductsId is null && dto.Quantities is not null) || (dto.ProductsId is not null && dto.Quantities is null))
            {
                return BadRequest(new { error = "Quantities and ProductsId must be not nulls" });
            }

            try
            {
                await _unitOfWork.Orders.UpdateAsync(order, dto);
                await _unitOfWork.CompleteAsync();

                return Ok(new
                {
                    order.Id,
                    order.Description,
                    order.CustomerId,
                    order.CreatedAt,
                });
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("Order doesn't exists!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Order? order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order is null)
                return NotFound(new { error = "No Orders found with this Id" });

            await _unitOfWork.Orders.DeleteAsync(order);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
