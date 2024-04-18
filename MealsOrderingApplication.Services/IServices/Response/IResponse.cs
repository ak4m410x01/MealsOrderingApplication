namespace MealsOrderingApplication.Services.IServices.Response
{
    public interface IResponse<T>
    {
        bool Succeeded { get; set; }
        string[]? Errors { get; set; }
        string? Message { get; set; }
        T? Result { get; set; }
    }
}
