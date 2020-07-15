using Licenta_V0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licenta_V0.Controllers
{
    
    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            var categories = from category in db.Categories
                             orderby category.CategoryName
                             select category;
            ViewBag.Categories = categories;
            return View();
        }
        public ActionResult Show(int id)
        {
            CategoryModels cat = db.Categories.Find(id);
            return View(cat);
        }
        [Authorize(Roles = "Admin,Editorial")]
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Editorial")]
        public ActionResult New(CategoryModels cat)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    db.Categories.Add(cat);
                    db.SaveChanges();
                    TempData["message"] = Licenta_V0.Resources.ErrorAndConfirmMessages.ConfirmCategoryUpload;
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(cat);
                }
            }
            catch(Exception e)
            {
                return View(cat);
            }
        }

        [Authorize(Roles = "Admin,Editorial")]
        public ActionResult Edit(int id)
        {
            CategoryModels category = db.Categories.Find(id);
            return View(category);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Editorial")]
        public ActionResult Edit(int id, CategoryModels requestCategory)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    CategoryModels cat = db.Categories.Find(id);
                    if (TryUpdateModel(cat))
                    {
                        cat.CategoryName = requestCategory.CategoryName;
                        TempData["message"] = Licenta_V0.Resources.ErrorAndConfirmMessages.ConfirmCategoryModify;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(requestCategory);
                }
            }
            catch (Exception e)
            {
                return View(requestCategory);
            }
        }
        [HttpDelete]
        [Authorize(Roles = "Admin,Editorial")]
        public ActionResult Delete(int id)
        {
            CategoryModels cat = db.Categories.Find(id);
            db.Categories.Remove(cat);
            TempData["message"] = Licenta_V0.Resources.ErrorAndConfirmMessages.ConfirmCategoryDeleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}