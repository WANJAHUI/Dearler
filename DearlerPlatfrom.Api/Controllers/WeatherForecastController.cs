using DearlerPlatform.Common.RedisModule;
using DearlerPlatfrom.Api.Models;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace DearlerPlatfrom.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IRedisWorker redisWorker,
            RedisCore redisCore
            )
        {
            _logger = logger;
            RedisWorker = redisWorker;
            RedisCore = redisCore;
        }

        public IRedisWorker RedisWorker { get; }
        public RedisCore RedisCore { get; }

        [HttpPost]
        public string Get()
        {
            List<UserInfo> userInfos = new();
            userInfos.Add(
                new()
                {
                    Id = 1,
                    UserName = "Ace",
                    Age = 18
                }
            );
            userInfos.Add(
                new()
                {
                    Id = 2,
                    UserName = "Taro",
                    Age = 16
                }
            );
            userInfos.Add(
                new()
                {
                    Id = 3,
                    UserName = "Leo",
                    Age = 15
                }
            );
            // RedisWorker.SetHashMemory<UserInfo>("ultraman",userInfos,m=>{
            //     var list = new List<string>();
            //     list.Add(m.Id.ToString());
            //     list.Add(m.UserName);
            //     return list;
            //     });
            RedisWorker.SetHashMemory<UserInfo>("ultraman", userInfos, m => new[] { m.Id.ToString(), m.UserName });
            RedisWorker.SetHashMemory<UserInfo>("user", new UserInfo { Id = 6, UserName = "Jack", Age = 20 });
            return default;
        }
        [HttpGet("hash")]
        public List<UserInfo> GetHashEntries()
        {
            RedisCore.RemoveKey("cart:bfd54bbc-46a2-4017-a924-83870d2e3668:AHAQWYZ");
           return  RedisWorker.GetHashMemory<UserInfo>("ultraman:*:*");
        }
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
    }
}