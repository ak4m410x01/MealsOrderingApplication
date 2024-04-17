namespace MealsOrderingApplication.Domain.Interfaces.Validations.ReviewValidation
{
    public interface IBaseReviewValidation : IBaseValidation
    {
        Task<bool> IsProductExistsAsync(int productId);
        Task<bool> IsCustomerExistsAsync(string customerId);
    }
}
