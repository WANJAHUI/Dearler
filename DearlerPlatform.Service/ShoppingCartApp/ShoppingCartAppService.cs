using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using DearlerPlatform.Common.EventBusHelper;
using DearlerPlatform.Common.RedisModule;
using DearlerPlatform.Core;
using DearlerPlatform.Core.Repository;
using DearlerPlatform.Domain;
using DearlerPlatform.Service.ShoppingCartApp.Dto;
using Microsoft.EntityFrameworkCore;

namespace DearlerPlatform.Service.ShoppingCartApp
{
    public partial class ShoppingCartAppService : IShoppingCartAppService
    {
        public IRepository<ShoppingCart> CartRepo { get; }
        public IMapper Mapper { get; }
        public LocalEventBus<List<ShoppingCartDto>> LocalEventBusShoppingCartDto { get; }
        public DealerPlatformContext Context { get; }
        public IRedisWorker RedisWorker { get; }
        public RedisCore RedisCore { get; }

        public ShoppingCartAppService(
            IRepository<ShoppingCart> cartRepo,
             IMapper mapper,
             LocalEventBus<List<ShoppingCartDto>> localEventBusShoppingCartDto,
             DealerPlatformContext context,
             IRedisWorker redisWorker,
             RedisCore redisCore
             )
        {
            CartRepo = cartRepo;
            Mapper = mapper;
            LocalEventBusShoppingCartDto = localEventBusShoppingCartDto;
            Context = context;
            RedisWorker = redisWorker;
            RedisCore = redisCore;
        }

        public async Task<ShoppingCart> SetShoppingCart(ShoppingCartInputDto input)
        {
            ShoppingCart shoppingCartRes = null;
            // var shoppingCart = await CartRepo.GetAsync(m => m.ProductNo == input.ProductNo);
            var shoppingCart = RedisWorker.GetHashMemory<ShoppingCart>($"cart:*:{input.CustomerNo}").FirstOrDefault(m => m.ProductNo == input.ProductNo);
            if (shoppingCart != null)
            {
                shoppingCart.ProductNum++;
                RedisWorker.SetHashMemory($"cart:{shoppingCart.CartGuid}:{shoppingCart.CustomerNo}", shoppingCart);
                // shoppingCartRes = await CartRepo.UpdateAsync(shoppingCart);
            }
            else
            {
                var shoppingcart = Mapper.Map<ShoppingCartInputDto, ShoppingCart>(input);
                shoppingcart.CartGuid = Guid.NewGuid().ToString();
                shoppingcart.CartSelected = true;
                // shoppingCartRes = await CartRepo.InsertAsync(shoppingcart);
                RedisWorker.SetHashMemory($"cart:{shoppingcart.CartGuid}:{shoppingcart.CustomerNo}", shoppingcart);
            }
            return shoppingCartRes;
        }

        public async Task<List<ShoppingCartDto>> GetShoppingCartDtos(string customerNo)
        {
            // var carts = await CartRepo.GetListAsync(m => m.CustomerNo == customerNo);
            var carts = RedisWorker.GetHashMemory<ShoppingCart>($"cart:*:{customerNo}");
            var dtos = Mapper.Map<List<ShoppingCart>, List<ShoppingCartDto>>(carts);
            await LocalEventBusShoppingCartDto.Publish(dtos);
            return dtos;
        }

        /// <summary>
        /// 指定更新某个字段
        /// </summary>
        /// <param name="cartGuid"></param>
        /// <param name="cartSelected"></param>
        /// <returns></returns>
        public async Task<string> UpdateCartSelected(ShoppingCartSelectedEditDto edit, string customerNo)
        {
            if (edit.ProductNum <= 0)
            {
                RedisCore.RemoveKey($"cart:{edit.CartGuids[0]}:{customerNo}");
                return "Remove";
            }
            var shoppingCart = RedisWorker.GetHashMemory<ShoppingCart>($"cart:{edit.CartGuids[0]}:*").FirstOrDefault();
            shoppingCart.CartSelected = edit.CartSelected;
            shoppingCart.ProductNum = edit.ProductNum;
            RedisWorker.SetHashMemory($"cart:{edit.CartGuids[0]}:{customerNo}", shoppingCart);
            return "Update";
            // try
            // {
            //     foreach (var cartGuid in edit.CartGuids)
            //     {
            //         ShoppingCart cart = new()
            //         {
            //             CartGuid = cartGuid,
            //             CartSelected = edit.CartSelected,
            //             ProductNum = edit.ProductNum
            //         };
            //         Context.Attach(cart);
            //         Context.Entry(cart).Property(m => m.CartSelected).IsModified = true;
            //         Context.Entry(cart).Property(m => m.ProductNum).IsModified = true;


            //     }
            //     return await Context.SaveChangesAsync() > 0;

            // }
            // catch (System.Exception)
            // {
            //     return false;
            //     throw;
            // }
        }
        /// <summary>
        /// 获取购物车数量
        /// </summary>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        public async Task<int> GetShoppingCartNum(string customerNo)
        {
            var carts = RedisWorker.GetHashMemory<ShoppingCart>($"cart:*:{customerNo}");
            // var carts = await CartRepo.GetListAsync(m => m.CustomerNo == customerNo && m.CartSelected);
            var currentCartNum = 0;
            carts.ForEach(c =>
            {
                currentCartNum += c.ProductNum;
            });
            return currentCartNum;
        }



    }
}