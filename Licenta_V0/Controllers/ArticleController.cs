using Licenta_V0.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            var art = context.Articles.ToList();
            return View(art);
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

        [Authorize(Roles = "Admin,Editorial")]
        public ActionResult Edit(int id)
        {
            var art = context.Articles.SingleOrDefault(m => m.Id == id);
            if(art == null)
            {
                return HttpNotFound();
            }
            return View(art);
        }
        [HttpPut]
        [Authorize(Roles = "Admin,Editorial")]
        public ActionResult Edit(int id, ArticleModels requestArt)
        {
            var art = context.Articles.SingleOrDefault(m => m.Id == id);
            if(art == null)
            {
                return HttpNotFound();
            }
            if(TryUpdateModel(art))
            {
                art.ArticleDate = requestArt.ArticleDate;
                art.ArticleDescription = requestArt.ArticleDescription;
                art.ArticleName = requestArt.ArticleName;
                art.ArticleText = requestArt.ArticleText;
                art.AuthorName = requestArt.AuthorName;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin,Editorial")]
        public ActionResult New()
        {
            var viewModel = new ArticleModels
                {
                    ArticleDate = DateTime.Now
                }
            ;
            return View(viewModel);
        }
        [HttpPut]
        [Authorize(Roles = "Admin,Editorial")]
        public ActionResult New(ArticleModels art)
        {
            art.CategoryId = 8;
            if (TryUpdateModel(art))
            {
                context.Articles.Add(art);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles ="Admin,Editorial")]
        public ActionResult Delete(int id)
        {
            var art = context.Articles.SingleOrDefault(m => m.Id == id);
            if (art == null)
                return HttpNotFound();
            context.Articles.Remove(art);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public int UploadImageInDataBase(HttpPostedFileBase file, ContentViewModels contentViewModel)
        {
            contentViewModel.ArticleImages = ConvertToBytes(file);
            var Content = new ArticleModels
            {
                ArticleName = contentViewModel.ArticleName,
                ArticleDescription = contentViewModel.ArticleDescription,
                ArticleText = contentViewModel.ArticleText,
                ArticleImages = contentViewModel.ArticleImages
            };
            context.Articles.Add(Content);
            int i = context.SaveChanges();
            if (i == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            if(image != null)
            {
                BinaryReader reader = new BinaryReader(image.InputStream);
                imageBytes = reader.ReadBytes((int)image.ContentLength);
                return imageBytes;
            }
            return null;
            
        }
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
        public byte[] GetImageFromDataBase(int Id)
        {
            var q = from temp in context.Articles where temp.Id == Id select temp.ArticleImages;
            byte[] cover = q.First();
            return cover;
        }
    }
}