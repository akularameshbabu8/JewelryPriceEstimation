using JewelryEstimation.Api.Middlewares;
using JewelryEstimation.Application;
using JewelryEstimation.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Register controllers and API behavior
builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .Select(e => new
            {
                Field = e.Key,
                Errors = e.Value.Errors.Select(er => er.ErrorMessage)
            });

        var response = new
        {
            Status = 400,
            Message = "Validation failed",
            Errors = errors
        };

        return new BadRequestObjectResult(response);
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Application & Infrastructure layer registration
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Run app
var app = builder.Build();
// Register middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
public partial class Program { }