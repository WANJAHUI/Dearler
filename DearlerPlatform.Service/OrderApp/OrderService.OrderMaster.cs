using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using DearlerPlatform.Domain;
using DearlerPlatform.Service.OrderApp.Dto;
using DearlerPlatform.Service.ShoppingCartApp.Dto;

namespace DearlerPlatform.Service.OrderApp
{
    public partial class OrderService
    {
        public async Task<bool> AddOrder(
            string customerNo,
            OrderMasterInputDto input,
            List<ShoppingCartDto> carts
        )
        {
            using TransactionScope ts = new(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                // 添加主订单
                DateTime inputDate = DateTime.Now;
                string orderNo = Guid.NewGuid().ToString();
                SaleOrderMaster master = new SaleOrderMaster
                {
                    CustomerNo = customerNo,
                    DeliveryDate = input.DeliveryDate,
                    EditUserNo = customerNo,
                    InputDate = inputDate,
                    Remark = input.Remark,
                    InvoiceNo = input.invoice,
                    SaleOrderNo = orderNo,
                    StockNo = ""
                };
                await OrderMasterRepo.InsertAsync(master);
                // 添加流程
                await AddProgress(orderNo, inputDate);
                // 添加订单详情
                await AddOrderDetail(carts, customerNo, orderNo, inputDate);
                // 提交事务
                ts.Complete();
                // 删除Redis中的购物车数据
                foreach (var cart in carts)
                {
                    RedisWorker.RemoveKey($"cart:{cart.CartGuid}:{customerNo}");
                }
                return true;
            }
            catch (System.Exception)
            {
                ts.Dispose();
                throw;
            }

        }
    }
}