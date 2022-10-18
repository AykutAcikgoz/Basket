using System;
using Microsoft.AspNetCore.Builder;

namespace Basket.Middleware.ClientCookie
{
    public static class ClientCookieExtension
    {
        public static IApplicationBuilder UseClientCookie(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ClientCookieMiddleware>();
        }
    }
}

