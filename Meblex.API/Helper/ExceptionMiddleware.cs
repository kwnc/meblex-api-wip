using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Meblex.API.Helper
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (HttpStatusCodeException ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ExceptionDetails()
            {
                Title = HttpStatusCode.InternalServerError.ToString(),
                Error = exception.Message,
                Status = (int) HttpStatusCode.InternalServerError,
                TraceId = Activity.Current?.Id ?? context.TraceIdentifier
            }.ToString());
        }

        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCodeException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) exception.StatusCode;

            return context.Response.WriteAsync(new ExceptionDetails()
            {
                Title = exception.StatusCode.ToString(),
                Error = exception.Message,
                Status = (int) exception.StatusCode,
                TraceId = Activity.Current?.Id ?? context.TraceIdentifier
            }.ToString());
        }
    }
}
