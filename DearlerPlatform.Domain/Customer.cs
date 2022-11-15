using System;
using System.Collections.Generic;

#nullable disable

namespace DearlerPlatform.Domain
{
    public partial class Customer: BaseEntity
    {
        
        public new int Id { get; set; }
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string FirstAreaNo { get; set; }
        public string FirstAreaName { get; set; }
        public string AreaNo { get; set; }
        public string AreaName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Tel { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string BankAccount { get; set; }
        public string BankNo { get; set; }
        public string BankName { get; set; }
        public string Ein { get; set; }
        public string CustomerNote { get; set; }
        public string OwnerWorkerNo { get; set; }
        public string OwnerWorkerName { get; set; }
        public string OwnerWorkerTel { get; set; }
        public string OpenId { get; set; }
        public string HeadImgUrl { get; set; }
    }
}
