using System.Collections.Generic;
using System.Threading.Tasks;
using DearlerPlatform.Domain;
using DearlerPlatform.Service.OrderApp.Dto;
using DearlerPlatform.Service.ShoppingCartApp.Dto;

namespace DearlerPlatform.Service.OrderApp
{
    public interface IOrderService : IocTag
    {
        Task<bool> AddOrder(
            string customerNo,
            OrderMasterInputDto input,
            List<ShoppingCartDto> carts
        );

        Task<SaleOrderDto> GetOrderInfoByOrderNo(string orderNo);
        Task<bool> BuyAgain(string SaleOrderNo);
    }
}