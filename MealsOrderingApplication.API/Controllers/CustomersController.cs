using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.Admin;
using MealsOrderingApplication.Domain.DTOs.Customer;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public CustomersController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            IQueryable<Customer> customers = await _unitOfWork.Customers.GetAllAsync();

            return Ok(customers.Select(c => new CustomerDTODetails()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Username = c.UserName,
                Phone = c.PhoneNumber,
                Location = c.Location
            }));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddCustomerDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            AuthanticationModel authModel = await _unitOfWork.Customers.AddAsync(model);
            if (!authModel.IsAuthenticated)
                return BadRequest(authModel.Message);

            await _unitOfWork.CompleteAsync();

            return Ok(new
            {
                UserId = authModel.UserId,
                Email = authModel.Email,
                Username = authModel.UserName,
                Token = authModel.AccessToken,
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
                Email = customer.Email,
                Username = customer.UserName,
                Phone = customer.PhoneNumber,
                Location = customer.Location
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, UpdateCustomerDTO model)
        {
            Customer? customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer is null)
                return NotFound(new { error = "No Customers found with this Id" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.FirstName is not null)
                customer.FirstName = model.FirstName;

            if (model.LastName is not null)
                customer.LastName = model.LastName;

            if (model.Email is not null)
                customer.Email = model.Email;

            if (model.Username is not null)
                customer.UserName = model.Username;

            if (model.Password is not null)
                customer.PasswordHash = _userManager.PasswordHasher.HashPassword(customer, model.Password);

            if (model.Phone is not null)
                customer.PhoneNumber = model.Phone;

            if (model.Location is not null)
                customer.Location = model.Location;

            await _unitOfWork.Customers.UpdateAsync(customer);
            await _unitOfWork.CompleteAsync();

            return Ok(new CustomerDTODetails()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Username = customer.UserName,
                Phone = customer.PhoneNumber,
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
