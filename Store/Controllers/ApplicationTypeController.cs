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
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ApplicationTypeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationType> categories = _db.Types;
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //вбудований механізм для форм введення,який має захист від викрадення даних
        public IActionResult Add(ApplicationType item)
        {
            if (ModelState.IsValid)
            {
                _db.Types.Add(item);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);

        }

        public IActionResult Edit(int? Id)
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
        public IActionResult Edit(ApplicationType item)
        {
            if (ModelState.IsValid)
            {
                _db.Types.Update(item);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _db.Types.Find(Id);
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
            var obj = _db.Types.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            _db.Types.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}

