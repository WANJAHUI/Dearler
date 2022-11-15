using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DearlerPlatform.Core.Repository;
using DearlerPlatform.Domain;
using DearlerPlatform.Service.CustomerApp.Dto;
using DearlerPlatform.Service.OrderApp.Dto;

namespace DearlerPlatform.Service.CustomerApp
{
    public partial class CustomerService
    {
        private async Task SaleOrderDtoLocalEventHandler(SaleOrderDto saleOrderDto)
        {
            saleOrderDto.CustomerInvoice = await GetCustomerInvoiceAsync(saleOrderDto.InvoiceNo);
        }
        public async Task<List<InvoiceOfOrderConfirmDto>> GetInvoicesByUser(string customerNo)
        {
            var invoices = await CustomerInvoiceRepo.GetListAsync(m => m.CustomerNo == customerNo);
            return Mapper.Map<List<CustomerInvoice>, List<InvoiceOfOrderConfirmDto>>(invoices);
        }

        private async Task<CustomerInvoice> GetCustomerInvoiceAsync(string invoiceNo)
        {
            return await CustomerInvoiceRepo.GetAsync(m => m.InvoiceNo == invoiceNo);
        }
    }
}