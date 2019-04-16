using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace VsExample.AspAPI
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ApiErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
        static ApiErrorMiddleware()
        {
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }

        public ApiErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (httpContext.Response.HasStarted)
                {
                    Console.WriteLine($"The error handling middleware has started. ");
                }
                else
                {
                    httpContext.Response.Clear();
                    if (ex is MvcException)
                    {
                        var mvc = ex as MvcException;
                        httpContext.Response.StatusCode = mvc.StatusCode;
                        httpContext.Response.ContentType = "application/json";
                    }
                    else
                    {
                        httpContext.Response.StatusCode = 500;
                        httpContext.Response.ContentType = "application/json";
                    }
                    httpContext.Response.Headers.Add("api-error", "true");
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(ex, jsonSerializerSettings));
                    return;
                }
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ApiErrorMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiErrorMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiErrorMiddleware>();
        }
    }
}
