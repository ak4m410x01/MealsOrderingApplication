namespace MealsOrderingApplication.Domain.Models
{
    public class AuthanticationModel
    {
        public string Message { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; }

        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }

        public string? AccessToken { get; set; }
    }
}
