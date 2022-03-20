using System;
using System.Threading.Tasks;
using Cache.Model;
using Microsoft.Extensions.Caching.Memory;

namespace Cache
{
    public class CachingInMemoryService : ICachingInMemory
    {
        #region 
        private bool _disposed;
        #endregion
        private readonly IMemoryCache _memoryCache;

        #region ctro
        public CachingInMemoryService(IMemoryCache memoryCache)
        {
            memoryCache = _memoryCache;
        }
        #endregion
        
        #region funcrion
        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> CreateAsync<T>(CacheModel key, object data)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync<T>(CacheModel key, Func<T> acquire)
        {
            throw new NotImplementedException();
        }

        public T Read<T>(CacheModel key, Func<T> acquire)
        {
            throw new NotImplementedException();
        }

        public Task<T> ReadAsync<T>(CacheModel key, Func<Task<T>> acquire)
        {
            throw new NotImplementedException();
        }

        public Task<T> ReadAsync<T>(CacheModel key, Func<T> acquire)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync<T>(CacheModel key, Func<T> acquire)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dispose cache manager
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private void Delete(CacheModel cacheKey, params object[] cacheKeyParameters)
        {
            
        }
        #endregion
    }
}