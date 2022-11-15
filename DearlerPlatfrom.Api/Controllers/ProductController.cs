using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DearlerPlatform.Core;
using DearlerPlatform.Service.ProductApp;
using DearlerPlatform.Service.ProductApp.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DearlerPlatfrom.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProductController : BaseController
{
    public ProductController(IProductService productService)
    {
        ProductService = productService;
    }

    public IProductService ProductService { get; }
    [HttpGet]
    public async Task<IEnumerable<ProductDto>> GetProductDtosAsync(
        string? searchText,
        string? productType,
        string belongTypeName,
        string? productProps,
        string? sort,
        int pageIndex = 1,
        int pageSize = 30,
        OrderType orderType = OrderType.Asc)
    {
        Dictionary<string, string> dicProductProps = new();
        var arrProductProps = productProps?.Split("^") ?? new string[0];
        foreach (var item in arrProductProps)
        {
            var key = item.Split("_")[0];
            var value = item.Split("_")[1];
            dicProductProps.Add(key, value);
        }

        return await ProductService.GetProductDto(searchText, productType, belongTypeName,dicProductProps, new PageWithSortDto()
        {
            Sort = sort,
            PageIndex = pageIndex,
            PageSize = pageSize,
            OrderType = orderType
        });
    }

    [HttpGet("BlongType")]
    public async Task<List<BlongTypeDto>> GetBlongType()
    {
        return await ProductService.GetBlongTypeDtosAsync();
    }
    [HttpGet("type")]
    public async Task<IEnumerable<ProductTypeDto>> GetProductTypeDtosAsync(string belongTypeName)
    {
        return await ProductService.GetProductType(belongTypeName);
    }
    [HttpGet("props")]
    public async Task<Dictionary<string, IEnumerable<string>>> GetProductProps(string belongTypeName, string? typeNo)
    {
        return await ProductService.GetProductProps(belongTypeName, typeNo);
    }


}
