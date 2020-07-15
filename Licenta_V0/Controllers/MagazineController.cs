using Dapper;
using Licenta_V0.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Licenta_V0.Controllers
{
    public class MagazineController : Controller
    {
        ApplicationDbContext context;
        public MagazineController()
        {
            context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MagazineUpload()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Editorial")]
        public ActionResult MagazineUpload(HttpPostedFileBase files)
        {
            if (files == null)
                return HttpNotFound();
            String FileExt = Path.GetExtension(files.FileName).ToUpper();

            if (FileExt == ".PDF")
            {
                Stream str = files.InputStream;
                BinaryReader Br = new BinaryReader(str);
                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                MagazineModels Fd = new Models.MagazineModels();
                Fd.MagazineName = files.FileName;
                Fd.MagazineContaint = FileDet;
                SaveMagazineDetails(Fd);
                return RedirectToAction("Index");
            }
            else
            {

                ViewBag.FileStatus = "Invalid file format.";
                return View();

            }

        }
        [HttpGet]
        public FileResult DownloadMagazine(int id)
        {


            List<MagazineModels> ObjFiles = GetMagazineList();

            var FileById = (from FC in ObjFiles
                            where FC.Id.Equals(id)
                            select new { FC.MagazineName, FC.MagazineContaint }).ToList().FirstOrDefault();

            return File(FileById.MagazineContaint, "application/pdf", FileById.MagazineName);

        }
        [HttpGet]
        public PartialViewResult MagazineDetails()
        {
            List<MagazineModels> DetList = GetMagazineList();

            return PartialView("MagazineDetails", DetList);


        }
        private List<MagazineModels> GetMagazineList()
        {
            List<MagazineModels> DetList = new List<MagazineModels>();

            DbConnection();
            con.Open();
            DetList = SqlMapper.Query<MagazineModels>(con, "GetMagazineDetails", commandType: CommandType.StoredProcedure).ToList();
            con.Close();
            return DetList;
        }

        [Authorize(Roles = "Admin,Editorial")]
        private void SaveMagazineDetails(MagazineModels objDet)
        {
            objDet.PublishDate = DateTime.Parse("Jan 1,2009");
            DynamicParameters Parm = new DynamicParameters();
            Parm.Add("@MagazineName", objDet.MagazineName);
            Parm.Add("@MagazineDescription", objDet.MagazineDescription);
            Parm.Add("@MagazineContaint", objDet.MagazineContaint);
            Parm.Add("@PublishDate", objDet.PublishDate);
            DbConnection();
            con.Open();
            con.Execute("AddMagazineDetails", Parm, commandType: System.Data.CommandType.StoredProcedure);
            con.Close();


        }
        [Authorize(Roles = "Admin,Editorial")]
        public ActionResult Edit(int id)
        {
            var mag = context.Magazines.SingleOrDefault(m => m.Id == id);
            if (mag == null)
                return HttpNotFound();
            return View(mag);

        }
        [HttpPut]
        [Authorize(Roles = "Admin,Editorial")]
        public ActionResult Edit(int id, MagazineModels requestMagazine)
        {
            var mag = context.Magazines.Find(id);
            if (TryUpdateModel(mag))
            {
                mag.MagazineDescription = requestMagazine.MagazineDescription;
                mag.MagazineName = requestMagazine.MagazineName;
                //mag.PublishDate = requestMagazine.PublishDate;
                context.SaveChanges();
            }

            return RedirectToAction("Index");

        }
        [Authorize(Roles = "Admin,Editorial")]
        public ActionResult Delete(int id)
        {
            var mag = context.Magazines.SingleOrDefault(m => m.Id == id);
            if (mag == null)
                return HttpNotFound();
            context.Magazines.Remove(mag);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        private SqlConnection con;
        private string constr;
        private void DbConnection()
        {
            constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constr);

        }
    }
}