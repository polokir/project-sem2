using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public static class WC
    {
        public static string ImagePath = @"\images\product\";
        public static string SessionCart = "ShoppingCartSession";

        public static string AdminRole = "Admin";
        public static string CustomerRole = "Customer";

    }
}
