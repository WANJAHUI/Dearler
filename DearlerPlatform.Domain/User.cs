using System;
using System.Collections.Generic;

#nullable disable

namespace DearlerPlatform.Domain
{
    public partial class User
    {
        public int Id { get; set; }
        public Guid UserGuid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HeadUrl { get; set; }
        public bool IsDel { get; set; }
    }
}
