using System;
using Microsoft.AspNetCore.Builder;

namespace Basket.Middleware.BasketException
{
    public static class BasketExceptionExtension
    {
        public static IApplicationBuilder UseBasketException(this IApplicationBuilder builder, bool isExceptionLoggingEnabled)
        {
            return builder.UseMiddleware<BasketExceptionMiddleware>(isExceptionLoggingEnabled);
        }
    }
}

