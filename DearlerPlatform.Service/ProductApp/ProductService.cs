using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using DearlerPlatform.Common.EventBusHelper;
using DearlerPlatform.Common.RedisModule;
using DearlerPlatform.Core;
using DearlerPlatform.Core.Consts;
using DearlerPlatform.Core.Repository;
using DearlerPlatform.Domain;
using DearlerPlatform.Service.ProductApp.Dto;
using DearlerPlatform.Service.ShoppingCartApp.Dto;
using JetBrains.Annotations;

namespace DearlerPlatform.Service.ProductApp
{
    public partial class ProductService : IProductService
    {
        public ProductService(
            IRepository<Product> productRepo,
            IRepository<ProductSale> ProductSale,
            IRepository<ProductSaleAreaDiff> ProductSaleAreaDiff,
            IRepository<ProductPhoto> ProductPhoto,
            IMapper mapper,
            DealerPlatformContext context,
            LocalEventBus<List<ShoppingCartDto>> localEventBusShoppingCartDto,
            IRedisWorker redisWorker
            )
        {
            ProductRepo = productRepo;
            this.ProductSaleRepo = ProductSale;
            this.ProductSaleAreaDiffRepo = ProductSaleAreaDiff;
            this.ProductPhotoRepo = ProductPhoto;
            Mapper = mapper;
            Context = context;
            RedisWorker = redisWorker;
            localEventBusShoppingCartDto.localEventHandler += LocalEventHandler;
        }

        public IRepository<Product> ProductRepo { get; }
        public IRepository<ProductSale> ProductSaleRepo { get; }
        public IRepository<ProductSaleAreaDiff> ProductSaleAreaDiffRepo { get; }
        public IRepository<ProductPhoto> ProductPhotoRepo { get; }
        public IMapper Mapper { get; }
        public DealerPlatformContext Context { get; }
        public IRedisWorker RedisWorker { get; }

        public async Task LocalEventHandler(List<ShoppingCartDto> dtos)
        {
            var nos = dtos.Select(d => d.ProductNo);
            // var productDtos = await GetProductByProductNos(nos.ToArray());
            var productDtos = await GetProductByProductNosInCache(nos.ToArray());
            dtos.ForEach(dto =>
            {
                var prodyctDto = productDtos.FirstOrDefault(m => m.ProductNo == dto.ProductNo);
                dto.ProductDto = prodyctDto;
            });
        }

