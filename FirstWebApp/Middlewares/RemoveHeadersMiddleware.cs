using Microsoft.Extensions.Options;

namespace FirstWebApp.MiddleWares;

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

public static class RemoveHeadersMiddlewareExtensions
{
    public static IApplicationBuilder UseRemoveHeadersMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RemoveHeadersMiddleware>();
    }
}
