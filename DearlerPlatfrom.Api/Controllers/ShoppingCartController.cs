using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DearlerPlatform.Domain;
using DearlerPlatform.Service.ProductApp;
using DearlerPlatform.Service.ShoppingCartApp;
using DearlerPlatform.Service.ShoppingCartApp.Dto;
using DearlerPlatfrom.Api.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DearlerPlatfrom.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [CtmAuthorizationFilter]
    public class ShoppingCartController : BaseController
    {
        public ShoppingCartController(
            IShoppingCartAppService shoppingCartService,
            IProductService productService)
        {
            ShoppingCartService = shoppingCartService;
        }

        public IShoppingCartAppService ShoppingCartService { get; }

        [HttpPost]
        public async Task<ShoppingCart> SetShoppingCart(ShoppingCartInputDto input)
        {
            var customerNo = HttpContext.Items["CustomerNo"]?.ToString();
            input.CustomerNo = customerNo;
            return await ShoppingCartService.SetShoppingCart(input);
        }
        [HttpGet("num")]
        public async Task<int> GetShoppingcartNum()
        {
            var customerNo = HttpContext.Items["CustomerNo"]?.ToString();
            return await ShoppingCartService.GetShoppingCartNum(customerNo);
        }
        [HttpGet]
        public async Task<dynamic> GetShoppingCartDtosAsync()
        {
            var customerNo = HttpContext.Items["CustomerNo"]?.ToString();
            var carts = await ShoppingCartService.GetShoppingCartDtos(customerNo);
            var productDtos = carts.Select(m => m.ProductDto);
            var types = productDtos?.Select(m => new { TypeNo = m?.TypeNo, TypeName = m?.TypeName, TypeSelected = false }).Distinct();
            return new { carts, types };
        }
        [HttpPost("CartSelected")]
        public async Task<string> UpdateCartSelected(ShoppingCartSelectedEditDto edit)
        {
            var customerNo = HttpContext.Items["CustomerNo"]?.ToString();
            return await ShoppingCartService.UpdateCartSelected(edit,customerNo);
        }
    }
}