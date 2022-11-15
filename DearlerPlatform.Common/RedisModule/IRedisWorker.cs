using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DearlerPlatform.Domain;
using StackExchange.Redis;

namespace DearlerPlatform.Common.RedisModule
{
    public interface IRedisWorker : IocTag
    {
        void RemoveKey(string key);
        string GetString(string key);
        Task<string> GetStringAsync(string key);
        void SetString(string key, string value, TimeSpan ts);
        Task SetStringAsync(string key, string value, TimeSpan ts);
        void SetHashMemory<T>(string key, T entity, Type type = null);
        void SetHashMemory<T>(string key, IEnumerable<T> entities, Func<T, IEnumerable<string>> func);
        List<T> GetHashMemory<T>(string keyLike) where T : new();
    }
}