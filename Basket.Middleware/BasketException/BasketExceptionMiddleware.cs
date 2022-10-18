using System;
using System.Net;
using Basket.Logger;
using Basket.Service.Payload;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Basket.Middleware.BasketException
{
    public class BasketExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IBasketLogger _logger;
        private readonly bool _isExceptionLoggingEnabled;

        public BasketExceptionMiddleware(RequestDelegate next, IConfiguration config, IBasketLogger logger, bool isExceptionLoggingEnabled = false)
        {
            _next = next;
            _logger = logger;
            _isExceptionLoggingEnabled = isExceptionLoggingEnabled;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try {
                await _next(context);
            }
            catch(Exception ex)
            {
                if (_isExceptionLoggingEnabled) _logger.Error(ex, "Internal Server Error");
                await HandleExceptionAsync(context, ex);
            }
            
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorPayload()
            {
                HttpStatusCode = context.Response.StatusCode,
                Exception = exception,
                ErrorMessage = "Internal Server Error"
            }));
        }
    }
}

