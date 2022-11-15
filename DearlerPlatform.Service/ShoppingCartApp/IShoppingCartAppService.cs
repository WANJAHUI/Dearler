using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DearlerPlatform.Domain;
using DearlerPlatform.Service.ShoppingCartApp.Dto;

namespace DearlerPlatform.Service.ShoppingCartApp
{
    public interface IShoppingCartAppService : IocTag
    {
        Task<ShoppingCart> SetShoppingCart(ShoppingCartInputDto input);
        Task<List<ShoppingCartDto>> GetShoppingCartDtos(string customerNo);
        Task<string> UpdateCartSelected(ShoppingCartSelectedEditDto edit, string customerNo);
        Task<int> GetShoppingCartNum(string customerNo);
    }
}