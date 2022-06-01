using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Store.Models;

namespace Store.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
       public IEnumerable<SelectListItem> CategorySelectedList { get; set; }
        public IEnumerable<SelectListItem> TypeSlectedList { get; set;}

    }
}
