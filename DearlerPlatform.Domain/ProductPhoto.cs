using System;
using System.Collections.Generic;

#nullable disable

namespace DearlerPlatform.Domain
{
    public partial class ProductPhoto
    {
        public int Id { get; set; }
        public string SysNo { get; set; }
        public string ProductNo { get; set; }
        public string ProductPhotoUrl { get; set; }
    }
}
