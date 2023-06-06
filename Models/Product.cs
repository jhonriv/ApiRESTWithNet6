using System;
using System.Collections.Generic;

namespace ApiRESTWithNet6.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public double Price { get; set; }
        public int Stock { get; set; }
    }
}
