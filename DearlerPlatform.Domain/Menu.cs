using System;
using System.Collections.Generic;

#nullable disable

namespace DearlerPlatform.Domain
{
    public partial class Menu
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public string ActionName { get; set; }
        public int? ParentMenuId { get; set; }
    }
}
