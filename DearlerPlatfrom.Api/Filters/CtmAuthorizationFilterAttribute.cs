using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DearlerPlatform.Core.Consts;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DearlerPlatfrom.Api.Filters
{
    public class CtmAuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)user.Identity;
            foreach (var claim in claimsIdentity.Claims)
            {
                if (claim.Type == HttpContextItemKeyName.CUSTOMER_NO)
                {
                    context.HttpContext.Items.Add(HttpContextItemKeyName.CUSTOMER_NO, claim.Value);
                }
            }
        }
    }
}