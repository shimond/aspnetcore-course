using FirstWebApp.Models.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace FirstWebApp.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RemoveHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public RemoveHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(
            HttpContext httpContext, 
            IOptionsSnapshot<HeaderRemoveConfig> options, 
            IProductsRepository productsRepo)
        {
            var headerConfig = options.Value;
            if (headerConfig.Enabled)
            {
                foreach (var headerKey in headerConfig.HeaderKeys)
                {
                    httpContext.Response.Headers.Remove(headerKey);
                }
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RemoveHeadersMiddlewareExtensions
    {
        public static IApplicationBuilder UseRemoveHeadersMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RemoveHeadersMiddleware>();
        }
    }
}
