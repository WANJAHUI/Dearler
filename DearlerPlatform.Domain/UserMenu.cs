using System;
using System.Collections.Generic;

#nullable disable

namespace DearlerPlatform.Domain
{
    public partial class UserMenu
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? MenuId { get; set; }
    }
}
