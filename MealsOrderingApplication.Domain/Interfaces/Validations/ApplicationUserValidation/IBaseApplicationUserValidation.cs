namespace MealsOrderingApplication.Domain.Interfaces.Validations.ApplicationUserValidation
{
    public interface IBaseApplicationUserValidation : IBaseValidation
    {
        Task<bool> IsEmailExistsAsync(string email);
        Task<bool> IsUsernameExistsAsync(string username);
    }
}
