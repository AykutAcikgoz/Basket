using System;
using Serilog;

namespace Basket.Logger
{
    public class BasketLogger : IBasketLogger
    {
        public void Information(string messageTemplate, params object[] propertyValues)
        {
            Log.Information(messageTemplate, propertyValues);
        }

        public void Information(string message)
        {
            Log.Information(message);
        }

        public void Error(Exception exception, string message)
        {
            Log.Error(exception, message);
        }

    }
}

