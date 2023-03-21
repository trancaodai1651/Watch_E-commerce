using DOANLAPTRINHWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANLAPTRINHWEB.Areas.Admin.Controllers
{
    [CustomAuthorization(Order = 1)]
    public class GlassSurfaceController : Controller
    {
        dbWatchDataContext data = new dbWatchDataContext();
        public ActionResult List()
        {
            var L_GlassSurface = from r in data.CHATLIEUMATKINHs select r;
            return View(L_GlassSurface);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(FormCollection collection, CHATLIEUMATKINH cl)
        {
            var TenChatLieuMatKinh = collection["TenChatLieuMatKinh"];
            cl.TenChatLieuMatKinh = TenChatLieuMatKinh.ToString();
            data.CHATLIEUMATKINHs.InsertOnSubmit(cl);
            data.SubmitChanges();
            return RedirectToAction("List", "GlassSurface");
        }
        public ActionResult Update(int id)
        {
            var MaChatLieuMatKinh = data.CHATLIEUMATKINHs.First(m => m.MaChatLieuMatKinh == id);
            return View(MaChatLieuMatKinh);
        }
        [HttpPost]
        public ActionResult Update(int id, FormCollection collection)
        {
            var MaChatLieuMatKinh = data.CHATLIEUMATKINHs.First(m => m.MaChatLieuMatKinh == id);
            var TenChatLieuMatKinh = collection["TenChatLieuMatKinh"];
            MaChatLieuMatKinh.TenChatLieuMatKinh = TenChatLieuMatKinh.ToString();
            UpdateModel(MaChatLieuMatKinh);
            data.SubmitChanges();
            return RedirectToAction("List", "GlassSurface");
        }
        public ActionResult Delete(int id)
        {
            var MaChatLieuMatKinh = data.CHATLIEUMATKINHs.First(m => m.MaChatLieuMatKinh == id);
            return View(MaChatLieuMatKinh);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var MaChatLieuMatKinh = data.CHATLIEUMATKINHs.First(m => m.MaChatLieuMatKinh == id);
            data.CHATLIEUMATKINHs.DeleteOnSubmit(MaChatLieuMatKinh);
            data.SubmitChanges();
            return RedirectToAction("List", "GlassSurface");
        }
    }
}