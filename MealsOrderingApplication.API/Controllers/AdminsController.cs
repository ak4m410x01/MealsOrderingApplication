using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.AdminDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Validations.AdminValidation;
using MealsOrderingApplication.Domain.Models;
using MealsOrderingApplication.Services.Services.Response;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        public AdminsController(IUnitOfWork unitOfWork, IUpdateAdminValidation updateAdminValidation, IAddAdminValidation addAdminValidation, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _addAdminValidation = addAdminValidation;
            _updateAdminValidation = updateAdminValidation;
            _httpContextAccessor = httpContextAccessor;
        }

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IAddAdminValidation _addAdminValidation;
        protected readonly IUpdateAdminValidation _updateAdminValidation;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        // Retrieve All Admins
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? email = null, string? username = null, string? name = null)
        {
            IQueryable<Admin> admins = await _unitOfWork.Admins.GetAllAsync();

            if (email is not null)
                admins = await _unitOfWork.Admins.FilterByEmailAsync(admins, email);

            if (username is not null)
                admins = await _unitOfWork.Admins.FilterByUsernameAsync(admins, username);

            if (name is not null)
                admins = await _unitOfWork.Admins.FilterByNameAsync(admins, name);

            PagedResponse<AdminDTODetails> response = new(
                    admins.Select(c => new AdminDTODetails
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


        // Add new Admin
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddAdminDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _addAdminValidation.AddIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = message,
                });

            AuthanticationModel authModel = await _unitOfWork.Admins.CreateAsync(dto);
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

        // Retrieve Admin By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            Admin? admin = await _unitOfWork.Admins.GetByIdAsync(id);
            if (admin is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Admins found with this Id = {id}"
                });

            return Ok(AdminDtoDetailsResponse(admin));
        }


        // Update exists Admin
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, UpdateAdminDTO dto)
        {
            Admin? admin = await _unitOfWork.Admins.GetByIdAsync(id);
            if (admin is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Admins found with this Id = {id}"
                });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _updateAdminValidation.UpdateIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = message,
                });

            await _unitOfWork.Admins.UpdateAsync(admin, dto);
            await _unitOfWork.CompleteAsync();

            return Ok(AdminDtoDetailsResponse(admin));
        }


        // Delete Admin
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            Admin? admin = await _unitOfWork.Admins.GetByIdAsync(id);
            if (admin is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Admins found with this Id = {id}"
                });

            await _unitOfWork.Admins.DeleteAsync(admin);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        protected virtual Response<AdminDTODetails> AdminDtoDetailsResponse(Admin admin)
        {
            return new Response<AdminDTODetails>(
                new AdminDTODetails()
                {
                    Id = admin.Id,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    Email = admin.Email!,
                    Username = admin.UserName!
                });
        }
    }
}
