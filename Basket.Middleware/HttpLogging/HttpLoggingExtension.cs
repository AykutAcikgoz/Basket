using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Basket.Middleware
{
    public static class HttpLoggingExtension
    {
        public static IApplicationBuilder UseHttpRequestLogging(this IApplicationBuilder builder, bool isRequestLoggingEnabled)
        {
            return builder.UseMiddleware<HttpRequestLoggingMiddleware>(isRequestLoggingEnabled);
        }
    }
}

