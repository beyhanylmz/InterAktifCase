using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soru2.ViewModels
{
    public class ProductVM
    {
        public Guid ProductId { get; set; }
        public Guid? CategoryId { get; set; }
        public string ProductName { get; set; }
        public short? UnitsInStock { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceVat { get; set; }

        public virtual CategoryVM Category { get; set; }
    }
}