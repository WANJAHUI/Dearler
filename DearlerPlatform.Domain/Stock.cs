using System;
using System.Collections.Generic;

#nullable disable

namespace DearlerPlatform.Domain
{
    public partial class Stock
    {
        public int Id { get; set; }
        public string StockNo { get; set; }
        public string StockName { get; set; }
        public string StockLinkman { get; set; }
        public string StockTel { get; set; }
        public string StockPhone { get; set; }
    }
}
