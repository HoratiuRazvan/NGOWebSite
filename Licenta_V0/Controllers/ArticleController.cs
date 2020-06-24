using Licenta_V0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licenta_V0.Controllers
{
    public class ArticleController : Controller
    {
        ApplicationDbContext context;
        public ArticleController() { context = new ApplicationDbContext(); }
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Show(int id)
        {
            var art = context.Articles.SingleOrDefault(m => m.Id == id);
            if( art == null)
            {
                return HttpNotFound();
            }
            return View(art);
        }
    }
}