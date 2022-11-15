using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DearlerPlatform.Core.Consts;
using DearlerPlatform.Service.CustomerApp;
using DearlerPlatform.Service.OrderApp;
using DearlerPlatform.Service.OrderApp.Dto;
using DearlerPlatfrom.Api.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DearlerPlatfrom.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [CtmAuthorizationFilter]
    public class OrderInfoController : BaseController
    {
        public OrderInfoController(IOrderService orderService,ICustomerService customerService)
        {
            OrderService = orderService;
        }

        public IOrderService OrderService { get; }

        [HttpGet]
        public async Task<SaleOrderDto> GetSaleOrderDto(string orderNo)
        {
            // var customerNo = HttpContext.Items[HttpContextItemKeyName.CUSTOMER_NO].ToString();
           return await OrderService.GetOrderInfoByOrderNo(orderNo);
        }
    }
}