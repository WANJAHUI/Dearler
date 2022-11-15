using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DearlerPlatform.Service.ShoppingCartApp.Dto
{
    public class ShoppingCartSelectedEditDto
    {
        public List<string> CartGuids { get; set; }
        public bool CartSelected { get; set; }
        public int ProductNum { get; set; }
    }
}