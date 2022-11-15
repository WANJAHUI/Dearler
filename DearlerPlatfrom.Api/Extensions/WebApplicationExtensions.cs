using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace DearlerPlatfrom.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void InitEnter(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseCors("any");


            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseAuthorization();
        }
        public static void InitMap(this IEndpointRouteBuilder app)
        {

            app.MapControllers();

            app.MapGet("/app/Test", () =>
            {
                return "Ace";
            });

        }
    }
}