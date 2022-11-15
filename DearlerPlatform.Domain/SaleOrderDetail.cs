using System;
using System.Collections.Generic;

#nullable disable

namespace DearlerPlatform.Domain
{
    public partial class SaleOrderDetail
    {
        public int Id { get; set; }
        public string SaleOrderGuid { get; set; }
        public string SaleOrderNo { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string ProductPhotoUrl { get; set; }
        public string CustomerNo { get; set; }
        public DateTime InputDate { get; set; }
        public int OrderNum { get; set; }
        public double BasePrice { get; set; }
        public double DiffPrice { get; set; }
        public double SalePrice { get; set; }
    }
}
