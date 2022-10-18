using System;
namespace Basket.Logger
{
    public interface IBasketLogger
    {
        void Information(string message);
        void Information(string messageTemplate, params object[] propertyValues);
        void Error(Exception exception, string message);
    }
}

