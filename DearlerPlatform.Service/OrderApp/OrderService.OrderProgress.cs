using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DearlerPlatform.Domain;

namespace DearlerPlatform.Service.OrderApp
{
    public partial class OrderService
    {
        private async Task AddProgress(string orderNo, DateTime stepTime)
        {
            SaleOrderProgress progress = new()
            {
                ProgressGuid = Guid.NewGuid().ToString(),
                SaleOrderNo = orderNo,
                StepName = "下单",
                StepSn = 1,
                StepTime = stepTime
            };
            await OrderProgressrRepo.InsertAsync(progress);
        }

        private async Task<List<SaleOrderProgress>> GetProgressByOrderNos(params string[] orderNos)
        {
            var progress = await OrderProgressrRepo.GetListAsync(m => orderNos.Contains(m.SaleOrderNo));
            return progress;
        }
    }
}