using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Utilities
{
    public static class ResponseHelper
    {
        public static IActionResult OK<T>(T data, string message = "Operacion exitosa")
        {
            var response = new ApiResponse<T>(data, message, 200);
            return new OkObjectResult(response);
        }

        public static IActionResult Created<T>(T data, string message = "Recurso creado")
        {
            var response = new ApiResponse<T>(data, message, 201);
            return new ObjectResult(response) { StatusCode = 201 };
        }

        public static IActionResult BadRequest(string message = "Solicitud incorrecta", List<string>? errors = null)
        {
            var response = new ApiResponse<string>(message, errors, 400);
            return new BadRequestObjectResult(response);
        }

        public static IActionResult NotFound(string message = "Recurso no encontrado", List<string>? errors = null)
        {
            var response = new ApiResponse<string>(message, errors, 404);
            return new NotFoundObjectResult(response);
        }

        public static IActionResult InternalServerError(string message = "Error interno", List<string>? errors = null)
        {
            var response = new ApiResponse<string>(message, errors, 500);
            return new ObjectResult(response) { StatusCode = 500 };
        }
    }
}
