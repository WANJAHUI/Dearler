using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DearlerPlatform.Domain;

namespace DearlerPlatform.Service.ProductApp
{
    public partial class ProductService
    {
        public async Task<List<ProductSale>> GetProductSalesByProductNo(params string[] productNos)
        {
            return await ProductSaleRepo.GetListAsync(m => productNos.Contains(m.ProductNo));
        }
    }
}