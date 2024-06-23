using ExchangeBook.Services.Exceptions;
using System.Net;
using System.Text.Json;

namespace ExchangeBook.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = exception switch
                {
                    InvalidRegistrationException or
                    InvalidRoleException or
                    UserAlreadyExistsException or
                    PersonAlreadyExistsException => (int)HttpStatusCode.BadRequest,

                    UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                    ForbiddenException => (int)HttpStatusCode.Forbidden,
                    UserNotFoundException => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var result = JsonSerializer.Serialize(new { message = exception?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}