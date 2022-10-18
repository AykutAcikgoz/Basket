using System;
namespace Basket.Store
{
    public interface IBasketStore
    {
        bool Set<T>(string key, T value) where T : class;

        Task<bool> SetAsync(string key, object value);

        T Get<T>(string key) where T : class;

        Task<T> GetAsync<T>(string key) where T : class;

        bool Remove(string key);
    }
}

