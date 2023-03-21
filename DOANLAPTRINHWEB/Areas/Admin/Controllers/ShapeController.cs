using DOANLAPTRINHWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANLAPTRINHWEB.Areas.Admin.Controllers
{
    [CustomAuthorization(Order = 1)]
    public class ShapeController : Controller
    {
        dbWatchDataContext data = new dbWatchDataContext();
        public ActionResult List()
        {
            var L_Shape = from r in data.HINHDANGs select r;
            return View(L_Shape);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(FormCollection collection, HINHDANG hd)
        {
            var TenHinhDang = collection["TenHinhDang"];
            hd.TenHinhDang = TenHinhDang.ToString();
            data.HINHDANGs.InsertOnSubmit(hd);
            data.SubmitChanges();
            return RedirectToAction("List", "Shape");
        }
        public ActionResult Update(int id)
        {
            var MaHinhDang = data.HINHDANGs.First(m => m.MaHinhDang == id);
            return View(MaHinhDang);
        }
        [HttpPost]
        public ActionResult Update(int id, FormCollection collection)
        {
            var MaHinhDang = data.HINHDANGs.First(m => m.MaHinhDang == id);
            var TenHinhDang = collection["TenHinhDang"];
            MaHinhDang.TenHinhDang = TenHinhDang.ToString();
            UpdateModel(MaHinhDang);
            data.SubmitChanges();
            return RedirectToAction("List", "Shape");
        }
        public ActionResult Delete(int id)
        {
            var MaHinhDang = data.HINHDANGs.First(m => m.MaHinhDang == id);
            return View(MaHinhDang);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var MaHinhDang = data.HINHDANGs.First(m => m.MaHinhDang == id);
            data.HINHDANGs.DeleteOnSubmit(MaHinhDang);
            data.SubmitChanges();
            return RedirectToAction("List", "Shape");
        }
    }
}