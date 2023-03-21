using DOANLAPTRINHWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANLAPTRINHWEB.Controllers
{
    public class BillController : Controller
    {
        // GET: Bill
        dbWatchDataContext data = new dbWatchDataContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TatCaDonHang()
        {
            KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
            if (kh == null)
            {
                return RedirectToAction("Login", "User");
            }
            var all_Bill = data.HOADONs.Where(u => u.IDKhachHang == kh.MaKhachHang).ToList();
            return View(all_Bill);
        }

        public ActionResult ChiTietDonHang(int? id)
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "User");
            }
            var ct_DonHang = data.CTHOADONBANDONGHOs.Where(m => m.IDHoaDon == id).ToList();
            var hd = data.HOADONs.Where(p => p.MaHoaDon == id).FirstOrDefault();
            ViewBag.MaDonHang = hd.MaHoaDon;
            ViewBag.NgayDat = hd.NgayLap;
            ViewBag.TinhTrang = hd.TINHTRANGDONHANG.TenTinhTrang;
            ViewBag.TongTien = hd.TongTien; 
            return View(ct_DonHang);
        }


    }
}