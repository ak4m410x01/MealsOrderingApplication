using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.DriverDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Validations.DriverValidation;
using MealsOrderingApplication.Domain.Models;
using MealsOrderingApplication.Services.Services.Response;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        public DriversController(IUnitOfWork unitOfWork, IUpdateDriverValidation updateDriverValidation, IAddDriverValidation addDriverValidation, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _addDriverValidation = addDriverValidation;
            _updateDriverValidation = updateDriverValidation;
            _httpContextAccessor = httpContextAccessor;
        }

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IAddDriverValidation _addDriverValidation;
        protected readonly IUpdateDriverValidation _updateDriverValidation;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        // Retrieve All Drivers
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? email = null, string? username = null, string? name = null)
        {
            IQueryable<Driver> Drivers = await _unitOfWork.Drivers.GetAllAsync();

            if (email is not null)
                Drivers = await _unitOfWork.Drivers.FilterByEmailAsync(Drivers, email);

            if (username is not null)
                Drivers = await _unitOfWork.Drivers.FilterByUsernameAsync(Drivers, username);

            if (name is not null)
                Drivers = await _unitOfWork.Drivers.FilterByNameAsync(Drivers, name);

            PagedResponse<DriverDTODetails> response = new(
                    Drivers.Select(c => new DriverDTODetails
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email!,
                        Username = c.UserName!
                    }),
            _httpContextAccessor.HttpContext!.Request, pageNumber, pageSize);

            return Ok(response);
        }


        // Add new Driver
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddDriverDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _addDriverValidation.AddIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = message,
                });

            AuthanticationModel authModel = await _unitOfWork.Drivers.CreateAsync(dto);
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

        // Retrieve Driver By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            Driver? Driver = await _unitOfWork.Drivers.GetByIdAsync(id);
            if (Driver is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Drivers found with this Id = {id}"
                });

            return Ok(DriverDtoDetailsResponse(Driver));
        }


        // Update exists Driver
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, UpdateDriverDTO dto)
        {
            Driver? Driver = await _unitOfWork.Drivers.GetByIdAsync(id);
            if (Driver is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Drivers found with this Id = {id}"
                });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _updateDriverValidation.UpdateIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = message,
                });

            await _unitOfWork.Drivers.UpdateAsync(Driver, dto);
            await _unitOfWork.CompleteAsync();

            return Ok(DriverDtoDetailsResponse(Driver));
        }


        // Delete Driver
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            Driver? Driver = await _unitOfWork.Drivers.GetByIdAsync(id);
            if (Driver is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Drivers found with this Id = {id}"
                });

            await _unitOfWork.Drivers.DeleteAsync(Driver);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        protected virtual Response<DriverDTODetails> DriverDtoDetailsResponse(Driver Driver)
        {
            return new Response<DriverDTODetails>(
                new DriverDTODetails()
                {
                    Id = Driver.Id,
                    FirstName = Driver.FirstName,
                    LastName = Driver.LastName,
                    Email = Driver.Email!,
                    Username = Driver.UserName!
                });
        }
    }
}
