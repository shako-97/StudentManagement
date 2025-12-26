using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using FluentValidation;
namespace StudentManagement.Application.Shared.Middlewares
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
            catch (Exception error)
            {
                var response = context.Response;
                var errorTypeName = error.GetType().Name;
                response.ContentType = "application/json";

                switch (errorTypeName)
                {
                    case nameof(KeyNotFoundException):
                        await this.HandleKeyNotFoundException(error as KeyNotFoundException, response);
                        break;
                    case nameof(ValidationException):
                        await this.HandleValidationException(error as ValidationException, response);
                        break;
                    default:
                        await this.HandleUnknownException(error, response);
                        break;
                }
            }
        }

        private async Task HandleValidationException(ValidationException exception, HttpResponse response)
        {
            await this.HandleException(response, HttpStatusCode.BadRequest, exception.Errors.Select(failure => failure.ErrorMessage), "Validation error");

        }

        private async Task HandleUnknownException(Exception exception, HttpResponse response)
        {
            await this.HandleException(response, HttpStatusCode.InternalServerError, new List<string> { exception.Message }, "Internal Server Error");
        }

        private async Task HandleKeyNotFoundException(KeyNotFoundException exception, HttpResponse response)
        {
            await this.HandleException(response, HttpStatusCode.NotFound, new List<string> { exception.Message }, "Key not found error");
        }

        private async Task HandleException(
            HttpResponse response,
            HttpStatusCode httpStatusCode,
            IEnumerable<string> errors,
            string message = "Internal System Error")
        {
            response.StatusCode = (int)httpStatusCode;

            var result = JsonSerializer.Serialize(new
            {
                message = message,
                errors = errors.ToList()
            });

            await response.WriteAsync(result);
        }
    }
}
