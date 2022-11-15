using System;
using System.Collections.Generic;

#nullable disable

namespace DearlerPlatform.Domain
{
    public partial class CustomerInvoice
    {
        
        public int Id { get; set; }
        public string CustomerNo { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceEin { get; set; }
        public string InvoiceBank { get; set; }
        public string InvoiceAccount { get; set; }
        public string InvoiceAddress { get; set; }
        public string InvoicePhone { get; set; }
    }
}
