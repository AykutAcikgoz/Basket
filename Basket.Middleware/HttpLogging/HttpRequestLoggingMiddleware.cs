using System;
using System.Net.Http;
using Basket.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Basket.Middleware
{
    public class HttpRequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IBasketLogger _logger;
        private readonly bool _isRequestLoggingEnabled;

        public HttpRequestLoggingMiddleware(RequestDelegate next, IConfiguration config, IBasketLogger logger, bool isRequestLoggingEnabled = false)
        {
            _next = next;
            _logger = logger;
            _isRequestLoggingEnabled = isRequestLoggingEnabled;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (_isRequestLoggingEnabled)
            {
                _logger.Information("Scheme: {scheme}, Host: {host}, Path: {path}, QueryString:{queryString}", context.Request.Scheme, context.Request.Host, context.Request.Path, context.Request.QueryString);
            }
            await _next(context);
        }
    }
}