        public async Task<IEnumerable<ProductDto>> GetProductDto(
            string searchText,
            string productType,
            string belongTypeName,
            Dictionary<string, string> productProps,
            PageWithSortDto pageWithSortDto)
        {
            // int skipNum = (pageIndex - 1) * PageSize;

            pageWithSortDto.Sort ??= "ProductName";

            #region linq
            /* 
             linq语句查询数据并排序
              [from item in list]： 从list集合中获取每一条数据
              [ orderby p.GetType().GetProperty(sort).GetValue(p)]：排序，从item中通过反射获取列的值进行排序判断
              [ select item]：返回需要的数据，可以是item本身，也可以是另外重组值
            
            /*
            var products = (from p in (await ProductRepo.GetListAsync())
                            orderby p.GetType().GetProperty(sort).GetValue(p) descending
                            select p).Skip(skipNum).Take(PageSize).ToList();
            */
            #endregion

            var bzgg = productProps.ContainsKey("ProductBZGG") ? productProps["ProductBZGG"] : null;
            productProps.TryGetValue("ProductCd", out string cd);
            productProps.TryGetValue("ProductPp", out string pp);
            productProps.TryGetValue("ProductXh", out string xh);
            productProps.TryGetValue("ProductCz", out string cz);
            productProps.TryGetValue("ProductHb", out string hb);
            productProps.TryGetValue("ProductHd", out string hd);
            productProps.TryGetValue("ProductGy", out string gy);
            productProps.TryGetValue("ProductHs", out string hs);
            productProps.TryGetValue("ProductMc", out string mc);
            productProps.TryGetValue("ProductDj", out string dj);
            productProps.TryGetValue("ProductGg", out string gg);
            productProps.TryGetValue("ProductYs", out string ys);

            int skip = (pageWithSortDto.PageIndex - 1) * pageWithSortDto.PageSize;
            var products = ProductRepo.GetQueryable().Where(m =>
                m.BelongTypeName.ToLower() == belongTypeName.ToLower() &&
                (m.TypeNo == productType || string.IsNullOrWhiteSpace(productType)) &&
                (m.ProductName.Contains(searchText) || string.IsNullOrWhiteSpace(searchText)) &&
                (bzgg == null || m.ProductBzgg == bzgg)
                    && (cd == null || m.ProductCd == cd)
                    && (pp == null || m.ProductPp == pp)
                    && (xh == null || m.ProductXh == xh)
                    && (cz == null || m.ProductCz == cz)
                    && (hb == null || m.ProductHb == hb)
                    && (hd == null || m.ProductHd == hd)
                    && (gy == null || m.ProductGy == gy)
                    && (hs == null || m.ProductHs == hs)
                    && (mc == null || m.ProductMc == mc)
                    && (dj == null || m.ProductDj == dj)
                    && (gg == null || m.ProductGg == gg)
                    && (ys == null || m.ProductYs == ys)
            )
            .OrderBy(pageWithSortDto.Sort).Skip(skip).Take(pageWithSortDto.PageSize);
            // 领域模型 转 视图模型
            var dtos = Mapper.Map<List<ProductDto>>(products);
            var productPhotos = await GetProductPhotosByProductNo(products.Select(m => m.ProductNo).ToArray());
            var productSales = await GetProductSalesByProductNo(products.Select(m => m.ProductNo).ToArray());
            dtos.ForEach(p =>
            {
                p.ProductPhoto = productPhotos.FirstOrDefault(m => m.ProductNo == p.ProductNo);
                p.ProductSale = productSales.FirstOrDefault(m => m.ProductNo == p.ProductNo);
                // var productSale = productSales.FirstOrDefault(m=>m.ProductNo == p.ProductNo);
            });
            return dtos;
        }

        public async Task<List<BlongTypeDto>> GetBlongTypeDtosAsync()
        {
            return await Task.Run(() =>
            {
                var res = ProductRepo.GetQueryable().Select(m => new BlongTypeDto
                {
                    SysNo = m.SysNo,
                    BelongTypeName = m.BelongTypeName,
                }).Distinct().ToList();
                return res;
            });


        }

        public async Task<IEnumerable<ProductTypeDto>> GetProductType(string belongTypeName)
        {
            return await Task.Run(() =>
            {
                var ProductType = ProductRepo.GetQueryable().Where(m => m.BelongTypeName == belongTypeName && !string.IsNullOrWhiteSpace(m.TypeNo) && !string.IsNullOrWhiteSpace(m.TypeName)).Select(m => new ProductTypeDto
                {
                    TypeName = m.TypeName,
                    TypeNo = m.TypeNo
                }).Distinct().ToList();

                return ProductType;
            });
        }

