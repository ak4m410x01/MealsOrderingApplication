using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.AdminDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        public AdminsController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        // Retrieve All Admins
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            IQueryable<Admin> admins = await _unitOfWork.Admins.GetAllAsync();

            return Ok(admins.Select(c => new AdminDTODetails()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email!,
                Username = c.UserName!
            }));
        }


        // Add new Admin
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddAdminDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            AuthanticationModel authModel = await _unitOfWork.Admins.CreateAsync(dto);
            if (!authModel.IsAuthenticated)
                return BadRequest(new { error = authModel.Message });

            await _unitOfWork.CompleteAsync();

            return Ok(new
            {
                UserId = authModel.UserId,
                Email = authModel.Email,
                Username = authModel.UserName,
            });
        }


        // Retrieve Admin By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            Admin? admin = await _unitOfWork.Admins.GetByIdAsync(id);
            if (admin is null)
                return NotFound(new { error = "No Admins found with this Id" });

            return Ok(new AdminDTODetails()
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email!,
                Username = admin.UserName!
            });
        }


        // Update exists Admin
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, UpdateAdminDTO dto)
        {
            Admin? admin = await _unitOfWork.Admins.GetByIdAsync(id);
            if (admin is null)
                return NotFound(new { error = "No Admins found with this Id" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _unitOfWork.Admins.UpdateAsync(admin, dto);
            await _unitOfWork.CompleteAsync();

            return Ok(new AdminDTODetails()
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email!,
                Username = admin.UserName!
            });
        }


        // Delete Admin
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            Admin? admin = await _unitOfWork.Admins.GetByIdAsync(id);
            if (admin is null)
                return NotFound(new { error = "No Admins found with this Id" });

            await _unitOfWork.Admins.DeleteAsync(admin);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
