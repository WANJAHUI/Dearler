using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DearlerPlatform.Service.ShoppingCartApp.Dto
{
    public class ShoppingCartInputDto
    {
        public string CustomerNo { get; set; }
        public string ProductNo { get; set; }
        public int ProductNum { get; set; }
    }
}