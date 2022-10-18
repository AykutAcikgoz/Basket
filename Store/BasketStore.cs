using System;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Basket.Store
{
    public class BasketStore : IBasketStore
    {
        private readonly ConnectionMultiplexer _client;

        public BasketStore(IOptions<BasketStoreSettings> basketStoreOptions)
        {
            var settings = basketStoreOptions.Value;
            ConfigurationOptions options = new ConfigurationOptions
            {
                EndPoints =
                {
                    settings.ConnectionString
                }
            };

            _client = ConnectionMultiplexer.Connect(options);
        }

        public T Get<T>(string key) where T : class
        {
            string value = _client.GetDatabase().StringGet(key);

            return value.ToObject<T>();
        }

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            string value = await _client.GetDatabase().StringGetAsync(key);

            return value.ToObject<T>();
        }

        public bool Remove(string key)
        {
            return _client.GetDatabase().KeyDelete(key);
        }

        public bool Set<T>(string key, T value) where T : class
        {
            return _client.GetDatabase().StringSet(key, value.ToJson());
        }

        public async Task<bool> SetAsync(string key, object value)
        {
            return await _client.GetDatabase().StringSetAsync(key, value.ToJson());
        }

    }
}

