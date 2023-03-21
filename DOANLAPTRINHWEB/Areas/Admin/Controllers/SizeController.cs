using DOANLAPTRINHWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANLAPTRINHWEB.Areas.Admin.Controllers
{
    [CustomAuthorization(Order = 1)]
    public class SizeController : Controller
    {
        dbWatchDataContext data = new dbWatchDataContext();
        public ActionResult List()
        {
            var L_Size = from r in data.KICHTHUOCs select r;
            return View(L_Size);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(FormCollection collection, KICHTHUOC kt)
        {
            var TenKichThuoc = collection["TenKichThuoc"];
            kt.TenKichThuoc = TenKichThuoc.ToString();
            data.KICHTHUOCs.InsertOnSubmit(kt);
            data.SubmitChanges();
            return RedirectToAction("List", "Size");
        }
        public ActionResult Update(int id)
        {
            var MaKichThuoc = data.KICHTHUOCs.First(m => m.MaKichThuoc == id);
            return View(MaKichThuoc);
        }
        [HttpPost]
        public ActionResult Update(int id, FormCollection collection)
        {
            var MaKichThuoc = data.KICHTHUOCs.First(m => m.MaKichThuoc == id);
            var TenKichThuoc = collection["TenKichThuoc"];
            MaKichThuoc.TenKichThuoc = TenKichThuoc.ToString();
            UpdateModel(MaKichThuoc);
            data.SubmitChanges();
            return RedirectToAction("List", "Size");
        }
        public ActionResult Delete(int id)
        {
            var MaKichThuoc = data.KICHTHUOCs.First(m => m.MaKichThuoc == id);
            return View(MaKichThuoc);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var MaKichThuoc = data.KICHTHUOCs.First(m => m.MaKichThuoc == id);
            data.KICHTHUOCs.DeleteOnSubmit(MaKichThuoc);
            data.SubmitChanges();
            return RedirectToAction("List", "Size");
        }
    }
}