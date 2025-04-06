using BaseApi.Middleware;
using BaseApi.Utilities;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
        .Where(e => e.Value?.Errors.Count() > 0)
        .SelectMany(x => x.Value!.Errors)
        .Select(x => x.ErrorMessage)
        .ToList();

        var response = new ApiResponse<string>(
            message : "Error en la validacion de datos",
            errors : errors,
            statusCode : 400
        );

        return new BadRequestObjectResult( response );
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();


app.Run();