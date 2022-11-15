using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DearlerPlatform.Domain;
using DearlerPlatform.Service.ShoppingCartApp.Dto;

namespace DearlerPlatform.Service.OrderApp;

public partial class OrderService
{
    private async Task AddOrderDetail(List<ShoppingCartDto> carts,
    string customerNo,
    string orderNo,
    DateTime inputDate)
    {
        foreach (var cart in carts)
        {
            SaleOrderDetail detail = new()
            {
                SaleOrderGuid = Guid.NewGuid().ToString(),
                SaleOrderNo = orderNo,
                ProductNo = cart.ProductNo,
                ProductName = cart.ProductDto.ProductName,
                ProductPhotoUrl = cart.ProductDto.ProductPhoto?.ProductPhotoUrl,
                CustomerNo = customerNo,
                InputDate = inputDate,
                OrderNum = cart.ProductNum,
                BasePrice = cart.ProductDto.ProductSale?.SalePrice ?? 0,
                DiffPrice = 0,
                SalePrice = cart.ProductDto.ProductSale?.SalePrice ?? 0
            };
            await OrderDetailRepo.InsertAsync(detail);
        }
    }
    private async Task<List<SaleOrderDetail>> GetOrderDetailsByOrderNo(string orderNo)
    {
        return await OrderDetailRepo.GetListAsync(m=>m.SaleOrderNo == orderNo);
    }
}