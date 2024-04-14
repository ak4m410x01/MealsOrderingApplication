using MealsOrderingApplication.Domain.DTOs.AuthanticationDTO;
using MealsOrderingApplication.Domain.Models;

namespace MealsOrderingApplication.Services.IServices
{
    public interface IAuthenticationService
    {
        Task<AuthanticationModel> RegisterAsync(RegisterDTO model);
        Task<AuthanticationModel> LoginAsync(LoginDTO model);
    }
}
