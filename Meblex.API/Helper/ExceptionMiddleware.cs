using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
                _logger.LogError($"Something went wrong: {ex}", Activity.Current?.Id ?? httpContext.TraceIdentifier);
                
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}", Activity.Current?.Id ?? httpContext.TraceIdentifier);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(ToJson(new ProblemDetails()
            {
                Title = ReasonPhrases.GetReasonPhrase((int) HttpStatusCode.InternalServerError),
                Status = (int) HttpStatusCode.InternalServerError,
                Detail = exception.Message,
                Extensions =
                    {new KeyValuePair<string, object>("traceId", Activity.Current?.Id ?? context.TraceIdentifier)}
            }));
        }

        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCodeException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) exception.StatusCode;


            return context.Response.WriteAsync(ToJson(new ProblemDetails()
            {
                Title = ReasonPhrases.GetReasonPhrase((int)exception.StatusCode),
                Status = (int)exception.StatusCode,
                Detail = exception.Message,
                Extensions = { new KeyValuePair<string, object>("traceId", Activity.Current?.Id ?? context.TraceIdentifier) }
            }));
        }

        private static string ToJson(object obj)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(obj, serializerSettings);
        }
    }
}
