using System;
using DearlerPlatform.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DearlerPlatform.Core
{
    public partial class DealerPlatformContext : DbContext
    {
        public DealerPlatformContext()
        {
        }

        public DealerPlatformContext(DbContextOptions<DealerPlatformContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerInvoice> CustomerInvoices { get; set; }
        public virtual DbSet<CustomerPwd> CustomerPwds { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductPhoto> ProductPhotos { get; set; }
        public virtual DbSet<ProductSale> ProductSales { get; set; }
        public virtual DbSet<ProductSaleAreaDiff> ProductSaleAreaDiffs { get; set; }
        public virtual DbSet<SaleOrderDetail> SaleOrderDetails { get; set; }
        public virtual DbSet<SaleOrderMaster> SaleOrderMasters { get; set; }
        public virtual DbSet<SaleOrderProgress> SaleOrderProgresses { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserMenu> UserMenus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=localhost;uid=sa;pwd=1qaz2wsx;database=DealerPlatform;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_PRC_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerNo)
                    .HasName("PK_T200_customer_code");

                entity.Property(e => e.CustomerNo)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AreaName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AreaNo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.BankAccount)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankNo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerNote)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Ein)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.FirstAreaName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstAreaNo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.HeadImgUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.OpenId)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerWorkerName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerWorkerNo)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerWorkerTel)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerInvoice>(entity =>
            {
                entity.HasKey(e => new { e.CustomerNo, e.InvoiceNo })
                    .HasName("PK_T200_customer_invoice");

                entity.Property(e => e.CustomerNo)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.InvoiceAccount)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceBank)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceEin)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.InvoicePhone)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerPwd>(entity =>
            {
                entity.HasKey(e => e.CustomerNo)
                    .HasName("PK_T101_customer_psw");

                entity.Property(e => e.CustomerNo)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerPwd1)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("CustomerPwd");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ActionName).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.MenuName).HasMaxLength(50);

                entity.Property(e => e.MenuUrl).HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => new { e.SysNo, e.ProductNo })
                    .HasName("PK_T200_product_code");

                entity.Property(e => e.SysNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BelongTypeName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BelongTypeNo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ProductBzgg)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ProductBZGG");

                entity.Property(e => e.ProductCd)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ProductCD");

                entity.Property(e => e.ProductCz)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ProductCZ");

                entity.Property(e => e.ProductDj)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ProductDJ");

                entity.Property(e => e.ProductGg)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ProductGG");

                entity.Property(e => e.ProductGy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ProductGY");

                entity.Property(e => e.ProductHb)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ProductHB");

                entity.Property(e => e.ProductHd)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ProductHD");

                entity.Property(e => e.ProductHs)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ProductHS");

                entity.Property(e => e.ProductMc)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ProductMC");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ProductNote)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ProductPp)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ProductPP");

                entity.Property(e => e.ProductXh)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ProductXH");

                entity.Property(e => e.ProductYs)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ProductYS");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.TypeNo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitNo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductPhoto>(entity =>
            {
                entity.HasKey(e => e.ProductNo)
                    .HasName("PK_T200_product_photo");

                entity.Property(e => e.ProductNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ProductPhotoUrl)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SysNo)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductSale>(entity =>
            {
                entity.HasKey(e => new { e.SysNo, e.ProductNo })
                    .HasName("PK_T201_product_sale_list");

                entity.Property(e => e.SysNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductNo)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.StockNo)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductSaleAreaDiff>(entity =>
            {
                entity.HasKey(e => new { e.SysNo, e.ProductNo, e.StockNo, e.AreaNo })
                    .HasName("PK_T201_product_sale_area_diff");

                entity.Property(e => e.SysNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StockNo)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.AreaNo)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.FirstAreaNo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SaleOrderDetail>(entity =>
            {
                entity.HasKey(e => e.SaleOrderGuid);

                entity.Property(e => e.SaleOrderGuid)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerNo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.InputDate).HasColumnType("datetime");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.ProductNo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.ProductPhotoUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SaleOrderNo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SaleOrderMaster>(entity =>
            {
                entity.HasKey(e => e.SaleOrderNo);

                entity.Property(e => e.SaleOrderNo)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerNo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.EditUserNo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.InputDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceNo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StockNo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SaleOrderProgress>(entity =>
            {
                entity.HasKey(e => new { e.SaleOrderNo, e.ProgressGuid });

                entity.Property(e => e.SaleOrderNo)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.ProgressGuid)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.StepName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StepTime).HasColumnType("datetime");
            });

           
            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasKey(e => e.CartGuid)
                    .HasName("PK_T200_shopping_cart");

                entity.Property(e => e.CartGuid)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerNo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.ProductNo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.StockNo)
                    .HasName("PK_T200_stock_code");

                entity.Property(e => e.StockNo)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.StockLinkman)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.StockName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StockPhone)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.StockTel)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserGuid);

                entity.Property(e => e.UserGuid).ValueGeneratedNever();

                entity.Property(e => e.HeadUrl).HasMaxLength(500);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<UserMenu>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
