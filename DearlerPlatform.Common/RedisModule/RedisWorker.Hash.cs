using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DearlerPlatform.Domain;
using Microsoft.Extensions.ObjectPool;
using StackExchange.Redis;

namespace DearlerPlatform.Common.RedisModule
{
    public partial class RedisWorker
    {
        /// <summary>
        /// Hash存储单纯的键值对
        /// </summary>
        public void SetHashMemory(string key, Dictionary<string, string> values)
        {
            var hashEntrys = new List<HashEntry>();
            foreach (var value in values)
            {
                hashEntrys.Add(new HashEntry(value.Key, value.Value));
            }
            SetHashMemory(key, hashEntrys.ToArray());
        }
        /// <summary>
        /// Hash存储单纯的键值对
        /// </summary>
        public void SetHashMemory(string key, params HashEntry[] entries)
        {
            RedisCore.Db.HashSet(key, entries);
        }

        public void SetHashMemory<T>(string key, T entity, Type type = null)
        {
            type ??= typeof(T);
            List<HashEntry> hashEntntries = new();
            PropertyInfo[] props = type.GetProperties();
            foreach (var prop in props)
            {
                string name = prop.Name;
                object value = prop.GetValue(entity);
                if (value.GetType().Name == "Boolean") value = (bool)value ? 1 : 0;
                {
                    hashEntntries.Add(new HashEntry(name, value?.ToString()));
                }
            }
            SetHashMemory(key, hashEntntries.ToArray());
        }

        public void SetHashMemory<T>(string key, IEnumerable<T> entities, Func<T, IEnumerable<string>> func)
        {
            Type type = typeof(T);
            foreach (var entity in entities)
            {
                var valueKeys = func(entity);
                SetHashMemory($"{key}:{string.Join(":", valueKeys)}", entity, type);
            }
        }

        // public List<HashEntry> GetHashMemory(string key){
        //     var res  = RedisCore.Db.HashGetAll(key);
        //     var list = res.ToList();
        //     return list;
        // }

        public List<T> GetHashMemory<T>(string keyLike) where T : new()
        {
            var keys = GetKeys(keyLike);
            List<T> ts = new();
            foreach (var key in keys)
            {
                T t = new();
                // 这里拿到的其实是一个集合
                // 我们要循环这个集合，拿到里面的每一个kv
                var res = RedisCore.Db.HashGetAll(key);
                var props = t.GetType().GetProperties();
                foreach (var item in res)
                {
                    foreach (var prop in props)
                    {
                        if (prop.Name == item.Name)
                        {
                            prop.SetValue(t, Convert.ChangeType(item.Value, prop.PropertyType));
                            break;
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }

    }
}