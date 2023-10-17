using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FirstWebApp.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ShimonMiddleware
    {
        private readonly RequestDelegate _next;

        public ShimonMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("ShimonHeaderKey", DateTime.Now.ToLongTimeString());
            await httpContext.Response.WriteAsync(" FROM SHIMON MIDDLEWARE A ");
            await _next(httpContext);
            await httpContext.Response.WriteAsync(" FROM SHIMON MIDDLEWARE B");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ShimonMiddlewareExtensions
    {
        public static IApplicationBuilder UseShimonMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ShimonMiddleware>();
        }
    }
}
