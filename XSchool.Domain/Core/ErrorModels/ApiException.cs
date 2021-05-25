namespace XSchool.Domain.Core.ErrorModels
{
    public class ApiException : ApiResponse
    {
        public string Details { get; protected set; }

        public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }
    }
}
