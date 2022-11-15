using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DearlerPlatform.Service.ProductApp.Dto;

namespace DearlerPlatform.Service.ShoppingCartApp.Dto
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }
        public string CartGuid { get; set; }
        public string CustomerNo { get; set; }
        public string ProductNo { get; set; }
        public int ProductNum { get; set; }
        public bool CartSelected { get; set; }
        public ProductDto ProductDto { get; set; }
    }
}