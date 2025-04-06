namespace BaseApi.Utilities
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public int StatusCode { get; set; }
        public List<string>? Errors { get; set; }

        public ApiResponse(T data, string message = "OK", int statusCode = 200)
        {
            Success = true;
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }

        public ApiResponse(string message, List<string>? errors = null, int statusCode = 400)
        {
            Success = false;
            Message = message;
            Errors = errors;
            StatusCode = statusCode;
        }
    }
}
