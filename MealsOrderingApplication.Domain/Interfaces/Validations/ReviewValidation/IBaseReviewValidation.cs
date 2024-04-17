namespace MealsOrderingApplication.Domain.Interfaces.Validations.ReviewValidation
{
    public interface IBaseReviewValidation : IBaseValidation
    {
        Task<bool> IsProductExists(int productId);
        Task<bool> IsCustomerExists(string customerId);
    }
}
