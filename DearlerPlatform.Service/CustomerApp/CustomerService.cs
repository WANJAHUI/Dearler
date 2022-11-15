using System;
using System.Threading.Tasks;
using AutoMapper;
using DearlerPlatform.Common.EventBusHelper;
using DearlerPlatform.Core.Repository;
using DearlerPlatform.Domain;
using DearlerPlatform.Service.OrderApp.Dto;

namespace DearlerPlatform.Service.CustomerApp
{
    public partial class CustomerService : ICustomerService
    {
        public CustomerService(
            IRepository<Customer> customerRepo,
            IRepository<CustomerInvoice> customerInvoiceRepo,
            IRepository<CustomerPwd> customerPwdRepo,
            IMapper mapper,
            LocalEventBus<SaleOrderDto> saleOrderDtoLocalEventBus
            )
        {
            CustomerRepo = customerRepo;
            CustomerInvoiceRepo = customerInvoiceRepo;
            CustomerPwdRepo = customerPwdRepo;
            Mapper = mapper;
         saleOrderDtoLocalEventBus.localEventHandler += SaleOrderDtoLocalEventHandler;
        }

       

        public IRepository<Customer> CustomerRepo { get; }
        public IRepository<CustomerInvoice> CustomerInvoiceRepo { get; }
        public IRepository<CustomerPwd> CustomerPwdRepo { get; }
        public IMapper Mapper { get; }

        public async Task<Customer> GetCustomerAsync(string customerNo){
            return await CustomerRepo.GetAsync(m=>m.CustomerNo == customerNo);
        }
    }
}