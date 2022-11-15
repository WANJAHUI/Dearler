using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DearlerPlatform.Common.EventBusHelper;
using DearlerPlatform.Common.RedisModule;
using DearlerPlatform.Common.TokenModule;
using DearlerPlatform.Core;
using DearlerPlatform.Domain;
using DearlerPlatform.Extensions;
using DearlerPlatform.Service;
using DearlerPlatform.Service.CustomerApp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace DearlerPlatfrom.Api.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ServiceEnter(this IServiceCollection services)
        {
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped(typeof(LocalEventBus<>));
            services.AddSingleton<RedisCore>();
            // services.AddTransient<IRedisWorker, RedisWorker>();
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddCors(c => c.AddPolicy("any", p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            var Configuration = services.GetConfiguration();
            var token = Configuration.GetSection("Jwt").Get<JwtTokenModel>();
            #region Jwt验证
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                opt =>
                {
                    // 是否是Https，默认true
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Security)),
                        ValidIssuer = token.Issuer,
                        ValidAudience = token.Audience
                    };
                    opt.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            //此处终止代码
                            context.HandleResponse();
                            var res = "{\"code\":401,\"err\":\"无权限\"}";
                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.WriteAsync(res);
                            return Task.FromResult(0);
                        }
                    };
                }
            );
            #endregion


            services.AddDbContext<DealerPlatformContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            // 第一步：引入Nuget包 AutoMapper 和 AutoMapper.Extensions.Microsoft.DependencyInjection
            // 第二步：创建映射类
            // 第三步：将Automapper注册到系统中，并且添加实体映射类
            services.AddAutoMapper(typeof(DealerPlatformProfile));
            services.RepositoryRegister();
            services.ServicesRegister();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DearlerPlatform.Web", Version = "v1" });

                // 添加安全定义
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "格式：Bearer {token}",
                    Name = "Authorization", // 默认的参数名
                    In = ParameterLocation.Header,// 放于请求头中
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                // 添加安全要求
                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                   {
                        new OpenApiSecurityScheme{
                             Reference = new OpenApiReference{
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, new string[]{}
                   }
                });
            });
        }

        public static IConfiguration GetConfiguration(this IServiceCollection services)
        {
            // var configration = services.FirstOrDefault(d => d.ServiceType == typeof(IConfiguration)).ImplementationInstance;
            var configration = services.BuildServiceProvider().GetService<IConfiguration>();
            return (IConfiguration)configration;
        }
    }
}