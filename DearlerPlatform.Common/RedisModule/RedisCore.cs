using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace DearlerPlatform.Common.RedisModule
{
    /// <summary>
    /// 核心类，连接redis并获取redis数据库
    /// </summary>
    public class RedisCore
    {
        public ConnectionMultiplexer Conn { get; set; }
        public IDatabase Db { get; set; }
        public RedisCore(IConfiguration configuration)
        {
            var redisConnectionStr = configuration["Redis"];
            ConfigurationOptions configurationOptions = ConfigurationOptions.Parse(redisConnectionStr);
            // 如果没有这句话，我们无法模糊搜索Key，因为权限不够
            configurationOptions.AllowAdmin = true;
            Conn = ConnectionMultiplexer.Connect(configurationOptions);
            Db = Conn.GetDatabase();
        }
        public void RemoveKey(string key)
        {
            Db.KeyDelete(key);
        }
    }
}