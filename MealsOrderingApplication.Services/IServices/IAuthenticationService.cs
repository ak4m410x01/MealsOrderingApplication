using MealsOrderingApplication.Domain.DTOs.AuthanticationDTO;
using MealsOrderingApplication.Domain.Models;

namespace MealsOrderingApplication.Services.IServices
{
    public interface IAuthenticationService
    {
        public Task<AuthanticationModel> RegisterAsync(RegisterDTO model);
        public Task<AuthanticationModel> LoginAsync(LoginDTO model);
    }
}
