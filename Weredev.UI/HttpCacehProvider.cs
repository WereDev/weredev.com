using System;
using Microsoft.Extensions.Caching.Memory;
using Weredev.UI.Domain.Interfaces;

namespace Weredev.UI {
    public class HttpCacheProvider : ICacheProvider
    {
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _expiration = new TimeSpan(1, 0, 0);

        public HttpCacheProvider(IMemoryCache memoryCache) {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public T Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            
            var standardizedKey = StandardizeKey(key);

            return _memoryCache.TryGetValue<T>(standardizedKey, out T t)
                    ? t
                    : default;
        }

        public void Set<T>(string key, T item)
        {
            Set(key, item, _expiration);
        }

        public void Set<T>(string key, T item, TimeSpan expiration)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            var standardizedKey = StandardizeKey(key);

            if (item == null)
                _memoryCache.Remove(standardizedKey);
            else
                _memoryCache.Set(standardizedKey, item, expiration);
        }

        private string StandardizeKey(string key) {
            return key.Trim().ToLower();
        }
    }
}