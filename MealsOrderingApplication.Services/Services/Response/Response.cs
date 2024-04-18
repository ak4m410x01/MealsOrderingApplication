using MealsOrderingApplication.Services.IServices.Response;

namespace MealsOrderingApplication.Services.Services.Response
{
    public class Response<T> : IResponse<T>
    {
        public Response()
        {
        }

        public Response(T result)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Result = result;
        }

        public bool Succeeded { get; set; }
        public string[]? Errors { get; set; }
        public string? Message { get; set; }
        public T? Result { get; set; }
    }
}