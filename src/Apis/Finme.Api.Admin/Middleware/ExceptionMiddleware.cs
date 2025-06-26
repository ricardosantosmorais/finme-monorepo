using Finme.Application.Admin.Common;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Finme.Api.Admin.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var errorResponse = exception switch
            {
                ValidationException validationEx => CreateValidationErrorResponse(validationEx),
                _ => CreateGenericErrorResponse(exception)
            };

            context.Response.StatusCode = errorResponse.Status;
            var json = JsonSerializer.Serialize(errorResponse);
            return context.Response.WriteAsync(json);
        }

        private static ErrorResponse CreateValidationErrorResponse(ValidationException ex)
        {
            var errorResponse = new ErrorResponse(
                title: "Validation Error",
                status: 400,
                detail: "One or more validation errors occurred."
            );
            foreach (var error in ex.Errors)
            {
                errorResponse.AddError(error.PropertyName, error.ErrorMessage);
            }
            return errorResponse;
        }

        private static ErrorResponse CreateGenericErrorResponse(Exception ex)
        {
            return ex switch
            {
                DuplicateEmailException dupEx => new ErrorResponse("Duplicate Email", 409, dupEx.Message),
                _ => new ErrorResponse("Internal Server Error", 500, ex.Message)
            };
        }
    }
}
