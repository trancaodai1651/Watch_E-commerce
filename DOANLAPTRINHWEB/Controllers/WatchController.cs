using DOANLAPTRINHWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANLAPTRINHWEB.Controllers
{
    public class WatchController : Controller
    {
        dbWatchDataContext data = new dbWatchDataContext();
        public ActionResult Index(int gioitinh = 0)
        {
            var I_Watch = from r in data.DONGHOs select r;
            return View(I_Watch);
        }
        public ActionResult Product(string SearchProduct = "", int thuonghieu = 0, int loaiday = 0, int chatlieu = 0, int hinhdang = 0, int kichthuoc = 0, int mucdo = 0, int gioitinh = 0)
        {
            if(SearchProduct != "")
            {
                var search_Watch = data.DONGHOs.Where(p => p.TenDongHo.ToUpper().Contains(SearchProduct.ToUpper())).ToList();
                return View(search_Watch);
            }

            if(thuonghieu != 0)
            {
                var filterThuongHieu = data.DONGHOs.Where(p => p.IDThuongHieu == thuonghieu).ToList();
                return View(filterThuongHieu);
            }
            if (loaiday != 0)
            {
                var filterLoaiDay = data.DONGHOs.Where(p => p.IDLoaiDay == loaiday).ToList();
                return View(filterLoaiDay);
            }
            if (chatlieu != 0)
            {
                var filterChatLieu = data.DONGHOs.Where(p => p.IDChatLieuMatKinh == chatlieu).ToList();
                return View(filterChatLieu);
            }
            if (hinhdang != 0)
            {
                var filterHinhDang = data.DONGHOs.Where(p => p.IDHinhDangMatSo == hinhdang).ToList();
                return View(filterHinhDang);
            }
            if (kichthuoc != 0)
            {
                var filterKichThuoc = data.DONGHOs.Where(p => p.IDKichThuocMatSo == kichthuoc).ToList();
                return View(filterKichThuoc);
            }
            if (mucdo != 0)
            {
                var fillterMucDo = data.DONGHOs.Where(p => p.IDMucDoChongNuoc == mucdo).ToList();
                return View(fillterMucDo);
            }

            if(gioitinh != 0)
            {
                var fillterGioiTinh = data.DONGHOs.Where(p => p.IDGioiTinh == gioitinh).ToList();
                return View(fillterGioiTinh);
            }
            var P_Watch = from r in data.DONGHOs select r;
            return View(P_Watch);
        }
        public ActionResult ProductDetail(int? id)
        {
            var PD_Watch = data.DONGHOs.Where(m => m.MaDongHo == id).First();
            return View(PD_Watch);
        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}