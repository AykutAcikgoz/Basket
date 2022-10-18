using System;
using Basket.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Basket.Middleware.ClientCookie
{
    public class ClientCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public ClientCookieMiddleware(RequestDelegate next, IConfiguration config, IBasketLogger logger, bool isRequestLoggingEnabled = false)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (string.IsNullOrEmpty(context.Request.Cookies["SESSION_ID"]))
            {
                context.Response.Cookies.Append("SESSION_ID", Guid.NewGuid().ToString(), new CookieOptions() { Expires = DateTime.Now.AddDays(1)});
            }
            await _next(context);
        }
    }
}

