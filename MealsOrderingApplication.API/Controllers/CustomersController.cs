using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.CustomerDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.Validations.CustomerValidation;
using MealsOrderingApplication.Domain.Models;
using MealsOrderingApplication.Services.Services.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public CustomersController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IUpdateCustomerValidation updateCustomerValidation, IAddCustomerValidation addCustomerValidation, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _addCustomerValidation = addCustomerValidation;
            _updateCustomerValidation = updateCustomerValidation;
            _httpContextAccessor = httpContextAccessor;
        }

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly IAddCustomerValidation _addCustomerValidation;
        protected readonly IUpdateCustomerValidation _updateCustomerValidation;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<Customer> customers = await _unitOfWork.Customers.GetAllAsync();
            return Ok(new PagedResponse<CustomerDTODetails>(
                customers.Select(c => new CustomerDTODetails
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email ?? "",
                    Username = c.UserName ?? "",
                    PhoneNumber = c.PhoneNumber ?? "",
                    Location = c.Location
                }),
                _httpContextAccessor.HttpContext!.Request, pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddCustomerDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _addCustomerValidation.AddIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = message,
                });

            AuthanticationModel authModel = await _unitOfWork.Customers.CreateAsync(dto);
            if (!authModel.IsAuthenticated)
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = authModel.Message,
                });

            await _unitOfWork.CompleteAsync();


            return Created(nameof(GetByIdAsync), new
            {
                authModel.UserId,
                authModel.Email,
                Username = authModel.UserName,
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            Customer? customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer is null)
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Customers found with this Id = {id}"
                });

            return Ok(CustomerDtoDetailsResponse(customer));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, UpdateCustomerDTO dto)
        {
            Customer? customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer is null)
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Customers found with this Id = {id}"
                });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _updateCustomerValidation.UpdateIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = message,
                });

            await _unitOfWork.Customers.UpdateAsync(customer, dto);
            await _unitOfWork.CompleteAsync();

            return Ok(CustomerDtoDetailsResponse(customer));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            Customer? customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer is null)
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Customers found with this Id = {id}"
                });

            await _unitOfWork.Customers.DeleteAsync(customer);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        protected virtual Response<CustomerDTODetails> CustomerDtoDetailsResponse(Customer customer)
        {
            return new Response<CustomerDTODetails>(
                new CustomerDTODetails()
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email!,
                    Username = customer.UserName!
                });
        }
    }
}
