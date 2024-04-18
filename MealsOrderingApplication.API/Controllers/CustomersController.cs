using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.CustomerDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.Validations.CustomerValidation;
using MealsOrderingApplication.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly IAddCustomerValidation _addCustomerValidation;
        protected readonly IUpdateCustomerValidation _updateCustomerValidation;

        public CustomersController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IUpdateCustomerValidation updateCustomerValidation, IAddCustomerValidation addCustomerValidation)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _addCustomerValidation = addCustomerValidation;
            _updateCustomerValidation = updateCustomerValidation;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            IQueryable<Customer> customers = await _unitOfWork.Customers.GetAllAsync();

            return Ok(customers.Select(c => new CustomerDTODetails()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email ?? "",
                Username = c.UserName ?? "",
                PhoneNumber = c.PhoneNumber ?? "",
                Location = c.Location
            }));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddCustomerDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _addCustomerValidation.AddIsValidAsync(dto);
            if (string.IsNullOrEmpty(message))
                return BadRequest(new { error = message });

            AuthanticationModel authModel = await _unitOfWork.Customers.CreateAsync(dto);
            if (!authModel.IsAuthenticated)
                return BadRequest(new { error = authModel.Message });

            await _unitOfWork.CompleteAsync();

            return Ok(new
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
                return NotFound(new { error = "No Customers found with this Id" });

            return Ok(new CustomerDTODetails()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email ?? "",
                Username = customer.UserName ?? "",
                PhoneNumber = customer.PhoneNumber ?? "",
                Location = customer.Location
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, UpdateCustomerDTO dto)
        {
            Customer? customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer is null)
                return NotFound(new { error = "No Customers found with this Id" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _updateCustomerValidation.UpdateIsValidAsync(dto);
            if (string.IsNullOrEmpty(message))
                return BadRequest(new { error = message });

            await _unitOfWork.Customers.UpdateAsync(customer, dto);
            await _unitOfWork.CompleteAsync();

            return Ok(new CustomerDTODetails()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email ?? "",
                Username = customer.UserName ?? "",
                PhoneNumber = customer.PhoneNumber ?? "",
                Location = customer.Location,
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            Customer? customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer is null)
                return NotFound(new { error = "No Customers found with this Id" });

            await _unitOfWork.Customers.DeleteAsync(customer);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
