using DOANLAPTRINHWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANLAPTRINHWEB.Areas.Admin.Controllers
{
    [CustomAuthorization(Order = 1)]
    public class WaterController : Controller
    {
        dbWatchDataContext data = new dbWatchDataContext();
        public ActionResult List()
        {
            var L_GlassSurface = from r in data.MUCDOCHONGNUOCs select r;
            return View(L_GlassSurface);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(FormCollection collection, MUCDOCHONGNUOC md)
        {
            var TenMucDoChongNuoc = collection["TenMucDoChongNuoc"];
            md.TenMucDoChongNuoc = TenMucDoChongNuoc.ToString();
            data.MUCDOCHONGNUOCs.InsertOnSubmit(md);
            data.SubmitChanges();
            return RedirectToAction("List", "Water");
        }
        public ActionResult Update(int id)
        {
            var MaMucDoChongNuoc = data.MUCDOCHONGNUOCs.First(m => m.MaMucDoChongNuoc == id);
            return View(MaMucDoChongNuoc);
        }
        [HttpPost]
        public ActionResult Update(int id, FormCollection collection)
        {
            var MaMucDoChongNuoc = data.MUCDOCHONGNUOCs.First(m => m.MaMucDoChongNuoc == id);
            var TenMucDoChongNuoc = collection["TenMucDoChongNuoc"];
            MaMucDoChongNuoc.TenMucDoChongNuoc = TenMucDoChongNuoc.ToString();
            UpdateModel(MaMucDoChongNuoc);
            data.SubmitChanges();
            return RedirectToAction("List", "Water");
        }
        public ActionResult Delete(int id)
        {
            var MaMucDoChongNuoc = data.MUCDOCHONGNUOCs.First(m => m.MaMucDoChongNuoc == id);
            return View(MaMucDoChongNuoc);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var MaMucDoChongNuoc = data.MUCDOCHONGNUOCs.First(m => m.MaMucDoChongNuoc == id);
            data.MUCDOCHONGNUOCs.DeleteOnSubmit(MaMucDoChongNuoc);
            data.SubmitChanges();
            return RedirectToAction("List", "Water");
        }
    }
}