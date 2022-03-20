using System;
using System.Threading.Tasks;
using Cache.Model;

namespace Cache
{
    /// <summary>
    /// Hành động cục bộ trên máy chủ HTTP
    /// </summary>
    public interface ICachingInMemory : IDisposable
    {
        /// <summary> Tạo mới một cache
        /// </summary>
        /// <param name="key">Khóa nhận diện </param>
        /// <param name="data">Dữ liệu đầu vào</param>
        /// <typeparam name="T">Type Cache</typeparam>
        /// <returns></returns>
        Task<T> CreateAsync<T>(CacheModel key, object data);
        /// <summary> Đọc cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="acquire"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> ReadAsync<T>(CacheModel key, Func<Task<T>> acquire);
        Task<T> ReadAsync<T>(CacheModel key, Func<T> acquire);
        T Read<T>(CacheModel key, Func<T> acquire);
        Task<T> UpdateAsync<T>(CacheModel key, Func<T> acquire);
        Task<T> DeleteAsync<T>(CacheModel key, Func<T> acquire);
        Task ClearAsync();
    }
}