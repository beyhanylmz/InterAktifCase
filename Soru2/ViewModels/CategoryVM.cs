using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soru2.ViewModels
{
    public class CategoryVM
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool? IsActive { get; set; }
        public string Order { get; set; }
    }
}