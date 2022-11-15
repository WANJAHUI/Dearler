using System.Collections.Generic;
using System.Threading.Tasks;
using DearlerPlatform.Domain;
using DearlerPlatform.Service.CustomerApp.Dto;
using DearlerPlatform.Service.OrderApp.Dto;

namespace DearlerPlatform.Service.CustomerApp
{
    public interface ICustomerService : IocTag
    {
        Task<Customer> GetCustomerAsync(string customerNo);
        Task<bool> CheckPassword(CustomerLoginDto dto);
        Task<List<InvoiceOfOrderConfirmDto>> GetInvoicesByUser(string customerNo);
    }
}