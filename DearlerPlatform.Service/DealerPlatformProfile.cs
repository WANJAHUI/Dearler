using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using DearlerPlatform.Domain;
using DearlerPlatform.Service.CustomerApp.Dto;
using DearlerPlatform.Service.OrderApp.Dto;
using DearlerPlatform.Service.ProductApp.Dto;
using DearlerPlatform.Service.ShoppingCartApp.Dto;
using Newtonsoft.Json;

namespace DearlerPlatform.Service
{
    public class DealerPlatformProfile : Profile
    {
        
        public DealerPlatformProfile()
        {
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<ProductSale,ProductDto>().ReverseMap();
            CreateMap<ProductPhoto,ProductDto>().ReverseMap();
            CreateMap<ProductSaleAreaDiff,ProductDto>().ReverseMap();
            CreateMap<ShoppingCart,ShoppingCartInputDto>().ReverseMap();
            CreateMap<ShoppingCart,ShoppingCartDto>().ReverseMap();
            CreateMap<CustomerInvoice,InvoiceOfOrderConfirmDto>().ReverseMap();
            CreateMap<SaleOrderMaster,SaleOrderDto>().ReverseMap();
            
            CreateMap<ProductDto,ProductCto>()
            .ForMember(cto=>cto.ProductPhoto,m=>m.MapFrom(dto=>JsonConvert.SerializeObject(dto.ProductPhoto)))
            .ForMember(cto=>cto.ProductSale,m=>m.MapFrom(dto=>JsonConvert.SerializeObject(dto.ProductSale)));

            CreateMap<ProductCto,ProductDto>()
            .ForMember(dto=>dto.ProductPhoto,m=>m.MapFrom(cto=>JsonConvert.DeserializeObject<ProductPhoto>(cto.ProductPhoto)))
            .ForMember(dto=>dto.ProductSale,m=>m.MapFrom(cto=>JsonConvert.DeserializeObject<ProductSale>(cto.ProductSale)));
        }
    }
}