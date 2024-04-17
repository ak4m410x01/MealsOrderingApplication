namespace MealsOrderingApplication.Domain.Interfaces.Validations.OrderValidation
{
    public interface IBaseOrderValidation : IBaseValidation
    {
        Task<bool> IsProductExistsAsync(int productId);
    }
}
