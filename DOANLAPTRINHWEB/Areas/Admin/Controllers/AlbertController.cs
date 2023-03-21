using DOANLAPTRINHWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANLAPTRINHWEB.Areas.Admin.Controllers
{
    [CustomAuthorization(Order = 1)]
    public class AlbertController : Controller
    {
        dbWatchDataContext data = new dbWatchDataContext();
        public ActionResult List()
        {
            var L_Albert = from r in data.LOAIDAYs select r;
            return View(L_Albert);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(FormCollection collection, LOAIDAY ld)
        {
            var TenLoaiDay = collection["TenLoaiDay"];
            ld.TenLoaiDay = TenLoaiDay.ToString();
            data.LOAIDAYs.InsertOnSubmit(ld);
            data.SubmitChanges();
            return RedirectToAction("List", "Albert");
        }
        public ActionResult Update(int id)
        {
            var MaLoaiDay = data.LOAIDAYs.First(m => m.MaLoaiDay == id);
            return View(MaLoaiDay);
        }
        [HttpPost]
        public ActionResult Update(int id,FormCollection collection)
        {
            var MaLoaiDay = data.LOAIDAYs.First(m => m.MaLoaiDay == id);
            var TenLoaiDay = collection["TenLoaiDay"];
            MaLoaiDay.TenLoaiDay = TenLoaiDay.ToString();
            UpdateModel(MaLoaiDay);
            data.SubmitChanges();
            return RedirectToAction("List", "Albert");
        }
        public ActionResult Delete(int id)
        {
            var MaLoaiDay = data.LOAIDAYs.First(m => m.MaLoaiDay == id);
            return View(MaLoaiDay);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var MaLoaiDay = data.LOAIDAYs.First(m => m.MaLoaiDay == id);
            data.LOAIDAYs.DeleteOnSubmit(MaLoaiDay);
            data.SubmitChanges();
            return RedirectToAction("List", "Albert");
        }
    }
}