using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.OrderDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Validations.OrderValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public OrdersController(IUnitOfWork unitOfWork, IAddOrderValidation addOrderValidation, IUpdateOrderValidation updateOrderValidation)
        {
            _unitOfWork = unitOfWork;
            _addOrderValidation = addOrderValidation;
            _updateOrderValidation = updateOrderValidation;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddOrderValidation _addOrderValidation;
        private readonly IUpdateOrderValidation _updateOrderValidation;

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

            string message = await _addOrderValidation.AddIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
            {
                return BadRequest(new { error = message });
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

            string message = await _updateOrderValidation.UpdateIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
            {
                return BadRequest(new { error = message });
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
