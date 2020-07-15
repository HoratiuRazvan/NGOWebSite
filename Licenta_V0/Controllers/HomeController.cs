using Licenta_V0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licenta_V0.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context;
        public HomeController() { context = new ApplicationDbContext(); }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EditHomePage()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Articles()
        {
            var art = context.Articles.ToList();
            return View(art);
        }
        public ActionResult Magazine()
        {
            var mag = context.Magazines.ToList();
            return View(mag);
        }
        public ActionResult EditHome()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Management()
        {
            return View();
        }
    }
}