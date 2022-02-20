using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_Core.ViewModels
{
    public class CategoryVM
    {
       
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool? IsActive { get; set; }
        public string Order { get; set; }

    }
}
