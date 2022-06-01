using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store.Models.ViewModels;
using Store.Data;
using Store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            IEnumerable<Product> products = _db.Products;
            foreach(var item in products)
            {
                item.Category = _db.Categories.FirstOrDefault(u => u.Id == item.CategoryId);
                item.Type = _db.Types.FirstOrDefault(u => u.Id == item.TypeId);
            }
            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            /*IEnumerable<SelectListItem> CategotyList = _db.Categories.Select(item => new SelectListItem
            {
                Text = item.Name,
                Value = item.Id.ToString()
            });

            ViewBag.CategotyList = CategotyList;

            Product product = new Product();*/

            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategorySelectedList = _db.Categories.Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                }),

                TypeSlectedList = _db.Types.Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                })
            };

            if (id == null)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _db.Products.Find(id);
                if (productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //вбудований механізм для форм введення,який має захист від викрадення даних
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootpath = _webHostEnvironment.WebRootPath;
                if (productVM.Product.Id == 0)
                {
                    //Creating
                    string upload = webRootpath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string ext = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + ext), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productVM.Product.Image = fileName + ext;

                    _db.Products.Add(productVM.Product);
                    
                }
                else
                {
                    //Update
                    var Item = _db.Products.AsNoTracking().FirstOrDefault(u => u.Id == productVM.Product.Id);

                    if (files.Count > 0)
                    {
                        string upload = webRootpath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string ext = Path.GetExtension(files[0].FileName);

                        var Old = Path.Combine(upload, Item.Image);
                        if (System.IO.File.Exists(Old))
                        {
                            System.IO.File.Delete(Old);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + ext), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productVM.Product.Image = fileName + ext;
                    }
                    else
                    {
                        productVM.Product.Image = Item.Image;
                    }
                    _db.Products.Update(productVM.Product);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            productVM.CategorySelectedList = _db.Categories.Select(item => new SelectListItem
            {
                Text = item.Name,
                Value = item.Id.ToString()
            });
            productVM.TypeSlectedList = _db.Types.Select(item => new SelectListItem
            {
                Text = item.Name,
                Value = item.Id.ToString()
            });
            return View(productVM);

        }

       

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Product product = _db.Products.Include(u=>u.Category).Include(u=>u.Type).FirstOrDefault(u=>u.Id==Id);

           // product.Category = _db.Categories.Find(product.CategoryId);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken] //вбудований механізм для форм введення,який має захист від викрадення даних
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Products.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            string upload =_webHostEnvironment.WebRootPath + WC.ImagePath;
            var Old = Path.Combine(upload, obj.Image);
            if (System.IO.File.Exists(Old))
            {
                System.IO.File.Delete(Old);
            }


            _db.Products.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}

