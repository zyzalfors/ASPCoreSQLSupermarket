using System;
using System.Collections.Generic;

#nullable disable

namespace Supermarket.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int? CategoryId { get; set; }
    }
}
