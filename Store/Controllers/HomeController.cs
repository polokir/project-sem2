using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Data;
using Store.Models;
using Store.Models.ViewModels;
using Store.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Products = _db.Products.Include(u => u.Category).Include(u => u.Type),
                Categories = _db.Categories
            };
            return View(homeVM);
        }

        public IActionResult Details(int id)
        {
            List<ShoppingCart> shoppingList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            
            DetailsVM detailsVM = new DetailsVM()
            {
                Product = _db.Products.Include(u => u.Category).Where(u => u.Id == id).FirstOrDefault(),
                IsExistInCard = false
            };

            foreach (var item in shoppingList)
            {
                if (item.ProductId == id)
                {
                    detailsVM.IsExistInCard = true;
                }
            }
            return View(detailsVM);
        }


        public IActionResult RemoveFromCart(int id)
        {
            List<ShoppingCart> shoppingList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            var deleted = shoppingList.SingleOrDefault(r => r.ProductId == id);
            if (deleted != null)
            {
                shoppingList.Remove(deleted);
            }

            HttpContext.Session.Set(WC.SessionCart, shoppingList);

            return RedirectToAction(nameof(Index));
        }



        [HttpPost,ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
            List<ShoppingCart> shoppingList = new List<ShoppingCart>();
            if(HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart)!=null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            shoppingList.Add(new ShoppingCart { ProductId = id });
            HttpContext.Session.Set(WC.SessionCart, shoppingList);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