        public async Task<Dictionary<string, IEnumerable<string>>> GetProductProps(string belongTypeName, string? typeNo)
        {
            Dictionary<string, IEnumerable<string>> dicProductType = new();
            // var products = Context.Products.Select(m=>new {
            //     PriductBzgg=m.ProductBzgg,
            //     ProductCd=m.ProductCd,})
            var products = await ProductRepo.GetListAsync(m => m.BelongTypeName == belongTypeName && (m.TypeNo == typeNo || string.IsNullOrWhiteSpace(typeNo)));
            dicProductType.Add("ProductBzgg|包装规格", products.Select(m => m.ProductBzgg).Distinct().Where(m => !string.IsNullOrWhiteSpace(m)).ToList());
            dicProductType.Add("ProductCd|产地", products.Select(m => m.ProductCd).Distinct().Where(m => !string.IsNullOrWhiteSpace(m)).ToList());
            dicProductType.Add("ProductCz|材质", products.Select(m => m.ProductCz).Distinct().Where(m => !string.IsNullOrWhiteSpace(m)).ToList());
            dicProductType.Add("ProductDj|等级", products.Select(m => m.ProductDj).Distinct().Where(m => !string.IsNullOrWhiteSpace(m)).ToList());
            dicProductType.Add("ProductGg|规格", products.Select(m => m.ProductGg).Distinct().Where(m => !string.IsNullOrWhiteSpace(m)).ToList());
            dicProductType.Add("ProductGy|工艺", products.Select(m => m.ProductGy).Distinct().Where(m => !string.IsNullOrWhiteSpace(m)).ToList());
            dicProductType.Add("ProductHb|环保", products.Select(m => m.ProductHb).Distinct().Where(m => !string.IsNullOrWhiteSpace(m)).ToList());
            dicProductType.Add("ProductHd|厚度", products.Select(m => m.ProductHd).Distinct().Where(m => !string.IsNullOrWhiteSpace(m)).ToList());
            dicProductType.Add("ProductHs|花色", products.Select(m => m.ProductHs).Distinct().Where(m => !string.IsNullOrWhiteSpace(m)).ToList());
            dicProductType.Add("ProductMc|面材", products.Select(m => m.ProductMc).Distinct().Where(m => !string.IsNullOrWhiteSpace(m)).ToList());
            dicProductType.Add("ProductPp|品牌", products.Select(m => m.ProductPp).Distinct().Where(m => !string.IsNullOrWhiteSpace(m)).ToList());
            dicProductType.Add("ProductXh|型号", products.Select(m => m.ProductXh).Distinct().Where(m => !string.IsNullOrWhiteSpace(m)).ToList());
            dicProductType.Add("ProductYs|颜色", products.Select(m => m.ProductYs).Distinct().Where(m => !string.IsNullOrWhiteSpace(m)).ToList());
            return dicProductType;
        }
        private static object lockObj = new object();
        public async Task<List<ProductDto>> GetProductByProductNosInCache(params string[] postProductNos)
        {
            List<ProductCto> ctos = new();
            foreach (var productNo in postProductNos)
            {
                var res = RedisWorker.GetHashMemory<ProductCto>($"{RedisKeyName.PRODUCT_KEY}:{productNo}").FirstOrDefault();
                if (res == null)
                {
                    lock (lockObj)
                    {
                        res = RedisWorker.GetHashMemory<ProductCto>($"{RedisKeyName.PRODUCT_KEY}:{productNo}").FirstOrDefault();
                        if (res == null)
                        {
                            res = Mapper.Map<ProductDto, ProductCto>(GetProductByProductNos(productNo).Result.FirstOrDefault());
                            RedisWorker.SetHashMemory<ProductCto>($"{RedisKeyName.PRODUCT_KEY}:{productNo}", res);
                        }
                    }

                }
                ctos.Add(res);
            }
            return Mapper.Map<List<ProductCto>, List<ProductDto>>(ctos);
        }
        public async Task<List<ProductDto>> GetProductByProductNos(params string[] postProductNos)
        {
            var productNos = postProductNos.Distinct();
            var products = await ProductRepo.GetListAsync(m => productNos.Contains(m.ProductNo));
            var productDtos = Mapper.Map<List<Product>, List<ProductDto>>(products);
            var ProductSales = await GetProductSalesByProductNo(productDtos.Select(m => m.ProductNo).ToArray());
            productDtos.ForEach(p =>
            {
                p.ProductSale = ProductSales.FirstOrDefault(m => m.ProductNo == p.ProductNo);
            });
            return productDtos;
        }

    }
}