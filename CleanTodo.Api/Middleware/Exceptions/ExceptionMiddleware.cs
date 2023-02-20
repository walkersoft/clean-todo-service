using CleanTodo.Core.Exceptions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CleanTodo.Api.Middleware.Exceptions
{
    internal sealed class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = ex switch
            {
                EntityNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.ContentType = "application/json";
            var payload = new ExceptionResponse { StatusCode = context.Response.StatusCode, Message = ex.Message };
            
            await context.Response.WriteAsync(
                JsonSerializer.Serialize(payload, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                })
            );
        }
    }
}
