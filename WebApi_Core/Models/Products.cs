using System;
using System.Collections.Generic;

namespace WebApi_Core.Models
{
    public partial class Products
    {
        public Guid ProductId { get; set; }
        public Guid? CategoryId { get; set; }
        public string ProductName { get; set; }
        public short? UnitsInStock { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceVat { get; set; }

        public virtual Categories Category { get; set; }
    }
}
