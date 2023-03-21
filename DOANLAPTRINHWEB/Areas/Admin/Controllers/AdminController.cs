using DOANLAPTRINHWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANLAPTRINHWEB.Areas.Admin.Controllers
{
    [CustomAuthorization(Order = 1)]
    public class AdminController : Controller
    {
        dbWatchDataContext data = new dbWatchDataContext();
        public ActionResult List()
        {
            var L_Admin = from r in data.QUANTRIs select r;
            return View(L_Admin);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Register(FormCollection collection, QUANTRI qt, TAIKHOAN tk)
        {
            //gan gia tri vao form
            var TenQuanTri = collection["HotenQT"];
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            var email = collection["Email"];
            var diachi = collection["DiaChi"];
            var sdt = collection["DienThoai"];
            if (string.IsNullOrEmpty(TenQuanTri) || string.IsNullOrEmpty(tendn) || string.IsNullOrEmpty(matkhau)
                 || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sdt))
            {
                ViewData["Error"] = "Vui lòng nhập đầy đủ thông tin";
            }
            else
            {
                //Them Tai Khoan
                tk.MaTaiKhoan = tendn;
                tk.MatKhau = matkhau;
                tk.Email = email;
                tk.IDVaiTro = 1;
                data.TAIKHOANs.InsertOnSubmit(tk);
                data.SubmitChanges();

                //Them Quan Tri
                qt.TenQuanTri = TenQuanTri;
                qt.DiachiQT = diachi;
                qt.DienThoaiQT = sdt;
                qt.IDTaiKhoanQT = tendn;
                data.QUANTRIs.InsertOnSubmit(qt);
                data.SubmitChanges();
                return RedirectToAction("List", "Admin");
            }
            return this.Register();
        }
        public ActionResult Logout()
        {
            Session["QuanTri"] = null;
            return RedirectToAction("Login", "User", new { area = ""});
        }

        public ActionResult Thongtintaikhoan(int id)
        {
            var D_detail = data.QUANTRIs.Where(m => m.MaQuanTri == id).First();
            return View(D_detail);
        }
        public ActionResult Update(int id)
        {
            var MaQuanTri = data.QUANTRIs.First(m => m.MaQuanTri == id);
            return View(MaQuanTri);
        }
        [HttpPost]
        public ActionResult Update(int id, FormCollection collection)
        {
            var QuanTri = data.QUANTRIs.First(m => m.MaQuanTri == id);
            var TaiKhoan = data.TAIKHOANs.First(m => m.MaTaiKhoan == QuanTri.IDTaiKhoanQT);
            //gan gia tri vao form
            var TenQuanTri = collection["TenQuanTri"];
            var DiachiQT = collection["DiachiQT"];
            var DienThoaiQT = collection["DienThoaiQT"];
            var MatKhau = collection["TAIKHOAN.MatKhau"];
            var Email = collection["TAIKHOAN.Email"];

            TaiKhoan.MatKhau = MatKhau.ToString();
            TaiKhoan.Email = Email.ToString();
            QuanTri.TenQuanTri = TenQuanTri.ToString();
            QuanTri.DiachiQT = DiachiQT.ToString();
            QuanTri.DienThoaiQT = DienThoaiQT.ToString();

            UpdateModel(TaiKhoan);
            UpdateModel(QuanTri);
            data.SubmitChanges();
            return RedirectToAction("List", "Admin");
        }
        public ActionResult Delete(int id)
        {
            var MaQuanTri = data.QUANTRIs.First(m => m.MaQuanTri == id);
            return View(MaQuanTri);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var MaQuanTri = data.QUANTRIs.First(m => m.MaQuanTri == id);
            data.QUANTRIs.DeleteOnSubmit(MaQuanTri);
            data.SubmitChanges();
            return RedirectToAction("List", "Admin");
        }
    }
}