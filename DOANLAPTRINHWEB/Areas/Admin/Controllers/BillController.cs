using DOANLAPTRINHWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANLAPTRINHWEB.Areas.Admin.Controllers
{
    [CustomAuthorization(Order = 1)]
    public class BillController : Controller
    {
        dbWatchDataContext data = new dbWatchDataContext();
        public ActionResult List()
        {
            var L_Bill = from r in data.HOADONs select r;
            return View(L_Bill);
        }
        public ActionResult Detail(int? id)
        {
            var hd = data.HOADONs.Where(p => p.MaHoaDon == id).FirstOrDefault();
            var ct_DonHang = data.CTHOADONBANDONGHOs.Where(m => m.IDHoaDon == id).ToList();
            ViewBag.MaDonHang = hd.MaHoaDon;
            ViewBag.NgayDat = hd.NgayLap;
            ViewBag.TinhTrang = hd.TINHTRANGDONHANG.TenTinhTrang;
            ViewBag.TongTien = hd.TongTien;
            return View(ct_DonHang);
        }

        public ActionResult CapNhatTrangThai(int? idDonHang, int? idTrangThai)
        {
            if (idDonHang != null && idTrangThai != null)
            {
                var donHang = data.HOADONs.Where(p => p.MaHoaDon == idDonHang).FirstOrDefault();
                switch (idTrangThai)
                {
                    case 1:
                        donHang.TinhTrang = 2; //Xác nhận
                        UpdateModel(donHang);
                        data.SubmitChanges();
                        break;
                    case 2:
                        donHang.TinhTrang = 3; //Giao Hàng
                        UpdateModel(donHang);
                        data.SubmitChanges();
                        break;
                    case 3:
                        donHang.TinhTrang = 4; //Hoàn thành
                        UpdateModel(donHang);
                        data.SubmitChanges();
                        break;

                }
            }
            return RedirectToAction("List", "Bill");
        }

        public ActionResult HuyDonHang(int? idDonHang)
        {
            if (idDonHang != null)
            {
                var donHang = data.HOADONs.Where(p => p.MaHoaDon == idDonHang).FirstOrDefault();
                donHang.TinhTrang = 5;
                UpdateModel(donHang);
                data.SubmitChanges();
            }
            return RedirectToAction("List", "Bill");
        }
    }
}