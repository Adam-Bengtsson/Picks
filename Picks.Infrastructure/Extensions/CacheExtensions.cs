using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Picks.Infrastructure.Extensions
{
    public static class CacheExtensions
    {
        public static async Task<bool> SetValueAsync<T>(this IDistributedCache cache, string key, T value)
        {
            await cache.SetStringAsync(key, JsonConvert.SerializeObject(value));
            return true;
        }

        public static async Task<T> GetValueAsync<T>(this IDistributedCache cache, string key)
        {
            var value = await cache.GetStringAsync(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static bool SetValue<T>(this IDistributedCache cache, string key, T value)
        {
            cache.SetString(key, JsonConvert.SerializeObject(value));
            return true;
        }

        public static T GetValue<T>(this IDistributedCache cache, string key)
        {
            var value = cache.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
