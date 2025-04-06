using BaseApi.Utilities;
using System.Net;
using System.Text.Json;

namespace BaseApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Excepcion atrapada: {ex.Message}");
                context.Response.ContentType = "application/json";

                var statusCode = ex switch
                {
                    UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    ArgumentException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                context.Response.StatusCode = statusCode;

                var response = new ApiResponse<string>(
                    message : "Ocurrio un error",
                    errors : new List<string> { ex.Message},
                    statusCode : statusCode
                );

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
