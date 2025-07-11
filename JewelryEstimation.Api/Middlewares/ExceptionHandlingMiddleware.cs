// File: Middlewares/ExceptionHandlingMiddleware.cs
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace JewelryEstimation.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Continue down pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception");

                context.Response.ContentType = "application/json";

                var statusCode = (int)HttpStatusCode.InternalServerError;

                var response = new
                {
                    Status = statusCode,
                    Message = "An unexpected error occurred.",
                    Detail = ex.Message // Remove in production for security
                };

                context.Response.StatusCode = statusCode;

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
