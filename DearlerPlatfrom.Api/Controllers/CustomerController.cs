using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DearlerPlatform.Core.Consts;
using DearlerPlatform.Service.CustomerApp;
using DearlerPlatform.Service.CustomerApp.Dto;
using DearlerPlatfrom.Api.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DearlerPlatfrom.Api.Controllers
{
     [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [CtmAuthorizationFilter]
    public class CustomerController : BaseController
    {
        public CustomerController(ICustomerService customerService)
        {
            CustomerService = customerService;
        }

        public ICustomerService CustomerService { get; }
        
        [HttpGet("Invoice")]
        public async Task<List<InvoiceOfOrderConfirmDto>> Get(){
            var cno = HttpContext.Items[HttpContextItemKeyName.CUSTOMER_NO]?.ToString();
            return await CustomerService.GetInvoicesByUser(cno);
        }
    }
}