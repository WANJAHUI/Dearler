using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DearlerPlatform.Common.RedisModule
{
    public partial class RedisWorker : IRedisWorker
    {
        public RedisWorker(RedisCore redisCore)
        {
            RedisCore = redisCore;
        }

        public RedisCore RedisCore { get; }

        /// <summary>
        /// 通过Scan获取有的适配的KEY
        /// </summary>
        /// <param name="key">可以带通配符的Key</param>
        /// <returns></returns>
        public List<string> GetKeys(string key)
        {
            List<string> keyList = new();
            var eps = RedisCore.Conn.GetEndPoints();
            var ep = eps[0];
            // 通过EndPoints拿到Redis 的服务
            var server = RedisCore.Conn.GetServer(ep);
            // 通过Server拿到Redis中所有符合条件的Key
            var keys = server.Keys(0, key).ToList();
            keys.ForEach(k =>
            {
                keyList.Add(k.ToString());
            });
            return keyList;
        }

        public void RemoveKey(string key)
        {
            RedisCore.Db.KeyDelete(key);
        }
    }
}