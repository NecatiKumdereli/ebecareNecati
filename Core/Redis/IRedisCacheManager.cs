using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Redis
{
    public interface IRedisCacheManager
    {
        bool Clear(string key);
        Task<bool> ClearAsync(string key);
        string GetValue(string key);
        Task<string> GetValueAsync(string key);
        T Get<T>(string key);
        Task<T> GetAsync<T>(string key);

        bool SetValue(string key, string value, int ExpireTime);
        Task<bool> SetValueAsync(string key, string value, int ExpireTime);

        T GetOrAdd<T>(string key, Func<T> action, int expireTime) where T : class;
        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> action, int expireTime) where T : class;

        bool Any(string key);
        Task<bool> AnyAsync(string key);

        IEnumerable<string> GetAllKeys();
    }
}
