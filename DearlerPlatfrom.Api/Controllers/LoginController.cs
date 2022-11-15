using System.Threading.Tasks;
using DearlerPlatform.Common.TokenModule;
using DearlerPlatform.Service.CustomerApp;
using DearlerPlatform.Service.CustomerApp.Dto;
using DearlerPlatfrom.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DearlerPlatform.Api.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(ICustomerService customerService, IConfiguration configuration)
        {
            CustomerService = customerService;
            Configuration = configuration;
        }

        public ICustomerService CustomerService { get; }
        public IConfiguration Configuration { get; }
        [HttpPost]
        public async Task<string> CheckLogin(CustomerLoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CustomerNo) || string.IsNullOrWhiteSpace(dto.Password))
            {
                HttpContext.Response.StatusCode = 400;
                return "NonLoginInfo";
            }
            var isSuccess = await CustomerService.CheckPassword(dto);
            if (isSuccess)
            {
                // TODO 获取真是用户数据
                var customer = await CustomerService.GetCustomerAsync(dto.CustomerNo);
                return GetToken(customer.Id, customer.CustomerNo, customer.CustomerName);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
                return "NonUser";
            }
        }

        private string GetToken(int customerId, string customerNo, string customerName)
        {
            var token = Configuration.GetSection("Jwt").Get<JwtTokenModel>();
            token.Id = customerId;
            token.CustomerNo = customerNo;
            token.CustomerName = customerName;
            return TokenHelper.CreateToken(token);
        }
    }
}