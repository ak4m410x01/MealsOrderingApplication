namespace MealsOrderingApplication.Domain.Interfaces.Validations.ApplicationUserValidation
{
    public interface IBaseApplicationUserValidation : IBaseValidation
    {
        Task<bool> IsEmailExists(string email);
        Task<bool> IsUsernameExists(string username);
    }
}
