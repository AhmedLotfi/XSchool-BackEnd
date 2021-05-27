namespace XSchool.Domain.Core.ErrorModels
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null, object data = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            Data = data;
        }

        public bool Success { get; set; } = true;

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            this.Success = statusCode switch
            {
                400 => false,
                401 => false,
                404 => false,
                500 => false,
                _ => true
            };

            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side. Errors lead to anger.  Anger leads to hate.  Hate leads to career change",
                _ => null
            };
        }
    }
}
