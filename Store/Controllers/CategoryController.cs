using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers 
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<Category> categories = _db.Categories;
            return View(categories);
        }

        public IActionResult Create()
        { 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //вбудований механізм для форм введення,який має захист від викрадення даних
        public IActionResult Add (Category cat)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(cat);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cat);
           
        }

        public IActionResult Edit(int? Id)
        {
            if(Id==null || Id == 0)
            {
                return NotFound();
            }
           
            var obj = _db.Categories.Find(Id);
            if (obj == null)
            {
               return NotFound();
            }

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken] //вбудований механізм для форм введення,який має захист від викрадення даних
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _db.Categories.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken] //вбудований механізм для форм введення,який має захист від викрадення даних
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);

            if (obj==null)
            {
                return NotFound();
            }
              _db.Categories.Remove(obj);
              _db.SaveChanges();
              return RedirectToAction("Index");
            
        }


    }
}

