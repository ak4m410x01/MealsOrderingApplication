using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.OrderDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Validations.OrderValidation;
using MealsOrderingApplication.Services.Services.Response;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public OrdersController(IUnitOfWork unitOfWork, IAddOrderValidation addOrderValidation, IUpdateOrderValidation updateOrderValidation, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _addOrderValidation = addOrderValidation;
            _updateOrderValidation = updateOrderValidation;
            _httpContextAccessor = httpContextAccessor;
        }

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IAddOrderValidation _addOrderValidation;
        protected readonly IUpdateOrderValidation _updateOrderValidation;
        protected readonly IHttpContextAccessor _httpContextAccessor;


        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? customerId = null)
        {
            IQueryable<Order> orders = await _unitOfWork.Orders.GetAllAsync();

            if (customerId is not null)
                orders = await _unitOfWork.Orders.FilterByCustomerAsync(orders, customerId);

            PagedResponse<OrderDTODetails> response = new(
                orders.Select(o => new OrderDTODetails
                {
                    Id = o.Id,
                    Description = o.Description,
                    CustomerId = o.CustomerId,
                    CreatedAt = o.CreatedAt,
                }),
                _httpContextAccessor.HttpContext!.Request, pageNumber, pageSize);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddOrderDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Customer? customer = await _unitOfWork.Customers.GetByIdAsync(dto.CustomerId);
            if (customer is null)
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Customers found with this Id = {dto.CustomerId}"
                });

            string message = await _addOrderValidation.AddIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = message,
                });

            try
            {
                Order order = await _unitOfWork.Orders.AddAsync(dto);
                await _unitOfWork.CompleteAsync();

                return Ok(OrderDtoDetailsResponse(order));
            }
            catch (ArgumentException)
            {
                return BadRequest("ProductId Already exists!");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Order? order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Orders found with this Id = {id}"
                });

            return Ok(OrderDtoDetailsResponse(order));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateOrderDTO dto)
        {
            Order? order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Orders found with this Id = {id}"
                });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _updateOrderValidation.UpdateIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = message,
                });

            try
            {
                await _unitOfWork.Orders.UpdateAsync(order, dto);
                await _unitOfWork.CompleteAsync();

                return Ok(OrderDtoDetailsResponse(order));
            }
            catch (NullReferenceException)
            {
                return BadRequest("Order doesn't exists!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Order? order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Orders found with this Id = {id}"
                });

            await _unitOfWork.Orders.DeleteAsync(order);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        protected virtual Response<OrderDTODetails> OrderDtoDetailsResponse(Order order)
        {
            return new Response<OrderDTODetails>(
                new OrderDTODetails()
                {
                    Id = order.Id,
                    Description = order.Description,
                    CustomerId = order.CustomerId,
                    CreatedAt = order.CreatedAt,
                });
        }
    }
}
