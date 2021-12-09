using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace CacheExtention.Caching
{
    /*  Cách sử dụng
     *  DI add như bình thường
     *  Khởi tạo new :
     *  private readonly static MemoryCache myCache = new MemoryCache(new MemoryCacheOptions());
     *  private ICacheBase _cache => new CacheMemoryHelper(myCache, true);
     *  private string _cacheKey;
     */
    public class CacheMemoryHelper : ICacheBase
    {
        private readonly IMemoryCache _cache;
        public CacheMemoryHelper(IMemoryCache cache)
        {
            this._cache = cache;
        }

        public void Add<T>(T o, string key)
        {
            // Look for cache key.
            if (!_cache.TryGetValue(key, out T _))
            {
                // Key not in cache, so get data.
                T cacheEntry = o;

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(7200)); // 2h

                // Save data in cache.
                _cache.Set(key, cacheEntry, cacheEntryOptions);
            }
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
