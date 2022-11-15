using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DearlerPlatform.Core;
using DearlerPlatform.Domain;
using DearlerPlatform.Service.ProductApp.Dto;
using JetBrains.Annotations;

namespace DearlerPlatform.Service.ProductApp
{
    public interface IProductService : IocTag
    {
        Task<IEnumerable<ProductDto>> GetProductDto(string searchText, string productType, string belongTypeName,Dictionary<string, string> productProps, PageWithSortDto pageWithSortDto);
        Task<List<ProductPhoto>> GetProductPhotosByProductNo(params string[] productNos);
        Task<List<ProductSale>> GetProductSalesByProductNo(params string[] productNos);
        Task<IEnumerable<ProductTypeDto>> GetProductType(string belongTypeName);
        Task<List<BlongTypeDto>> GetBlongTypeDtosAsync();
        Task<Dictionary<string, IEnumerable<string>>> GetProductProps(string belongTypeName, string? typeNo);
    }
}