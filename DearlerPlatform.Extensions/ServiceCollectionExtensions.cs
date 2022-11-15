using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DearlerPlatform.Core.Repository;
using DearlerPlatform.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DearlerPlatform.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RepositoryRegister(this IServiceCollection services)
        {
            var asmCore = Assembly.Load("DearlerPlatform.Core");
            var implementationType = asmCore.GetTypes().FirstOrDefault(m =>
            m.Name == "Repository`1"
             );
            // var interfaceType = implementationType.GetInterfaces().FirstOrDefault();
            var interfaceType = implementationType.GetInterface("IRepository`1").GetGenericTypeDefinition();
            services.AddTransient(interfaceType, implementationType);
            return services;
        }
        public static IServiceCollection ServicesRegister(this IServiceCollection services)
        {
            List<Assembly> assemblys = new();

            var provider  = services.BuildServiceProvider();
            var configuration = provider.GetService<IConfiguration>();
            List<string> classes = configuration["IocClasses"].Split(",").ToList();

            classes.ForEach(c=>{
                assemblys.Add(Assembly.Load(c));
            });
            
            foreach (var assembly in assemblys)
            {
                var implementationTypes = assembly.GetTypes().Where(
                    m => m.IsAssignableTo(typeof(IocTag)) &&
                    !m.IsAbstract &&
                    !m.IsInterface
                );
                foreach (var implementationType in implementationTypes)
                {
                    var interfaceType = implementationType.GetInterfaces().Where(m => m != typeof(IocTag)).FirstOrDefault();
                    services.AddTransient(interfaceType, implementationType);
                }
            }
            return services;
        }
    }
}