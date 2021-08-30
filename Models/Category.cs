using System;
using System.Collections.Generic;

#nullable disable

namespace Supermarket.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public HashSet<Product> Products;
    }
}
