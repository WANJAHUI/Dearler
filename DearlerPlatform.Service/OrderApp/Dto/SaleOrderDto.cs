using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DearlerPlatform.Domain;

namespace DearlerPlatform.Service.OrderApp.Dto
{
    public class SaleOrderDto
    {
        public int Id { get; set; }
        public string SaleOrderNo { get; set; }
        public string CustomerNo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InputDate { get; set; }
        public string StockNo { get; set; }
        public string EditUserNo { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Remark { get; set; }
        public List<SaleOrderDetail> OrderDetails { get; set; }
        public SaleOrderProgress OrderProgress { get; set; }
        public CustomerInvoice CustomerInvoice { get; set; }
    }
}